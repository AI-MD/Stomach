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
        public static Controller _con;

        Client _client = new Client();

        Dictionary<string, int> grid;
        Dictionary<string, int> grid2; // 위 관찰 시작 지점을 따로 저장할수 있는 두번째 그리드 딕셔너리
        Stopwatch _sw = new Stopwatch();
        Stopwatch _stw = new Stopwatch();
        Boolean _isStart = false; // 위 관찰이 시작되었는지 확인하는 변수
        public Boolean _isEnd = false; // 위 관찰이 종료되었는지 확인하는 변수
        public Boolean _isRunningFlag = false;
        public Boolean _isSaveFlag = false;
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
        public delegate Dictionary<String, int> myfunc(DataGrid dataGrid, Packet packet, Boolean ai_flag);
        PictureBox p1 = null;
        PictureBox p2 = null;
        TimeSpan ts = new TimeSpan();
        TimeSpan sts = new TimeSpan();
        long current_id = -1; 
        string caseID = "";
        Boolean biopsy_flag = false; //클래스 c 여부 
       
        Boolean is_Pause = false;
        int x_count = 0;
        int s6_count = 0; 
        DBAdapter dbAdapter;
        public String current_user = "";
        public Controller()
        {
            _con = this;

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
                if (Main._f.user.Text.Equals("USER"))
                {
                    if (current_user.Equals(""))
                    {
                        MessageBox.Show("사용자를 선택하세요");
                        return;
                    }
                }


                if(Main._f.ai_flag)
                {
                    Main._f.checkBox2.Checked = true;
                    Main._f.checkBox2.Text = "인공지능 ON";
                    Main._f.checkBox2.BackColor = Color.Red;
                    Main._f.checkBox2.ForeColor = Color.White;
                }
                else
                {
                    Main._f.checkBox2.Checked = false;
                    Main._f.checkBox2.Text = "인공지능 OFF";
                    Main._f.checkBox2.BackColor = Color.Blue;
                    Main._f.checkBox2.ForeColor = Color.White;
                }
                if(Main._f.sedative_flag)
                {
                    Main._f.checkBox1.Checked = true;
                    Main._f.checkBox1.Text = "진정내시경 Yes";
                    Main._f.checkBox1.BackColor = Color.Red;
                    Main._f.checkBox1.ForeColor = Color.White;
                }
                else
                {

                    Main._f.checkBox1.Checked = false;
                    Main._f.checkBox1.Text = "진정내시경 NO";
                    Main._f.checkBox1.BackColor = Color.Blue;
                    Main._f.checkBox1.ForeColor = Color.White;

                }


                if (_client.Initalize(ipaddress, port))

                {
                    _isRunningFlag = true;
                    _client.BeginRead();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveTimeInfo(ImageDisplay form)
        {
            ts = _sw.Elapsed;

            /*
            * 이미지가 찍힌 시간 저장 
            */
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                                        ts.Hours, ts.Minutes, ts.Seconds
                                        );


            form.time_info.Text = elapsedTime;
        }

        private void setPicStomach(string class_name)
        {
            switch (class_name)
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
            
            
            form.iTag.Text = packet.GetTag() + "\t  " + packet.GetProb();
            if (form.iTag.Text.Contains("X"))
            {
                form.iTag.Left = 35;
                x_count = x_count + 1;
            }
            else if (form.iTag.Text.Contains("S6"))
            {

                form.iTag.Left = 35;
                s6_count = s6_count + 1;
            }
            else
            {
                form.iTag.Left = 65;
             }
            
           
            Main._f.BeginInvoke((Action)(() =>
            {
                TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition(count, 0);
                Main._f.imageDisplayLayout.SetCellPosition(form, pos);
                Main._f.imageDisplayLayout.Controls.Add(form);

                Main._f.imageDisplayLayout.ScrollControlIntoView(form);

                /******************************************************
                 이미지 출력시 위 이미지 변경 효과 이벤트 부분        */
                
                if(Main._f.ai_flag)
                {
                    for (int i = 0; i < check_class.Length; i++)
                    {
                        if (grid[check_class[i]] == 1 && isCount[i] == false)
                        {
                            setPicStomach(check_class[i]);
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
                }
               
                /**********************************************************/


                /*위 관찰 시작*/
                if (grid["D1"] > 0 || grid["D2"] > 0)
                {
                    if (packet.GetTag() != "C" && packet.GetTag() != "D1" && packet.GetTag() != "D2" && packet.GetTag() != "E" && packet.GetTag() != "X" && packet.GetTag() != "S6" && !_isStart)
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
               
                _gridCount++;
                Console.WriteLine("test" + _gridCount);

                /*만약 gridCount가 1 즉 이미지가 한장이 나오면 전체시간 SW가 시작된다*/
                if (_gridCount == 1)
                {
                    _sw.Start();

                    saveTimeInfo(form);

                }

                /*이후로 이미지가 추가로 한장씩 출력될때마다 시간을 계속해서 책정하며 최종적으로는 마지막 이미지의 시간이 저장된다*/
                if (_gridCount > 1)
                { 

                    ts = _sw.Elapsed;

                    /*
                     * 이미지가 찍힌 시간 저장 
                     */
                    saveTimeInfo(form);
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
            object[] myArray = new object[3];
            myArray[0] = DataGrid._g;
            myArray[1] = packet;
            myArray[2] = Main._f.ai_flag;

            IAsyncResult i_res = Main._f.BeginInvoke(new myfunc(processingGrid), myArray );

            object grid_object = Main._f.EndInvoke(i_res);
            grid = grid_object as Dictionary<String, int>;
        }

        public Dictionary<String, int> processingGrid(DataGrid dataGrid, Packet packet, Boolean ai_flag)
        {
            if(ai_flag)
            {
                /*출력된 부분은 Count를 증가시키고 폰트 색상은 검은색으로 변경해주는 이벤트 부분*/
                for (int i = 0; i < dataGrid.dataGridView1.RowCount; i++)
                {
                    if (packet.GetTag().Equals("C"))
                    {
                        biopsy_flag = true;
                    }

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
           
            else
            {
                if (packet.GetTag().Equals("C"))
                {
                    biopsy_flag = true;
                }
                else
                {
                    grid[packet.GetTag()]++;
                }
 
                return grid;
            }
           

        }
        
        
        /*프로그램 일시정지시 발생하는 이벤트 =>  data 기록 이벤트 */
        public void save_control()
        {

            _isSaveFlag = true;
            
            DataGrid datagrid = DataGrid._g;

            DateTime recordDate = DateTime.Now;
            string sqlFormattedDate = recordDate.ToString("yyyy-MM-dd");
            Console.WriteLine(sqlFormattedDate);

            StringBuilder insertSql = new StringBuilder();

           
          
            string total_time = "";
            string stomach_time = "";

            if (Main._f.totalTimeView.Rows[0].Cells[0].Value != null)
                total_time = Main._f.totalTimeView.Rows[0].Cells[0].Value.ToString();
            if (Main._f.stomachTimeView.Rows[0].Cells[0].Value != null)
                stomach_time = Main._f.stomachTimeView.Rows[0].Cells[0].Value.ToString();

            insertSql.Append("'" + caseID + "','" + sqlFormattedDate + "','" + total_time + "','" + stomach_time +
                "'," + biopsy_flag );
            if (_client._imgCount > 0)
            {
                for (int i = 0; i < datagrid.dataGridView1.RowCount; i++)
                {
                    insertSql.Append("," + grid[check_class[i]]);
                }

            }
            else
            {
                for (int i = 0; i < datagrid.dataGridView1.RowCount; i++)
                {
                    insertSql.Append("," + 0);
                }
            }

            insertSql.Append("," + x_count);
            insertSql.Append("," + s6_count);


            insertSql.Append("," + Main._f.sedative_flag);
            insertSql.Append("," + Main._f.ai_flag);
            insertSql.Append(",'" + current_user + "'");
          
            try
            {
                //MessageBox.Show(insertSql.ToString());

                if(current_id > -1)
                {
                    if (MessageBox.Show("동일한 케이스가 저장되어 있습니다. 현재 기록으로 수정하시겠습니까?", "종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        StringBuilder udateSql = new StringBuilder();
                        try
                        {
                            udateSql.Append("all_time ='" + total_time + "',");
                            udateSql.Append("stomach_time ='" + stomach_time + "'");

                            for (int i = 0; i < datagrid.dataGridView1.RowCount; i++)
                            {
                                udateSql.Append("," + check_class[i] + " = " + grid[check_class[i]]);
                            }
                            udateSql.Append(", X = " + x_count);
                            udateSql.Append(", S6 = " + s6_count);


                            string whereStr = "record_index =" + current_id;

                            dbAdapter.Update("record_stomach_new", udateSql.ToString(), whereStr);
                          
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("수정이 되지 않았습니다");
                        }
                    }

                    else
                    {
                        MessageBox.Show("수정이 되지 않았습니다");
                        return;
                    }
                }
                else
                {
                    current_id = dbAdapter.record_Insert("record_stomach_new", insertSql.ToString());
                    MessageBox.Show("저장되었습니다.");
                }
 

            }
            catch (Exception e)
            {

                MessageBox.Show(""+e.ToString());
                MessageBox.Show("저장이 되지 않았습니다");

              
            }

        }


        public void pause()
        {
            try
            {
                if (_client.tcpClient != null && _client.tcpClient.Connected)
                {

                    if (!Main._f._is_Pause)
                    {
                        string xml = "pause\r\n";
                        _client.BeginSend(xml);
                        Main._f._is_Pause = true;
                        _sw.Stop();
                        _stw.Stop();

                    }
                    else
                    {
                        string xml = "continue\r\n";
                        _client.BeginSend(xml);
                        Main._f._is_Pause = false;
                        _sw.Start();
                        _stw.Start();
                    }

                    Main._f.pauseInit();

                }
                else
                {
                    MessageBox.Show("프로그램이 실행된 이후에 이용가능합니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("start 버튼을 눌러주세요.");
            }

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
                        _isRunningFlag = false;
                        string query = "stop\r\n";
                        
                        _client.BeginSend(query);
                       
                        
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

                       

                        _isStart = false; _isEnd = false;
                        _gridCheck = false;
                        _gridCheck2 = false;
                        _gridCount = 0;
                        _gridCount2 = 0;
                        test = 0;
                        pictureCount = 0;
                        
                        Main._f._is_Pause = false;
                        Main._f.save_btn.Text = "SAVE";
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
                //MessageBox.Show(ex.Message);
            }
        }

        public void changeStratStomachTime(object pbP)
        {
            
     
            String endtime = "";
            
            foreach (ImageDisplay id in Main._f.imageDisplayLayout.Controls)
            {
                if( id.BackColor == Color.Red)
                    id.BackColor = SystemColors.Control;
                if (id.BackColor == Color.Blue)
                    endtime = id.time_info.Text;
            }
            ImageDisplay form = pbP as ImageDisplay;

            form.BackColor = Color.Red;

         

            if (endtime == "")
            {
                MessageBox.Show("위 관찰 끝나는 위치를 선택해주세요");
            }
            else
            {
                TimeSpan start = TimeSpan.Parse(form.time_info.Text);
                TimeSpan end = TimeSpan.Parse(endtime);


                if (start < end)
                    ts = end - start;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                                            ts.Hours, ts.Minutes, ts.Seconds
                                            );

                Main._f.stomachTimeView.Rows[0].Cells[0].Value = elapsedTime;
            }
        }
        public void changeEndStomachTime(object pbP)
        {
           
            String starttime = "";

            foreach (ImageDisplay id in Main._f.imageDisplayLayout.Controls)
            {
                if (id.BackColor == Color.Blue)
                    id.BackColor = SystemColors.Control;

                if (id.BackColor == Color.Red)
                    starttime = id.time_info.Text;
            }
            ImageDisplay form = pbP as ImageDisplay;

            form.BackColor = Color.Blue;
            if (starttime == "")
            {
                MessageBox.Show("위 관찰 시작하는 위치를 선택해주세요");
            }
            else
            {
                TimeSpan start = TimeSpan.Parse(starttime);
                TimeSpan end = TimeSpan.Parse(form.time_info.Text);

                if (start < end)
                    ts = end - start;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                                            ts.Hours, ts.Minutes, ts.Seconds
                                            );

                Main._f.stomachTimeView.Rows[0].Cells[0].Value = elapsedTime;
            }
        }

        public void removeStomachTime(object pbP)
        {
            Control pbc = pbP as Control;

            pbc.BackColor = SystemColors.Control;

            Main._f.stomachTimeView.Rows[0].Cells[0].Value = "";

        }

        /*
         * 
         * 일시정지 후 사용자가 태그 변경시 태그를 변경시켜주는 이벤트
         * 220117 : 기능 비활성화 
         * 
         */
        public void removeTag(object pbP)
        {
            //if (_isRunningFlag)
            //{
            //    MessageBox.Show("stop을 누른뒤에 클래스 변경이 가능합니다. ");
            //}
            //else
            //{
               
                Control c = pbP as Control;

                DataGrid dataGrid = DataGrid._g;
                foreach (Control cont in c.Controls)
                {

                    if (cont.Name == "iTag")
                    {
                        Label tag_labe = cont as Label;
                        string[] token = tag_labe.Text.Split('\t');
                        
                        if (token[0] == "X")
                            x_count--;
                        
                        if (token[0] == "S6")
                            s6_count--;

                        if (token[0] != "X" || token[0] != "S6")
                            grid[token[0]]--;
                       
                        for (int k = 0; k < dataGrid.dataGridView1.RowCount; k++)
                        {
                            if (dataGrid.dataGridView1.Rows[k].Cells[1].Value.ToString().Equals(token[0]))
                            {

                                dataGrid.dataGridView1.Rows[k].Cells[2].Value = Int32.Parse(dataGrid.dataGridView1.Rows[k].Cells[2].Value.ToString()) - 1;
                                if (Int32.Parse(dataGrid.dataGridView1.Rows[k].Cells[2].Value.ToString()) == 0)
                                {
                                    dataGrid.dataGridView1.Rows[k].Cells[0].Style.ForeColor = Color.Red;
                                    dataGrid.dataGridView1.Rows[k].Cells[1].Style.ForeColor = Color.Red;
                                    dataGrid.dataGridView1.Rows[k].Cells[2].Style.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                    
                }
                c.Visible = false;
            //}
        
        }
        public void changeTag(object pbP, String class_name)
        {



            //if (_isRunningFlag)
            //{
            //   MessageBox.Show("stop을 누른뒤에 클래스 변경이 가능합니다. ");
            //}
            //else
            //{
                ImageDisplay id = pbP as ImageDisplay;
           

                Control c = pbP as Control;

                
            
                DataGrid dataGrid = DataGrid._g;

                if (class_name == "X")
                    x_count++;
                if (class_name == "S6")
                    s6_count++;

                foreach (Control cont in c.Controls)
                {
                    if (cont.Name == "iTag")
                    {
                        Label tag_labe = cont as Label;

                        string[] token = tag_labe.Text.Split('\t');
                        
                        if (token[0] == "X")
                            x_count--;
                        if (token[0] == "S6")
                            s6_count--;
                       
                        if (token[0] != "X" || token[0] != "S6")
                            grid[token[0]]--;
                       
                        if(class_name !="C")
                            grid[class_name]++; 
                        

                        for (int k = 0; k < dataGrid.dataGridView1.RowCount; k++)
                        {

                            if (dataGrid.dataGridView1.Rows[k].Cells[1].Value.ToString().Equals(class_name))
                            {

                                dataGrid.dataGridView1.Rows[k].Cells[2].Value = Int32.Parse(dataGrid.dataGridView1.Rows[k].Cells[2].Value.ToString()) + 1;
                                if (Int32.Parse(dataGrid.dataGridView1.Rows[k].Cells[2].Value.ToString()) > 0)
                                {
                                    dataGrid.dataGridView1.Rows[k].Cells[0].Style.ForeColor = Color.Black;
                                    dataGrid.dataGridView1.Rows[k].Cells[1].Style.ForeColor = Color.Black;
                                    dataGrid.dataGridView1.Rows[k].Cells[2].Style.ForeColor = Color.Black;
                                }
                            }

                            if (dataGrid.dataGridView1.Rows[k].Cells[1].Value.ToString().Equals(token[0]))
                            {

                                dataGrid.dataGridView1.Rows[k].Cells[2].Value = Int32.Parse(dataGrid.dataGridView1.Rows[k].Cells[2].Value.ToString()) - 1;
                                if (Int32.Parse(dataGrid.dataGridView1.Rows[k].Cells[2].Value.ToString()) == 0)
                                {
                                    dataGrid.dataGridView1.Rows[k].Cells[0].Style.ForeColor = Color.Red;
                                    dataGrid.dataGridView1.Rows[k].Cells[1].Style.ForeColor = Color.Red;
                                    dataGrid.dataGridView1.Rows[k].Cells[2].Style.ForeColor = Color.Red;

                                }
                            }
                        }

                        tag_labe.Text = class_name;
                        tag_labe.ForeColor = Color.Red;
                        tag_labe.Left = 90;
                        id.changeFlag = true;

                    }

                }


            //}

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
        public Boolean updateMain()
        {
            if (_client.tcpClient != null)
            {
                MessageBox.Show("이미 프로그램이 실행중입니다.");
                return false;
            }
            else
            {
                if (_client._imgCount > 0)
                {
                    if (!_isSaveFlag)
                    {
                        if (MessageBox.Show("save를 안하고 넘어가시겠습니까?", "저장", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            _isSaveFlag = false;
                            _client._imgCount = 0;
                            MessageBox.Show("관찰 기록이 저장되지 않았습니다."); 
                            return false;
                        }
                        else
                        {
                            MessageBox.Show("관찰 기록이 저장되지 않았습니다.");
                            return false;
                        }
                    }
                }
                checksedativeflag();
                checkAIflag();

                _isSaveFlag = false;
                current_id = -1;
                _client._imgCount = 0;

                Main._f.ai_flag = false;
                Main._f.sedative_flag = false;

                Main._f.imageDisplayLayout.Controls.Clear();
                DataGrid._g.dataGridView1.Rows.Clear();
                DataGrid._g.SetRowData();

                for (int i = 0; i < check_class.Length; i++)
                {
                    grid[check_class[i]] = 0;
                    grid2[check_class[i]] = 0;
                    isCount[i] = false;
                }

                _isStart = false; _isEnd = false;
                _gridCheck = false;
                _gridCount = 0;
                _gridCount2 = 0;
                test = 0;

                Main._f._is_Pause = false;
                Main._f.save_btn.Text = "SAVE";
                //_client.tcpClient.Close();
                _client.stopIs = true;
                _client._connectFlag = false;

                _sw.Reset();
                _stw.Reset();
                
                biopsy_flag = false;

                Main._f.totalTimeView.Rows[0].Cells[0].Value = null;
                Main._f.stomachTimeView.Rows[0].Cells[0].Value = null;
                x_count = 0;
                s6_count = 0;


                return true;
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
                            if (y == 5|| y == 16|| y == 17)//flag 변수 값 변환
                            {
                                if (currentRow[y].ToString() == "0")
                                    strRow += "\"" + "no" + "\"";
                                else
                                    strRow += "\"" + "yes" + "\"";
                            }
                            else
                            {
                                strRow += "\"" + currentRow[y].ToString() + "\"";
                            }
                            

                            if (y < intColumnCount - 1 && y >= 0)
                                strRow += ",";
                        }
                        content.Append(strRow + "\r\n");
                    }
                }
            }
            return content.ToString();
        }
        /*사용자가 세이브버튼을 누르면 발생하는 이벤트*/
        public void imageSave()
        {
            //if (Main._f._is_Pause)
            //{
                string savefolder = @"./imagesave";

                if (!System.IO.Directory.Exists(savefolder))
                {
                    System.IO.Directory.CreateDirectory(savefolder);
                }

                foreach (ImageDisplay id in Main._f.imageDisplayLayout.Controls)
                {
                    
                    if(id.changeFlag)
                    {
                        string fileN = id.fileName.Text;

                        string[] token = id.iTag.Text.Split('\t');

                        string directoryName = Path.GetDirectoryName(fileN);
                        string baseName_1 = Path.GetFileName(directoryName);
                        string baseName_2 = Path.GetFileName(fileN);
                        string temp_folder = savefolder + "\\" + baseName_1;


                        if (!System.IO.Directory.Exists(temp_folder))
                        {
                            System.IO.Directory.CreateDirectory(temp_folder);
                        }
                        string cls_temp_folder = temp_folder + "\\" + token[0];

                        if (!System.IO.Directory.Exists(cls_temp_folder))
                        {
                            System.IO.Directory.CreateDirectory(cls_temp_folder);
                        }

                        string saveImagepath = cls_temp_folder + "\\" + baseName_2;

                        id.pBimage.Image.Save(saveImagepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    
                }
                MessageBox.Show("저장이 완료 되었습니다..");


                current_id = -1;
                _client._imgCount = 0;

                Main._f.imageDisplayLayout.Controls.Clear();

            //}
            //else
            //{
            //    MessageBox.Show("일시정지를 누르시고 저장을 눌러주세요");
            //}
        }
        public void export()
        {
            
            /*
             * 
             * record
            
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


            DataSet ds = dbAdapter.SelectAll("record_stomach_new");
            

            Console.WriteLine(ds.GetXml());
            string result = ConvertToCSV(ds);


            Console.WriteLine(result);


            try
            {
                string file_name = $"./result_{sqlFormattedDate}.csv";
                File.WriteAllText(file_name, result, Encoding.Default);
                MessageBox.Show("파일이 추출되었습니다.");
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.ToString());
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
        public void checksedativeflag()
        {
            if (Main._f.checkBox1.Checked)
            {
                
                Main._f.sedative_flag = true;
                Main._f.checkBox1.Text = "진정내시경 YES";
                Main._f.checkBox1.BackColor = Color.Red;
                Main._f.checkBox1.ForeColor = Color.White;
            }
            else
            {
                Main._f.sedative_flag = false;
                Main._f.checkBox1.Text = "진정내시경 NO";
                Main._f.checkBox1.BackColor = Color.Blue;
                Main._f.checkBox1.ForeColor = Color.White;
            }
                
        }
        public void checkAIflag()
        {
            if (Main._f.checkBox2.Checked)
            {
                Main._f.ai_flag = true;
                Main._f.checkBox2.Text = "인공지능 ON";
                Main._f.checkBox2.BackColor = Color.Red;
                Main._f.checkBox2.ForeColor = Color.White;
            }
            else
            {
                Main._f.ai_flag = false;
                Main._f.checkBox2.Text = "인공지능 OFF";
                Main._f.checkBox2.BackColor = Color.Blue;
                Main._f.checkBox2.ForeColor = Color.White;
            }

        }

        public void addImage(PictureBox parentBox, PictureBox childBox)
        {
            childBox.Parent = parentBox;
           
            childBox.Visible = true;
        }
        public void minusImage(PictureBox parentBox, PictureBox childBox)
        {
            childBox.Parent = parentBox;
           
            childBox.Visible = false;
        }
        public void enroll()
        {
   
            memberForm memberForm = new memberForm();
            memberForm.ShowDialog();
            search();
        }
        public void search()
        {
            /*
                * select member 
            */
            DataSet ds = dbAdapter.SelectAll("member");

            member mem = new member();
            List<member> memList = new List<member>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                memList.Add(new member { membeid = Convert.ToInt32(dr["memberID"]), name = Convert.ToString(dr["name"]) });
            }

            ComboBox com = Main._f.comboBox1;
            com.Items.Clear();

            for (int i = 0; i < memList.Count; i++)
            {
                com.Items.Add(memList[i].name);
            }
        }
        public void changeUser(string name)
        {
      
            current_user = name;
            Main._f.user.Text = current_user;

            //MessageBox.Show(current_user);
        }
        public void deleteUser()
        {
            if (MessageBox.Show(current_user + "를 삭제 하시겠습니까?", "삭제" , MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbAdapter.DeleteDetail("member", "name", current_user);
                search();
                
                
                ComboBox com = Main._f.comboBox1;
                com.Items.Add("선택");
                com.SelectedIndex = com.Items.IndexOf("선택");

                current_user = "";
                Main._f.user.Text = "User";
            }
            else
            {
                return;
            }

        }
       

    }

}
