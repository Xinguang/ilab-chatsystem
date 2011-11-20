/**
 * 作者	: Hikaru
 * 日期	: 2011/11/21
 * 时间	: 5:14
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;

namespace ilab.KanSea.Chat.Helper.model
{
	/// <summary>
	/// Description of Slang.
	/// </summary>
	public class Slang
    {
        #region 属性
        public int id { get; set; }
        public string word { get; set; }
        public string explanation { get; set; }
        public string synonyms { get; set; }
        public string lang { get; set; }
        public DateTime insertdate { get; set; }
        #endregion

        #region 方法
        public bool Equals(Slang other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return id.Equals(other.id);
        }
        public bool IsNull()
        {
            return word.Equals(null) && explanation.Equals(null) && synonyms.Equals(null) && lang.Equals(null);
        }
        #endregion
	}
}
