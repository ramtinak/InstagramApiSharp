using InstagramApiSharp.Classes.SessionHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MultipleAccountExample.MultipleHelper;
namespace MultipleAccountExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindowLoaded;
        }


        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadSessions();
            await Task.Delay(1500);
            SessionsCombo.ItemsSource = LoggedInUsers;
        }

        private async void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(UserText.Text) && !string.IsNullOrEmpty(PassText.Text))
            {
                var username = UserText.Text.ToLower();
                var api = BuildApi(username, PassText.Text);
                var sessionHandler = new FileSessionHandler { FilePath = username.GetAccountPath(), InstaApi = api };

                api.SessionHandler = sessionHandler;
                var loginResult = await api.LoginAsync();
                if(loginResult.Succeeded)
                {
                    LoggedInUsers.Add(api.GetLoggedUser().LoggedInUser.UserName.ToLower());
                    ApiList.Add(api);
                    if (SessionsCombo.SelectedIndex == -1)
                        SessionsCombo.SelectedIndex = 0;
                    api.SessionHandler.Save();
                }
                else
                {
                    MessageBox.Show($"Error:\r\n{loginResult.Info.Message}\r\n\r\n" +
                        $"Please check ChallengeExample for handling two factor or challenge..."
                        , loginResult.Info.ResponseType.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private async void SendMessageToSingleButtonClick(object sender, RoutedEventArgs e)
        {
            if (ApiList.Count == 0)
            {
                MessageBox.Show($"At least one username MUST be login first"
                     , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var currentUsername = SessionsCombo.SelectedItem.ToString();
                var firstApi = ApiList.FirstOrDefault(api => api.GetLoggedUser().LoggedInUser.UserName.ToLower() == currentUsername);
                var userToText = await firstApi.UserProcessor.GetUserAsync("ministaapp");
                var textToSend = MessageText.Text;
                if (string.IsNullOrEmpty(textToSend))
                    textToSend = "This is a test message";
                if (userToText.Succeeded)
                {
                    var directText = await firstApi.MessagingProcessor.SendDirectTextAsync(userToText.Value.Pk.ToString(), null, textToSend);
                
                    if(directText.Succeeded)
                    {
                        // your message sent successfully 
                    }
                    else
                    {
                        // an error occured
                    }
                }

            }
        }

        private async void SendMessageToAllButtonClick(object sender, RoutedEventArgs e)
        {
            if (ApiList.Count == 0)
            {
                MessageBox.Show($"At least one username MUST be login first"
                     , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var firstApi = ApiList[0];
                var userToText = await firstApi.UserProcessor.GetUserAsync("ministaapp");
                var textToSend = MessageText.Text;
                if (string.IsNullOrEmpty(textToSend))
                    textToSend = "This is a test message";

                if (userToText.Succeeded)
                {
                    List<Task> tasks = new List<Task>();
                    foreach (var api in ApiList)
                    {
                        var t = api.MessagingProcessor.SendDirectTextAsync(userToText.Value.Pk.ToString(), null, textToSend);
                        tasks.Add(t);
                    }
                    await Task.WhenAll(tasks);
                }
            }
        }
    }
}
