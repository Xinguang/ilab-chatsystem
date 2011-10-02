﻿/**
 * 作者	: Hikaru
 * 日期	: 2011/10/2
 * 时间	: 3:14
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Net;

namespace ilab.KanSea.Chat.Helper.model
{
	/// <summary>
	/// 信息模型
	/// </summary>
    [Serializable]
	public class Message
	{
		#region 属性
        /// <summary>
        /// 信息类型
        /// </summary>
        public MsgType MsgStatus{get;set;}
		/// <summary>
		/// 用户名
		/// </summary>
        public string UserName {get;set;}
        /// <summary>
        /// 密码
        /// </summary>
        public string Password {get;set;}
        /// <summary>
        /// 内网地址端口
        /// </summary>
        public IPEndPoint ClientIntranet{get;set;}
        /// <summary>
        /// 外网地址端口
        /// </summary>
        public IPEndPoint ClientExtranet {get;set;}
        /// <summary>
        /// 发送信息或留言
        /// </summary>
        public string Content{ get; set; }
        /// <summary>
        /// 发送日期
        /// </summary>
        public DateTime SendDate {get;set;}
        #endregion
	}
}
