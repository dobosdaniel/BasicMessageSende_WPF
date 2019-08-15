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
            UsersListBox.ItemsSource = userRepository.GetAllUsers(_loggedUser.Username);
            
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
