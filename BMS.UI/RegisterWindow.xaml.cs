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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserRepository userRepository;
        public RegisterWindow()
        {
            InitializeComponent();
            userRepository = new UserRepository();

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userRepository.RegisterUser(UserNametextBox.Text, FirstNametextBox.Text, SurnametextBox.Text, PasswordtextBox.Text, PhoneNumbertextBox.Text);
                User loggedUser = userRepository.GetUserByUserName(UserNametextBox.Text);
                if (loggedUser == null)
                    throw new Exception("User had not been registered, please register again!");
                else
                {
                    //Send Confirmation email
                    MessageWindow mw = new MessageWindow();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
