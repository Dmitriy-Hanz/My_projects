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
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;


namespace TP_F
{
    public partial class MainWindow : Window
    {
        public static AuditoriesWin Auditories_W;
        public static MainWindow Main_W;
        public static InventoryWin Inventory_W;
        public static MoreInfWin MoreInfW;
        public static HistoryWin HistoryW;

        public MainWindow()
        {
            InitializeComponent();
            Main_W = this;
            App.sysH = new SysHelperDB();
            FACEHandlers fHandler = new FACEHandlers();//нужен для ивентов
            App.sysH.ReadAll();
            App.Current.Exit += App.sysH.WhenExit;
        }

        private void Auditories_B_Click(object sender, RoutedEventArgs e)
        {
            Auditories_W = new AuditoriesWin();
            Main_W.Hide();
            Auditories_W.Show();
            Main_W.Owner = Auditories_W;
        }
        private void Finder_B_Click(object sender, RoutedEventArgs e)
        {
            App.Finder_W = new FinderWin();
            Main_W.Hide();
            App.Finder_W.Show();
            Main_W.Owner = App.Finder_W;
        }
        private void Inventory_B_Click(object sender, RoutedEventArgs e)
        {
            Inventory_W = new InventoryWin("ALL");
            Main_W.Hide();
            Inventory_W.Show();
            Main_W.Owner = Inventory_W;
        }

        private void Manual_B_Click(object sender, RoutedEventArgs e)
        {
            Process.Start($"{Environment.CurrentDirectory}\\SysHelper.chm");
        }
        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void History_B_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists($"{Environment.CurrentDirectory}\\FacilityLog.txt") == false)
            { MessageBox.Show("Журнал движений пуст", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);return; }
            HistoryW = new HistoryWin();
            HistoryW.ShowDialog();
        }
    }
}
