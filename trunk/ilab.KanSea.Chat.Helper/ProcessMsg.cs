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
        private MessageStatus _Status;
        private object _Object;
        public bool _IsGoon = true;
        #endregion
        #region 构造函数
        public ProcessMsg(object obj,MessageStatus status)
		{
            this._Status = status;
            this._Object = obj;
		}
        #endregion
        #region 方法
        /// <summary>
        /// 信息处理入口
        /// </summary>
        public void Entrance()
        {
            switch (this._Status)
            {
                case MessageStatus.Login:
                    this.login();
                    break;
                case MessageStatus.Logout:
                    this.logout();
                    break;
                case MessageStatus.GetUserList:
                    this.getUserList();
                    break;
                case MessageStatus.GetUserInfo:
                    this.getUserInfo();
                    break;
                case MessageStatus.GetMsgList:
                    this.getMsgList();
                    break;
                case MessageStatus.GetMsgUser:
                    this.getMsgUser();
                    break;
                case MessageStatus.SendUserList:
                    this.sendUserList();
                    break;
                case MessageStatus.SendUserInfo:
                    this.sendUserInfo();
                    break;
                case MessageStatus.SendMsgList:
                    this.sendMsgList();
                    break;
                case MessageStatus.SendMsgUser:
                    this.sendMsgUser();
                    break;
                default:
                    break;
            }
  
        }
        #region 服务器接收到的信息
        private void login()
        {
            UserInfo userinfo = (UserInfo)this._Object;
            string testmsg = "login:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void logout()
        {
            UserInfo userinfo = (UserInfo)this._Object;
            string testmsg = "logout:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getUserList()
        {
            UserInfo[] userinfos = (UserInfo[])this._Object;
            string testmsg = "getInfoList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getUserInfo()
        {
            UserInfo userinfo = (UserInfo)this._Object;
            string testmsg = "getInfoUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getMsgList()
        {
            Message[] messages = (Message[])this._Object;
            string testmsg = "getMsgList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getMsgUser()
        {
            Message message = (Message)this._Object;
            string testmsg = "getMsgUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        #endregion

        #region 客户端接收到的信息
        private void sendUserList()
        {
            UserInfo[] userinfos = (UserInfo[])this._Object;
            string testmsg = "sendInfoList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendUserInfo()
        {
            UserInfo userinfo = (UserInfo)this._Object;
            string testmsg = "sendInfoUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendMsgList()
        {
            Message[] messages = (Message[])this._Object;
            string testmsg = "sendMsgList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendMsgUser()
        {
            Message message = (Message)this._Object;
            string testmsg = "sendMsgUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        #endregion
        #endregion
    }
}
