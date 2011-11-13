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
using System.Data;
using System.Data.SQLite;

namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// Description of Data.
	/// </summary>
	public class Data
    {
        private SQLiteDataAdapter adp;
        private SQLiteCommand cmd;
        private SQLiteConnection conn = null;
        private DataSet ds;

		public Data()
		{
        }
        public ~Data()
        {
            if (this.conn != null)
            {
                this.close();
            }
        }
        #region 方法
        /// <summary>
        /// 单体模式 
        /// </summary>
        private static Data objInstance = null;
        /// <summary>
        /// 单体模式
        /// </summary>
        /// <returns></returns>
        public static Data getInstance()
        {
            if (objInstance == null) objInstance = new Data();
            return objInstance;
        }
        public String Path
        {
            get;
            set;
        }
        public String Table
        {
            get;
            set;
        }
        public String[] Keys
        {
            get;
            set;
        }
        public String[] Values
        {
            get;
            set;
        }
        public String Where
        {
            get;
            set;
        }
        public String Orderby
        {
            get;
            set;
        }
        public Int32 Limit
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void close()
        {
            this.conn.Close();
        }
        public bool Add()
        {
            if (this.Table == null&&!this.CheckKeyVal()) { return false; }
            string key = null;
            string value = null;
            for (int i = 0, len = this.Keys.Length; i < len; i++)
            {
                if (i>0) { key += ","; value += ","; }
                key += this.Keys[i];
                value += this.Values[i];
            }
            return this.DoExecute("INSERT INTO [" + this.Table + "] (" + key + ") VALUES ( " + value + ");");
        }
        public bool Del()
        {
            if (this.Table == null && this.Where == null) { return false; }
            return this.DoExecute("DELETE FROM [" + this.Table + "] WHERE " + this.Where + ";");
        }
        public bool Update()
        {
            if (this.Table == null && !this.CheckKeyVal()) { return false; }
            string setvalue = null;
            for (int i = 0, len = this.Keys.Length; i < len; i++)
            {
                if (i > 0) { setvalue += ","; }
                setvalue += this.Keys[i] + "=" + this.Values[i];
            }
            if (this.Where == null) { this.Where = "1"; }
            return this.DoExecute("UPDATE [" + this.Table + "] SET " + setvalue + " WHERE "+this.Where+";");
        }
        public DataSet Get()
        {
            return this.ds;
        }
        private bool CheckKeyVal()
        {
            if (this.Keys == null || this.Values == null || this.Keys.Length != this.Values.Length)
            {
                return false;
            }
            return true;
        }
        private void clean() {
            this.Table = null;
            this.Keys = null;
            this.Values = null;
            this.Where = null;
            this.Orderby = null;
            this.Limit = 1;
        }
        private bool Open()
        {
            try
            {
                this.conn = new SQLiteConnection("Data Source=" + this.Path);
                this.conn.Open();
                return true;
            }
            catch { return false; }
        }
        public bool Open(string path)
        {
            this.Path = path;
            return this.Open();
        }
        /// <summary>
        /// 执行sql语句
        /// 更新 删除 插入
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private bool DoExecute(string sql)
        {
            try
            {
                this.cmd = new SQLiteCommand(sql, this.conn);
                if (this.cmd.ExecuteNonQuery() == 0) return false;
                return true;
            }
            catch { return false; }
        }
        /// 查询并返回
        /// </summary>
        /// <param name="t"></param>
        /// <param name="w"></param>
        /// <param name="o"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public DataSet exs(string t, string w, string o, string l)
        {
            string str;
            if (t == null)
                str = w;
            else
            {
                str = "SELECT * FROM [" + t + "]";
                if (w == null) {
                    w = "1";
                }
                str = str + " WHERE " + w;
            }
            if (o != null) str = str + " ORDER BY [" + o + "] DESC";
            if (l != null) str = str + " LIMIT " + l;
            this.adp = new SQLiteDataAdapter(str, this.conn);
            this.ds = new DataSet();
            this.adp.Fill(this.ds);
            return this.ds;
        }
        #endregion
	}







    public class Sqlite
    {

        /// <summary>

        /// 获得连接对象

        /// </summary>

        /// <returns></returns>

        public static SQLiteConnection GetSQLiteConnection(string path)
        {
            return new SQLiteConnection("Data Source=" + path);
        }



        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params object[] p)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (p != null)
            {
                foreach (object parm in p)
                {
                    cmd.Parameters.AddWithValue(string.Empty, parm);
                }

                //for (int i = 0; i < p.Length; i++)

                //    cmd.Parameters[i].Value = p[i];
            }
        }
        public static DataSet ExecuteDataset(string path,string cmdText, params object[] p)
        {
            DataSet ds = new DataSet();
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection(path))
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds);
            }
            return ds;

        }

        public static DataRow ExecuteDataRow(string path, string cmdText, params object[] p)
        {
            DataSet ds = ExecuteDataset(path, cmdText, p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0];
            }
            return null;

        }
        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="cmdText">a</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string path, string cmdText, params object[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection(path))
            {
                PrepareCommand(command, connection, cmdText, p);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 返回SqlDataReader对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>

        public static SQLiteDataReader ExecuteReader(string path, string cmdText, params object[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = GetSQLiteConnection(path);
            try
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }
        /// <summary>
        /// 返回结果集中的第一行第一列，忽略其他行或列
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string path, string cmdText, params object[] p)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection(path))
            {
                PrepareCommand(cmd, connection, cmdText, p);
                return cmd.ExecuteScalar();
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cmdText"></param>
        /// <param name="countText"></param>
        /// <param name="p"></param>
        /// <returns></returns>

        public static DataSet ExecutePager(string path, ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, params object[] p)
        {

            if (recordCount < 0)
            {
                recordCount = int.Parse(ExecuteScalar(path, countText, p).ToString());
            }
            DataSet ds = new DataSet();

            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection(path))
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds, (pageIndex - 1) * pageSize, pageSize, "result");
            }
            return ds;
        }

    }

}
