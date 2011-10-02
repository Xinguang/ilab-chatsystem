/**
 * 作者	: Hikaru
 * 日期	: 2011/10/3
 * 时间	: 0:26
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace ilab.KanSea.Chat.Helper
{
	/// <summary>
	/// Description of BufferHelper.
	/// </summary>
	public class BufferHelper
	{
		public BufferHelper()
		{
        }
        #region 方法
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">要被序列化的类</param>
        /// <returns>byte[]</returns>
        public static byte[] Serialize(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, obj);
            byte[] datas = stream.ToArray();
            stream.Dispose();
            return datas;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="datas">byte[]数据</param>
        /// <param name="index">开始位置</param>
        /// <returns>object</returns>
        public static object Deserialize(byte[] datas, int index)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(datas, index, datas.Length - index);
            object obj = bf.Deserialize(stream);
            stream.Dispose();
            return obj;
        }
        /// <summary>
        /// 加密发送信息
        /// </summary>
        public static byte[] Encrypt(byte[] input)
        {
            return null;
        }
        /// <summary>
        /// 解密发送信息
        /// </summary>
        public static byte[] Decrypt(byte[] input)
        {
            return null;
        }
        #endregion
    }
}
