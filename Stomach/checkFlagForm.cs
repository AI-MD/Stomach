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
    public partial class checkFlagForm : Form
    {
       
        Boolean sedative_flag = false;
        Boolean ai_flag = false;
        Controller con;
        
        public checkFlagForm()
        {

          
            InitializeComponent();
        }
       
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                sedative_flag = true;
               
             
            }
            else
            {
                sedative_flag = false;
               
                
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
            {
                ai_flag = true;

               
            }
            else
            {
                ai_flag = false;

                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.OK;



            Main._f.ai_flag = ai_flag;
            Main._f.sedative_flag = sedative_flag;

            this.Close();
        }
    }
}
