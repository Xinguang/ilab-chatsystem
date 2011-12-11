/**
 * 作者	: Hikaru
 * 日期	: 2011/12/5
 * 时间	: 5:29
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using ilab.KanSea.Chat.Client.config;
using ilab.KanSea.Chat.Helper;
using ilab.KanSea.Chat.Helper.model;

namespace ilab.KanSea.Chat.Client
{
	/// <summary>
	/// Description of Login.
	/// </summary>
	public partial class Login :Ilab.KanSea.Chat.UI.BaseForm
	{
        private string system_lang = System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower();

        private Sockets clientSocket = Sockets.getInstance();
        private MainForm parentForm = null;
		public Login(MainForm pFrom)
		{
            this.parentForm = pFrom;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            this.Size = new System.Drawing.Size(290, 167);
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.parentForm.Close();
            this.Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = this.Username.Text;
            userInfo.Password = this.Password.Text;

            if (userInfo.UserName.Equals(""))
            {
                switch (this.system_lang)
                {
                    case "zh-cn":
                    case "zh-tw":
                        MessageBox.Show(lang.cn_login_noid);
                        break;
                    case "ja-jp":
                        MessageBox.Show(lang.ja_login_noid);
                        break;
                    default:
                        MessageBox.Show(lang.en_login_noid);
                        break;
                }
                return;
            }
            if (userInfo.Password.Equals(""))
            {
                switch (this.system_lang)
                {
                    case "zh-cn":
                    case "zh-tw":
                        MessageBox.Show(lang.cn_login_nopass);
                        break;
                    case "ja-jp":
                        MessageBox.Show(lang.ja_login_nopass);
                        break;
                    default:
                        MessageBox.Show(lang.en_login_nopass);
                        break;
                }
                return;
            }
            
            try
            {
                this.clientSocket.send(userInfo, MessageStatus.Login);
                ///////////////
                this.Close();
                this.parentForm.init();
                ///////////////
                this.clientSocket.clientListen();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
	}
}
