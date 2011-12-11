/**
 * 作者	: Hikaru
 * 日期	: 2011/12/5
 * 时间	: 5:29
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
namespace ilab.KanSea.Chat.Client
{
	partial class Login
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.Username = new Ilab.KanSea.Chat.UI.Controls.TextBox();
            this.Password = new Ilab.KanSea.Chat.UI.Controls.TextBox();
            this.button_Close = new Ilab.KanSea.Chat.UI.Controls.Button();
            this.button_OK = new Ilab.KanSea.Chat.UI.Controls.Button();
            this.label_username = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.BackColor = System.Drawing.Color.Transparent;
            this.Username.font = new System.Drawing.Font("微软雅黑", 9F);
            this.Username.Ico = null;
            this.Username.IcoPadding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.Username.Isico = false;
            this.Username.IsPass = false;
            this.Username.lines = new string[0];
            this.Username.Location = new System.Drawing.Point(78, 53);
            this.Username.MaxLength = 32767;
            this.Username.Multiline = false;
            this.Username.Name = "Username";
            this.Username.PassChar = '\0';
            this.Username.ReadOnly = false;
            this.Username.Size = new System.Drawing.Size(160, 22);
            this.Username.TabIndex = 4;
            this.Username.text = "";
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.Color.Transparent;
            this.Password.font = new System.Drawing.Font("微软雅黑", 9F);
            this.Password.Ico = null;
            this.Password.IcoPadding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.Password.Isico = false;
            this.Password.IsPass = true;
            this.Password.lines = new string[0];
            this.Password.Location = new System.Drawing.Point(78, 94);
            this.Password.MaxLength = 32767;
            this.Password.Multiline = false;
            this.Password.Name = "Password";
            this.Password.PassChar = '●';
            this.Password.ReadOnly = false;
            this.Password.Size = new System.Drawing.Size(160, 22);
            this.Password.TabIndex = 5;
            this.Password.text = "";
            // 
            // button_Close
            // 
            this.button_Close.BackColor = System.Drawing.Color.Transparent;
            this.button_Close.BackImg = null;
            this.button_Close.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button_Close.Location = new System.Drawing.Point(54, 134);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 6;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = false;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.BackImg = null;
            this.button_OK.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button_OK.Location = new System.Drawing.Point(163, 134);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 7;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.BackColor = System.Drawing.Color.Transparent;
            this.label_username.Location = new System.Drawing.Point(52, 59);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(16, 12);
            this.label_username.TabIndex = 8;
            this.label_username.Text = "ID";
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.BackColor = System.Drawing.Color.Transparent;
            this.label_Password.Location = new System.Drawing.Point(21, 99);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(54, 12);
            this.label_Password.TabIndex = 9;
            this.label_Password.Text = "Password";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 248);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.FormSystemBtnSet = Ilab.KanSea.Chat.UI.BaseForm.FormSystemBtn.SystemNo;
            this.IsResize = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Text = "Login";
            this.Controls.SetChildIndex(this.label_username, 0);
            this.Controls.SetChildIndex(this.button_OK, 0);
            this.Controls.SetChildIndex(this.button_Close, 0);
            this.Controls.SetChildIndex(this.Password, 0);
            this.Controls.SetChildIndex(this.Username, 0);
            this.Controls.SetChildIndex(this.label_Password, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private Ilab.KanSea.Chat.UI.Controls.TextBox Username;
        private Ilab.KanSea.Chat.UI.Controls.TextBox Password;
        private Ilab.KanSea.Chat.UI.Controls.Button button_Close;
        private Ilab.KanSea.Chat.UI.Controls.Button button_OK;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Label label_Password;
	}
}
