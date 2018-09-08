namespace TwoFactorSample
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.TwoFactorGroupBox = new System.Windows.Forms.GroupBox();
            this.TwoFactorButton = new System.Windows.Forms.Button();
            this.txtTwoFactorCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TwoFactorGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(85, 16);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(201, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(85, 51);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(201, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(211, 80);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // TwoFactorGroupBox
            // 
            this.TwoFactorGroupBox.Controls.Add(this.TwoFactorButton);
            this.TwoFactorGroupBox.Controls.Add(this.txtTwoFactorCode);
            this.TwoFactorGroupBox.Controls.Add(this.label3);
            this.TwoFactorGroupBox.Location = new System.Drawing.Point(12, 122);
            this.TwoFactorGroupBox.Name = "TwoFactorGroupBox";
            this.TwoFactorGroupBox.Size = new System.Drawing.Size(274, 87);
            this.TwoFactorGroupBox.TabIndex = 5;
            this.TwoFactorGroupBox.TabStop = false;
            this.TwoFactorGroupBox.Text = "Two factor authentication required";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Code:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(291, 111);
            this.Controls.Add(this.TwoFactorGroupBox);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Two Factor";
            this.TwoFactorGroupBox.ResumeLayout(false);
            this.TwoFactorGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.GroupBox TwoFactorGroupBox;
        private System.Windows.Forms.Button TwoFactorButton;
        private System.Windows.Forms.TextBox txtTwoFactorCode;
        private System.Windows.Forms.Label label3;
    }
}

