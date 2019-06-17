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
            string name = nameTxt.Text;
            string username = usernameTxt.Text;
            string email = emailTxt.Text.Trim();
            string degree = degreeTxt.Text.Trim();
            string salary = salaryTxt.Text.Trim();
            string surname = usernameTxt.Text.Trim();
            string password = passwordTxt.Text.Trim();

           
            if(validate())
            { 
                name = nameTxt.Text.Trim();
                name = name.Substring(0, 1).ToUpper() + name.Substring(1);

                surname = surnameTxt.Text.Trim();
                surname = surname.Substring(0, 1).ToUpper() + surname.Substring(1);


                string strToServer = name + ' ' + surname + ' ' + email + ' ' + degree + ' '+ salary + ' ' + username + ' ' + password;
                string strEncryptedToServer = Login.enkriptoDes(strToServer);
                byte[] byteToServer = Encoding.UTF8.GetBytes(strEncryptedToServer);

                Login.clientSocket.Send(byteToServer);

                byte[] byteFromServer = new byte[1024];
                int length = Login.clientSocket.Receive(byteFromServer);
                string strFromServer = Encoding.UTF8.GetString(byteFromServer, 0, length);
                string strFromServerDecrypted = Login.dekriptoDes(strFromServer);

                if (strFromServerDecrypted.Equals("true"))
                {
                    MessageBox.Show("Registered successfully");
                    Login login = new Login();
                    this.Hide();
                    login.Show();
                }
                else
                    MessageBox.Show(strFromServerDecrypted);
            }
            else
            {
                MessageBox.Show("The inputs are wrong!");
            }
           
        }
        //
        //validating methods
        //
        private bool validateName()
        {
            string pattern = "^[a-zA-Z]";
            if (Regex.IsMatch(nameTxt.Text, pattern) && nameTxt.Text.Length > 2)
            {
                lblName.Text = "";
                return true;
            }
            else
            {
                lblName.Text = "*Wrong input";
                return false;
            }
        }
        private bool validateSurname(string input)
        {
            string pattern = "^[a-zA-Z]";
            if (Regex.IsMatch(input, pattern) && input.Length > 2)
            {
                lblEmail.Text = "";
                return true;
            }
            else
            {
                lblEmail.Text = "*Wrong email";
                return false;
            }
        }
        private bool validateDegree(string input)
        {
            string pattern = "^[a-zA-Z]";
            if (Regex.IsMatch(input, pattern))
            {
                lblDegree.Text = "";
                return true;
            }
            else
            {
                lblDegree.Text = "*Wrong input";
                return false;
            }
        }
        private static bool validateSalary(double input)
        {
            if (input == double.NaN)
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
            string pattern = "^[\\S*$]"; // no spaces
            if (input.Length > 6 && Regex.IsMatch(input,pattern))
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
        private bool validate()
        {

            if (validateName() == false)
            {
                
                return false;
            }
            if (validateEmail(emailTxt.Text) == false)
            {
                
                return false;
            }
            if (validateDegree(degreeTxt.Text) == false)
            {
                
                return false;
            }
            if (validatePass(passwordTxt.Text) == false)
            {
                lblPassword.Text = "*Input more chars";
                return false;
            }
            if (validateSalary(double.Parse(salaryTxt.Text)) == false)
            {
                lblSalary.Text = "*Input a number!";
                return false;
            }
            if (validateSurname(surnameTxt.Text) == false)
            {
                lblSurname.Text = "*Wrong input";
                return false;
            }
            if (validateUsername(usernameTxt.Text) == false)
            {
                lblUsername.Text = "*Wrong input";
                return false;
            }
            if (lblConfirmPassword.Text != lblPassword.Text)
            {
                lblConfirmPassword.Text = "*Passwords does'nt match";
                return false;
            }
            if (validateUsername(usernameTxt.Text) && validateSurname(surnameTxt.Text) && 
                validateSalary(double.Parse(salaryTxt.Text)) &&
                validatePass(passwordTxt.Text) && validateName() && 
                validateEmail(emailTxt.Text) && validateDegree(degreeTxt.Text))
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
