using Card_management_system.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Card_management_system.Classes
{
    static class DataBase
    {
        private static void SaveChangesDataBase(string message)
        {
            try
            {
                PageClass.connectDB.SaveChanges();
                MessageBox.Show(message, 
                    "Объявление", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
    {
    }
}
