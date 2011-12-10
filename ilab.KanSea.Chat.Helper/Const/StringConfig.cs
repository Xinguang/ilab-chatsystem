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
        private const string Server = "192.168.0.15";
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
    public class Socktes_Server
    {
        public const Int32 ServerPort = 125;
        public const Int32 headerLength = 20;
    }
}
