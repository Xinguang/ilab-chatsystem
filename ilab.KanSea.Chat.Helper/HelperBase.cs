/**
 * 作者	: Hikaru
 * 日期	: 2011/9/28
 * 时间	: 1:19
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace ilab.KanSea.Chat.Helper
{
    /// <summary>
    /// Description of HelperBase.
    /// </summary>
    public class HelperBase
    {
        #region 方法
        /// <summary>
        /// 不重复随即名
        /// </summary>
        /// <returns></returns>
        public static string GetRandomName()
        {
            return Math.Abs(DateTime.Now.ToBinary()).ToString();
        }
        /// <summary>
        /// 正则 判断是否包含 
        /// </summary>
        /// <param name="input">被检测字符</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>boolean</returns>
        public static bool IsHaveString(string input, string pattern)
        {
            try
            {
                return Regex.IsMatch(input, pattern);
            }
            catch { return false; }
        }
        /// <summary>
        /// 获取匹配字符
        /// </summary>
        /// <param name="input">被检测字符</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="S">index</param>
        /// <returns>string</returns>
        public static string GetString(string input, string pattern, int S)
        {
            try
            {
                if (IsHaveString(input, pattern))
                {
                    return Regex.Match(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline).Groups[S].Value;
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取匹配字符集
        /// </summary>
        /// <param name="input">被检测字符</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>IList<String[]></returns>
        public static IList<String[]> GetString(string input, string pattern)
        {
            String[] strtemp = null;
            IList<String[]> StrList = null;
            if (input == null) { return null; }
            try
            {
                if (IsHaveString(input, pattern))
                {
                    StrList = new List<String[]>();
                    MatchCollection RegData = Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
                    for (int i = 0; i < RegData.Count; i++)
                    {
                        strtemp = null;
                        strtemp = new String[RegData[i].Groups.Count];
                        for (int j = 0; j < strtemp.Length; j++)
                        {
                            strtemp[j] = RegData[i].Groups[j + 1].Value;
                            //result[i][j] = RegData[i].Groups[j + 1].Value;
                            //this.SetText(RegData[i].Groups[j + 1].Value + "\n");
                        }
                        StrList.Add(strtemp);
                    }
                }
            }
            catch { }
            return StrList;
        }
        /// <summary>
        /// Copy Stream to MemoryStream 
        /// .Net 4.0 CopyTo();
        /// </summary>
        /// <param name="inputStream">Original Stream</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream CopyStream(Stream inputStream)
        {
            MemoryStream outputStream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = inputStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesRead = inputStream.Read(buffer, 0, Length);
            }
            inputStream.Close();
            outputStream.Close();
            return outputStream;
        }
        public static void Msg(string text)
        {
            System.Windows.Forms.MessageBox.Show(text);
        }
        #endregion
    }
}
