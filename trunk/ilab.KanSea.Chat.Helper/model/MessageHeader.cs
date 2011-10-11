/**
 * 作者	: Hikaru
 * 日期	: 2011/10/10
 * 时间	: 11:59
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;

namespace ilab.KanSea.Chat.Helper.model
{
	/// <summary>
	/// information header model
	/// </summary>
    [Serializable]
	public class MessageHeader
	{
		#region 属性
        /// <summary>
        /// information status
        /// </summary>
        public MessageStatus Status{get;set;}
        /// <summary>
        /// information size
        /// </summary>
        public int Length {get;set;}
        #endregion
	}
}
