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
using System.Net.Sockets;
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
        private Message msg = null;
        private Message callbackMsg =null;
        #endregion
        #region 构造函数
        public ProcessMsg()
		{
		}
        #endregion
        #region 方法
        /// <summary>
        /// 信息处理入口
        /// </summary>
        /// <param name="msg">接收到的信息</param>
        public Message callback(Message receiveMsg)
        {
            this.msg = receiveMsg;

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
            return this.callbackMsg;
  
        }
        #region 服务器接收到的信息
        private void login()
        {
            string testmsg = "login:" + this.msg.UserName + msg.Password;
            this.callbackMsg = new Message();
            this.callbackMsg.Content = testmsg;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void logout()
        {
            string testmsg = "logout:" + this.msg.UserName;
            this.callbackMsg = new Message();
            this.callbackMsg.Content = testmsg;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getInfoList()
        {
            string testmsg = "getInfoList:" + this.msg.SendDate.ToString() + this.msg.ClientIntranet.ToString();
            this.callbackMsg = new Message();
            this.callbackMsg.Content = testmsg;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getInfoUser()
        {
            string testmsg = "getInfoUser:" + this.msg.SendDate.ToString() + this.msg.ClientIntranet.ToString() + this.msg.Content;
            this.callbackMsg = new Message();
            this.callbackMsg.Content = testmsg;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getMsgList()
        {
            string testmsg = "getMsgList:" + this.msg.SendDate.ToString() + this.msg.ClientIntranet.ToString();
            this.callbackMsg = new Message();
            this.callbackMsg.Content = testmsg;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getMsgUser()
        {
            string testmsg = "getMsgUser:" + this.msg.SendDate.ToString() + this.msg.ClientIntranet.ToString() + this.msg.Content;
            this.callbackMsg = new Message();
            this.callbackMsg.Content = testmsg;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        #endregion

        #region 客户端接收到的信息
        private void sendInfoList()
        {
            string testmsg = "sendInfoList:" + this.msg.SendDate.ToString() + this.msg.ClientIntranet.ToString() +  this.msg.Content;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendInfoUser()
        {
            string testmsg = "sendInfoUser:" + this.msg.SendDate.ToString() + this.msg.ClientIntranet.ToString() +  this.msg.Content;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendMsgList()
        {
            string testmsg = "sendMsgList:" + this.msg.SendDate.ToString() + this.msg.ClientIntranet.ToString() +  this.msg.Content;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendMsgUser()
        {
            string testmsg = "sendMsgUser:" + this.msg.SendDate.ToString() + this.msg.ClientIntranet.ToString() +  this.msg.Content;
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        #endregion
        #endregion
    }
}
