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
    /// 信息属性
    /// </summary>
    [Flags]
    public enum MsgType
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login = 1,
        /// <summary>
        /// 退出
        /// </summary>
        Logout = 2,
        /// <summary>
        /// 获取该用户所有好友信息列表
        /// </summary>
        GetInfoList = 4,
        /// <summary>
        /// 发送该用户所有好友信息列表
        /// </summary>
        SendInfoList = 8,
        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        GetInfoUser = 16,
        /// <summary>
        /// 发送指定用户信息
        /// </summary>
        SendInfoUser = 32,
        /// <summary>
        /// 获取 留言 列表
        /// </summary>
        GetMsgList = 64,
        /// <summary>
        /// 发送 留言 列表
        /// </summary>
        SendMsgList = 128,
        /// <summary>
        /// 获取指定用户的留言
        /// </summary>
        GetMsgUser = 256,
        /// <summary>
        /// 发送指定用户的留言
        /// </summary>
        SendMsgUser = 512
        /// <summary>
        /// 什么都不做
        /// </summary>
    }
}
