namespace ChallengeExample
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
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.GetFeedButton = new System.Windows.Forms.Button();
            this.WebBrowserRmt = new System.Windows.Forms.WebBrowser();
            this.RtBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(87, 17);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(316, 20);
            this.txtUser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(87, 43);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(316, 20);
            this.txtPass.TabIndex = 3;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(328, 69);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButtonClick);
            // 
            // GetFeedButton
            // 
            this.GetFeedButton.Location = new System.Drawing.Point(181, 98);
            this.GetFeedButton.Name = "GetFeedButton";
            this.GetFeedButton.Size = new System.Drawing.Size(222, 23);
            this.GetFeedButton.TabIndex = 5;
            this.GetFeedButton.Text = "Get some feeds after login";
            this.GetFeedButton.UseVisualStyleBackColor = true;
            this.GetFeedButton.Click += new System.EventHandler(this.GetFeedButtonClick);
            // 
            // WebBrowserRmt
            // 
            this.WebBrowserRmt.Location = new System.Drawing.Point(2, 138);
            this.WebBrowserRmt.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowserRmt.Name = "WebBrowserRmt";
            this.WebBrowserRmt.Size = new System.Drawing.Size(411, 448);
            this.WebBrowserRmt.TabIndex = 6;
            this.WebBrowserRmt.Visible = false;
            this.WebBrowserRmt.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowserRmtDocumentCompleted);
            // 
            // RtBox
            // 
            this.RtBox.Location = new System.Drawing.Point(2, 137);
            this.RtBox.Name = "RtBox";
            this.RtBox.Size = new System.Drawing.Size(411, 456);
            this.RtBox.TabIndex = 7;
            this.RtBox.Text = "";
            this.RtBox.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 130);
            this.Controls.Add(this.RtBox);
            this.Controls.Add(this.WebBrowserRmt);
            this.Controls.Add(this.GetFeedButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Challenge Required Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button GetFeedButton;
        private System.Windows.Forms.WebBrowser WebBrowserRmt;
        private System.Windows.Forms.RichTextBox RtBox;
    }
}

