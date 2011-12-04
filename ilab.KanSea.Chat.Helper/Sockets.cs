/**
 * 作者	: Hikaru
 * 日期	: 2011/9/28
 * 时间	: 1:20
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using ilab.KanSea.Chat.Helper.model;

namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// Description of Sockets.
	/// </summary>
	public class Sockets
    {
        #region 声明
        //服务器监听端口
        private Int32 _listen_port = 125;//服务器监听端口
        private Int32 _send_port = 125;//发送端口
        private Int32 _headerLength = 20;//头文件大小
		private IPAddress _listen_ip;//服务器监听ip地址
		private IPAddress _server_ip;//服务器ip
		private TcpListener _listen_tcp;//tcp
        private UdpClient _listen_udp;
        private Socket _sender;
        private Hashtable _user_list = new Hashtable();
        /// <summary>
        /// 监听线程
        /// </summary>
		private Thread _listen_thread;
        /// <summary>
        /// 发送线程
        /// </summary>
		private Thread _sender_thread;
        /// <summary>
        /// UDP IPEndPoint
        /// </summary>
        private IPEndPoint remotePoint;
        /// <summary>
        /// 单体模式 
        /// </summary>
        private static Sockets objInstance = null;
        #endregion
        public Sockets()
		{
		}
		#region 属性
		#endregion
		#region 方法
        #region 通用
        /// <summary>
        /// 单体模式
        /// </summary>
        /// <returns></returns>
        public static Sockets getInstance()
        {
            if (objInstance == null) objInstance = new Sockets();
            return objInstance;
        }
		/// <summary>
		/// 获取本机ip
		/// 用于服务器监听
		/// </summary>
		/// <returns></returns>
		private IPAddress getHostIP()
		{
			try
			{
				IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
				foreach (IPAddress curAdd in heserver.AddressList)
				{
					if (curAdd.AddressFamily.ToString() != ProtocolFamily.InterNetworkV6.ToString())
					{
						return curAdd;
					}
				}
			}
			catch{}
			return IPAddress.Parse("127.0.0.1");
		}
		/// <summary>
		/// 获取服务器ip
		/// 以便客户端连接
		/// </summary>
		/// <returns></returns>
		private IPAddress getServerIP(){
			try{
				return Dns.GetHostEntry("chat.kansea.com").AddressList[0];
			}catch{
				return IPAddress.Parse("127.0.0.1");
			}
		}
		#endregion
		#region 服务器监听
		/// <summary>
		/// udp服务开始
		/// </summary>
		public void UdpServerStart(){
			this.remotePoint = new IPEndPoint(IPAddress.Any, 0);
			this._listen_udp = new UdpClient(this._listen_port);
			this._listen_thread = new Thread(new ThreadStart(this.udpServerListener));
			this._listen_thread.IsBackground = true;
			this._listen_thread.Start();
		}
		/// <summary>
		/// udp监听线程
		/// </summary>
		private void udpServerListener(){
			while(true){
				byte[] msgBuff = this._listen_udp.Receive(ref this.remotePoint);
				//System.Text.Encoding.UTF8.GetString(msgBuff));
				//this.remotePoint.Address.ToString();
			}
		}
		/// <summary>
		/// 开始服务
		/// </summary>
		public void serverStart(){
            this._listen_ip = this.getHostIP();
			this._listen_tcp = new TcpListener(this._listen_ip,this._listen_port);
			this._listen_tcp.Start();
			this._listen_thread = new Thread(new ThreadStart(this.serverListener));
			this._listen_thread.IsBackground = true;
			this._listen_thread.Start();
		}
        /// <summary>
        /// 停止服务
        /// </summary>
        public void serverStop()
        {
            this._listen_thread.Abort();
        }
		/// <summary>
		/// 监听线程
		/// </summary>
		private void serverListener(){
			while(true){
                Socket clientSocket = this._listen_tcp.AcceptSocket();//获取客户端请求

                Thread clientThread = new Thread(new ParameterizedThreadStart(this.Receive));
                clientThread.Name = String.Format("Ilab_Client_{0}", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
                clientThread.IsBackground = true;
                clientThread.Start(clientSocket);
			}
		}
        /// <summary>
        /// 客户端监听
        /// </summary>
        public void clientListen()
        {
            if (null == this._sender_thread)
            {
                this._sender_thread = new Thread(new ThreadStart(this.clientListener));
                this._sender_thread.IsBackground = true;
                this._sender_thread.Start();
            }
        }
        /// <summary>
        /// 停止客户端监听
        /// </summary>
        public void clientStop()
        {
            if (null != this._sender_thread)
            {
                this._sender_thread.Abort();
            }
        }
        /// <summary>
        /// 客户端监听线程
        /// </summary>
        private void clientListener()
        {
            if (null == this._sender)
            {
                this._sender_thread.Abort();
            }
            else
            {
                lock (this)//Only once
                {
                    this.Receive(this._sender);
                }
            }
        }
        /// <summary>
        /// 接收信息
        /// </summary>
        private void Receive(object receiver)
        {
            this.Receive((Socket)receiver);
        }
        /// <summary>
        /// 接收信息
        /// </summary>
        private void Receive(Socket receiver)
        {
            byte[] buffinfo = new byte[this._headerLength];
            receiver.Receive(buffinfo, this._headerLength, 0);//接收信息流头
            int BuffLength = Convert.ToInt32(System.Text.Encoding.UTF8.GetString(buffinfo));
            if (0 < BuffLength)
            {
                byte[] buffHeader = new byte[BuffLength];
                receiver.Receive(buffHeader, BuffLength, 0);//接收信息流头

                try
                {
                    MessageHeader msgheader = (MessageHeader)BufferHelper.Deserialize(BufferHelper.Decrypt(buffHeader));
                    object objectBuff = null;
                    if (0 < msgheader.Length)
                    {
                        byte[] buff = new byte[msgheader.Length];
                        receiver.Receive(buff, msgheader.Length, 0);//接收信息流头
                        objectBuff = BufferHelper.Deserialize(BufferHelper.Decrypt(buff));
                    }
                    int hashCode = this.checkClientSocket(receiver);
                    ProcessMsg pMsg = new ProcessMsg(objectBuff, msgheader.Status, this._user_list,hashCode);
                    pMsg.Entrance();//根据status 强制转换 object的类型
                    if (!pMsg._IsGoon)
                    {
                        this.closeClientSocket(receiver);
                        return;
                    }

                }
#if DEBUG
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Receive1" + e.Message);
                }
#else
                catch{
                }
#endif
            }
            else//Just only tell server the client is alive
            {

            }
            try
            {
                this.Receive(receiver);
            }
            catch{
                this.closeClientSocket(receiver);
            }
        }
        private int checkClientSocket(Socket receiver)
        {
            Container user = new Container();
            user.clientThread = Thread.CurrentThread;
            user.userName = null;
            user.clientSocket = receiver;

            int hashCode = receiver.GetHashCode();
            if (!this._user_list.Contains(hashCode))
            {
                this._user_list.Add(hashCode, user);
            }
            else
            {
                //already exists
            }
            //System.Windows.Forms.MessageBox.Show(this._user_list.Count.ToString());
            return hashCode;// this._user_list[hashCode];
            //写入数据库
            //更新用户状态
            //登陆的用户 加入列表 
            //用户名关联 线程名
        }
        private void closeClientSocket(Socket receiver)
        {
            receiver.Close();
            Thread.CurrentThread.Abort();
        }
		#endregion
		
		#region 发送信息

        public byte[] getSendBuff(object obj,MessageStatus msgStatus)
        {
            byte[] Byte_send = null;
            byte[] Byte_header = null;


            MessageHeader msgHeader = new MessageHeader();

            if (null != obj)
            {
                msgHeader.Status = msgStatus;
                /*
                Type objtype = obj.GetType();

                if (typeof(Message) == objtype)
                {
                    msgHeader.Status = MessageStatus.GetMsgUser;
                }
                else if (typeof(Message[]) == objtype)
                {
                    msgHeader.Status = MessageStatus.GetMsgList;
                }
                else if (typeof(UserInfo) == objtype)
                {
                    msgHeader.Status = MessageStatus.GetUserInfo;
                }
                else if (typeof(UserInfo[]) == objtype)
                {
                    msgHeader.Status = MessageStatus.GetUserList;
                }
                */
                Byte_send = BufferHelper.Serialize(obj);
                Byte_send = BufferHelper.Encrypt(Byte_send);
                //要发送的内容长度
                msgHeader.Length = Byte_send.Length;
            }
            else
            {
                msgHeader.Length = 0;
            }
            Byte_header = BufferHelper.Serialize(msgHeader);
            Byte_header = BufferHelper.Encrypt(Byte_header);


            //头文件信息
            byte[] Byte_header_info = System.Text.Encoding.UTF8.GetBytes(Byte_header.Length.ToString());


            byte[] Byte_result = new byte[this._headerLength + Byte_header.Length + msgHeader.Length];

            Byte_header_info.CopyTo(Byte_result, 0);
            Byte_header.CopyTo(Byte_result, this._headerLength);
            if (null != Byte_send)
            {
                Byte_send.CopyTo(Byte_result, this._headerLength + Byte_header.Length);
            }
            return Byte_result;
        }
        /// <summary>
        /// 发送信息
        /// Socket sender 未初始化 则为客户端  连接服务器 否则 发送给上次连接的Socket
        /// </summary>
        /// <param name="obj">发送的数据模型</param>
        /// <param name="msgStatus">请求标志</param>
        public void send(object obj, MessageStatus msgStatus)
        {
            if (null == this._sender)
            {
                this._sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (null == this._server_ip)
                {
                    this._server_ip = this.getServerIP();
                    //this._server_ip = IPAddress.Parse("192.168.0.11");
                    //this._server_ip = IPAddress.Parse("127.0.0.1");
                }
                this._sender.Connect(this._server_ip, this._send_port);
            }
            this._sender.Send(this.getSendBuff(obj, msgStatus));
        }
        /// <summary>
        /// 指定发送对象
        /// 由服务器发送给 客户 客户发送给客户
        /// </summary>
        /// <param name="text">发送内容</param>
        /// <param name="clientSocket">指定发送对象的Socket</param>
        public void send(object obj, MessageStatus msgStatus, Socket clientSocket)
        {
            this._sender = clientSocket;
            this.send(obj, msgStatus);
        }
        #region client to server
        /// <summary>
        /// 发送信息
        /// Socket sender 未初始化 则为客户端  连接服务器 否则 发送给上
        /// </summary>
        /// <param name="msg">发送模型</param>
        public void send(Message msg)
        {
            this.send(msg, MessageStatus.GetMsgUser);
        }
        public void send(Message[] msgs)
        {
            this.send(msgs, MessageStatus.GetMsgList);
        }
        public void send(UserInfo userinfo)
        {
            this.send(userinfo, MessageStatus.GetUserInfo);
        }
        public void send(UserInfo[] userinfos)
        {
            this.send(userinfos, MessageStatus.GetUserList);
        }
        public void login(UserInfo userinfo)
        {
            this.send(userinfo, MessageStatus.Login);
        }
        public void logout(UserInfo userinfo)
        {
            this.send(userinfo, MessageStatus.Logout);
        }
        #endregion
        #region server to send
        /// <summary>
        /// 发送给客户端
        /// </summary>
        /// <param name="msg">发送模型</param>
        /// <param name="clientSocket">客户端</param>
        public void send(Message msg, Socket clientSocket)
        {
            this._sender = clientSocket;
            this.send(msg, MessageStatus.SendMsgUser);
        }
        /// <summary>
        /// 发送给客户端
        /// </summary>
        /// <param name="msgs">发送模型</param>
        /// <param name="clientSocket">客户端</param>
        public void send(Message[] msgs, Socket clientSocket)
        {
            this._sender = clientSocket;
            this.send(msgs, MessageStatus.SendMsgList);
        }
        /// <summary>
        /// 发送给客户端
        /// </summary>
        /// <param name="userinfo">发送模型</param>
        /// <param name="clientSocket">客户端</param>
        public void send(UserInfo userinfo, Socket clientSocket)
        {
            this._sender = clientSocket;
            this.send(userinfo, MessageStatus.SendUserInfo);
        }
        /// <summary>
        /// 发送给客户端
        /// </summary>
        /// <param name="userinfos">发送模型</param>
        /// <param name="clientSocket">客户端</param>
        public void send(UserInfo[] userinfos, Socket clientSocket)
        {
            this._sender = clientSocket;
            this.send(userinfos, MessageStatus.SendUserList);
        }
        #endregion
        #endregion
        #endregion
    }
}
