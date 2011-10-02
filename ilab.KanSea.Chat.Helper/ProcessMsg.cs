/**
 * 作者	: Hikaru
 * 日期	: 2011/10/1
 * 时间	: 1:43
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using ilab.KanSea.Chat.Helper.model;

namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// 判断接发信息内容
	/// 并调用相关函数
	/// </summary>
	public class ProcessMsg
    {
        #region 声明
        private Object thisLock = new Object();
        private Message msg;
        #endregion
        #region 构造函数
        public ProcessMsg()
		{
		}
        #endregion
        #region 方法
        /// <summary>
		/// 单体模式 
		/// </summary>
		private static ProcessMsg objInstance = null; 
		/// <summary>
		/// 单体模式
		/// </summary>
		/// <returns></returns>
		public static ProcessMsg getInstance() {
			if (objInstance==null) objInstance=new ProcessMsg();
			return objInstance;
		}
        /// <summary>
        /// 信息处理入口
        /// </summary>
        /// <param name="buff">接收到的信息</param>
        public void callback(byte[] buff)
        {
            lock (thisLock)//按顺序运行
            {
                this.msg = (Message)BufferHelper.Deserialize(buff, 0);
                switch (this.msg.MsgStatus)
                {
                    case MsgType.Login:
                        this.login();
                        break;
                    case MsgType.Logout:
                        this.logout();
                        break;
                    case MsgType.GetInfoList:
                        this.getInfoList();
                        break;
                    case MsgType.GetInfoUser:
                        this.getInfoUser();
                        break;
                    case MsgType.GetMsgList:
                        this.getMsgList();
                        break;
                    case MsgType.GetMsgUser:
                        this.getMsgUser();
                        break;
                    case MsgType.SendInfoList:
                        this.sendInfoList();
                        break;
                    case MsgType.SendInfoUser:
                        this.sendInfoUser();
                        break;
                    case MsgType.SendMsgList:
                        this.sendMsgList();
                        break;
                    case MsgType.SendMsgUser:
                        this.sendMsgUser();
                        break;
                    default:
                        break;
                }
            }
        }
        #region 服务器接收到的信息
        private void login()
        {
            System.Windows.Forms.MessageBox.Show("login");
        }
        private void logout()
        {
            System.Windows.Forms.MessageBox.Show("logout");
        }
        private void getInfoList()
        {
            System.Windows.Forms.MessageBox.Show("getInfoList");
        }
        private void getInfoUser()
        {
            System.Windows.Forms.MessageBox.Show("getInfoUser");
        }
        private void getMsgList()
        {
            System.Windows.Forms.MessageBox.Show("getMsgList");
        }
        private void getMsgUser()
        {
            System.Windows.Forms.MessageBox.Show("getMsgUser");
        }
        #endregion

        #region 客户端接收到的信息
        private void sendInfoList()
        {
            System.Windows.Forms.MessageBox.Show("sendInfoList");
        }
        private void sendInfoUser()
        {
            System.Windows.Forms.MessageBox.Show("sendInfoUser");
        }
        private void sendMsgList()
        {
            System.Windows.Forms.MessageBox.Show("sendMsgList");
        }
        private void sendMsgUser()
        {
            System.Windows.Forms.MessageBox.Show("sendMsgUser");
        }
        #endregion
        #endregion
    }
}
