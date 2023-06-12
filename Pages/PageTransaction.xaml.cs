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
    /// Логика взаимодействия для PageTransaction.xaml
    /// </summary>
    public partial class PageTransaction : Page
    {
        private Client client;
        private Client recipientClient;
        private Users recipientUser;
        private Transactions transactions;
        public PageTransaction(Client client)
        {
            InitializeComponent();
            this.client = client;
            comboBoxSelectMethod.ItemsSource = new List<string>() { "По номеру телефона", "По номеру карты" };
            FillingComboBox.ComboBoxItems(comboBoxSenderCard, "id", "cardnumber");
            comboBoxSenderCard.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid == client.userid).ToList();
            FillingComboBox.ComboBoxItems(comboBoxSelectRecipientCard, "id", "cardnumber");
        }

        private void comboBoxSelectMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeStatusOfStackPanel changeStatus = new ChangeStatusOfStackPanel();
            textBoxRecipientCardNum.Clear();
            textBoxTelephone.Clear();
            if (comboBoxSelectMethod.SelectedIndex == 0)
            {
                changeStatus.ChangeStatus(stackPanelCard, false);
                changeStatus.ChangeStatus(stackPanelTelephone, true);
                return;
            }
            changeStatus.ChangeStatus(stackPanelCard, true);
            changeStatus.ChangeStatus(stackPanelTelephone, false);
        }

        private void textBoxTelephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxTelephone.Text.Count(t => t == '+') == 0)
                textBoxTelephone.Text = "+" + textBoxTelephone.Text;
        }

        private void ChooseMethodTransaction()
        {
            string recipientCardNum;
            if (stackPanelCard.IsEnabled)
            {
                recipientCardNum = Convert.ToInt64(textBoxRecipientCardNum.Text).ToString("#### #### #### ####").ToString();
                FillingTransactions(PageClass.connectDB.Client.FirstOrDefault(x => x.cardnumber == recipientCardNum).id, recipientCardNum);
                recipientClient = PageClass.connectDB.Client.FirstOrDefault(x => x.cardnumber == recipientCardNum);
            }
            else
            {
                FillingTransactions(PageClass.connectDB.Client.FirstOrDefault(x => x.userid == recipientUser.id).id,
                    (comboBoxSelectRecipientCard.SelectedItem as Client).cardnumber);
                recipientClient = PageClass.connectDB.Client.FirstOrDefault(x => x.userid == recipientUser.id);
            }

        }

        private void FillingTransactions(int recipientId, string recipientCardNum)
        {
            transactions.recipientid= recipientId;
            transactions.recipientcardnum= recipientCardNum;
        }

        private bool CheckMoneySum(TextBox textBox)
        {
            decimal num;
            return Decimal.TryParse(textBox.Text, out num) && int.Parse(textBox.Text) <= (comboBoxSenderCard.SelectedItem as Client).balance;
        }

        public bool CheckSenderCard(TextBox textBox)
        {
            return PageClass.connectDB.Client.FirstOrDefault(x => x.cardnumber == textBox.Text) == (comboBoxSenderCard.SelectedItem as Client);
        }

        // Сделать вычитание суммы
        public void MinusMoneySum()
        {
            var client = PageClass.connectDB.Client.FirstOrDefault(x=>x.cardnumber == (comboBoxSenderCard.SelectedItem as Client).cardnumber);
            client.balance -= Convert.ToInt32(textBoxMoneySum.Text);
        }

        private void buttonTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckMoneySum(textBoxMoneySum) && !CheckSenderCard(textBoxRecipientCardNum))
                return;
            try
            {
                transactions = new Transactions()
                {
                    senderid = client.id,
                    sendercardnum = (comboBoxSenderCard.SelectedItem as Client).cardnumber,
                    moneysum = Convert.ToDecimal(textBoxMoneySum.Text),
                    transactiondate = DateTime.Now
                };
                ChooseMethodTransaction();
                PageClass.connectDB.Transactions.Add(transactions);
                client.balance -= Convert.ToInt32(textBoxMoneySum.Text);
                recipientClient.balance += Convert.ToInt32(textBoxMoneySum.Text);
                DataBaseCardManagement.SaveChangesDataBase("Успешно");
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Проверьте вводимые данные",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void textBoxTelephone_LostFocus(object sender, RoutedEventArgs e)
        {
            recipientUser = PageClass.connectDB.Users.FirstOrDefault(x => x.number == textBoxTelephone.Text);
            if(recipientUser != null && client.userid != recipientUser.id) 
                comboBoxSelectRecipientCard.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid == recipientUser.id).ToList();
            else
                comboBoxSelectRecipientCard.ItemsSource = null;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageClient(PageClass.connectDB.Users.FirstOrDefault(x => x.id == client.userid)));
        }

        private void textBoxMoneySum_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Clear();
        }

        private void imageClipboard_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxTelephone.Text = Clipboard.GetText();
        }
    }
}
