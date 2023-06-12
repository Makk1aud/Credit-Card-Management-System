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
    /// Логика взаимодействия для PageProfile.xaml
    /// </summary>
    public partial class PageProfile : Page
    {
        Users user;
        public PageProfile(Users user)
        {
            InitializeComponent();
            this.user = user;
            dataGridAboutUser.ItemsSource = PageClass.connectDB.Users.Where(x => x.id == user.id).ToList();
            dataGridUserCards.ItemsSource = PageClass.connectDB.Client.Where(x => x.userid== user.id).ToList();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.GoBack();
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageLogin());
        }

        private void buttonHistoryOfTransaction_Click(object sender, RoutedEventArgs e)
        {
            PageClass.frameObject.Navigate(new PageClientTransaction(PageClass.connectDB.Client.FirstOrDefault(x => x.userid == user.id)));
        }
    }
}
