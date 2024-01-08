using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace EOM_v3_M
{
    public partial class MainForm : Form
    {
        const int FORM_WIDTH_SIZE = 1280;
        const int FORM_HEIGHT_SIZE = 700;
        const string PACKING_REGISTER_CONTENTS = "---------- 첫 투입 품번 등록 ----------";
        const string GUEST_LOGIN_MSG = "GUEST님 로그인";

        string strConn = string.Empty;
        string[] arr1ColumnData = { "False", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };

        public static bool complete = false;
        public static bool historyInsertComplete = false;
        public static bool adminMode = false;

        public static int login = 0;
        public static int permission = 0;

        public static string searchType = string.Empty;
        public static string eoType = string.Empty;
        public static string eoCarData = string.Empty; 
        //public static string eoViewData = string.Empty;
        //public static string subjectData = string.Empty;
        public static string userNameData = string.Empty;
        public static string programName = "Engineer Order Manager v3.1 (멀티 통합)";
        public static string settingFileName = @"settingData";
        public static string updateFileName = @"updateData";
        public static string lineSelectFileName = @"lineSelectData";
        public static string floorFileName = @"floorData";
        public static string excelFileName = @"formPrint";
        public static string extensionName = @".dat";
        public static string extensionExcelName = @".xlsx";
        public static string mhtData = string.Empty;
        public static string modelSelectData = string.Empty;
        public static string carSelectData = string.Empty;

        /// <summary>
        /// settingData
        /// [0] : eom_1floor
        /// [1] : model_data
        public static string[] settingData;
        public static string[] updateData, lineSelectData;
        public static string[] eoSelectData = new string[9];
        public static string[] printData = new string[13];
        public static string[] floorData = new string[] { "" };
        public static readonly string[] LINE_NAME_LIST = new string[] { "오디오 플랫폼", "D-오디오 수삽", "D-오디오 조립", "D-오디오 SUB", "클러스터", "HUD" };
        //public static string[] shipmentData = { "OEM", "CKD", "KD" };
        public static string[] searchData = new string[] { "", "" };

        public static DefaultClass dc = new DefaultClass();
        public static PackingAndEOM pe;
        public static MariaDBClass mariaDB = new MariaDBClass();
        public static ComboBox cbbProductName01, cbbLineName01, cbbMetroFloor01;
        public static DataGridView datagridview01, datagridview02;

        private void initialSetDataGridView()
        {
            dc.DataGridViewDoubleBuffered(dgvSelect, true);
            dc.DataGridViewDoubleBuffered(dgvGuest, true);

            // dataGridView 자동 조절
            dgvSelect.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvGuest.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // dataGridView 줄바꿈
            dgvSelect.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvGuest.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // dataGridView 정렬
            ModelSort();

            // 처음 위치 재설정
            dgvSelect.Location = new Point(14, 193);
            dgvGuest.Location = new Point(14, 193);

            //dgvGuest.Visible = false;

            // 2022.07.01
            // 그리드 뷰 사이즈 조정 
            SetDataGridViewCellsWidthSize(dgvSelect);
            SetDataGridViewCellsWidthSize(dgvGuest);
        }

        private void SetDataGridViewCellsWidthSize(DataGridView _dataGridView)
        {
            int[] setSize =
            {
                30,     // CHECK BOX
                140,    // 품번
                100,    // 차종
                90,     // 고객사 EO
                90,     // 모비스 EO

                320,    // EO 내용
                50,     // 스티커 색상
                130,    // 적용일
                130,    // 종료일
                100,    // 등록자

                70,     // 타입
                60,     // 출하지
                60,     // 근거자료
                100,    // MAIN PCB
                100,    // SUB PCB

                100,    // EO 구분
                100,    // 적용 오더
            };

            for (int i = 0; i < _dataGridView.ColumnCount; i++)
            {
                _dataGridView.Columns[i].Width = setSize[i];
            }
        }

        private void InitialControlSize()
        {
            lblUserName.Left = Width - 391;
            // 2022.11.22
            // 출하지 폼 크기 위치 이동 수정
            txtMenuAllSearch.Left = Width - txtMenuAllSearch.Width - 109;
            pnShipmentView.Left = Width - pnShipmentView.Width - 593;
            btnShipmentMaster.Left = Width - btnShipmentMaster.Width - 477;
            btnShipmentChange.Left = Width - btnShipmentChange.Width - 361;
            btnProductSearch.Left = Width - btnProductSearch.Width - 245;
            btnProductAdd.Left = Width - btnProductAdd.Width - 129;
            btnProductDelete.Left = Width - btnProductDelete.Width - 13;

            dgvGuest.Size = new Size(Width - 27, Height - 206);
            dgvSelect.Size = new Size(Width - 27, Height - 206);
        }

        public static void Guna2Msg(string _type, string _text)
        {
            Guna2MessageBox guna2MessageBox = new Guna2MessageBox(_type, _text);
            guna2MessageBox.ShowDialog();
        }

        public static void Guna2Msg(string _type, string _text, bool _data)
        {
            if (_data) dc.LogFileSave(_text);

            Guna2MessageBox guna2MessageBox = new Guna2MessageBox(_type, _text);
            guna2MessageBox.ShowDialog();
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            try
            {
                //Text = programName + " - " + dc.Version();
                lblFormTitle.Text = programName + " - " + dc.Version();
                Text = lblFormTitle.Text;

                // 해상도에 따라 윈폼 크기가 변경되지 않도록 수정
                //AutoScaleMode = AutoScaleMode.None;

                // 컨트롤 디자인 동적 설정
                cbbProductName01 = cbbProductName;
                cbbLineName01 = cbbLineName;
                cbbMetroFloor01 = cbbMetroFloor;
                datagridview01 = dgvSelect;
                datagridview02 = dgvGuest;

                // 셋팅 값 로드
                settingData = dc.DatFileLoad(@"settingData.dat");

                // 현재 셋팅 데이터 체크
                if (!dc.DatFileCheck(settingData, 9))
                {
#if DEBUG
                    settingData = new string[] { "eom_1floor", "model_data", "eo_data", "print_data", "registrant_data", "log_data", "update_data", "127.0.0.1", "3306" };
#else
                    settingData = new string[] { "eom_1floor", "model_data", "eo_data", "print_data", "registrant_data", "log_data", "update_data", "10.239.19.91", "3306" };
#endif
                    dc.DatFileSave(settingFileName + extensionName, settingData);
                }

#if DEBUG
                Text += " [디버깅 모드]";

                settingData[0] = "eom_1floor_trunk";
                settingData[7] = "127.0.0.1";
#else
                guna2ProgressBar1.Visible = false;
#endif

                // 2020.09.04
                // MariaDB 셋팅 먼저 검사
                strConn = "Server=" + settingData[settingData.Length - 2] + ";Port=" + settingData[settingData.Length - 1] + ";Database=" + settingData[0] + ";Uid=user;Pwd=autonics12#;";
                mariaDB.DbConnInfo = strConn;

                pe = new PackingAndEOM(strConn);

                // DB 서버 죽으면 점검 메세지
                if (!mariaDB.IsConnected())
                {
                    Guna2Msg("오류", "서버 점검중 입니다");
                    Application.ExitThread();
                    return;
                }

                // 라인 값 로드
                lineSelectData = dc.DatFileLoad(@"lineSelectData.dat");

                // 현재 라인 데이터 체크
                if (!dc.DatFileCheck(lineSelectData, 1))
                {
                    lineSelectData = new string[] { "0" };

                    dc.DatFileSave(lineSelectFileName + extensionName, lineSelectData);
                }

                cbbLineName.Items.Clear();

                // 라인 설정
                // 2021.02.02
                // 오디오 플랫폼 제외
                for (int i = 1; i < LINE_NAME_LIST.Length; i++)
                {
                    cbbLineName.Items.Add(LINE_NAME_LIST[i]);
                }

                // DataGridView 초기 셋팅
                initialSetDataGridView();

                // 디버깅
                debugLabel();

                /*
                // 층 설정
                floorData = dc.DatFileLoad(@"floorData.dat");

                if (!dc.DatFileCheck(floorData, 1))
                {
                    floorData = new string[] { "1F" };
                }
                else
                {
                    cbbMetroFloor01.SelectedItem = floorData[0];
                }
                */

                // 사용자 확인
                string userData = NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();

                // 2021.02.16
                // Floor 정보는 여기서 채워짐 (1F, 2F) ++++++++ 추가
                SetFloorData(userData);

                // 2021.08.27
                // 조별 선임 체크 선언
                userNameData = SearchRegistrant(userData);

                // 사용자 확인되면 버튼 활성화
                // Floor 정보는 여기서 채워짐 (1F, 2F) -------- 삭제
                if (!userNameData.Equals(userData))
                {
                    lblUserName.Text = userNameData + "님(" + floorData[0] + ") 로그인";

                    btnProductAdd.Enabled = true;
                    //btnProductDelete.Enabled = true;
                    cbbMetroFloor.Visible = false;
                }

                // 2020.03.04
                // 라인 지정
                cbbLineName01.SelectedIndex = Convert.ToInt32(lineSelectData[0]);

                // 게스트 모드라면
                //if (!btnProductDelete.Enabled)
                // 2022.11.22
                // 게스트 모드 조건 변경
                if (lblUserName.Text == GUEST_LOGIN_MSG)
                {
                    // 층 설정
                    floorData = dc.DatFileLoad(@"floorData.dat");

                    if (!dc.DatFileCheck(floorData, 1))
                    {
                        floorData = new string[] { "1F" };
                    }
                    else
                    {
                        cbbMetroFloor01.SelectedItem = floorData[0];
                    }

                    //cbbMetroFloor.Visible = true;

                    // 2020.09.01
                    // 콤보 박스 활성화
                    //VisibleFloor();

                    // 2F
                    cbbMetroFloor01.SelectedItem = floorData[0];

                    GuestDataGridViewUpdate();

                    // 자동 갱신
                    timer1.Interval = 60000;
                    timer1.Start();
                }

#if DEBUG
#else
                // 업데이트 파일 로드
                updateData = dc.DatFileLoad(@"updateData.dat");

                // 업데이트 내역 체크
                if (!dc.DatFileCheck(updateData, 1))
                {
                    updateData = new string[] { "0" };

                    dc.DatFileSave(updateFileName + extensionName, updateData);
                }

                if (!dc.Version().Equals(updateData[0]))
                {
                    UpdateListViewFormClass updateListViewFormClass = new UpdateListViewFormClass();

                    updateListViewFormClass.FormTitleName = programName + " - 업데이트 내역";
                    updateListViewFormClass.DB = settingData[0];
                    updateListViewFormClass.Table = settingData[6];
                    updateListViewFormClass.StrConn = strConn;
                    updateListViewFormClass.BoundsLocation = 0;
                    updateListViewFormClass.StartForm();

                    // 값 변경
                    updateData[0] = dc.Version();

                    // updateData.dat 파일 저장
                    dc.DatFileSave(updateFileName + extensionName, updateData);
                }
#endif

                // 디버깅
                debugLabel();

                /*
                // 2020.09.12
                // 업데이트 내용 체크 ----- 삭제
                if (dc.CheckServerMainFile("ftp://10.239.19.91:2121/EOM_v3/"))
                {
                    //label11.Visible = true;
                    label11.Text = "신규 버전이 등록되어 있습니다";
                    label11.ForeColor = Color.Red;
                    label11.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

                    timer2.Interval = 1500;
                    timer2.Start();
                }
                */

                timer3.Interval = 3000;
                timer3.Start();

                // 접속 로그
                mariaDB.InsertLogDB("프로그램 시작 [" + dc.Version() + "], " + sw.ElapsedMilliseconds + "ms", false);

#if DEBUG
#else
                // 2021.02.23
                // 실행 시 자동 업데이트
                if (dc.CheckServerMainFile("ftp://10.239.19.91:2121/EOM_v3/"))
                {
                    timer2.Interval = 1000;
                    timer2.Start();
                }
#endif
                Width = FORM_WIDTH_SIZE;
                Height = FORM_HEIGHT_SIZE;

                // 2023.03.24
                // Guna UI 강제 위치 적용
                Location = dc.CenterLocation(Width, Height);
            }
            catch (Exception ex)
            {
                mariaDB.InsertLogDB(ex.Message, false);
                mariaDB.InsertLogDB(ex.StackTrace, false);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 접속 로그
            mariaDB.InsertLogDB("프로그램 종료", false);

            // 게스트 모드라면
            //if (!btnProductDelete.Enabled)
            // 2022.11.22
            // 게스트 모드 조건 변경
            if (lblUserName.Text == GUEST_LOGIN_MSG)
            {
                // 2020.09.03
                // 비활성화 경우가 발생하면 값을 불러오지 못함

                // 층 설정 파일 저장
                dc.DatFileSave(floorFileName + extensionName, floorData);
            }

            // 라인 설정 파일 저장
            dc.DatFileSave(lineSelectFileName + extensionName, lineSelectData);
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (OwnedForms.Length > 0)
            {
                OwnedForms[0].TopMost = true;
                OwnedForms[0].TopMost = false;
            }

            Width = FORM_WIDTH_SIZE;
            Height = FORM_HEIGHT_SIZE;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            InitialControlSize();
        }

        public static string SplitConvert(string[] _data)
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

        private void debugLabel()
        {
            bool blDebug = false;

            if (blDebug)
            {
                string test = string.Empty;

                if (cbbMetroFloor01.SelectedItem != null)
                    test = cbbMetroFloor01.SelectedItem.ToString();

                //label11.Text = "floorData: " + floorData[0] + " / Select: " + test + " / lineSelectData: " + lineSelectData[0];
            }
            else
            {
                //label11.Visible = false;
            }
        }

        private void GuestModeView()
        {
            dgvGuest.Visible = true;

            GuestDataGridViewUpdate();
        }

        private string RegistrantFilterAdd(string _data)
        {
            string[] selectData = new string[] { "print_address", _data };
            string query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[4], selectData, "SELECT");
            string[] tmpData = mariaDB.SelectQueryCount(query, "name");

            string returnData = string.Empty;

            if (tmpData.Length > 0)
            {
                returnData = " AND (";

                for (int i = 0; i < tmpData.GetLength(0); i++)
                {
                    returnData += (i == 0) ? "registrant = '" + tmpData[i] + "'" : " OR registrant = '" + tmpData[i] + "'";
                }

                returnData += ")";
            }

            //Clipboard.SetText(returnData);

            return returnData;
        }

        private void GuestDataGridViewUpdate()
        {
            string query = string.Empty;

            Stopwatch sw = new Stopwatch();

            sw.Start();

            cbbProductName.SelectedIndex = -1;

            // 콤보 박스 리스트 갱신
            ModelListUpdate();

            // 게스트 모드일 때만
            //if (!btnProductDelete.Enabled)
            // 2022.11.22
            // 게스트 모드 조건 변경
            if (lblUserName.Text == GUEST_LOGIN_MSG)
            {
                // 2020.09.03
                // 층에 따른 결과 값
                // 1F
                if (cbbLineName01.Text == "오디오 플랫폼" || cbbLineName01.Text == "D-오디오 SUB")
                {
                    floorData[0] = "1F";
                }
                // 2F
                else if (cbbLineName01.Text == "D-오디오 수삽" || cbbLineName01.Text == "클러스터" || cbbLineName01.Text == "HUD")
                {
                    floorData[0] = "2F";
                }
                // 1F or 2F
                else
                {
                    // 2020.09.08
                    // 예외 처리
                    if (cbbMetroFloor01.SelectedItem == null)
                    {
                        cbbMetroFloor01.SelectedIndex = 0;
                    }

                    floorData[0] = cbbMetroFloor01.SelectedItem.ToString();
                }

                // dataGridView 초기화
                dgvGuest.Rows.Clear();

                // Update Time
                lblDBUpTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", " + sw.ElapsedMilliseconds.ToString() + "ms";
            }

            query = "SELECT * FROM `" + settingData[0] + "`.`" + settingData[1] + "` WHERE (line = '" + cbbLineName01.Text + "' AND end_date > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')" + RegistrantFilterAdd(floorData[0]);
            query += " ORDER BY start_date";
            // 2021.03.24
            // 첫 투입 내용은 제일 상단에 올리도록
            //query += " ORDER BY FIELD(eo_contents, '" + PACKING_REGISTER_CONTENTS + "') DESC, start_date ASC";

            //Clipboard.SetText(query);

            string[,] modelData = mariaDB.SelectQuery2(query);

            DGVRowsDataAdd(dgvGuest, modelData, 1);

            ModelSort();

            // Update Time
            lblDBUpTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", " + sw.ElapsedMilliseconds.ToString() + "ms";
        }

        public void DataGridViewUpdate()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            // 콤보 박스 리스트 갱신
            ModelListUpdate();

            // dataGridView 초기화
            dgvSelect.Rows.Clear();

            // Update Time
            lblDBUpTime.Text = DateTime.Now.ToString() + ", " + sw.ElapsedMilliseconds.ToString() + "ms";
        }

        public void ModelListUpdate()
        {
            Stopwatch sw = new Stopwatch();

            sw.Reset();
            sw.Start();

            string[] selectData = new string[] { "line", cbbLineName01.Text };
            //string query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
            string query = "SELECT model_name FROM `" + settingData[0] + "`.`" + settingData[1] + "` WHERE line = '" + cbbLineName01.Text + "' GROUP BY model_name";
            //string[] modelData = mariaDB.SelectQueryCount(query, "model_name");
            string[,] modelData = mariaDB.SelectQuery4(query, 1);

            // 모델 데이터 로드
            cbbProductName.Items.Clear();

            /*
            for (int i = 0; i < modelData.Length; i++)
            {
                // 중복 제거
                if (!cbbProductName.Items.Contains(modelData[i]) && !modelData[i].Equals(string.Empty))
                {
                    cbbProductName.Items.Add(modelData[i]);
                }
            }
            */

            for (int i = 0; i < modelData.GetLength(0); i++)
            {
                cbbProductName.Items.Add(modelData[i, 0]);
            }
        }

        public static bool IsSpaceCheck(string _data)
        {
            if (!_data.Equals(string.Empty))
            {
                for (int i = 0; i < _data.Length; i++)
                {
                    if (_data.Substring(i, 1).Equals(" "))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string BackSlashConvert(string _data)
        {
            string convertData = string.Empty;

            for (int i = 0; i < _data.Length; i++)
            {
                if (_data.Substring(i, 1).Equals("'"))
                {
                    convertData += "\\'";
                }
                else
                {
                    convertData += _data.Substring(i, 1);
                }
            }

            return convertData;
        }

        private void SetFloorData(string _data)
        {
            string[] selectData = new string[] { };
            string query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[4], selectData, "SELECT");
            string[,] resultData = mariaDB.SelectQuery2(query);

            for (int i = 0; i < resultData.GetLength(0); i++)
            {
                if (resultData[i, 0] == _data)
                {
                    floorData[0] = resultData[i, 2];
                }
            }
        }

        public static string SearchRegistrant(string _data)
        {
            string[] selectData = new string[] { };
            string query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[4], selectData, "SELECT");
            string[,] tmpData = mariaDB.SelectQuery2(query);

            for (int i = 0; i < tmpData.GetLength(0); i++)
            {
                // 있을 경우
                if (tmpData[i, 0].Equals(_data))
                {
                    // 층 설정
                    //floorData[0] = tmpData[i, 2];
                    return tmpData[i, 1];
                }
            }

            return _data;
        }

        private void DGVRowsDataAdd(DataGridView _dgv, string[,] _data, int _type)
        {
            // dataGridView 초기화
            _dgv.Rows.Clear();

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                // 등록자 불러오기
                string registrantData = SearchRegistrant(_data[i, 10]);
                //string printType = string.Empty;
                string printType = _data[i, 11];

                /*
                // 타입
                // D-오디오 수삽
                printType = cbbLineName.SelectedItem.Equals(LINE_NAME_LIST[1]) ? "사내용" : _data[i, 11];
                */

                string shipment = string.Empty;

                // 출하지
                // D-오디오 수삽, D-오디오 SUB
                if (cbbLineName.SelectedItem.Equals(LINE_NAME_LIST[1]) || cbbLineName.SelectedItem.Equals(LINE_NAME_LIST[3]))
                {
                    shipment = "-";
                }
                else
                {
                    shipment = _data[i, 12];
                }

                // *.mht
                string eoMhtData = string.Empty;

                if (!_data[i, 13].Equals(string.Empty))
                    eoMhtData = "보기";

                // 방문자
                if (_type == 1)
                {
                    dgvGuest.Visible = true;

                    _dgv.Rows.Add(
                        false,
                        _data[i, 1],                                                            // 품번
                        _data[i, 2],                                                            // 차종
                        _data[i, 3],                                                            // 고객사 EO
                        _data[i, 4],                                                            // 모비스 EO
                        _data[i, 5],                                                            // EO 내용
                        _data[i, 7],                                                            // 스티커 내용
                        Convert.ToDateTime(_data[i, 8]).ToString("yyyy-MM-dd HH:mm:ss"),        // 적용일
                        Convert.ToDateTime(_data[i, 9]).ToString("yyyy-MM-dd HH:mm:ss"),        // 종료일
                        string.Empty,                                                           // -등록자
                        printType,                                                              // 타입
                        shipment,                                                               // 출하지
                        eoMhtData,                                                              // mht
                        _data[i, 14],                                                           // MAIN PCB
                        _data[i, 15],                                                           // SUB PCB
                        _data[i, 16],                                                           // EO 구분
                        _data[i, 17]                                                            // 적용 오더
                    );
                }
                // 사용자
                else if (_type == 2)
                {
                    dgvSelect.Visible = true;

                    _dgv.Rows.Add(
                        false,
                        _data[i, 1],                                                            // -품번
                        _data[i, 2],                                                            // 차종
                        _data[i, 3],                                                            // 고객사 EO
                        _data[i, 4],                                                            // 모비스 EO
                        _data[i, 5],                                                            // EO 내용
                        _data[i, 7],                                                            // 스티커 내용
                        Convert.ToDateTime(_data[i, 8]).ToString("yyyy-MM-dd HH:mm:ss"),        // 적용일
                        Convert.ToDateTime(_data[i, 9]).ToString("yyyy-MM-dd HH:mm:ss"),        // 종료일
                        registrantData,                                                         // 등록자
                        printType,                                                              // 타입
                        shipment,                                                               // 출하지
                        eoMhtData,                                                              // mht
                        _data[i, 14],                                                           // MAIN PCB
                        _data[i, 15],                                                           // SUB PCB
                        _data[i, 16],                                                           // EO 구분
                        _data[i, 17]                                                            // 적용 오더
                    );
                }

                switch (_data[i, 6])
                {
                    case "Orange":
                        _dgv.Rows[i].Cells[6].Style.BackColor = Color.Orange;
                        break;
                    case "Blue":
                        _dgv.Rows[i].Cells[6].Style.BackColor = Color.Blue;
                        break;
                    case "LimeGreen":
                        _dgv.Rows[i].Cells[6].Style.BackColor = Color.LimeGreen;
                        break;
                    case "White":
                        _dgv.Rows[i].Cells[6].Style.BackColor = Color.White;
                        break;
                    case "Yellow":
                        _dgv.Rows[i].Cells[6].Style.BackColor = Color.Yellow;
                        break;
                    default:
                        _dgv.Rows[i].Cells[6].Style.BackColor = SystemColors.ScrollBar;
                        _dgv.Rows[i].Cells[6].Value = "없음";
                        break;
                }
            }

            ModelSort();
        }

        public void ModelSelect(string _data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // 모델 데이터 로드
            string[] selectData = new string[] { "line", cbbLineName01.Text, "model_name", _data };
            string query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");

            // 2021.03.24
            // 첫 투입 내용은 제일 상단에 올리도록
            query += " ORDER BY FIELD(eo_contents, '" + PACKING_REGISTER_CONTENTS + "') DESC, start_date ASC";

            string[,] modelData = mariaDB.SelectQuery2(query);

            DGVRowsDataAdd(dgvSelect, modelData, 2);

            // 2022.06.30
            // 출하지 텍스트 출력
            if (pnShipmentView.Visible)
            {
                string shipmentData = pe.NowShipment(settingData[0], cbbLineName.Text, cbbProductName.Text);

                lblShipmentView.Text = shipmentData;
            }

            // 2023.05.17
            // 초물리스트 등록 여부
            selectData = new string[] { "line", cbbLineName01.Text, "model_name", _data };
            query = dc.SelectDeleteQueryANDConvert("fpi", settingData[1], selectData, "SELECT");

            string[] modelCount = mariaDB.SelectQueryCount(query, "model_name");

            if (modelCount.Length > 0)
            {
                lblFpiCheck.ForeColor = Color.FromArgb(0, 192, 0);
                lblFpiCheck.Text = "등록 완료";
            }
            else
            {
                lblFpiCheck.ForeColor = Color.FromArgb(192, 0, 0);
                lblFpiCheck.Text = "미등록";
            }

            dc.LogFileSave(sw.ElapsedMilliseconds + "ms");
        }


        public void ModelSelect(string _data, int _type)
        {
            // dataGridView 초기화
            datagridview02.Rows.Clear();

            // 쿼리 변수 선언
            string query = string.Empty;

            // 차종 조회
            if (_type == 1)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "car_name", _data };
                query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
            }
            // 고객사 EO 조회
            else if (_type == 2)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "customer_eo_number", _data };
                query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
            }
            // 모비스 EO 조회
            else if (_type == 3)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "mobis_eo_number", _data };
                query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
            }

            // EO 내용 조회
            else if (_type == 4)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "eo_contents", _data };
                query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
            }
            // MAIN PCB 조회
            else if (_type == 5)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "main_pcb_name", _data };
                query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
            }
            // SUB PCB 조회
            else if (_type == 6)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "sub_pcb_name", _data };
                query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
            }

            else
            {
                Guna2Msg("오류", "치명적인 오류");
                return;
            }

            //Clipboard.SetText(query);

            string[,] resultData = mariaDB.SelectQuery2(query);

            DGVRowsDataAdd(dgvGuest, resultData, 1);
        }


        public static void SetDoNotSort(DataGridView dgv)
        {
            foreach (DataGridViewColumn i in dgv.Columns)
            {
                i.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ModelSort()
        {
            //dgvSelect.Sort(dgvSelect.Columns[6], ListSortDirection.Ascending);
            //dgvGuest.Sort(dgvGuest.Columns[6], ListSortDirection.Ascending);
            SetDoNotSort(dgvSelect);
            SetDoNotSort(dgvGuest);
            ShipmentBackColor(dgvSelect, 11);
            ShipmentBackColor(dgvGuest, 11);
        }

        static public void ShipmentBackColor(DataGridView _dataGridView, int _index)
        {
            for (int i = 0; i < _dataGridView.RowCount; i++)
            {
                if (_dataGridView.Rows[i].Cells[_index].Value.ToString().Equals("CKD"))
                {
                    _dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
                }
                else if (_dataGridView.Rows[i].Cells[_index].Value.ToString().Equals("KD"))
                {
                    _dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
                }
            }

            _dataGridView.ClearSelection();
        }

        static public void SeparateBackColor(DataGridView _dataGridView, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            //dc.LogFileSave(_dataGridView.Rows[e.RowIndex].Cells["Column3"].Value.ToString().Contains("내부관리").ToString());

            if (_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString().Contains("내부관리"))
            {
                _dataGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        public void ExportToExcel(DataGridView grid)
        {
            Excel.Application myApplication;
            Excel._Workbook myWorkbook;
            Excel._Worksheet mySheet;
            //Excel.Range oRng;

            MessageFormClass messageFormClass = new MessageFormClass();

            messageFormClass.FormMsg = "엑셀에 작성하고 있습니다";
            messageFormClass.ShowDelay = 100;
            messageFormClass.InstanceForm = this;
            messageFormClass.StartForm();

            try
            {
                //Start Excel and get Application object.
                myApplication = new Excel.Application();
                myApplication.Visible = false;

                //Get a new workbook.
                myWorkbook = (Excel._Workbook)(myApplication.Workbooks.Add(Missing.Value));
                mySheet = (Excel._Worksheet)myWorkbook.ActiveSheet;

                //Add table headers going cell by cell.
                int k = 0;
                string[] colHeader = new string[grid.ColumnCount - 1];
                for (int i = 0; i < grid.Columns.Count - 1; i++)
                {
                    mySheet.Cells[1, i + 1] = grid.Columns[i + 1].HeaderText;

                    k = i + 65;
                    colHeader[i] = Convert.ToString((char)k);
                }
                //Format A1:D1 as bold, vertical alignment = center.
                mySheet.get_Range("A1", colHeader[colHeader.Length - 1] + "1").Font.Bold = true;
                mySheet.get_Range("A1", colHeader[colHeader.Length - 1] + "1").Interior.Color = ColorTranslator.ToOle(Color.DarkGray);
                //mySheet.get_Range("A1", colHeader[colHeader.Length - 1] + "1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                mySheet.Cells.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Create an array to multiple values at once.
                object[,] saNames = new object[grid.RowCount, grid.ColumnCount];

                string tp;
                for (int i = 0; i < grid.RowCount; i++)
                {
                    for (int j = 1; j < grid.ColumnCount; j++)
                    {
                        tp = grid.Rows[i].Cells[j - 1].ValueType.Name;
                        if (tp == "String") //2000-01-01 형태의 날짜 필터하기 위함(숫자로 변환 방지)
                            saNames[i, j - 1] = "'" + grid.Rows[i].Cells[j].Value.ToString();
                        else
                            saNames[i, j - 1] = grid.Rows[i].Cells[j].Value;
                    }
                }

                //Fill A2:B6 with an array of values (First and Last Names).
                //mySheet.get_Range("A2", "B6").Value2 = saNames;
                mySheet.get_Range(colHeader[0] + "2", colHeader[colHeader.Length - 1] + (grid.RowCount + 1)).Value2 = saNames;

                mySheet.Columns.AutoFit();

                myApplication.Visible = true;
                myApplication.UserControl = true;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }

        private void PrintDataSetting(DataGridView _dataGridView)
        {
            try
            {
                // 2020.06.22
                // D-오디오 수삽 인쇄 안됨
                /*
                if (metroComboBox2.Text.Equals("D-오디오 수삽"))
                {
                    return;
                }
                */
                if (btnProductAdd.Enabled || adminMode)
                {
                    string[] columnData = new string[_dataGridView.ColumnCount];

                    for (int i = 0; i < columnData.Length; i++)
                    {
                        columnData[i] = _dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[i].Value.ToString();
                    }

                    printData[0] = cbbLineName01.Text;                                                                                      // line
                    printData[1] = _dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[1].Value.ToString();                                                // model_name
                    printData[2] = _dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[2].Value.ToString();                                                // car_name
                    printData[3] = _dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[3].Value.ToString();                                                // customer_eo_number
                    printData[4] = _dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[4].Value.ToString();                                                // mobis_eo_number
                    printData[5] = _dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[5].Value.ToString();                                                // eo_contents
                    printData[6] = Convert.ToDateTime(_dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[7].Value.ToString()).ToString("yyyy-MM-dd");     // reporting_date
                    printData[7] = string.Empty;                                                                                                                // manager_name
                    printData[8] = string.Empty;                                                                                                                // contact_number
                    printData[9] = _dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[10].Value.ToString();                                               // print_type
                    printData[10] = string.Empty;                                                                                                               // print_count
                    printData[11] = string.Empty;                                                                                                               // print_address
                    printData[12] = _dataGridView.Rows[_dataGridView.CurrentRow.Index].Cells[11].Value.ToString();

                    // 2020.06.22
                    // D-오디오 수삽 무조건 초도품
                    if (cbbLineName.Text.Equals("D-오디오 수삽"))
                    {
                        printData[9] = "초도품";
                    }

                    // Date 2020.02.26
                    // 날짜 비교 후 출력
                    if (Convert.ToDateTime(columnData[8]) < DateTime.Now)
                    {
                        if (MessageBox.Show("종료일이 지난 설변 태그입니다. 그래도 출력하시겠습니까?", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    PrintForm printForm = new PrintForm();

                    printForm.Owner = this;
                    printForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                mariaDB.InsertLogDB(ex.Message, false);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                BtnProductUpdate.PerformClick();
            }
            catch (Exception ex)
            {
                mariaDB.InsertLogDB(ex.Message, false);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /*
            label11.Visible = true;

            if (label11.ForeColor == Color.Red)
            {
                label11.ForeColor = Color.LimeGreen;
            }
            else if (label11.ForeColor == Color.LimeGreen)
            {
                label11.ForeColor = Color.Blue;
            }
            else
            {
                label11.ForeColor = Color.Red;
            }
            */
            timer2.Stop();

            업데이트ToolStripMenuItem.PerformClick();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                string[,] s = mariaDB.SelectQuery2("SELECT * FROM `eom_1floor`.`message_data` WHERE receive_name = '" + userNameData + "' AND receive_check = 'C' ORDER BY write_time");

                // 메세지가 있다면
                if (s.GetLength(0) > 0)
                {
                    string[] sendData = { s[0, 0], s[0, 1], s[0, 2], s[0, 3], s[0, 4] };

                    MessageForm_Receive messageForm_Receive = new MessageForm_Receive(sendData);

                    timer3.Stop();
                    messageForm_Receive.Owner = this;
                    messageForm_Receive.ShowDialog();
                    timer3.Start();
                }
            }
            catch (Exception ex)
            {
                dc.LogFileSave(ex.Message);
            }
        }

        private void 로그인ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 로그아웃ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 사용자등록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignForm signForm = new SignForm();

            signForm.Owner = this;
            signForm.ShowDialog();
        }

        private void 업데이트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();

                mariaDB.InsertLogDB("업데이트 시도", false);

                Guna2UpdateFormClass updateForm = new Guna2UpdateFormClass();

                updateForm.FormTitleName = Text;
                updateForm.DefaultPath = "ftp://10.239.19.91:2121/EOM_v3";
                updateForm.ToolsFolderPath = "ftp://10.239.19.91:2121/TOOLS";
                updateForm.UpdateFolderName = "UPDATE_FILE";
                updateForm.ToolsFileName = "UpdateTools.exe";
                updateForm.PatchFileName = "PatchList_M.dat";
                updateForm.InstanceForm = this;
                updateForm.StartForm();

                // 게스트 모드만 타이머 동작
                //if (!btnProductDelete.Enabled)
                // 2022.11.22
                // 게스트모드 조건변경
                if (lblUserName.Text == GUEST_LOGIN_MSG)
                {
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                mariaDB.InsertLogDB(ex.Message, false);
            }
        }

        private void 도움말HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 추가, 삭제 버튼 비활성화이면
            //if (!(btnProductAdd.Enabled && btnProductDelete.Enabled))
            // 2022.11.22
            // 게스트모드 조건 변경
            if (!(btnProductAdd.Enabled && lblUserName.Text != GUEST_LOGIN_MSG))
            {
                if (!adminMode)
                {
                    PasswordFormClass passwordFormClass = new PasswordFormClass();

                    passwordFormClass.FormTitleName = Text;
                    //.AdminPwd = "11223";
                    passwordFormClass.StartForm();

                    if (passwordFormClass.ReturnValue)
                    {
                        //AddMetroBtn.Enabled = true;
                        adminMode = true;
                        dgvSelect.Visible = true;
                        //DataGridViewUpdate();
                        timer1.Stop();

                        dgvGuest.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dgvGuest.RowTemplate.Height = 50;

                        BtnProductUpdate.PerformClick();

                        lblUserName.Text = "GUEST ADMIN님 로그인";
                    }
                }
                else
                {
                    btnProductAdd.Enabled = false;
                    adminMode = false;
                    dgvGuest.Visible = true;
                    GuestDataGridViewUpdate();
                    timer1.Start();

                    dgvGuest.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    BtnProductUpdate.PerformClick();

                    lblUserName.Text = GUEST_LOGIN_MSG;
                }
            }
        }

        private void 업데이트내역ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateListViewFormClass updateListViewFormClass = new UpdateListViewFormClass();

            updateListViewFormClass.FormTitleName = programName + " - 업데이트 내역";
            updateListViewFormClass.DB = settingData[0];
            updateListViewFormClass.Table = settingData[6];
            updateListViewFormClass.StrConn = strConn;
            updateListViewFormClass.BoundsLocation = 0;
            updateListViewFormClass.StartForm();
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if (cbbLineName.SelectedIndex != -1)
            {
                SearchForm searchForm = new SearchForm();

                searchType = "MAIN";
                searchForm.Owner = this;
                searchForm.ShowDialog();

                if (searchData[0] != string.Empty)
                {
                    ModelListUpdate();

                    ModelSelect(searchData[0], Convert.ToInt32(searchData[1]));

                    searchData[0] = string.Empty;
                    searchData[1] = string.Empty;
                }
            }
            else
            {
                Guna2Msg("오류", "라인을 선택해주세요");
            }
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            if (cbbLineName.SelectedIndex != -1)
            {
                ModelForm_M showForm = new ModelForm_M();

                showForm.Owner = this;
                showForm.ShowDialog();

                // 추가 된 내용이 있을 경우에만
                if (!modelSelectData.Equals(string.Empty))
                {
                    DataGridViewUpdate();
                    cbbProductName.SelectedItem = modelSelectData;
                    modelSelectData = string.Empty;
                }
            }
            else
            {
                Guna2Msg("오류", "라인을 선택해주세요");
            }
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < dgvSelect.RowCount; i++)
                {
                    if (dgvSelect.Rows[i].Cells[0].Value.ToString().Equals("True"))
                    {
                        string[] deleteData = new string[]
                        {
                            "line",
                            cbbLineName01.Text,
                            "model_name",
                            dgvSelect.Rows[dgvSelect.CurrentRow.Index].Cells[1].Value.ToString(),
                            "car_name",
                            dgvSelect.Rows[i].Cells[2].Value.ToString(),
                            "customer_eo_number",
                            dgvSelect.Rows[i].Cells[3].Value.ToString(),
                            "mobis_eo_number",
                            dgvSelect.Rows[i].Cells[4].Value.ToString(),
                            "eo_contents",
                            dgvSelect.Rows[i].Cells[5].Value.ToString(),
                            "start_date",
                            dgvSelect.Rows[i].Cells[7].Value.ToString(),
                            "end_date",
                            dgvSelect.Rows[i].Cells[8].Value.ToString(),
                            // 보여지는 것과 데이터가 틀림
                            // "registrant",
                            // dataGridView1.Rows[i].Cells[].Value.ToString(),
                            // "print_type",
                            // dataGridView1.Rows[i].Cells[].Value.ToString()
                            "shipment",
                            dgvSelect.Rows[i].Cells[11].Value.ToString(),
                        };

                        string query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], deleteData, "DELETE");
                        mariaDB.EtcQuery(query);

                        // 로그
                        string[] logData = new string[dgvSelect.ColumnCount];

                        // 라인, 모델
                        logData[0] = cbbLineName01.Text;
                        //logData[1] = cbbProductName01.Text;

                        for (int j = 1; j < logData.Length; j++)
                        {
                            logData[j] = dgvSelect.Rows[i].Cells[j].Value.ToString();

                            //dc.LogFileSave(logData[j]);
                        }

                        mariaDB.InsertLogDB(SplitConvert(logData) + " 모델 삭제", false);

                        count++;
                    }
                }

                if (count > 0)
                {
                    string tmpData = cbbProductName01.Text;

                    ModelListUpdate();
                    dgvSelect.Rows.Clear();
                    cbbProductName01.SelectedItem = tmpData;

                    Guna2Msg("오류", count.ToString() + "건 삭제가 완료되었습니다");
                }
                else
                {
                    Guna2Msg("오류", "선택 된 자료가 없습니다");
                }
            }
            catch (Exception ex)
            {
                mariaDB.InsertLogDB(ex.Message, false);
            }
        }

        private void BtnProductUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BtnProductUpdate.Enabled = false;

                // 2020.09.08
                // 종료일 버튼 삭제로 로직 변경
                if (cbbLineName.SelectedIndex != -1)
                {
                    GuestModeView();
                }
                else
                {
                    Guna2Msg("오류", "라인을 선택해주세요");
                }

                BtnProductUpdate.Enabled = true;

                debugLabel();
            }
            catch (Exception ex)
            {
                BtnProductUpdate.Enabled = true;

                mariaDB.InsertLogDB(ex.Message, false);
            }
        }

        private void MouseRightClickView(DataGridView _dataGridView, DataGridViewCellMouseEventArgs _e)
        {
            try
            {
                if (_e.RowIndex != -1)
                {
                    if (_e.Button == MouseButtons.Right)
                    {
                        _dataGridView.Rows[_e.RowIndex].Selected = true;                         // 마우스 우 클릭 선택
                        _dataGridView.CurrentCell = _dataGridView.Rows[_e.RowIndex].Cells[0];    // index 보정

                        string[] data = new string[_dataGridView.ColumnCount];

                        for (int i = 0; i < data.Length; i++)
                        {
                            data[i] = _dataGridView.Rows[_dataGridView.CurrentCell.RowIndex].Cells[i].Value.ToString();
                        }

                        MouseRightMenuDGV(_dataGridView, data);
                    }
                }
            }
            catch (Exception ex)
            {
                Guna2Msg("오류", ex.Message, true);
            }
        }

        public static void DGVMHTDataView(DataGridView _dgv, DataGridViewCellEventArgs _e, Form _form)
        {
            // 보기
            if (_e.ColumnIndex == 12)
            {
                string[] selectData = new string[]
                {
                    "line", cbbLineName01.Text,                                                            // 라인
                    "car_name", _dgv.Rows[_dgv.CurrentRow.Index].Cells[2].Value.ToString(),                 // 차종
                    "customer_eo_number", _dgv.Rows[_dgv.CurrentRow.Index].Cells[3].Value.ToString(),       // 고객사 EO
                    "mobis_eo_number", _dgv.Rows[_dgv.CurrentRow.Index].Cells[4].Value.ToString(),          // 모비스 EO
                    "eo_contents", _dgv.Rows[_dgv.CurrentRow.Index].Cells[5].Value.ToString(),              // EO 내용
                    //"registrant", _dgv.Rows[_dgv.CurrentRow.Index].Cells[9].Value.ToString(),             // 등록자
                    "print_type", _dgv.Rows[_dgv.CurrentRow.Index].Cells[10].Value.ToString(),              // 타입
                    "shipment", _dgv.Rows[_dgv.CurrentRow.Index].Cells[11].Value.ToString()                 // 출하지
                };
                string query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
                string[] columnData = mariaDB.SelectQueryCount(query, "mht_data");

                // 공백이 아니라면 파일 생성
                if (!columnData[0].Equals(string.Empty))
                {
                    string tmpPath = @"C:\" + Application.ProductName + @"\mht_data.mht";

                    FileInfo fi = new FileInfo(tmpPath);

                    // 파일 삭제
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }

                    // 파일 쓰기
                    File.WriteAllText(tmpPath, columnData[0], Encoding.Default);

                    string[] mhtSearchData = dc.MhtFileSearch(tmpPath);

                    // 제목
                    //subjectData = mhtSearchData[0];

                    // mht 경로
                    //eoViewData = tmpPath;

                    ViewForm viewForm = new ViewForm(tmpPath);

                    viewForm.Owner = _form;
                    viewForm.ShowDialog();
                }
            }
        }

        public class SearchEventArgs : EventArgs
        {
            public int theVar { get; private set; }

            public SearchEventArgs(int argVar)
            {
                theVar = argVar;
            }

        }

        private void MouseRightMenuDGV(DataGridView _dgv, string[] _data)
        {
            string shipmentData = pe.NowShipment(settingData[0], cbbLineName.Text, _data[1]);

            ContextMenu m = new ContextMenu();

            MenuItem menuItem0 = new MenuItem("설변 태그 인쇄");
            MenuItem menuItemLine1 = new MenuItem("-");
            MenuItem menuItem1 = new MenuItem("등록 내용 수정 (" + dc.EditMasterConvert() + " 권한 부여)");
            MenuItem menuItem2 = new MenuItem("오더 누락 입력 (샘플 제외)");
            MenuItem menuItemLine2 = new MenuItem("-");
            MenuItem menuItem11 = new MenuItem("해당 품번 선택 (품번 '" + _data[1] + "')");
            MenuItem menuItem12 = new MenuItem("동일 내용 검색 (차종 '" + _data[2] + "')");
            MenuItem menuItem13 = new MenuItem("동일 내용 검색 (고객사 EO '" + _data[3] + "')");
            MenuItem menuItem14 = new MenuItem("동일 내용 검색 (모비스 EO '" + _data[4] + "')");
            MenuItem menuItem15 = new MenuItem("동일 내용 검색 (EO 내용 '" + _data[5] + "')");
            MenuItem menuItem16 = new MenuItem("동일 내용 검색 (MAIN PCB '" + _data[13] + "')");
            MenuItem menuItem17 = new MenuItem("동일 내용 검색 (SUB PCB '" + _data[14] + "')");
            MenuItem menuItemLine3 = new MenuItem("-");

            MenuItem menuItem21 = new MenuItem("출하지 수정 (현재 '" + shipmentData + "')");
            //MenuItem menuItem21 = new MenuItem("출하지 수정 (현재 '" + lblShipmentView.Text + "')");

            MenuItem menuItem22 = new MenuItem("출하지 변경이력");
            MenuItem menuItemLine4 = new MenuItem("-");
            MenuItem menuItem31 = new MenuItem("EO 적용 여부 점검");

            m.MenuItems.Add(menuItem0);
            m.MenuItems.Add(menuItemLine1);
            m.MenuItems.Add(menuItem1);

            if (_dgv.Rows[_dgv.CurrentCell.RowIndex].Cells[16].Value.ToString() == "")
            {
                m.MenuItems.Add(menuItem2);
            }

            m.MenuItems.Add(menuItemLine2);

            if (dgvGuest.Visible)
            {
                m.MenuItems.Add(menuItem11);
            }

            m.MenuItems.Add(menuItem12);
            m.MenuItems.Add(menuItem13);
            m.MenuItems.Add(menuItem14);
            m.MenuItems.Add(menuItem15);
            m.MenuItems.Add(menuItem16);
            m.MenuItems.Add(menuItem17);
            m.MenuItems.Add(menuItemLine3);

            if (cbbLineName.Text == "D-오디오 조립")
            {
                m.MenuItems.Add(menuItem21);
                m.MenuItems.Add(menuItem22);
                m.MenuItems.Add(menuItemLine4);
            }

            m.MenuItems.Add(menuItem31);

            menuItem0.Click += new EventHandler(TagPrint);
            menuItem1.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 1, _data));
            menuItem2.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 2, _data));

            menuItem11.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 11, _data));
            menuItem12.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 12, _data));
            menuItem13.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 13, _data));
            menuItem14.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 14, _data));
            menuItem15.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 15, _data));
            menuItem16.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 16, _data));
            menuItem17.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 17, _data));

            if (cbbLineName.Text == "D-오디오 조립")
            {
                menuItem21.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 21, _data));
                menuItem22.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 22, _data));
            }

            menuItem31.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 31, _data));

            // 2021.08.27
            // 조별 선임 권한
            if (!dc.CheckEditMaster(userNameData))
            {
                menuItem1.Enabled = false;
                menuItem2.Enabled = false;
            }

            m.Show(_dgv, _dgv.PointToClient(Cursor.Position));
        }

        private void TagPrint(object sender, EventArgs e)
        {
            if (dgvSelect.Visible) PrintDataSetting(dgvSelect);
            else PrintDataSetting(dgvGuest);
        }

        private void ShipmentFirstData()
        {

        }

        private void ModelCurrentSearch(object sender, EventArgs e, int _type, string[] _data)
        {
            // _type 값 보정 값
            const int TYPE_OFFSET_VALUE = -10;
            int valueAfterCalibration = _type + TYPE_OFFSET_VALUE;

            switch (_type)
            {
                case 1:
                    if (_data[5] == "---------- 첫 투입 품번 등록 ----------")
                    {
                        Guna2Msg("오류", "수정할 수 없는 내용입니다");
                        return;
                    }

                    string[] selectData = new string[]
                    {
                        "line", cbbLineName01.Text,
                        "model_name", _data[1],
                        "car_name", _data[2],
                        "customer_eo_number", _data[3],
                        "mobis_eo_number", _data[4],
                        "eo_contents", _data[5],
                        "shipment", _data[11]
                    };
                    string query = dc.SelectDeleteQueryANDConvert(settingData[0], settingData[1], selectData, "SELECT");
                    string[,] resultData = mariaDB.SelectQuery2(query);

                    if (resultData.GetLength(0) == 1)
                    {
                        ModelForm_E editForm = new ModelForm_E(resultData);

                        editForm.Owner = this;
                        editForm.ShowDialog();

                        if (modelSelectData != string.Empty)
                        {
                            DataGridViewUpdate();
                            cbbProductName.SelectedItem = modelSelectData;
                            modelSelectData = string.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show("검색 데이터 2개 오류 발생 [윤민규 사원 문의]");
                    }
                    break;
                case 2:
                    Guna2Msg("오류", "이 기능은 미입력 될 경우 보정할 때 사용하므로 문의 후 사용바랍니다");

                    query = "SELECT * FROM `packingtoorder`.`" + settingData[5] + "` WHERE contents LIKE '%" + _data[1] + "%' AND contents LIKE '%" + _data[5] + "%' ORDER BY write_time";
                    query = query.Replace("\r\n", " "); // 보정 필요
                    string[] resultData2 = mariaDB.SelectQueryCount(query, "write_time");

                    if (resultData2.Length > 0)
                    {
                        DateTime indexDateTime = Convert.ToDateTime(resultData2[0]);

                        query = "SELECT * FROM `packingtoorder`.`" + settingData[5] + "` WHERE write_time < '" + indexDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "' AND write_time > '" + indexDateTime.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") + "' AND contents LIKE '%" + _data[1] + "%'";
                        string[,] resultData3 = mariaDB.SelectQuery2(query);

                        if (resultData3.GetLength(0) > 0)
                        {
                            string[] spilitContents = resultData3[0, 1].Split(' ');

                            if (spilitContents.Length == 6 && spilitContents[5].Length == 8)
                            {
                                spilitContents[5] = spilitContents[5].Replace(")", "");

                                // 오더 자리수 검사
                                if (spilitContents[5].Length == 7)
                                {
                                    if (MessageBox.Show("누락된 오더 '" + spilitContents[5] + "' 입력하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        string conditionQuery = "WHERE line = '" + cbbLineName01.Text + "' AND model_name = '" + _data[1] + "' AND car_name = '" + _data[2] + "' AND customer_eo_number = '" + _data[3] + "' AND mobis_eo_number = '" + _data[4] + "' AND eo_contents = '" + _data[5] + "' AND shipment = '" + _data[11] + "'";
                                        query = "SELECT * FROM `" + settingData[0] + "`.`" + settingData[1] + "` " + conditionQuery;
                                        string[,] resultData4 = mariaDB.SelectQuery2(query);

                                        if (resultData4.GetLength(0) == 1)
                                        {
                                            query = "UPDATE `" + settingData[0] + "`.`" + settingData[1] + "` SET start_order = '" + spilitContents[5] + "' " + conditionQuery;
                                            mariaDB.EtcQuery(query);
                                            //Clipboard.SetText(query);
                                            Guna2Msg("알림", "정상적으로 입력되었습니다");
                                        }
                                        else
                                        {
                                            Guna2Msg("오류", "1개 초과 조회 오류 [윤민규 사원 문의]");
                                        }
                                    }
                                    else
                                    {
                                        Guna2Msg("오류", "입력이 취소되었습니다");
                                    }
                                }
                                else
                                {
                                    Guna2Msg("오류", "오더 자리수 불일치");
                                }
                            }
                        }
                        else
                        {
                            Guna2Msg("오류", "검색된 데이터가 없어 자동 입력이 불가합니다 [2]");
                        }
                    }
                    else
                    {
#if DEBUG

#endif
                        Guna2Msg("오류", "검색된 데이터가 없어 자동 입력이 불가합니다 [1]");
                    }
                    break;
                // ---------------------
                //  품번 선택
                // ---------------------
                case 11:
                    // 2
                    if (dgvGuest.Visible)
                    {
                        cbbProductName.SelectedItem = dgvGuest.Rows[dgvGuest.CurrentCell.RowIndex].Cells[valueAfterCalibration].Value.ToString();
                    }
                    break;
                // ---------------------
                //  동일 내용 검색
                // ---------------------
                case 12:
                case 13:
                case 14:
                case 15:
                    // 2022.06.30
                    // 콤보 박스 선택 값 초기화
                    cbbProductName.SelectedIndex = -1;

                    if (dgvSelect.Visible) ModelSelect(dgvSelect.Rows[dgvSelect.CurrentCell.RowIndex].Cells[valueAfterCalibration].Value.ToString(), valueAfterCalibration - 1);
                    else ModelSelect(dgvGuest.Rows[dgvGuest.CurrentCell.RowIndex].Cells[valueAfterCalibration].Value.ToString(), valueAfterCalibration - 1);
                    break;
                // ---------------------
                //  동일 내용 검색 (M/PCB, S/PCB)
                // ---------------------
                case 16:
                case 17:
                    // 2022.06.30
                    // 콤보 박스 선택 값 초기화
                    cbbProductName.SelectedIndex = -1;

                    if (dgvSelect.Visible) ModelSelect(dgvSelect.Rows[dgvSelect.CurrentCell.RowIndex].Cells[valueAfterCalibration + 7].Value.ToString(), valueAfterCalibration - 1);
                    else ModelSelect(dgvGuest.Rows[dgvGuest.CurrentCell.RowIndex].Cells[valueAfterCalibration + 7].Value.ToString(), valueAfterCalibration - 1);
                    break;
                // ---------------------
                case 21:
                    string shipmentData = pe.NowShipment(settingData[0], cbbLineName.Text, _data[1]);
                    string[] sendData = { "", _data[1], shipmentData, "", "", "", "", "" };

                    OrderMasterForm_E orderMasterForm_E = new OrderMasterForm_E(sendData);

                    orderMasterForm_E.Owner = this;
                    orderMasterForm_E.ShowDialog();

                    historyInsertComplete = false;
                    break;
                case 22:
                    OrderMasterForm_H orderMasterForm_H = new OrderMasterForm_H(_data);
                    orderMasterForm_H.Owner = this;
                    orderMasterForm_H.ShowDialog();
                    break;
                case 31:
                    // M/PCB, 고객사 EO, 모비스 EO, EO 내용
                    //string[] sendData = {  };

                    EOCheckForm_M eoCheckForm_M = new EOCheckForm_M(_data);
                    //eoCheckForm_M.Owner = this;
                    //eoCheckForm_M.ShowDialog();
                    eoCheckForm_M.Show();
                    break;
            }
        }

        private void MouseRightClickView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MouseRightClickView((DataGridView)sender, e);
        }

        private void SeparateBackColor_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            SeparateBackColor((DataGridView)sender, e);
        }

        private void DGVMHTDataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGVMHTDataView((DataGridView)sender, e, this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dgvSelect.Visible)
            {
                ExportToExcel(dgvSelect);
            }
            else if (dgvGuest.Visible && dgvGuest.Rows.Count > 0)
            {
                ExportToExcel(dgvGuest);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Silver;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void label5_Click(object sender, EventArgs e)
        {
#if DEBUG
            /*
            RFIDFormClass rfidForm  = new RFIDFormClass();

            rfidForm.FormTitleName = Text;
            rfidForm.LineData = "A86";
            rfidForm.InstanceForm = this;
            rfidForm.StartForm();
            */

            EOForm eOForm = new EOForm();
            eOForm.ShowDialog();

            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "mht files (*.mht)|*.mht";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == string.Empty)
                return;

            ViewForm viewForm = new ViewForm(openFileDialog.FileName);
            viewForm.ShowDialog();
            */
#endif
        }

        private void btnShipmentChange_Click(object sender, EventArgs e)
        {
            OrderMasterForm_M orderMasterForm = new OrderMasterForm_M();

            Opacity = 0;

            orderMasterForm.Owner = this;
            orderMasterForm.ShowDialog();

            Opacity = 1;
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 2022.06.30
            // 중복 모델 선택 시퀀스 예방
            // 값이 없으면 실행 안함
            if (cbbProductName.SelectedIndex != -1)
            {
                ModelSelect(cbbProductName.Text);
                ModelSort();
            }
        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lineSelectData[0] = cbbLineName01.SelectedIndex.ToString();

            // 2020.09.04
            // 게스트 모드
            //if (!btnProductDelete.Enabled)
            // 2022.11.22
            // 삭제 버튼 비활성화 필요로 조건 변경
            if (lblUserName.Text == GUEST_LOGIN_MSG)
            {
                if (cbbLineName01.Text == "D-오디오 조립")
                {
                    cbbMetroFloor01.Visible = true;
                }
                else
                {
                    cbbMetroFloor01.Visible = false;
                }
            }

            // 2020.09.04
            // 자동 뷰
            BtnProductUpdate.PerformClick();

            // 2022.01.06
            // 출하지 마스터 활성화
            if (cbbLineName01.Text == "D-오디오 조립")
            {
                btnShipmentMaster.Visible = true;
                btnShipmentChange.Visible = true;
            }
            else
            {
                btnShipmentMaster.Visible = false;
                btnShipmentChange.Visible = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageForm_List messageForm_List = new MessageForm_List();

            messageForm_List.Owner = this;
            messageForm_List.ShowDialog();

            /*
            MessageFormClass messageFormClass = new MessageFormClass();
            messageFormClass.FormMsg = "메세지 전송이 완료되었습니다";
            messageFormClass.ShowDelay = 1000;
            messageFormClass.InstanceForm = this;
            messageFormClass.StartForm();
            */
        }

        private void lblLine_Click(object sender, EventArgs e)
        {
#if DEBUG
            EOForm eOForm = new EOForm();
            eOForm.ShowDialog();

            /*
            if (guna2ProgressBar1.Value == 100)
            {
                guna2ProgressBar1.Value = 0;
            }
            else
            {
                guna2ProgressBar1.Value += 20;
            }
            */
#endif
        }

        private void lblFormTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            guna2ControlBox2.PerformClick();
            //if (WindowState == FormWindowState.Normal)
            //{
            //    WindowState = FormWindowState.Maximized;
            //}
            //else if (WindowState == FormWindowState.Maximized)
            //{
            //    WindowState = FormWindowState.Normal;
            //}
        }

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnProductUpdate.PerformClick();
        }

        private void lblShipmentView_VisibleChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(lblShipmentView.Visible.ToString());
        }

        private void dataGridView1_VisibleChanged(object sender, EventArgs e)
        {
            if (dgvSelect.Visible)
            {
                dgvGuest.Visible = false;

                // 2022.06.30
                // 출하지 텍스트 출력
                if (cbbLineName.Text == "D-오디오 조립")
                {
                    pnShipmentView.Visible = true;
                    pnFpiCheck.Visible = true;
                }
            }
            else
            {
                pnShipmentView.Visible = false;
                pnFpiCheck.Visible = false;
            }
        }

        private void dgvGuest_VisibleChanged(object sender, EventArgs e)
        {
            if (dgvGuest.Visible)
            {
                dgvSelect.Visible = false;

                btnProductDelete.Enabled = false;
            }
            else
            {
                btnProductDelete.Enabled = true;
            }
        }

    }
}
