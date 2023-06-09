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

namespace Card_management_system.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageAdminListOfCards.xaml
    /// </summary>
    public partial class PageAdminListOfCards : Page
    {
        Users users;
        public PageAdminListOfCards(Users users)
        {
            InitializeComponent();
            dataGridListOfUsers.ItemsSource = PageClass.connectDB.Users.ToList();
            this.users = users;
        }

        private void buttonAboutUser_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageAdminAboutUser(dataGridListOfUsers.SelectedItem as Users));
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.GoBack();
        }

        private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            DataBaseCardManagement.SaveChangesDataBase("Успешно");
        }

        private void textBoxNameSort_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
