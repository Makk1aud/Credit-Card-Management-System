using Card_management_system.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private string cardNumber;
        public Random random = new Random();
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
            {
                for (int i = 0; i < 4; i++)
                    cardNumber += $"{random.Next(1000, 9999)} ";
                Client client = new Client()
                {
                    userid = user.id,
                    cardnumber = cardNumber.Remove(cardNumber.Length-1),
                    carddate = DateTime.Now,
                    Cards = comboBoxCardType.SelectedItem as Cards,
                    cvv = textBoxCVV.Text
                };
                PageClass.connectDB.Client.Add(client);
                PageClass.connectDB.SaveChanges();
                MessageBox.Show("Вы успешно создали карту",
                    "Успешно", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                PageClass.frameObject.Navigate(new PageClient(user));
            }
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
