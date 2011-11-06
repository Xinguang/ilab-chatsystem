/**
 * 作者	: Hikaru
 * 日期	: 2011/10/25
 * 时间	: 0:21
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;

namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// Google Translate API
	/// </summary>
	partial class Translate
    {
        /// <summary>
        /// Google Translate API V 2.0
        /// {0} type "/detect" or null
        /// {0} API Key
        /// {1} keyword
        /// {2} source
        /// {3} target
        /// </summary>
        private const string _url_Google_v2 = "https://www.googleapis.com/language/translate/v2{0}?key={1}&q={2}&source={3}&target={4}";
        /// <summary>
        /// API Key
        /// </summary>
        private const string _url_Google_v2_key = "AIzaSyAhDUYuMo4gAD55ASNVrTWdY6Ux3Ov0evQ";

        public static string Google_Get()
        {
            HttpHelper http = new HttpHelper();
            http.TargetUrl = string.Format(_url_Google_v2, null, _url_Google_v2_key, "test", "en", "ja");
            http.Encoding = System.Text.Encoding.UTF8;
            //http.GetRequest(string.Format(_url_Google_v2,null,_url_Google_v2_key,"test","en","ja"));
            return http.GetRequest(string.Format(_url_Google_v2, null, _url_Google_v2_key, "test", "en", "ja"));
        }
	}
}
