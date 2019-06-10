using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public static RSACryptoServiceProvider objRsa;
        public static DESCryptoServiceProvider objDes;
        public static Socket clientSocket;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            createRsa();
            createDes();
            createClientSocket();
            string serverIp = "127.0.0.1";
            int port = 12000;

            try
            {
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIp), port));
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Couldn't connect to this server!\n"+ex.Message);
            }

            clientSocket.Send(Encoding.UTF8.GetBytes(enkriptoCelesin()));

        }



        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private static void createClientSocket()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        private static void createDes()
        {
            objDes = new DESCryptoServiceProvider();
            objDes.GenerateIV();
            objDes.Padding = PaddingMode.Zeros;
            objDes.Mode = CipherMode.CBC;
            objDes.GenerateKey();
        }

        private void createRsa()
        {
            objRsa = new RSACryptoServiceProvider();
            string path = "C:\\Users\\Admin\\Documents\\GitHub\\DS_1819_Gr15\\Project2\\Server\\bin\\Debug\\ServerPublicKey.xml";
            StreamReader sr = new StreamReader(path);
            string strXmlParametrat = sr.ReadToEnd();
            sr.Close();


            objRsa.FromXmlString(strXmlParametrat);
        }

        private string enkriptoCelesin()
        {
            byte[] byteCelesiPlain = objDes.Key;
            byte[] byteCelesiCipher = objRsa.Encrypt(byteCelesiPlain, true);

            return Convert.ToBase64String(byteCelesiCipher);
        }
    }
}
