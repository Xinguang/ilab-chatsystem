/**
 * 作者	: Hikaru
 * 日期	: 2011/9/28
 * 时间	: 1:20
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;

namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// Description of Words.
	/// </summary>
	public class Words
	{
		public Words()
		{
		}
		#region 属性
		/// <summary>
		/// 单体模式 
		/// </summary>
		private static Words objInstance = null; 
		/// <summary>
		/// 单体模式
		/// </summary>
		/// <returns></returns>
		public static Words getInstance() {
			if (objInstance==null) objInstance=new Words();
			return objInstance;
		}
		#endregion
	}
}
