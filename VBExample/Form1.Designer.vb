<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SubmitPhoneChallengeButton = New System.Windows.Forms.Button()
        Me.label6 = New System.Windows.Forms.Label()
        Me.SubmitPhoneChallengeGroup = New System.Windows.Forms.GroupBox()
        Me.txtSubmitPhoneForChallenge = New System.Windows.Forms.TextBox()
        Me.TwoFactorButton = New System.Windows.Forms.Button()
        Me.txtTwoFactorCode = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.TwoFactorGroupBox = New System.Windows.Forms.GroupBox()
        Me.RtBox = New System.Windows.Forms.RichTextBox()
        Me.GetFeedButton = New System.Windows.Forms.Button()
        Me.ResendButton = New System.Windows.Forms.Button()
        Me.VerifyButton = New System.Windows.Forms.Button()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.txtVerifyCode = New System.Windows.Forms.TextBox()
        Me.LblForSmsEmail = New System.Windows.Forms.Label()
        Me.SendCodeButton = New System.Windows.Forms.Button()
        Me.SelectMethodGroupBox = New System.Windows.Forms.GroupBox()
        Me.RadioVerifyWithEmail = New System.Windows.Forms.RadioButton()
        Me.RadioVerifyWithPhoneNumber = New System.Windows.Forms.RadioButton()
        Me.VerifyCodeGroupBox = New System.Windows.Forms.GroupBox()
        Me.LoginButton = New System.Windows.Forms.Button()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.SubmitPhoneChallengeGroup.SuspendLayout()
        Me.TwoFactorGroupBox.SuspendLayout()
        Me.SelectMethodGroupBox.SuspendLayout()
        Me.VerifyCodeGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'SubmitPhoneChallengeButton
        '
        Me.SubmitPhoneChallengeButton.Location = New System.Drawing.Point(126, 51)
        Me.SubmitPhoneChallengeButton.Name = "SubmitPhoneChallengeButton"
        Me.SubmitPhoneChallengeButton.Size = New System.Drawing.Size(142, 23)
        Me.SubmitPhoneChallengeButton.TabIndex = 5
        Me.SubmitPhoneChallengeButton.Text = "Submit and send code"
        Me.SubmitPhoneChallengeButton.UseVisualStyleBackColor = True
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(9, 28)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(79, 13)
        Me.label6.TabIndex = 3
        Me.label6.Text = "Phone number:"
        '
        'SubmitPhoneChallengeGroup
        '
        Me.SubmitPhoneChallengeGroup.Controls.Add(Me.SubmitPhoneChallengeButton)
        Me.SubmitPhoneChallengeGroup.Controls.Add(Me.txtSubmitPhoneForChallenge)
        Me.SubmitPhoneChallengeGroup.Controls.Add(Me.label6)
        Me.SubmitPhoneChallengeGroup.Location = New System.Drawing.Point(71, 206)
        Me.SubmitPhoneChallengeGroup.Name = "SubmitPhoneChallengeGroup"
        Me.SubmitPhoneChallengeGroup.Size = New System.Drawing.Size(274, 87)
        Me.SubmitPhoneChallengeGroup.TabIndex = 26
        Me.SubmitPhoneChallengeGroup.TabStop = False
        Me.SubmitPhoneChallengeGroup.Text = "Your account needs to submit phone number"
        Me.SubmitPhoneChallengeGroup.Visible = False
        '
        'txtSubmitPhoneForChallenge
        '
        Me.txtSubmitPhoneForChallenge.Location = New System.Drawing.Point(91, 25)
        Me.txtSubmitPhoneForChallenge.Name = "txtSubmitPhoneForChallenge"
        Me.txtSubmitPhoneForChallenge.Size = New System.Drawing.Size(177, 20)
        Me.txtSubmitPhoneForChallenge.TabIndex = 4
        '
        'TwoFactorButton
        '
        Me.TwoFactorButton.Location = New System.Drawing.Point(193, 51)
        Me.TwoFactorButton.Name = "TwoFactorButton"
        Me.TwoFactorButton.Size = New System.Drawing.Size(75, 23)
        Me.TwoFactorButton.TabIndex = 5
        Me.TwoFactorButton.Text = "Auth"
        Me.TwoFactorButton.UseVisualStyleBackColor = True
        '
        'txtTwoFactorCode
        '
        Me.txtTwoFactorCode.Location = New System.Drawing.Point(67, 25)
        Me.txtTwoFactorCode.Name = "txtTwoFactorCode"
        Me.txtTwoFactorCode.Size = New System.Drawing.Size(201, 20)
        Me.txtTwoFactorCode.TabIndex = 4
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(9, 28)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(35, 13)
        Me.label5.TabIndex = 3
        Me.label5.Text = "Code:"
        '
        'TwoFactorGroupBox
        '
        Me.TwoFactorGroupBox.Controls.Add(Me.TwoFactorButton)
        Me.TwoFactorGroupBox.Controls.Add(Me.txtTwoFactorCode)
        Me.TwoFactorGroupBox.Controls.Add(Me.label5)
        Me.TwoFactorGroupBox.Location = New System.Drawing.Point(71, 210)
        Me.TwoFactorGroupBox.Name = "TwoFactorGroupBox"
        Me.TwoFactorGroupBox.Size = New System.Drawing.Size(274, 87)
        Me.TwoFactorGroupBox.TabIndex = 25
        Me.TwoFactorGroupBox.TabStop = False
        Me.TwoFactorGroupBox.Text = "Two factor authentication required"
        Me.TwoFactorGroupBox.Visible = False
        '
        'RtBox
        '
        Me.RtBox.Location = New System.Drawing.Point(4, 131)
        Me.RtBox.Name = "RtBox"
        Me.RtBox.Size = New System.Drawing.Size(406, 441)
        Me.RtBox.TabIndex = 24
        Me.RtBox.Text = ""
        Me.RtBox.Visible = False
        '
        'GetFeedButton
        '
        Me.GetFeedButton.Location = New System.Drawing.Point(174, 91)
        Me.GetFeedButton.Name = "GetFeedButton"
        Me.GetFeedButton.Size = New System.Drawing.Size(222, 23)
        Me.GetFeedButton.TabIndex = 23
        Me.GetFeedButton.Text = "Get some feeds after login"
        Me.GetFeedButton.UseVisualStyleBackColor = True
        Me.GetFeedButton.Visible = False
        '
        'ResendButton
        '
        Me.ResendButton.Location = New System.Drawing.Point(6, 119)
        Me.ResendButton.Name = "ResendButton"
        Me.ResendButton.Size = New System.Drawing.Size(134, 23)
        Me.ResendButton.TabIndex = 12
        Me.ResendButton.Text = "Resend verification code"
        Me.ResendButton.UseVisualStyleBackColor = True
        '
        'VerifyButton
        '
        Me.VerifyButton.Location = New System.Drawing.Point(196, 144)
        Me.VerifyButton.Name = "VerifyButton"
        Me.VerifyButton.Size = New System.Drawing.Size(75, 23)
        Me.VerifyButton.TabIndex = 11
        Me.VerifyButton.Text = "Verify"
        Me.VerifyButton.UseVisualStyleBackColor = True
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(26, 28)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(238, 26)
        Me.label3.TabIndex = 0
        Me.label3.Text = "You need to verify that this is your account." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please choose an method to verify " &
    "your account:"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(6, 96)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(63, 13)
        Me.label4.TabIndex = 7
        Me.label4.Text = "Verify code:"
        '
        'txtVerifyCode
        '
        Me.txtVerifyCode.Location = New System.Drawing.Point(75, 93)
        Me.txtVerifyCode.Name = "txtVerifyCode"
        Me.txtVerifyCode.Size = New System.Drawing.Size(196, 20)
        Me.txtVerifyCode.TabIndex = 6
        '
        'LblForSmsEmail
        '
        Me.LblForSmsEmail.AutoSize = True
        Me.LblForSmsEmail.Location = New System.Drawing.Point(6, 28)
        Me.LblForSmsEmail.Name = "LblForSmsEmail"
        Me.LblForSmsEmail.Size = New System.Drawing.Size(187, 13)
        Me.LblForSmsEmail.TabIndex = 1
        Me.LblForSmsEmail.Text = "We sent verity code to your sms/email"
        Me.LblForSmsEmail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'SendCodeButton
        '
        Me.SendCodeButton.Location = New System.Drawing.Point(156, 135)
        Me.SendCodeButton.Name = "SendCodeButton"
        Me.SendCodeButton.Size = New System.Drawing.Size(75, 23)
        Me.SendCodeButton.TabIndex = 10
        Me.SendCodeButton.Text = "Send code"
        Me.SendCodeButton.UseVisualStyleBackColor = True
        '
        'SelectMethodGroupBox
        '
        Me.SelectMethodGroupBox.Controls.Add(Me.SendCodeButton)
        Me.SelectMethodGroupBox.Controls.Add(Me.label3)
        Me.SelectMethodGroupBox.Controls.Add(Me.RadioVerifyWithEmail)
        Me.SelectMethodGroupBox.Controls.Add(Me.RadioVerifyWithPhoneNumber)
        Me.SelectMethodGroupBox.Location = New System.Drawing.Point(65, 154)
        Me.SelectMethodGroupBox.Name = "SelectMethodGroupBox"
        Me.SelectMethodGroupBox.Size = New System.Drawing.Size(289, 187)
        Me.SelectMethodGroupBox.TabIndex = 21
        Me.SelectMethodGroupBox.TabStop = False
        Me.SelectMethodGroupBox.Text = "Challenge require"
        Me.SelectMethodGroupBox.Visible = False
        '
        'RadioVerifyWithEmail
        '
        Me.RadioVerifyWithEmail.AutoSize = True
        Me.RadioVerifyWithEmail.Location = New System.Drawing.Point(29, 94)
        Me.RadioVerifyWithEmail.Name = "RadioVerifyWithEmail"
        Me.RadioVerifyWithEmail.Size = New System.Drawing.Size(50, 17)
        Me.RadioVerifyWithEmail.TabIndex = 9
        Me.RadioVerifyWithEmail.TabStop = True
        Me.RadioVerifyWithEmail.Text = "Email"
        Me.RadioVerifyWithEmail.UseVisualStyleBackColor = True
        Me.RadioVerifyWithEmail.Visible = False
        '
        'RadioVerifyWithPhoneNumber
        '
        Me.RadioVerifyWithPhoneNumber.AutoSize = True
        Me.RadioVerifyWithPhoneNumber.Location = New System.Drawing.Point(29, 71)
        Me.RadioVerifyWithPhoneNumber.Name = "RadioVerifyWithPhoneNumber"
        Me.RadioVerifyWithPhoneNumber.Size = New System.Drawing.Size(93, 17)
        Me.RadioVerifyWithPhoneNumber.TabIndex = 8
        Me.RadioVerifyWithPhoneNumber.TabStop = True
        Me.RadioVerifyWithPhoneNumber.Text = "PhoneNumber"
        Me.RadioVerifyWithPhoneNumber.UseVisualStyleBackColor = True
        Me.RadioVerifyWithPhoneNumber.Visible = False
        '
        'VerifyCodeGroupBox
        '
        Me.VerifyCodeGroupBox.Controls.Add(Me.ResendButton)
        Me.VerifyCodeGroupBox.Controls.Add(Me.VerifyButton)
        Me.VerifyCodeGroupBox.Controls.Add(Me.label4)
        Me.VerifyCodeGroupBox.Controls.Add(Me.txtVerifyCode)
        Me.VerifyCodeGroupBox.Controls.Add(Me.LblForSmsEmail)
        Me.VerifyCodeGroupBox.Location = New System.Drawing.Point(65, 154)
        Me.VerifyCodeGroupBox.Name = "VerifyCodeGroupBox"
        Me.VerifyCodeGroupBox.Size = New System.Drawing.Size(289, 187)
        Me.VerifyCodeGroupBox.TabIndex = 22
        Me.VerifyCodeGroupBox.TabStop = False
        Me.VerifyCodeGroupBox.Text = "Verify code"
        Me.VerifyCodeGroupBox.Visible = False
        '
        'LoginButton
        '
        Me.LoginButton.Location = New System.Drawing.Point(321, 62)
        Me.LoginButton.Name = "LoginButton"
        Me.LoginButton.Size = New System.Drawing.Size(75, 23)
        Me.LoginButton.TabIndex = 20
        Me.LoginButton.Text = "Login"
        Me.LoginButton.UseVisualStyleBackColor = True
        '
        'txtPass
        '
        Me.txtPass.Location = New System.Drawing.Point(80, 36)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(316, 20)
        Me.txtPass.TabIndex = 19
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(80, 7)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(316, 20)
        Me.txtUser.TabIndex = 18
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(16, 37)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(56, 13)
        Me.label2.TabIndex = 17
        Me.label2.Text = "Password:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(16, 11)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(58, 13)
        Me.label1.TabIndex = 16
        Me.label1.Text = "Username:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 125)
        Me.Controls.Add(Me.SubmitPhoneChallengeGroup)
        Me.Controls.Add(Me.TwoFactorGroupBox)
        Me.Controls.Add(Me.RtBox)
        Me.Controls.Add(Me.GetFeedButton)
        Me.Controls.Add(Me.SelectMethodGroupBox)
        Me.Controls.Add(Me.VerifyCodeGroupBox)
        Me.Controls.Add(Me.LoginButton)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "VB.NET Example"
        Me.SubmitPhoneChallengeGroup.ResumeLayout(False)
        Me.SubmitPhoneChallengeGroup.PerformLayout()
        Me.TwoFactorGroupBox.ResumeLayout(False)
        Me.TwoFactorGroupBox.PerformLayout()
        Me.SelectMethodGroupBox.ResumeLayout(False)
        Me.SelectMethodGroupBox.PerformLayout()
        Me.VerifyCodeGroupBox.ResumeLayout(False)
        Me.VerifyCodeGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents SubmitPhoneChallengeButton As Button
    Private WithEvents label6 As Label
    Private WithEvents SubmitPhoneChallengeGroup As GroupBox
    Private WithEvents txtSubmitPhoneForChallenge As TextBox
    Private WithEvents TwoFactorButton As Button
    Private WithEvents txtTwoFactorCode As TextBox
    Private WithEvents label5 As Label
    Private WithEvents TwoFactorGroupBox As GroupBox
    Private WithEvents RtBox As RichTextBox
    Private WithEvents GetFeedButton As Button
    Private WithEvents ResendButton As Button
    Private WithEvents VerifyButton As Button
    Private WithEvents label3 As Label
    Private WithEvents label4 As Label
    Private WithEvents txtVerifyCode As TextBox
    Private WithEvents LblForSmsEmail As Label
    Private WithEvents SendCodeButton As Button
    Private WithEvents SelectMethodGroupBox As GroupBox
    Private WithEvents RadioVerifyWithEmail As RadioButton
    Private WithEvents RadioVerifyWithPhoneNumber As RadioButton
    Private WithEvents VerifyCodeGroupBox As GroupBox
    Private WithEvents LoginButton As Button
    Private WithEvents txtPass As TextBox
    Private WithEvents txtUser As TextBox
    Private WithEvents label2 As Label
    Private WithEvents label1 As Label
End Class
