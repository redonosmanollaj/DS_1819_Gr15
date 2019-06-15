using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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


        private static string getSaltedHash(string salt, string password)
        {
            string saltedPassword = salt + password;
            SHA1CryptoServiceProvider objHash = new SHA1CryptoServiceProvider();

            byte[] byteSaltedPassword = Encoding.UTF8.GetBytes(saltedPassword);
            byte[] byteSaltedHash = objHash.ComputeHash(byteSaltedPassword);

            return Convert.ToBase64String(byteSaltedHash);
            
        }

        private void LinkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            
            string name = nameTxt.Text.Trim();
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);

            string surname = surnameTxt.Text.Trim();
            surname = surname.Substring(0, 1).ToUpper() + surname.Substring(1);

            string email = emailTxt.Text;
            string degree = degreeTxt.Text;
            double salary = double.Parse(salaryTxt.Text);
            string username = usernameTxt.Text;
            string password = passwordTxt.Text;

            //
            //validation
            //
            if (validateName(name) == false)
            {
                lblName.Text = "*Wrong input";
            }
            else
            {
                lblName.Text = "";
            }
            if(validateEmail(email) == false)
            {
                lblEmail.Text = "*Wrong email";
            }
            else
            {
                lblEmail.Text = "";
            }
            if(validateDegree(degree) == false)
            {
                lblDegree.Text = "*Wrong input";
            }
            if(validatePass(password) == false)
            {
                lblPassword.Text = "*Input more chars";
            }
            if(validateSalary(salary) == false)
            {
                lblSalary.Text = "*Input a number!";
            }
            if(validateSurname(surname) == false)
            {
                lblSurname.Text = "*Wrong input";
            }
            if(validateUsername(username) == false)
            {
                lblUsername.Text = "*Wrong input";
            }
            if(lblConfirmPassword.Text != lblPassword.Text)
            {
                lblConfirmPassword.Text = "*Passwords does'nt match";
            }

            if (validateUsername(username) && validateSurname(surname) && validateSalary(salary) &&
                validatePass(password) && validateName(name) && validateEmail(email) && validateDegree(degree))
            {
                MessageBox.Show("Logged in succesfully");




                Random random = new Random(DateTime.Now.Millisecond);
                string salt = random.Next(100000, 1000000).ToString();

                string saltedHash = getSaltedHash(salt, password);

                string strToServer = name + "%" + surname + "%" + email + "%" + degree + "%" + salary + "%" + username + "%" + password;
                string strEncryptedToServer = Login.enkriptoDes(strToServer);
                byte[] byteToServer = Encoding.UTF8.GetBytes(strEncryptedToServer);

                Login.clientSocket.Send(byteToServer);
            }
           
        }
        private bool validateName(string input)
        {
            string pattern = "^[a-zA-Z]";
            if (Regex.IsMatch(input, pattern) && input.Length > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool validateSurname(string input)
        {
            string pattern = "^[a-zA-Z]";
            if (Regex.IsMatch(input, pattern) && input.Length > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool validateDegree(string input)
        {
            string pattern = "^[a-zA-Z]";
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool validateSalary(double input)
        {
            if (input == double.NaN && input < 150 && input > 2000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private static bool validateUsername(string input)
        {
            string pattern = "^[a-zA-Z0-9]";
            if (Regex.IsMatch(input, pattern) && input.Length > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool validatePass(string input)
        {
            if (input.Length > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool validateEmail(string input)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
