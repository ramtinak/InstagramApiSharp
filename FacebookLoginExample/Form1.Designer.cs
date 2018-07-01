namespace FacebookLoginExample
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
            this.FacebookLoginButton = new System.Windows.Forms.Button();
            this.RtBox = new System.Windows.Forms.RichTextBox();
            this.GetFeedButton = new System.Windows.Forms.Button();
            this.FacebookWebBrowser = new System.Windows.Forms.WebBrowser();
            this.LoadingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.LoadingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FacebookLoginButton
            // 
            this.FacebookLoginButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.FacebookLoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FacebookLoginButton.ForeColor = System.Drawing.Color.White;
            this.FacebookLoginButton.Location = new System.Drawing.Point(174, 69);
            this.FacebookLoginButton.Name = "FacebookLoginButton";
            this.FacebookLoginButton.Size = new System.Drawing.Size(155, 23);
            this.FacebookLoginButton.TabIndex = 0;
            this.FacebookLoginButton.Text = "Login with Facebook";
            this.FacebookLoginButton.UseVisualStyleBackColor = false;
            this.FacebookLoginButton.Click += new System.EventHandler(this.FacebookLoginButton_Click);
            // 
            // RtBox
            // 
            this.RtBox.Location = new System.Drawing.Point(1, 134);
            this.RtBox.Name = "RtBox";
            this.RtBox.Size = new System.Drawing.Size(522, 283);
            this.RtBox.TabIndex = 10;
            this.RtBox.Text = "";
            this.RtBox.Visible = false;
            // 
            // GetFeedButton
            // 
            this.GetFeedButton.Location = new System.Drawing.Point(149, 98);
            this.GetFeedButton.Name = "GetFeedButton";
            this.GetFeedButton.Size = new System.Drawing.Size(222, 23);
            this.GetFeedButton.TabIndex = 8;
            this.GetFeedButton.Text = "Get some feeds after login";
            this.GetFeedButton.UseVisualStyleBackColor = true;
            this.GetFeedButton.Visible = false;
            this.GetFeedButton.Click += new System.EventHandler(this.GetFeedButtonClick);
            // 
            // FacebookWebBrowser
            // 
            this.FacebookWebBrowser.Location = new System.Drawing.Point(436, 12);
            this.FacebookWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.FacebookWebBrowser.Name = "FacebookWebBrowser";
            this.FacebookWebBrowser.Size = new System.Drawing.Size(87, 51);
            this.FacebookWebBrowser.TabIndex = 11;
            this.FacebookWebBrowser.Visible = false;
            // 
            // LoadingPanel
            // 
            this.LoadingPanel.BackColor = System.Drawing.Color.White;
            this.LoadingPanel.Controls.Add(this.label1);
            this.LoadingPanel.Location = new System.Drawing.Point(149, 184);
            this.LoadingPanel.Name = "LoadingPanel";
            this.LoadingPanel.Size = new System.Drawing.Size(442, 233);
            this.LoadingPanel.TabIndex = 15;
            this.LoadingPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading...\r\nPlease wait...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(522, 422);
            this.Controls.Add(this.LoadingPanel);
            this.Controls.Add(this.FacebookWebBrowser);
            this.Controls.Add(this.RtBox);
            this.Controls.Add(this.GetFeedButton);
            this.Controls.Add(this.FacebookLoginButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facebook login example";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LoadingPanel.ResumeLayout(false);
            this.LoadingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FacebookLoginButton;
        private System.Windows.Forms.RichTextBox RtBox;
        private System.Windows.Forms.Button GetFeedButton;
        private System.Windows.Forms.WebBrowser FacebookWebBrowser;
        private System.Windows.Forms.Panel LoadingPanel;
        private System.Windows.Forms.Label label1;
    }
}

