﻿/**
 * 作者	: Hikaru
 * 日期	: 2011/9/28
 * 时间	: 1:24
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ilab.KanSea.Chat.Helper;

namespace ilab.KanSea.Chat.Server
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Ilab.KanSea.Chat.UI.BaseForm
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            this.Size = new System.Drawing.Size(226, 127);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        private void TCPServer_Click(object sender, EventArgs e)
        {
            Sockets TcpServer = Sockets.getInstance();
            TcpServer.serverStart();
            this.Server_msg.Text = "Server is running";
            this.StopServer.Visible = true;
            this.TCPServer.Visible = false;
        }

        private void slang_Click(object sender, EventArgs e)
        {

        }

        private void StopServer_Click(object sender, EventArgs e)
        {
            Sockets TcpServer = Sockets.getInstance();
            TcpServer.serverStop();
            this.Server_msg.Text = "Server is stopped";
            this.StopServer.Visible = false;
            this.TCPServer.Visible = true;
        }
	}
}
