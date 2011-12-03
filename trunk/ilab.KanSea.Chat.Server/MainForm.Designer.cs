/**
 * 作者	: Hikaru
 * 日期	: 2011/9/28
 * 时间	: 1:24
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
namespace ilab.KanSea.Chat.Server
{
	partial class MainForm
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
            this.slang = new Ilab.KanSea.Chat.UI.Controls.Button();
            this.TCPServer = new Ilab.KanSea.Chat.UI.Controls.Button();
            this.SuspendLayout();
            // 
            // slang
            // 
            this.slang.BackColor = System.Drawing.Color.Transparent;
            this.slang.BackImg = null;
            this.slang.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.slang.Location = new System.Drawing.Point(23, 52);
            this.slang.Name = "slang";
            this.slang.Size = new System.Drawing.Size(75, 23);
            this.slang.TabIndex = 4;
            this.slang.Text = "俗語収集";
            this.slang.UseVisualStyleBackColor = false;
            // 
            // TCPServer
            // 
            this.TCPServer.BackColor = System.Drawing.Color.Transparent;
            this.TCPServer.BackImg = null;
            this.TCPServer.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.TCPServer.Location = new System.Drawing.Point(127, 52);
            this.TCPServer.Name = "TCPServer";
            this.TCPServer.Size = new System.Drawing.Size(75, 23);
            this.TCPServer.TabIndex = 5;
            this.TCPServer.Text = "TCPserver";
            this.TCPServer.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 127);
            this.Controls.Add(this.slang);
            this.Controls.Add(this.TCPServer);
            this.Name = "MainForm";
            this.Text = "ilab.KanSea.Chat.Server";
            this.Controls.SetChildIndex(this.TCPServer, 0);
            this.Controls.SetChildIndex(this.slang, 0);
            this.ResumeLayout(false);

		}

        private Ilab.KanSea.Chat.UI.Controls.Button slang;
        private Ilab.KanSea.Chat.UI.Controls.Button TCPServer;
	}
}
