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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class VerifySignForm : Form
    {
        RSACryptoServiceProvider objRsaForVerify;
        string username;
        public VerifySignForm(string jsonWebToken,string signature,string username)
        {
            InitializeComponent();
            rtSignature.Text = signature;
            rtJsonWebToken.Text = jsonWebToken;
            this.username = username;
        }

        private void BtnVerifySign_Click(object sender, EventArgs e)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(rtJsonWebToken.Text);
            byte[] byteSignature = Convert.FromBase64String(rtSignature.Text);

            objRsaForVerify = new RSACryptoServiceProvider(); // ME CELS PUBLIK T SERVERIT

            StreamReader sr = new StreamReader("serverPublicKeyForSign.xml");
            string strXml = sr.ReadToEnd();
            objRsaForVerify.FromXmlString(strXml);

            sr.Close();

            bool valid;
            valid = objRsaForVerify.VerifyData(byteData, byteSignature, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

            if (valid)
            {
                MessageBox.Show("The signature is valid");
                ProfesorDataForm pdf = new ProfesorDataForm(rtJsonWebToken.Text,rtSignature.Text,username);
                this.Hide();
                pdf.Show();
            }
            else
            {
                MessageBox.Show("The signature is not valid!");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Main main = new Main(rtJsonWebToken.Text, rtSignature.Text, username); ;
            this.Hide();
            main.Show();
        }

    }
}
