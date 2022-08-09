using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace TP_F
{
    public partial class FinderWin : Window
    {
        private string requestTemplate = "";
        private string requestJoins = "";
        private string requestConditions = "";
        private DataTable DICK = new DataTable();
        public FinderWin()
        {
            InitializeComponent();
            FacilityType_CB.SelectionChanged += FacilityType_CB_SelectionChanged;
            RequestResult_DG.SelectedCellsChanged += FACEHandlers.FinderFacilityItem_Click;
        }

        private void Find_B_Click(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxItem)FacilityType_CB.SelectedItem).Content.ToString() == "Компьютер")
            {
                if (KomputerNameFlag_CB.IsChecked == true) { if (KomputerNameSS_TB.Text.Length > 50) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Имя\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
                if (KomputerProcessorFlag_CB.IsChecked == true) { if (KomputerProcessorSS_TB.Text.Length > 50) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Процессор\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
                if (KomputerVideoCardFlag_CB.IsChecked == true) { if (KomputerVideoCardSS_TB.Text.Length > 150) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Видеокарта\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
                if (KomputerDiskFlag_CB.IsChecked == true) { if (KomputerDiskSS_TB.Text.Length > 100) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Жесткий диск\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
                if (KomputerOZUSizeFlag_CB.IsChecked == true) { if (int.TryParse(KomputerOZUSizeSS_TB.Text, out int temp) == false) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Объем ОЗУ\", имеет некорректный формат", "Сообщение", MessageBoxButton.OK); return; } }
                if (KomputerSystemFlag_CB.IsChecked == true) { if (KomputerSystemSS_TB.Text.Length > 100) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"ОС\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
            }
            else
            {
                if (MonitorModelFlag_CB.IsChecked == true) { if (MonitorModelSS_TB.Text.Length > 70) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Модель\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
                if (MonitorDiagonalFlag_CB.IsChecked == true)
                {
                    if (double.TryParse(MonitorDiagonalSS_TB.Text, out double temp) == false) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Диагональ\", имеет некорректный формат", "Сообщение", MessageBoxButton.OK); return; }
                    if (MonitorDiagonalSS_TB.Text.Length > 10) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Диагональ\", слишком большое", "Сообщение", MessageBoxButton.OK); return; }
                }
                if (MonitorSideRatioFlag_CB.IsChecked == true) { if (MonitorSideRatioSS_TB.Text.Length > 5) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Соотношение сторон\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
                if (MonitorMatrixFlag_CB.IsChecked == true) { if (MonitorMatrixSS_TB.Text.Length > 3) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Матрица\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
                if (MonitorFreqFlag_CB.IsChecked == true) { if (MonitorFreqSS_TB.Text.Length > 3) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Частота\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
                if (MonitorResolutionFlag_CB.IsChecked == true) { if (MonitorResolutionSS_TB.Text.Length > 9) { MessageBox.Show("Значение, введенное в поле кретерия поиска \"Разрешение\", слишком большое", "Сообщение", MessageBoxButton.OK); return; } }
            }

            SetInf_SP.Visibility = Visibility.Hidden;
            DICK = new DataTable();
            requestTemplate = "";
            requestJoins = "";
            requestConditions = "";
            if (((ComboBoxItem)FacilityType_CB.SelectedItem).Content.ToString() == "Компьютер")
            {
                if (((CheckBox)((Canvas)KomputerNameSS_TB.Parent).Children[0]).IsChecked == true)
                { 
                    requestConditions += $"Компьютер.имя like '%{KomputerNameSS_TB.Text}%' and "; 
                }
                if (((CheckBox)((Canvas)KomputerProcessorSS_TB.Parent).Children[0]).IsChecked == true)
                {
                    requestJoins += "INNER JOIN СписокЦПУ ON ID_компьютера_P = СписокЦПУ.ID_Компьютера_F INNER JOIN ЦПУ ON ID_ЦПУ_F = ID_Процессора_P ";
                    requestConditions += $"ЦПУ.название like '%{KomputerProcessorSS_TB.Text}%' and ";
                }
                if (((CheckBox)((Canvas)KomputerVideoCardSS_TB.Parent).Children[0]).IsChecked == true)
                {
                    requestJoins += "INNER JOIN СписокГрафАдаптеров ON ID_компьютера_P = СписокГрафАдаптеров.ID_компьютера_F INNER JOIN ГрафическийАдаптер ON ID_графАдаптера_P = ID_графАдаптера_F ";
                    requestConditions += $"ГрафическийАдаптер.название like '%{KomputerVideoCardSS_TB.Text}%' and ";
                }
                if (((CheckBox)((Canvas)KomputerDiskSS_TB.Parent).Children[0]).IsChecked == true)
                {
                    requestJoins += "INNER JOIN СписокДисков ON ID_компьютера_P = СписокДисков.ID_Компьютера_F INNER JOIN Диск ON ID_Диска_F = ID_Диска_P ";
                    requestConditions += $"Диск.модель like '%{KomputerDiskSS_TB.Text}%' and ";
                }
                if (((CheckBox)((Canvas)KomputerOZUSizeSS_TB.Parent).Children[0]).IsChecked == true)
                { 
                    requestConditions += $"Компьютер.объемОЗУ {((ComboBoxItem)OZUSizeSignBoard_CB.SelectedItem).Content} {KomputerOZUSizeSS_TB.Text} and "; 
                }
                if (((CheckBox)((Canvas)KomputerSystemSS_TB.Parent).Children[0]).IsChecked == true)
                {
                    requestJoins += "INNER JOIN СписокОС ON ID_компьютера_P = СписокОС.ID_Компьютера_F INNER JOIN ОперационнаяСистема ON ID_ОС_F = ID_ОС_P ";
                    requestConditions += $"ОперационнаяСистема.название like '%{KomputerSystemSS_TB.Text}%' and ";
                }
                requestConditions = requestConditions.TrimEnd(' ', 'd', 'n', 'a');

                if (requestConditions == "")
                {
                    requestTemplate = "SELECT DISTINCT CONCAT('ИН: \"',ИН_Компьютера,'\"   Кабинет: \"',Кабинет.название,'\"   Рабочее место: \"', РабочееМесто.название,'\"') as 'Результаты поиска', ID_компьютера_P as 'ID' " +
                      $"FROM Компьютер INNER JOIN РабочееМесто ON ID_компьютера_F = ID_компьютера_P INNER JOIN Кабинет ON ID_Кабинета_P = ID_Кабинета_F {requestJoins}";
                    SysHelperDB.ExecCom(requestTemplate, DICK);
                    requestTemplate = $"SELECT DISTINCT CONCAT('ИН: \"',ИН_Компьютера,'\"   \"В инвентаре\"')  as 'Результаты поиска', ID_компьютера_P as 'ID' FROM Компьютер {requestJoins}  WHERE статус = 'на складе'";
                    SysHelperDB.ExecCom(requestTemplate, DICK);
                }
                else
                {
                    requestTemplate = "SELECT DISTINCT CONCAT('ИН: \"',ИН_Компьютера,'\"   Кабинет: \"',Кабинет.название,'\"   Рабочее место: \"', РабочееМесто.название,'\"') as 'Результаты поиска', ID_компьютера_P as 'ID' " +
                      $"FROM Компьютер INNER JOIN РабочееМесто ON ID_компьютера_F = ID_компьютера_P INNER JOIN Кабинет ON ID_Кабинета_P = ID_Кабинета_F {requestJoins} WHERE {requestConditions}";
                    SysHelperDB.ExecCom(requestTemplate, DICK);
                    requestTemplate = $"SELECT DISTINCT CONCAT('ИН: \"',ИН_Компьютера,'\"   \"В инвентаре\"')  as 'Результаты поиска', ID_компьютера_P as 'ID' FROM Компьютер {requestJoins}  WHERE статус = 'на складе' and {requestConditions}";
                    SysHelperDB.ExecCom(requestTemplate, DICK);
                }
            }
            else
            {
                if (((CheckBox)((Canvas)MonitorModelSS_TB.Parent).Children[0]).IsChecked == true)
                { requestConditions += $"модель like '%{MonitorModelSS_TB.Text}%' and "; }
                if (((CheckBox)((Canvas)MonitorDiagonalSS_TB.Parent).Children[0]).IsChecked == true)
                { requestConditions += $"диагональ like '%{MonitorDiagonalSS_TB.Text}%' and "; }
                if (((CheckBox)((Canvas)MonitorSideRatioSS_TB.Parent).Children[0]).IsChecked == true)
                { requestConditions += $"соотношениеСторон like '%{MonitorSideRatioSS_TB.Text}%' and "; }
                if (((CheckBox)((Canvas)MonitorMatrixSS_TB.Parent).Children[0]).IsChecked == true)
                { requestConditions += $"матрица like '%{MonitorMatrixSS_TB.Text}%' and "; }
                if (((CheckBox)((Canvas)MonitorFreqSS_TB.Parent).Children[0]).IsChecked == true)
                { requestConditions += $"частота like '%{MonitorFreqSS_TB.Text}%' and "; }
                if (((CheckBox)((Canvas)MonitorResolutionSS_TB.Parent).Children[0]).IsChecked == true)
                { requestConditions += $"разрешение like '%{MonitorResolutionSS_TB.Text}%' and "; }
                requestConditions = requestConditions.TrimEnd(' ', 'd', 'n', 'a');

                if (requestConditions == "")
                {
                    requestTemplate = "SELECT DISTINCT CONCAT('ИН: \"',ИН_Монитора,'\"   Кабинет: \"',Кабинет.название,'\"   Рабочее место: \"', РабочееМесто.название,'\"') as 'Результаты поиска', ID_монитора_P as 'ID' " +
                      $"FROM Монитор INNER JOIN РабочееМесто ON ID_монитора_F = ID_монитора_P INNER JOIN Кабинет ON ID_Кабинета_P = ID_Кабинета_F";
                    SysHelperDB.ExecCom(requestTemplate, DICK);
                    requestTemplate = $"SELECT DISTINCT CONCAT('ИН: \"',ИН_Монитора,'\"   \"В инвентаре\"')  as 'Результаты поиска', ID_монитора_P as 'ID' FROM Монитор WHERE статус = 'на складе'";
                    SysHelperDB.ExecCom(requestTemplate, DICK);
                }
                else
                {
                    requestTemplate = "SELECT DISTINCT CONCAT('ИН: \"',ИН_Монитора,'\"   Кабинет: \"',Кабинет.название,'\"   Рабочее место: \"', РабочееМесто.название,'\"') as 'Результаты поиска', ID_монитора_P as 'ID' " +
                      $"FROM Монитор INNER JOIN РабочееМесто ON ID_монитора_F = ID_монитора_P INNER JOIN Кабинет ON ID_Кабинета_P = ID_Кабинета_F WHERE {requestConditions}";
                    SysHelperDB.ExecCom(requestTemplate, DICK);
                    requestTemplate = $"SELECT DISTINCT CONCAT('ИН: \"',ИН_Монитора,'\"   \"В инвентаре\"')  as 'Результаты поиска', ID_монитора_P as 'ID' FROM Монитор WHERE статус = 'на складе' and {requestConditions}";
                    SysHelperDB.ExecCom(requestTemplate, DICK);
                }
            }
            RequestResult_DG.ItemsSource = DICK.DefaultView;
            RequestResult_DG.Columns[0].Width = 416;
            RequestResult_DG.Columns[1].Visibility = Visibility.Hidden;
        }

        private void BackToMenu_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Main_W.Owner = null;
            App.Finder_W.Owner = MainWindow.Main_W;
            App.Finder_W.Close();
            MainWindow.Main_W.Show();
        }
        private void MoreInf_B_Click(object sender, RoutedEventArgs e)
        {
            MoreInfWin.kTemp = (Komp)FACEHandlers.selectedF;
            MainWindow.MoreInfW = new MoreInfWin();
            MainWindow.MoreInfW.ShowDialog();
        }
        private void Edit_B_Click(object sender, RoutedEventArgs e)
        {
            string id = ((DataRowView)RequestResult_DG.SelectedCells[1].Item)[1].ToString();
            if (((ComboBoxItem)FacilityType_CB.SelectedItem).Content.ToString() == "Компьютер")
            {
                App.FacilityEditorW = new FacilityEditorWin(new WorkPlace { kompukter = App.sysH.FindKomp(int.Parse(id)) }, "EDIT");
                App.FacilityEditorW.ShowDialog();
                DataContext = (App.FacilityEditorW.tempWorkplace.kompukter);
                DICK.DefaultView[RequestResult_DG.SelectedIndex][0] = DICK.DefaultView[RequestResult_DG.SelectedIndex][0].ToString().Replace(DICK.DefaultView[RequestResult_DG.SelectedIndex][0].ToString().Split('\"')[1], App.FacilityEditorW.tempWorkplace.kompukter.kompIN);
            }
            else
            {
                App.FacilityEditorW = new FacilityEditorWin(new WorkPlace { monitor = App.sysH.FindMonitor(int.Parse(id)) }, "EDIT");
                App.FacilityEditorW.ShowDialog();
                DataContext = (App.FacilityEditorW.tempWorkplace.monitor);
                DICK.DefaultView[RequestResult_DG.SelectedIndex][0] = DICK.DefaultView[RequestResult_DG.SelectedIndex][0].ToString().Replace(DICK.DefaultView[RequestResult_DG.SelectedIndex][0].ToString().Split('\"')[1], App.FacilityEditorW.tempWorkplace.monitor.monitorIN);
            }
            RequestResult_DG.ItemsSource = DICK.DefaultView;
        }
        private void Delete_B_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данное оборудование (после удаления его нельзя будет восстановить)", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            FACEHandlers.DeleteFacility(((Button)sender).Uid);
            DICK.DefaultView.Delete(RequestResult_DG.SelectedIndex);
            RequestResult_DG.ItemsSource = DICK.DefaultView;
            SetInf_SP.Visibility = Visibility.Hidden;
            MoreInf_B.IsEnabled = Delete_B.IsEnabled = Edit_B.IsEnabled = false;
        }

        private void FacilityType_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestResult_DG.ItemsSource = null;
            SetInf_SP.Visibility = Visibility.Hidden;
            MoreInf_B.IsEnabled = Delete_B.IsEnabled = Edit_B.IsEnabled = false;

            SearchSettings_L.Content = $" Критерии поиска ({((ComboBoxItem)((ComboBox)sender).SelectedItem).Content}) ";
            if(((ComboBoxItem)((ComboBox)sender).SelectedItem).Content.ToString() == "Компьютер")
            {
                KomputerSearchSettings_CAN.Visibility = Visibility.Visible;
                MonitorSearchSettings_CAN.Visibility = Visibility.Collapsed;
            }
            else
            {
                KomputerSearchSettings_CAN.Visibility = Visibility.Collapsed;
                MonitorSearchSettings_CAN.Visibility = Visibility.Visible;
            }
        }

        private async void SearchSettings_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    if (((TextBox)sender).Text == "") { ((CheckBox)((Canvas)((TextBox)sender).Parent).Children[0]).IsChecked = false; }
                    else
                    {
                        if (((CheckBox)((Canvas)((TextBox)sender).Parent).Children[0]).IsChecked != true)
                        { ((CheckBox)((Canvas)((TextBox)sender).Parent).Children[0]).IsChecked = true; }
                    }
                });
            });
        }
        private void CreateReport_B_Click(object sender, RoutedEventArgs e)
        {
            if (RequestResult_DG.ItemsSource == null) { MessageBox.Show("Невозможно создать отчет по пустой выборке.", "Оповещение", MessageBoxButton.OK); return; }
            if (((DataView)RequestResult_DG.ItemsSource).Table.Rows.Count == 0) {MessageBox.Show("Невозможно создать отчет по пустой выборке.", "Оповещение", MessageBoxButton.OK); return; }
            ExcelReport EXR = new ExcelReport();
            EXR.CreateReport_Requests(((DataView)RequestResult_DG.ItemsSource).Table, ((ComboBoxItem)FacilityType_CB.SelectedItem).Content.ToString());
        }
    }
}
