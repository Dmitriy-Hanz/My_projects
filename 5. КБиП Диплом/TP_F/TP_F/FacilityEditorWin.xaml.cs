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
using System.Data;

namespace TP_F
{
    public partial class FacilityEditorWin : Window
    {
        public List<TextBox> errorsList = new List<TextBox>();
        public WorkPlace originalSource;
        public WorkPlace tempWorkplace;
        private DataTable tempDT;
        private DataTable tempDT2;
        private string senderType;
        public FacilityEditorWin(WorkPlace source, string mode)
        {
            InitializeComponent();
            App.FacilityEditorW = this;
            
            KI_CPU_Delete_B.Click += FACEHandlers.KI_CPU_Delete_B_Click;
            KI_CPU_Add_B.Click += FACEHandlers.KI_CPU_Add_B_Click;
            KI_VideoDevices_Add_B.Click += FACEHandlers.KI_VideoDevices_Add_B_Click;
            KI_Disks_Add_B.Click += FACEHandlers.KI_Disks_Add_B_Click;
            KI_System_Add_B.Click += FACEHandlers.KI_Systems_Add_B_Click;

            senderType = mode;
            tempWorkplace = (WorkPlace)source.Clone();
            originalSource = source;
            RebootFacilityInf();
            DataContext = null;
            DataContext = tempWorkplace;
        }
        private void RebootFacilityInf()
        {
            if (tempWorkplace.kompukter != null)
            {
                if (tempWorkplace.kompukter.cpu == null)
                {
                    FACEHandlers.KI_CPU_Delete_B_Click(null, null);
                }
                else
                {
                    switch (tempWorkplace.kompukter.cpu.cashMemoryType)
                    {
                        case "L1": { KI_CPU_CashType_L.SelectedItem = KI_CPU_CashType_L.Items[0]; break; }
                        case "L2": { KI_CPU_CashType_L.SelectedItem = KI_CPU_CashType_L.Items[1]; break; }
                        case "L3": { KI_CPU_CashType_L.SelectedItem = KI_CPU_CashType_L.Items[2]; break; }
                    }
                }
                if (tempWorkplace.kompukter.videoAdapters.Count != 0)//Видео адаптеры
                {
                    for (int i = 0; i < tempWorkplace.kompukter.videoAdapters.Count; i++)
                    {
                        VideoDevicesList_WP.Children.Add(new Separator { Uid = tempWorkplace.kompukter.videoAdapters[i].adapterID.ToString(), Height = 11, Width = 463, Margin = new Thickness(0), Background = Brushes.Black });
                        FACECreator.CreateVideoCardE(VideoDevicesList_WP, tempWorkplace.kompukter.videoAdapters[i], $"kompukter.videoAdapters[{i}]");
                    }
                    SetHeightGB("video");// расчет размеров группы видео
                }
                if (tempWorkplace.kompukter.disks.Count != 0)//Физические диски
                {
                    for (int i = 0; i < tempWorkplace.kompukter.disks.Count; i++)
                    {
                        DisksList_WP.Children.Add(new Separator { Uid = tempWorkplace.kompukter.disks[i].diskID.ToString(), Height = 11, Width = 463, Margin = new Thickness(0), Background = Brushes.Black });
                        FACECreator.CreateDiskE(DisksList_WP, tempWorkplace.kompukter.disks[i], $"kompukter.disks[{i}]");
                    }
                    SetHeightGB("disk");// расчет размеров группы дисков
                }
                if (tempWorkplace.kompukter.systems.Count != 0)//Операционные системы
                {
                    for (int i = 0; i < tempWorkplace.kompukter.systems.Count; i++)
                    {
                        SystemsList_WP.Children.Add(new Separator { Uid = tempWorkplace.kompukter.systems[i].osID.ToString(), Height = 11, Width = 463, Margin = new Thickness(0), Background = Brushes.Black });
                        FACECreator.CreateSystemE(SystemsList_WP, tempWorkplace.kompukter.systems[i], $"kompukter.systems[{i}]");
                    }
                    SetHeightGB("system");// расчет размеров группы ОС
                }
            }
            else
            {
                KompInfBorder_BOR.Visibility = Visibility.Collapsed;
            }

            if (tempWorkplace.monitor != null)
            {

            }
            else
            {
                MonitorInfBorder_BOR.Visibility = Visibility.Collapsed;
            }
            SetHeightPanel();
        }
        private void ErrorsValidator(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError((TextBox)sender) == true)
            {
                if (errorsList.Contains(sender) == false) { errorsList.Add((TextBox)sender); }
                if (e.Error.ErrorContent.ToString() == "Входная строка имела неверный формат." == true)
                { MessageBox.Show("Введенный текст содержит недопустимые символы."); return; }
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
            else { errorsList.Remove((TextBox)sender); }
        }

        public void SetHeightPanel()
        {
            KompInfBorder_BOR.Height = General_GB.Height + Processor_GB.Height + VideoDevices_GB.Height + Disks_GB.Height + Systems_GB.Height + 30 + 20;
            EquipInfList_WP.Height = 20;
            if (KompInfBorder_BOR.Visibility == Visibility.Visible) { EquipInfList_WP.Height += KompInfBorder_BOR.Height; }
            if (MonitorInfBorder_BOR.Visibility == Visibility.Visible) { EquipInfList_WP.Height += MonitorInfBorder_BOR.Height; }
        }
        public void SetHeightGB(string gbName)
        {
            switch (gbName)
            {
                case "video":
                    {
                        if (tempWorkplace.kompukter.videoAdapters.Count == 0) { VideoDevices_GB.Height = 65; break; }
                        VideoDevices_GB.Height = 65 + (tempWorkplace.kompukter.videoAdapters.Count - 1) * 11 + tempWorkplace.kompukter.videoAdapters.Count * 89;
                        break;
                    }
                case "disk":
                    {
                        if (tempWorkplace.kompukter.disks.Count == 0) { Disks_GB.Height = 65; break; }
                        Disks_GB.Height = 65 + (tempWorkplace.kompukter.disks.Count - 1) * 11 + tempWorkplace.kompukter.disks.Count * 89;
                        break;
                    }
                case "system":
                    {
                        if (tempWorkplace.kompukter.systems.Count == 0) { Systems_GB.Height = 65; break; }
                        Systems_GB.Height = 65 + (tempWorkplace.kompukter.systems.Count - 1) * 11 + tempWorkplace.kompukter.systems.Count * 44;
                        break;
                    }
            }
            SetHeightPanel();
        }

        private bool KompValidator()
        {
            if (tempWorkplace.kompukter.kompIN == "") { MessageBox.Show("Поле \"Инвентарный номер (компьютер)\" должно быть заполнено", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.kompukter.kompIN.Length > 10) { MessageBox.Show("Текст, введенный в поле \"Инвентарный номер (компьютер)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.kompukter.name.Length > 50) { MessageBox.Show("Текст, введенный в поле \"Имя (компьютер)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.kompukter.logDisks.Length > 50) { MessageBox.Show("Текст, введенный в поле \"Логические диски (компьютер)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.kompukter.cpu != null)
            {
                if (tempWorkplace.kompukter.cpu.name == "") { MessageBox.Show("поле названия модели процессора должно быть заполнено", "Сообщение", MessageBoxButton.OK); return false; }
                if (tempWorkplace.kompukter.cpu.name.Length > 100) { MessageBox.Show("Текст, введенный в поле модели процессора, слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            }
            if (tempWorkplace.kompukter.videoAdapters.Count != 0)
            {
                foreach (VideoAdapter item in tempWorkplace.kompukter.videoAdapters)
                { if (item.name == "") { MessageBox.Show("Одно или несколько полей имени графического адптера не заполнены", "Сообщение", MessageBoxButton.OK); return false; } }
            }
            if (tempWorkplace.kompukter.disks.Count != 0)
            {
                foreach (Disk item in tempWorkplace.kompukter.disks)
                { if (item.model == "") { MessageBox.Show("Одно или несколько полей названия модели физического диска не заполнены", "Сообщение", MessageBoxButton.OK); return false; } }
            }
            if (tempWorkplace.kompukter.systems.Count != 0)
            {
                foreach (OS item in tempWorkplace.kompukter.systems)
                { if (item.name == "") { MessageBox.Show("Одно или несколько полей названия операционной системы не заполнены", "Сообщение", MessageBoxButton.OK); return false; } }
            }
            return true;
        }
        private bool MonitorValidator()
        {
            if (tempWorkplace.monitor.monitorIN == "") { MessageBox.Show("Поле \"Инвентарный номер (монитор)\" должно быть заполнено", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.monitor.monitorIN.Length > 10) { MessageBox.Show("Текст, введенный в поле \"Инвентарный номер (монитор)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.monitor.model.Length > 70) { MessageBox.Show("Текст, введенный в поле \"Модель (монитор)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.monitor.diagonal.Length > 10) { MessageBox.Show("Текст, введенный в поле \"Диагональ (монитор)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.monitor.sideRatio.Length > 5) { MessageBox.Show("Текст, введенный в поле \"Соотношение сторон (монитор)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.monitor.matrix.Length > 3) { MessageBox.Show("Текст, введенный в поле \"Матрица (монитор)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.monitor.freq.Length > 3) { MessageBox.Show("Текст, введенный в поле \"Частота (монитор)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            if (tempWorkplace.monitor.resolution.Length > 9) { MessageBox.Show("Текст, введенный в поле \"Разрешение (монитор)\", слишком большой", "Сообщение", MessageBoxButton.OK); return false; }
            return true;
        }
        private void Save_B_Click(object sender, RoutedEventArgs e)
        {
            if (errorsList.Count != 0)
            { MessageBox.Show("Сохранение невозможно, так как есть ошибки в введенных данных (поля с ошибками помечены красным)", "Сообщение", MessageBoxButton.OK); return; }
            
            tempDT = new DataTable();
            tempDT2 = new DataTable();
            if (tempWorkplace.kompukter != null)
            {
                if (KompValidator() == false) { return; }
                if (senderType != "ADD") { SaveEditKomputer(); } else { SaveAddKomputer(); }
                originalSource.kompukter = tempWorkplace.kompukter;
                tempDT = new DataTable();
                tempDT2 = new DataTable();
            }
            if (tempWorkplace.monitor != null)
            {
                if (MonitorValidator() == false) { return; }
                if (senderType != "ADD") { SaveEditMonitor(); } else { SaveAddMonitor(); }
                originalSource.monitor = tempWorkplace.monitor;
            }
        }
        private void SaveEditKomputer()
        {
            SysHelperDB.ExecCom($"UPDATE Компьютер SET ИН_компьютера = '{tempWorkplace.kompukter.kompIN}', имя = '{tempWorkplace.kompukter.name}', объемОЗУ = '{tempWorkplace.kompukter.ozuSize}', логическиеДиски = '{tempWorkplace.kompukter.logDisks}' WHERE ID_компьютера_P = '{tempWorkplace.kompukter.kompID}'");
            if (tempWorkplace.kompukter.cpu == null)//проверка на процессор
            {
                SysHelperDB.ExecCom($"DELETE FROM СписокЦПУ WHERE ID_Компьютера_F = {tempWorkplace.kompukter.kompID}");
            }
            else
            {
                SysHelperDB.ExecCom($"SELECT * FROM ЦПУ WHERE название like '%{tempWorkplace.kompukter.cpu.name}%'", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    tempWorkplace.kompukter.cpu.cashMemoryType = ((ComboBoxItem)KI_CPU_CashType_L.SelectedItem).Content.ToString();
                    SysHelperDB.ExecCom($"INSERT INTO ЦПУ VALUES ('{tempWorkplace.kompukter.cpu.name}','{tempWorkplace.kompukter.cpu.coreCount}','{tempWorkplace.kompukter.cpu.cashMemoryType}','{tempWorkplace.kompukter.cpu.cashMemoryValue}') {Environment.NewLine} SELECT @@IDENTITY", tempDT2);
                    SysHelperDB.ExecCom($"INSERT INTO СписокЦПУ VALUES ('{tempDT2.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
                else
                {
                    tempWorkplace.kompukter.cpu = new CPU(tempDT.Rows[0]);
                    SysHelperDB.ExecCom($"INSERT INTO СписокЦПУ VALUES ('{tempDT.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
            }

            //проверка на видео адаптеры
            SysHelperDB.ExecCom($"DELETE FROM СписокГрафАдаптеров WHERE ID_Компьютера_F = {tempWorkplace.kompukter.kompID}");
            foreach (VideoAdapter item in tempWorkplace.kompukter.videoAdapters)
            {
                tempDT = new DataTable();
                tempDT2 = new DataTable();
                SysHelperDB.ExecCom($"SELECT * FROM ГрафическийАдаптер WHERE название like '%{item.name}%'", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    SysHelperDB.ExecCom($"INSERT INTO ГрафическийАдаптер VALUES ('{item.name}','{item.videoProcessor}','{item.driverVersion}','{item.adapterRAM}') {Environment.NewLine} SELECT @@IDENTITY", tempDT2);
                    SysHelperDB.ExecCom($"INSERT INTO СписокГрафАдаптеров VALUES ('{tempDT2.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
                else
                {
                    item.Initialize(tempDT.Rows[0]);
                    SysHelperDB.ExecCom($"INSERT INTO СписокГрафАдаптеров VALUES ({tempDT.Rows[0][0]},{tempWorkplace.kompukter.kompID})");
                }
            }

            //проверка на физические диски
            SysHelperDB.ExecCom($"DELETE FROM СписокДисков WHERE ID_Компьютера_F = {tempWorkplace.kompukter.kompID}");
            for (int i = 0; i < tempWorkplace.kompukter.disks.Count; i++)
            {
                tempDT = new DataTable();
                tempDT2 = new DataTable();
                SysHelperDB.ExecCom($"SELECT * FROM Диск WHERE модель like '%{tempWorkplace.kompukter.disks[i].model}%'", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    SysHelperDB.ExecCom($"INSERT INTO Диск VALUES ('{tempWorkplace.kompukter.disks[i].model}','{tempWorkplace.kompukter.disks[i]._interface}','{tempWorkplace.kompukter.disks[i].type}','{tempWorkplace.kompukter.disks[i].memoryValue}') {Environment.NewLine} SELECT @@IDENTITY", tempDT2);
                    SysHelperDB.ExecCom($"INSERT INTO СписокДисков VALUES ('{tempDT2.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
                else
                {
                    tempWorkplace.kompukter.disks[i] = new Disk(tempDT.Rows[0]);
                    SysHelperDB.ExecCom($"INSERT INTO СписокДисков VALUES ({tempDT.Rows[0][0]},{tempWorkplace.kompukter.kompID})");
                }
            }

            //проверка на операционную систему
            SysHelperDB.ExecCom($"DELETE FROM СписокОС WHERE ID_Компьютера_F = {tempWorkplace.kompukter.kompID}");
            for (int i = 0; i < tempWorkplace.kompukter.systems.Count; i++)
            {
                tempDT = new DataTable();
                tempDT2 = new DataTable();
                SysHelperDB.ExecCom($"SELECT * FROM ОперационнаяСистема WHERE название like '%{tempWorkplace.kompukter.systems[i].name}%' and версия = '{tempWorkplace.kompukter.systems[i].version}' and архитектура = '{tempWorkplace.kompukter.systems[i].architecture}'", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    tempWorkplace.kompukter.systems[i].architecture = ((ComboBoxItem)((ComboBox)((Canvas)SystemsList_WP.Children[(i+1)*2]).Children[6]).SelectedItem).Content.ToString();
                    SysHelperDB.ExecCom($"INSERT INTO ОперационнаяСистема VALUES ('{tempWorkplace.kompukter.systems[i].name}','{tempWorkplace.kompukter.systems[i].version}','{tempWorkplace.kompukter.systems[i].architecture}') {Environment.NewLine} SELECT @@IDENTITY", tempDT2);
                    SysHelperDB.ExecCom($"INSERT INTO СписокОС VALUES ('{tempDT2.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
                else
                {
                    tempWorkplace.kompukter.systems[i] = new OS(tempDT.Rows[0]);
                    SysHelperDB.ExecCom($"INSERT INTO СписокОС VALUES ({tempDT.Rows[0][0]},{tempWorkplace.kompukter.kompID})");
                }
            }

            if (App.sysH.FindFacility(tempWorkplace.kompukter.kompID.ToString(), "K") != null)
            {
                ((Komp)App.sysH.FindFacility(tempWorkplace.kompukter.kompID.ToString(), "K")).CopyHere(tempWorkplace.kompukter);
            }
        }
        private void SaveEditMonitor()
        {
            SysHelperDB.ExecCom($"SELECT * FROM Монитор WHERE ID_монитора_P = {tempWorkplace.monitor.monitorID}", tempDT);
            if(tempDT.Rows.Count == 0)
            {
                SysHelperDB.ExecCom($"INSERT INTO Монитор VALUES('{tempWorkplace.monitor.monitorIN}','{tempWorkplace.monitor.model}','{tempWorkplace.monitor.diagonal}','{tempWorkplace.monitor.sideRatio}','{tempWorkplace.monitor.matrix}','{tempWorkplace.monitor.freq}','{tempWorkplace.monitor.resolution}','на складе')");
            }
            else
            {
                SysHelperDB.ExecCom($"SELECT ID_монитора_F FROM РабочееМесто WHERE ID_монитора_F = {tempWorkplace.monitor.monitorID}", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    SysHelperDB.ExecCom($"UPDATE Монитор SET ИН_монитора = '{tempWorkplace.monitor.monitorIN}', модель = '{tempWorkplace.monitor.model}', диагональ = '{tempWorkplace.monitor.diagonal}', соотношениеСторон = '{tempWorkplace.monitor.sideRatio}', матрица = '{tempWorkplace.monitor.matrix}', частота = '{tempWorkplace.monitor.freq}', разрешение = '{tempWorkplace.monitor.resolution}', статус = 'на складе' WHERE ID_монитора_P = {tempWorkplace.monitor.monitorID}");
                }
                else
                {
                    SysHelperDB.ExecCom($"UPDATE Монитор SET ИН_монитора = '{tempWorkplace.monitor.monitorIN}', модель = '{tempWorkplace.monitor.model}', диагональ = '{tempWorkplace.monitor.diagonal}', соотношениеСторон = '{tempWorkplace.monitor.sideRatio}', матрица = '{tempWorkplace.monitor.matrix}', частота = '{tempWorkplace.monitor.freq}', разрешение = '{tempWorkplace.monitor.resolution}', статус = 'рабочий' WHERE ID_монитора_P = {tempWorkplace.monitor.monitorID}");
                }
            }

            if (App.sysH.FindFacility(tempWorkplace.monitor.monitorID.ToString(), "M") != null)
            {
                ((Monitor)App.sysH.FindFacility(tempWorkplace.monitor.monitorID.ToString(), "M")).CopyHere(tempWorkplace.monitor);
            }
        }

        private void SaveAddKomputer()
        {
            SysHelperDB.ExecCom($"INSERT INTO Компьютер VALUES ('{tempWorkplace.kompukter.kompIN}','{tempWorkplace.kompukter.name}',{tempWorkplace.kompukter.ozuSize},'{tempWorkplace.kompukter.logDisks}','на складе') {Environment.NewLine} SELECT @@IDENTITY",tempDT);
            tempWorkplace.kompukter.kompID = tempWorkplace.kompukter.uID = int.Parse(tempDT.Rows[0][0].ToString());
            
            //проверка на процессор
            tempDT = new DataTable();
            tempDT2 = new DataTable();
            if (tempWorkplace.kompukter.cpu != null)
            {
                SysHelperDB.ExecCom($"SELECT * FROM ЦПУ WHERE название like '%{tempWorkplace.kompukter.cpu.name}%'", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    tempWorkplace.kompukter.cpu.cashMemoryType = ((ComboBoxItem)KI_CPU_CashType_L.SelectedItem).Content.ToString();
                    SysHelperDB.ExecCom($"INSERT INTO ЦПУ VALUES ('{tempWorkplace.kompukter.cpu.name}','{tempWorkplace.kompukter.cpu.coreCount}','{tempWorkplace.kompukter.cpu.cashMemoryType}','{tempWorkplace.kompukter.cpu.cashMemoryValue}') {Environment.NewLine} SELECT @@IDENTITY", tempDT2);
                    SysHelperDB.ExecCom($"INSERT INTO СписокЦПУ VALUES ('{tempDT2.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
                else
                {
                    tempWorkplace.kompukter.cpu = new CPU(tempDT.Rows[0]);
                    SysHelperDB.ExecCom($"INSERT INTO СписокЦПУ VALUES ('{tempDT.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
            }

            //проверка на видео адаптеры
            tempDT = new DataTable();
            tempDT2 = new DataTable();
            foreach (VideoAdapter item in tempWorkplace.kompukter.videoAdapters)
            {
                tempDT = new DataTable();
                tempDT2 = new DataTable();
                SysHelperDB.ExecCom($"SELECT * FROM ГрафическийАдаптер WHERE название like '%{item.name}%'", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    SysHelperDB.ExecCom($"INSERT INTO ГрафическийАдаптер VALUES ('{item.name}','{item.videoProcessor}','{item.driverVersion}','{item.adapterRAM}') {Environment.NewLine} SELECT @@IDENTITY", tempDT2);
                    SysHelperDB.ExecCom($"INSERT INTO СписокГрафАдаптеров VALUES ('{tempDT2.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
                else
                {
                    item.Initialize(tempDT.Rows[0]);
                    SysHelperDB.ExecCom($"INSERT INTO СписокГрафАдаптеров VALUES ({tempDT.Rows[0][0]},{tempWorkplace.kompukter.kompID})");
                }
            }

            //проверка на физические диски
            tempDT = new DataTable();
            tempDT2 = new DataTable();
            for (int i = 0; i < tempWorkplace.kompukter.disks.Count; i++)
            {
                tempDT = new DataTable();
                tempDT2 = new DataTable();
                SysHelperDB.ExecCom($"SELECT * FROM Диск WHERE модель like '%{tempWorkplace.kompukter.disks[i].model}%'", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    SysHelperDB.ExecCom($"INSERT INTO Диск VALUES ('{tempWorkplace.kompukter.disks[i].model}','{tempWorkplace.kompukter.disks[i]._interface}','{tempWorkplace.kompukter.disks[i].type}','{tempWorkplace.kompukter.disks[i].memoryValue}') {Environment.NewLine} SELECT @@IDENTITY", tempDT2);
                    SysHelperDB.ExecCom($"INSERT INTO СписокДисков VALUES ('{tempDT2.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
                else
                {
                    tempWorkplace.kompukter.disks[i] = new Disk(tempDT.Rows[0]);
                    SysHelperDB.ExecCom($"INSERT INTO СписокДисков VALUES ({tempDT.Rows[0][0]},{tempWorkplace.kompukter.kompID})");
                }
            }

            //проверка на операционную систему
            tempDT = new DataTable();
            tempDT2 = new DataTable();
            for (int i = 0; i < tempWorkplace.kompukter.systems.Count; i++)
            {
                tempDT = new DataTable();
                tempDT2 = new DataTable();
                tempWorkplace.kompukter.systems[i].architecture = ((ComboBoxItem)((ComboBox)((Canvas)SystemsList_WP.Children[(i + 1)*2]).Children[6]).SelectedItem).Content.ToString();
                SysHelperDB.ExecCom($"SELECT * FROM ОперационнаяСистема WHERE название like '%{tempWorkplace.kompukter.systems[i].name}%' and версия = '{tempWorkplace.kompukter.systems[i].version}' and архитектура = '{tempWorkplace.kompukter.systems[i].architecture}'", tempDT);
                if (tempDT.Rows.Count == 0)
                {
                    SysHelperDB.ExecCom($"INSERT INTO ОперационнаяСистема VALUES ('{tempWorkplace.kompukter.systems[i].name}','{tempWorkplace.kompukter.systems[i].version}','{tempWorkplace.kompukter.systems[i].architecture}') {Environment.NewLine} SELECT @@IDENTITY", tempDT2);
                    SysHelperDB.ExecCom($"INSERT INTO СписокОС VALUES ('{tempDT2.Rows[0][0]}','{tempWorkplace.kompukter.kompID}')");
                }
                else
                {
                    tempWorkplace.kompukter.systems[i] = new OS(tempDT.Rows[0]);
                    SysHelperDB.ExecCom($"INSERT INTO СписокОС VALUES ({tempDT.Rows[0][0]},{tempWorkplace.kompukter.kompID})");
                }
            }
            tempWorkplace.kompukter.uIN = tempWorkplace.kompukter.kompIN;
            App.sysH.Inventory.Facilities.Add(tempWorkplace.kompukter);
            InventoryWin.AddingCaseW.tempF = tempWorkplace.kompukter;
            Hide();
        }
        private void SaveAddMonitor()
        {
            SysHelperDB.ExecCom($"INSERT INTO Монитор VALUES('{tempWorkplace.monitor.monitorIN}','{tempWorkplace.monitor.model}','{tempWorkplace.monitor.diagonal}','{tempWorkplace.monitor.sideRatio}','{tempWorkplace.monitor.matrix}','{tempWorkplace.monitor.freq}','{tempWorkplace.monitor.resolution}','на складе') {Environment.NewLine} SELECT @@IDENTITY", tempDT);
            tempWorkplace.monitor.uID = tempWorkplace.monitor.monitorID = int.Parse(tempDT.Rows[0][0].ToString());
            tempWorkplace.monitor.uIN = tempWorkplace.monitor.monitorIN;
            App.sysH.Inventory.Facilities.Add(tempWorkplace.monitor);
            InventoryWin.AddingCaseW.tempF = tempWorkplace.monitor;
            Hide();
        }

        private void Close_B_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void KompINAutoGen_B_Click(object sender, RoutedEventArgs e)
        {
            KI_IN_L.Clear();
            FACEHandlers.AutoGenIN(tempWorkplace.kompukter);
            DataContext = null;
            DataContext = tempWorkplace;
        }
        private void MonitorINAutoGen_B_Click(object sender, RoutedEventArgs e)
        {
            MI_MonitorIN_TB.Clear();
            FACEHandlers.AutoGenIN(tempWorkplace.monitor);
            DataContext = null;
            DataContext = tempWorkplace;
        }
    }
}

//ПОМОЙКА

// xaml-код для группы операционных систем:

//<GroupBox x:Name="Systems_GB" Header="Операционные системы" BorderBrush="Black" Padding="3,3,3,0" BorderThickness="1" HorizontalAlignment="Left" Height="63" Width="481" VerticalAlignment="Top" FontSize="14" Margin="10,0,0,0">
//      <WrapPanel x:Name="SystemsList_WP" Margin="0,0,0,3">
//             <Button x:Name="KI_System_Add_B" Content="Добавить" Width="130" Margin="0,0,0,2"/>
//      </WrapPanel>
//</GroupBox>

// xaml-код для элемента операционной системы:

//<Canvas Height = "42" Width="463" Margin="10,0,0,0">
//    <Image HorizontalAlignment = "Left" Height="42" VerticalAlignment="Top" Width="42" Source="images/Icon_CPU.bmp"/>
//    <Label Content = "Версия:   " FontFamily="Arial Unicode MS" Foreground="#FF686868" Padding="0" Canvas.Left="47" Canvas.Top="22" FontWeight="Bold" FontSize="12" VerticalContentAlignment="Center" Height="20"/>
//    <Label Content = "Разрядность:   " FontFamily="Arial Unicode MS" Foreground="#FF686868" Height="20" Padding="0" Canvas.Left="234" Canvas.Top="22" FontWeight="Bold" FontSize="12" VerticalContentAlignment="Center"/>
//    <TextBox FontFamily = "Arial Unicode MS" Foreground="#FF2491B9" Width="416" Height="20" Canvas.Left="47" VerticalContentAlignment="Center" FontWeight="Bold" FontSize="12" />
//    <TextBox FontFamily = "Arial Unicode MS" Foreground="#FF686868" Height="20"  Padding="0" Canvas.Left="103" Canvas.Top="22" VerticalContentAlignment="Center" FontSize="12" HorizontalContentAlignment="Stretch" Width="126"/>
//    <ComboBox x:Name="KI_OS_Arcitecture_CB" FontFamily="Arial Unicode MS" Foreground="#FF686868" Height="20" Width="141" Canvas.Left="322" Canvas.Top="22" FontSize="12" Padding="3,2,0,0">
//        <ComboBoxItem x:Name="x32" Content="32-х разрядная" IsSelected="True"/>
//        <ComboBoxItem x:Name="x64" Content="64-х разрядная"/>
//        <ComboBoxItem x:Name="x86" Content="86-х разрядная"/>
//    </ComboBox>
//</Canvas>