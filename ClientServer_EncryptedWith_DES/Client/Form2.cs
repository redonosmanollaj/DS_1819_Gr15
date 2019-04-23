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
            string strKerkesa = txtKerkesa.Text;
            Fillo(strKerkesa);
        }

        private void Fillo(string kerkesa)
        {
            switch (kerkesa)
            {
                case "IPADRESA": return; break;
                case "NUMRIIPORTIT": return; break;
                case "BASHKETINGELLORE": return; break;
                case "KOHA": return; break;
                case "FIBONACCI": return; break;
                case "KONVERTIMI": return; break;
                default: MessageBox.Show("Kete kerkese nuk e permban serveri !!\n" +
                    "Provoni njeren nga kerkesat e paraqitura ne dritare.");break;
            }
        }
    }
}
