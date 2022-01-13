using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Data.SQLite;
using System.Data;

//앞으로 모든 실행을 담당할 메인 컨트롤러

namespace Stomach
{
    class Controller
    {

        Client _client = new Client();

        Dictionary<string, int> grid;
        Dictionary<string, int> grid2; // 위 관찰 시작 지점을 따로 저장할수 있는 두번째 그리드 딕셔너리
        Stopwatch _sw = new Stopwatch();
        Stopwatch _stw = new Stopwatch();
        Boolean _isStart = false; // 위 관찰이 시작되었는지 확인하는 변수
        public Boolean _isEnd = false; // 위 관찰이 종료되었는지 확인하는 변수
        public int _s3Count = 0;
        int _gridCount = 0; //grid 증가를 체크하는 변수
        int _gridCount2 = 0; //grid[S1] ~ grid[S6] 까지 값이 들어왔는지 확인하기 위한 변수
        String[] check_class = { "E", "S1", "S2", "S3", "S4", "S5", "D1", "D2", "S6", "X" };
        String gridMessage = "";
        Boolean _gridCheck = false;//grid[S1] ~ grid[S6] 까지 값이 전부 들어왔다면 true를 반환함
        Boolean _gridCheck2 = false;//위가 전부 찍힌 이후 E가 찍히는 지를 반환함
        int test = 0;
        int pictureCount = 0;
        bool[] isCount = new bool[10];
        public delegate Dictionary<String, int> myfunc(DataGrid dataGrid, Packet packet);
        PictureBox p1 = null;
        PictureBox p2 = null;
        TimeSpan ts = new TimeSpan();
        TimeSpan sts = new TimeSpan();
        
        string caseID = "";
        Boolean biopsy_flag = false; //클래스 c 여부 

        DBAdapter dbAdapter;

        public Controller()
        {
           
            //util.cs 의 TCP 연결을 사용하기위해 호출
            //_client = new Client();

            _client.incomingData = bindPacket;
            _client.gridData = addDataGrid;
            grid = new Dictionary<string, int>();
            grid2 = new Dictionary<string, int>();
            for (int i = 0; i < check_class.Length; i++)
            {
                grid.Add(check_class[i], 0);
                grid2.Add(check_class[i], 0);
                isCount[i] = false;
            }
            p1 = Main._f.pictureBox1;
            p2 = null;
           
        }
        public void setDBAdapter(DBAdapter dbadapter)
        {
            dbAdapter = dbadapter;

        }
        /* 프로그램의 X 버튼을 눌렀을 때 발생하는 이벤트 */
        public void x_Close()
        {
            try
            {
                if (_client.tcpClient != null)
                {
                    Byte[] sendBytes = Encoding.UTF8.GetBytes("stop\r\n");
                    _client.tcpClient.GetStream().BeginWrite(sendBytes, 0, sendBytes.Length, stop_tcp, null);
                }

                // Action that might throw an exception
                throw new Exception("Some exception");
            }
            catch (Exception ex)
            {
                OnClosingException = ex;
            }
        }

       public Exception OnClosingException { get; protected set; }


        private void stop_tcp(IAsyncResult ar)
        {
            if (ar.IsCompleted)
            {
                _client.tcpClient.Close();
            }
        }


        /* 프로그램과 클라이언트 서버를 연결하는 부분 */
        public void mainTask(string ipaddress, int port)
        {
            try
            {
                //util.cs에서 TCP 연결을 위한 메소드

                //DataGrid._g.SetRowData();
                //_sw.Start();
                if (_client.Initalize(ipaddress, port))
                    _client.BeginRead();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /* 이미지 받아오는 이벤트 부분 */
        private void bindPacket(Packet packet, int count)
        {

            ImageDisplay form = new ImageDisplay();
            string tag = packet.GetTag();
            form.pBimage.Image = Image.FromFile(packet.GetPath());
            form.pBimage.Left = 10;
            form.pBimage.Top = 10;
            form.iTag.Text = packet.GetTag() + "\t  " + packet.GetProb();
            form.fileName.Text = packet.GetPath();
           
            
            if (packet.GetProb().Contains("C"))
            {
                biopsy_flag = true;
            }

            /* 
             * valid check
             * 
             */

            caseID = Path.GetFileName(Path.GetDirectoryName(packet.GetPath()));
            
            if (Main._f._adminFlag)
            {
                form.iTag.Text = packet.GetTag() + "\t  " + packet.GetProb();
                if (form.iTag.Text.Contains("X"))
                {
                    form.iTag.Left = 35;
                }
                else
                {
                    form.iTag.Left = 65;
                }
            }
            else
            {
                form.iTag.Text = packet.GetTag();
                form.iTag.Left = 90;
            }
           
            Main._f.BeginInvoke((Action)(() =>
            {
                TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition(count, 0);
                Main._f.imageDisplayLayout.SetCellPosition(form, pos);
                Main._f.imageDisplayLayout.Controls.Add(form);

                Main._f.imageDisplayLayout.ScrollControlIntoView(form);

                /******************************************************
                 이미지 출력시 위 이미지 변경 효과 이벤트 부분        */
                for (int i = 0; i < check_class.Length; i++)
                {
                    if (grid[check_class[i]] == 1 && isCount[i] == false)
                    {
                        switch (check_class[i])
                        {
                            case "E":
                                p2 = Main._f.pic0;
                                break;
                            case "S1":
                                p2 = Main._f.pic1;
                                break;
                            case "S2":
                                p2 = Main._f.pic2;
                                break;
                            case "S3":
                                p2 = Main._f.pic2;
                                break;
                            case "S4":
                                p2 = Main._f.pic3;
                                break;
                            case "S5":
                                p2 = Main._f.pic4;
                                break;
                            case "D1":
                                p2 = Main._f.pic5;
                                break;
                            case "D2":
                                p2 = Main._f.pic6;
                                break;
                        }
                        addImage(p1, p2);

                        isCount[i] = true;
                        if (check_class[i] == "S2" || check_class[i] == "S3")
                        {
                            isCount[2] = true;
                            isCount[3] = true;
                        }
                        p1 = p2;
                        pictureCount++;
                    }
                }
                /**********************************************************/


                /*위 관찰 시작*/
                if (grid["D1"] > 0 || grid["D2"] > 0)
                {
                    if (packet.GetTag() != "D1" && packet.GetTag() != "D2" && packet.GetTag() != "E" && packet.GetTag() != "X" && packet.GetTag() != "S6" && !_isStart)
                    {

                        Main._f.statusImage.Image = Properties.Resources._2_start;
                        form.BackColor = Color.Red;
                        _isStart = true;

                        for (int i = 0; i < 5; i++)
                        {
                            grid2["S" + (i + 1)] = 0;
                        }
                        grid2[packet.GetTag()]++;

                    }
                }
                /*****************************************************/

                /* _isStart 라는 시작 체크하는 변수가 TRUE 일경우에 E가 출력되면 위관찰 종료 시키는 이벤트*/
                for (int i = 1; i < 6; i++)
                {
                    if (grid2[check_class[i]] > 0 && _isStart)
                    {
                        _gridCount2++;
                        if (_gridCount2 == 5)
                        {
                            _gridCheck = true;
                        }
                    }
                    if (i == 5 && _gridCount2 < 5)
                    {
                        _gridCount2 = 0;
                    }
                }

                if (!_isEnd && _isStart)
                {
                    if (packet.GetTag() == "E")
                    {
                        _gridCheck2 = true;
                    }
                }
                /********************************************************************************************/

                /*위 관찰이 정상적으로 종료되었다면 gridCheck2 가 TRUE 로 활성화 되며 위 종료를 시키는 이벤트*/
                if (_gridCheck2 && !_isEnd && _isStart)
                {
                    Main._f.statusImage.Image = Properties.Resources._3_end;
                    form.BackColor = Color.Blue;
                    _isEnd = true;
                }
                /*******************************************************************************************/


                /* _gridCount 는 전체 시간을 측정하기위한 변수이며 출력된 총 사진의 개수를 의미한다*/
                for (int i = 0; i < check_class.Length; i++)
                {
                    _gridCount = _gridCount + grid[check_class[i]];
                }

                /*만약 gridCount가 1 즉 이미지가 한장이 나오면 전체시간 SW가 시작된다*/
                if (_gridCount == 1)
                {
                    _sw.Start();
                }

                /*이후로 이미지가 추가로 한장씩 출력될때마다 시간을 계속해서 책정하며 최종적으로는 마지막 이미지의 시간이 저장된다*/
                if (_gridCount > 1)
                { 

                    ts = _sw.Elapsed;
                    /*
                     * 이미지가 찍힌 시간 저장 
                     */
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds
                                                );
                }

                /*위 관찰시간을 측정하는 이벤트 부분으로 만약 십이지장이 출력된 이후 위 부위가 출력되면 STW 를 시작한다*/
                if (grid["D1"] > 0 || grid["D2"] > 0)
                {
                    if (packet.GetTag() == "S1" || packet.GetTag() == "S2" || packet.GetTag() == "S3" || packet.GetTag() == "S4" || packet.GetTag() == "S5")
                    {
                        _stw.Start();
                    }
                }
                /*이후에 식도가 출력되면 위관찰 시간은 종료된다*/
                if (_gridCheck2 && grid["E"] > 0)
                {
                    _stw.Stop();
                    
                    sts = _stw.Elapsed;

                    string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}",
                                                sts.Hours, sts.Minutes, sts.Seconds
                                                );

                    Main._f.stomachTimeView.Rows[0].Cells[0].Value = elapsedTime2;

                }
                
                /*위관찰이 종료된 이후에도 만일 위가 계속 관찰된다면 위 관찰시간은 이후에 측정된 시간을 계속해서 더해준다*/
                if (_isEnd)
                {
                    if (packet.GetTag() == "S1" || packet.GetTag() == "S2" || packet.GetTag() == "S3" || packet.GetTag() == "S4" || packet.GetTag() == "S5")
                    {
                        _stw.Start();

                        sts = _stw.Elapsed;

                        string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}",
                                                    sts.Hours, sts.Minutes, sts.Seconds
                                                    );
                    }
                }

                /* 위관찰이 완료되지않았는데 E(식도가 출력되면 메세지 출력하는 메소드)
                //if (grid["D1"] > 0 && grid["D2"] > 0)
                //{
                //    if (grid["E"] > 0)
                //    {
                //        for (int i = 1; i < 6; i++)
                //        {
                //            if (grid[check_class[i]] == 0 && test == 0)
                //            {
                //                test = 1;
                //                AutoClosingMessageBox.Show("아직 위가 전부 관측되지않았습니다.", "주의", 3000);
                //            }
                //        }
                //    }
                //}
                //else if (grid["D1"] == 0 || grid["D2"] == 0)
                //{
                //    if (grid["E"] > 0)
                //    {
                //        for (int i = 1; i < 6; i++)
                //        {
                //            if (grid[check_class[i]] == 0 && test == 0)
                //            {
                //                test = 1;
                //                AutoClosingMessageBox.Show("아직 위가 전부 관측되지않았습니다.", "주의", 3000);
                //            }
                //        }
                //    }
                //}
                */

            }));

        }


        /*프로그램 datagrid를 수정해주는 이벤트 부분*/
        public void addDataGrid(Packet packet)
        {
            object[] myArray = new object[2];
            myArray[0] = DataGrid._g;
            myArray[1] = packet;
            IAsyncResult i_res = Main._f.BeginInvoke(new myfunc(processingGrid), myArray);

            object grid_object = Main._f.EndInvoke(i_res);
            grid = grid_object as Dictionary<String, int>;
        }

        public Dictionary<String, int> processingGrid(DataGrid dataGrid, Packet packet)
        {

            /*출력된 부분은 Count를 증가시키고 폰트 색상은 검은색으로 변경해주는 이벤트 부분*/
            for (int i = 0; i < dataGrid.dataGridView1.RowCount; i++)
            {
                if (dataGrid.dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(packet.GetTag()))
                {

                    grid[packet.GetTag()]++;
                    grid2[packet.GetTag()]++;

                    dataGrid.dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                    dataGrid.dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Black;
                    dataGrid.dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                    dataGrid.dataGridView1.Rows[i].Cells[2].Value = grid[packet.GetTag()];
                }
                dataGrid.dataGridView1.Rows[i].Cells[2].Value = grid[check_class[i]];
            }
            return grid;

        }
        
        
        /*프로그램 일시정지시 발생하는 이벤트 =>  data 기록 이벤트 */
        public void pause_control(bool is_pause)
        {


            DataGrid datagrid = DataGrid._g;

            DateTime recordDate = DateTime.Now;
            string sqlFormattedDate = recordDate.ToString("yyyy-MM-dd");
            Console.WriteLine(sqlFormattedDate);

            StringBuilder insertSql = new StringBuilder();

            insertSql.Append("'" + caseID + "','" + sqlFormattedDate + "','" + Main._f.totalTimeView.Rows[0].Cells[0].Value + "','" + Main._f.stomachTimeView.Rows[0].Cells[0].Value +
                "'," + biopsy_flag);


            for (int i = 0; i < datagrid.dataGridView1.RowCount; i++)
            {
                insertSql.Append("," + datagrid.dataGridView1.Rows[i].Cells[2].Value);
            }


            try
            {
                dbAdapter.Insert("record_stomach", insertSql.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("동일한 케이스가 저장되어 있습니다. ");
            }

           

            //try
            //{
                
               
                
                
            //    if (_client.tcpClient != null && _client.tcpClient.Connected)
            //    {

            //        //if (!is_pause)
            //        //{
            //        //    string xml = "pause\r\n";
            //        //    _client.BeginSend(xml);
            //        //    Main._f._is_Pause = true;
            //        //    _sw.Stop();
            //        //    _stw.Stop();

            //        //}
            //        //else
            //        //{
            //        //    string xml = "continue\r\n";
            //        //    _client.BeginSend(xml);
            //        //    Main._f._is_Pause = false;
            //        //    _sw.Start();
            //        //    _stw.Start();
            //        //}

            //        //Main._f.pauseInit();

            //    }
            //    else
            //    {
            //        MessageBox.Show("프로그램이 실행된 이후에 이용가능합니다.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("start 버튼을 눌러주세요.");
            //}
        }
        /******************************************************************************/

        /*프로그램 정지시 발생하는 이벤트*/
        public void stop_control()
        {

            //dbAdapter.DeleteAll("record_stomach");

            DataGrid datagrid = DataGrid._g;
            try
            {
                if (_client._connectFlag)
                {
                    /*위관찰이 종료되었을때 프로그램 정지가 가능하게 함*/
                    if (_isEnd)
                    {
                        string query = "stop\r\n";
                       
                        
                        _client.BeginSend(query);
                        _client._imgCount = 0;
                        
                        /**************************************************************/

                        /*전체관찰시간의 경우 사용자가 STOP을 누르면 출력되게끔 진행함*/
                        _sw.Stop();

                        //TimeSpan ts = _sw.Elapsed;

                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                                                    ts.Hours, ts.Minutes, ts.Seconds
                                                    );


                        Main._f.totalTimeView.Rows[0].Cells[0].Value = elapsedTime; // 나중에 수정예정, 

                        /*
                         * 데이터그리드와 이미지 디스플레이를 전부 초기화함
                            Main._f.imageDisplayLayout.Controls.Clear();
                            DataGrid._g.dataGridView1.Rows.Clear();
                            DataGrid._g.SetRowData();
                            Main._f.statusImage.Image = Properties.Resources._4_complete;
                        */
                        /*************************************************/

                        /*기존에 프로그램이 실행되면서 쌓인 데이터들을 전부 초기화해줌*/

                        for (int i = 0; i < check_class.Length; i++)
                        {
                            grid[check_class[i]] = 0;
                            grid2[check_class[i]] = 0;
                            isCount[i] = false;
                        }

                        _isStart = false; _isEnd = false;
                        _gridCheck = false;
                        _gridCheck2 = false;
                        _gridCount = 0;
                        _gridCount2 = 0;
                        test = 0;
                        pictureCount = 0;
                        biopsy_flag = false;

                        Main._f._is_Pause = false;
                        Main._f.pause_btn.Text = "SAVE";
                        Main._f.imageDisplayLayout.AutoScrollPosition = new Point(0, 0);
                        Main._f.pic0.Visible = false;
                        Main._f.pic1.Visible = false;
                        Main._f.pic2.Visible = false;
                        Main._f.pic3.Visible = false;
                        Main._f.pic4.Visible = false;
                        Main._f.pic5.Visible = false;
                        Main._f.pic6.Visible = false;

                        p1 = Main._f.pictureBox1;
                        p2 = null;
                        

                        //_client.tcpClient.Close();
                        _client.stopIs = true;
                        _client._connectFlag = false;


                        _sw.Reset();
                        _stw.Reset();

                        _client.tcpClient = null;
                    }

                    /*위 관찰이 완료되지 않았을경우 사용자에게 정말 프로그램 종료 여부를 묻는 메세지 전송 */
                    else
                    {
                        if (MessageBox.Show("아직 위 관찰이 완료되지않았습니다. 그래도 종료하시겠습니까?", "종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            _isEnd = true;
              
                            stop_control();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("서버가 연결되었는지 확인해주세요");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*일시정지 후 사용자가 태그 변경시 태그를 변경시켜주는 이벤트*/
        
        public void changeTag(object pbP)
        {
            Control c = pbP as Control;
            var radioButtons = Main._f.groupBox1.Controls.OfType<RadioButton>();

            foreach (Control con in Main._f.groupBox1.Controls)
            {
                foreach (Control cont in c.Controls)
                {
                    c.BackColor = Color.Gray;
                    if (cont.Name == "iTag")
                    {
                        Label tag_labe = cont as Label;

                        if (con.GetType() == typeof(RadioButton))
                        {
                            RadioButton rb = con as RadioButton;
                            if (rb.Checked)
                            {
                                string t = rb.Name.Replace("_button", "");
                                string[] token = tag_labe.Text.Split('\t');

                                if (token[0] != "X" || token[0] != "S6")
                                    grid[token[0]]--;
                                grid[t]++;
                                tag_labe.Text = t;
                                tag_labe.ForeColor = Color.Red;
                                tag_labe.Left = 90;
                            }
                        }
                    }
                }
            }
        }

        /* 프로그램 상단의 필터를 변경하면 이미지 디스플레이에 해당 필터에 해당하는 부위만 보이게해주는 이벤트*/
        public void changeFilter(string idx)
        {

            ImageDisplay form = new ImageDisplay();


            foreach (ImageDisplay id in Main._f.imageDisplayLayout.Controls)
            {

                string[] token = id.iTag.Text.Split('\t');


                if (idx == "ALL")
                {
                    TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition(_s3Count, 0);
                    Main._f.imageDisplayLayout.SetCellPosition(id, pos);
                    _s3Count++;
                    id.Visible = true;
                }
                else if (token[0] != idx)
                {

                    id.Visible = false;

                }
                else if (token[0] == idx)
                {
                    TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition(_s3Count, 0);
                    Main._f.imageDisplayLayout.SetCellPosition(id, pos);
                    _s3Count++;
                    id.Visible = true;
                    Main._f.imageDisplayLayout.AutoScrollPosition = new Point(0, 0);
                }

            }
            _s3Count = 0;

        }

        /*프로그램 시작 시 발생하는 메소드이며 프로그램을 초기화 해주는 이벤트*/
        public void updateMain()
        {
            if (_client.tcpClient != null)
            {
                MessageBox.Show("이미 프로그램이 실행중입니다.");
            }
            else
            {

                _client._imgCount = 0;

                Main._f.imageDisplayLayout.Controls.Clear();
                DataGrid._g.dataGridView1.Rows.Clear();
                DataGrid._g.SetRowData();

                for (int i = 0; i < check_class.Length; i++)
                {
                    grid[check_class[i]] = 0;
                    grid2[check_class[i]] = 0;
                }

                _isStart = false; _isEnd = false;
                _gridCheck = false;
                _gridCount = 0;
                _gridCount2 = 0;
                test = 0;

                Main._f._is_Pause = false;
                Main._f.pause_btn.Text = "SAVE";
                //_client.tcpClient.Close();
                _client.stopIs = true;
                _client._connectFlag = false;

                _sw.Reset();
                _stw.Reset();

                Main._f.totalTimeView.Rows[0].Cells[0].Value = null;
                Main._f.stomachTimeView.Rows[0].Cells[0].Value = null;
            }
        }
        private string ConvertToCSV(DataSet objDataSet)
        {
            StringBuilder content = new StringBuilder();

            if (objDataSet.Tables.Count >= 1)
            {
                DataTable table = objDataSet.Tables[0];

                if (table.Rows.Count > 0)
                {
                    DataRow dr1 = (DataRow)table.Rows[0];
                    int intColumnCount = dr1.Table.Columns.Count;
                    int index = 1;

                    //add column names
                    foreach (DataColumn item in dr1.Table.Columns)
                    {
                        content.Append(String.Format("\"{0}\"", item.ColumnName));
                        if (index < intColumnCount)
                            content.Append(",");
                        else
                            content.Append("\r\n");
                        index++;
                    }

                    //add column data
                    foreach (DataRow currentRow in table.Rows)
                    {
                        string strRow = string.Empty;
                        for (int y = 0; y <= intColumnCount - 1; y++)
                        {
                            strRow += "\"" + currentRow[y].ToString() + "\"";

                            if (y < intColumnCount - 1 && y >= 0)
                                strRow += ",";
                        }
                        content.Append(strRow + "\r\n");
                    }
                }
            }

            return content.ToString();
        }
        /*사용자가 일시정지 후 세이브버튼을 누르면 발생하는 이벤트*/
        public void saveTag(bool is_pause)
        {
            /*
             * 
             * 
             * */


            /*
                       * data record  db save 
                       * 
                       *  케이스 id, 날짜, 전체 시간, 위 촬영시간, 클래스 c 여부(조직검사여부), 나머지 클래스 각 개수 
                       *  caseID,  날짜 구하기, elapsedTime, Main._f.stomachTimeView.Rows[0].Cells[0].Value, biopsy_flag
                       *  
                       *  Console.WriteLine(datagrid.dataGridView1.Rows[i].Cells[1].Value); 클래스 이름 
                          Console.WriteLine(datagrid.dataGridView1.Rows[i].Cells[2].Value); 개수 


                          //insert, select, export csv 파일 추출 

             */

   
            DateTime recordDate = DateTime.Now;
            string sqlFormattedDate = recordDate.ToString("yyyy-MM-dd");


            Console.WriteLine(sqlFormattedDate);


            DataSet ds = dbAdapter.SelectAll("record_stomach");
            Console.WriteLine(ds.GetXml());
            string result = ConvertToCSV(ds);
            Console.WriteLine(result);


            try
            {
                string file_name = $"./result_{sqlFormattedDate}.csv";
                File.WriteAllText(file_name, result);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }


            //ImageDisplay form = new ImageDisplay();
            //System.Text.StringBuilder packet_string = new System.Text.StringBuilder();
            //packet_string.Append("save\t");


            //if (is_pause)
            //{

            //    foreach (ImageDisplay id in Main._f.imageDisplayLayout.Controls)
            //    {
            //        string fileN = id.fileName.Text;
            //        string[] token = id.iTag.Text.Split('\t');
            //        packet_string.Append(fileN + "\t" + token[0] + "\t");

            //    }
            //    _client.BeginSend(packet_string.ToString());
            //}
            //else
            //{
            //    MessageBox.Show("프로그램을 일시정지 후 사용해 주세요!");
            //}
        }
        
        public void addImage(PictureBox parentBox, PictureBox childBox)
        {
            childBox.Parent = parentBox;
            childBox.Visible = true;
        }

    }
}
