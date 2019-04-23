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
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
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
            while (true)
            {
                try
                {
                    string strKerkesa = txtKerkesa.Text;
                    byte[] byteKerkesa = Encoding.UTF8.GetBytes(strKerkesa);
                    Enkripto(ref byteKerkesa);
                    Form1.socketClient.Send(byteKerkesa);


                    byte[] byteArdhura = new byte[1024];
                    int length = Form1.socketClient.Receive(byteArdhura);

                    Dekripto(ref byteArdhura);
                    txtArdhura.Text = Encoding.UTF8.GetString(byteArdhura);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error!" + ex.Message);
                }
            }
            Form1.socketClient.Close();
        }

        private void Enkripto(ref byte[] byteKerkesa)
        {
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(byteKerkesa, 0, byteKerkesa.Length);
            cs.Close();

            byteKerkesa = ms.ToArray();
        }

        private void Dekripto(ref byte[] byteArdhura)
        {
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;


            MemoryStream ms = new MemoryStream(byteArdhura);
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Read);

            byteArdhura = new byte[ms.Length];
            cs.Read(byteArdhura, 0, byteArdhura.Length);
            cs.Close();

        }
    }
}
