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
        public VerifySignForm(string jsonWebToken,string signature)
        {
            InitializeComponent();
            rtSignature.Text = signature;
            rtJsonWebToken.Text = jsonWebToken;
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
                MessageBox.Show("Nenshkrimi eshte valid");
                ProfesorDataForm pdf = new ProfesorDataForm(rtJsonWebToken.Text);
                pdf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nenshkrimi nuk eshte valid!");
            }
        }
    }
}
