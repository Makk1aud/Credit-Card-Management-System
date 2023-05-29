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
    /// Логика взаимодействия для PageCreateCard.xaml
    /// </summary>
    public partial class PageCreateCard : Page
    {
        private Users user;
        public PageCreateCard(Users user)
        {
            InitializeComponent();

            comboBoxCardType.SelectedValuePath = "id";
            comboBoxCardType.DisplayMemberPath= "name";
            comboBoxCardType.ItemsSource = PageClass.connectDB.Cards.ToList();
            this.user = user;
        }

        private bool CheckCVV(TextBox textBlock)
        {
            int number;
            return int.TryParse(textBoxCVV.Text, out number) && textBoxCVV.Text.Length == 3;
        }

        private void buttonCreateCard_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxCardType.SelectedItem != null && CheckCVV(textBoxCVV))
                MessageBox.Show("Nace");
        }

        private void CheckBoxChange(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            buttonCreateCard.IsEnabled = checkBox.IsChecked.Value;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.GoBack();
        }
    }
}
