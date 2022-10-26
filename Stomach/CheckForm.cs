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
    public partial class CheckForm : Form
    {
        public CheckForm()
        {
            
            InitializeComponent();
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "ZioMED-Hallym-01")
            {
                Properties.Settings.Default.userRes = true;
                Properties.Settings.Default.Save();
                //setting value true 
                //setting value true면 프로그램 실행. 
            }
           
            if (Properties.Settings.Default.userRes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("번호를 올바르게 입력해 주세요.");
            }
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {       
            if (Properties.Settings.Default.userRes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
