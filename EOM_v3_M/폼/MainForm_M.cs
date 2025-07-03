using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
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

        public static readonly string RESERVATION_DEFAULT_DATE = $"1000-01-01 00:00:00";

#if !DEBUG
        public static readonly string DATABASE_NAME = $"eom_1floor";
#else
        public static readonly string DATABASE_NAME = $"eom_1floor_trunk";
#endif

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
        //public static string userNameData = string.Empty;
        public static string programName = $"Engineer Order Manager v3.1 (멀티생산파트 통합)";
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
        /// <summary>
        /// 2024.11.03
        /// 데이터베이스 전환
        /// [0]: 이름
        /// [1]: 네트워크 맥 주소
        /// [2]: 프린터 이름
        /// </summary>
        public static string[] strUserAddressData;
        public static string[] updateData, lineSelectData;
        public static string[] eoSelectData = new string[9];
        public static string[] printData = new string[13];
        public static readonly string[] LINE_NAME_LIST = new string[] { "D-오디오 수삽", "D-오디오 조립", "D-오디오 SUB", "클러스터" };
        //public static string[] shipmentData = { "OEM", "CKD", "KD" };
        public static string[] searchData = new string[] { "", "" };

        public static DefaultClass dc = new DefaultClass();
        public static PackingAndEOM pe;
        public static MariaDBClass mariaDB = new MariaDBClass();
        public static ComboBox cbbProductName01, cbbLineName01, cbbFloor01;
        public static DataGridView datagridview01, datagridview02;

        /// <summary>
        /// DataGridView 초기 설정
        /// </summary>
        private void initialSetDataGridView()
        {
            dc.DataGridViewDoubleBuffered(dgvMain, true);
            dc.DataGridViewDoubleBuffered(dgvSelect, true);

            // dataGridView 자동 조절
            //dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //dgvSelect.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // dataGridView 줄바꿈
            dgvMain.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSelect.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // dataGridView 정렬
            ModelSort();

            // 처음 위치 재설정
            //dgvSelect.Location = new Point(11, 193);
            //dgvMain.Location = new Point(11, 193);
            pnMain.Location = new Point(11, 193);
            pnSelect.Location = new Point(11, 193);

            pnMain.Visible = true;
            pnSelect.Visible = false;

            // 2022.07.01
            // 그리드 뷰 사이즈 조정 
            SetDataGridViewCellsWidthSize(dgvMain);
            SetDataGridViewCellsWidthSize(dgvSelect);
        }

        /// <summary>
        /// DataGridView 셀 넓이 초기 설정
        /// </summary>
        /// <param name="_dgv"></param>
        private void SetDataGridViewCellsWidthSize(DataGridView _dgv)
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

            for (int i = 0; i < _dgv.ColumnCount; i++)
            {
                _dgv.Columns[i].Width = setSize[i];
            }
        }

        /// <summary>
        /// 컨트롤 위치 초기 설정
        /// </summary>
        private void InitialControlSize()
        {
            lblUserName.Left = Width - 245;
            // 2022.11.22
            // 출하지 폼 크기 위치 이동 수정
            
            txtMenuAllSearch.Left =     Width - txtMenuAllSearch.Width  - 107;

            /*
            nShipmentView.Left =        Width - pnShipmentView.Width    - 474;
            btnShipmentMaster.Left =    Width - btnShipmentMaster.Width - 378;
            btnShipmentChange.Left =    Width - btnShipmentChange.Width - 282;
            btnProductSearch.Left =     Width - btnProductSearch.Width  - 185;
            btnProductAdd.Left =        Width - btnProductAdd.Width     - 99;
            btnProductDelete.Left =     Width - btnProductDelete.Width  - 13;
            */

            grpProduct.Left = Width - grpProduct.Width - 10;
            grpEOCheck.Left = grpProduct.Left - grpEOCheck.Width - 6;
            grpShipment.Left = grpEOCheck.Left - grpShipment.Width - 6;
            grpDgvFilter.Left = grpShipment.Left - grpDgvFilter.Width - 6;

            //dgvMain.Size =  new Size(Width - 22, Height - 204);
            //dgvSelect.Size = new Size(Width - 22, Height - 204);
            pnMain.Size =  new Size(Width - 22, Height - 204);
            pnSelect.Size = new Size(Width - 22, Height - 204);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form">현재 폼 인스턴스</param>
        /// <param name="_type">오류</param>
        /// <param name="_msg">메시지 내용</param>
        public static void Guna2Msg(Form _form, string _type, string _msg)
        {
            try
            {
                Guna2MessageBox guna2MessageBox = new Guna2MessageBox();
                guna2MessageBox.InstanceForm = _form;
                guna2MessageBox.FormType = _type;
                guna2MessageBox.FormMsg = _msg;
                guna2MessageBox.StartForm();
            }
            catch (Exception ex)
            {
                dc.LogFileSave(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form">현재 폼 인스턴스</param>
        /// <param name="_type">오류</param>
        /// <param name="_msg">메시지 내용</param>
        public static void Guna2Msg(Form _form, string _type, string _msg, bool _data)
        {
            try
            {
                Guna2MessageBox guna2MessageBox = new Guna2MessageBox();
                guna2MessageBox.InstanceForm = _form;
                guna2MessageBox.FormType = _type;
                guna2MessageBox.FormMsg = _msg;
                guna2MessageBox.StartForm();

                if (_data) dc.LogFileSave(_msg);
            }
            catch (Exception ex)
            {
                dc.LogFileSave(ex.Message);
            }
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
                //cbbFloor01 = cbbFloor;
                datagridview01 = dgvSelect;
                datagridview02 = dgvMain;

                // 2024.04.26
                // 셋팅 파일 로드
                settingData = dc.SettinFileLoad();

#if DEBUG
                Text += " [디버깅 모드]";
#endif

                // 2020.09.04
                // MariaDB 셋팅 먼저 검사
                strConn = $"Server={ settingData[settingData.Length - 2] }; Port={ settingData[settingData.Length - 1] }; Database={ DATABASE_NAME }; Uid=root; Pwd=9001271;";

                mariaDB.DbConnInfo = strConn;

                pe = new PackingAndEOM(strConn);

                // DB 서버 죽으면 점검 메세지
                if (!mariaDB.IsConnected())
                {
                    Guna2Msg(this, "오류", "서버 연결에 실패하였습니다.");
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
                for (int i = 0; i < LINE_NAME_LIST.Length; i++)
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
                    cbbFloor01.SelectedItem = floorData[0];
                }
                */

                // 2021.02.16
                // Floor 정보는 여기서 채워짐 (1F, 2F) ++++++++ 추가
                // SetFloorData(userData);

                // 2024.11.03
                // 사용자 설정 NEW
                strUserAddressData = mariaDB.SelectRegstrant("user_data");

                // 2021.08.27
                // 조별 선임 체크 선언
                //userNameData = SearchRegistrant(userData);

                // 2025.04.11
                // 조별 선임 권한
                if (dc.CheckSeniorMaster(strUserAddressData[0]))
                {
                    btnNewEO.Enabled = true;
                }

                // 사용자 확인되면 버튼 활성화
                // Floor 정보는 여기서 채워짐 (1F, 2F) -------- 삭제
                //if (!userNameData.Equals(userData))
                if (strUserAddressData != null)
                {
                    lblUserName.Text = strUserAddressData[0] + "님(" + strUserAddressData[2] + ") 로그인";

                    btnProductAdd.Enabled = true;
                    //btnProductDelete.Enabled = true;
                    //cbbFloor.Visible = false;
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
                    /*
                    // 층 설정
                    floorData = dc.DatFileLoad(@"floorData.dat");

                    if (!dc.DatFileCheck(floorData, 1))
                    {
                        floorData = new string[] { "1F" };
                    }
                    else
                    {
                        cbbFloor01.SelectedItem = floorData[0];
                    }

                    //cbbFloor.Visible = true;

                    // 2020.09.01
                    // 콤보 박스 활성화
                    //VisibleFloor();

                    // 2F
                    cbbFloor01.SelectedItem = floorData[0];
                    */

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
                    updateListViewFormClass.DB = DATABASE_NAME;
                    updateListViewFormClass.Table = $"update_data";
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
                    label11.Text = $"신규 버전이 등록되어 있습니다";
                    label11.ForeColor = Color.Red;
                    label11.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

                    timer2.Interval = 1500;
                    timer2.Start();
                }
                */

                timer3.Interval = 1000 * 60 * 10;
                timer3.Start();

                // 접속 로그
                //mariaDB.InsertLogDB("프로그램 시작 [" + dc.Version() + "], " + sw.ElapsedMilliseconds + "ms", false);
                mariaDB.InsertLogDB(DATABASE_NAME, "프로그램 시작[" + dc.Version() + "], " + sw.ElapsedMilliseconds + "ms", "user_data", false);

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
                //mariaDB.InsertLogDB(ex.Message, false);
                //mariaDB.InsertLogDB(ex.StackTrace, false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.Message, "user_data", false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.StackTrace, "user_data", false);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer3.Enabled)
            {
                // 접속 로그
                mariaDB.InsertLogDB(DATABASE_NAME, "프로그램 종료", "user_data", false);
            }
            else
            {
                mariaDB.InsertLogDB(DATABASE_NAME, "프로그램 자동 종료", "user_data", false);

                //Guna2Msg(this, "알림", "프로그램 정책에 따라 5분 미사용시 자동 종료됩니다.");
            }

            // 게스트 모드라면
            //if (!btnProductDelete.Enabled)
            // 2022.11.22
            // 게스트 모드 조건 변경
            if (lblUserName.Text == GUEST_LOGIN_MSG)
            {
                // 2020.09.03
                // 비활성화 경우가 발생하면 값을 불러오지 못함

                // 층 설정 파일 저장
                //dc.DatFileSave(floorFileName + extensionName, floorData);
            }

            // 라인 설정 파일 저장
            dc.DatFileSave(lineSelectFileName + extensionName, lineSelectData);

            
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            try
            { 
                if (OwnedForms.Length > 0)
                {
                    OwnedForms[0].TopMost = true;
                    OwnedForms[0].TopMost = false;
                }

                Width = FORM_WIDTH_SIZE;
                Height = FORM_HEIGHT_SIZE;
            }
            catch (Exception ex)
            {
                dc.LogFileSave(ex.Message);
            }
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

                if (cbbFloor01.SelectedItem != null)
                    test = cbbFloor01.SelectedItem.ToString();

                //label11.Text = $"floorData: " + floorData[0] + " / Select: " + test + " / lineSelectData: " + lineSelectData[0];
            }
            else
            {
                //label11.Visible = false;
            }
        }

        private void GuestModeView()
        {
            pnMain.Visible = true;

            GuestDataGridViewUpdate();
        }

        private string RegistrantFilterAdd(string _data)
        {
            string[] selectData = new string[] { "print", _data };
            //string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "registrant_data", selectData, "SELECT");
            string query = dc.SelectDeleteQueryANDConvert("registrant", "user_data", selectData, "SELECT");
            string[] tmpData = mariaDB.SelectQueryCount(query, "name");

            string returnData = string.Empty;

            if (tmpData.Length > 0)
            {
                returnData = $" AND (";

                for (int i = 0; i < tmpData.GetLength(0); i++)
                {
                    returnData += (i == 0) ? "registrant = '" + tmpData[i] + "'" : " OR registrant = '" + tmpData[i] + "'";
                }

                returnData += ")";
            }

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
                    //floorData[0] = $"1F";
                }
                // 2F
                else if (cbbLineName01.Text == "D-오디오 수삽" || cbbLineName01.Text == "클러스터" || cbbLineName01.Text == "HUD")
                {
                    //floorData[0] = $"2F";
                }
                // 1F or 2F
                else
                {
                    // 2020.09.08
                    // 예외 처리
                    if (cbbFloor01.SelectedItem == null)
                    {
                        cbbFloor01.SelectedIndex = 0;
                    }

                    //floorData[0] = cbbFloor01.SelectedItem.ToString();
                }

                // dataGridView 초기화
                dgvMain.Rows.Clear();

                // Update Time
                lblDBUpTime.Text = $"{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }, { sw.ElapsedMilliseconds.ToString() }ms";
            }

            /*
             * 체크박스 조건 추가
             */
            string reservationString = chkReservationView.Checked ? "OR end_date = '1000-01-01 00:00:00'" : "";

            // 클러스터, D-오디오 SUB 라인은 등록자 여부 상관없이 보이도록 수정
                if (cbbLineName01.Text == "클러스터" || cbbLineName01.Text == "D-오디오 SUB")
            {
                query = $"SELECT * FROM `{ DATABASE_NAME }`.`model_data` WHERE (line = '{ cbbLineName01.Text }' AND end_date > '{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }')";
            }
            else
            {
                query = $"SELECT * FROM `{ DATABASE_NAME }`.`model_data` WHERE (line = '{ cbbLineName01.Text }' AND (end_date > '{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }' {reservationString})) { RegistrantFilterAdd(strUserAddressData[2]) }";
            }

            query += " ORDER BY start_date";

            Clipboard.SetText(query);

            // 2021.03.24
            // 첫 투입 내용은 제일 상단에 올리도록
            //query += " ORDER BY FIELD(eo_contents, '" + PACKING_REGISTER_CONTENTS + "') DESC, start_date ASC";

            string[,] modelData = mariaDB.SelectQuery2(query);

            dc.LogFileSave(query);

            DGVRowsDataAdd(dgvMain, modelData, 1);

            ModelSort();

            // Update Time
            lblDBUpTime.Text = $"{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }, { sw.ElapsedMilliseconds.ToString() }ms";
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
            lblDBUpTime.Text = DateTime.Now.ToString() + ", { sw.ElapsedMilliseconds.ToString() }ms";
        }

        public void ModelListUpdate()
        {
            Stopwatch sw = new Stopwatch();

            sw.Reset();
            sw.Start();

            string[] selectData = new string[] { "line", cbbLineName01.Text };
            //string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
            string query = $"SELECT model_name FROM `{ DATABASE_NAME }`.`model_data` WHERE line = '{ cbbLineName01.Text }' GROUP BY model_name";
            //string[] modelData = mariaDB.SelectQueryCount(query, "model_name");
            string[,] modelData = mariaDB.SelectQuery4(query, 1);

            // 모델 데이터 로드
            cbbProductName.Items.Clear();

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

        /*
        private void SetFloorData(string _data)
        {
            string[] selectData = new string[] { };
            string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "registrant_data", selectData, "SELECT");
            string[,] resultData = mariaDB.SelectQuery2(query);

            for (int i = 0; i < resultData.GetLength(0); i++)
            {
                if (resultData[i, 0] == _data)
                {
                    floorData[0] = resultData[i, 2];
                }
            }
        }
        */

        /// <summary>
        /// 맥 어드레스
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public static string SearchRegistrant(string _data)
        {
            string[] selectData = new string[] { };
            //string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "registrant_data", selectData, "SELECT");
            string query = dc.SelectDeleteQueryANDConvert("registrant", "user_data", selectData, "SELECT");
            string[,] tmpData = mariaDB.SelectQuery2(query);

            for (int i = 0; i < tmpData.GetLength(0); i++)
            {
                // 맥 어드레스 존재
                if (tmpData[i, 1].Equals(_data))
                {
                    // 층 설정
                    //floorData[0] = tmpData[i, 2];
                    // 이름 설정
                    return tmpData[i, 0];
                }
            }

            return _data;
        }

        private void DGVRowsDataAdd(DataGridView _dgv, string[,] _data, int _type)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            // dataGridView 초기화
            _dgv.Rows.Clear();

            // dataGridView 초기화
            dgvSelect.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dc.LogFileSave(_data.GetLength(0) + "EA");

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                // 등록자 불러오기
                //string registrantData = SearchRegistrant(_data[i, 10]);
                //string printType = string.Empty;
                string printType = _data[i, 11];

                /*
                // 타입
                // D-오디오 수삽
                printType = cbbLineName.SelectedItem.Equals(LINE_NAME_LIST[0]) ? "사내용" : _data[i, 11];
                */

                string shipment = string.Empty;

                // 출하지
                // D-오디오 수삽, D-오디오 SUB
                if (cbbLineName.SelectedItem.Equals(LINE_NAME_LIST[0]) || cbbLineName.SelectedItem.Equals(LINE_NAME_LIST[2]))
                {
                    shipment = $"-";
                }
                else
                {
                    shipment = _data[i, 12];
                }

                // *.mht
                string eoMhtData = string.Empty;

                if (!_data[i, 13].Equals(string.Empty))
                    eoMhtData = $"보기";

                // 메인
                if (_type == 1)
                {
                    pnMain.Visible = true;

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
                        //string.Empty,                                                           // -등록자
                        _data[i, 10],                                                           // 등록자
                        printType,                                                              // 타입
                        shipment,                                                               // 출하지
                        eoMhtData,                                                              // mht
                        _data[i, 14],                                                           // MAIN PCB
                        _data[i, 15],                                                           // SUB PCB
                        _data[i, 16],                                                           // EO 구분
                        _data[i, 17]                                                            // 적용 오더
                    );
                }
                // 모델 선택
                else if (_type == 2)
                {
                    pnSelect.Visible = true;

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
                        //registrantData,                                                         // -등록자
                        _data[i, 10],                                                           // 등록자
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
                        _dgv.Rows[i].Cells[6].Value = $"없음";
                        break;
                }

                // 2025.03.31
                // 예약 기능관련 적용일 뷰
                if (_dgv.Rows[i].Cells[8].Value.ToString() == RESERVATION_DEFAULT_DATE)
                {
                    _dgv.Rows[i].Cells[8].Value = $"-";
                }
            }

            //ModelSort();

            // dataGridView 자동 조절
            dgvSelect.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dc.LogFileSave("DataGridView Add items.. " + sw.ElapsedMilliseconds + "ms");
        }

        public void ModelSelect(string _data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // 모델 데이터 로드
            string[] selectData = new string[] { "line", cbbLineName01.Text, "model_name", _data };
            string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");

            // 2021.03.24
            // 첫 투입 내용은 제일 상단에 올리도록
            query += " ORDER BY FIELD(eo_contents, '" + PACKING_REGISTER_CONTENTS + "') DESC, start_date ASC";

            string[,] modelData = mariaDB.SelectQuery2(query);

            DGVRowsDataAdd(dgvSelect, modelData, 2);

            // 2022.06.30
            // 출하지 텍스트 출력
            if (pnShipmentView.Visible)
            {
                string shipmentData = pe.NowShipment(DATABASE_NAME, cbbLineName.Text, cbbProductName.Text);

                lblShipmentView.Text = shipmentData;
            }

            // 2023.05.17
            // 초물리스트 등록 여부
            selectData = new string[] { "line", cbbLineName01.Text, "model_name", _data };
            query = dc.SelectDeleteQueryANDConvert("fpi", "model_data", selectData, "SELECT");

            string[] modelCount = mariaDB.SelectQueryCount(query, "model_name");

            if (modelCount.Length > 0)
            {
                lblFpiCheck.ForeColor = Color.FromArgb(0, 192, 0);
                lblFpiCheck.Text = $"등록 완료";
            }
            else
            {
                lblFpiCheck.ForeColor = Color.FromArgb(192, 0, 0);
                lblFpiCheck.Text = $"미등록";
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
                query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
            }
            // 고객사 EO 조회
            else if (_type == 2)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "customer_eo_number", _data };
                query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
            }
            // 모비스 EO 조회
            else if (_type == 3)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "mobis_eo_number", _data };
                query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
            }

            // EO 내용 조회
            else if (_type == 4)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "eo_contents", _data };
                query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
            }
            // MAIN PCB 조회
            else if (_type == 5)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "main_pcb_name", _data };
                query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
            }
            // SUB PCB 조회
            else if (_type == 6)
            {
                string[] selectData = new string[] { "line", cbbLineName01.Text, "sub_pcb_name", _data };
                query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
            }

            else
            {
                Guna2Msg(this, "오류", "치명적인 오류");
                return;
            }

            string[,] resultData = mariaDB.SelectQuery2(query);

            DGVRowsDataAdd(dgvMain, resultData, 1);
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
            SetDoNotSort(dgvSelect);
            SetDoNotSort(dgvMain);
            DGVRowsBackColor(dgvSelect);
            DGVRowsBackColor(dgvMain);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dgv"></param>
        static public void DGVRowsBackColor(DataGridView _dgv)
        {
            for (int i = 0; i < _dgv.RowCount; i++)
            {
                // 0. EO 예약
                // 종료일 [8]
                if (_dgv.Rows[i].Cells[8].Value.ToString() == "-")
                {
                    _dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 128, 128);
                }
                // 1. 출하지
                // 출하지 [11]
                else if (_dgv.Rows[i].Cells[11].Value.ToString() == "CKD")
                {
                    _dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
                }
                else if (_dgv.Rows[i].Cells[11].Value.ToString() == "KD")
                {
                    _dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
                }
            }

            _dgv.ClearSelection();
        }

        static public void SeparateBackColor(DataGridView _dgv, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            //dc.LogFileSave(_dgv.Rows[e.RowIndex].Cells["Column3"].Value.ToString().Contains("내부관리").ToString());

            if (_dgv.Rows[e.RowIndex].Cells[5].Value.ToString().Contains("내부관리"))
            {
                _dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        public void ExportToExcel(DataGridView grid)
        {
            Excel.Application myApplication;
            Excel._Workbook myWorkbook;
            Excel._Worksheet mySheet;
            //Excel.Range oRng;

            MessageFormClass messageFormClass = new MessageFormClass();

            messageFormClass.FormMsg = $"엑셀에 작성하고 있습니다";
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
                            saNames[i, j - 1] = $"'" + grid.Rows[i].Cells[j].Value.ToString();
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
                errorMessage = $"Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }

        private void PrintDataSetting(DataGridView _dgv)
        {
            try
            {
                if (btnProductAdd.Enabled || adminMode)
                {
                    string[] columnData = new string[_dgv.ColumnCount];

                    for (int i = 0; i < columnData.Length; i++)
                    {
                        columnData[i] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[i].Value.ToString();
                    }

                    printData[0] = cbbLineName01.Text;                                                                                          // line
                    printData[1] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[1].Value.ToString();                                                  // model_name
                    printData[2] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[2].Value.ToString();                                                  // car_name
                    printData[3] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[3].Value.ToString();                                                  // customer_eo_number
                    printData[4] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[4].Value.ToString();                                                  // mobis_eo_number
                    printData[5] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[5].Value.ToString();                                                  // eo_contents
                    printData[6] = Convert.ToDateTime(_dgv.Rows[_dgv.CurrentRow.Index].Cells[7].Value.ToString()).ToString("yyyy-MM-dd");       // reporting_date
                    printData[7] = string.Empty;                                                                                                // manager_name
                    printData[8] = string.Empty;                                                                                                // contact_number
                    printData[9] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[10].Value.ToString();                                                 // print_type
                    printData[10] = string.Empty;                                                                                               // print_count
                    printData[11] = string.Empty;                                                                                               // print_address
                    printData[12] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[11].Value.ToString();

                    // 2020.06.22
                    // D-오디오 수삽 무조건 초도품
                    if (cbbLineName.Text.Equals("D-오디오 수삽"))
                    {
                        printData[9] = $"초도품";
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
                //mariaDB.InsertLogDB(ex.Message, false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.Message, "user_data", false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.StackTrace, "user_data", false);
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
                //mariaDB.InsertLogDB(ex.Message, false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.Message, "user_data", false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.StackTrace, "user_data", false);
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
            //mariaDB.InsertLogDB(DATABASE_NAME, "프로그램 미사용 자동종료", "user_data", false);
            timer3.Stop();
            Application.Exit();
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

                mariaDB.InsertLogDB(DATABASE_NAME, "업데이트 시도", "user_data", false);

                Guna2UpdateFormClass updateForm = new Guna2UpdateFormClass();

                updateForm.FormTitleName = Text;
                updateForm.DefaultPath = $"ftp://10.239.19.91:2121/EOM_v3";
                updateForm.ToolsFolderPath = $"ftp://10.239.19.91:2121/TOOLS";
                updateForm.UpdateFolderName = $"UPDATE_FILE";
                updateForm.ToolsFileName = $"UpdateTools.exe";
                updateForm.PatchFileName = $"PatchList_M.dat";
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
                //mariaDB.InsertLogDB(ex.Message, false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.Message, "user_data", false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.StackTrace, "user_data", false);
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
                    //.AdminPwd = $"11223";
                    passwordFormClass.StartForm();

                    if (passwordFormClass.ReturnValue)
                    {
                        adminMode = true;
                        pnSelect.Visible = true;
                        //DataGridViewUpdate();
                        timer1.Stop();

                        dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dgvMain.RowTemplate.Height = 50;

                        BtnProductUpdate.PerformClick();

                        lblUserName.Text = $"GUEST ADMIN님 로그인";
                    }
                }
                else
                {
                    btnProductAdd.Enabled = false;
                    adminMode = false;
                    GuestDataGridViewUpdate();
                    timer1.Start();

                    dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    BtnProductUpdate.PerformClick();

                    lblUserName.Text = GUEST_LOGIN_MSG;
                }
            }
        }

        private void 업데이트내역ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateListViewFormClass updateListViewFormClass = new UpdateListViewFormClass();

            updateListViewFormClass.FormTitleName = programName + " - 업데이트 내역";
            updateListViewFormClass.DB = DATABASE_NAME;
            updateListViewFormClass.Table = $"update_data";
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

                searchType = $"MAIN";
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
                Guna2Msg(this, "오류", "라인을 선택해주세요");
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
                Guna2Msg(this, "오류", "라인을 선택해주세요");
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
                            //"end_date",
                            //dgvSelect.Rows[i].Cells[8].Value.ToString(),
                            // 보여지는 것과 데이터가 틀림
                            // "registrant",
                            // dataGridView1.Rows[i].Cells[].Value.ToString(),
                            // "print_type",
                            // dataGridView1.Rows[i].Cells[].Value.ToString()
                            "shipment",
                            dgvSelect.Rows[i].Cells[11].Value.ToString(),
                        };

                        string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", deleteData, "DELETE");
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

                        mariaDB.InsertLogDB(DATABASE_NAME, SplitConvert(logData) + " 모델 삭제", "user_data", false);

                        count++;
                    }
                }

                if (count > 0)
                {
                    string tmpData = cbbProductName01.Text;

                    ModelListUpdate();
                    dgvSelect.Rows.Clear();
                    cbbProductName01.SelectedItem = tmpData;

                    Guna2Msg(this, "오류", count.ToString() + "건 삭제가 완료되었습니다");
                }
                else
                {
                    Guna2Msg(this, "오류", "선택 된 자료가 없습니다");
                }
            }
            catch (Exception ex)
            {
                mariaDB.InsertLogDB(DATABASE_NAME, ex.Message, "user_data", false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.StackTrace, "user_data", false);
            }
        }

        private void BtnProductUpdate_Click(object sender, EventArgs e)
        {
            dc.LogFileSave("갱신 클릭");

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
                    Guna2Msg(this, "오류", "라인을 선택해주세요");
                }

                BtnProductUpdate.Enabled = true;

                // 2024.04.04
                lblFpiCheck.Text = $"초물 확인";
                lblFpiCheck.ForeColor = Color.Blue;

                debugLabel();
            }
            catch (Exception ex)
            {
                BtnProductUpdate.Enabled = true;

                mariaDB.InsertLogDB(DATABASE_NAME, ex.Message, "user_data", false);
                mariaDB.InsertLogDB(DATABASE_NAME, ex.StackTrace, "user_data", false);
            }
        }

        private void MouseRightClickView(DataGridView _dgv, DataGridViewCellMouseEventArgs _e)
        {
            try
            {
                if (_e.RowIndex != -1)
                {
                    if (_e.Button == MouseButtons.Right)
                    {
                        _dgv.Rows[_e.RowIndex].Selected = true;                             // 마우스 우 클릭 선택
                        _dgv.CurrentCell = _dgv.Rows[_e.RowIndex].Cells[_e.ColumnIndex];    // index 보정

                        string[] data = new string[_dgv.ColumnCount + 1];

                        for (int i = 0; i < 6; i++)
                        {
                            data[i] = _dgv.Rows[_dgv.CurrentCell.RowIndex].Cells[i].Value.ToString();
                        }

                        // 스티커 색상
                        data[6] = _dgv.Rows[_dgv.CurrentRow.Index].Cells[6].Style.BackColor.Name.ToString();

                        for (int i = 7; i < data.Length; i++)
                        {
                            data[i] = _dgv.Rows[_dgv.CurrentCell.RowIndex].Cells[i - 1].Value.ToString();
                        }


                        MouseRightMenuDGV(_dgv, data);
                    }
                }
            }
            catch (Exception ex)
            {
                Guna2Msg(this, "오류", ex.Message, true);
            }
        }

        public static void DGVMHTDataView(DataGridView _dgv, DataGridViewCellEventArgs _e, Form _form)
        {
            // 보기
            if (_e.ColumnIndex == 12)
            {
                string[] selectData = new string[]
                {
                    "line", cbbLineName01.Text,                                                             // 라인
                    "car_name", _dgv.Rows[_dgv.CurrentRow.Index].Cells[2].Value.ToString(),                 // 차종
                    "customer_eo_number", _dgv.Rows[_dgv.CurrentRow.Index].Cells[3].Value.ToString(),       // 고객사 EO
                    "mobis_eo_number", _dgv.Rows[_dgv.CurrentRow.Index].Cells[4].Value.ToString(),          // 모비스 EO
                    "eo_contents", _dgv.Rows[_dgv.CurrentRow.Index].Cells[5].Value.ToString(),              // EO 내용
                    //"registrant", _dgv.Rows[_dgv.CurrentRow.Index].Cells[9].Value.ToString(),             // 등록자
                    "print_type", _dgv.Rows[_dgv.CurrentRow.Index].Cells[10].Value.ToString(),              // 타입
                    "shipment", _dgv.Rows[_dgv.CurrentRow.Index].Cells[11].Value.ToString()                 // 출하지
                };
                string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
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

        private void MouseRightMenuDGV(DataGridView _dgv, string[] _data)
        {
            string shipmentData = pe.NowShipment(DATABASE_NAME, cbbLineName.Text, _data[1]);

            ContextMenu m = new ContextMenu();

            MenuItem menuItem0 = new MenuItem("설변 태그 인쇄");
            MenuItem menuItemLine1 = new MenuItem("-");
            MenuItem menuItem1 = new MenuItem("등록 내용 수정 (" + dc.SeniorMasterConvert() + " 권한 부여)");
            MenuItem menuItem2 = new MenuItem("오더 누락 입력 (샘플 제외)");
            MenuItem menuItemLine2 = new MenuItem("-");
            MenuItem menuItem11 = new MenuItem("해당 품번 선택 (품번 '" + _data[1] + "')");
            MenuItem menuItem12 = new MenuItem("동일 내용 검색 (차종 '" + _data[2] + "')");
            MenuItem menuItem13 = new MenuItem("동일 내용 검색 (고객사 EO '" + _data[3] + "')");
            MenuItem menuItem14 = new MenuItem("동일 내용 검색 (모비스 EO '" + _data[4] + "')");
            MenuItem menuItem15 = new MenuItem("동일 내용 검색 (EO 내용 '" + _data[5] + "')");
            MenuItem menuItem16 = new MenuItem("동일 내용 검색 (MAIN PCB '" + _data[14] + "')");
            MenuItem menuItem17 = new MenuItem("동일 내용 검색 (SUB PCB '" + _data[15] + "')");
            MenuItem menuItemLine3 = new MenuItem("-");

            MenuItem menuItem21 = new MenuItem("출하지 수정 (현재 '" + shipmentData + "')");
            //MenuItem menuItem21 = new MenuItem("출하지 수정 (현재 '" + lblShipmentView.Text + "')");

            MenuItem menuItem22 = new MenuItem("출하지 변경이력");
            MenuItem menuItemLine4 = new MenuItem("-");
            MenuItem menuItem31 = new MenuItem("EO 적용 점검 (MAIN PCB '" + _data[14] + "')");
            MenuItem menuItem32 = new MenuItem("EO 적용 점검 (SUB PCB '" + _data[15] + "')");
            MenuItem menuItem33 = new MenuItem("EO 적용 점검 (EO 내용)");

            m.MenuItems.Add(menuItem0);
            m.MenuItems.Add(menuItemLine1);
            m.MenuItems.Add(menuItem1);

            if (_dgv.Rows[_dgv.CurrentCell.RowIndex].Cells[16].Value.ToString() == "")
            {
                m.MenuItems.Add(menuItem2);
            }

            m.MenuItems.Add(menuItemLine2);

            if (pnMain.Visible)
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
            m.MenuItems.Add(menuItem32);
            m.MenuItems.Add(menuItem33);

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
            menuItem32.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 32, _data));
            menuItem33.Click += new EventHandler((sender, e) => ModelCurrentSearch(sender, e, 33, _data));

            // 2021.08.27
            // 조별 선임 권한
            if (dc.CheckSeniorMaster(strUserAddressData[0]))
            {
                menuItem1.Enabled = true;
                menuItem2.Enabled = true;
            }
            else
            {
                menuItem1.Enabled = false;
                menuItem2.Enabled = false;
            }

            m.Show(_dgv, _dgv.PointToClient(Cursor.Position));
        }

        private void TagPrint(object sender, EventArgs e)
        {
            if (pnSelect.Visible) PrintDataSetting(dgvSelect);
            else PrintDataSetting(dgvMain);
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
                        Guna2Msg(this, "오류", "수정할 수 없는 내용입니다");
                        return;
                    }

                    if (_data[9] == "-")
                    {
                        Guna2Msg(this, "오류", "예약으로 등록된 내용은 수정할 수 없습니다.\n삭제 후 다시 등록해주세요.");
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
                        "shipment", _data[12]
                    };
                    string query = dc.SelectDeleteQueryANDConvert(DATABASE_NAME, "model_data", selectData, "SELECT");
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
                    Guna2Msg(this, "오류", "이 기능은 미입력 될 경우 보정할 때 사용하므로 문의 후 사용바랍니다");

                    query = $"SELECT * FROM `packingtoorder`.`" + "log_data" + "` WHERE contents LIKE '%" + _data[1] + "%' AND contents LIKE '%" + _data[5] + "%' ORDER BY write_time";
                    query = query.Replace("\r\n", " "); // 보정 필요
                    string[] resultData2 = mariaDB.SelectQueryCount(query, "write_time");

                    if (resultData2.Length > 0)
                    {
                        DateTime indexDateTime = Convert.ToDateTime(resultData2[0]);

                        query = $"SELECT * FROM `packingtoorder`.`" + "log_data" + "` WHERE write_time < '" + indexDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "' AND write_time > '" + indexDateTime.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") + "' AND contents LIKE '%" + _data[1] + "%'";
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
                                        string conditionQuery = $"WHERE line = '{ cbbLineName01.Text }' AND model_name = '" + _data[1] + "' AND car_name = '" + _data[2] + "' AND customer_eo_number = '" + _data[3] + "' AND mobis_eo_number = '" + _data[4] + "' AND eo_contents = '" + _data[5] + "' AND shipment = '" + _data[12] + "'";
                                        query = $"SELECT * FROM `{ DATABASE_NAME }`.`model_data` " + conditionQuery;
                                        string[,] resultData4 = mariaDB.SelectQuery2(query);

                                        if (resultData4.GetLength(0) == 1)
                                        {
                                            query = $"UPDATE `{ DATABASE_NAME }`.`model_data` SET start_order = '" + spilitContents[5] + "' " + conditionQuery;
                                            mariaDB.EtcQuery(query);
                                            Guna2Msg(this, "알림", "정상적으로 입력되었습니다");
                                        }
                                        else
                                        {
                                            Guna2Msg(this, "오류", "1개 초과 조회 오류 [윤민규 사원 문의]");
                                        }
                                    }
                                    else
                                    {
                                        Guna2Msg(this, "오류", "입력이 취소되었습니다");
                                    }
                                }
                                else
                                {
                                    Guna2Msg(this, "오류", "오더 자리수 불일치");
                                }
                            }
                        }
                        else
                        {
                            Guna2Msg(this, "오류", "검색된 데이터가 없어 자동 입력이 불가합니다 [2]");
                        }
                    }
                    else
                    {
#if DEBUG

#endif
                        Guna2Msg(this, "오류", "검색된 데이터가 없어 자동 입력이 불가합니다 [1]");
                    }
                    break;
                // ---------------------
                //  품번 선택
                // ---------------------
                case 11:
                    // 2
                    if (pnMain.Visible)
                    {
                        cbbProductName.SelectedItem = dgvMain.Rows[dgvMain.CurrentCell.RowIndex].Cells[valueAfterCalibration].Value.ToString();
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

                    if (pnSelect.Visible) ModelSelect(dgvSelect.Rows[dgvSelect.CurrentCell.RowIndex].Cells[valueAfterCalibration].Value.ToString(), valueAfterCalibration - 1);
                    else ModelSelect(dgvMain.Rows[dgvMain.CurrentCell.RowIndex].Cells[valueAfterCalibration].Value.ToString(), valueAfterCalibration - 1);
                    break;
                // ---------------------
                //  동일 내용 검색 (M/PCB, S/PCB)
                // ---------------------
                case 16:
                case 17:
                    // 2022.06.30
                    // 콤보 박스 선택 값 초기화
                    cbbProductName.SelectedIndex = -1;

                    if (pnSelect.Visible) ModelSelect(dgvSelect.Rows[dgvSelect.CurrentCell.RowIndex].Cells[valueAfterCalibration + 7].Value.ToString(), valueAfterCalibration - 1);
                    else ModelSelect(dgvMain.Rows[dgvMain.CurrentCell.RowIndex].Cells[valueAfterCalibration + 7].Value.ToString(), valueAfterCalibration - 1);
                    break;
                // ---------------------
                case 21:
                    string shipmentData = pe.NowShipment(DATABASE_NAME, cbbLineName.Text, _data[1]);
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
                    // 
                case 31:
                case 32:
                    string[] productData = new string[2];

                    if (_data[16] != "MAIN PCB" && _data[16] != "SUB PCB")
                    {
                        Guna2Msg(this, "오류", "EO 구분이 MAIN PCB 또는 SUB PCB만 조회할 수 있습니다");
                        return;
                    }

                    if (_type == 31)
                    {
                        productData[0] = _data[14];
                        productData[1] = $"MAIN PCB";
                    }
                    else 
                    {
                        productData[0] = _data[15];
                        productData[1] = $"SUB PCB";
                    }

                    EOCheckPCBForm_M eOCheckPCBForm_M = new EOCheckPCBForm_M(productData);
                    eOCheckPCBForm_M.Show();
                    break;
                case 33:
                    if (_data[16] != "MAIN PCB" && _data[16] != "SUB PCB")
                    {
                        Guna2Msg(this, "오류", "EO 구분이 MAIN PCB 또는 SUB PCB만 조회할 수 있습니다");
                        return;
                    }

                    EOCheckContentsForm_M eOCheckContentsForm_M = new EOCheckContentsForm_M(_data);
                    eOCheckContentsForm_M.Show();
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
            if (pnSelect.Visible)
            {
                ExportToExcel(dgvSelect);
            }
            else if (pnMain.Visible && dgvMain.Rows.Count > 0)
            {
                ExportToExcel(dgvMain);
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
            rfidForm.LineData = $"A86";
            rfidForm.InstanceForm = this;
            rfidForm.StartForm();
            */

            EOForm eOForm = new EOForm();
            eOForm.ShowDialog();

            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = $"mht files (*.mht)|*.mht";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == string.Empty)
                return;

            ViewForm viewForm = new ViewForm(openFileDialog.FileName);
            viewForm.ShowDialog();
            */
#endif
        }

        private void btnShipmentMaster_Click(object sender, EventArgs e)
        {

        }

        private void btnShipmentChange_Click(object sender, EventArgs e)
        {
            OrderMasterForm_M orderMasterForm = new OrderMasterForm_M();

            Opacity = 0;

            orderMasterForm.Owner = this;
            orderMasterForm.ShowDialog();

            Opacity = 1;
        }

        private void cbbProductName_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbbLineName_SelectedIndexChanged(object sender, EventArgs e)
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
                    cbbFloor01.Visible = true;
                }
                else
                {
                    cbbFloor01.Visible = false;
                }
            }

            // 2020.09.04
            // 자동 뷰
            BtnProductUpdate.PerformClick();

            // 2022.01.06
            // 출하지 마스터 활성화
            if (cbbLineName01.Text == "D-오디오 조립")
            {
                grpShipment.Visible = true;
                grpEOCheck.Visible = true;
                //btnShipmentMaster.Visible = true;
                //btnShipmentChange.Visible = true;
                pnFpiCheck.Visible = true;
            }
            else
            {
                grpShipment.Visible = false;
                grpEOCheck.Visible = false;
                //btnShipmentMaster.Visible = false;
                //btnShipmentChange.Visible = false;
                pnFpiCheck.Visible = false;
            }
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

        private void lblFpiCheck_Click(object sender, EventArgs e)
        {
            FpiCheckForm fpiCheckForm = new FpiCheckForm();
            fpiCheckForm.ShowDialog();
        }

        private void 실험실ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestForm testForm = new TestForm();
            testForm.Show();
        }

        private void btnEOPCB_Click(object sender, EventArgs e)
        {
            EOCheckPCBForm_M eOCheckPCBForm_M = new EOCheckPCBForm_M();
            eOCheckPCBForm_M.Show();
        }

        private void btnEOSearch_Click(object sender, EventArgs e)
        {
            EOCheckContentsForm_M eOCheckContentsForm_M = new EOCheckContentsForm_M();
            eOCheckContentsForm_M.Show();
        }

        private void btnNewEO_Click(object sender, EventArgs e)
        {
            EOForm eoForm = new EOForm();
            eoForm.ShowDialog();
        }

        /*
        bool process = false;

        /// <summary>
        /// FTP 파일 다운로드
        /// </summary>
        /// <param name="_serverPath"></param>
        /// <param name="_localPath"></param>
        public void FtpWebClientDownload(string _serverPath, string _localPath)
        {
            WebClientPlus wc = new WebClientPlus();

            wc.Credentials = new NetworkCredential("user", "autonics12#");
            //wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
            wc.DownloadFileAsync(new Uri(_serverPath), _localPath);

            while (process)
            {
                dc.LogFileSave("대기..");

                System.Windows.Forms.Application.DoEvents();
            }

            wc.Dispose();
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            dc.LogFileSave("완료");

            process = false;
        }
        */

        private void ResetInactivityTimer()
        {
            timer3.Stop();  // 타이머 재설정
            timer3.Start();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            ResetInactivityTimer();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            ResetInactivityTimer();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            ResetInactivityTimer();
        }

        private void chkReservationView_CheckedChanged(object sender, EventArgs e)
        {
            BtnProductUpdate.PerformClick();
        }

        private void cbbFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnProductUpdate.PerformClick();
        }

        private void lblShipmentView_VisibleChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(lblShipmentView.Visible.ToString());
        }

        private void dataGridView1_VisibleChanged(object sender, EventArgs e)
        {
            if (pnSelect.Visible)
            {
                pnMain.Visible = false;

                // 2022.06.30
                // 출하지 텍스트 출력
                if (cbbLineName.Text == "D-오디오 조립")
                {
                    //pnShipmentView.Visible = true;
                    //pnFpiCheck.Visible = true;
                }
            }
            else
            {
                lblShipmentView.Text = $"-";
                //pnShipmentView.Visible = false;
                //pnFpiCheck.Visible = false;
            }
        }

        private void pnMain_VisibleChanged(object sender, EventArgs e)
        {
            if (pnMain.Visible)
            {
                pnSelect.Visible = false;

                btnProductDelete.Enabled = false;
            }
            else
            {
                btnProductDelete.Enabled = true;
            }
        }
    }
}
