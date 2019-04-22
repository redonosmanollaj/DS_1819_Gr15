using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{

    public partial class Form1 : Form
    {
        static public string strIp = "";
        static public int port = 0;

        static public IPEndPoint ip;
        public Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void btnKonektohu_Click(object sender, EventArgs e)
        {
            try
            {
                strIp = txtIP.Text;
                port = Convert.ToInt32(txtPorti.Text);
                ip = new IPEndPoint(IPAddress.Parse(strIp), port);
                socketClient.Connect(ip);
                this.Hide();
                Form2 f2 = new Form2();
                f2.Show();              
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Lidhja me kete server deshtoi!\n" + ex.Message);
            }
        }
    }
}

