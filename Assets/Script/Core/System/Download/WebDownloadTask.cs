//*************************************************************************
//	创建日期:	2017-10-26
//	文件名称:	WebDownloadTask.cs
//  创 建 人:   Silekey
//	版权所有:	LT
//	说    明: 提供网络下载功能
// 支持的功能有:
// 1. 支持断点续传
// 2. https
//  断点续传逻辑: 获得远端文件的大小, 若不一致就进行断点续传. 一致认为已经下载完成了! (文件下载后的校验由其他模块完成!)
// 2023-fix, 提供下载暂停功能
//*************************************************************************
using System.IO;
using System.Net;
using System;

public class WebDownloadTask : IDownloadTask
{
    private string m_url;           // 下载文件的URL地址
    private string m_filePath;      // 下载文件的本地地址
    private long m_size;            // 文件大小
    private long m_downloadSize;        // 已下载大小
    private bool m_isDone;
    private int m_timeOut;          // 毫秒
    //private DOWNLOAD_SIZE m_downloadCb;
    const string USER_AGENT = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";

    // 下载进度（该接口thread safe)
    public float _process = 0;
    public float process { get { return _process; } }

    public long size {  get { return m_size;  } }
    public long downloadSize { get { return m_downloadSize;  } }
    public bool isDone { get { return m_isDone;  } }

    // 下载错误提示(该接口thread safe)
    public string _error;
    public string error { get { return _error;  } }
    public string url { get { return m_url;  } }
    private IDownload m_downloader;

    public WebDownloadTask(IDownload downloader, string url, string filePath, int timeOut)
    {
        m_url = url;
        m_filePath = filePath;
        m_timeOut = timeOut;
        m_size = 0;            // 文件大小
        m_downloadSize = 0;        // 已下载大小
        m_isDone = false;
        m_downloader = downloader;
    }

    // 确保文件下的目录是有效的
    static void MakeDir(string filePath)
    {
        // 获得路径
        string dirPath = Path.GetDirectoryName(filePath); 
        // 判断下载路径是否存在, 不存在就创建一个
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
    }

    /// <summary>
    /// 获取下载文件的大小
    /// </summary>
    /// <returns>The length.</returns>
    /// <param name="url">URL.</param>
    long GetLength(string url)
    {
        WebRequest requet = WebRequest.Create(url);
        requet.Method = "HEAD";
        requet.Timeout = m_timeOut;

        using (WebResponse response = requet.GetResponse())
        {
            return response.ContentLength;
        }
    }

    // 被线程调用进行下载
    // @param buff 下载用的buff, 这样每个任务就无需再创建内存了
    public bool Do(byte[] buff, Action wait)
    {
        MakeDir(m_filePath);

        // 使用流操作文件
        try
        {
            using (FileStream fs = new FileStream(m_filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                wait?.Invoke();
                long fileLength = fs.Length; // 文件长度
                long totalLength = GetLength(m_url);
                m_downloadSize = fileLength;
                m_size = totalLength;

                if (fileLength > 0 && m_downloader != null)
                    m_downloader.Task_FileDownloadSize(this, fileLength);

                if (fileLength >= totalLength)
                {
                    // 文件下载完毕
                    _process = 1;
                    fs.Close();
                    return true;
                }

                // 文件指针移到文件尾
                fs.Seek(fileLength, SeekOrigin.Begin);
                HttpWebRequest req = WebRequest.Create(m_url) as HttpWebRequest;
                //req.Proxy = null;
                req.Method = "GET";
                req.UserAgent = USER_AGENT;
                req.Timeout = m_timeOut;
                req.AddRange((int)fileLength);

                // 读取网络数据并写文件
                using (var resp = req.GetResponse())
                {
                    using (Stream stream = resp.GetResponseStream())
                    {
                        for (int length = stream.Read(buff, 0, buff.Length); length > 0; length = stream.Read(buff, 0, buff.Length))
                        {
                            wait?.Invoke();
                            
                            fs.Write(buff, 0, length);
                            fileLength      += length;
                            m_downloadSize  = fileLength;

                            if (m_downloader != null)
                                m_downloader.Task_FileDownloadSize(this, (long)length);
                            _process = (float)fileLength / totalLength;
                        }
                    }
                }
            }
        }
        catch (IOException e)
        {
            _error = e.Message;
            if (m_downloader != null)
                m_downloader.Task_DownloadFail(this);
            return false;
        }
        catch (WebException e)
        {
            _error = e.Message;
            if (m_downloader != null)
                m_downloader.Task_DownloadFail(this);
            return false;
        }
        finally
        {
            m_downloader = null;
            m_isDone = true;
        }

        return true;
    }

    public void Dispose()
    {
        m_downloader = null;
    }
}
