using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LidarTest
{
    public partial class Form1 : Form
    {

        SerialPort COM = new SerialPort();
        Bitmap mapimage = new Bitmap(320, 320);
        Graphics mapDraw;
        byte[] databuff = new byte[1024];



        bool[] point_enable = new bool[360];
        int[] point_length = new int[360];
        int[] point_power = new int[360];

        const int ringmax = 10240;
        volatile byte[] ringbuff = new byte[ringmax];
        volatile int ringcount = 0;
        volatile int ringwritepos = 0;
        volatile int ringreadpos = 0;


        int mode;
        int pindex;
        int ploop;
        int bitloop;
        bool updateflag;

        int maxangle = 0;
        public Form1()
        {
            InitializeComponent();
            COM.BaudRate = 115200;
            COM.DataBits = 8;
            COM.StopBits = StopBits.One;
            COM.Parity = Parity.None;
            COM.DataReceived += COM_DataReceived;
            mapDraw = Graphics.FromImage(mapimage);
            Timer updatetimer = new Timer();
            updatetimer.Interval = 1;
            updatetimer.Tick += Updatetimer_Tick;
            updatetimer.Start();
            Timer updatedraw = new Timer();
            updatedraw.Interval = 1;
            updatedraw.Tick += Updatedraw_Tick;
            updatedraw.Start();
        }

        private void Updatedraw_Tick(object sender, EventArgs e)
        {
            if (updateflag)
            {
                drawmap();
                updateflag = false;
            }
        }

        double scal = 320.0/2/3000;
        int marksize=2;
        void drawmap()
        {
            mapDraw.Clear(Color.White);
            Brush bs= new SolidBrush(Color.FromArgb(0,0,0));
            double h = 0, v = 1 * Math.PI / 180;
            for (int i = 0; i < 360; i++)
            {
                if (point_enable[i]) continue;
                int x, y;
                x = (int)(Math.Cos(h) * point_length[i]* scal);
                y = (int)(Math.Sin(h) * point_length[i] * scal);
                h += v;
                int r = point_power[i] / 2,g;
                if (r > 0xff) r = 0xff;
                g = 0xff - r;

                mapDraw.FillRectangle(new SolidBrush(Color.FromArgb(r, g, 0)), x+160, y + 160, marksize, marksize);
            }
            pointcloudimg.Image = mapimage;
        }

        private void Updatetimer_Tick(object sender, EventArgs e)
        {
            while (ringcount!=0)
            {
                byte dat = ringbuff[ringreadpos++];
                if (ringreadpos == ringmax) ringreadpos = 0;
                switch (mode)
                {
                    case 0:
                        if (dat == 0xfa)
                            mode++;
                        break;
                    case 1:
                        if(dat<0xa0)
                        {
                            dat = 0xa0;
                        }
                        pindex=dat-0xa0;
                        pindex *= 4;
                        mode++;
                        
                        break;
                    case 2:
                        mode++;
                        break;
                    case 3:
                        mode++;
                        ploop = 0;
                        bitloop = 0;
                        break;
                    case 4:
                        switch (bitloop)
                        {
                            case 0:
                                 point_length[pindex]=dat;
                                break;
                            case 1:
                                point_length[pindex] += (dat & 0x3f)<<8;
                                point_enable[pindex] = (dat & 0x80) != 0 ? true : false;
                                break;
                            case 2:
                                point_power[pindex] = dat;
                                break;
                            case 3:
                                point_power[pindex] += dat<<8;
                                break;
                        }
                        if (++bitloop >= 4)
                        {
                            if (++pindex >= 360)
                            {
                                pindex = 0;
                                updateflag = true;
                            }
                            bitloop = 0;
                            if (++ploop >= 4)
                                mode++;
                        }
                        break;
                    case 5:
                        mode++;
                        break;
                    case 6:
                        mode=0;
                        break;
                }
                ringcount--;
            }
                
        }

        private void COM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int len=COM.Read(databuff, 0, 1024);
            for(int i=0;i< len; i++)
            {
                ringbuff[ringwritepos] = databuff[i];
                ringcount++;
                if (++ringwritepos == ringmax) ringwritepos = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string com in SerialPort.GetPortNames())
            {
                ComSelect.Items.Add(com);
            }
            if (ComSelect.Items.Count != 0)
            {
                ComSelect.SelectedIndex = 0;
            }
        }

        private void opencom_Click(object sender, EventArgs e)
        {
            if (COM.IsOpen)
            {
                try
                {
                    COM.Close();
                    ComSelect.Enabled = true;
                    opencom.Text = "打开串口";
                }
                catch { }
            }
            else
            {
                try
                {
                    if (ComSelect.SelectedItem == null) return;
                    COM.PortName = (string)ComSelect.SelectedItem;
                    COM.Open();
                    ComSelect.Enabled = false;
                    opencom.Text = "关闭串口";
                }
                catch { };
            }
        }
    }
}
