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
using System.Security.Cryptography;
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
        public static object Deserialize(byte[] datas)
        {
            return Deserialize(datas,0);
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
            return Encrypt(Encrypt(Encrypt(input, "KanSea"), "ilab"),"chatsystem");
        }
        /// <summary>
        /// 解密发送信息
        /// </summary>
        public static byte[] Decrypt(byte[] input)
        {
            return Decrypt(Decrypt(Decrypt(input, "chatsystem"), "ilab"), "KanSea");
        }
        /// <summary>
        /// 加密Byte[]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private static byte[] Encrypt(byte[] input, String Password)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();


            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            CryptoStream cs = new CryptoStream(ms,
               alg.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(input, 0, input.Length);

            cs.Close();

            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }
        /// <summary>
        /// 解密 Byte[]
        /// </summary>
        /// <param name="cipherData"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private static byte[] Decrypt(byte[] cipherData, String Password)
        {
            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(cipherData, 0, cipherData.Length);

            cs.Close();

            byte[] decryptedData = ms.ToArray();

            return decryptedData;
        }
        #endregion
    }
}
