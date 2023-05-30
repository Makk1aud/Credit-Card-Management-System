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
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        public PageRegistration()
        {
            InitializeComponent();
            comboBoxGender.ItemsSource = new List<string>() { "Муж", "Жен", "Нет" };
        }

        public bool CountNumbers(TextBox textBox)
        {
            if (textBox.Text.Where(x => Char.IsNumber(x)).Count() == 0 && !String.IsNullOrEmpty(textBox.Text))
                return true;
            textBox.Text = null;
            return false;
        }

        public bool CountNumbers(PasswordBox passwordBox) => passwordBox.Password.Where(x => Char.IsNumber(x)).Count() > 0;

        public bool CheckEmail(TextBox textBox) => textBox.Text.Count(t => t == '@') > 0 && textBox.Text.Count(t => t == '@') < 2;

        private bool CheckPassword(PasswordBox passwordBox) => CountNumbers(passwordBox) && passwordBox.Password.Length > 7;

        private bool CheckUserInputData()
        {
            if (CheckPassword(passwordBoxPassFirst) && !Equals(passwordBoxPassFirst.Password, passwordBoxPassSec.Password))
            {
                MessageBox.Show("Пароли должный совпадать и иметь хотя бы 1 цифру");
                return false;
            }
            return CountNumbers(textBoxName) && CountNumbers(textBoxSurname)
                && CheckEmail(textBoxEmail) && textBoxTelephone.Text.Length == 12;    
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckUserInputData())
                return;
            Users users = new Users()
            {
                name = textBoxName.Text,
                surname = textBoxSurname.Text,
                patronymic = null,
                gender = comboBoxGender.Text,
                number = textBoxTelephone.Text,
                login = textBoxEmail.Text,
                password = passwordBoxPassFirst.Password,
                roleid = 2
            };
            PageClass.connectDB.Users.Add(users);
            PageClass.connectDB.SaveChanges();
            MessageBox.Show("Вы зарегистрировались",
                    "Успешно", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            PageClass.frameObject.GoBack();
        }

        private void textBoxTelephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxTelephone.Text.Count(t => t == '+') == 0)
                textBoxTelephone.Text = "+" + textBoxTelephone.Text;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.GoBack();
        }
    }
}
