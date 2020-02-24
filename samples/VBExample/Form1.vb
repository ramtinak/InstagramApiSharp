'
'  Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https :  //t.me/ramtinak ]
'
'  Github source : https :  //github.com/ramtinak/InstagramApiSharp
'  Nuget package : https :    //www.nuget.org/packages/InstagramApiSharp
'  Update Date:   01 October 2018
'  IRANIAN DEVELOPERS
'
Imports InstagramApiSharp.API
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Threading
Imports System.Diagnostics
Imports System.IO
Imports InstagramApiSharp.Classes
Imports InstagramApiSharp.API.Builder
Imports InstagramApiSharp.Logger
Imports System.Text.RegularExpressions
Imports InstagramApiSharp.Classes.Models
Imports System.Net
Imports InstagramApiSharp
Imports InstagramApiSharp.Classes.SessionHandlers

Public Class Form1

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    ' VBExample project is a port of Challenge Example to Visual Basic.NET
    ' Which supports:
    '               - Challenge handlers
    '               - Two Factor Authentication
    ' So this is a best path to start using InstagramApiSharp in your VB.NET projects
    '
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' There are two different type of challenge is exists!
    '  - 1. You receive challenge while you already logged in:
    '       "This is me" or "This is not me" option!
    '       If some suspecious login happend, this will promp up, and you should accept it to get rid of it
    ' 
    '       Use Task<IResult<InstaLoggedInChallengeDataInfo>> GetLoggedInChallengeDataInfoAsync() to get information like coordinate of
    '       login request and more data info
    '       Use Task<IResult<bool>> AcceptChallengeAsync() to accept that you are the ONE that requests for login!
    ' 
    ' 
    ' 
    '  - 2. You receive challenge while you calling LoginAsync
    ' 
    ' Note: new challenge require functions is very easy to use.
    ' there are 5 functions I've added to IInstaApi for challenge require (checkpoint_endpoint)
    ' 
    ' 
    ' here:
    ' 1. Task<IResult<ChallengeRequireVerifyMethod>> GetChallengeRequireVerifyMethodAsync();
    ' If your login needs challenge, first you should call this function.
    '
    ' 
    ' Note: if you call this and SubmitPhoneRequired was true, you should sumbit phone number
    ' with this function:
    ' Task<IResult<ChallengeRequireSMSVerify>> SubmitPhoneNumberForChallengeRequireAsync();
    ' 
    ' 
    ' 2. Task<IResult<ChallengeRequireSMSVerify>> RequestVerifyCodeToSMSForChallengeRequireAsync();
    ' This function will send you verification code via SMS.
    ' 
    ' 
    ' 3. Task<IResult<ChallengeRequireEmailVerify>> RequestVerifyCodeToEmailForChallengeRequireAsync();
    ' This function will send you verification code via Email.
    ' 
    ' 
    ' 4. Task<IResult<ChallengeRequireVerifyMethod>> ResetChallengeRequireVerifyMethodAsync();
    ' Reset challenge require.
    ' Example: if your account has phone number and email, and you request for email(or phone number)
    ' and now you want to change it to another one, you should first call this function,
    ' then you have to call GetChallengeRequireVerifyMethodAsync and after that you can change your method!!!
    ' 
    ' 
    ' 5. Task<IResult<ChallengeRequireVerifyCode>> VerifyCodeForChallengeRequireAsync(string verifyCode);
    ' Verify sms or email verification code for login.
    ' 
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Dim AppName As String = "VB.NET Example"
    Dim StateFile As String = "state.bin"
    Dim NormalSize As Size = New Size(432, 164)
    Dim ChallengeSize As Size = New Size(432, 604)
    Dim InstaApi As IInstaApi

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Size = NormalSize
    End Sub

    Private Async Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        Size = NormalSize
        Dim userSession As UserSessionData = New UserSessionData With {
            .UserName = txtUser.Text,
            .Password = txtPass.Text
        }
        ' Session handler, set a file path to save/load your state/session data
        Dim sessionHandler = New FileSessionHandler With {.FilePath = StateFile}

        InstaApi = InstaApiBuilder.CreateBuilder.SetUser(userSession).UseLogger(New DebugLogger(LogLevel.All)).SetRequestDelay(RequestDelay.FromSeconds(0, 1)).SetSessionHandler(sessionHandler).Build
        Text = $"{AppName} Connecting"
        LoadSession()

        If Not InstaApi.IsUserAuthenticated Then
            ' Call this function before calling LoginAsync
            Await InstaApi.SendRequestsBeforeLoginAsync()


            Dim logInResult = Await InstaApi.LoginAsync()
            Debug.WriteLine(logInResult.Value)
            If logInResult.Succeeded Then
                GetFeedButton.Visible = True
                Text = $"{AppName} Connected"
                'Call this function after a successful login
                Await InstaApi.SendRequestsAfterLoginAsync()
                ' Save session 
                SaveSession()
            ElseIf (logInResult.Value = InstaLoginResult.ChallengeRequired) Then
                Dim challenge = Await InstaApi.GetChallengeRequireVerifyMethodAsync()
                If challenge.Succeeded Then
                    If challenge.Value.SubmitPhoneRequired Then
                        SubmitPhoneChallengeGroup.Visible = True
                        Size = ChallengeSize
                    ElseIf (Not (challenge.Value.StepData) Is Nothing) Then
                        If Not String.IsNullOrEmpty(challenge.Value.StepData.PhoneNumber) Then
                            RadioVerifyWithPhoneNumber.Checked = False
                            RadioVerifyWithPhoneNumber.Visible = True
                            RadioVerifyWithPhoneNumber.Text = challenge.Value.StepData.PhoneNumber
                        End If

                        If Not String.IsNullOrEmpty(challenge.Value.StepData.Email) Then
                            RadioVerifyWithEmail.Checked = False
                            RadioVerifyWithEmail.Visible = True
                            RadioVerifyWithEmail.Text = challenge.Value.StepData.Email
                        End If

                        SelectMethodGroupBox.Visible = True
                        Size = ChallengeSize
                    End If

                Else
                    MessageBox.Show(challenge.Info.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            ElseIf (logInResult.Value = InstaLoginResult.TwoFactorRequired) Then
                TwoFactorGroupBox.Visible = True
                Size = ChallengeSize
            Else
                MessageBox.Show($"{logInResult.Info?.Message}", "Error")
            End If

        Else
            Text = $"{AppName} Connected"
            GetFeedButton.Visible = True
        End If
    End Sub

    Private Async Sub SubmitPhoneChallengeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SubmitPhoneChallengeButton.Click
        Try
            If (String.IsNullOrEmpty(txtSubmitPhoneForChallenge.Text) OrElse String.IsNullOrWhiteSpace(txtSubmitPhoneForChallenge.Text)) Then
                MessageBox.Show("Please type a valid phone number(with country code)." & vbCrLf & "i.e: +989123456789", "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim phoneNumber = txtSubmitPhoneForChallenge.Text
            If Not phoneNumber.StartsWith("+") Then
                phoneNumber = $"+{phoneNumber}"
            End If

            Dim submitPhone = Await InstaApi.SubmitPhoneNumberForChallengeRequireAsync(phoneNumber)
            If submitPhone.Succeeded Then
                VerifyCodeGroupBox.Visible = True
                SubmitPhoneChallengeGroup.Visible = False
            Else
                MessageBox.Show(submitPhone.Info.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "EX", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Async Sub SendCodeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SendCodeButton.Click
        Dim isEmail As Boolean = False
        If RadioVerifyWithEmail.Checked Then
            isEmail = True
        End If

        'if (RadioVerifyWithPhoneNumber.Checked)
        '    isEmail = false;
        Try
            ' Note: every request to this endpoint is limited to 60 seconds                 
            If isEmail Then
                ' send verification code to email
                Dim email = Await InstaApi.RequestVerifyCodeToEmailForChallengeRequireAsync()
                If email.Succeeded Then
                    LblForSmsEmail.Text = "We sent verify code to this email:" & vbLf & $"{email.Value.StepData.ContactPoint}"
                    VerifyCodeGroupBox.Visible = True
                    SelectMethodGroupBox.Visible = False
                Else
                    MessageBox.Show(email.Info.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                ' send verification code to phone number
                Dim phoneNumber = Await InstaApi.RequestVerifyCodeToSMSForChallengeRequireAsync()
                If phoneNumber.Succeeded Then
                    LblForSmsEmail.Text = "We sent verify code to this phone number(it's end with this):" & vbLf & $"{phoneNumber.Value.StepData.ContactPoint}"
                    VerifyCodeGroupBox.Visible = True
                    SelectMethodGroupBox.Visible = False
                Else
                    MessageBox.Show(phoneNumber.Info.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "EX", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Async Sub ResendButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ResendButton.Click
        Dim isEmail As Boolean = False
        If RadioVerifyWithEmail.Checked Then
            isEmail = True
        End If

        Try
            ' Note: every request to this endpoint is limited to 60 seconds                 
            If isEmail Then
                ' send verification code to email
                Dim email = Await InstaApi.RequestVerifyCodeToEmailForChallengeRequireAsync(replayChallenge:=True)
                If email.Succeeded Then
                    LblForSmsEmail.Text = "We sent verification code one more time to this email:" & vbLf & $"{email.Value.StepData.ContactPoint}"
                    VerifyCodeGroupBox.Visible = True
                    SelectMethodGroupBox.Visible = False
                Else
                    MessageBox.Show(email.Info.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                ' send verification code to phone number
                Dim phoneNumber = Await InstaApi.RequestVerifyCodeToSMSForChallengeRequireAsync(replayChallenge:=True)
                If phoneNumber.Succeeded Then
                    LblForSmsEmail.Text = "We sent verification code one more time to this phone number(it's end with this): " & vbLf & $"{phoneNumber.Value.StepData.ContactPoint}"
                    VerifyCodeGroupBox.Visible = True
                    SelectMethodGroupBox.Visible = False
                Else
                    MessageBox.Show(phoneNumber.Info.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "EX", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Async Sub VerifyButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles VerifyButton.Click
        txtVerifyCode.Text = txtVerifyCode.Text.Trim
        txtVerifyCode.Text = txtVerifyCode.Text.Replace(" ", "")
        Dim regex = New Regex("^-*[0-9,\.]+$")
        If Not regex.IsMatch(txtVerifyCode.Text) Then
            MessageBox.Show("Verification code is numeric!!!", "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If (txtVerifyCode.Text.Length <> 6) Then
            MessageBox.Show("Verification code must be 6 digits!!!", "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            ' Note: calling VerifyCodeForChallengeRequireAsync function, 
            ' if user has two factor enabled, will wait 15 seconds and it will try to
            ' call LoginAsync.
            Dim verifyLogin = Await InstaApi.VerifyCodeForChallengeRequireAsync(txtVerifyCode.Text)
            If verifyLogin.Succeeded Then
                ' you are logged in sucessfully.
                SelectMethodGroupBox.Visible = False
                VerifyCodeGroupBox.Visible = False
                Size = ChallengeSize
                GetFeedButton.Visible = True
                ' Save session
                SaveSession()
                Text = $"{AppName} Connected"
            Else
                SelectMethodGroupBox.Visible = False
                VerifyCodeGroupBox.Visible = False
                ' two factor is required
                If (verifyLogin.Value = InstaLoginResult.TwoFactorRequired) Then
                    TwoFactorGroupBox.Visible = True
                Else
                    MessageBox.Show(verifyLogin.Info.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "EX", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Async Sub TwoFactorButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TwoFactorButton.Click
        If (InstaApi Is Nothing) Then
            Return
        End If

        If String.IsNullOrEmpty(txtTwoFactorCode.Text) Then
            MessageBox.Show("Please type your two factor code and then press Auth button.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' send two factor code
        Dim twoFactorLogin = Await InstaApi.TwoFactorLoginAsync(txtTwoFactorCode.Text)
        Debug.WriteLine(twoFactorLogin.Value)
        If twoFactorLogin.Succeeded Then
            ' connected
            ' save session
            SaveSession()
            Size = ChallengeSize
            TwoFactorGroupBox.Visible = False
            GetFeedButton.Visible = True
            Text = $"{AppName} Connected"
            Size = NormalSize
        Else
            MessageBox.Show(twoFactorLogin.Info.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Async Sub GetFeedButton_Click(sender As Object, e As EventArgs) Handles GetFeedButton.Click
        If (InstaApi Is Nothing) Then
            MessageBox.Show("Login first.")
            Return
        End If

        If Not InstaApi.IsUserAuthenticated Then
            MessageBox.Show("Login first.")
            Return
        End If

        Dim topicalExplore = Await InstaApi.FeedProcessor.GetTopicalExploreFeedAsync(PaginationParameters.MaxPagesToLoad(1))

        If topicalExplore.Succeeded = False Then
            If (topicalExplore.Info.ResponseType = ResponseType.ChallengeRequired) Then
                Dim challengeData = Await InstaApi.GetLoggedInChallengeDataInfoAsync()
                ' Do something to challenge data, if you want!

                Dim acceptChallenge = Await InstaApi.AcceptChallengeAsync()
                ' If Succeeded was TRUE, you can continue to your work!
            End If
        Else
            Dim sb As StringBuilder = New StringBuilder
            Dim sb2 As StringBuilder = New StringBuilder

            sb2.AppendLine("Like 5 Media>")

            For Each item In topicalExplore.Value.Medias.Take(5)
                ' like media...
                Dim liked = Await InstaApi.MediaProcessor.LikeMediaAsync(item.InstaIdentifier)
                sb2.AppendLine($"{item.InstaIdentifier} liked? {liked.Succeeded}")
            Next

            sb.AppendLine("Explore categories: " & topicalExplore.Value.Clusters.Count)
            Dim ix As Integer = 1
            For Each cluster As InstaTopicalExploreCluster In topicalExplore.Value.Clusters
                sb.AppendLine("#" & ix & " " & cluster.Name)
                ix += 1
            Next

            sb.AppendLine()
            sb.AppendLine()
            sb.AppendLine("Explore tv channels: " & topicalExplore.Value.TVChannels.Count)
            sb.AppendLine()
            sb.AppendLine()

            sb.AppendLine("Explore Feeds Result: " & topicalExplore.Succeeded)
            For Each media As InstaMedia In topicalExplore.Value.Medias
                sb.AppendLine(DebugUtils.PrintMedia("Feed media", media))
            Next

            RtBox.Text = (sb2.ToString _
                        & (Environment.NewLine _
                        & (Environment.NewLine & Environment.NewLine)))

            RtBox.Text = (RtBox.Text & sb.ToString)

            RtBox.Visible = True
            Size = ChallengeSize
        End If


        '' old explore page
        'Dim x = Await InstaApi.FeedProcessor.GetExploreFeedAsync(PaginationParameters.MaxPagesToLoad(1))

        'If x.Succeeded = False Then
        '    If (x.Info.ResponseType = ResponseType.ChallengeRequired) Then
        '        Dim challengeData = Await InstaApi.GetLoggedInChallengeDataInfoAsync()
        '        ' Do something to challenge data, if you want!

        '        Dim acceptChallenge = Await InstaApi.AcceptChallengeAsync()
        '        ' If Succeeded was TRUE, you can continue to your work!
        '    End If
        'Else
        '    Dim sb As StringBuilder = New StringBuilder
        '    Dim sb2 As StringBuilder = New StringBuilder

        '    sb2.AppendLine("Like 5 Media>")

        '    For Each item In x.Value.Medias.Take(5)
        '        ' like media...
        '        Dim liked = Await InstaApi.MediaProcessor.LikeMediaAsync(item.InstaIdentifier)
        '        sb2.AppendLine($"{item.InstaIdentifier} liked? {liked.Succeeded}")
        '    Next

        '    sb.AppendLine("Explore Feeds Result: " & x.Succeeded)

        '    For Each media As InstaMedia In x.Value.Medias
        '        sb.AppendLine(DebugUtils.PrintMedia("Feed media", media))
        '    Next

        '    RtBox.Text = (sb2.ToString _
        '                & (Environment.NewLine _
        '                & (Environment.NewLine & Environment.NewLine)))

        '    RtBox.Text = (RtBox.Text & sb.ToString)

        '    RtBox.Visible = True
        '    Size = ChallengeSize
        'End If
    End Sub

    Private Sub LoadSession()
        InstaApi.SessionHandler.Load()
        '' Old load session
        'Try
        '    If File.Exists(StateFile) Then
        '        Debug.WriteLine("Loading state from file")
        '        Dim fs = File.OpenRead(StateFile)
        '        InstaApi.LoadStateDataFromStream(fs)
        '    End If

        'Catch ex As Exception
        '    Debug.WriteLine(ex)
        'End Try
    End Sub

    Private Sub SaveSession()
        If (InstaApi Is Nothing) Then
            Return
        End If

        If Not InstaApi.IsUserAuthenticated Then
            Return
        End If
        InstaApi.SessionHandler.Save()
        '' Old save session
        'Dim state = InstaApi.GetStateDataAsStream
        'Dim fileStream = File.Create(StateFile)
        'state.Seek(0, SeekOrigin.Begin)
        'state.CopyTo(fileStream)
    End Sub

End Class
