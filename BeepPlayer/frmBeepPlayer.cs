using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace BeepPlayer
{
    public partial class frmBeepPlayer : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        // C4, D4, E4, F4, G4, A4, B4, C5
        int[] freq = { 523, 587, 659, 698, 784, 880, 988, 1046 };

        int initWidth = 0;
        int initHeight = 0;
        Dictionary<string, Rect> initControl = new Dictionary<string, Rect>();
        public frmBeepPlayer()
        {
            InitializeComponent();
            InitializeButton();
        }
        private void InitializeButton()
        {
            // 讓btn1~btn8共用同一個事件處理函式
            btn2.Click += btn1_Click;
            btn3.Click += btn1_Click;
            btn4.Click += btn1_Click;
            btn5.Click += btn1_Click;
            btn6.Click += btn1_Click;
            btn7.Click += btn1_Click;
            btn8.Click += btn1_Click;
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            Beep(freq[btn.TabIndex], 300);
            btn.Enabled = true;
        }

        private void frmBeepPlayer_Load(object sender, EventArgs e)
        {
            // MessageBox.Show("歡迎使用BeepPlayer！\n\n請按下按鈕來播放不同的音符。");
            this.initWidth = this.palMain.Width;
            this.initHeight = this.palMain.Height;
            foreach (Control ctl in this.palMain.Controls)
            {
                this.initControl.Add(ctl.Name, new Rect(ctl.Left, ctl.Top,
                ctl.Width, ctl.Height));
            }
        }

        private void frmBeepPlayer_SizeChanged(object sender, EventArgs e)
        {
            // 【防呆 1】確保 Load 事件已經執行過。
            // 如果 initWidth 或 initHeight 還是 0，代表還沒初始化完畢，直接跳出不要做事。
            if (this.initWidth == 0 || this.initHeight == 0) return;

            double width = this.palMain.Width;
            double height = this.palMain.Height;
            double iRatioWith = width / this.initWidth;
            double iRatioHeight = height / this.initHeight;

            foreach (Control ctl in this.palMain.Controls)
            {
                // 【防呆 2】檢查字典裡確實有記錄這個控制項，才進行縮放。
                // 這可以避免未來動態新增控制項、或是遇到沒有 Name 的控制項時當機。
                if (initControl.ContainsKey(ctl.Name))
                {
                    ctl.Left = (int)(initControl[ctl.Name].Left * iRatioWith);
                    ctl.Top = (int)(initControl[ctl.Name].Top * iRatioHeight);
                    ctl.Width = (int)(initControl[ctl.Name].Width * iRatioWith);
                    ctl.Height = (int)(initControl[ctl.Name].Height * iRatioHeight);
                }
            }
        }

        private void frmBeepPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("確定要關閉應用程式嗎？", "關閉確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // 取消關閉
            }
        }
    }
}
