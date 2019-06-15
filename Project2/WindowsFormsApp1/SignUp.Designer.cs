namespace WindowsFormsApp1
{
    partial class SignUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.confirmpswTxt = new System.Windows.Forms.TextBox();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.passwordTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.usernameTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.emailTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.surnameTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLogin = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.degreeTxt = new System.Windows.Forms.TextBox();
            this.salaryTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // confirmpswTxt
            // 
            this.confirmpswTxt.BackColor = System.Drawing.Color.Linen;
            this.confirmpswTxt.Location = new System.Drawing.Point(221, 468);
            this.confirmpswTxt.Name = "confirmpswTxt";
            this.confirmpswTxt.Size = new System.Drawing.Size(293, 24);
            this.confirmpswTxt.TabIndex = 7;
            // 
            // nameTxt
            // 
            this.nameTxt.BackColor = System.Drawing.Color.Linen;
            this.nameTxt.Location = new System.Drawing.Point(221, 149);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(293, 24);
            this.nameTxt.TabIndex = 6;
            // 
            // passwordTxt
            // 
            this.passwordTxt.BackColor = System.Drawing.Color.Linen;
            this.passwordTxt.Location = new System.Drawing.Point(221, 425);
            this.passwordTxt.Name = "passwordTxt";
            this.passwordTxt.Size = new System.Drawing.Size(293, 24);
            this.passwordTxt.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "e-Mail";
            // 
            // usernameTxt
            // 
            this.usernameTxt.BackColor = System.Drawing.Color.Linen;
            this.usernameTxt.Location = new System.Drawing.Point(221, 379);
            this.usernameTxt.Name = "usernameTxt";
            this.usernameTxt.Size = new System.Drawing.Size(293, 24);
            this.usernameTxt.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(109, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Surname";
            // 
            // emailTxt
            // 
            this.emailTxt.BackColor = System.Drawing.Color.Linen;
            this.emailTxt.Location = new System.Drawing.Point(221, 240);
            this.emailTxt.Name = "emailTxt";
            this.emailTxt.Size = new System.Drawing.Size(293, 24);
            this.emailTxt.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Name";
            // 
            // surnameTxt
            // 
            this.surnameTxt.BackColor = System.Drawing.Color.Linen;
            this.surnameTxt.Location = new System.Drawing.Point(221, 193);
            this.surnameTxt.Name = "surnameTxt";
            this.surnameTxt.Size = new System.Drawing.Size(293, 24);
            this.surnameTxt.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 425);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 468);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Confirm password";
            // 
            // confirmBtn
            // 
            this.confirmBtn.Location = new System.Drawing.Point(399, 501);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(115, 36);
            this.confirmBtn.TabIndex = 13;
            this.confirmBtn.Text = "Confim";
            this.confirmBtn.UseVisualStyleBackColor = true;
            this.confirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // linkLogin
            // 
            this.linkLogin.AutoSize = true;
            this.linkLogin.LinkColor = System.Drawing.Color.MediumBlue;
            this.linkLogin.Location = new System.Drawing.Point(454, 540);
            this.linkLogin.Name = "linkLogin";
            this.linkLogin.Size = new System.Drawing.Size(53, 17);
            this.linkLogin.TabIndex = 14;
            this.linkLogin.TabStop = true;
            this.linkLogin.Text = "Log In";
            this.linkLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLogin_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources.icons8_add_user_male_filled_501;
            this.pictureBox1.Location = new System.Drawing.Point(294, 9);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 114);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // degreeTxt
            // 
            this.degreeTxt.BackColor = System.Drawing.Color.Linen;
            this.degreeTxt.Location = new System.Drawing.Point(221, 286);
            this.degreeTxt.Name = "degreeTxt";
            this.degreeTxt.Size = new System.Drawing.Size(293, 24);
            this.degreeTxt.TabIndex = 18;
            // 
            // salaryTxt
            // 
            this.salaryTxt.BackColor = System.Drawing.Color.Linen;
            this.salaryTxt.Location = new System.Drawing.Point(221, 333);
            this.salaryTxt.Name = "salaryTxt";
            this.salaryTxt.Size = new System.Drawing.Size(293, 24);
            this.salaryTxt.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(132, 333);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Salary";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(122, 286);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "Degree";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(422, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "* Wrong Name";
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(630, 613);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.degreeTxt);
            this.Controls.Add(this.salaryTxt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.linkLogin);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.surnameTxt);
            this.Controls.Add(this.emailTxt);
            this.Controls.Add(this.usernameTxt);
            this.Controls.Add(this.passwordTxt);
            this.Controls.Add(this.confirmpswTxt);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkGray;
            this.Name = "SignUp";
            this.Text = "Sign Up";
            this.Load += new System.EventHandler(this.SignUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox confirmpswTxt;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TextBox passwordTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox usernameTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox emailTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox surnameTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button confirmBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox degreeTxt;
        private System.Windows.Forms.TextBox salaryTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}