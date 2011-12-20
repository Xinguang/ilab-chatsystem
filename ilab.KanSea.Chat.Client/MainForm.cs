/**
 * 作者	: Hikaru
 * 日期	: 2011/9/28
 * 时间	: 0:51
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ilab.KanSea.Chat.Helper;
using ilab.KanSea.Chat.Client.config;


namespace ilab.KanSea.Chat.Client
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm :Ilab.KanSea.Chat.UI.BaseForm
	{

        private HttpHelper http = HttpHelper.getInstance();
        private string sendStrTemp = null;
        private String sendLinkTemp = null;
        private string originalStr = null;
		public MainForm()
		{
            Login loginForm = new Login(this);
            loginForm.ShowDialog();
		}
        public void init()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            this.Translateto.SelectedIndex = 1;
            this.SystemBtnSet();
            //this.Size = new System.Drawing.Size(446, 380);
            //this.Size = new System.Drawing.Size(724, 380);
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        private void MessageSend_Click(object sender, EventArgs e)
        {
            this.MessageTranslate_Click(null, null);
            this.MessageRecord.SelectedText = "\n\n";
           // this.MessageRecord.InsertLink(this.UserName.Text, "USERNAME");
            this.MessageRecord.InsertLink(this.UserName.Text, "USERNAME");
            this.MessageRecord.SelectedText = "\n\n" + this.sendStrTemp + "\n\n";
        }

        private void MessageTranslate_Click(object sender, EventArgs e)
        {
            string inputStr = this.MessageInput.Text;
            if (inputStr == "" || inputStr == null) { return; }
            if (this.originalStr != inputStr + this.Translateto.Text)
            {
                this.originalStr = inputStr + this.Translateto.Text;
                string tranType = this.Translateto.Text;
                string InputParse = "";
                string ngramtable = "english";
                string localLang = this.getLocalLang(tranType);
                if (localLang == "ja")
                {
                    ngramtable = "japanese";
                    InputParse = Words.MeCabParseString(inputStr);
                    //InputParse = Words.MeCabParse(inputStr);
                }
                if (localLang == "zh-CHS" || localLang == "zh-CHT")
                {
                    ngramtable = "chinese";
                    InputParse = Words.ICTCLAParse(inputStr);
                }

                InputParse = Words.replaceSlang(InputParse, ngramtable);// InputParse;
                string linktest = HelperBase.GetString(InputParse,"([^#]*)######",1);
                InputParse = InputParse.Replace(linktest + "######", "");
                //this.MessageRecord.Text = InputParse;// InputParse;
                //this.MessageRecord.Text = Words.getslang(linktest);
                this.sendLinkTemp = Words.getslang(InputParse);
                //this.sendLinkTemp = Words.getslang(linktest);
                string tranTo = this.getTranTo();
                string tranStr = Translate.Microsoft_Get(System.Web.HttpUtility.UrlEncode(InputParse), localLang, tranTo);
                string tranBackStr = Translate.Microsoft_Get(System.Web.HttpUtility.UrlEncode(tranStr), tranTo, localLang);
                toolTips1.is_show = true;
                Int32 Tipheight =250;
                if (tranBackStr.Length > 100) { Tipheight = 400; }
                this.sendStrTemp = tranStr + "\n---------------------\n(" + inputStr + ")";
                string showtext = "解析:" + InputParse + "\n--------\n翻訳文:" + tranStr + "\n--------\n折り返し翻訳：" + tranBackStr + "\n--------\n俗語:\n" + this.sendLinkTemp;

                toolTips1.SetToolTip(this.MessageTranslate, showtext, 300, Tipheight);
            }
        }
        private string getLocalLang(string tranType)
        {
            if (HelperBase.IsHaveString(tranType, "Local"))
            {
                return getLocalLang();
            }
            else
            {
                string local = HelperBase.GetString(tranType,"(.*)\\-\\>",1);
                switch (local)
                {
                    case "Simplified Chinese":
                        local = "zh-CHS";
                        break;
                    case "Traditional Chinese":
                        local = "zh-CHT";
                        break;
                    case "Japanese":
                        local = "ja";
                        break;
                    default:
                        local = "en";
                        break;
                }
                return local;
            }
        }
        private string getTranTo()
        {
            string local = HelperBase.GetString(this.Translateto.Text, "\\-\\>(.*)", 1);
            switch (local)
            {
                case "Simplified Chinese":
                    local = "zh-CHS";
                    break;
                case "Traditional Chinese":
                    local = "zh-CHT";
                    break;
                case "Japanese":
                    local = "ja";
                    break;
                default:
                    local = "en";
                    break;
            }
            return local;
        }

        private string getLocalLang()
        {
            string systemLang = System.Globalization.CultureInfo.InstalledUICulture.Name;
            switch (systemLang)
            {
                case "zh-CN":
                    systemLang = "zh-CHS";
                    break;
                case "zh-TW":
                    systemLang = "zh-CHT";
                    break;
                case "ja-JP":
                    systemLang = "ja";
                    break;
                default:
                    break;
            }
            return systemLang;
        }

        private void MessageRecord_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            toolTips1.is_show = true;
            Int32 Tipheight = 150;
            string info = e.LinkText;
            info = HelperBase.GetString(info,"([^#]*)",1);
            string msg = HelperBase.GetString(info, "#([^$])", 1);
            String wiki = null;

            if (msg != "UserName")
            {
                this.http.Encoding = Encoding.UTF8;
                string locallang = this.getLocalLang();
                string uri = null;
                string reg = null;
                switch (locallang)
                {
                    case "zh-CHS":
                        uri = "http://zh.wikipedia.org/wiki/";
                        break;
                    case "zh-CHT":
                        uri = "http://zh-yue.wikipedia.org/wiki/";
                        break;
                    case "ja":
                        reg = "bodycontent[^<]*<[^>]*><p>(.*)<\\/p>[^<]*<table";
                        uri = "http://ja.wikipedia.org/wiki/";
                        break;
                    default:
                        uri = "http://en.wikipedia.org/wiki/";
                        break;
                }
                wiki = this.http.GetRequest(uri+info);
                wiki = HelperBase.GetString(wiki, reg, 1);
            }

            if (info.Length > 100) { Tipheight = 300; }
            toolTips1.SetToolTip(this.UserName, info + "\n--------\n"+wiki, 300, Tipheight);
        }






    }
}
