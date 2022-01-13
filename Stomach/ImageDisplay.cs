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
    public partial class ImageDisplay : UserControl
    {


        public static ImageDisplay _i;
        Controller controller = new Controller();

        public ImageDisplay()
        {
            _i = this;
            InitializeComponent();
        }
        /*
         * 선택시 시간이 변경되도록 구현, 
         * 
         */
        private void pBimage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                

                PictureBox pb = sender as PictureBox;

                controller.changeTag(pb.Parent);

            }
        }
    }
}
