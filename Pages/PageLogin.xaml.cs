using Card_management_system.DataApp;
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

namespace Card_management_system.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
            textBoxPass.Visibility = Visibility.Hidden;
            
        }

        private void checkBoxShowPassword_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if(checkBox.IsChecked.Value)
            {
                textBoxPass.Visibility = Visibility.Visible;
                textBoxPass.Text = passwordBoxPass.Password;
                passwordBoxPass.Visibility = Visibility.Hidden;
            }
            else
            {
                passwordBoxPass.Visibility = Visibility.Visible;
                passwordBoxPass.Password = textBoxPass.Text;
                textBoxPass.Visibility = Visibility.Hidden;
            }
        }

        private void textBlockRegistration_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageRegistration());
        }

        public Users CheckSignIn() => PageClass.connectDB.Users.
            FirstOrDefault(t => t.login == textboxLogin.Text && 
            (t.password == textBoxPass.Text || t.password == passwordBoxPass.Password));

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = CheckSignIn();
            if(user != null)
            {
                // Вход в систему 
                MessageBox.Show("nice");
            }
        }
    }
}
