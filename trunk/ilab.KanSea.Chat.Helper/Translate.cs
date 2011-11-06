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
	/// Description of Translate.
	/// </summary>
    public partial class Translate
	{
        private static HttpHelper _http = new HttpHelper();
		public Translate()
		{
		}
		#region 方法
		/// <summary>
		/// 单体模式 
		/// </summary>
		private static Translate objInstance = null; 
		/// <summary>
		/// 单体模式
		/// </summary>
		/// <returns></returns>
		public static Translate getInstance() {
			if (objInstance==null) objInstance=new Translate();
			return objInstance;
		}
		#endregion
	}
}
