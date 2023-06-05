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
            if(comboBoxSelectMethod.SelectedIndex == 0)
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
            }
            else
                FillingTransactions(PageClass.connectDB.Client.FirstOrDefault(x => x.userid == recipientUser.id).id,
                    (comboBoxSelectRecipientCard.SelectedItem as Client).cardnumber);
        }

        private void FillingTransactions(int recipientId, string recipientCardNum)
        {
            transactions.recipientid= recipientId;
            transactions.recipientcardnum= recipientCardNum;
        }

        private void buttonTransaction_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                transactions = new Transactions()
                {
                    senderid = client.id,
                    sendercardnum = (comboBoxSenderCard.SelectedItem as Client).cardnumber,
                    moneysum = Convert.ToDecimal(textBoxMoneySum.Text),
                    transactiondata = DateTime.Now
                };
                ChooseMethodTransaction();
                PageClass.connectDB.Transactions.Add(transactions);
                DataBaseCardManagement.SaveChangesDataBase("Успешно");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Все поля должны быть заполнены");
            }
        }

        private void textBoxTelephone_LostFocus(object sender, RoutedEventArgs e)
        {
            recipientUser = PageClass.connectDB.Users.FirstOrDefault(x => x.number == textBoxTelephone.Text);
            if(recipientUser != null)
                comboBoxSelectRecipientCard.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid == recipientUser.id).ToList();
            else
                comboBoxSelectRecipientCard.ItemsSource = null;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.GoBack();
        }

        private void textBoxMoneySum_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Clear();
        }
    }
}
