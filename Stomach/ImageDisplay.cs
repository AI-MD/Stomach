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
        Controller controller = Controller._con;
        public Boolean changeFlag = false; 

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
            PictureBox pb = sender as PictureBox;
            
            if(e.Button.Equals(MouseButtons.Right))
            {
                
                //오른쪽 메뉴를 만듭니다
                
                ContextMenu m = new ContextMenu(); //메뉴에 들어갈 아이템을 만듭니다
                MenuItem start_stomach = new MenuItem(); 
                MenuItem end_stomach = new MenuItem();
                MenuItem remove_time = new MenuItem();
                MenuItem remove_item = new MenuItem();

                MenuItem choice_item = new MenuItem();
                
                MenuItem e_item = new MenuItem();
                MenuItem s1_item = new MenuItem();
                MenuItem s2_item = new MenuItem();
                MenuItem s3_item = new MenuItem();
                MenuItem s4_item = new MenuItem();
                MenuItem s5_item = new MenuItem();
                MenuItem d1_item = new MenuItem();
                MenuItem d2_item = new MenuItem();
                MenuItem x_item = new MenuItem();
                MenuItem s6_item = new MenuItem();
                MenuItem c_item = new MenuItem();

                start_stomach.Text = "start stomach";
                end_stomach.Text = "end stomach";
                remove_time.Text = "remove time";
                remove_item.Text = "remove image";

                

                choice_item.Text = "---select class---";
                e_item.Text = "E";
                s1_item.Text = "S1";
                s2_item.Text = "S2";
                s3_item.Text = "S3";
                s4_item.Text = "S4";
                s5_item.Text = "S5";
                d1_item.Text = "D1";
                d2_item.Text = "D2";
                x_item.Text = "X";
                s6_item.Text = "S6";
                c_item.Text = "C";
                //e_item.Visible = false;
                //s1_item.Visible = false;
                //s2_item.Visible = false;
                //s3_item.Visible = false;
                //s4_item.Visible = false;
                //s5_item.Visible = false;
                //d1_item.Visible = false;
                //d2_item.Visible = false;
                //x_item.Visible = false;


                start_stomach.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    Console.WriteLine(this.time_info.Text);
                    
                    /*
                     * image display 변경 및 위내시경 관찰 시간 변경 
                     */

                    controller.changeStratStomachTime(pb.Parent);


                };

                end_stomach.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    Console.WriteLine(this.time_info.Text);
                   
                    /*
                     * image display 변경 및 위내시경 관찰 시간 변경 
                     * 
                     */
                    
                    controller.changeEndStomachTime(pb.Parent);

                };

                remove_time.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    Console.WriteLine(this.time_info.Text);

                    /*
                     * image display 색상 초기화 & 위내시경 시간 remove 
                     * 
                     */

                    controller.removeStomachTime(pb.Parent); 

                };
                remove_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.removeTag(pb.Parent);

                };

                e_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "E");

                };
                s1_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "S1");

                };
                s2_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "S2");

                };
                s3_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "S3");

                };
                s4_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "S4");

                };
                s5_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "S5");

                };
                d1_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "D1");

                };
                d2_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "D2");

                };
                x_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "X");

                };
                s6_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "S6");

                };
                c_item.Click += (senders, es) => { //외부 함수에 아까 선택했던 아이템의 정보를 넘겨줍니다.

                    controller.changeTag(pb.Parent, "C");

                };

                m.MenuItems.Add(start_stomach);
                m.MenuItems.Add(end_stomach);
                m.MenuItems.Add(remove_time);
                m.MenuItems.Add(choice_item);

                m.MenuItems.Add(e_item);
                m.MenuItems.Add(s1_item);
                m.MenuItems.Add(s2_item);
                m.MenuItems.Add(s3_item);
                m.MenuItems.Add(s4_item);
                m.MenuItems.Add(s5_item);
                m.MenuItems.Add(d1_item);
                m.MenuItems.Add(d2_item);
                m.MenuItems.Add(x_item);
                m.MenuItems.Add(s6_item);
                m.MenuItems.Add(c_item);
                m.MenuItems.Add(remove_item);
                
                //현재 마우스가 위치한 장소에 메뉴를 띄워줍니다
                m.Show(this.pBimage, new Point(e.X, e.Y));

           
            }
        }
    }
}
