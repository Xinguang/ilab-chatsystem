/**
 * 作者	: Hikaru
 * 日期	: 2011/10/25
 * 时间	: 0:22
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Collections.Generic;
namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// Microsoft Translate API
	/// </summary>
	partial class Translate
    {      /// <summary>
        /// Google Translate API V 2.0
        /// {0} type "Detect" or "Translate" or "TranslateArray"
        /// {0} API Key
        /// {1} keyword
        /// {2} source
        /// {3} target
        /// http://api.microsofttranslator.com/v2/Ajax.svc/Translate?appid=EFED07CA427393406128B9552DCFA7331B706702&to=en&text=测试
        /// </summary>
        private const string _url_Microsoft = "http://api.microsofttranslator.com/v2/Ajax.svc/{0}?appId={1}&text={2}&from={3}&to={4}";

        private const string _url_Microsoft_Detect = "http://api.microsofttranslator.com/v2/Ajax.svc/Detect";
        private const string _url_Microsoft_Translate = "http://api.microsofttranslator.com/v2/Ajax.svc/Translate";
        private const string _url_Microsoft_TranslateList = "http://api.microsofttranslator.com/V2/Ajax.svc/TranslateArray";
        /// <summary>
        /// API Key
        /// </summary>
        private const string _url_Microsoft_v2_key = "EFED07CA427393406128B9552DCFA7331B706702";

        public static string Microsoft_GetLang()
        {
            return null;
        }
        public static string Microsoft_Get(string str, string from, string to)
        {
            string httpstr = _http.GetRequest(_url_Microsoft_Translate + string.Format("?appId={0}&text={1}&from={2}&to={3}",_url_Microsoft_v2_key, str, from, to));
            httpstr = HelperBase.GetString(httpstr, "\\\"(.*)\\\"", 1);
            return httpstr.Replace("\\u000d","").Replace("\\u000a","");
        }
        public static String[] Microsoft_GetStrings(string[] strs, string from, string to)
        {
            string texts = "[";
            string split = null;
            foreach (string str in strs)
            {
                texts += split + "\"" + str.Replace(",","，") + "\"";
                if (null == split) { split = ","; }
            }
            texts += "]"; 
            string httpstr = _http.GetRequest(_url_Microsoft_TranslateList + string.Format("?appId={0}&texts={1}&from={2}&to={3}&maxTranslations=5", _url_Microsoft_v2_key, texts, from, to));
            /*
            IList<String[]> resultList = HelperBase.GetString(httpstr, "TranslatedText\\W{3}([^,]*)\\W,\\WTranslatedTextSentenceLengths");
            string[] result = new string[resultList.Count];
            for (int i = 0, len = resultList.Count; i < len; i++)
            {
                result[i] = resultList[i][0];
            }
             * */
            string[] result = HelperBase.GetString(1,httpstr, "TranslatedText\\W{3}([^,]*)\\W,\\WTranslatedTextSentenceLengths");
            return result;
        }
        /*
         http://api.microsofttranslator.com/V2/Ajax.svc/GetTranslationsArray
         * ?appId=EFED07CA427393406128B9552DCFA7331B706702&texts=[%22test%22,%22apple%22,%22unknow%22]&from=en&to=zh-CHS&maxTranslations=5
         *
         */
    }
}
