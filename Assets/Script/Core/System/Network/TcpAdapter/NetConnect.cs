//*************************************************************************
//	创建日期:	2016-8-22
//	文件名称:	NetConnect.cs
//  创 建 人:   Silekey
//	版权所有:	LT
//	说    明:	SOCKET封装类
//*************************************************************************

using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using MemUtils;
using System.Runtime.InteropServices;

// 除了socket之外其他都不处理
namespace GameNet {
	public enum CONN_STATE {	// 连接状态
        //NONE = 1, 		// 无操作
		UNCONNECT = 0,	 	// 未连接
        CONNECTED = 1,  // 已连接
        CONNING,		// 连接中
		CONNECTFAIL, 	// 连接失败
		CLOSE,			// 已关闭(主动关闭)
        PASSIVE_CLOSE,  // 被动关闭
    }

	// 消息接口
	public interface IConnect {
		NET_ERR		Connect (string host, int port);                    // 连接
                                                                        //NET_ERR		SendPackage (Package pkg);							// 发送消息
                                                                        //NET_ERR		SendMessage (ushort msgId, byte[] datas);			// 发送消息
        NET_ERR Send(byte[] data, int start_pos, int len);              // 发送消息

		CONN_STATE	GetConnState();										// 获取连接状态
		bool		IsConnect(); 										// 判断是否连接
		void		Close(bool isNotify = true);						// 关闭socket连接
		void		Clear();											// 清空资源
		bool		UpdatePackage(int maxProcess);						// 处理接收的消息包
        bool        IsAlive();                                          // 判断是否是有效的对象
        int         GetSocketErr();                                      
	}

	// 封装TCP socket的连接
	public class NetConnect {//: IConnect {
        public const int SOCKET_SEND_BUFFER = 1024 * 32; // 32K 的BUFFER已经很够了把
        public const int SOCKET_RECIVE_BUFFER = 1024 * 32; // 
        public Action<NetConnect, CONN_STATE, CONN_STATE> onStateChange;
        CircleBuffer mSendBuffer;                       // 消息发送缓存
        CircleBuffer mRecvBuffer;				        // 消息接收缓存	
		Socket			m_Socket;    					// socket 对象
		ManualResetEvent m_ManualSendEvent;  			// 发送消息的线程状态
        SimpleBuffer     mBuffer;
        int              mPackMaxSize;                  // 一个包的最大大小

        volatile int mConnState; // 连接状态
        volatile int mNextConnState;
        IPEndPoint  mIpAddress;

		// 线程相关
		Thread mReciveThread;
		Thread mSendthread;

        volatile NET_ERR mConnFail;
		volatile int 	mSocketErrorCode;

        ~NetConnect() {
            Disponse();
        }


        public void Initinalize(int readBuff, int writeBuff, int maxPackageSize) {
            mSocketErrorCode = 0;
            //mController = controller;
            mSendBuffer = new CircleBuffer();
            mRecvBuffer = new CircleBuffer();
            mSendBuffer.Initialize(readBuff);
            mRecvBuffer.Initialize(writeBuff);
            this.mPackMaxSize = maxPackageSize;
            mBuffer = new SimpleBuffer();//new byte[maxPackageSize];
            mBuffer.Initialize(mPackMaxSize);
            m_ManualSendEvent = new ManualResetEvent(false);
            mNextConnState = 0;
            mConnState = 0;
        }

        public bool IsConnect() {

            return mConnState == (int)CONN_STATE.CONNECTED;
            //return m_Socket != null && m_Socket.Connected;
		}

		public NET_ERR Connect(string host, int port) {
			Close ();

			Interlocked.Exchange(ref mConnState, (int)CONN_STATE.CONNING);

			try
			{
                IPAddress address;
                if (false == IPAddress.TryParse(host, out address))
                {
                    Interlocked.Exchange(ref mNextConnState, (int)CONN_STATE.CONNECTFAIL);
                    return NET_ERR.ARGUMENT_FAIL;
                }
                mIpAddress = new IPEndPoint(address, port);
            }
			catch (System.Exception excep)
			{
				Debug.LogError("NetTCPSocketConnect::Connect DNS Error:" + excep.ToString());
				Interlocked.Exchange (ref mNextConnState, (int)CONN_STATE.CONNECTFAIL);
				return NET_ERR.CONN_DNS_FAIL;
			}

			mSendthread = new Thread (ThreadConnect);
			mSendthread.Start ();
			return NET_ERR.SUCCESS;
		}

        // 发送消息
        public NET_ERR Send(byte[] data, int start_pos, int len)
        {
            int write_len = mSendBuffer.Write(data, start_pos, len);
            m_ManualSendEvent.Set();

            if (write_len != len)
            {
                Close();
                return NET_ERR.OUT_OF_MEMORY; // 内存不足
            }

            return NET_ERR.SUCCESS;
        }

		static void CloseThread (ref Thread th) {
            Thread t = th;
			while (t != null && t.IsAlive) {
                t.Abort();
                Debug.LogWarning("Abort Thread Close!" + t.ManagedThreadId.ToString());
                t.Join(10); // 直接等待线程结束
            }

            t = null;
            th = null;
		}

        // 安全等待线程结束 
        static bool SafeWaitThread(Thread th, int milliseconds)
        {
            if (th == null || th.IsAlive == false)
                return true;

            return th.Join(milliseconds);
        }

        // 关闭连接
        public void Close(bool isNotify = true) {
            try
            {
                // 关闭socket
                if (m_Socket != null)
                {
                    if (m_Socket.Connected)
                    {
                        Interlocked.Exchange(ref mNextConnState, (int)CONN_STATE.CLOSE);
                        /*
					    if (isNotify) {
						    UpdateConnState ();
					    }
                        */
                        m_Socket.Shutdown(SocketShutdown.Both);
                    }
                    m_Socket.Close();
                }
                if (m_ManualSendEvent != null)
                {
                    m_ManualSendEvent.Set(); // 激活关闭
                }

                // 正常等待线程结束
                SafeWaitThread(mReciveThread, 20);
                SafeWaitThread(mSendthread, 20);

                // 强制关闭线程
                CloseThread(ref mReciveThread);
                CloseThread(ref mSendthread);

                // 清空缓存(回收给内存!)
                ClearAllBuffer();

                // 修改状态
                mIpAddress = null;
                m_Socket = null;
                //System.GC.Collect();
            }
            catch (Exception e)
            {
                Debug.LogError("close net connet =" + e.ToString());
            }
		}

        void Disponse()
        {
            if (m_Socket != null) {
                m_Socket.Close();
                m_Socket = null;
            }

            ClearAllBuffer();
            mSendBuffer = null;
            mRecvBuffer = null;
            mBuffer = null;
            if (m_ManualSendEvent != null)
            {
                m_ManualSendEvent.Close();
                m_ManualSendEvent = null;
            }
        }

		public void Clear() {
            Close(false);
			mSendBuffer = null;
			mRecvBuffer = null;
			//mController = null;
            mBuffer = null;

            if (m_ManualSendEvent != null)
            {
                m_ManualSendEvent.Close();
                m_ManualSendEvent = null;
            }
		}

        void FreeBuffer(CircleBuffer buff)
        {
            // 现在不需要回收了
            buff.Clear();
        }

        void ClearAllBuffer()
        {
            FreeBuffer(mSendBuffer);
            FreeBuffer(mRecvBuffer);
        }

		// socket 发送消息
		private static int SocketSendData(Socket socket, byte[] datas, int len) {
			for (int nSendData = 0; nSendData < len;) {
				int ret = 0;
                ret = socket.Send (datas, nSendData, len - nSendData, SocketFlags.None);
				if (ret <= 0) {
					return ret;
				}

				nSendData += ret;
			}

			return len;
		}

		// socket 被动关闭
		private void PassiveSocketClose() {
            if (m_ManualSendEvent != null) {
                m_ManualSendEvent.Set(); // 激活关闭
            }

            if (IsCloseState((CONN_STATE)mNextConnState) == false &&  IsCloseState((CONN_STATE)mConnState) == false)
            {
                // 非关闭状态下换成被动关闭
                Interlocked.Exchange(ref mNextConnState, (int)CONN_STATE.PASSIVE_CLOSE);
            }

			// 只有连接状态才能转换为被动
		}

        public static bool IsCloseState(CONN_STATE state)
        {
            return state == CONN_STATE.CLOSE || state == CONN_STATE.PASSIVE_CLOSE;
        }

		// 线程是否存活
		public bool IsAlive() {
			CONN_STATE cur = (CONN_STATE)mConnState;
			CONN_STATE next = (CONN_STATE)mNextConnState;

			if (IsCloseState(next) || IsCloseState(cur)) {
				return false;
			}

			if (cur == CONN_STATE.CONNING 
                || cur == CONN_STATE.CONNECTED 
                || next == CONN_STATE.CONNING 
                || next == CONN_STATE.CONNECTED) {
				return true;
			}

			return false;
		}

		// 消息发送线程
		private void ThreadSender() {
            byte[] buff = new byte[SOCKET_SEND_BUFFER];//this.mPackMaxSize];// (int)PACK.MAX_SIZE];
            Socket socket = m_Socket;

            try
            {
                while (IsAlive())
                {
                    m_ManualSendEvent.WaitOne();    // 等待数据通知
                    m_ManualSendEvent.Reset();      // 重置等待

                    for (int readLen = mSendBuffer.Read(buff, 0, buff.Length); 
                        readLen > 0 && IsAlive(); 
                        readLen = mSendBuffer.Read(buff, 0, buff.Length))
                    {
                        // 发送消息
                        int ret = SocketSendData(socket, buff, readLen);
                        if (ret <= 0)
                        {
                            // socket 已经关闭
                            PassiveSocketClose();
                            return;
                        }
                    }
                }
            }
            catch(SocketException e)
            {
                Interlocked.CompareExchange(ref mSocketErrorCode, e.ErrorCode, 0);
                mConnFail = NET_ERR.SOCKET_ERR;
            }
            catch(Exception e)
            {
                Debug.LogError("socket send exception=" + e.ToString());
            }
            finally
            {
                socket.Close();
                PassiveSocketClose();
                FreeBuffer(mSendBuffer);
            }
        }

		// 消息接收线程
		private void ThreadReciver() {
            byte[] buff = new byte[SOCKET_RECIVE_BUFFER];

            Socket mSocket = m_Socket;
			try
			{
				while (IsAlive()) {
                    // 接收消息头
                    // 一次尽量多读些数据
                    int recvLen = mRecvBuffer.writeSize;
                    recvLen = recvLen <= buff.Length ? recvLen : buff.Length;
                    if (recvLen == 0)
                    {
                        Thread.Sleep(1);
                        continue;
                    }

                    recvLen = mSocket.Receive (buff, 0 , recvLen, SocketFlags.None);
					if (recvLen <= 0) {
						PassiveSocketClose ();
						break;
					}

                    // 写入缓存
                    mRecvBuffer.Write(buff, 0, recvLen);
                }
			}
			catch (SocketException e) {
                Interlocked.CompareExchange(ref mSocketErrorCode, e.ErrorCode, 0);
                mConnFail = NET_ERR.SOCKET_ERR;
			}
			catch (Exception e) {
				Debug.LogError ("recv data fail!" + e.Message);
			}
            finally
            {
                mSocket.Close();
                PassiveSocketClose();
                FreeBuffer(mRecvBuffer);
                buff = null;
            }
        }

        public bool isIPV4
        {
            get;set;
        }

		// socket连接线程
		private void ThreadConnect() {
			mConnFail = NET_ERR.SUCCESS;

            if (this.isIPV4)
            {
                this.m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            else
            {
                this.m_Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            }

			try {
				GameDebug.Log(mIpAddress.ToString());
                m_Socket.Connect(mIpAddress);
			}
			catch (ArgumentNullException) {
				mConnFail = NET_ERR.ARGUMENT_FAIL;
			}
			catch (ArgumentOutOfRangeException) {
				mConnFail = NET_ERR.ARGUMENT_FAIL;
			}
			catch (SocketException e) {
				//e.ErrorCode
				mConnFail = NET_ERR.SOCKET_ERR;
                Interlocked.Exchange(ref mSocketErrorCode, e.ErrorCode);
			}
			catch (ObjectDisposedException) {
				mConnFail = NET_ERR.SOCKET_CLOSE;
			}
			catch (NotSupportedException) {
				mConnFail = NET_ERR.UNKNOWN;
			}
			catch (ArgumentException) {
				mConnFail = NET_ERR.ARGUMENT_FAIL;
			}
			catch (InvalidOperationException) {
				mConnFail = NET_ERR.UNKNOWN;
			}
            catch (Exception)
            {
                mConnFail = NET_ERR.UNKNOWN;
            }
				
			if (m_Socket.Connected) {
				Interlocked.Exchange (ref mNextConnState, (int)CONN_STATE.CONNECTED);
			} else {
				// connect fail
				Interlocked.Exchange (ref mNextConnState, (int)CONN_STATE.CONNECTFAIL);
				Debug.LogWarning ("connect fail!");
				return;
			}

			// create recive thread
			mReciveThread = new Thread(ThreadReciver);
			mReciveThread.Start ();

			// run send, 连上了就顺便跑跑发送线程
			ThreadSender ();
		}

        public int Recive(byte[] data, int start_pos, int len)
        {
            return mRecvBuffer.Read(data, start_pos, len);
        }

		public CONN_STATE GetConnState() {
			return (CONN_STATE)mConnState;
		}

		public NET_ERR GetConnErr() {
			return mConnFail;
		}

		public int GetSocketErr() {
			return mSocketErrorCode;
		}

		// 更新连接状态
		public void UpdateConnState() {
			if (mNextConnState == 0) {
				return;
			}

			int nextState = Interlocked.Exchange (ref mNextConnState, 0);
			CONN_STATE state0 = (CONN_STATE)nextState;
			CONN_STATE state1 = (CONN_STATE)mConnState;
			if (state0 == state1) {
				return;
			}

            Interlocked.Exchange(ref mConnState, nextState);
            if (onStateChange != null)
            {
                onStateChange(this, state0, state1);
            }
		}
	}
}