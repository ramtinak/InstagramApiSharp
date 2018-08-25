namespace ChallengeRequireExample
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.RadioVerifyWithPhoneNumber = new System.Windows.Forms.RadioButton();
            this.RadioVerifyWithEmail = new System.Windows.Forms.RadioButton();
            this.SelectMethodGroupBox = new System.Windows.Forms.GroupBox();
            this.SendCodeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.VerifyCodeGroupBox = new System.Windows.Forms.GroupBox();
            this.ResendButton = new System.Windows.Forms.Button();
            this.VerifyButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVerifyCode = new System.Windows.Forms.TextBox();
            this.LblForSmsEmail = new System.Windows.Forms.Label();
            this.GetFeedButton = new System.Windows.Forms.Button();
            this.RtBox = new System.Windows.Forms.RichTextBox();
            this.TwoFactorGroupBox = new System.Windows.Forms.GroupBox();
            this.TwoFactorButton = new System.Windows.Forms.Button();
            this.txtTwoFactorCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SelectMethodGroupBox.SuspendLayout();
            this.VerifyCodeGroupBox.SuspendLayout();
            this.TwoFactorGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(80, 7);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(316, 20);
            this.txtUser.TabIndex = 5;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(80, 36);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(316, 20);
            this.txtPass.TabIndex = 6;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(321, 62);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 7;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RadioVerifyWithPhoneNumber
            // 
            this.RadioVerifyWithPhoneNumber.AutoSize = true;
            this.RadioVerifyWithPhoneNumber.Location = new System.Drawing.Point(29, 71);
            this.RadioVerifyWithPhoneNumber.Name = "RadioVerifyWithPhoneNumber";
            this.RadioVerifyWithPhoneNumber.Size = new System.Drawing.Size(93, 17);
            this.RadioVerifyWithPhoneNumber.TabIndex = 8;
            this.RadioVerifyWithPhoneNumber.TabStop = true;
            this.RadioVerifyWithPhoneNumber.Text = "PhoneNumber";
            this.RadioVerifyWithPhoneNumber.UseVisualStyleBackColor = true;
            this.RadioVerifyWithPhoneNumber.Visible = false;
            // 
            // RadioVerifyWithEmail
            // 
            this.RadioVerifyWithEmail.AutoSize = true;
            this.RadioVerifyWithEmail.Location = new System.Drawing.Point(29, 94);
            this.RadioVerifyWithEmail.Name = "RadioVerifyWithEmail";
            this.RadioVerifyWithEmail.Size = new System.Drawing.Size(50, 17);
            this.RadioVerifyWithEmail.TabIndex = 9;
            this.RadioVerifyWithEmail.TabStop = true;
            this.RadioVerifyWithEmail.Text = "Email";
            this.RadioVerifyWithEmail.UseVisualStyleBackColor = true;
            this.RadioVerifyWithEmail.Visible = false;
            // 
            // SelectMethodGroupBox
            // 
            this.SelectMethodGroupBox.Controls.Add(this.SendCodeButton);
            this.SelectMethodGroupBox.Controls.Add(this.label3);
            this.SelectMethodGroupBox.Controls.Add(this.RadioVerifyWithEmail);
            this.SelectMethodGroupBox.Controls.Add(this.RadioVerifyWithPhoneNumber);
            this.SelectMethodGroupBox.Location = new System.Drawing.Point(65, 154);
            this.SelectMethodGroupBox.Name = "SelectMethodGroupBox";
            this.SelectMethodGroupBox.Size = new System.Drawing.Size(289, 187);
            this.SelectMethodGroupBox.TabIndex = 10;
            this.SelectMethodGroupBox.TabStop = false;
            this.SelectMethodGroupBox.Text = "Challenge require";
            this.SelectMethodGroupBox.Visible = false;
            // 
            // SendCodeButton
            // 
            this.SendCodeButton.Location = new System.Drawing.Point(156, 135);
            this.SendCodeButton.Name = "SendCodeButton";
            this.SendCodeButton.Size = new System.Drawing.Size(75, 23);
            this.SendCodeButton.TabIndex = 10;
            this.SendCodeButton.Text = "Send code";
            this.SendCodeButton.UseVisualStyleBackColor = true;
            this.SendCodeButton.Click += new System.EventHandler(this.SendCodeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "You need to verify that this is your account.\r\nPlease choose an method to verify " +
    "your account:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VerifyCodeGroupBox
            // 
            this.VerifyCodeGroupBox.Controls.Add(this.ResendButton);
            this.VerifyCodeGroupBox.Controls.Add(this.VerifyButton);
            this.VerifyCodeGroupBox.Controls.Add(this.label4);
            this.VerifyCodeGroupBox.Controls.Add(this.txtVerifyCode);
            this.VerifyCodeGroupBox.Controls.Add(this.LblForSmsEmail);
            this.VerifyCodeGroupBox.Location = new System.Drawing.Point(65, 154);
            this.VerifyCodeGroupBox.Name = "VerifyCodeGroupBox";
            this.VerifyCodeGroupBox.Size = new System.Drawing.Size(289, 187);
            this.VerifyCodeGroupBox.TabIndex = 11;
            this.VerifyCodeGroupBox.TabStop = false;
            this.VerifyCodeGroupBox.Text = "Verify code";
            this.VerifyCodeGroupBox.Visible = false;
            // 
            // ResendButton
            // 
            this.ResendButton.Location = new System.Drawing.Point(6, 119);
            this.ResendButton.Name = "ResendButton";
            this.ResendButton.Size = new System.Drawing.Size(134, 23);
            this.ResendButton.TabIndex = 12;
            this.ResendButton.Text = "Resend verification code";
            this.ResendButton.UseVisualStyleBackColor = true;
            this.ResendButton.Click += new System.EventHandler(this.ResendButton_Click);
            // 
            // VerifyButton
            // 
            this.VerifyButton.Location = new System.Drawing.Point(196, 144);
            this.VerifyButton.Name = "VerifyButton";
            this.VerifyButton.Size = new System.Drawing.Size(75, 23);
            this.VerifyButton.TabIndex = 11;
            this.VerifyButton.Text = "Verify";
            this.VerifyButton.UseVisualStyleBackColor = true;
            this.VerifyButton.Click += new System.EventHandler(this.VerifyButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Verify code:";
            // 
            // txtVerifyCode
            // 
            this.txtVerifyCode.Location = new System.Drawing.Point(75, 93);
            this.txtVerifyCode.Name = "txtVerifyCode";
            this.txtVerifyCode.Size = new System.Drawing.Size(196, 20);
            this.txtVerifyCode.TabIndex = 6;
            // 
            // LblForSmsEmail
            // 
            this.LblForSmsEmail.AutoSize = true;
            this.LblForSmsEmail.Location = new System.Drawing.Point(6, 28);
            this.LblForSmsEmail.Name = "LblForSmsEmail";
            this.LblForSmsEmail.Size = new System.Drawing.Size(187, 13);
            this.LblForSmsEmail.TabIndex = 1;
            this.LblForSmsEmail.Text = "We sent verity code to your sms/email";
            this.LblForSmsEmail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // GetFeedButton
            // 
            this.GetFeedButton.Location = new System.Drawing.Point(174, 91);
            this.GetFeedButton.Name = "GetFeedButton";
            this.GetFeedButton.Size = new System.Drawing.Size(222, 23);
            this.GetFeedButton.TabIndex = 12;
            this.GetFeedButton.Text = "Get some feeds after login";
            this.GetFeedButton.UseVisualStyleBackColor = true;
            this.GetFeedButton.Visible = false;
            this.GetFeedButton.Click += new System.EventHandler(this.GetFeedButton_Click);
            // 
            // RtBox
            // 
            this.RtBox.Location = new System.Drawing.Point(4, 131);
            this.RtBox.Name = "RtBox";
            this.RtBox.Size = new System.Drawing.Size(406, 441);
            this.RtBox.TabIndex = 13;
            this.RtBox.Text = "";
            this.RtBox.Visible = false;
            // 
            // TwoFactorGroupBox
            // 
            this.TwoFactorGroupBox.Controls.Add(this.TwoFactorButton);
            this.TwoFactorGroupBox.Controls.Add(this.txtTwoFactorCode);
            this.TwoFactorGroupBox.Controls.Add(this.label5);
            this.TwoFactorGroupBox.Location = new System.Drawing.Point(71, 210);
            this.TwoFactorGroupBox.Name = "TwoFactorGroupBox";
            this.TwoFactorGroupBox.Size = new System.Drawing.Size(274, 87);
            this.TwoFactorGroupBox.TabIndex = 14;
            this.TwoFactorGroupBox.TabStop = false;
            this.TwoFactorGroupBox.Text = "Two factor authentication required";
            this.TwoFactorGroupBox.Visible = false;
            // 
            // TwoFactorButton
            // 
            this.TwoFactorButton.Location = new System.Drawing.Point(193, 51);
            this.TwoFactorButton.Name = "TwoFactorButton";
            this.TwoFactorButton.Size = new System.Drawing.Size(75, 23);
            this.TwoFactorButton.TabIndex = 5;
            this.TwoFactorButton.Text = "Auth";
            this.TwoFactorButton.UseVisualStyleBackColor = true;
            this.TwoFactorButton.Click += new System.EventHandler(this.TwoFactorButton_Click);

            // 
            // txtTwoFactorCode
            // 
            this.txtTwoFactorCode.Location = new System.Drawing.Point(67, 25);
            this.txtTwoFactorCode.Name = "txtTwoFactorCode";
            this.txtTwoFactorCode.Size = new System.Drawing.Size(201, 20);
            this.txtTwoFactorCode.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Code:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 125);
            this.Controls.Add(this.TwoFactorGroupBox);
            this.Controls.Add(this.RtBox);
            this.Controls.Add(this.GetFeedButton);
            this.Controls.Add(this.VerifyCodeGroupBox);
            this.Controls.Add(this.SelectMethodGroupBox);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Challenge Required";
            this.SelectMethodGroupBox.ResumeLayout(false);
            this.SelectMethodGroupBox.PerformLayout();
            this.VerifyCodeGroupBox.ResumeLayout(false);
            this.VerifyCodeGroupBox.PerformLayout();
            this.TwoFactorGroupBox.ResumeLayout(false);
            this.TwoFactorGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.RadioButton RadioVerifyWithPhoneNumber;
        private System.Windows.Forms.RadioButton RadioVerifyWithEmail;
        private System.Windows.Forms.GroupBox SelectMethodGroupBox;
        private System.Windows.Forms.Button SendCodeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox VerifyCodeGroupBox;
        private System.Windows.Forms.Label LblForSmsEmail;
        private System.Windows.Forms.Button VerifyButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVerifyCode;
        private System.Windows.Forms.Button ResendButton;
        private System.Windows.Forms.Button GetFeedButton;
        private System.Windows.Forms.RichTextBox RtBox;
        private System.Windows.Forms.GroupBox TwoFactorGroupBox;
        private System.Windows.Forms.Button TwoFactorButton;
        private System.Windows.Forms.TextBox txtTwoFactorCode;
        private System.Windows.Forms.Label label5;
    }
}

