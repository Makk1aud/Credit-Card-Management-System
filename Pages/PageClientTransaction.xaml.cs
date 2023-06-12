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
    /// Логика взаимодействия для PageClientTransaction.xaml
    /// </summary>
    public partial class PageClientTransaction : Page
    {
        Client client;
        public PageClientTransaction(Client client)
        {
            InitializeComponent();
            dataGridTransactions.ItemsSource = PageClass.connectDB.Transactions.Where(x => x.senderid == client.id || x.recipientid == client.id).ToList();
            this.client = client;
            comboBoxMoneySort.ItemsSource = new List<string>() { "По возрастанию", "По убыванию" };
        }

        public List<Transactions> DefaultList() => PageClass.connectDB.Transactions.Where(x => x.senderid == client.id).ToList();

        public void FillingDataGrid()
        {
            var listSort = PageClass.connectDB.Transactions.Where(x => x.senderid == client.id || x.recipientid == client.id).ToList();
            ComboBoxSort(ref listSort);
            TextBoxSort(ref listSort);
            DatePickerSort(ref listSort);
            dataGridTransactions.ItemsSource = listSort;
        }

        private void ComboBoxSort(ref List<Transactions> listSort)
        {
            if (comboBoxMoneySort.SelectedIndex == 0)
                listSort = listSort.OrderBy(x => x.moneysum).ToList();
            else if (comboBoxMoneySort.SelectedIndex == 1)
                listSort = listSort.OrderByDescending(x => x.moneysum).ToList();
        }

        private void TextBoxSort( ref List<Transactions> listSort)
        {
            if(textBoxMoneySort.Text != String.Empty)
                listSort = listSort.Where(x => x.moneysum > int.Parse(textBoxMoneySort.Text)).ToList();
        }

        private void DatePickerSort(ref List<Transactions> listSort)
        {
            if(datePickerTransactionSort.SelectedDate != null)
                listSort = listSort.Where(x => x.transactiondate == datePickerTransactionSort.SelectedDate).ToList();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.GoBack();
        }

        private void datePickerTransactionSort_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FillingDataGrid();
        }

        private void comboBoxMoneySort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillingDataGrid();
        }

        private void textBoxMoneySort_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillingDataGrid();
        }
    }
}
