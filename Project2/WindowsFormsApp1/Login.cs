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
            //createRsa();
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

            clientSocket.Send(objDes.Key);
            clientSocket.Send(objDes.IV);


        }

        public static void createRsa()
        {
            string fullPathToServerKey = "C:\\Users\\Admin\\Documents\\GitHub\\DS_1819_Gr15\\Project2\\Server\\bin\\Debug\\public-key.xml";
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
            File.WriteAllText("des-key.xml",Encoding.UTF8.GetString(byteCelesiCipher));
            return byteCelesiCipher;
        }

        private static string dekriptoDes(string strFromServer)
        {
            byte[] byteFromServer = Convert.FromBase64String(strFromServer);

            MemoryStream ms = new MemoryStream(byteFromServer);
            CryptoStream cs = new CryptoStream(ms, objDes.CreateDecryptor(), CryptoStreamMode.Read);

            byte[] byteFromServerDecrypted = new byte[ms.Length];
            cs.Read(byteFromServerDecrypted, 0, byteFromServerDecrypted.Length);
            cs.Close();

            string strFromServerDecrypted = Encoding.UTF8.GetString(byteFromServerDecrypted);
            return strFromServerDecrypted;
        }

        public static string enkriptoDes(string strFromClient)
        {
            byte[] byteFromClient = Encoding.UTF8.GetBytes(strFromClient);

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, objDes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(byteFromClient, 0, byteFromClient.Length);
            cs.Close();

            byte[] byteFromClientEncrypted = ms.ToArray();
            string strFromClientEncrypted = Convert.ToBase64String(byteFromClientEncrypted);
            return strFromClientEncrypted;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string strFromClient = txtUsername.Text+'%'+txtPassword.Text;

                if (strFromClient != "")
                {
                    clientSocket.Send(Encoding.UTF8.GetBytes(enkriptoDes(strFromClient)));
                    byte[] byteFromServer = new byte[1024];
                    int length = clientSocket.Receive(byteFromServer);
                    string strFromServer = Encoding.UTF8.GetString(byteFromServer, 0, length);
                    string strFromServerDecrypted = dekriptoDes(strFromServer);

                    if (strFromServerDecrypted.Equals("false"))
                    {
                        MessageBox.Show("Username or Password are incorrect!");
                    }
                    else
                    {
                        string[] tokens = strFromServerDecrypted.Split(' ');
                        string signature = tokens[0];
                        string jsonWebToken = tokens[1];
                        VerifySignForm vsf = new VerifySignForm(jsonWebToken,signature);
                        vsf.Show();
                        this.Hide();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Ju lutem jepini te dhenat!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message);
            }
        }


        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
            this.Hide();
        }
    }
}
