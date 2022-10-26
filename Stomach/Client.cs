using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
namespace Stomach
{
 
    //TCP 연결만을 담당하는 util 컨트롤러 
    class Client
    {
        public int _imgCount = 0;
        //TcpClient 서버 연결하는 코드
        public static Queue<Packet> bindQueue = new Queue<Packet>();

        public TcpClient tcpClient;
      

        public delegate void incomingDataCallback(Packet data, int count);
        public incomingDataCallback incomingData = null;

        public delegate void gridDataCallback(Packet data);
        public gridDataCallback gridData = null;

        string ip;
        int port;
        public Boolean stopIs = false;
        public Boolean _connectFlag = false;
       

        public bool Initalize(string ip, int port)
        {
            try
            {
                this.ip = ip;
                this.port = port;

                if (tcpClient == null || stopIs)
                {
                    tcpClient = new TcpClient(ip, port);
                    Main._f.statusImage.Image = Properties.Resources._1_connected;
                    _connectFlag = true;
                   
                }

                if (tcpClient.Connected)
                {
                    //Main._f.status.Items.Add("server connected");
                    // 이미 실행 
                    Console.WriteLine("Connected to: {0}:{1}", ip, port);
                    stopIs = false;


                    _connectFlag = true;
                }
    
            }
            catch (Exception ex)
            {
                /*
                 * error code
                 */
                _connectFlag = false;
                Console.WriteLine(ex.Message);
                MessageBox.Show("서버가 켜져있는지 확인해주십시오.","server disconnect", MessageBoxButtons.OK, MessageBoxIcon.Error);
    
            }
            return _connectFlag;
        }


        public void BeginRead()
        {
            if (tcpClient == null)
            {
                Main._f.statusImage.Image = null;
                return;
            }
            if (tcpClient.Connected)
            {
                var buffer = new byte[4096];
                var ns = tcpClient.GetStream();
                ns.BeginRead(buffer, 0, buffer.Length, EndRead, buffer);
            }
            else
            {
                return;
            }
              
        }

        public void EndRead(IAsyncResult result)
        {
            try
            {
                var buffer = (byte[])result.AsyncState;
                var ns = tcpClient.GetStream();


                var bytesAvailable = ns.EndRead(result);


                string responseData = Encoding.ASCII.GetString(buffer, 0, bytesAvailable);

                if (responseData.Contains("no"))
                {
                    MessageBox.Show(responseData);

                    tcpClient = null;
                }
          
                else
                {
                    Packet packet = splitResult(responseData);
                    if (gridData != null)
                    {
                        gridData(packet);
                    }

                    if (incomingData != null)
                    {
                        incomingData(packet, _imgCount);

                        _imgCount++;
                    }
                }

                BeginRead();

            }
            catch(Exception ex)
            {
                /*
                 * 추가 로그를 파일로 저장 
                 */

                Console.WriteLine(ex.Message);
                MessageBox.Show("server disconnected");
                tcpClient = null;
                //Initalize(ip, port);

              
                
            }
        }

        public Packet splitResult(string responseData)
        {
            Packet packet = new Packet();
            string[] token = responseData.Split('\t');

            packet.SetPath(token[1]);
            packet.SetTag(token[2]);
            packet.SetProb(token[3]);


            bindQueue.Enqueue(packet);
            return packet;
        }

        public void BeginSend(string xml)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes(xml);

            //var bytes = Encoding.ASCII.GetBytes(xml);
            var ns = tcpClient.GetStream();
            //ns.BeginWrite(bytes, 0, bytes.Length, EndSend, bytes);
            ns.BeginWrite(sendBytes, 0, sendBytes.Length, EndSend, sendBytes);
        }

        public void EndSend(IAsyncResult result)
        {
            var bytes = (byte[])result.AsyncState;
            Console.WriteLine("Sent  {0} bytes to server.", bytes.Length);
            Console.WriteLine("Sent: {0}", Encoding.ASCII.GetString(bytes));
        }

    }

}
