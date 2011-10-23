/**
 * 作者	: Hikaru
 * 日期	: 2011/10/22
 * 时间	: 23:27
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace ilab.KanSea.Chat.Helper
{
    /// <summary>
    /// Method type
    /// </summary>
    public enum methodType
    {
        POST,
        GET
    }
    /// <summary>
    /// Description of HttpHelper.
    /// </summary>
    public class HttpHelper
    {
        #region 属性
        /// <summary>
        /// HttpWebRequest对象用来发起请求
        /// </summary>
        private HttpWebRequest Request = null;
        /// <summary>
        /// 获取影响流的数据对象
        /// </summary>
        private HttpWebResponse Response = null;
        /// <summary>
        /// 读取流的对象
        /// </summary>
        private StreamReader Reader = null;
        /// <summary>
        /// 需要返回的数据对象
        /// </summary>
        private string ReturnData = "Error";
        /// <summary>
        /// 目标地址
        /// </summary>
        public string TargetUrl { get; set; }
        /// <summary>
        /// cookies
        /// </summary>
        public CookieContainer Cookies { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// Method type
        /// </summary>
        /// <summary>
        /// Post Or Get
        /// </summary>
        public methodType Method { get; set; }
        /// <summary>
        /// Accept
        /// ContentType组  多个ContentType属性 按顺序排列
        /// </summary>
        public string Accept { get; set; }
        /// <summary>
        /// ContentType返回类型
        /// text/plain（纯文本）
        /// text/html（HTML文档）
        /// application/xhtml+xml（XHTML文档）
        /// image/gif（GIF图像）
        /// image/jpeg（JPEG图像）【PHP中为：image/pjpeg】
        /// image/png（PNG图像）【PHP中为：image/x-png】
        /// video/mpeg（MPEG动画）
        /// application/octet-stream（任意的二进制数据）
        /// application/pdf（PDF文档）
        /// application/msword（Microsoft Word文件）
        /// message/rfc822（RFC 822形式）
        /// multipart/alternative（HTML邮件的HTML形式和纯文本形式，相同内容使用不同形式表示）
        /// application/x-www-form-urlencoded（使用HTTP的POST方法提交的表单）
        /// multipart/form-data（同上，但主要用于表单提交时伴随文件上传的场合）
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
        /// </summary>
        public string UserAgent { get; set; }

        #endregion
        #region 方法
        /// <summary>
        /// 构造函数
        /// </summary>
        public HttpHelper()
        {
            this.Cookies = new CookieContainer();
        }
        /// <summary>
        /// 单体模式 
        /// </summary>
        private static HelperBase objInstance = null;
        /// <summary>
        /// 单体模式
        /// </summary>
        /// <returns></returns>
        public static HelperBase getInstance()
        {
            if (objInstance == null) objInstance = new HelperBase();
            return objInstance;
        }
        /// <summary>
        /// 为请求准备参数
        /// </summary>
        /// <param name="targetUrl">请求的URL地址</param>
        /// <param name="method">请求方式Get或者Post</param>
        /// <param name="accept">Accept</param>
        /// <param name="contentType">ContentType返回类型</param>
        /// <param name="userAgent">UserAgent客户端的访问类型，包括浏览器版本和操作系统信息</param>
        /// <param name="encoding">读取数据时的编码方式</param>
        /// <param name="cookies">Cookies</param>
        private void SetRequest(string targetUrl, string method, string accept, string contentType, string userAgent, Encoding encoding, CookieContainer cookies)
        {
            this.TargetUrl = null == targetUrl ?
                this.TargetUrl :
                targetUrl;
            if (null == this.TargetUrl || "" == this.TargetUrl) { return; }
            //请求的URL地址
            this.TargetUrl = this.GetNewUrl(this.TargetUrl);
            //请求方式Get或者Post
            this.Method = null == method ? this.Method : -1 == method.IndexOf("p") ? methodType.GET : methodType.POST;
            //页面元素默认值
            //image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*
            this.Accept = null == accept ? null == this.Accept ? "text/html, application/xhtml+xml, */*" : this.Accept : accept;
            //ContentType返回类型
            this.ContentType = null == contentType ? null == this.ContentType ? methodType.POST == this.Method ? "application/x-www-form-urlencoded" : "application/xhtml+xml" : this.ContentType : contentType;
            //UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
            this.UserAgent = null == userAgent ? null == this.UserAgent ? "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)" : this.UserAgent : userAgent;
            //读取数据时的编码方式
            this.Encoding = null == encoding ? null == this.Encoding ? Encoding.Default : this.Encoding : encoding;
            //Cookies
            this.Cookies = null == cookies ? this.Cookies : cookies;


            //初始化对像，并设置请求的URL地址
            this.Request = (HttpWebRequest)WebRequest.Create(this.TargetUrl);
            //请求方式Get或者Post
            this.Request.Method = this.Method.ToString();
            //Accept
            this.Request.Accept = this.Accept;
            //ContentType返回类型
            this.Request.ContentType = this.ContentType;
            //UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
            this.Request.UserAgent = this.UserAgent;
            //SSl凭证
            this.Request.Credentials = CredentialCache.DefaultCredentials;
            //Cookies
            this.Request.CookieContainer = this.Cookies;
            //支持跳转页面，查询结果将是跳转后的页面
            this.Request.AllowAutoRedirect = true;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="targetUrl">URL</param>
        public void GetRequest(string targetUrl)
        {
            this.GetRequest(targetUrl, methodType.GET.ToString(), null, null, null, null, null, null);
        }
        /// <summary>
        /// Post 
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="strPostdata">Post Data</param>
        public void GetRequest(string targetUrl, string strPostdata)
        {
            this.GetRequest(targetUrl, methodType.POST.ToString(), null, null, null, null, null, strPostdata);
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="encoding">Encoding</param>
        public void GetRequest(string targetUrl, Encoding encoding)
        {
            this.GetRequest(targetUrl, methodType.GET.ToString(), null, null, null, encoding, null, null);
        }
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="strPostdata">Post Data</param>
        /// <param name="encoding">Encoding</param>
        public void GetRequest(string targetUrl, string strPostdata, Encoding encoding)
        {
            this.GetRequest(targetUrl, methodType.POST.ToString(), null, null, null, encoding, null, strPostdata);
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="cookies">Cookies</param>
        public void GetRequest(string targetUrl, CookieContainer cookies)
        {
            this.GetRequest(targetUrl, methodType.GET.ToString(), null, null, null, null, cookies, null);
        }
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="strPostdata">Post Data</param>
        /// <param name="cookies">Cookies</param>
        public void GetRequest(string targetUrl, string strPostdata, CookieContainer cookies)
        {
            this.GetRequest(targetUrl, methodType.POST.ToString(), null, null, null, null, cookies, strPostdata);
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="cookies">Cookies</param>
        public void GetRequest(string targetUrl, Encoding encoding, CookieContainer cookies)
        {
            this.GetRequest(targetUrl, methodType.GET.ToString(), null, null, null, encoding, cookies, null);
        }
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="strPostdata">Post Data</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="cookies">Cookies</param>
        public void GetRequest(string targetUrl, string strPostdata, Encoding encoding, CookieContainer cookies)
        {
            this.GetRequest(targetUrl, methodType.POST.ToString(), null, null, null, encoding, cookies, strPostdata);
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="accept">Accept</param>
        /// <param name="contentType">Content Type</param>
        /// <param name="userAgent">User Agent</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="cookies">Cookies</param>
        public void GetRequest(string targetUrl, string accept, string contentType, string userAgent, Encoding encoding, CookieContainer cookies)
        {
            this.GetRequest(targetUrl, methodType.GET.ToString(), accept, contentType, userAgent, encoding, cookies, null);
        }
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="accept">Accept</param>
        /// <param name="contentType">Content Type</param>
        /// <param name="userAgent">User Agent</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="cookies">Cookies</param>
        /// <param name="strPostdata">Post Data</param>
        public void GetRequest(string targetUrl, string accept, string contentType, string userAgent, Encoding encoding, CookieContainer cookies, string strPostdata)
        {
            this.GetRequest(targetUrl, methodType.POST.ToString(), accept, contentType, userAgent, encoding, cookies, strPostdata);
        }
        /// <summary>
        /// Post And Get
        /// </summary>
        /// <param name="targetUrl">URL</param>
        /// <param name="method">Method</param>
        /// <param name="accept">Accept</param>
        /// <param name="contentType">Content Type</param>
        /// <param name="userAgent">User Agent</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="cookies">Cookies</param>
        /// <param name="strPostdata">Post Data</param>
        public void GetRequest(string targetUrl, string method, string accept, string contentType, string userAgent, Encoding encoding, CookieContainer cookies, string strPostdata)
        {
            this.SetRequest(targetUrl, method, accept, contentType, userAgent, encoding, cookies);
            if (null != strPostdata && methodType.POST == this.Method)
            {
                byte[] buffer = this.Encoding.GetBytes(strPostdata);
                this.Request.ContentLength = buffer.Length;
                this.Request.GetRequestStream().Write(buffer, 0, buffer.Length);
                //关闭没问题把......
                this.Request.GetRequestStream().Close();
                #region 得到请求的response
                try
                {
                    using (this.Response = (HttpWebResponse)this.Request.GetResponse())
                    {
                        Stream ResposeStream = this.Response.GetResponseStream();

                        if (null != this.Response.ContentEncoding && this.Response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                        {
                            using (this.Reader = new StreamReader(new GZipStream(this.Response.GetResponseStream(), CompressionMode.Decompress), this.Encoding))
                            {
                                this.ReturnData = this.Reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            using (this.Reader = new StreamReader(this.Response.GetResponseStream(), this.Encoding))
                            {
                                this.ReturnData = this.Reader.ReadToEnd();
                            }
                        }

                        //未设置编码 或设为系统编码时  自动检测页面编码
                        if (Encoding.Default == this.Encoding){
                            string charset = HelperBase.GetString(this.ReturnData, "<meta([^<]*)charset=([^<]*)[\"']",2);
                        }
                    }
                }
                catch { ReturnData = "Error"; }
                #endregion
            }
        }
        /// <summary>
        /// 防止缓存 给url添加参数
        /// </summary>
        /// <param name="targetUrl">目标地址</param>
        /// <returns>新地址</returns>
        private string GetNewUrl(string targetUrl)
        {
            if (targetUrl.IndexOf("?") != -1)
                targetUrl += "&";
            else
                targetUrl += "?";
            targetUrl += HelperBase.GetRandomName();
            return targetUrl;


        }
        #endregion
    }
}
