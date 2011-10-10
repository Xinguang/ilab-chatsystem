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
        private MessageHeader msg = null;
        private MessageHeader callbackMsg =null;
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
        public MessageHeader callback(MessageHeader receiveMsg)
        {
            this.msg = receiveMsg;

            switch (this.msg.Status)
            {
                case MsgType.Login:
                    this.login();
                    break;
                case MsgType.Logout:
                    this.logout();
                    break;
                case MsgType.GetUserList:
                    this.getInfoList();
                    break;
                case MsgType.GetUserInfo:
                    this.getInfoUser();
                    break;
                case MsgType.GetMsgList:
                    this.getMsgList();
                    break;
                case MsgType.GetMsgUser:
                    this.getMsgUser();
                    break;
                case MsgType.SendUserList:
                    this.sendInfoList();
                    break;
                case MsgType.SendUserInfo:
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
            string testmsg = "login:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void logout()
        {
            string testmsg = "logout:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getInfoList()
        {
            string testmsg = "getInfoList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getInfoUser()
        {
            string testmsg = "getInfoUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getMsgList()
        {
            string testmsg = "getMsgList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void getMsgUser()
        {
            string testmsg = "getMsgUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        #endregion

        #region 客户端接收到的信息
        private void sendInfoList()
        {
            string testmsg = "sendInfoList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendInfoUser()
        {
            string testmsg = "sendInfoUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendMsgList()
        {
            string testmsg = "sendMsgList:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        private void sendMsgUser()
        {
            string testmsg = "sendMsgUser:";
            System.Windows.Forms.MessageBox.Show(testmsg);
        }
        #endregion
        #endregion
    }
}
