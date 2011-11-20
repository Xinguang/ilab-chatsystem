/**
 * 作者	: Hikaru
 * 日期	: 2011/11/21
 * 时间	: 0:50
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Data;
using System.Collections.Generic;
using ilab.KanSea.Chat.Helper.Const;
using ilab.KanSea.Chat.Helper.model;
using MySql.Data.MySqlClient;

namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// Description of MysqlHelper.
	/// </summary>
	public class MysqlHelper
	{
        private static MySqlConnection conn = null;
        private static MySqlDataAdapter da = null;
        private static MySqlCommand cmd = null;

        private static bool Open()
        {
            try
            {
                if (MysqlHelper.conn == null)
                {
                    MysqlHelper.conn = new MySqlConnection(Data_Server.ConnStr);
                    MysqlHelper.conn.Open();
                }
                return true;
            }
            catch { return false; }
        }
        private static bool Close()
        {
            try
            {
                if (MysqlHelper.conn != null)
                {
                    MysqlHelper.conn.Close();
                }
                if (MysqlHelper.cmd != null)
                {
                    MysqlHelper.cmd.Dispose();
                }
                if (MysqlHelper.da != null)
                {
                    MysqlHelper.da.Dispose();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #region Ngram方法
        public static bool Add(string table,Ngram ngram)
        {
            MysqlHelper.Open();
            MysqlHelper.cmd.Parameters.Clear();
            MysqlHelper.cmd.Connection = MysqlHelper.conn;
            MysqlHelper.cmd.CommandText = "INSERT INRO " + table+" VALUES ({},{},{})";
            return false;
        }
        public static bool Del(string table, Ngram ngram)
        {
            MysqlHelper.Open();
            return false;
        }
        public static bool update(string table, Ngram ngram)
        {
            MysqlHelper.Open();
            return false;
        }
        public static Ngram[] select(string table, Ngram ngram)
        {
            MysqlHelper.Open();
            return null;
        }

        #endregion



        #region Slang方法
        public static bool Add(Slang slang)
        {
            if (!slang.IsNull())
            {
                string CommandText = "INSERT INTO `" + Data_Server.Table_Slang + "` (word ,explanation ,synonyms ,lang )VALUES (@WORD,@EXPLANATION,@SYNONYMS,@LANG);";

                MysqlHelper.Open();
                MysqlHelper.cmd = new MySqlCommand(CommandText, MysqlHelper.conn);
                //MysqlHelper.cmd.CommandType = CommandType.StoredProcedure;
                MysqlHelper.cmd.Parameters.Add("@WORD", MySqlDbType.Char,255,"word").Value = "slang.word";
                MysqlHelper.cmd.Parameters.Add("@EXPLANATION", MySqlDbType.Text).Value = "slang.explanation";
                MysqlHelper.cmd.Parameters.Add("@SYNONYMS", MySqlDbType.Char).Value = "slang.synonyms";
                MysqlHelper.cmd.Parameters.Add("@LANG", MySqlDbType.Char).Value = "slang.lang";
                foreach (MySqlParameter p in MysqlHelper.cmd.Parameters)
                {
                    System.Windows.Forms.MessageBox.Show(p.Value.ToString() + p.ParameterName);
                }
                MysqlHelper.cmd.ExecuteNonQuery();
            }
            return false;
        }
        public static bool Del(Slang slang)
        {
            MysqlHelper.Open();
            return false;
        }
        public static bool update(Slang slang)
        {
            MysqlHelper.Open();
            return false;
        }
        public static Slang[] select(Slang slang)
        {
            MysqlHelper.Open();
            return null;
        }
        #endregion
	}
}
