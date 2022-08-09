using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TP_F
{
    public partial class InventoryWin : Window
    {
        public static AddingCaseWin AddingCaseW;
        private static string callFlag;
        public InventoryWin(string flag)
        {
            InitializeComponent();
            MainWindow.Inventory_W = this;
            SetInf_SP.Visibility = Visibility.Hidden;
            callFlag = flag;
            if (callFlag == "ALL")
            {
                for (int i = 0; i < App.sysH.Inventory.Facilities.Count; i++)
                {
                    FACECreator.CreateInventoryItem(InventoryItems_WP, App.sysH.Inventory.Facilities[i],$"Facilities[{i}].uIN");
                }
            }
            else
            {
                if (callFlag == "M")
                {
                    for (int i = 0; i < App.sysH.Inventory.Facilities.Count; i++)
                    {
                        if (App.sysH.Inventory.Facilities[i] is Monitor) { FACECreator.CreateInventoryItem(InventoryItems_WP, App.sysH.Inventory.Facilities[i], $"Facilities[{i}].uIN"); }
                    }
                }
                if (callFlag == "K")
                {
                    for (int i = 0; i < App.sysH.Inventory.Facilities.Count; i++)
                    {
                        if (App.sysH.Inventory.Facilities[i] is Komp) { FACECreator.CreateInventoryItem(InventoryItems_WP, App.sysH.Inventory.Facilities[i], $"Facilities[{i}].uIN"); }
                    }
                }
                BackToMenu_B.Visibility = Visibility.Hidden;
                AddToWorkplace_B.Visibility = Visibility.Visible;
                Header_L.Content = "Выберите добавляемое оборудование";
            }
            InventoryItems_WP.DataContext = App.sysH.Inventory;
            FACECreator.ResizeInventoryFacilityList();
        }

        private void MoreInf_B_Click(object sender, RoutedEventArgs e)
        {
            MoreInfWin.kTemp = (Komp)FACEHandlers.selectedF;
            MainWindow.MoreInfW = new MoreInfWin();
            MainWindow.MoreInfW.ShowDialog();
        }
        private void Delete_B_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данное оборудование (после удаления его нельзя будет восстановить)", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            Loger.AppendLogEvent("Оборудование удалено",FACEHandlers.selectedF);
            FACEHandlers.DeleteFacility(((Button)sender).Uid);
            foreach (ToggleButton item in MainWindow.Inventory_W.InventoryItems_WP.Children)
            {
                if (item.Uid == ((Button)sender).Uid)
                {
                    MainWindow.Inventory_W.InventoryItems_WP.Children.Remove(item); break;
                }
            }
            SetInf_SP.Visibility = Visibility.Hidden;
            MoreInf_B.IsEnabled = Delete_B.IsEnabled = Edit_B.IsEnabled = false;
            FACECreator.ResizeInventoryFacilityList();
        }
        private void Edit_B_Click(object sender, RoutedEventArgs e)
        {
            if (FACEHandlers.selectedF.fType == "K")
            { App.FacilityEditorW = new FacilityEditorWin(new WorkPlace { kompukter = (Komp)FACEHandlers.selectedF },"EDIT"); }
            if (FACEHandlers.selectedF.fType == "M")
            { App.FacilityEditorW = new FacilityEditorWin(new WorkPlace { monitor = (Monitor)FACEHandlers.selectedF }, "EDIT"); }
            App.FacilityEditorW.ShowDialog();
            FACEHandlers.RebootInventoryInfo();
        }
        private void AddFacility_B_Click(object sender, RoutedEventArgs e)
        {
            AddingCaseW = new AddingCaseWin();
            AddingCaseW.ShowDialog();
            if (AddingCaseW.tempF == null) { return; }
            
            if (callFlag == "K" && AddingCaseW.tempF is Komp == true || callFlag == "M" && AddingCaseW.tempF is Monitor == true || callFlag == "ALL")
            {
                FACECreator.CreateInventoryItem(InventoryItems_WP, AddingCaseW.tempF, $"Facilities[{App.sysH.Inventory.Facilities.Count - 1}].uIN");
                FACECreator.ResizeInventoryFacilityList();
            }
            else
            {
                MessageBox.Show("ВНИМАНИЕ! Вы находитесь в режиме добавления оборудования на рабочее место. Тип созданного вами оборудования не соответствует типу оборудования, добавляемого на рабочее место, поэтому оно не будет отображаться в списке доступного оборудования.", "Предупреждение", MessageBoxButton.OK,MessageBoxImage.Information);
            }
            Loger.AppendLogEvent("Оборудование создано", AddingCaseW.tempF);
            App.FacilityEditorW.Close();
            AddingCaseW.Close();
            DataContext = App.sysH.Inventory;
        }

        private void BackToMenu_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Main_W.Owner = null;
            MainWindow.Inventory_W.Owner = MainWindow.Main_W;
            MainWindow.Inventory_W.Close();
            MainWindow.Main_W.Show();
        }

        private void AddToWorkplace_B_Click(object sender, RoutedEventArgs e)
        {
            if(FACEHandlers.selectedF == null)
            { MessageBox.Show("Вы должны выбрать оборудование, которое хотите добавить", "Сообщение", MessageBoxButton.OK);return; }

            Loger.AppendLogEvent("Оборудование перемещено на рабочее место", FACEHandlers.selectedF, FACEHandlers.selectedK, FACEHandlers.selectetWP);
            if (FACEHandlers.selectedF is Monitor)
            {
                App.sysH.Inventory.Facilities.Remove(FACEHandlers.selectedF);
                FACEHandlers.selectetWP.monitor = (Monitor)FACEHandlers.selectedF;
                MainWindow.Auditories_W.DeleteFacilityM_B.Uid = FACEHandlers.selectedF.uID + "_M";
                SysHelperDB.ExecCom($"UPDATE РабочееМесто SET ID_монитора_F = {((Monitor)FACEHandlers.selectedF).monitorID} WHERE ID_РабМеста_P = {FACEHandlers.selectetWP.workPlaceID}");
                MainWindow.Auditories_W.AddMonitor_B.Visibility = Visibility.Collapsed;
                MainWindow.Auditories_W.MonicInf_Panel.Visibility = Visibility.Visible;
                MainWindow.Auditories_W.MoreInf_B.IsEnabled = false;
            }
            else
            {
                App.sysH.Inventory.Facilities.Remove(FACEHandlers.selectedF);
                FACEHandlers.selectetWP.kompukter = (Komp)FACEHandlers.selectedF;
                MainWindow.Auditories_W.DeleteFacilityK_B.Uid = FACEHandlers.selectedF.uID + "_K";
                SysHelperDB.ExecCom($"UPDATE РабочееМесто SET ID_компьютера_F = {((Komp)FACEHandlers.selectedF).kompID} WHERE ID_РабМеста_P = {FACEHandlers.selectetWP.workPlaceID}");
                MainWindow.Auditories_W.AddKomputer_B.Visibility = Visibility.Collapsed;
                MainWindow.Auditories_W.KompInf_Panel.Visibility = Visibility.Visible;
                MainWindow.Auditories_W.MoreInf_B.IsEnabled = true;
            }
            MainWindow.Auditories_W.Edit_B.IsEnabled = true;
            FACEHandlers.selectetWP.UpdateFACE();
            MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = null;
            MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = FACEHandlers.selectedK;
            MainWindow.Auditories_W.SetInf_SP.DataContext = null;
            MainWindow.Auditories_W.SetInf_SP.DataContext = FACEHandlers.selectetWP;
            Close();
        }
    }
}
