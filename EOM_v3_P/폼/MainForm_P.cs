using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace EOM_v3_P
{
    public partial class MainForm : Form
    {
        int count = 0;
        string strConn, strSetQuery = string.Empty;
        string programName = "EOM v3 자동 인쇄 (멀티 통합)";
        string excelFilePathName = Application.StartupPath + @"\PrintForm.xlsx";
        string[] settingData, addressData;

        const string PROCESS_NAME = "EXCEL.EXE";
        const string DB_NAME_RELEASE = "eom_1floor";
        const string DB_NAME_DEBUG = "eom_1floor_trunk";

        public static string DATABASE_NAME = string.Empty;

        DefaultClass dc = new DefaultClass();
        MariaDBClass mariaDB = new MariaDBClass();

        public string SplitConvert(string[] _data)
        {
            string returnData = string.Empty;

            for (int i = 0; i < _data.Length; i++)
            {
                // 마지막 값이라면
                if (i == _data.Length - 1)
                    returnData += _data[i];
                else
                    returnData += _data[i] + "/";
            }

            return returnData;
        }

        public void InsertLogDB(string _data)
        {
            string[] insertData = new string[]
            {
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                _data,
                //registrantData
                settingData[4]
            };
            string query = dc.InsertQueryArrayConvert(DATABASE_NAME, "print_data", insertData);
            mariaDB.EtcQuery(query);
        }

        private void PrintStart(string[] _data)
        {
            // 파일 서버 파일 다운로드
            //File.Copy(serverPath + @"\print.dat", excelFileName, true);
            //dc.FtpDownloadUsingWebClient("ftp://10.239.19.91:2121/EOM_v3/print.dat", excelFilePathName);

            Excel.Application myApplication = new Excel.Application();
            Excel.Workbook wb = myApplication.Workbooks.Open(excelFilePathName);
            Excel.Worksheet ws = myApplication.Worksheets.Item["EO 테그 양식"];

            string[] inputData = 
            {
                _data[9],                   // 샘플, 초도품
                _data[6],                   // 작성일
                string.Empty,               // 담당자
                string.Empty,               // 연락처
                _data[1],                   // 품번
                _data[2],                   // 차종
                _data[3],                   // 고객사 EO
                _data[4],                   // 모비스 EO
                _data[5],                   // 변경내역
            };
            string[] printFormData = dc.PrintFormConditionConvert(_data[0], inputData);

            // 2021.12.03
            // 품번 제외 (변경내역, 기존 형식, 차종만)
            printFormData[4] = dc.ProductNameExclusion(printFormData[6], printFormData[4], inputData[5]);

            // 인쇄 내용 삽입
            // 초도품 or 샘플
            ws.Cells[2, 2] = printFormData[0];
            // 작성일
            ws.Cells[4, 6] = printFormData[1];
            // 담당자
            ws.Cells[5, 6] = printFormData[2];
            // 연락처
            ws.Cells[6, 6] = printFormData[3];
            // 차종 (품번)
            ws.Cells[7, 3] = printFormData[4];
            // E/O NO.
            ws.Cells[8, 3] = printFormData[5];
            // 변경내역
            ws.Cells[9, 3] = printFormData[6];

            // 초도품
            // 2021.04.30
            // 글자 수에 따라 폰트 크기 지정
            if (_data[5].Length < 50)
            {
                for (int i = 9; i <= 27; i += 9)
                {
                    ws.Cells[i, 3].Font.Size = 11;
                    ws.Cells[i, 9].Font.Size = 11;
                }
            }
            else
            {
                for (int i = 9; i <= 27; i += 9)
                {
                    ws.Cells[i, 3].Font.Size = 9;
                    ws.Cells[i, 9].Font.Size = 9;
                }
            }

            // 샘플
            if (printFormData[0] == "샘 플")
            {
                // 2021.01.14
                // 이벤트 확대 인쇄
                if (dc.EventStepCheck(printFormData[6]))
                {
                    for (int i = 9; i <= 27; i += 9)
                    {
                        ws.Cells[i, 3].Font.Size = 36;
                        ws.Cells[i, 9].Font.Size = 36;
                    }
                }
            }
            
            // 인쇄
            if (addressData != null)
            {
                ws.PrintOut(1, 1, _data[10], null, addressData[3]);
                dc.LogFileSave("PRINT NAME DATA [" + addressData[3] + "]");
            }
            else
            {
                ws.PrintOut(1, 1, _data[10]);
                dc.LogFileSave("PRINT NAME DATA [NULL]");
            }

            // 내용 초기화
            ws.Cells[4, 6] = "";

            // 차종, EO 번호, 내용 초기화 
            for (int i = 0; i < 3; i++)
            {
                ws.Cells[i + 7, 3] = "";
            }

            string query = "DELETE FROM `" + DATABASE_NAME + "`.`print_data` WHERE model_name = '" + _data[1] + "' AND car_name = '" + _data[2] + "' AND customer_eo_number = '" + _data[3] + "' AND mobis_eo_number = '" + _data[4] + "' AND print_count = '" + _data[10] + "'";
            mariaDB.EtcQuery(query);

            // EXCEL 종료
            wb.Save();
            myApplication.Quit();

            // 날짜 데이터만 들어가도록 수정
            _data[6] = _data[6].Substring(0, 10);

            // EXCEL 강제 종료
            Process[] processes = Process.GetProcessesByName(PROCESS_NAME);

            if (processes.Length > 0)
            {
                processes[0].Kill();
                dc.LogFileSave("'" + PROCESS_NAME + "' 실행되어 있음으로 프로세스를 강제 종료");
            }

            InsertLogDB(SplitConvert(_data) + " 인쇄 완료");
        }

        private void PrintTableUpdate()
        {
            try
            {
                string[] selectData = new string[] { "print_address", settingData[4] };
                string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "print_data", selectData, "SELECT");

                strSetQuery = query;

                if (settingData[4] == "2F")
                {
                    query += " OR print_address = '2F_A53'";
                }

                string[,] modelData = mariaDB.SelectQuery2(query);
                string[] printData = new string[modelData.GetLength(1)];

                if (modelData.GetLength(0) > 0)
                {
                    // 첫번째만 임시 저장
                    for (int i = 0; i < modelData.GetLength(1); i++)
                    {
                        printData[i] = modelData[0, i];
                    }

                    timer1.Stop();
                    PrintStart(printData);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                dc.LogFileSave(ex.Message);

                if (!timer1.Enabled) timer1.Start();
            }
        }

        // 폼 종료 버튼 비활성화 선언
        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = programName + " - " + dc.Version();

            notifyIcon1.Text = Text;

#if DEBUG
            Text += " [디버깅 모드]";
            DATABASE_NAME = DB_NAME_DEBUG;
#else
            DATABASE_NAME = DB_NAME_RELEASE;
#endif

            // 셋팅 값 로드
            settingData = dc.DatFileLoad(@"C:\" + Process.GetCurrentProcess().ProcessName, @"settingData.dat");

            // 현재 셋팅 데이터 체크
            if (!dc.DatFileCheck(settingData, 7))
            {
                settingData = new string[7] { DATABASE_NAME, "print_data", settingData[4], "log_data", "1F", "10.239.19.91", "3308" };

                dc.DatFileSave(@"C:\" + Process.GetCurrentProcess().ProcessName, "settingData.dat", settingData);
            }

            // mariaDB 셋팅
            strConn = "Server=" + settingData[settingData.Length - 2] + ";Port=" + settingData[settingData.Length - 1] + ";Database=" + DATABASE_NAME + ";Uid=root;Pwd=9001271;";

            mariaDB.DbConnInfo = strConn;

            timer1.Interval = 5000;
            timer1.Start();

            // 최소화
            WindowState = FormWindowState.Minimized;

            // 2020.09.15
            // 시작 프로그램 등록
            dc.StartUpRegistry(true);

            dc.AutoUpdate(업데이트UToolStripMenuItem, new TimeSpan[] { new TimeSpan(0, 0, 15), new TimeSpan(7, 0, 0) });

#if DEBUG
            mariaDB.InsertLogDB(DATABASE_NAME, "프로그램 시작 [" + dc.Version() + "]", false);
#else
            mariaDB.InsertLogDB(DATABASE_NAME, "프로그램 시작 [" + dc.Version() + "]", false);
#endif
            addressData = dc.LineMatchingData();

            if (addressData != null)
            {
                dc.LogFileSave("ADDRESS DATA [" + addressData[0] + "/" + addressData[1] + "/" + addressData[2] + "/" + addressData[3] + "]");
            }
            else
            {
                dc.LogFileSave("라인 정보 없음");
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Visible = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
#if DEBUG
            mariaDB.InsertLogDB(DATABASE_NAME, "프로그램 종료", false);
#else
            mariaDB.InsertLogDB(DATABASE_NAME, "프로그램 종료", false);
#endif
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // 프린트 데이터 쿼리 날림
                PrintTableUpdate();
            }
            catch(Exception ex)
            {
                mariaDB.InsertLogDB(ex.Message, false);
            }
        }

        private void 업데이트UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guna2UpdateFormClass updateForm = new Guna2UpdateFormClass();

            timer1.Stop();

            mariaDB.InsertLogDB("업데이트 시도", settingData, false);

            updateForm.FormTitleName = Text;
            updateForm.DefaultPath = "ftp://10.239.19.91:2121/EOM_v3";
            updateForm.ToolsFolderPath = "ftp://10.239.19.91:2121/TOOLS";
            updateForm.UpdateFolderName = "UPDATE_FILE";
            updateForm.ToolsFileName = "UpdateTools.exe";
            updateForm.PatchFileName = "PatchList_P.dat";
            updateForm.StartForm();
            timer1.Start();
        }

        private void 종료QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;

            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void 파일복구RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dc.FtpDownloadUsingWebClient("ftp://10.239.19.91:2121/EOM_v3/print_v2.dat", excelFilePathName);
            dc.FtpWebClientDownload("ftp://10.239.19.91:2121/EOM_v3/print_v2.dat", excelFilePathName);

            dc.Delay(3000);

            FileInfo fi = new FileInfo(excelFilePathName);

            if (fi.Length > 40000)
            {
                dc.Msg("알림", "정상적으로 복구되었습니다");
            }
            else
            {
                dc.Msg("오류", "복구에 실패하였습니다");
            }
        }

        private void 정보IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordFormClass passwordForm = new PasswordFormClass();
            passwordForm.FormTitleName = Text;
            passwordForm.StartForm();
            
            if (!passwordForm.ReturnValue)
            {
                passwordForm.CloseForm();
                return;
            }

            string s = string.Empty;

            for(int i = 0; i < settingData.Length; i++)
            {
                s += settingData[i] + "\n";
            }

            s += strConn + "\n";
            s += strSetQuery;

#if DEBUG
            Clipboard.SetText(strSetQuery);
#endif

            dc.Msg("오류", s);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (count % 4 == 0)
            {
                count = 0;
                label1.Text = "Queue 찾는 중";
            }
            else
            {
                label1.Text += ".";
            }

            count++;
        }
    }
}
