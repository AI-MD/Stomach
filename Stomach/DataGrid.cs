using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stomach
{
    public partial class DataGrid : UserControl
    {
        public static DataGrid _g;
       
        public DataGrid()
        {
            _g = this;
            InitializeComponent();


            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            //{
            //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    dataGridView1.Rows.RemoveAt(row.Index);
            //}

            //dataGridView1.Font = new Font("", 6.5F, FontStyle.Regular);

            dataGridView1.ForeColor = Color.Red;

            dataGridView1.Columns["label"].Width = 200;
            dataGridView1.Columns["Column1"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Column1"].FillWeight = 10;
            dataGridView1.Columns["count"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["count"].FillWeight = 10;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            SetRowData();

        }

        public void SetRowData()
        {
            dataGridView1.RowTemplate.Height = 22;
            dataGridView1.Rows.Add("식도", "E", "");
            dataGridView1.Rows.Add("분문부와 기저부(cardia and fundus)의 내시경 반전 사진(inversion view)", "S1", "");
            dataGridView1.Rows.Add("위 체부(corpus)의 전방 직시 사진(forward view)으로 소만부 포함한 촬영(including lesser curvature)", "S2", "");
            dataGridView1.Rows.Add("위 체부(corpus)의 내시경 반전 사진(retroflex view)으로 대만부 포함한 촬영(including greater curvature)", "S3", "");
            dataGridView1.Rows.Add("위각부(angle)", "S4", "");
            dataGridView1.Rows.Add("위 전정부(antrum)", "S5", "");
            dataGridView1.Rows.Add("십이지장 구부(bulb)", "D1", "");
            dataGridView1.Rows.Add("십이지장 제2부(2nd part)", "D2", "");

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = (dataGridView1.ClientRectangle.Height - dataGridView1.ColumnHeadersHeight) / dataGridView1.Rows.Count;
            }

        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = (dataGridView1.ClientRectangle.Height - dataGridView1.ColumnHeadersHeight) / dataGridView1.Rows.Count;

                int fontSize = dataGridView1.ClientRectangle.Width / 80;

                if (fontSize != 0)
                    dataGridView1.Font = new Font("", fontSize, FontStyle.Regular);
            }
        }
    }
}
