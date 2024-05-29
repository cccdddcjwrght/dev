/*
 * 版本更新管理模块， 主控版本更新逻辑
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fibers;
using libx;
using log4net;
using FileMode = System.IO.FileMode;

namespace SGame
{
    public class VersionUpdater : MonoSingleton<VersionUpdater>, IDisposable
    {
        public enum STATE
        {
            INIT                 = 0,                           // 初始化
            COPY_STREAM_ASSETS      ,                           // 拷贝本地版本信息与MD5
            CHECK_VERSION           ,                           // 检测版本号
            COUNT_UPDATELIST        ,                           // 计算下载资源列表
            DOWNLOADING             ,                           // 下载更新资源

            CHECK_MD5               ,                           // 校验文件MD5
            COPY_FILES              ,                           // 拷贝文件
            RELOAD                  ,                           // 重新加载资源管理
            FAIL                    ,                           // 失败
            SUCCESS                 ,                           // 更新成功
            LASTVERSION             ,                           // 最新版本无需更新
        }

        // 错误代号
        public enum Error : int
        {
            SUCCESS             = 0,                            // 成功
            FAIL                = 1,                            // 失败
            APP_STORE           = 2,                            // 需要进入商店更新
            DOWN_LOAD_FAIL      = 3,                            // 下载失败
            LOAD_STREAM_FAIL    = 4,                            // 读取Stream Asset 资源 失败
            MD5_FAIL            = 5,                            // MD5文件校验失败
            REPAIRE             = 6,                            // 服务器维护, 停服
            NETWORK_BREAK       = 7,                            // 网络断开链接
        }

        public delegate void    UPDATE_STATE(STATE newState);   // 更新回调
        string                  m_error                     ;   // 错误信息
        Error                   m_errCode                   ;   // 错误代码
        long                    m_totalSize                 ;   // 下载总数量
        bool                    m_isPause                   ;   // 是否暂停            

        bool                    m_bCheckMd5Success          ;   // 校验下载的文件成功
        IList<VFile>            m_updateList                ;   // 更新列表
        Downloader              m_downloader                ;   // 下载器

        UPDATE_STATE            _mCbUpateState              ;
        STATE                   m_curState                  ;   // 当前更新状态
        Fiber                   m_fiber                     ;   // 协程处理类

        string                  m_saveDir                   ;   // 资源路径
        string                  m_downloadDir               ;   // 下周保存路径
        string                  m_streamDir                 ;   // STREAM ASSETS 下的资源路径
        
        GameVersion             m_RemoteVer                 ;   // 远程版本
        GameVersion             m_LocalVer  = new GameVersion();   // 本地版本
        string                  m_ServerUrl                 ;   // 远端URL

        // 是否需要重启
        public bool isNeedRestart
        {
            get
            {
                return true;
            }
        }

        // 判断是否结束
        public bool             isDone          { get { return m_curState >= STATE.FAIL; } }

        // 总共要下载的大小
        public long             totalSize       { get { return m_totalSize; } }

        // 当前下载数据大小
        public long             downloadSize    { get { return m_downloader.downloadSize; } }

        // 当前错误信息
        public string           error           { get { return m_error; } }
        
        // 显示错误代号
        public Error            errCode         { get { return m_errCode;  } }

        // 当前状态
        public STATE            state           { get { return m_curState; } }
        
        static EditorBoolValue  s_toggleUseVersionUpdater  =          
            new EditorBoolValue("updateVersion", false, false);

        // 当前游戏版本号
        public GameVersion gameVersion { get { return m_LocalVer;  } }

        private static ILog log = LogManager.GetLogger("Core.Update");
        
        public static bool useVersionUpdater
        {
            get { return s_toggleUseVersionUpdater.GetValue(); } 
            set { s_toggleUseVersionUpdater.SetValue(value);}
        }

        // 设置加载路径
        static public void PreInitalize()
        {
#if UNITY_EDITOR
            // 没开启更新流程直接进入游戏
            if (Assets.useVersionUpdate == false)
            {
                return;
            }
#endif
            Assets.updatePath = UpdateUtils.GetUpdatePath();
        }

        // 开始下载
        public void Initalize(string remoteUrl, int timeOut)
        {
            // 版本下载地址
            m_ServerUrl     = remoteUrl;
            
            // bundle 加载地址
            m_saveDir       = UpdateUtils.GetUpdatePath();
            
            // 下载地址
            m_downloadDir   = UpdateUtils.GetDownloadPath();
            
            // Unity Stream Assets 资源路径
            m_streamDir = UpdateUtils.GetStreamingAssetsPath();

#if UNITY_EDITOR
            // 没开启更新流程直接进入游戏
            if (Assets.useVersionUpdate == false)
            {
                m_LocalVer      = GameVersion.LoadFile("Assets/" + GameVersion.FileName);
                SwitchState(STATE.LASTVERSION);
                return;
            }
#endif

            // 设置使用路径
            //Assets.updatePath   = m_saveDir;
            m_downloader        = new Downloader();
            m_downloader.timeOut = timeOut;
            m_fiber             = new Fiber(Run(), FiberBucket.Manual);
        }

        // 清空目录
        static void CleanDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(path);
        }

        // 创建目录
        void MakeDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        
        string GetDownloadError()
        {
            var errHanle = m_downloader.GetDownloadFail();
            if (errHanle != null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                foreach (var handle in errHanle)
                {
                    sb.AppendFormat("url={0}, err={1}\n", handle.url, handle.error);
                }

                SetError(Error.DOWN_LOAD_FAIL, sb.ToString());
                m_downloader.Stop();
                return m_error;
            }
            m_errCode = Error.SUCCESS;
            m_error = null;
            return null;
        }

        void SetError(Error err, string error_info)
        {
            Debug.LogError("error = " + error_info);
            SwitchState(STATE.FAIL);
            m_error = error_info;
            m_errCode = err;
        }

        // 更新暂停
        public bool Pause
        {
            get
            {
                return m_isPause;
            }

            set
            {
                // 设置暂替
                if (m_isPause != value)
                {
                    m_downloader.Pause = value;
                    m_isPause          = value;
                }
            }
        }

        // 调试等待 
        static IEnumerator WaitDebug(float t)
        {
            yield return null;//FiberHelper.Wait(t);
        }

        IEnumerator WaitPause()
        {
            while (m_isPause == true)
                yield return null;
        }

        /// <summary>
        /// 是否是快速更新, 快速更新不检测资源版本号, 只要协议版本通过就通过
        /// </summary>
        private bool m_isFastUpdate = false;
        public bool IsFastUpdate
        {
            get => m_isFastUpdate;
            set
            {
                m_isFastUpdate = value;
            }
        }

        // 下载 资源信息(版本信息, hashfile, serverList
        IEnumerator LoadVersion()
        {
            SwitchState(STATE.CHECK_VERSION);
            yield return WaitDebug(1.0f);

            string downloadPath = m_downloadDir;
            MakeDirectory(downloadPath);

            string remoteTmpFile = downloadPath + GameVersion.FileName + ".tmp";
            if (File.Exists(remoteTmpFile))
                File.Delete(remoteTmpFile);
            
            // 下载GameVersion.json
            UpdateUtils.StrReturn err = new UpdateUtils.StrReturn();
            string downloadRemoteUrl = "";
            
            if (string.IsNullOrEmpty(m_LocalVer.test_remote_url))
            {
                // 正常下载
                downloadRemoteUrl = m_ServerUrl + GameVersion.FileName;
            }
            else
            {
                // 下载测试版本
                downloadRemoteUrl = m_LocalVer.test_remote_url;
            }
            yield return UpdateUtils.DownloadFile(downloadRemoteUrl, remoteTmpFile, err);

            
            if (!string.IsNullOrEmpty(err.Value))
            {
                Debug.LogError("download path=" + downloadRemoteUrl);
                SetError(Error.DOWN_LOAD_FAIL, err.Value);
                yield break;
            }
            
            // 新版本
            m_RemoteVer          = GameVersion.LoadFile(remoteTmpFile);
            if (m_RemoteVer.server_close)
            {
                SetError(Error.REPAIRE, "server is close!");
                yield break;
            }

            // 机器本地版本
            GameVersion localVer = GameVersion.LoadFile(m_saveDir + GameVersion.FileName);

            // 需要更新代码
            if (m_RemoteVer.codeVer > localVer.codeVer)
            {
                // 需要进入appstore
                //EventManager.Instance.Trigger((int)GameEvent.GAME_UPDATE_START, 1); // 通知强更
                SetError(Error.APP_STORE, "need update code");
                yield break;
            }

            // 协议版本检测
            if (m_RemoteVer.protoVer <= localVer.protoVer)
            {
                // 资源更新检测, 如果是快速更新则无需处理资源更新
                if (m_RemoteVer.buildNo <= localVer.buildNo || IsFastUpdate)
                {
                    // 最新版本
                    SwitchState(STATE.LASTVERSION);
                    yield break;
                }
            }

            // 正常进入更新
            // 拷贝文件
            FileOperator.CopyFile(remoteTmpFile, downloadPath + GameVersion.FileName, true);
            File.Delete(remoteTmpFile);
            
            // 热更通知
            //EventManager.Instance.Trigger((int)GameEvent.GAME_UPDATE_START, 0); // 通知正常更新
        }
        

        // 下载更新列表中的文件
        // @param downloadUrl, 下载的地址
        IEnumerator DownloadUpdateList(string downloadUrl)
        {
            m_downloader.Stop(true);
            
            // 添加下载列表开始下载
            foreach (var item in m_updateList)
            {
                //item.Key
                string remoteFile   = UpdateUtils.GetDownloadFileName(downloadUrl, item.name, item.hash);// Utility.assetUrl + fileName;
                string downloadFile = Path.Combine(m_downloadDir, item.name);
                m_downloader.AddHttpDownload(remoteFile, downloadFile);
            }
            m_downloader.Start();

            // 等待下载结束
            while (m_downloader.error == null && m_downloader.isDone == false)
            {
                yield return null;
            }

            // 格式化下载错误
            GetDownloadError();
        }
        

        // 线程函数, 检测下载对象是否正确
        IEnumerator Thread_CheckDownload(bool? ret)
        {
            ret = true;

            foreach (var f in m_updateList)
            {
                string  filePath    = Path.Combine(m_downloadDir, f.name);
                string  md5         = null;
                long    fileSize    = 0;
                yield return WaitPause();
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    string hash = libx.Utility.GetFileHash(fs);
                    if (!string.Equals(hash, f.hash, StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.LogError(string.Format("md5 check fail name=", f.name));
                        File.Delete(filePath);
                        ret = false;
                    }
                }
            }
        }

        // 校验下载的MD5文件是否正确
        public IEnumerator CheckDownloadFileMd5()
        {
            SwitchState(STATE.CHECK_MD5);
            yield return WaitDebug(1.0f);
            Nullable<bool> ret = true;
            yield return Thread_CheckDownload(ret);
            {
                if (!ret.Value)
                {
                    SetError(Error.MD5_FAIL, "check md5 fial fail!");
                }
            }
            yield return null;
        }

        // 线程拷贝文件
        void Thread_CopyDownloadfiles()
        {
            string downloadDir  = m_downloadDir;
            string destDir      = m_saveDir;   

            FileOperator.CopyFiles(downloadDir, destDir, true);
        }

        // 拷贝并解压下载的AB数据
        public IEnumerator CopyDownloadfiles()
        {
            SwitchState(STATE.COPY_FILES);
            yield return WaitDebug(1.0f);

            yield return null;
            Thread_CopyDownloadfiles();
        }

        // 重新加载资源管理器
        IEnumerator ReloadAssetManager()
        {
            SwitchState(STATE.RELOAD);
            yield return WaitDebug(1.0f);

            Assets.Clear();
            yield return Assets.Initialize();
            //ConfigSystem.Instance.Reload();
            //LuaSystem.Instance.Reload();
            m_LocalVer = m_RemoteVer;
            
            //EventManager.Instance.Trigger((int)GameEvent.GAME_RELOAD);
        }

        // 清空下载目录
        void Clean()
        {
            // 删除下载目录
            Directory.Delete(m_downloadDir, true); 
        }

        public void AddListener(UPDATE_STATE cbState)
        {
            _mCbUpateState = cbState;
        }

        void RemoveAllListener()
        {
            _mCbUpateState = null;
        }
        
        // 切换状态
        void SwitchState(STATE state)
        {
            if (m_curState >= STATE.FAIL)
            {
                Debug.LogError("Already Switch Fail!");
                return;
            }

            m_curState = state;
            if (_mCbUpateState != null)
            {
                _mCbUpateState(state);
            }
        }

        /// <summary>
        /// 判断本地是否有GameVersion文件, 用于判断是否首次进入
        /// </summary>
        /// <returns></returns>
        public bool GameVersionExists()
        {
            return File.Exists(m_saveDir + GameVersion.FileName);
        }

        // 将Stream中的文件解压到存储位置
        IEnumerator CopyStreamAsset()
        {
            SwitchState(STATE.COPY_STREAM_ASSETS);
            yield return WaitDebug(1.0f);
            
            // 先判断文件是否存在
            if (File.Exists(m_saveDir + GameVersion.FileName))
            {
                m_LocalVer = GameVersion.LoadFile(m_saveDir + GameVersion.FileName);
                if (m_LocalVer != null && m_LocalVer.buildNo > 0)
                {
                    // 如果文件已经存在就取消
                    yield break;
                }
            }

            // 下载version 文件
            string[] srcFiles   = new string[] { m_streamDir + Versions.Filename,   m_streamDir + GameVersion.FileName, };
            string[] destFiles  = new string[] { m_saveDir + Versions.Filename,     m_saveDir + GameVersion.FileName, };
            UpdateUtils.StrReturn err = new UpdateUtils.StrReturn();
            yield return UpdateUtils.DownloadFiles(srcFiles, destFiles, err);
            if (!string.IsNullOrEmpty(err.Value))
            {
                SetError(Error.LOAD_STREAM_FAIL, err.Value);
                yield break;
            }

            // 拷贝其他文件
            m_LocalVer                  = GameVersion.LoadFile(m_saveDir + GameVersion.FileName);
        }

        // 根据版本号获得远端版本文件名
        static string GetRemoteVersionFileName(int buildNo)
        {
            return string.Format("{0}_{1}", Versions.Filename, buildNo);
        }
        
        // 下载更新文件
        IEnumerator DownloadRemoteFiles()
        {
            // 获得更新列表
            SwitchState(STATE.COUNT_UPDATELIST); 
            yield return WaitDebug(1.0f);

            
            // 下载更新列表文件
            string remoteVersionsUrl = GetRemoteVersionFileName(m_RemoteVer.buildNo);
            string localVersionsPath = Path.Combine(m_downloadDir, Versions.Filename);
             
            // 下载文件描述信息
            string error = null;
            string[] resource_url = UpdateUtils.GetResourceUrl();
            foreach (var url in resource_url)
            {
                if (File.Exists(localVersionsPath))
                    File.Delete(localVersionsPath);

                string srcUrl = UpdateUtils.GetDownloadFileName(url, remoteVersionsUrl, null);
                UpdateUtils.StrReturn err = new UpdateUtils.StrReturn();
                yield return UpdateUtils.DownloadFile(srcUrl, localVersionsPath, err);
                error = err.Value;

                if (string.IsNullOrEmpty(error))
                    break;
                
                log.Warn("Down load Fail Url = " + srcUrl);
                break;
            }
            if (!string.IsNullOrEmpty(error))
            {
                SetError(Error.DOWN_LOAD_FAIL, "ver file downalod fail " + error);
                yield break;
            }

            // 获得更新列表
            IList<VFile> remoteFiles    = Versions.LoadVersions(localVersionsPath);
            IList<VFile> localFiles     = Versions.LoadVersions(Path.Combine(m_saveDir, Versions.Filename));
            m_updateList                = UpdateUtils.GetUpdateList(remoteFiles, localFiles);
            m_totalSize                 = UpdateUtils.GetFilesSize(m_updateList);

            // 下载文件
            SwitchState(STATE.DOWNLOADING);

            // 尝试3个CDN去下载
            foreach (var url in resource_url)
            {
                yield return DownloadUpdateList(url);
                if (m_errCode == Error.SUCCESS)
                    break;
            }
            yield return WaitDebug(1.0f);

            if (m_errCode != Error.SUCCESS)
            {
                SwitchState(STATE.FAIL);
            }
        }
        
        // 运行更新功能
        // @param localVersion 本地版本
        // @param remoteVersion 服务器版本
        IEnumerator Run()
        {
            Debug.Log("downloadPath=" + m_downloadDir);
            if (m_error != null)
            {
                Debug.LogError("down load error=" + m_error);
                SwitchState(STATE.FAIL);
                yield break;
            }
            
            // 下载远程版本号与hash文件
            yield return CopyStreamAsset();
            if (state >= STATE.FAIL)
                yield break;
            
            // 加载版本信息
            yield return LoadVersion();
            if (state >= STATE.FAIL)
                yield break;
            yield return WaitPause();


            // 进入下载
            yield return DownloadRemoteFiles();
            if (state >= STATE.FAIL)
                yield break;
            yield return WaitPause();

            // 下载更新列表中的文件(相同的文件会自动续传)
            yield return CheckDownloadFileMd5();
            if (state >= STATE.FAIL)
                yield break;

            // 拷贝下载目录到目标文件夹
            yield return CopyDownloadfiles();
            yield return WaitPause();

            // 重新加载资源
            yield return ReloadAssetManager();
            yield return WaitPause();

            // 清空下载目录
            Clean();

            // 完成状态
            SwitchState(STATE.SUCCESS);
        }

        // 自动更新
        void Update()
        {
            if (m_fiber != null)
            {
                m_fiber.Step();
            }
        }

        void OnDestory()
        {
            Dispose();
        }

        public void Dispose()
        {
            m_downloader.Stop();
            m_fiber.Terminate();
            m_fiber = null;
        }
    }
}