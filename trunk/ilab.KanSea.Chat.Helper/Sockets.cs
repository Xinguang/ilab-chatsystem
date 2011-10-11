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
        private List<Container> _user_list;
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
				return Dns.GetHostEntry("kansea.com").AddressList[0];
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
		/// 监听线程
		/// </summary>
		private void serverListener(){
			while(true){
                Socket clientSocket = this._listen_tcp.AcceptSocket();//获取客户端请求
                Container user_container = new Container { clientSocket=clientSocket ,clientThread=new Thread(new ParameterizedThreadStart(this.Receive))};
                //添加新线程调用下面
                this.Receive(user_container);
			}
		}
		/// <summary>
		/// 客户端监听
		/// </summary>
        public void clientListen()
        {
            this._sender_thread = new Thread(new ThreadStart(this.clientListener));
            this._sender_thread.IsBackground = true;
            this._sender_thread.Start();
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
                //添加新线程调用下面
                this.Receive(this._sender);
            }
        }
        private void newrewiththread(Socket receiver)
        {
            Thread clientThread = new Thread(new ParameterizedThreadStart(this.Receive));
            clientThread.Name = String.Format("Ilab_Client_{0}", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            clientThread.IsBackground = true;
            clientThread.Start(receiver);
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
                    ProcessMsg pMsg = new ProcessMsg(objectBuff, msgheader.Status);
                    pMsg.Entrance();//根据status 强制转换 object的类型
                    if (!pMsg._IsGoon)
                    {
                        return;
                    }
                    /*
                    ProcessMsg pMsg = new ProcessMsg();
                    pMsg.callback(objectBuff,msgheader.Status);//根据status 强制转换 object的类型
                    
                    this.getBuffUserInfo();//senduserinfo  //server login logout 
                    this.getBuffUserList();//send userlist
                    this.getBuffMsgInfo();//send msginfo
                    this.getBuffMsgList();//send msglist
                    
                    this.SetBuffUserInfo();//getuserinfo  //client login logout 
                    this.SetBuffUserList();//get userlist
                    this.SetBuffMsgInfo();//get msginfo
                    this.SetBuffMsgList();//get msglist
                     */
                }
                catch
#if DEBUG
 (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
#else
                (){
                }
#endif
            }
            else//Just only tell server the client is alive
            {

            }
            this.checkClientSocket();
            this.Receive(receiver);
        }
        private void checkClientSocket()
        {
            //写入数据库
            //更新用户状态
            //登陆的用户 加入列表 
            //用户名关联 线程名
        }
		#endregion
		
		#region 发送信息
        private byte[] getSendBuff(Message msg)
        {
            byte[] sendByte = BufferHelper.Serialize(msg);
            sendByte = BufferHelper.Encrypt(sendByte);
            //要发送的内容长度
            int sendLength = sendByte.Length;
            //头文件信息
            byte[] sendHeader = System.Text.Encoding.UTF8.GetBytes(sendLength.ToString());
            //实际发送数据
            byte[] sendBuff = new byte[this._headerLength + sendLength];

            sendHeader.CopyTo(sendBuff, 0);
            sendByte.CopyTo(sendBuff, this._headerLength);
            return sendBuff;
        }
        /// <summary>
        /// 发送信息
        /// Socket sender 未初始化 则为客户端  连接服务器 否则 发送给上
        /// </summary>
        /// <param name="msg">发送模型</param>
        public void send(Message msg){
            if (null == this._sender)
            {
                this._sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (null == this._server_ip)
                {
                    //this._server_ip = this.getServerIP();
                    this._server_ip = IPAddress.Parse("192.168.0.11");
                }
                this._sender.Connect(this._server_ip, this._send_port);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(this._sender.LocalEndPoint.ToString());
            }
            int erroecode = this._sender.Send(this.getSendBuff(msg));
            System.Windows.Forms.MessageBox.Show(erroecode.ToString());
        }
        /// <summary>
        /// 指定发送对象
        /// 由服务器发送给 客户 客户发送给客户
        /// </summary>
        /// <param name="text">发送内容</param>
        /// <param name="clientSocket">指定发送对象的Socket</param>
        public void send(Message msg, Socket clientSocket)
        {
            this._sender = clientSocket;
            this.send(msg);
        }
        public void send(Message msg, EndPoint clientEndPoint)
        {
            this._sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._sender.Connect(clientEndPoint);
            this.send(msg);
        }
        /// <summary>
        /// 指定ip及端口
        /// </summary>
        /// <param name="text">发送内容</param>
        /// <param name="ipaddress">ip地址 strng型</param>
        /// <param name="port">端口</param>
        public void send(Message msg, string ipaddress, Int32 port)
        {
        	this.send(msg,IPAddress.Parse(ipaddress), port);
        }
        /// <summary>
        /// 指定ip及端口
        /// </summary>
        /// <param name="text">发送内容</param>
        /// <param name="ipaddress">ip地址 IPAddress型</param>
        /// <param name="port">端口</param>
        public void send(Message msg, IPAddress ipaddress, Int32 port)
        {
        	this._sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        	this._sender.Connect(ipaddress, port);
            this.send(msg);
        }
        #endregion
        #endregion
    }
}
