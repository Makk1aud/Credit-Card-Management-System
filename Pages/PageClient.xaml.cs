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
            textBlockBalance.Text = client.balance.ToString();
            FillingCardDescription(client);
        }

        private void FillingCardDescription(Client client)
        {
            if (client.cardid == null)
                return;
            textBlockClientName.Text = $"{user.name} {user.surname}";
            textBlockBalance.Text = client.balance.ToString();
            textBlockCardCvv.Text = client.cvv.ToString();
            textBlockCardDate.Text = $"{client.carddate.Value.Month}/{client.carddate.Value.Year}";
            //textBlockCardType.Text = PageClass.connectDB.Cards.FirstOrDefault(x => x.id == client.cardid).name;
            textBlockCardNumber.Text = client.cardnumber.ToString();

        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imageProfile_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Nice");
        }
    }
}
