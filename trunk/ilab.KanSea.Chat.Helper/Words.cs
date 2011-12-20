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
using System.Text;
using NMeCab;
using System.Runtime.InteropServices;
using ilab.KanSea.Chat.Helper.Const;
using System.Collections.Generic;
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
		#region 方法
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
        public static string MeCabParse(string input)
        {
            try
            {
                MeCabTagger tagger = MeCabTagger.Create();
                tagger.LatticeLevel = MeCabLatticeLevel.Zero;
                tagger.OutPutFormatType = "lattice";
                tagger.AllMorphs = false;
                tagger.Partial = false;

                return tagger.Parse(input);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static string MeCabParseString(string input)
        {
            try
            {
                string result = MeCabParse(input);
                return HelperBase.Repalce(result, "[\\t].*[\\n]?(EOS)?", " ");
                //return null;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        /// <summary>
        /// 替换俗语
        /// </summary>
        /// <param name="input"></param>
        /// <param name="langtable"></param>
        /// <returns></returns>
        public static string replaceSlang(string input, string langtable)
        {
            //获取俗语
            HttpHelper http = HttpHelper.getInstance();
            http.Encoding = Encoding.UTF8;
            //this.http.GetRequest(string.Format(config.config.url_get_slang, input, "1"));
            string cantsplit = http.GetRequest(string.Format(Api.url_check_slang, langtable, input));
            cantsplit = cantsplit.Trim();

            string k = null;
            foreach (char c in cantsplit)
            {
                if (k != null) { k += "|"; }
                k += c;
            }
            string webhtml = http.GetRequest(string.Format(Api.url_fix_split, k, input, langtable));

            cantsplit = webhtml;
            //cantsplit = k;
            return cantsplit;
        }
        /*
        private static string getsplit(string input,string cantsplit, string langtable)
        {
            if (cantsplit != "" && cantsplit != null)
            {
                HttpHelper http = HttpHelper.getInstance();
                if ("japanese" == langtable)
                {
                    string cantsplitTemp = HelperBase.GetString(input, "(([^\\s]*[\\s])?" + cantsplit + "([\\s][^\\s]*)?)", 1);
                    string webhtml = http.GetRequest(string.Format(Api.url_get_furigana, cantsplitTemp.Trim()));
                    string cantsplit_webhtml_furigana = http.GetRequest(string.Format(Api.yahoo_Url_text, Api.yahoo_appid, cantsplit));
                    string cantsplit_furigana = HelperBase.GetString(cantsplit_webhtml_furigana, "Hiragana>(.*)<\\/Hiragana", 1);
                    //cantsplit = cantsplit_furigana + "--" + webhtml.Replace(cantsplit_furigana, " ");

                    string afk = webhtml.Replace(cantsplit_furigana, " ");
                    string k = null;
                    foreach (char c in cantsplit_furigana)
                    {
                        if (k != null) { k += "|"; }
                        k += c;
                    }
                    // cantsplit = k+"----"+afk;
                    webhtml = http.GetRequest(string.Format(Api.url_fix_slang, afk, k));
                    input = input.Replace(cantsplitTemp, webhtml);
                    cantsplit = input;
                }
                return cantsplit;
            }
            return null;
        }*/

        public static string getslang(string slangs)
        {
            HttpHelper http = HttpHelper.getInstance();
            http.Encoding = Encoding.UTF8;
            return http.GetRequest(string.Format(Api.url_get_slang, slangs, 1));
        }



        #region ICTCLAS2011
        // Fields
        private const string path = "ICTCLAS2011.dll";

        // Methods
        [DllImport("ICTCLAS2011.dll", CharSet = CharSet.Ansi)]
        private static extern bool ICTCLAS_Exit();
        [DllImport("ICTCLAS2011.dll", CharSet = CharSet.Ansi)]
        private static extern bool ICTCLAS_FileProcess(string sSrcFilename, string sDestFilename, int bPOStagged);
        [DllImport("ICTCLAS2011.dll", CharSet = CharSet.Ansi)]
        private static extern bool ICTCLAS_Init(string sInitDirPath, int encoding);
        [DllImport("ICTCLAS2011.dll", CharSet = CharSet.Ansi)]
        private static extern int ICTCLAS_ParagraphProcessE(string sParagraph, StringBuilder sResult, int bPOStagged);

        [STAThread]
        public static string ICTCLAParse(string input)
        {
            if (!ICTCLAS_Init("", 0))
            {
                HttpHelper http = HttpHelper.getInstance();
                http.Encoding = Encoding.UTF8;
                return http.GetRequest(string.Format("http://ilab.kansea.com/wwwbak/split/test.php?d=string&q={0}", input));
            }
            else
            {
                //ICTCLAS_FileProcess("Input.txt", "Input_result.txt", 1);
                StringBuilder sResult = new StringBuilder(10000);
                ICTCLAS_ParagraphProcessE(input, sResult, 1);
                
                ICTCLAS_Exit();
                string result = sResult.ToString();
                return HelperBase.Repalce(result, "\\/[\\S]*\\s", " ");
            }
        }

		#endregion
		#endregion
	}
}
