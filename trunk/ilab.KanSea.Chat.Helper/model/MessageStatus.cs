/**
 * 作者	: Hikaru
 * 日期	: 2011/10/2
 * 时间	: 3:37
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;

namespace ilab.KanSea.Chat.Helper.model
{
    /// <summary>
    /// information status
    /// </summary>
    [Flags]
    public enum MessageStatus:int
    {
        /// <summary>
        /// login
        /// </summary>
        Login = 1,
        /// <summary>
        /// logout
        /// </summary>
        Logout = 2,
        /// <summary>
        /// Require all your friends' information 
        /// </summary>
        GetUserList = 4,
        /// <summary>
        /// Require the information of the user 
        /// </summary>
        GetUserInfo = 8,
        /// <summary>
        /// Require all your messages
        /// </summary>
        GetMsgList = 16,
        /// <summary>
        /// Require the message of the user
        /// </summary>
        GetMsgUser = 32,
        /// <summary>
        /// Send all your friends' information 
        /// </summary>
        SendUserList = 64,
        /// <summary>
        /// Send the information of the user 
        /// </summary>
        SendUserInfo = 128,
        /// <summary>
        /// Send all your messages
        /// </summary>
        SendMsgList = 256,
        /// <summary>
        /// Send the message of the user
        /// </summary>
        SendMsgUser = 512,
    }
}
