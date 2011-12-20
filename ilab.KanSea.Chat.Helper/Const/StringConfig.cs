/**
 * 作者	: Hikaru
 * 日期	: 2011/11/21
 * 时间	: 1:05
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;

namespace ilab.KanSea.Chat.Helper.Const
{
    public class Data_Server
    {
        private const string Server = "localhost";
        //private const string Server = "192.168.0.15";
        private const string User = "ngram";
        private const string Pass = "yRpWAEdTmxTb8Z3E";
        private const string DataBase = "ngram";
        public static readonly string ConnStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false",
               Server, User, Pass, DataBase);
        public const string Table_Chinese = "chinese";
        public const string Table_Japanese = "japanese";
        public const string Table_English = "english";
        public const string Table_Slang = "slang";
    }
    public class Api
    {
        public const string url_get_slang = "http://ilab.kansea.com/wwwbak/Webngram/slang.php?k={0}&p={1}";
        public const string url_check_slang = "http://ilab.kansea.com/wwwbak/Webngram/checkword.php?t={0}&k={1}";
        public const string url_fix_split = "http://ilab.kansea.com/wwwbak/Webngram/fixsplit.php?ks={0}&input={1}&lang={2}";

        //public const string yahoo_appid = "xUtu2PKxg64uz3UjrK7EBiFgZckyki_veV2_h_PxsjZavxUhg6N3muKcytn1PBU-";
        //public const string yahoo_Url_text = "http://jlp.yahooapis.jp/JIMService/V1/conversion?appid={0}&sentence={1}&response=hiragana,katakana";
        //public const string yahoo_Url_Furigana = "http://jlp.yahooapis.jp/FuriganaService/V1/furigana?appid={0}&sentence={1}";
    }
    public class Socktes_Server
    {
        public const Int32 ServerPort = 125;
        public const Int32 headerLength = 20;
    }
}
