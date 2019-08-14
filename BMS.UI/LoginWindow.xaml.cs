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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserRepository userRepository;
        public LoginWindow()
        {
            InitializeComponent();
            userRepository = new UserRepository();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = userRepository.Login(userNameTextBox.Text, passwordBox.Password);
            if(result)
            {
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("Incorret password!");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            this.Close();
            rw.Show();
        }
    }
}
