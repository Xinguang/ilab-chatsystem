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

namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// 判断接发信息内容
	/// 并调用相关函数
	/// </summary>
	public class MsgInfo
	{
		public MsgInfo()
		{
		}
		#region 属性
		/// <summary>
		/// 单体模式 
		/// </summary>
		private static MsgInfo objInstance = null; 
		/// <summary>
		/// 单体模式
		/// </summary>
		/// <returns></returns>
		public static MsgInfo getInstance() {
			if (objInstance==null) objInstance=new MsgInfo();
			return objInstance;
		}
		#endregion
	}
}
