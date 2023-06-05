using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Card_management_system.Classes
{
    internal class ChangeStatusOfStackPanel : ChangeStatusOfControl
    {
        public override void ChangeStatus(object obj, bool turner)
        {
            StackPanel stackPanel = obj as StackPanel;
            if (turner)
                stackPanel.Visibility = Visibility.Visible;
            else
                stackPanel.Visibility = Visibility.Hidden;
            stackPanel.IsEnabled = turner;
        }
    }
}
