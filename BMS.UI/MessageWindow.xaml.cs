using BasicMessageSender.Data.Models;
using BasicMessageSender.Data.Repositories;
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

namespace BMS.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        private User _loggedUser;
        private MessageRepository messageRepository;
        private UserRepository userRepository;
        public MessageWindow(User loggedUser)
        {
            _loggedUser = loggedUser;
            messageRepository = new MessageRepository();
            userRepository = new UserRepository();
            InitializeComponent();
            
            label.Content = "Logged in as: " + _loggedUser.FirstName + " " + _loggedUser.Surname;
            HideButton.Content = _loggedUser.IsHidden ? "Unhide" : "Hide";


            MessagesDataGrid.ItemsSource = messageRepository.GetAllReceivedMessagesForUser(_loggedUser.Username);
            messageNumberLabel.Content = "Daily messages: " + messageRepository.GetSentMessagesNumberByUserSentToday(_loggedUser.Username);
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            HideButton.Content = userRepository.HideFromList(_loggedUser) ? "Unhide" : "Hide";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
        private void NewMessageButton_Click(object sender, RoutedEventArgs e)
        {
            new NewMessageWindow(_loggedUser).Show();
        }

        private void MessagesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        void OnChecked(object sender, RoutedEventArgs e)
        {
            int a = 5; 
            throw new NotImplementedException();
        }
    }
}
