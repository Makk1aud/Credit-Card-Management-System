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
    /// Логика взаимодействия для PageTransaction.xaml
    /// </summary>
    public partial class PageTransaction : Page
    {
        public PageTransaction(Client client)
        {
            InitializeComponent();
            comboBoxSelectMethod.ItemsSource = new List<string>() { "По номеру телефона", "По номеру карты" };
            comboBoxSenderCard.SelectedValuePath = "id";
            comboBoxSenderCard.DisplayMemberPath = "cardnumber";
            comboBoxSenderCard.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid == client.userid).ToList();

            comboBoxSelectRecipientCard.SelectedValuePath = "id";
            comboBoxSelectRecipientCard.DisplayMemberPath = "cardnumber";
        }

        private void comboBoxSelectMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxSelectMethod.SelectedIndex == 0)
            {
                stackPanelCard.Visibility = Visibility.Hidden;
                stackPanelTelephone.Visibility = Visibility.Visible;
                return;
            }
            stackPanelCard.Visibility = Visibility.Visible;
            stackPanelTelephone.Visibility = Visibility.Hidden;
        }

        private void textBoxTelephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxTelephone.Text.Count(t => t == '+') == 0)
                textBoxTelephone.Text = "+" + textBoxTelephone.Text;
        }

        private void buttonTransaction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textBoxTelephone_LostFocus(object sender, RoutedEventArgs e)
        {
            var recipientUser = PageClass.connectDB.Users.FirstOrDefault(x => x.number == textBoxTelephone.Text);
            if(recipientUser != null)
                comboBoxSelectRecipientCard.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid == recipientUser.id).ToList();
        }
    }
}
