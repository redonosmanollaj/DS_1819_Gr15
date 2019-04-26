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
           
            
              //  try
              //  {
                    string strKerkesa = txtKerkesa.Text;
 
                    Form1.socketClient.Send(Encoding.UTF8.GetBytes(Enkripto(strKerkesa)));

                // -----------------------------------------

                    byte[] byteArdhura = new byte[1024];
                    int length = Form1.socketClient.Receive(byteArdhura);
                    string strArdhura = Encoding.UTF8.GetString(byteArdhura,0,length);
                    string strArdhuraDekriptuar = Dekripto(strArdhura);

                    txtArdhura.Text = strArdhuraDekriptuar;
             //   }
              //  catch(Exception ex)
              //  {
               //     MessageBox.Show("Error!" + ex.Message);
                //}
            
        }

        private static string Enkripto(string strKerkesa)
        {
            byte[] byteKerkesa = Encoding.UTF8.GetBytes(strKerkesa);

            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(byteKerkesa, 0, byteKerkesa.Length);
            cs.Close();

            byte[] byteKerkesaEnkriptuar = ms.ToArray();
            string strKerkesaEnkriptuar = Convert.ToBase64String(byteKerkesaEnkriptuar);

            return strKerkesaEnkriptuar;
        }

        private static string Dekripto(string strArdhura)
        {
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;

            //Encoding.ASCII.GetBytes(strArdhura);
            //string strArdhura64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(strArdhura));
            byte[] byteArdhuraEnkriptuar = Convert.FromBase64String(strArdhura);

            MemoryStream ms = new MemoryStream(byteArdhuraEnkriptuar);
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Read);

            byte[] byteArdhuraDekriptuar = new byte[ms.Length];
            cs.Read(byteArdhuraDekriptuar, 0, byteArdhuraDekriptuar.Length);
            cs.Close();

            string strArdhuraDekriptuar = Encoding.UTF8.GetString(byteArdhuraDekriptuar);
            return strArdhuraDekriptuar;
        }
    }
}
