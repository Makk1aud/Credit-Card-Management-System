using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Card_management_system.Classes
{
    static class FillingComboBox
    {
        public static void ComboBoxItems(ComboBox comboBox ,string selectedValuepath, string displayMemberPath)
        {
            comboBox.SelectedValuePath= selectedValuepath;
            comboBox.DisplayMemberPath= displayMemberPath;
        }
    }
}
