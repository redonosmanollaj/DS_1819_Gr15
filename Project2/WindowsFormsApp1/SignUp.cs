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


                    MessageBox.Show("Registered successfully");
                    Login login = new Login();
                    this.Hide();
                    login.Show();

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
        private bool validateSurname()
        {
            string pattern = "^[a-zA-Z]";
            if (Regex.IsMatch(surnameTxt.Text, pattern) && surnameTxt.Text.Length > 2)
            {
                lblSurname.Text = "";
                return true;
            }
            else
            {
                lblSurname.Text = "*Wrong input";
                return false;
            }
        }
        private bool validateDegree()
        {
            string pattern = "^[a-zA-Z]";
            if (Regex.IsMatch(degreeTxt.Text, pattern))
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
        private bool validateSalary()
        {
            if (Double.Parse(salaryTxt.Text) == double.NaN)
            {
                lblSalary.Text = "*Input a number!";
                return false;
            }
            else
            {
                lblSalary.Text = "";
                return true;
            }
        }
        private bool validateUsername()
        {
            string pattern = "^[a-zA-Z0-9]";
            if (Regex.IsMatch(usernameTxt.Text, pattern) && usernameTxt.Text.Length > 5)
            {
                lblUsername.Text = "";
                return true;
            }
            else
            {
                lblUsername.Text = "*Wrong input";
                return false;
            }
        }
        private bool validatePass()
        {
            string pattern = "^[\\S*$]"; // no spaces
            if (passwordTxt.Text.Length > 6 && Regex.IsMatch(passwordTxt.Text,pattern))
            {
                lblPassword.Text = "";
                return true;
            }
            else
            {
                lblPassword.Text = "*Input more chars";
                return false;
            }
        }

        private bool validateConfirmPass()
        {
            if(confirmpswTxt.Text != passwordTxt.Text)
            {
                lblConfirmPassword.Text = "*Passwords does'nt match";
                return false;
            }
            else
            {
                lblConfirmPassword.Text = "";
                return true;
            }
        }
        private bool validateEmail()
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (Regex.IsMatch(emailTxt.Text, pattern, RegexOptions.IgnoreCase))
            {
                lblEmail.Text = "";
                return true;
            }
            else
            {
                lblEmail.Text = "* Wrong email";
                return false;
            }
        }
        private bool validate()
        {

            if(validateName() && validateSurname() && validateEmail() && validateDegree() && validateSalary() &&
                validateUsername() && validatePass() && validateConfirmPass())
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
