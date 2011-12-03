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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ilab.KanSea.Chat.Client
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm :Ilab.KanSea.Chat.UI.BaseForm
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            this.Size = new System.Drawing.Size(446, 380);
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	}
}
