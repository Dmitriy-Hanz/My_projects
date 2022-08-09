using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Census.utils
{
    class TypicalActions
    {
        public static void GoToWindow<OwnerType, TargetType>() where OwnerType : Window where TargetType : Window, new()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is OwnerType)
                {
                    TargetType win = new TargetType();
                    item.Hide();
                    win.Show();
                    item.Owner = win;
                    item.Close();
                }
            }
        }
        public static void CloseWindow<TargetType>() where TargetType : Window
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is TargetType)
                {
                    item.Close();
                }
            }
        }
    }
}
