﻿/**
 * 作者	: Hikaru
 * 日期	: 2011/11/7
 * 时间	: 1:01
 * QQ	: 39396682
 * Email: admin@kansea.com
 * 网站	: http://kansea.com  
 * 声明	: 未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Ilab.KanSea.Chat.UI.Class;

namespace Ilab.KanSea.Chat.UI
{
    public partial class BaseForm : Form
    {
        #region 声明
        private Bitmap _BacklightImg = ImageObject.GetResBitmap("Ilab.KanSea.Chat.UI.Resources.all_inside_bkg.png");//窗体光泽背景图片
        private Rectangle _BacklightLTRB = new Rectangle(10, 80, 10, 70);//窗体光泽重绘边界
        private int _RgnRadius=4;//设置窗口圆角
        private int Rgn;
        private Graphics g;
        private bool _IsResize=true;//是否允许改变窗口大小
        private FormSystemBtn _FormSystemBtnSet = FormSystemBtn.SystemAll;
        private Bitmap btn_closeImg = ImageObject.GetResBitmap("Ilab.KanSea.Chat.UI.Resources.btn_close.png");
        private Bitmap btn_maxImg = ImageObject.GetResBitmap("Ilab.KanSea.Chat.UI.Resources.btn_max.png");
        private Bitmap btn_miniImg = ImageObject.GetResBitmap("Ilab.KanSea.Chat.UI.Resources.btn_mini.png");
        private Bitmap btn_restoreImg = ImageObject.GetResBitmap("Ilab.KanSea.Chat.UI.Resources.btn_restore.png");
        private Size oldSize;//记录当前窗口大小
        //枚举系统按钮状态
        public enum FormSystemBtn
        {
            SystemAll = 0,
            SystemNo = 1,
            btn_close = 2,
            btn_miniAndbtn_close = 3,
            btn_maxAndbtn_close = 4
        }        
        #endregion

        #region 构造函数
        public BaseForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);//自绘
            this.SetStyle(ControlStyles.DoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            //this.SetStyle(ControlStyles.Opaque, true);//如果为真，控件将绘制为不透明，不绘制背景
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);   //透明效果
            SystemBtnSet();
        }
        #endregion

        #region 属性

        [DefaultValue(4)]
        [CategoryAttribute("KanSea窗口属性"), Description("设置窗口圆角半径")]
        public int RgnRadius
        {
            get { return this._RgnRadius; }
            set { 
                _RgnRadius = value;
                this.Invalidate(); 
                }

        }

        [CategoryAttribute("KanSea窗口属性"), Description("设置窗体光泽背景")]
        public Bitmap BacklightImg 
        {

            get { return this._BacklightImg; }
            set {
                 _BacklightImg = value;
                 this.Invalidate(); 
                }
        }

        [CategoryAttribute("KanSea窗口属性"), Description("设置窗体光泽背景重绘边界，例如 10,10,10,10")]
        public Rectangle BacklightLTRB
        {

            get { return this._BacklightLTRB; }
            set {
                _BacklightLTRB = value;
                if (_BacklightLTRB != Rectangle.Empty)
                { 
                    this.Invalidate(); 
                }
            }
        }

        [DefaultValue(true)]
        [CategoryAttribute("KanSea窗口属性"), Description("是否允许改变窗口大小")]
        public bool IsResize
        {
            get { return this._IsResize; }
            set { _IsResize = value;}
        }

        [CategoryAttribute("KanSea窗口属性"), Description("系统按钮设置")]
        public FormSystemBtn FormSystemBtnSet
        {
            get
            {
                return _FormSystemBtnSet;
            }
            set
            {
                _FormSystemBtnSet = value;
                this.Invalidate();

            }
        }
        #endregion

        #region 重写方法
        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            SetReion();
            base.OnInvalidated(e); 
        }

        //重绘窗口
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality; //高质量
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
                ImageDrawRect.DrawRect(g, _BacklightImg, ClientRectangle, Rectangle.FromLTRB(_BacklightLTRB.X, _BacklightLTRB.Y, _BacklightLTRB.Width, _BacklightLTRB.Height), 1, 1);                
            }
            catch
            { }
        }
        //重载WndProc方法
        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    //用户选择最大化按钮，最小化按钮，复原按钮或关闭按钮时，窗口将会接收该消息
                    case Win32.WM_SYSCOMMAND:
                        #region
                        if ((m.WParam != (IntPtr)Win32.SC_MAXIMIZE) && (m.WParam.ToInt32() != 0xf032))
                        {
                            if ((m.WParam == (IntPtr)Win32.SC_RESTORE) || (m.WParam.ToInt32() == 0xf122))
                            {
                                base.Size = this.oldSize;
                            }
                            else if ((m.WParam == (IntPtr)Win32.SC_MINIMIZE) || (m.WParam.ToInt32() == 0xf022))
                            {
                                if (this.oldSize.Width == 0)
                                {
                                    this.oldSize = base.Size;
                                }
                            }
                            break;
                        }
                        this.oldSize = base.Size;
                        #endregion
                        break;
                    //在需要计算窗口客户区的大小和位置时发送。通过处理这个消息，应用程序可以在窗口大小或位置改变时控制客户区的内容
                    case Win32.WM_NCCALCSIZE:
                    //窗体客户区以外的重绘消息,一般是由系统负责处理
                    case Win32.WM_NCPAINT:
                        return;
                    //鼠标移动,按下或释放都会执行该消息
                    case Win32.WM_NCHITTEST:
                        WM_NCHITTEST(ref m);
                        return;
                    //画窗体被激活或者没有被激活时的样子//http://blog.csdn.net/commandos/archive/2007/11/27/1904558.aspx
                    case Win32.WM_NCACTIVATE:
                        #region
                        if (m.WParam == (IntPtr)Win32.WM_FALSE)
                        {
                            m.Result = (IntPtr)Win32.WM_TRUE;
                        }
                        #endregion
                        return;
                    default:
                        base.WndProc(ref m);
                        return;
                }
                base.WndProc(ref m);
            }
            catch { }
        }

        private void AlBaseForm_Resize(object sender, EventArgs e)
        {
            SystemBtnSet();
        }
       
        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            this.oldSize = base.Size;
        } 

        #endregion

        #region 方法
        protected void SystemBtnSet()
        {
            switch ((int)_FormSystemBtnSet)
            {
                case 0:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, 6);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 93, 6);
                    btn_max.BackImg = btn_maxImg;
                    btn_restore.BackImg = btn_restoreImg;
                    if (WindowState == FormWindowState.Normal)
                    {
                        btn_max.Location = new Point(this.Width - 68, 6);
                        btn_restore.Location = new Point(this.Width - 68, -20);
                    }
                    else
                    {
                        btn_max.Location = new Point(this.Width - 68, -20);
                        btn_restore.Location = new Point(this.Width - 68, 6);
                    }
                    break;
                case 1:
                    btn_close.Visible = false;
                    btn_max.Visible = false;
                    btn_mini.Visible = false;
                    btn_restore.Visible = false;
                    break;
                case 2:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, 6);
                    btn_max.BackImg = btn_maxImg;
                    btn_max.Location = new Point(this.Width - 68, -20);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 93, -20);
                    btn_restore.BackImg = btn_restoreImg;
                    btn_restore.Location = new Point(this.Width - 68, -20);
                    break;
                case 3:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, 6);
                    btn_max.BackImg = btn_maxImg;
                    btn_max.Location = new Point(this.Width - 68, -20);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 68, 6);
                    btn_restore.BackImg = btn_restoreImg;
                    btn_restore.Location = new Point(this.Width - 68, -20);
                    break;
                case 4:
                    btn_close.BackImg = btn_closeImg;
                    btn_close.Location = new Point(this.Width - 43, 6);
                    btn_mini.BackImg = btn_miniImg;
                    btn_mini.Location = new Point(this.Width - 93,-20);
                    btn_max.BackImg = btn_maxImg;
                    btn_restore.BackImg = btn_restoreImg;
                    if (WindowState == FormWindowState.Normal)
                    {
                        btn_max.Location = new Point(this.Width - 68, 6);
                        btn_restore.Location = new Point(this.Width - 68, -20);
                    }
                    else
                    {
                        btn_max.Location = new Point(this.Width - 68, -20);
                        btn_restore.Location = new Point(this.Width - 68, 6);
                    }
                    break;                    
            
            }

        }
        /// <summary>
        /// 给窗口圆角
        /// </summary>
        protected void SetReion()
        {
            Rgn = Win32.CreateRoundRectRgn(5, 5, ClientRectangle.Width - 4, ClientRectangle.Height - 4, _RgnRadius, _RgnRadius);
            Win32.SetWindowRgn(this.Handle, Rgn, true);
        }

        private void WM_NCHITTEST(ref Message m)
        {
            int wparam = m.LParam.ToInt32();
            Point point = new Point(Win32.LOWORD(wparam),Win32.HIWORD(wparam));
            point = PointToClient(point);
            if (_IsResize)
            {
                if (point.X <= 8)
                {
                    if (point.Y <= 8)
                        m.Result = (IntPtr)Win32.HTTOPLEFT;
                    else if (point.Y > Height - 8)
                        m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
                    else
                        m.Result = (IntPtr)Win32.HTLEFT;
                }
                else if (point.X >= Width - 8)
                {
                    if (point.Y <= 8)
                        m.Result = (IntPtr)Win32.HTTOPRIGHT;
                    else if (point.Y >= Height - 8)
                        m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
                    else
                        m.Result = (IntPtr)Win32.HTRIGHT;
                }
                else if (point.Y <= 8)
                {
                    m.Result = (IntPtr)Win32.HTTOP;
                }
                else if (point.Y >= Height - 8)
                    m.Result = (IntPtr)Win32.HTBOTTOM;
                else
                    m.Result = (IntPtr)Win32.HTCAPTION;
            }
            else
            { m.Result = (IntPtr)Win32.HTCAPTION; }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_mini_Click(object sender, EventArgs e)
        {
            Win32.PostMessage(base.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            Win32.PostMessage(base.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MAXIMIZE, 0);
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            Win32.PostMessage(base.Handle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
        }

        private void btn_close_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btn_close, "关闭");
        }

        private void btn_max_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btn_max, "最大化");
        }

        private void btn_mini_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btn_restore, "还原");
        }

        private void btn_restore_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btn_mini, "最小化");
        }
        #endregion
    }
}
