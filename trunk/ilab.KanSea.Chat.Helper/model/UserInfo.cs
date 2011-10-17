/**
 * 作者	: Hikaru
 * 日期	: 2011/10/10
 * 时间	: 12:10
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Net;

namespace ilab.KanSea.Chat.Helper.model
{
	/// <summary>
	/// user information model
	/// </summary>
    [Serializable]
    public class UserInfo : IEquatable<UserInfo>
    {
        #region 属性
		/// <summary>
		/// user name
		/// </summary>
        public string UserName {get;set;}
        /// <summary>
        /// password
        /// </summary>
        public string Password {get;set;}
        /// <summary>
        /// last login date
        /// </summary>
        public DateTime LoginDate {get;set;}
        /// <summary>
        /// is online
        /// </summary>
        public bool IsOnline {get;set;}
        /// <summary>
        /// user ipaddress 
        /// </summary>
        public IPEndPoint UserAddress { get; set; }

        #endregion

        #region 方法
        public bool Equals(UserInfo other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return true;
        }
        #endregion
	}
}
