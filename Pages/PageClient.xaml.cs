using Card_management_system.Classes;
using Card_management_system.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Card_management_system.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageClient.xaml
    /// </summary>
    public partial class PageClient : Page
    {
        private Users user;
        private Client client;
        public PageClient(Users user)
        {
            InitializeComponent();
            this.user = user;
            textBlockFullName.Text = $"{user.name} {user.surname}";
            client = PageClass.connectDB.Client.FirstOrDefault(x => x.userid == user.id);
            FillingCardDescription(client);

            FillingComboBox.ComboBoxItems(comboBoxCardChoose, "id", "cardnumber");
            comboBoxCardChoose.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid == user.id).ToList();
            comboBoxCardChoose.SelectedItem = comboBoxCardChoose.Items[0];
        }

        private bool CheckForTransaction() => PageClass.connectDB.Client.Count(x => x.userid == user.id) > 0;

        // Filling all description about user
        private void FillingCardDescription(Client client)
        {
            if (client == null)
                return;
            textBlockClientName.Text = $"{user.name} {user.surname}";
            textBlockBalance.Text = client.balance.ToString();
            textBlockCardCvv.Text = "$$$";
            textBlockCardDate.Text = $"{client.carddate.Value.Month}/{client.carddate.Value.Year}";
            textBlockCardType.Text = PageClass.connectDB.Cards.FirstOrDefault(x => x.id == client.cardid).name;
            textBlockCardNumber.Text = client.cardnumber.ToString();

        }

        //swap client cvv for special symbols
        private void SwapCVV()
        {
            if (textBlockCardCvv.Text == "$$$")
                textBlockCardCvv.Text = client.cvv.ToString();
            else
                textBlockCardCvv.Text = "$$$";
        }

        //swap images for hide or open cvv
        private void ShowHideCVV(object sender, MouseButtonEventArgs e)
        {
            if(client== null) return;
            System.Windows.Controls.Image image = sender as System.Windows.Controls.Image;
            if(image == imageHideCVV)
            {
                imageShowCVV.Visibility = Visibility.Visible;
                imageHideCVV.Visibility = Visibility.Hidden; 
            }
            else
            {
                imageShowCVV.Visibility = Visibility.Hidden;
                imageHideCVV.Visibility = Visibility.Visible;
            }
            SwapCVV();
        }

        private void imageProfile_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageProfile(user));
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageCreateCard(user));
        }

        private void comboBoxCardChoose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = Convert.ToInt32(comboBoxCardChoose.SelectedValue);
            client = PageClass.connectDB.Client.FirstOrDefault(x => x.id == selectedIndex);
            FillingCardDescription(client);
        }

        private void buttonTransaction_Click(object sender, RoutedEventArgs e)
        {
            if(CheckForTransaction())
                PageClass.frameObject.Navigate(new PageTransaction(client));
            else
                MessageBox.Show("У вас нет карт",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        private void buttonPageTransaction_Click(object sender, RoutedEventArgs e)
        {
            var client = PageClass.connectDB.Client.FirstOrDefault(x => x.userid == user.id);
            if (CheckForTransaction())
                PageClass.frameObject.Navigate(new PageClientTransaction(client));
            else
                MessageBox.Show("У вас нет карт", 
                    "Warning", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
        }
    }
}
