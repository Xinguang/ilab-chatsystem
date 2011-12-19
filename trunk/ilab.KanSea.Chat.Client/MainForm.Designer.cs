/**
 * 作者	: Hikaru
 * 日期	: 2011/9/28
 * 时间	: 0:51
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
namespace ilab.KanSea.Chat.Client
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
            this.MessageRecord = new Ilab.KanSea.Chat.UI.Controls.RichTextBoxEx();
            this.MessageInput = new Ilab.KanSea.Chat.UI.Controls.TextBox();
            this.MessageSend = new Ilab.KanSea.Chat.UI.Controls.Button();
            this.UserName = new System.Windows.Forms.Label();
            this.toolTips1 = new Ilab.KanSea.Chat.UI.Controls.ToolTips();
            this.MessageTranslate = new Ilab.KanSea.Chat.UI.Controls.Button();
            this.Translateto = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageRecord
            // 
            this.MessageRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageRecord.Location = new System.Drawing.Point(2, 2);
            this.MessageRecord.MaxLength = 32767;
            this.MessageRecord.Name = "MessageRecord";
            this.MessageRecord.ReadOnly = true;
            this.MessageRecord.Size = new System.Drawing.Size(417, 190);
            this.MessageRecord.TabIndex = 4;
            this.MessageRecord.Text = "";
            this.MessageRecord.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.MessageRecord_LinkClicked);
            // 
            // MessageInput
            // 
            this.MessageInput.BackColor = System.Drawing.Color.Transparent;
            this.MessageInput.font = new System.Drawing.Font("微软雅黑", 9F);
            this.MessageInput.Ico = null;
            this.MessageInput.IcoPadding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.MessageInput.Isico = false;
            this.MessageInput.IsPass = false;
            this.MessageInput.lines = new string[0];
            this.MessageInput.Location = new System.Drawing.Point(13, 236);
            this.MessageInput.MaxLength = 32767;
            this.MessageInput.Multiline = true;
            this.MessageInput.Name = "MessageInput";
            this.MessageInput.PassChar = '\0';
            this.MessageInput.ReadOnly = false;
            this.MessageInput.Size = new System.Drawing.Size(421, 103);
            this.MessageInput.TabIndex = 5;
            this.MessageInput.text = "";
            // 
            // MessageSend
            // 
            this.MessageSend.BackColor = System.Drawing.Color.Transparent;
            this.MessageSend.BackImg = null;
            this.MessageSend.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.MessageSend.Location = new System.Drawing.Point(353, 345);
            this.MessageSend.Name = "MessageSend";
            this.MessageSend.Size = new System.Drawing.Size(75, 23);
            this.MessageSend.TabIndex = 7;
            this.MessageSend.Text = "Send";
            this.MessageSend.UseVisualStyleBackColor = false;
            this.MessageSend.Click += new System.EventHandler(this.MessageSend_Click);
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.BackColor = System.Drawing.Color.Transparent;
            this.UserName.Location = new System.Drawing.Point(13, 13);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(58, 12);
            this.UserName.TabIndex = 8;
            this.UserName.Text = "UserName";
            // 
            // toolTips1
            // 
            this.toolTips1.is_show = false;
            this.toolTips1.Location = new System.Drawing.Point(83, 8);
            this.toolTips1.Name = "toolTips1";
            this.toolTips1.Size = new System.Drawing.Size(150, 150);
            this.toolTips1.TabIndex = 9;
            // 
            // MessageTranslate
            // 
            this.MessageTranslate.BackColor = System.Drawing.Color.Transparent;
            this.MessageTranslate.BackImg = null;
            this.MessageTranslate.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.MessageTranslate.Location = new System.Drawing.Point(15, 345);
            this.MessageTranslate.Name = "MessageTranslate";
            this.MessageTranslate.Size = new System.Drawing.Size(75, 23);
            this.MessageTranslate.TabIndex = 10;
            this.MessageTranslate.Text = "Translate to";
            this.MessageTranslate.UseVisualStyleBackColor = false;
            this.MessageTranslate.Click += new System.EventHandler(this.MessageTranslate_Click);
            this.MessageTranslate.MouseEnter += new System.EventHandler(this.MessageTranslate_Click);
            // 
            // Translateto
            // 
            this.Translateto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Translateto.FormattingEnabled = true;
            this.Translateto.Items.AddRange(new object[] {
            "Local->English",
            "Local->Simplified Chinese",
            "Local->Traditional Chinese",
            "Local->Japanese",
            "Simplified Chinese->English",
            "Simplified Chinese->Japanese",
            "Traditional Chinese->English",
            "Traditional Chinese->Japanese",
            "Japanese->English",
            "Japanese->Simplified Chinese",
            "Japanese->Traditional Chinese",
            "English->Simplified Chinese",
            "English->Traditional Chinese",
            "English->Japanese"});
            this.Translateto.Location = new System.Drawing.Point(96, 345);
            this.Translateto.Name = "Translateto";
            this.Translateto.Size = new System.Drawing.Size(153, 20);
            this.Translateto.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MessageRecord);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.toolTips1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(13, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 194);
            this.panel1.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(419, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2, 190);
            this.panel4.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(2, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(419, 2);
            this.panel5.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2, 192);
            this.panel3.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 192);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(421, 2);
            this.panel2.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 381);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Translateto);
            this.Controls.Add(this.MessageTranslate);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.MessageSend);
            this.Controls.Add(this.MessageInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.FormSystemBtnSet = Ilab.KanSea.Chat.UI.BaseForm.FormSystemBtn.btn_miniAndbtn_close;
            this.IsResize = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "ilab.KanSea.Chat.Client";
            this.Controls.SetChildIndex(this.MessageInput, 0);
            this.Controls.SetChildIndex(this.MessageSend, 0);
            this.Controls.SetChildIndex(this.UserName, 0);
            this.Controls.SetChildIndex(this.MessageTranslate, 0);
            this.Controls.SetChildIndex(this.Translateto, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        private Ilab.KanSea.Chat.UI.Controls.RichTextBoxEx MessageRecord;
        private Ilab.KanSea.Chat.UI.Controls.TextBox MessageInput;
        private Ilab.KanSea.Chat.UI.Controls.Button MessageSend;
        private System.Windows.Forms.Label UserName;
        private Ilab.KanSea.Chat.UI.Controls.ToolTips toolTips1;
        private Ilab.KanSea.Chat.UI.Controls.Button MessageTranslate;
        private System.Windows.Forms.ComboBox Translateto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
	}
}
