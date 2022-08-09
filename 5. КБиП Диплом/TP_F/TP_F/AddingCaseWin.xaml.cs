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
using System.Windows.Shapes;

namespace TP_F
{
    public partial class AddingCaseWin : Window
    {
        public Facility tempF;
        public AddingCaseWin()
        {
            InitializeComponent();
            tempF = null;
        }

        private void NewKomputer_B_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            App.FacilityEditorW = new FacilityEditorWin(new WorkPlace { kompukter = new Komp() }, "ADD");
            App.FacilityEditorW.ShowDialog();
            Close();
        }

        private void NewMonitor_B_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            App.FacilityEditorW = new FacilityEditorWin(new WorkPlace { monitor = new Monitor() }, "ADD");
            App.FacilityEditorW.ShowDialog();
            Close();
        }
    }
}
