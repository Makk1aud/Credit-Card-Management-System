using Card_management_system.Classes;
using Card_management_system.DataApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //Function for sorting of data grid it will use when one of items from sorting elements change
        public void FillingDataGrid()
        {
            var listSort = PageClass.connectDB.Transactions.Where(x => x.senderid == client.id || x.recipientid == client.id).ToList();
            ComboBoxSort(ref listSort);
            TextBoxSort(ref listSort);
            DatePickerSort(ref listSort);
            SortByRadioButton(ref listSort);
            dataGridTransactions.ItemsSource = listSort;
        }
        // sort by combo box 
        private void ComboBoxSort(ref List<Transactions> listSort)
        {
            if (comboBoxMoneySort.SelectedIndex == 0)
                listSort = listSort.OrderBy(x => x.moneysum).ToList();
            else if (comboBoxMoneySort.SelectedIndex == 1)
                listSort = listSort.OrderByDescending(x => x.moneysum).ToList();
        }
        //sort by text box
        private void TextBoxSort( ref List<Transactions> listSort)
        {
            if(textBoxMoneySort.Text != String.Empty)
                listSort = listSort.Where(x => x.moneysum > int.Parse(textBoxMoneySort.Text)).ToList();
        }

        //sort by date picker
        private void DatePickerSort(ref List<Transactions> listSort)
        {
            if(datePickerTransactionSort.SelectedDate != null)
                listSort = listSort.Where(x => x.transactiondate.Value.Date == datePickerTransactionSort.SelectedDate.Value.Date).ToList();
        }

        private void SortByRadioButton(ref List<Transactions> listSort)
        {
            if((bool)radioButtonBothVariants.IsChecked)
                listSort = listSort.Where(x => x.senderid == client.id || x.recipientid == client.id).ToList();
            else if((bool)radioButtonSender.IsChecked)
                listSort = listSort.Where(x => x.senderid == client.id).ToList();
            else
                listSort = listSort.Where(x => x.recipientid == client.id).ToList();
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

        private void radioButtonChange(object sender, RoutedEventArgs e)
        {
            FillingDataGrid();
        }
    }
}
