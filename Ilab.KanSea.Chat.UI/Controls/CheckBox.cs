/**
 * 作者	: Hikaru
 * 日期	: 2011/11/7
 * 时间	: 2:37
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Ilab.KanSea.Chat.UI.Class;
using System.ComponentModel;
namespace Ilab.KanSea.Chat.UI.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.CheckBox))]
	public class CheckBox: System.Windows.Forms.CheckBox
	{
        #region 声明
        private State state = State.Normal;
        private Bitmap _BackImg = ImageObject.GetResBitmap("Ilab.KanSea.Chat.UI.Resources.Checkbox.png");
        //枚鼠标状态
        private enum State
        {
            Normal = 1,
            MouseOver = 2,
            MouseDown = 3,
            Disable = 4
        }
        #endregion

        #region 构造函数
        public CheckBox()
        {
            this.SetStyle(ControlStyles.UserPaint, true);//设置控件自行绘制
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);//背景透明     
            this.SetStyle(ControlStyles.StandardDoubleClick, false);
            this.BackColor = System.Drawing.Color.Transparent;//背景设为透明

        }
        #endregion
      
        #region 属性
        [CategoryAttribute("KanSea自定义属性"), Description("CheckBox图片")]
        public Bitmap BackImg
        {
            get { return this._BackImg; }
            set
            {
                _BackImg = value;
                this.Invalidate();
            }
        }
        #endregion

        #region 方法
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);
            if (_BackImg == null)
            {
                base.OnPaint(e);
                return;
            }

            int i = (int)state;
            if (!this.Enabled) i = 4;
            if (this.CheckState == CheckState.Checked) i += 4;
            if (this.CheckState == CheckState.Indeterminate) i += 8;

            Rectangle rc = this.ClientRectangle;
            Rectangle r1 = rc;
            Rectangle textRect;
            Graphics g = e.Graphics;

            if (this.CheckAlign == ContentAlignment.MiddleLeft)//对齐状态
            {
                r1 = new Rectangle(0, (rc.Height - 15) / 2, 16, 15);
                textRect = new Rectangle(16, 0, rc.Width - 16, rc.Height);
            }
            else
            {
                r1 = new Rectangle(r1.Right - 16, (rc.Height - 16) / 2, 16, 15);
                textRect = new Rectangle(0, 0, rc.Width - 16, rc.Height);
            }
            ImageDrawRect.DrawRect(g, _BackImg, r1, Rectangle.FromLTRB(0, 0, 0, 0), i, 12);
            Color textColor = Enabled ? ForeColor : SystemColors.GrayText;
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, textRect, textColor);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            state = State.MouseOver;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            state = State.Normal;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
            state = State.MouseOver;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                state = State.MouseOver;
            this.Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
	}
}
