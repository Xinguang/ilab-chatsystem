/**
 * 作者	: Hikaru
 * 日期	: 2011/12/11
 * 时间	: 7:28
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Ilab.KanSea.Chat.UI.Controls
{
	/// <summary>
	/// Description of ToolTips.
	/// </summary>
	public class ToolTips: UserControl
	{
        private MsgForm Tips = null;
        public ToolTips()
        {
            this.Tips = new MsgForm();
        }
        public Boolean is_show { get; set; }

        public void SetToolTip(Control control, String Text,int width,int height)
        {
            this.Tips.Size = new System.Drawing.Size(width, height);
            this.SetToolTip(control, Text);
        }
        public void SetToolTip(Control control,String Text)
        {
            control.Controls.Clear();
            Label TipText = new Label();
            TipText.Text = Text;
            TipText.Name = "_TipText";
            TipText.Visible = false;
            control.Controls.Add(TipText);
            TipText = null;
            //this.Tips.StartLocation = this.StartLocation;
            control.MouseEnter += new EventHandler((object sender, EventArgs e) => { ShowTips(control); });
            control.MouseLeave += new EventHandler((object sender, EventArgs e) => { HideTips(); });
            if (this.is_show)
            {
                ShowTips(control);
            }
            //this.Tips.panel1.MouseLeave += new EventHandler((object sender, EventArgs e) => { ShowTips(control); });
            //this.Tips.panel1.MouseEnter += new EventHandler((object sender, EventArgs e) => { HideTips(); });
        }
        private void ShowTips(Control control)
        {
            try
            {
                try
                {
                    Tips.GetControl("_MsgTextBox").Text = control.Controls["_TipText"].Text;
                }
                catch
                {
                    RichTextBox MsgTextBox = new RichTextBox();
                    MsgTextBox.Name = "_MsgTextBox";
                    MsgTextBox.Text = control.Controls["_TipText"].Text;
                    MsgTextBox.ReadOnly = true;
                    MsgTextBox.BorderStyle = BorderStyle.None;
                    MsgTextBox.Dock = DockStyle.Fill;
                    //Tips.Controls["Body"].Controls.Add(MsgTextBox);
                    Tips.AddControl(MsgTextBox);
                }
                this.Tips.StartLocation = new Point(this.ParentForm.Location.X, 0);
                this.Tips.ParentFormWidth = this.ParentForm.Width;
                Tips.reLoadLocation();
                Tips.show();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
        private void HideTips()
        {
            try
            {
            	Tips.hide();
                GC.Collect();
            }
            catch {}//(Exception e) { MessageBox.Show(e.Message); }
        }
	}
}
