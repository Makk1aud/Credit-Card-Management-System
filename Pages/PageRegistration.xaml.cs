using Card_management_system.Classes;
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

        public bool CheckNames(TextBox textBox)
        {
            if (textBox.Text.Where(x => Char.IsNumber(x)).Count() == 0 && !String.IsNullOrEmpty(textBox.Text))
                return true;
            textBox.Text = null;
            return false;
        }

        public bool CheckTelephone(TextBox textBox) => textBoxTelephone.Text.Length == 12 && CheckTelephoneSymbols(textBox);

        public bool CountNumbers(PasswordBox passwordBox) => passwordBox.Password.Where(x => Char.IsNumber(x)).Count() > 0;

        public bool CheckTelephoneSymbols(TextBox textBox) => textBox.Text.Where(x => Char.IsNumber(x)).Count() == 11;

        public bool CheckEmail(TextBox textBox) => textBox.Text.Contains("@") && textBox.Text.Count(t => t == '@') < 2;

        private bool CheckPassword(PasswordBox passwordBox) => CountNumbers(passwordBox) && passwordBox.Password.Length > 7;

        private bool CheckUserInputData()
        {
            if (CheckPassword(passwordBoxPassFirst) && !Equals(passwordBoxPassFirst.Password, passwordBoxPassSec.Password))
            {
                MessageBox.Show(
                    "Пароли должный совпадать и иметь хотя бы 1 цифру",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }
            return CheckNames(textBoxName)
                   && CheckNames(textBoxSurname)
                   && CheckEmail(textBoxEmail)
                   && CheckTelephone(textBoxTelephone)
                   && !DataBaseCardManagement.CheckDistinctEmailData(textBoxEmail.Text);    
        }

        private Users CreateUser()
        {
            try
            {
                Users users = new Users()
                {
                    name = textBoxName.Text,
                    surname = textBoxSurname.Text,
                    gender = comboBoxGender.Text,
                    number = textBoxTelephone.Text,
                    login = textBoxEmail.Text,
                    password = passwordBoxPassFirst.Password,
                    roleid = 2
                };
                return users;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            } 
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckUserInputData() || CreateUser() == null)
            {
                MessageBox.Show(
                    "Проверьте поля", 
                    "Warning", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            PageClass.connectDB.Users.Add(CreateUser());
            DataBaseCardManagement.SaveChangesDataBase("Вы успешно зарегистрировались");
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

        private void imageClipboard_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxTelephone.Text = Clipboard.GetText();
        }
    }
}
