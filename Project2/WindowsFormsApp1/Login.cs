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
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public static RSACryptoServiceProvider objRsa;
        public static DESCryptoServiceProvider objDes;
        public static Socket clientSocket;

        private void Login_Load(object sender, EventArgs e)
        {
            createRsa();
            createDes();

            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12000));
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Connection with server failed!\n"+ex.Message);
            }

            clientSocket.Send(encryptKey());



        }

        public static void createRsa()
        {
            string fullPathToServerKey = "C:\\Users\\Admin\\Documents\\GitHub\\DS_1819_Gr15\\Project2\\Server\\bin\\Debug\\ServerPublicKey.xml";
            StreamReader sr = new StreamReader(fullPathToServerKey);

            string strXmlParametrat = sr.ReadToEnd();
            sr.Close();

            objRsa = new RSACryptoServiceProvider();

            objRsa.FromXmlString(strXmlParametrat);
        }

        private static void createDes()
        {
            objDes = new DESCryptoServiceProvider();
            objDes.GenerateIV();
            objDes.Padding = PaddingMode.Zeros;
            objDes.Mode = CipherMode.CBC;
            objDes.GenerateKey();
        }

        private static byte[] encryptKey()
        {
            byte[] byteCelesiCipher = objRsa.Encrypt(objDes.Key, true);
            return byteCelesiCipher;
        }
    }
}
