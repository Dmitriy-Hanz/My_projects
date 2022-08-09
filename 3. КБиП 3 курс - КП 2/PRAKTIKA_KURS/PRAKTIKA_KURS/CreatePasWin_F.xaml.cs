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

namespace PRAKTIKA_KURS
{
    public partial class CreatePasWin_F : Window
    {
        public CreatePasWin_F()
        {
            InitializeComponent();
        }
        public bool Validator()
        {
            if (int.TryParse(PasFloor_TB.Text, out int d1) == false) { MessageBox.Show("Введенные данные имеют неверный формат"); return false; }
            if (int.TryParse(PasWeight_TB.Text, out int d2) == false) { MessageBox.Show("Введенные данные имеют неверный формат"); return false; }
            if (int.TryParse(PasTargetFloor_TB.Text, out int d3) == false) { MessageBox.Show("Введенные данные имеют неверный формат"); return false; }
            if (PasFloor_TB.Text == PasTargetFloor_TB.Text) { MessageBox.Show("Этаж пассажира и целевой этаж не могут совпадать"); return false; }
            if (int.Parse(PasFloor_TB.Text) < 1 || int.Parse(PasWeight_TB.Text) < 1 || int.Parse(PasTargetFloor_TB.Text) < 1)
            { MessageBox.Show("Значения этажей и веса не могут быть отрицательными или нулевыми"); return false; }
            return true;
        }
        private void Create_B_Click(object sender, RoutedEventArgs e)
        {
            if (Validator() == false) { return; }
            Passenger p = new Passenger(int.Parse(PasFloor_TB.Text),int.Parse(PasWeight_TB.Text),int.Parse(PasTargetFloor_TB.Text));
            if (MainWindow.FLOORS[p.BasedFloor] == null) { MainWindow.FLOORS[p.BasedFloor] = new List<Passenger>(); }
            MainWindow.FLOORS[p.BasedFloor].Add(p);
            MainWindow.TaR.genPasCount++;
            MainWindow.TaR.InformationTargeting(MainWindow.Лифт);
        }

        private void Out_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.CreatePasWin.Close();
        }
    }
}
