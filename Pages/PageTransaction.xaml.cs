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
        Client client;
        Users recipientUser;
        public PageTransaction(Client client)
        {
            InitializeComponent();
            this.client = client;
            comboBoxSelectMethod.ItemsSource = new List<string>() { "По номеру телефона", "По номеру карты" };
            comboBoxSenderCard.SelectedValuePath = "id";
            comboBoxSenderCard.DisplayMemberPath = "cardnumber";
            comboBoxSenderCard.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid == client.userid).ToList();

            comboBoxSelectRecipientCard.SelectedValuePath = "id";
            comboBoxSelectRecipientCard.DisplayMemberPath = "cardnumber";
        }
        // сделать также с паролем вначале
        private void TurnOffControl(StackPanel stackPanel ,bool turner)
        {
            if (turner)
                stackPanel.Visibility = Visibility.Visible;
            else
                stackPanel.Visibility = Visibility.Hidden;
            stackPanel.IsEnabled = turner;
        }

        private void TurnOffControl(StackPanel stackPanel ,bool turner)
        {
            if (turner)
                stackPanel.Visibility = Visibility.Visible;
            else
                stackPanel.Visibility = Visibility.Hidden;
            stackPanel.IsEnabled = turner;
        }

        private void comboBoxSelectMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxSelectMethod.SelectedIndex == 0)
            {
                TurnOffControl(stackPanelCard, false);
                TurnOffControl(stackPanelTelephone, true);
                return;
            }
            TurnOffControl(stackPanelCard, true);
            TurnOffControl(stackPanelTelephone, false);
        }

        private void textBoxTelephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxTelephone.Text.Count(t => t == '+') == 0)
                textBoxTelephone.Text = "+" + textBoxTelephone.Text;
        }

        private StackPanel IsEnableStackPanel() => stackPanelCard.IsEnabled ? stackPanelCard : stackPanelTelephone;

        private void buttonTransaction_Click(object sender, RoutedEventArgs e)
        {
            string recipientCardNum;
            var transactions = new Transactions()
            {
                senderid = client.id,
                sendercardnum = (comboBoxSenderCard.SelectedItem as Client).cardnumber,
                moneysum = Convert.ToDecimal(textBoxMoneySum.Text)
            };
            if (stackPanelCard.IsEnabled)
            {
                recipientCardNum = Convert.ToInt64(textBoxRecipientCardNum.Text).ToString("#### #### #### ####").ToString();
                transactions.recipientid = PageClass.connectDB.Client.FirstOrDefault(x => x.cardnumber == recipientCardNum).id;
                transactions.recipientcardnum = recipientCardNum;
            }
            else
            {
                transactions.recipientid = PageClass.connectDB.Client.FirstOrDefault(x => x.userid == recipientUser.id).id;
                transactions.recipientcardnum = (comboBoxSelectRecipientCard.SelectedItem as Client).cardnumber;
            }
            PageClass.connectDB.Transactions.Add(transactions);
            try
            {
                PageClass.connectDB.SaveChanges();
                MessageBox.Show("Получилось");
            }
            catch (Exception ex)
            {

            }
            
        }

        private void textBoxTelephone_LostFocus(object sender, RoutedEventArgs e)
        {
            recipientUser = PageClass.connectDB.Users.FirstOrDefault(x => x.number == textBoxTelephone.Text);
            if(recipientUser != null)
                comboBoxSelectRecipientCard.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid == recipientUser.id).ToList();
        }
    }
}
