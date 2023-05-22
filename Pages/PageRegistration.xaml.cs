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
    }
}
