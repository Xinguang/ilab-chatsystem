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
using System.Collections;
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
        private Sockets _sender = new Sockets();
        private int _hashCode;
        private Hashtable _user_list;
        private Container _userConnection;
        #endregion
        #region 构造函数
        public ProcessMsg(object obj, MessageStatus status, Hashtable user_list, int hashCode)
		{
            this._Status = status;
            this._Object = obj;
            this._user_list = user_list;
            this._hashCode = hashCode;
            this._userConnection = (Container)this._user_list[this._hashCode];
		}
        #endregion
        #region 方法
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

            if ("star" == userinfo.UserName && "star" == userinfo.Password)
            {
                userinfo.LoginDate = DateTime.Now;
                this._user_list.Remove(this._hashCode);
                this._userConnection.userName = userinfo.UserName;
                this._user_list.Add(this._hashCode, this._userConnection);
            }
            this._sender.send(userinfo, this._userConnection.clientSocket);
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
            iMessage[] messages = (iMessage[])this._Object;
            string testmsg = "getMsgList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getMsgUser()
        {
            iMessage message = (iMessage)this._Object;
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
            System.Windows.Forms.MessageBox.Show(testmsg+userinfo.LoginDate.ToString());
        }
        private void sendMsgList()
        {
            iMessage[] messages = (iMessage[])this._Object;
            string testmsg = "sendMsgList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendMsgUser()
        {
            iMessage message = (iMessage)this._Object;
            string testmsg = "sendMsgUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        #endregion
        #endregion
    }
}
