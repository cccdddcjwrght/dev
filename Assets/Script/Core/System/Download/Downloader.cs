//*************************************************************************
//	创建日期:	2017-10-26
//	文件名称:	Downloader.cs
//  创 建 人: Silekey
//	版权所有:	LT
//	说    明: 提供多线程网络下载功能
// 支持的功能有:
// 1. 支持大文件下载
// 2. https
// 3. 支持配置同时下载数量与失败尝试次数
// 注意:
// 为了防止玩家更新到非官方的更新资源, 可以用HTTPS给每个网站签发证书. （目前该证书检测功能还未做)
// fix
// 2023-02-14 提供下载暂停功能 
//*************************************************************************
using System;
using UnityEngine;
using System.Threading;

public delegate void DOWNLOAD_SIZE(IDownloadTask task, long addSize); // 下载回调
public interface IDownloadTask
{
    /// <summary>
    /// 执行下载任务
    /// </summary>
    /// <param name="buff">下载提供的缓存</param>
    /// <param name="wait">等待函数</param>
    /// <returns></returns>
    bool Do(byte[] buff, Action wait);
    
    /// <summary>
    /// 释放资源
    /// </summary>
    void Dispose();
    
    /// <summary>
    /// 已下载数据大小
    /// </summary>
    long downloadSize   { get; }
    
    /// <summary>
    /// 错误提示
    /// </summary>
    string error        { get; }
    
    /// <summary>
    /// 远程URL
    /// </summary>
    string url          { get; } 
    
    /// <summary>
    /// 是否下载结束
    /// </summary>
    bool isDone         { get; }
}

public interface IDownload
{
    void Task_FileDownloadSize  (IDownloadTask task, long addSize);
    void Task_DownloadFail      (IDownloadTask task);
}

// 下载器管理类
public class Downloader : IDownload
{
    const int BUFF_SIZE         = 6000; // 6kb BUFF大小

    // 下载超时
    private int _timeOut        = 10000;  // 10秒
    
    public int timeOut { get { return _timeOut; } set { _timeOut = value; } }
    

    // 获得当前已下载文件大小(thread safe)
    public long downloadSize { get {  return m_downloadSize; } }

    // 获得下载失败的下载器
    public IDownloadTask[] GetDownloadFail()
    {
        if (m_failTask.size <= 0)
            return null;

        long size = m_failTask.size;
        IDownloadTask[] ret = null;
        lock (m_failTask)
        {
            if (m_failTask.size > 0)
            {
                ret = new IDownloadTask[m_failTask.size];
                for (int i = 0; i < m_failTask.size; i++)
                {
                    ret[i] = m_failTask[i];
                }
            }
        }

        return ret;
    }

    // 获得下载的第一个错误
    public string error
    {
        get
        {
            if (m_failTask.size <= 0)
                return null;

            string e = null;
            lock(m_failTask)
            {
                IDownloadTask task = m_failTask.Peek();
                e = task.error;
            }
            
            return e;
        }
    }


    // 下载剩余数量
    public int  taskNum { get { return m_task.size; } }

    // 最大可同时下载的任务
    private int _maxTaskNum = 1;
    public int  maxTaskNum { get { return _maxTaskNum;  } set { _maxTaskNum = value;  } }

    // 下载失败可尝试的次数
    private int _errorTryNum = 1;
    
    public int errorTryNum { get { return _errorTryNum; } set { _errorTryNum = value; } }

    private BetterList<IDownloadTask> m_task        = new BetterList<IDownloadTask>();   // 还未下载任务(未完成的任务才在这里面)
    private BetterList<IDownloadTask> m_allTask     = new BetterList<IDownloadTask>();   // 所有的下载任务
    private BetterList<IDownloadTask> m_failTask    = new BetterList<IDownloadTask>();   // 失败了的下载任务

    private Thread[]                  m_Worker                  ;                       // 干活的线程
    private int                       m_workerFinish            ;                       // 结束的线程数量
    private long                      m_downloadSize            ;                       // 下载的总文件大小
    private volatile bool             m_isPause                 ;                       // 是否暂停
    private ManualResetEvent          m_pauseEvent              ;                       // 暂停事件


    const int                         CONNECT_LIMIT         = 10;

    public Downloader()
    {
        Debug.Log("connect limit=" + System.Net.ServicePointManager.DefaultConnectionLimit.ToString());
        System.Net.ServicePointManager.DefaultConnectionLimit = CONNECT_LIMIT;
        m_isPause       = false;
        m_pauseEvent    = new ManualResetEvent(true);
    }

    // 判断下载是否正常结束
    public bool isDone { get { return m_Worker == null || m_Worker.Length == m_workerFinish;  } }

    // 获得任务thread safe
    IDownloadTask PopTask()
    {
        IDownloadTask obj = null;
        lock(m_task)
        {
            obj = m_task.Pop();
        }
        return obj;
    }

    // 插入任务(thread safe)
    void PushTask(IDownloadTask task)
    {
        lock(m_task)
        {
            m_task.Add(task);
        }

        lock(m_allTask)
        {
            m_allTask.Add(task);
        }

        //Interlocked.Increment(ref _count);
    }

    // 添加web下载任务
    public bool AddHttpDownload(string url, string filePath)
    {
        // 1. 生成一个任务丢到队列里
        var task = new WebDownloadTask(this, url, filePath, timeOut);
        PushTask(task);
        return true;
    }

    // 更新下载文件大小
    public void Task_FileDownloadSize(IDownloadTask task, long addSize)
    {
        Interlocked.Add(ref m_downloadSize, addSize);
    }

    public void Task_DownloadFail(IDownloadTask task)
    {
        lock(m_failTask)
        {
            m_failTask.Add(task);
        }
    }

    // 开始下载
    public void Start()
    {
        if (isDone == false)
        {
            Debug.LogError("download task is running!");
            return;
        }

        // 开启线程
        m_workerFinish = 0;
        m_downloadSize = 0;
        m_Worker = new Thread[maxTaskNum];
        for (int i = 0; i < _maxTaskNum; i++)
        {
            m_Worker[i] = new Thread(ThreadDownloader);
            m_Worker[i].Start();
        }
    }

    // 等待下载
    private void WaitDownload()
    {
        if (m_isPause)
        {
            m_pauseEvent.WaitOne();
        }
    }

    // 下载线程
    private void ThreadDownloader()
    {
        int         tryNum          = errorTryNum;
        byte[]      buffer          = new byte[BUFF_SIZE];
        Action      delegate_wait   = WaitDownload;

        // 1. 领取下载任务
        for (IDownloadTask task = PopTask(); task != null; task = PopTask())
        {
            // 最多尝试N次下载
            for (int i = 0; i < tryNum; i++)
            {
                if (task.Do(buffer, delegate_wait))
                {
                    // 成功下载就无需接着下载
                    break;
                }

                // 每下载完一个就调用下垃圾回收
                System.GC.Collect();
            }
        }

        // 2. 线程结束
        Interlocked.Increment(ref m_workerFinish);
    }

    void ForceStopThread(Thread t)
    {
        while (t != null && t.IsAlive)
        {
            t.Abort();
            t.Join(10);
        }
    }

    // 挂起下载中的线程
    public bool Pause
    {
        get
        {
            return m_isPause;
        }

        set
        {
            if (m_isPause != value)
            {
                m_isPause = value;
                if (value)
                    m_pauseEvent.Reset();
                else
                    m_pauseEvent.Set();
            }
        }
    }

    // 强制终止所有下载
    public void Stop(bool clearTask = true)
    {
        if (m_Worker != null)
        {
            Pause = false;
            lock (m_task)
            {
                // 对线程进行终止
                for (int i = 0; i < m_Worker.Length; i++)
                {
                    if (m_Worker[i].IsAlive)
                        m_Worker[i].Abort();
                }
            }

            for (int i = 0; i < m_Worker.Length; i++)
            {
                ForceStopThread(m_Worker[i]);
            }
            m_Worker = null;
        }

        // 释放所有下载任务数据
        if (clearTask == true)
        {
            for (int i = 0; i < m_allTask.size; i++)
            {
                m_allTask[i].Dispose();
            }
            m_allTask.Clear();
            m_task.Clear();
            m_failTask.Clear();
        }
        m_workerFinish = 0;
    }

    // 不判断证书, 后续需要加入证书判断防止客户端被DNS劫持与中间人攻击
    public static void AllowAllHttpsWeb()
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
    }
    static bool CheckValidationResult(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors)
    {
        // allow All
        return true;
    }
}
