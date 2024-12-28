using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_M
{
    public partial class TestForm : Form
    {
        bool process = false;
        double fileSize = 0;

        const string FTP_SERVER_ID = "user";
        const string FTP_SERVER_PWD = "autonics12#";

        public TestForm()
        {
            InitializeComponent();
        }

        private void WebClientDownloadProgress(string _server, string _local)
        {
            // 서버 파일 사이즈 구하기
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_server);
            request.Credentials = new NetworkCredential(FTP_SERVER_ID, FTP_SERVER_PWD);
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Timeout = 1000;

            if (checkBox1.Checked)
            {
                listBox1.Items.Add("오류 수정 O");

                request.KeepAlive = false;
                request.UsePassive = true;
                request.UseBinary = false;
            }
            else
            {
                listBox1.Items.Add("오류 수정 X");
            }

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            fileSize = response.ContentLength;

            // 다운로드 시작
            WebClientPlus wc = new WebClientPlus();
            wc.Credentials = new NetworkCredential(FTP_SERVER_ID, FTP_SERVER_PWD);
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
            wc.DownloadFileAsync(new Uri(_server), _local);

            process = true;

            MainForm.dc.LogFileSave("1. 다운로드 시작");

            while (process)
            {
                System.Windows.Forms.Application.DoEvents();
            }

            MainForm.dc.LogFileSave("3. 다운로드 종료");

            wc.Dispose();
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int value = Convert.ToInt32((Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(fileSize)) * 100);

            //0/0Byte - 0%
            //if (value % 10 == 0)
            {
                progressBar1.Value = value;
            }

            label.Text = string.Format("{0}/{1} Byte - {2}%", e.BytesReceived, fileSize, value);

            MainForm.dc.LogFileSave("2. 다운로드 중 [" + e.BytesReceived + "/" + fileSize + "]");
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MainForm.dc.ListBoxLogFileSave("다운로드가 완료 되었습니다. [1]", listBox1, false);
            //MainForm.dc.Delay(1000);
            process = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    DateTime remoteTime = MainForm.dc.GetDateTimeFTPServer(@"ftp://10.239.19.91:2121/EOM_v3/UPDATE_FILE/" + Application.ProductName + ".exe");

                    listBox1.Items.Add(remoteTime);
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebClientDownloadProgress(@"ftp://10.239.19.91:2121/EOM_v3/UPDATE_FILE/" + Application.ProductName + ".exe", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + Application.ProductName + ".exe");

            MainForm.dc.ListBoxLogFileSave("다운로드가 완료 되었습니다. [2]", listBox1, false);
        }
    }
}
