/**
 * 作者	: Hikaru
 * 日期	: 2011/12/11
 * 时间	: 7:24
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ilab.KanSea.Chat.UI.Controls
{
    public partial class MsgForm : Form
    {
        public MsgForm()
        {
            InitializeComponent();
            caozuo = "load";
            timer.Enabled = true;
            this.Opacity = 0;
        	this.Body.MouseEnter += new System.EventHandler((object sender, EventArgs e)=>{show();});
        	this.Body.MouseLeave += new System.EventHandler((object sender, EventArgs e)=>{hide();});
        }
        private string caozuo = "";

        private void timer_Tick(object sender, EventArgs e)
        {
            if (caozuo == "load")
            {
                this.Opacity += 0.1;
            }
            else if (caozuo == "close")
            {
                this.Opacity = this.Opacity - 0.05;
                if (this.Opacity == 0)
                    this.Hide();
            }
        }

        public void AddControl(Control control)
        {
            this.Body.Controls.Add(control);
            this.AllControlsAction();
            this.reLoadLocation();
        }
        public Control GetControl(String ControlName)
        {
            return this.Body.Controls[ControlName];
        }
        public void show(){
            try
            {
                this.Show();
	            caozuo = "load";
            }
            catch {}//(Exception e) { MessageBox.Show(e.Message); }
        }
        public void hide(){
            try
            {
	            caozuo = "close";
                GC.Collect();
            }
            catch {}//(Exception e) { MessageBox.Show(e.Message); }
        }
        public Point StartLocation
        {
            get;
            set;
        }
        public Int32 ParentFormWidth
        {
            get;
            set;
        }
        public void reLoadLocation(){
            Int32 pX = this.StartLocation.X-this.Width, pY = this.StartLocation.Y;
            if (this.StartLocation.X == 0)
            {
                pX = Cursor.Position.X;
            }
            if (pX < 0)
            {
                pX = this.StartLocation.X + this.ParentFormWidth;
            }
            if (this.StartLocation.Y == 0)
            {
                pY = Cursor.Position.Y;
            }

            this.Location = new Point(pX, pY);//窗体位置
        }
        private void AllControlsAction(){
        	try{
        		foreach(Control control in this.Body.Controls){
        			control.MouseEnter+= new System.EventHandler((object sender, EventArgs e)=>{show();});
                    if (control.Dock == DockStyle.Fill) {
                        control.MouseLeave += new System.EventHandler((object sender, EventArgs e) => { hide(); }); 
                    }
        		}
        	}catch{}
        }
        
    }
}
