using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void LinkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            //lblErrorName.Text = "* Error Name";
            string name = nameTxt.Text.Trim();
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);

            string surname = surnameTxt.Text.Trim();
            surname = surname.Substring(0, 1).ToUpper() + surname.Substring(1);

            string email = emailTxt.Text;
            string degree = degreeTxt.Text;
            double salary = Double.Parse(salaryTxt.Text);
            string username = usernameTxt.Text;
            string password = passwordTxt.Text;

            Random random = new Random(DateTime.Now.Millisecond);
            string salt = random.Next(100000, 1000000).ToString();

            string saltedHash = getSaltedHash(salt, password);

            string strToServer = name + "%" + surname + "%" + email + "%" + degree + "%" + salary + "%" + username + "%" + salt + "%" + saltedHash;
            string strEncryptedToServer = Login.enkriptoDes(strToServer);
            byte[] byteToServer = Encoding.UTF8.GetBytes(strEncryptedToServer);

            Login.clientSocket.Send(byteToServer);
        }

        private static string getSaltedHash(string salt,string password)
        {
            string saltedPassword = salt + password;
            SHA1CryptoServiceProvider objHash = new SHA1CryptoServiceProvider();

            byte[] byteSaltedPassword = Encoding.UTF8.GetBytes(saltedPassword);
            byte[] byteSaltedHash = objHash.ComputeHash(byteSaltedPassword);

            return Convert.ToBase64String(byteSaltedHash);
        }
    }
}
