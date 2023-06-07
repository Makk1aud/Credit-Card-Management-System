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
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        Users user;
        public PageAdmin(Users user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void buttonClientPage_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageClient(user));
        }

        private void buttonListOfCards_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageAdminListOfCards(user));
        }
    }
}
