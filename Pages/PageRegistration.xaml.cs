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

        public bool CountNumbers(TextBox textBox) => textBox.Text.Where(x => Char.IsNumber(x)).Count() == 0;

        public bool CountNumbers(PasswordBox passwordBox) => passwordBox.Password.Where(x => Char.IsNumber(x)).Count() > 0;

        public bool CheckEmail(TextBox textBox) => textBox.Text.Count(t => t == '@') > 0 && textBox.Text.Count(t => t == '@') < 2;

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if(CountNumbers(textBoxName) && CountNumbers(textBoxSurname))
            {
                MessageBox.Show("Цифр нет");
            }
            if (CheckEmail(textBoxEmail))
                MessageBox.Show("nice");
            
        }

        private void textBoxTelephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxTelephone.Text.Count(t => t == '+') == 0)
                textBoxTelephone.Text = "+" + textBoxTelephone.Text;
        }

        private bool CheckPassword(PasswordBox passwordBox) => CountNumbers(passwordBox) && passwordBox.Password.Length > 8;

        private void CheckEqualPassword(object sender, RoutedEventArgs e)
        {
            if (CheckPassword(passwordBoxPassFirst) && Equals(passwordBoxPassFirst.Password, passwordBoxPassSec.Password))
                MessageBox.Show("Equal");
            else
                MessageBox.Show("Пароли должный совпадать");
        }
    }
}
