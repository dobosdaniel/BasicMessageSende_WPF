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
using System.Windows.Shapes;

namespace BMS.UI
{
    /// <summary>
    /// Interaction logic for NewMessageWindow.xaml
    /// </summary>
    public partial class NewMessageWindow : Window
    {
        private User _loggedUser;
        private MessageRepository messageRepository;
        private UserRepository userRepository;
        public NewMessageWindow(User loggedUser)
        {
            _loggedUser = loggedUser;
            messageRepository = new MessageRepository();
            userRepository = new UserRepository();
            InitializeComponent();
            UsersListBox.ItemsSource = userRepository.GetAllUsers(_loggedUser.Username).Select(x=>x.Username);

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (messageRepository.AddMessage(_loggedUser.Username, UsersListBox.SelectedItem.ToString(), MessageTextBox.Text) > 0)
                    MessageBox.Show("Message was sent successfully!");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error occured during saving the message. " + ex.Message);
            }
             
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userRepository.BlockUser(_loggedUser, UsersListBox.SelectedItem.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error occured during saving the message. " + ex.Message);
            }
        }
    }
}
