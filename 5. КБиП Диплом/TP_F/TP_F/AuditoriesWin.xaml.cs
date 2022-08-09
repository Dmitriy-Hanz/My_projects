using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TP_F
{
    public partial class AuditoriesWin : Window
    {
        public static WorkplaceEditorWin WorkplaceEditor_W;
        public static KabinetEditorWin KabinetEditor_W;
        public AuditoriesWin()
        {
            InitializeComponent();
            SetInf_SP.Visibility = Visibility.Hidden;
            WriteToInventoryK_B.Click += FACEHandlers.WriteToInventory_Click;
            WriteToInventoryM_B.Click += FACEHandlers.WriteToInventory_Click;
            for (int i = 0; i < App.sysH.kabinets.Count; i++)
            {
                FACECreator.CreateKabinetMP(AuditoriesList_WP, $"kabinets[{i}]");
            }
            AuditoriesList_WP.DataContext = null;
            AuditoriesList_WP.DataContext = App.sysH;
        }

        private void MoreInf_B_Click(object sender, RoutedEventArgs e)
        {
            MoreInfWin.kTemp = FACEHandlers.selectetWP.kompukter;
            MainWindow.MoreInfW = new MoreInfWin();
            MainWindow.MoreInfW.ShowDialog();
        }

        private void DeleteFacility_B_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данное оборудование (после удаления его нельзя будет восстановить)", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            FACEHandlers.DeleteFacility(((Button)sender).Uid);
            FACEHandlers.selectetWP.UpdateFACE();
            FACECreator.RebootInfoAuditories(FACEHandlers.selectetWP);
            MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = null;
            MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = FACEHandlers.selectedK;
            MainWindow.Auditories_W.SetInf_SP.DataContext = null;
            MainWindow.Auditories_W.SetInf_SP.DataContext = FACEHandlers.selectetWP;
        }

        private void BackToMenu_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Main_W.Owner = null;
            MainWindow.Auditories_W.Owner = MainWindow.Main_W;
            MainWindow.Auditories_W.Close();
            MainWindow.Main_W.Show();
        }

        private void Edit_B_Click(object sender, RoutedEventArgs e)
        {
            App.FacilityEditorW = new FacilityEditorWin(FACEHandlers.selectetWP,"EDIT");
            App.FacilityEditorW.ShowDialog();
            MainWindow.Auditories_W.SetInf_SP.DataContext = null;
            MainWindow.Auditories_W.SetInf_SP.DataContext = FACEHandlers.selectetWP;
        }

        private async void KabinetSearchField_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(((TextBox)sender).Text == "") { AuditoriesList_WP.Height = AuditoriesList_WP.Children.Count * 76 + 6; }
            await Task.Run(() => KabinetMiniSearch(sender));
        }
        private async void WorkplaceSearchField_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "") { WorkPlacesList_WP.Height = WorkPlacesList_WP.Children.Count * 75 + 6; }
            await Task.Run(() => WorkplaceMiniSearch(sender));
        }
        private void KabinetMiniSearch(object sender)
        {
            Thread.Sleep(500);
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,(ThreadStart)delegate ()
                {
                    AuditoriesList_WP.Height = 3;
                    foreach (ToggleButton item in AuditoriesList_WP.Children)
                    {
                        item.Visibility = Visibility.Collapsed;
                        if (((Label)((Canvas)((Border)item.Content).Child).Children[1]).Content.ToString().Contains(((TextBox)sender).Text) == true)
                        {
                            item.Visibility = Visibility.Visible;
                            AuditoriesList_WP.Height += 76; 
                        }
                    }
                }
            );
        }
        private void WorkplaceMiniSearch(object sender)
        {
            Thread.Sleep(500);
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                WorkPlacesList_WP.Height = 3;
                foreach (ToggleButton item in WorkPlacesList_WP.Children)
                {
                    item.Visibility = Visibility.Collapsed;
                    if (((Label)((Canvas)((Border)item.Content).Child).Children[2]).Content.ToString().Contains(((TextBox)sender).Text) == true)
                    {
                        item.Visibility = Visibility.Visible;
                        WorkPlacesList_WP.Height += 75;
                    }
                }
            }
            );
        }

        private void AddMonitor_B_Click(object sender, RoutedEventArgs e)
        {
            FACEHandlers.selectedF = null;
            MainWindow.Inventory_W = new InventoryWin("M");
            MainWindow.Inventory_W.ShowDialog();
        }
        private void AddKomputer_B_Click(object sender, RoutedEventArgs e)
        {
            FACEHandlers.selectedF = null;
            MainWindow.Inventory_W = new InventoryWin("K");
            MainWindow.Inventory_W.ShowDialog();
        }
        private void AddKabinet_B_Click(object sender, RoutedEventArgs e)
        {
            KabinetEditor_W = new KabinetEditorWin();
            KabinetEditor_W.ShowDialog();
        }
        private void AddWorkPlace_B_Click(object sender, RoutedEventArgs e)
        {
            WorkplaceEditor_W = new WorkplaceEditorWin();
            WorkplaceEditor_W.ShowDialog();
            FACEHandlers.selectedK.workPlaceCount = FACEHandlers.selectedK.workPlaces.Count;
            MainWindow.Auditories_W.AuditoriesList_WP.DataContext = null;
            MainWindow.Auditories_W.AuditoriesList_WP.DataContext = App.sysH;
        }
        private void CreateReport_B_Click(object sender, RoutedEventArgs e)
        {
            ExcelReport EXR = new ExcelReport();
            EXR.CreateReport_General();
        }
        
        private void EditKabinet_B_Click(object sender, RoutedEventArgs e)
        {
            KabinetEditor_W = new KabinetEditorWin(FACEHandlers.selectedK);
            KabinetEditor_W.ShowDialog();
            AuditoriesList_WP.DataContext = null;
            AuditoriesList_WP.DataContext = App.sysH;
        }
        private void EditWorkPlace_B_Click(object sender, RoutedEventArgs e)
        {
            WorkplaceEditor_W = new WorkplaceEditorWin(FACEHandlers.selectetWP);
            WorkplaceEditor_W.ShowDialog();
            WorkPlacesList_WP.DataContext = null;
            WorkPlacesList_WP.DataContext = FACEHandlers.selectedK;
        }


    }
}
