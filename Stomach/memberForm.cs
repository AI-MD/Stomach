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
    public partial class memberForm : Form
    {
        Controller con;

        
        public  memberForm()
        {
           
            InitializeComponent();
        }

       
        private void reg_btn_Click(object sender, EventArgs e)
        {


            string member_name = this.textBox1.Text;


            DBAdapter dbAdapter = new DBAdapter();


            DateTime recordDate = DateTime.Now;
            string sqlFormattedDate = recordDate.ToString("yyyy-MM-dd");
            Console.WriteLine(sqlFormattedDate);

            StringBuilder insertSql = new StringBuilder();

            insertSql.Append("'" + member_name + "','" + sqlFormattedDate + "'");
            
            
            try
            {
                dbAdapter.member_Insert("member", insertSql.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            this.Close();

        }
    }
}
