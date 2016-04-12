using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace WJ.Infrastructure.Util
{
    public class WinformUtil
    {
        WindowState windowstate = new WindowState();
        /// <summary>
        /// 屏幕高度
        /// </summary>
        private int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        /// <summary>
        /// 任务栏高度
        /// </summary>
        private int renwulanHeight = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;
        /// <summary>
        /// 工作区高度
        /// </summary>
        private int workHeight = Screen.PrimaryScreen.WorkingArea.Height;
        /// <summary>
        /// 工作区宽度
        /// </summary>
        private int workWidth = Screen.PrimaryScreen.WorkingArea.Width;

        #region 窗体高度减去任务栏高度
        /// <summary>
        /// 窗体高度减去任务栏高度
        /// </summary>
        public void SetWorkHeight(Control control)
        {
            lessenHeight(control);
        }
        private void lessenHeight(Control control)
        {
            control.Height = control.Height - renwulanHeight;
        }

        #endregion

        #region 关闭提示确认
        public static void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult re = System.Windows.Forms.MessageBox.Show("确定要退出吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (re == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 最小化到托盘，右键菜单
        public static void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Form form = sender as Form;
        }

        /// <summary>
        /// 还原窗口
        /// </summary>
        public void MinimizedToNormal(Form form)
        {
            if (form.Visible)
            {
                return;
            }

            if (windowstate == WindowState.Maximized)
            {
                MaximizedForm(form);
            }
            form.Visible = true;
            //SetMid(form);
        }

        /// <summary>
        /// 窗口到最小到托盘
        /// </summary>
        public void NormalToMinimized(Form form)
        {
            form.Visible = false;
        }

        #endregion

        #region 窗体最大化
        public void MaxOrRestoreForm(Form form, int defaultwidth, int defaultheight)
        {

            if (windowstate == WindowState.Normal)
            {
                MaximizedForm(form);
            }
            else
            {
                int Height = defaultheight - renwulanHeight;
                form.Size = new Size(defaultwidth, Height);
                windowstate = WindowState.Normal;

            }
            SetMid(form);
        }
        /// <summary>
        /// 最大化窗体
        /// </summary>
        private void MaximizedForm(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            if (windowstate != WindowState.Maximized)
            {
                windowstate = WindowState.Maximized;
                form.Size = new Size(workWidth, workHeight);
            }

        }
        #endregion

        #region 窗体居中
        /// <summary>
        /// 窗体居中
        /// </summary>
        public void SetMid(Form form)
        {
            int x = Screen.GetBounds(form).Width / 2 - form.Width / 2;
            int y = Screen.GetBounds(form).Height / 2 - (form.Height + renwulanHeight) / 2;
            form.Location = new Point(x, y);
        }
        #endregion

        #region 获取硬盘序列号
        [DllImport("DiskID32.dll")]
        public static extern long DiskID32(ref byte DiskModel, ref byte DiskID);

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        public string GetDiskID()
        {

            byte[] DiskModel = new byte[31];
            byte[] DiskID = new byte[31];
            int i;
            string ID = "";

            if (DiskID32(ref DiskModel[0], ref DiskID[0]) != 1)
            {

                for (i = 0; i < 31; i++)
                {

                    if (Convert.ToChar(DiskID[i]) != Convert.ToChar(0))
                    {
                        ID = ID + Convert.ToChar(DiskID[i]);
                    }
                }
                ID = ID.Trim();
            }

            return ID;
        }
        #endregion

        #region 判断网络连接是否连接
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        /// <summary>
        /// 当前网络连接状态
        /// </summary>
        /// <returns>当为true时，网络正常;当为false时，网络断开</returns>
        public bool IsConnected()
        {
            int I = 0;
            bool state = InternetGetConnectedState(out I, 0);
            return state;
        }
        #endregion

        #region 判断与某服务器是否连接
        /// <summary>
        /// 测试与指定服务器是否正常连接
        /// </summary>
        /// <param name="strIp"></param>
        /// <returns></returns>
        public string CmdPing(string strIp)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string pingrst;
            p.Start();
            p.StandardInput.WriteLine("ping -n 1 " + strIp);
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            if (strRst.IndexOf("(0% loss)") != -1)
                pingrst = "连接";
            else if (strRst.IndexOf("Destination host unreachable.") != -1)
                pingrst = "无法到达目的主机";
            else if (strRst.IndexOf("Request timed out.") != -1)
                pingrst = "超时";
            else if (strRst.IndexOf("Unknown host") != -1)
                pingrst = "无法解析主机";
            else
                pingrst = strRst;
            p.Close();
            return pingrst;
        }

        #endregion


        public void txt_onlyint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || ((int)e.KeyChar) == 8)//8为Backspac所对应的ascii
            {
                //表示：该事件尚未处理，事件会继续处理
                e.Handled = false;
            }
            else
            {
                //表示：该事件已经处理过了，事件就不会在响应去处理了
                e.Handled = true;
            }
        }

        public void txt_onlyfloat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || ((int)e.KeyChar) == 8 || ((int)e.KeyChar) == 46)//8为Backspac所对应的ascii
            {
                if (((int)e.KeyChar) == 46 && ((TextBox)sender).Text.Contains("."))
                {
                    e.Handled = true;
                    return;
                }
                //表示：该事件尚未处理，事件会继续处理
                e.Handled = false;
            }
            else
            {
                //表示：该事件已经处理过了，事件就不会在响应去处理了
                e.Handled = true;
            }
        }
    }
}
