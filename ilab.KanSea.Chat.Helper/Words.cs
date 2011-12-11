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
        public static string MeCabParse(string input){
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
                return "Init ICTCLAS failed!";
            }
            else
            {
                //ICTCLAS_FileProcess("Input.txt", "Input_result.txt", 1);
                StringBuilder sResult = new StringBuilder(600);
                ICTCLAS_ParagraphProcessE(input, sResult, 1);
                
                ICTCLAS_Exit();
                return sResult.ToString();
            }
        }

		#endregion
		#endregion
	}
}
