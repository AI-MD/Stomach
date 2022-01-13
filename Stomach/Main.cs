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
using System.Threading;
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

        public Boolean _adminFlag;
        public Boolean _is_Pause = false;

        public static Main _f;
        //추후 ip나 포트 변경을 대비해 Main 폼에서 ip와 포트 관리할 것
        string ipAddress;
        int port;
        //메인 컨트롤러를 호출함으로서 앞으로 실행관련 모든 것들이 담길 Controller.cs를 호출
        Controller controller;
        DBAdapter dbAdapter;
        
        public Main(string[] args)
        {
            //ipAdress = "127.0.0.1";
            //port = 9999;
           
            if (args.Length == 0)
            {
                ipAddress = "127.0.0.1";
                port = 9999;
                _adminFlag = true;
            }

            else
            {
                ipAddress = args[0];
                string p = args[1];

                for (int i = 0; i < p.Length; i++)
                {

                    port = port * 10 + (p[i] - '0');

                }

                _adminFlag = Convert.ToBoolean(args[2]);
            }

     
             _f = this;
            InitializeComponent();
            clientInit();
            pauseInit();


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
            pause_btn.ForeColor = Color.Chocolate;
            pause_btn.FlatStyle = FlatStyle.Flat;
            pause_btn.FlatAppearance.BorderColor = Color.Chocolate;
            stop_btn.ForeColor = Color.Red;
            stop_btn.FlatStyle = FlatStyle.Flat;
            stop_btn.FlatAppearance.BorderColor = Color.Red;
            save_button.ForeColor = Color.BlueViolet;
            save_button.FlatStyle = FlatStyle.Flat;
            save_button.FlatAppearance.BorderColor = Color.BlueViolet;

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
        }


        private void Form_Closing(object sender, FormClosedEventArgs e)
        {
            controller.x_Close();
            //controller.stop_control();
        }

        /*사용자의 권한에 따른 차이를 두는 부분 (ADMIN, CLIENT)*/
        private void clientInit()
        {
            if (_adminFlag)
            {
                user.Text = "USER : ADMIN";
                label2.Visible = true;
                S1_button.Visible = true;
                S2_button.Visible = true;
                S3_button.Visible = true;
                S4_button.Visible = true;
                S5_button.Visible = true;
                D1_button.Visible = true;
                D2_button.Visible = true;
                E_button.Visible = true;
                groupBox1.Visible = false;
            }
            else
            {
                user.Text = "USER : CLIENT";
                label2.Visible = false;
                S1_button.Visible = false;
                S2_button.Visible = false;
                S3_button.Visible = false;
                S4_button.Visible = false;
                S5_button.Visible = false;
                D1_button.Visible = false;
                D2_button.Visible = false;
                E_button.Visible = false;
                groupBox1.Visible = false;

            }
        }
        /*****************************************************/

        /*일시정지 상태에 따른 차이를 두는 부분*/
        public void pauseInit()
        {
            if (_is_Pause)
            {
                pause_btn.Text = "RESUME";
                S1_button.Enabled = true;
                S2_button.Enabled = true;
                S4_button.Enabled = true;
                S3_button.Enabled = true;
                S5_button.Enabled = true;
                D1_button.Enabled = true;
                D2_button.Enabled = true;
                E_button.Enabled = true;
            }
            else
            {
                pause_btn.Text = "SAVE";
                S1_button.Enabled = false;
                S2_button.Enabled = false;
                S4_button.Enabled = false;
                S3_button.Enabled = false;
                S5_button.Enabled = false;
                D1_button.Enabled = false;
                D2_button.Enabled = false;
                E_button.Enabled = false;

            }
        }
        /****************************************************/
        private void user_convert_Click(object sender, EventArgs e)
        {
            if (_adminFlag)
            {
                _adminFlag = false;
            }
            else
            {
                _adminFlag = true;
            }

            clientInit();
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            controller.updateMain();
            controller.mainTask(ipAddress, port);

        }

        private void pause_btn_Click(object sender, EventArgs e)
        {

            controller.pause_control(_is_Pause);

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
        public void ImageDisplay_Click(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                PictureBox pb = sender as PictureBox;

                controller.changeTag(pb.Parent);

            }
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

        private void save_button_Click(object sender, EventArgs e)
        {

            controller.saveTag(_is_Pause);
        }

        private void imageDisplayLayout_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pb = sender as PictureBox;

                controller.changeTag(pb.Parent);

            }
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

        private void TimeLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

