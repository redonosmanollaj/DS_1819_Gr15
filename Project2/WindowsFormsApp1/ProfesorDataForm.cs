using JWT;
using JWT.Serializers;

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
    public partial class ProfesorDataForm : Form
    {
        string jsonWebToken;
        string signature;
        string username;
        public ProfesorDataForm(string jsonWebToken,string signature,string username)
        {
            InitializeComponent();
            this.jsonWebToken = jsonWebToken;
            this.signature = signature;
            this.username = username;
        }

        private void ProfesorDataForm_Load(object sender, EventArgs e)
        {
            string token = jsonWebToken;
            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);
                Console.WriteLine(json);
                rtTeksti.Text = json;

                var data = Newtonsoft.Json.Linq.JObject.Parse(json);

                lblName.Text = data["name"]+" ";
                lblSurname.Text = data["surname"]+"";
                lblDegree.Text = data["degree"]+"";
                lblSalary.Text = data["salary"]+"";
                lblEmail.Text = data["email"]+"";
                lblUsername.Text = data["username"]+"";

                
            }
            catch (TokenExpiredException)
            {
                MessageBox.Show("Token has been expired");
            }
            catch (SignatureVerificationException)
            {
                MessageBox.Show("Token has invalid signature");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            VerifySignForm vsf = new VerifySignForm(jsonWebToken, signature,username);
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
