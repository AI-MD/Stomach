using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.IO;


namespace Stomach
{
    public partial class Main : Form
    {
        //public Bitmap bitmap;
        //public Bitmap bitmap2;
        //public Mat mat;
        //public Mat blur;
        //public Mat addImage;
        //public Bitmap addbit;

        public PictureBox pic0 = new PictureBox();
        public PictureBox pic1 = new PictureBox();
        public PictureBox pic2 = new PictureBox();
        public PictureBox pic3 = new PictureBox();
        public PictureBox pic4 = new PictureBox();
        public PictureBox pic5 = new PictureBox();
        public PictureBox pic6 = new PictureBox();

       
        public Boolean _is_Pause = false;

        public static Main _f;
        //추후 ip나 포트 변경을 대비해 Main 폼에서 ip와 포트 관리할 것
        string ipAddress;
        int port;
        //메인 컨트롤러를 호출함으로서 앞으로 실행관련 모든 것들이 담길 Controller.cs를 호출
        Controller controller;
        DBAdapter dbAdapter;
        
        public Boolean ai_flag = false;
        public Boolean sedative_flag = false;
       
        public Main(string[] args)
        {
            //ipAdress = "127.0.0.1";
            //port = 9999;
           




            if (args.Length == 0)
            {
                ipAddress = "127.0.0.1";
                port = 9991; 
            }

            else
            {
                ipAddress = args[0];
                string p = args[1];

                for (int i = 0; i < p.Length; i++)
                {
                    port = port * 10 + (p[i] - '0');
                }
            }

     
             _f = this;
            InitializeComponent();
            clientInit();
           

            this.FormClosed += Form_Closing;
            imageDisplayLayout.Controls.Clear();

            DataGrid dataGrid = new DataGrid();
            dataGrid.Dock = DockStyle.Fill;
            DataGridPanel.Controls.Add(dataGrid);


            dataGrid.Location = new System.Drawing.Point(10, 0);

            var radioButtons = groupBox1.Controls.OfType<RadioButton>();

            start_btn.ForeColor = Color.Green;
            start_btn.FlatStyle = FlatStyle.Flat;
            start_btn.FlatAppearance.BorderColor = Color.Green;
            save_btn.ForeColor = Color.Chocolate;
            save_btn.FlatStyle = FlatStyle.Flat;
            save_btn.FlatAppearance.BorderColor = Color.Chocolate;
            stop_btn.ForeColor = Color.Red;
            stop_btn.FlatStyle = FlatStyle.Flat;
            stop_btn.FlatAppearance.BorderColor = Color.Red;
            export_button.ForeColor = Color.BlueViolet;
            export_button.FlatStyle = FlatStyle.Flat;
            export_button.FlatAppearance.BorderColor = Color.BlueViolet;

            DataGridViewRow row = totalTimeView.Rows[0];
            row.Height = 35;
            totalTimeView.Columns["TotalTime"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


            DataGridViewRow row2 = stomachTimeView.Rows[0];
            row2.Height = 35;
            stomachTimeView.Columns["StomachTime"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            totalTimeView.Columns["TotalTime"].HeaderCell.Style.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            stomachTimeView.Columns["StomachTime"].HeaderCell.Style.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);

            totalTimeView.Rows[0].Cells[0].Style.BackColor = Color.FromArgb(240, 240, 240);
            totalTimeView.Rows[0].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            stomachTimeView.Rows[0].Cells[0].Style.BackColor = Color.FromArgb(240, 240, 240);
            stomachTimeView.Rows[0].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            /*프로그램 우측 상단에 위치하는 위 이미지들을 정의해놓은 파트*/
            /**********************************************************************/
            /**********************************************************************/
            pictureBox1.Image = Properties.Resources.stomach;
            
            RightTopLayout.Controls.Add(this.pic0, 1, 0);
            pic0.Dock = System.Windows.Forms.DockStyle.Fill;
            pic0.BackColor = Color.Transparent;
            pic0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic0.Image = Properties.Resources.E;
            pic0.Visible = false;

            RightTopLayout.Controls.Add(this.pic1, 1, 0);
            pic1.Dock = System.Windows.Forms.DockStyle.Fill;
            pic1.BackColor = Color.Transparent;
            pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic1.Image = Properties.Resources.S1;
            pic1.Visible = false;

            RightTopLayout.Controls.Add(this.pic2, 1, 0);
            pic2.Dock = System.Windows.Forms.DockStyle.Fill;
            pic2.BackColor = Color.Transparent;
            pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic2.Image = Properties.Resources.S2S3;
            pic2.Visible = false;

            RightTopLayout.Controls.Add(this.pic3, 1, 0);
            pic3.Dock = System.Windows.Forms.DockStyle.Fill;
            pic3.BackColor = Color.Transparent;
            pic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic3.Image = Properties.Resources.S4;
            pic3.Visible = false;

            RightTopLayout.Controls.Add(this.pic4, 1, 0);
            pic4.Dock = System.Windows.Forms.DockStyle.Fill;
            pic4.BackColor = Color.Transparent;
            pic4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic4.Image = Properties.Resources.S5;
            pic4.Visible = false;

            RightTopLayout.Controls.Add(this.pic5, 1, 0);
            pic5.Dock = System.Windows.Forms.DockStyle.Fill;
            pic5.BackColor = Color.Transparent;
            pic5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic5.Image = Properties.Resources.D1;
            pic5.Visible = false;

            RightTopLayout.Controls.Add(this.pic6, 1, 0);
            pic6.Dock = System.Windows.Forms.DockStyle.Fill;
            pic6.BackColor = Color.Transparent;
            pic6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic6.Image = Properties.Resources.D2;
            pic6.Visible = false;

            /***********************************************************************
             * ********************************************************************/
            dbAdapter = new DBAdapter();
            controller = new Controller();
            controller.setDBAdapter(dbAdapter);
            controller.search();
        }


        private void Form_Closing(object sender, FormClosedEventArgs e)
        {
            controller.x_Close();
            //controller.stop_control();
        }

      
        private void clientInit()
        {
                /*
                 * 
                 * 회원 관리 추가
                 * 
                 * */
                enroll.Text = "등록";
                user.Text = "USER";

        }

        public void pauseInit()
        {

            if (_is_Pause)
                pause.Text = "continue";
            else
                pause.Text = "pause";
        }
        

        /****************************************************/
        private void delete_Click(object sender, EventArgs e)
        {
            controller.deleteUser();
        }

        private void start_btn_Click(object sender, EventArgs e)
        {



            if (controller.updateMain())
            {
                checkFlagForm checkFlag = new checkFlagForm();
               
                DialogResult result =  checkFlag.ShowDialog();
                if (result == DialogResult.OK)
                {
                    controller.mainTask(ipAddress, port);
                }
                else
                {
                    MessageBox.Show("진정 내시경 및 인공지능 사용 유무를 설정하고 시작해야 합니다.");
                }
            }


        }

        private void save_btn_Click(object sender, EventArgs e)
        {

            controller.save_control();

        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            controller.stop_control();
        }

        private void filtering_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller._s3Count = 0;
            controller.changeFilter(filtering_box.Text);

        }
      
        private void Main_Resize(object sender, EventArgs e)
        {
            switch (imageDisplayLayout.Width / 200)
            {
                case 0:
                    imageDisplayLayout.ColumnCount = 1;
                    break;
                case 1:
                    imageDisplayLayout.ColumnCount = 1;
                    break;
                case 2:
                    imageDisplayLayout.ColumnCount = 2;
                    break;
                case 3:
                    imageDisplayLayout.ColumnCount = 3;
                    break;
                case 4:
                    imageDisplayLayout.ColumnCount = 4;
                    break;
                case 5:
                    imageDisplayLayout.ColumnCount = 5;
                    break;
                case 6:
                    imageDisplayLayout.ColumnCount = 6;
                    break;
            }

        }

        private void export_button_Click(object sender, EventArgs e)
        {

            controller.export();
        }

       
        private void totalTimeView_SizeChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in totalTimeView.Rows)
            {
                row.Height = (totalTimeView.ClientRectangle.Height - totalTimeView.ColumnHeadersHeight) / totalTimeView.Rows.Count;
            }
        }

        private void stomachTimeView_SizeChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in stomachTimeView.Rows)
            {
                row.Height = (stomachTimeView.ClientRectangle.Height - stomachTimeView.ColumnHeadersHeight) / stomachTimeView.Rows.Count;
            }
        }


        private void enroll_Click(object sender, EventArgs e)
        {
            controller.enroll();
        
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox com = (ComboBox)sender;
            controller.changeUser((string)com.SelectedItem);
        }

        private void Main_Load(object sender, EventArgs e)
        {

            CheckForm checkForm = new CheckForm();
            
            DialogResult result = checkForm.ShowDialog();

            if (result != DialogResult.OK)
            {
                MessageBox.Show("프로그램 종료");
                this.Close();
            }
            else
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller.imageSave();
        }

        private void pause_Click(object sender, EventArgs e)
        {
            controller.pause();
        }
      
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            controller.checkAIflag();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            controller.checksedativeflag();
        }

       
    }
}

