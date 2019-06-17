using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{   
    public partial class Main : Form
    {
        string jSonWebToken;
        string signature;
        string username;
        public Main(string jSonWebToken, string signature,string username)
        {
            InitializeComponent();
            this.jSonWebToken = jSonWebToken;
            this.signature = signature;
            this.username = username;
            lblHello.Text = "Hello, " + username;

        }

        private void BtnFetchData_Click(object sender, EventArgs e)
        {
            VerifySignForm vsf = new VerifySignForm(jSonWebToken, signature,username);
            this.Hide();
            vsf.Show();
        }



        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }
    }
}
