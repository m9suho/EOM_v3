using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_M
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 1차 개선 중복 실행 방지
            Process[] procs = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            DefaultClass dc = new DefaultClass();

            try
            {
                if (procs.Length <= 1)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else
                {
                    dc.Msg("경고", "이미 실행중 입니다.");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                dc.LogFileSave(ex.Message);
            }
        }
    }
}
