using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Threading;
using System.Security.Cryptography;
using System.IO;

namespace Client
{
    public partial class Form2 : Form
    {
        public static MyDes objMyDes;

        public Form2()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            objMyDes = new MyDes();
            lblIpAddress.Text = Form1.strIp;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtKerkesa_Click(object sender, EventArgs e)
        {
            txtKerkesa.Text = "";
        }

        private void btnKerko_Click(object sender, EventArgs e)
        {
           
            
                try
                {
                    string strKerkesa = txtKerkesa.Text;

                    if (strKerkesa != "")
                    {
                        Form1.socketClient.Send(Encoding.UTF8.GetBytes(Enkripto(strKerkesa)));
                        byte[] byteArdhura = new byte[1024];
                        int length = Form1.socketClient.Receive(byteArdhura);
                        string strArdhura = Encoding.UTF8.GetString(byteArdhura, 0, length);
                        string strArdhuraDekriptuar = Dekripto(strArdhura);

                        txtArdhura.Text = strArdhuraDekriptuar;
                    }
                    else
                    {
                    MessageBox.Show("Kerkesa eshte boshe!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error!" + ex.Message);
                }
            
        }

        private static string Enkripto(string strKerkesa)
        {
            byte[] byteKerkesa = Encoding.UTF8.GetBytes(strKerkesa);

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, objMyDes.objDES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(byteKerkesa, 0, byteKerkesa.Length);
            cs.Close();

            byte[] byteKerkesaEnkriptuar = ms.ToArray();
            string strKerkesaEnkriptuar = Convert.ToBase64String(byteKerkesaEnkriptuar);

            return strKerkesaEnkriptuar;
        }

        private static string Dekripto(string strArdhura)
        {

            byte[] byteArdhuraEnkriptuar = Convert.FromBase64String(strArdhura);

            MemoryStream ms = new MemoryStream(byteArdhuraEnkriptuar);
            CryptoStream cs = new CryptoStream(ms, objMyDes.objDES.CreateDecryptor(), CryptoStreamMode.Read);

            byte[] byteArdhuraDekriptuar = new byte[ms.Length];
            cs.Read(byteArdhuraDekriptuar, 0, byteArdhuraDekriptuar.Length);
            cs.Close();

            string strArdhuraDekriptuar = Encoding.UTF8.GetString(byteArdhuraDekriptuar);
            return strArdhuraDekriptuar;
        }

        
    }
}
