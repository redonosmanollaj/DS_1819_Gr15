﻿namespace WindowsFormsApp1
{
    partial class VerifySignForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.rtSignature = new System.Windows.Forms.RichTextBox();
            this.btnVerifySign = new System.Windows.Forms.Button();
            this.rtJsonWebToken = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "SIGNATURE";
            // 
            // rtSignature
            // 
            this.rtSignature.BackColor = System.Drawing.Color.DarkCyan;
            this.rtSignature.Location = new System.Drawing.Point(24, 43);
            this.rtSignature.Name = "rtSignature";
            this.rtSignature.Size = new System.Drawing.Size(471, 125);
            this.rtSignature.TabIndex = 1;
            this.rtSignature.Text = "";
            // 
            // btnVerifySign
            // 
            this.btnVerifySign.BackColor = System.Drawing.Color.Chartreuse;
            this.btnVerifySign.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerifySign.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerifySign.Location = new System.Drawing.Point(398, 312);
            this.btnVerifySign.Name = "btnVerifySign";
            this.btnVerifySign.Size = new System.Drawing.Size(97, 44);
            this.btnVerifySign.TabIndex = 2;
            this.btnVerifySign.Text = "Verify Sign";
            this.btnVerifySign.UseVisualStyleBackColor = false;
            this.btnVerifySign.Click += new System.EventHandler(this.BtnVerifySign_Click);
            // 
            // rtJsonWebToken
            // 
            this.rtJsonWebToken.BackColor = System.Drawing.Color.DarkCyan;
            this.rtJsonWebToken.Location = new System.Drawing.Point(24, 204);
            this.rtJsonWebToken.Name = "rtJsonWebToken";
            this.rtJsonWebToken.Size = new System.Drawing.Size(471, 73);
            this.rtJsonWebToken.TabIndex = 3;
            this.rtJsonWebToken.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "JSON WEB TOKEN";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Firebrick;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(282, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 44);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // VerifySignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(530, 387);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtJsonWebToken);
            this.Controls.Add(this.btnVerifySign);
            this.Controls.Add(this.rtSignature);
            this.Controls.Add(this.label1);
            this.Name = "VerifySignForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VerifySignForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtSignature;
        private System.Windows.Forms.Button btnVerifySign;
        private System.Windows.Forms.RichTextBox rtJsonWebToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
    }
}