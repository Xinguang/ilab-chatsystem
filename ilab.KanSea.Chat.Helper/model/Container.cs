/**
 * 作者	: Hikaru
 * 日期	: 2011/10/6
 * 时间	: 1:01
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Net.Sockets;
using System.Threading;

namespace ilab.KanSea.Chat.Helper.model
{
	/// <summary>
	/// Description of container.
	/// </summary>
    public class Container : IEquatable<Container>
    {
        #region 属性
        public string userName { get; set; }
        public Socket clientSocket { get; set; }
        public Thread clientThread { get; set; }
		#endregion
		#region 属性

        public bool Equals(Container other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return clientSocket.Equals(other.clientSocket) && clientThread.Equals(other.clientThread);
        }
		#endregion
	}
}
