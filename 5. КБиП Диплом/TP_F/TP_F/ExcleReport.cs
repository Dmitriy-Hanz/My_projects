using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXL = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;
using System.Data;


namespace TP_F
{
    class ExcelReport
    {
        EXL.Application excelApp;
        EXL.Workbook workBook;
        EXL.Worksheet workSheet;
        SaveFileDialog SFD;
        public void CreateReport_Komputer(Komp k)
        {
            SFD = new SaveFileDialog();
            SFD.FileName = "Отчет_компьютер";
            if (SFD.ShowDialog() == true)
            {
                excelApp = new EXL.Application();
                //System.Diagnostics.Process excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL").Last();

                File.Delete($"{SFD.FileName}.xlsx");
                excelApp.SheetsInNewWorkbook = 1;
                workBook = excelApp.Workbooks.Add(Type.Missing);
                excelApp.DisplayAlerts = false;
                workSheet = (EXL.Worksheet)excelApp.Worksheets.get_Item(1);

                int j = 1;
                workSheet.Name = "Информация об оборудовании";

                //ЗАГОЛОВОК ОТЧЕТА И ДАТА СОЗДАНИЯ
                workSheet.get_Range($"A{j}", $"H{j}").Merge(Type.Missing);
                SetFontProps($"A{j}", $"H{j}", "Arial Black", 16, false, System.Drawing.Color.Black);
                workSheet.get_Range($"A{j}", $"H{j}").EntireRow.RowHeight = 51;
                workSheet.get_Range($"A{j}", $"H{j}").WrapText = true;
                workSheet.Cells[j++, 1] = "Отчет о текущей единице оборудования:";
                j++;
                workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                workSheet.get_Range($"D{j}", $"G{j}").Cells.Font.Bold = true;
                workSheet.get_Range($"A{j}", $"G{j}").EntireRow.Font.Size = 12;
                workSheet.Cells[j, 1] = "Дата создания:";
                workSheet.get_Range($"C{j}", $"D{j}").Merge(Type.Missing);
                workSheet.Cells[j++, 3] = DateTime.Now.ToShortDateString();
                j++;

                //ВСЕ ОСТАЛЬНОЕ

                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                workSheet.get_Range($"A{j}", $"E{j}").EntireRow.Font.Name = "Arial Unicode MS";
                workSheet.get_Range($"A{j}", $"E{j}").EntireRow.Font.Size = 18;
                workSheet.get_Range($"A{j}", $"E{j}").HorizontalAlignment = EXL.Constants.xlCenter;
                workSheet.get_Range($"A{j}", $"E{j}").EntireRow.Font.Bold = true;
                workSheet.get_Range($"A{j}", $"E{j}").EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 0x24, 0x91, 0xB9));
                workSheet.Cells[j++, 1] = "Компьютер (Сис. блок)";

                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                workSheet.get_Range($"A{j}", $"E{j}").EntireRow.Font.Name = "Arial Unicode MS";
                workSheet.get_Range($"A{j}", $"E{j}").EntireRow.Font.Size = 12;
                workSheet.Cells[j++, 1] = "Общая информация";

                SetFontProps($"B{j}", $"C{j + 4}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));

                workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                workSheet.Cells[j, 2] = "Инвентарный номер:   ";
                workSheet.Cells[j++, 4] = k.kompIN;

                workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                workSheet.Cells[j, 2] = "Имя:   ";
                workSheet.Cells[j++, 4] = k.name;

                workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                workSheet.Cells[j, 2] = "Объем ОЗУ:   ";
                workSheet.Cells[j++, 4] = k.ozuSize + " ГБ";

                workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                workSheet.Cells[j, 2] = "Логические диски:   ";
                workSheet.Cells[j++, 4] = k.logDisks;

                SetFontProps($"D{j}", $"E{j - 4}", "Arial Unicode MS", 10, false, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                workSheet.get_Range("B1").EntireColumn.ColumnWidth = 11;
                workSheet.get_Range("C1").EntireColumn.ColumnWidth = 15;
                workSheet.get_Range("E1").EntireColumn.ColumnWidth = 23;

                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                workSheet.get_Range($"A{j}", $"E{j}").EntireRow.Font.Name = "Arial Unicode MS";
                workSheet.get_Range($"A{j}", $"E{j}").EntireRow.Font.Size = 12;

                if (k.cpu == null) { workSheet.Cells[j++, 1] = "Процессор:   нет"; }
                else
                {
                    workSheet.Cells[j++, 1] = "Процессор:   ";

                    SetFontProps($"B{j}", $"C{j + 4}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                    SetFontProps($"D{j}", $"E{j + 4}", "Arial Unicode MS", 10, false, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                    workSheet.get_Range($"D{j}", $"E{j + 4}").HorizontalAlignment = EXL.Constants.xlLeft;

                    workSheet.get_Range($"B{j}", $"E{j}").Merge(Type.Missing);
                    SetFontProps($"B{j}", $"E{j}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x24, 0x91, 0xB9));
                    workSheet.Cells[j++, 2] = k.cpu.name;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Число ядер:   ";
                    workSheet.Cells[j++, 4] = k.cpu.coreCount;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Тип кэш-памяти:   ";
                    workSheet.Cells[j++, 4] = k.cpu.cashMemoryType;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Макс. объем кэш-памяти:   ";
                    workSheet.Cells[j++, 4] = k.cpu.cashMemoryValue + " КБ";
                }

                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                SetFontProps($"A{j}", $"E{j}", "Arial Unicode MS", 12, false, System.Drawing.Color.FromArgb(0, 0x00, 0x00, 0x00));
                workSheet.Cells[j++, 1] = "Графический адаптер";
                foreach (var item in k.videoAdapters)
                {
                    SetFontProps($"B{j}", $"C{j + 3}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                    SetFontProps($"D{j}", $"E{j + 3}", "Arial Unicode MS", 10, false, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                    SetFontProps($"B{j}", $"E{j}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x24, 0x91, 0xB9));
                    workSheet.get_Range($"D{j}", $"E{j + 3}").HorizontalAlignment = EXL.Constants.xlLeft;

                    workSheet.get_Range($"B{j}", $"E{j}").Merge(Type.Missing);

                    workSheet.Cells[j++, 2] = item.name;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Граф. процессор:   ";
                    workSheet.Cells[j++, 4] = item.videoProcessor;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Версия драйвера:   ";
                    workSheet.Cells[j++, 4] = item.driverVersion;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "RAM:   ";
                    workSheet.Cells[j++, 4] = item.adapterRAM;
                }

                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                SetFontProps($"A{j}", $"E{j}", "Arial Unicode MS", 12, false, System.Drawing.Color.FromArgb(0, 0x00, 0x00, 0x00));
                workSheet.Cells[j++, 1] = "Физические диски";
                foreach (var item in k.disks)
                {
                    SetFontProps($"B{j}", $"C{j + 3}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                    SetFontProps($"D{j}", $"E{j + 3}", "Arial Unicode MS", 10, false, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                    SetFontProps($"B{j}", $"E{j}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x24, 0x91, 0xB9));
                    workSheet.get_Range($"D{j}", $"E{j + 3}").HorizontalAlignment = EXL.Constants.xlLeft;

                    workSheet.get_Range($"B{j}", $"E{j}").Merge(Type.Missing);

                    workSheet.Cells[j++, 2] = item.model;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Интерфейс:   ";
                    workSheet.Cells[j++, 4] = item._interface;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Тип носителя:   ";
                    workSheet.Cells[j++, 4] = item.type;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Объем памяти:   ";
                    workSheet.Cells[j++, 4] = item.memoryValue + " ГБ";
                }

                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                SetFontProps($"A{j}", $"E{j}", "Arial Unicode MS", 12, false, System.Drawing.Color.FromArgb(0, 0x00, 0x00, 0x00));
                workSheet.Cells[j++, 1] = "Операционные системы";
                foreach (var item in k.systems)
                {
                    SetFontProps($"B{j}", $"C{j + 2}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                    SetFontProps($"D{j}", $"E{j + 2}", "Arial Unicode MS", 10, false, System.Drawing.Color.FromArgb(0, 0x68, 0x68, 0x68));
                    SetFontProps($"B{j}", $"E{j}", "Arial Unicode MS", 10, true, System.Drawing.Color.FromArgb(0, 0x24, 0x91, 0xB9));
                    workSheet.get_Range($"D{j}", $"E{j + 2}").HorizontalAlignment = EXL.Constants.xlLeft;

                    workSheet.get_Range($"B{j}", $"E{j}").Merge(Type.Missing);

                    workSheet.Cells[j++, 2] = item.name;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Версия:   ";
                    workSheet.Cells[j++, 4] = item.version;

                    workSheet.get_Range($"B{j}", $"C{j}").Merge(Type.Missing);
                    workSheet.get_Range($"D{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.Cells[j, 2] = "Архитектура:   ";
                    workSheet.Cells[j++, 4] = item.architecture;
                }
                workSheet.Application.ActiveWorkbook.SaveAs($"{SFD.FileName}.xlsx");

                workBook.Close(false, Type.Missing, Type.Missing);
                
                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                excelApp = null;
                workBook = null;
                workSheet = null;

                System.GC.Collect();
                //excelProc.Kill();
            }
        }
        public void CreateReport_General()
        {
            SFD = new SaveFileDialog();
            SFD.FileName = "Отчет_организация";
            if (SFD.ShowDialog() == true)
            {
                if (MessageBox.Show("Сейчас будет создан отчет обо всем оборудовании, рабочих местах и кабинетах организации. Желаете продолжить?", "Оповещение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
                
                excelApp = new EXL.Application();
                //System.Diagnostics.Process excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL").Last();

                File.Delete($"{SFD.FileName}.xlsx");
                excelApp.SheetsInNewWorkbook = 1;
                workBook = excelApp.Workbooks.Add(Type.Missing);
                excelApp.DisplayAlerts = false;
                workSheet = (EXL.Worksheet)excelApp.Worksheets.get_Item(1);

                int j = 1, workPlaceCount = 0, kompCount = 0, monitorCount = 0, inventoryKompCount = 0, inventoryMonitorCount = 0;
                foreach (var item in App.sysH.Inventory.Facilities)
                {
                    if(item is Komp == true) { inventoryKompCount++; } else { inventoryMonitorCount++; }
                }

                workSheet.Name = "Оборудование по организации";

                //ЗАГОЛОВОК ОТЧЕТА И ДАТА СОЗДАНИЯ
                workSheet.get_Range($"A{j}", $"H{j}").Merge(Type.Missing);
                SetFontProps($"A{j}", $"H{j}", "Arial Black", 16, false, System.Drawing.Color.Black);
                workSheet.get_Range($"A{j}", $"H{j}").EntireRow.RowHeight = 51;
                workSheet.get_Range($"A{j}", $"H{j}").WrapText = true;
                workSheet.Cells[j++, 1] = "Отчет об общем количестве оборудования в организации:";
                j++;
                workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                workSheet.get_Range($"D{j}", $"G{j}").Cells.Font.Bold = true;
                workSheet.get_Range($"A{j}", $"G{j}").EntireRow.Font.Size = 12;
                workSheet.Cells[j, 1] = "Дата создания:";
                workSheet.get_Range($"C{j}", $"D{j}").Merge(Type.Missing);
                workSheet.Cells[j++, 3] = DateTime.Now.ToShortDateString();
                j++;

                //ВСЕ ОСТАЛЬНОЕ
                workSheet.get_Range($"A{j}", $"D{j}").Borders.Color = System.Drawing.Color.Black.ToArgb();
                workSheet.get_Range($"A{j}", $"D{j}").EntireRow.Font.Bold = true;
                workSheet.get_Range($"A{j}", $"D{j}").EntireRow.VerticalAlignment = EXL.Constants.xlCenter;
                workSheet.get_Range($"A{j}", $"D{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlCenter;
                workSheet.get_Range($"A{j}", $"D{j}").WrapText = true;
                workSheet.get_Range($"A{j}").EntireColumn.ColumnWidth = 20;
                workSheet.Cells[j, 1] = "Название помещения";
                workSheet.get_Range($"B{j}").EntireColumn.ColumnWidth = 13.14;
                workSheet.Cells[j, 2] = "Кол-во рабочих мест";
                workSheet.get_Range($"C{j}").EntireColumn.ColumnWidth = 11;
                workSheet.Cells[j, 3] = "Кол-во сис. блоков";
                workSheet.get_Range($"D{j}").EntireColumn.ColumnWidth = 11;
                workSheet.Cells[j++, 4] = "Кол-во мониторов";

                int temp1=0, temp2 = 0;
                foreach (Kabinet item in App.sysH.kabinets)
                {
                    workSheet.Cells[j, 1] = item.name;
                    workSheet.Cells[j, 2] = item.workPlaces.Count;
                    workPlaceCount += item.workPlaces.Count;
                    foreach (WorkPlace item2 in item.workPlaces)
                    {
                        if (item2.kompukter != null) { temp1++; kompCount++; }
                        if (item2.monitor != null) { temp2++; monitorCount++; }
                    }
                    workSheet.Cells[j, 3] = temp1;
                    workSheet.Cells[j, 4] = temp2;
                    temp1 = temp2 = 0;
                    j++;
                }
                j++;

                workSheet.get_Range($"A{j}", $"C{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Общее кол-во кабинетов:";
                workSheet.Cells[j++, 4] = App.sysH.kabinets.Count;
                workSheet.get_Range($"A{j}", $"C{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Общее кол-во рабочих мест:";
                workSheet.Cells[j++, 4] = workPlaceCount;
                workSheet.get_Range($"A{j}", $"C{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Общее кол-во сис. блоков:";
                workSheet.Cells[j++, 4] = kompCount;
                workSheet.get_Range($"A{j}", $"C{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Общее кол-во сис. блоков (включая инвентарь):";
                workSheet.Cells[j++, 4] = kompCount + inventoryKompCount;
                workSheet.get_Range($"A{j}", $"C{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Общее кол-во мониторов:";
                workSheet.Cells[j++, 4] = monitorCount;
                workSheet.get_Range($"A{j}", $"C{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Общее кол-во мониторов (включая инвентарь):";
                workSheet.Cells[j++, 4] = monitorCount + inventoryMonitorCount;

                workSheet.Application.ActiveWorkbook.SaveAs($"{SFD.FileName}.xlsx");

                workBook.Close(false, Type.Missing, Type.Missing);

                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                excelApp = null;
                workBook = null;
                workSheet = null;

                System.GC.Collect();
                //excelProc.Kill();
            }
        }
        public void CreateReport_Requests(DataTable requestDT,string fType)
        {
            string[] ass = requestDT.Rows[0][0].ToString().Split('"');

            SFD = new SaveFileDialog();
            SFD.FileName = "Отчет_выборка";
            if (SFD.ShowDialog() == true)
            {
                if (MessageBox.Show("Сейчас будет создан отчет по текущему поисковому запросу. Желаете продолжить?", "Оповещение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }

                excelApp = new EXL.Application();
                //System.Diagnostics.Process excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL").Last();

                File.Delete($"{SFD.FileName}.xlsx");
                excelApp.SheetsInNewWorkbook = 1;
                workBook = excelApp.Workbooks.Add(Type.Missing);
                excelApp.DisplayAlerts = false;
                workSheet = (EXL.Worksheet)excelApp.Worksheets.get_Item(1);

                int j = 1,wpCount = 0, facInventoryCount = 0;
                workSheet.Name = "Выборка";

                workSheet.get_Range($"A{j}").EntireColumn.ColumnWidth = 12;

                //ЗАГОЛОВОК ОТЧЕТА И ДАТА СОЗДАНИЯ
                workSheet.get_Range($"A{j}", $"H{j}").Merge(Type.Missing);
                SetFontProps($"A{j}", $"H{j}", "Arial Black",16,false,System.Drawing.Color.Black);
                workSheet.get_Range($"A{j}", $"H{j}").EntireRow.RowHeight = 51;
                workSheet.get_Range($"A{j}", $"H{j}").WrapText = true;
                workSheet.Cells[j++, 1] = "Отчет о результатах поиска оборудования в организации:";
                j++;
                workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                workSheet.get_Range($"D{j}", $"G{j}").Cells.Font.Bold = true;
                workSheet.get_Range($"A{j}", $"G{j}").EntireRow.Font.Size = 12;
                workSheet.Cells[j, 1] = "Дата создания:";
                workSheet.get_Range($"C{j}", $"D{j}").Merge(Type.Missing);
                workSheet.Cells[j++, 3] = DateTime.Now.ToShortDateString();
                j++;

                //ВСЕ ОСТАЛЬНОЕ
                workSheet.get_Range($"A{j}", $"C{j}").Merge(Type.Missing);
                workSheet.get_Range($"A{j}", $"G{j}").EntireRow.Font.Size = 12;
                workSheet.Cells[j, 1] = "Тип оборудования:";
                workSheet.get_Range($"D{j}", $"G{j}").Merge(Type.Missing);
                workSheet.get_Range($"D{j}", $"G{j}").Cells.Font.Bold = true;
                workSheet.get_Range($"D{j}", $"G{j}").Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 0x24, 0x91, 0xB9));
                if (fType == "Компьютер") { workSheet.Cells[j++, 4] = $"{fType} (сис. блок)"; } else { workSheet.Cells[j++, 4] = fType; }


                workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                workSheet.get_Range($"A{j}", $"G{j}").Borders.Color = System.Drawing.Color.Black.ToArgb();
                workSheet.get_Range($"A{j}", $"G{j}").EntireRow.Font.Bold = true;
                workSheet.get_Range($"A{j}", $"G{j}").EntireRow.VerticalAlignment = EXL.Constants.xlCenter;
                workSheet.get_Range($"A{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlCenter;
                workSheet.Cells[j, 1] = "Критерий поиска";
                workSheet.Cells[j++, 3] = "Значение";
                if (fType == "Компьютер")
                {
                    if (((CheckBox)((Canvas)App.Finder_W.KomputerNameSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Имя";
                        workSheet.Cells[j++, 3] = App.Finder_W.KomputerNameSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.KomputerProcessorSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Процессор";
                        workSheet.Cells[j++, 3] = App.Finder_W.KomputerProcessorSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.KomputerVideoCardSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Граф. адаптер";
                        workSheet.Cells[j++, 3] = App.Finder_W.KomputerVideoCardSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.KomputerDiskSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Физ. диск";
                        workSheet.Cells[j++, 3] = App.Finder_W.KomputerDiskSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.KomputerOZUSizeSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Объем ОЗУ";
                        workSheet.Cells[j++, 3] = App.Finder_W.KomputerOZUSizeSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.KomputerSystemSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "ОС";
                        workSheet.Cells[j++, 3] = App.Finder_W.KomputerSystemSS_TB.Text;
                    }
                }
                else
                {
                    if (((CheckBox)((Canvas)App.Finder_W.MonitorModelSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Модель";
                        workSheet.Cells[j++, 3] = App.Finder_W.MonitorModelSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.MonitorDiagonalSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Диагональ";
                        workSheet.Cells[j++, 3] = App.Finder_W.MonitorDiagonalSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.MonitorSideRatioSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Соотношение сторон";
                        workSheet.Cells[j++, 3] = App.Finder_W.MonitorSideRatioSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.MonitorMatrixSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Матрица";
                        workSheet.Cells[j++, 3] = App.Finder_W.MonitorMatrixSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.MonitorFreqSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Частота";
                        workSheet.Cells[j++, 3] = App.Finder_W.MonitorFreqSS_TB.Text;
                    }
                    if (((CheckBox)((Canvas)App.Finder_W.MonitorResolutionSS_TB.Parent).Children[0]).IsChecked == true)
                    {
                        workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").Merge(Type.Missing);
                        workSheet.get_Range($"C{j}", $"G{j}").EntireRow.HorizontalAlignment = EXL.Constants.xlLeft;
                        workSheet.Cells[j, 1] = "Разрешение";
                        workSheet.Cells[j++, 3] = App.Finder_W.MonitorResolutionSS_TB.Text;
                    }
                }
                j++;

                workSheet.get_Range($"A{j}", $"C{j}").Merge(Type.Missing);
                workSheet.Cells[j++, 1] = "Результат выборки";

                workSheet.get_Range($"A{j}", $"H{j}").EntireRow.RowHeight = 30;
                workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                workSheet.get_Range($"C{j}", $"E{j}").Merge(Type.Missing);
                workSheet.get_Range($"F{j}", $"H{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Идентификационный номер";
                workSheet.Cells[j, 3] = "Название кабинета/инвентарь";
                workSheet.Cells[j, 6] = "Название раб. места";
                workSheet.get_Range($"A{j}", $"H{j}").EntireRow.Font.Bold = true;
                workSheet.get_Range($"A{j}", $"H{j}").HorizontalAlignment = EXL.Constants.xlCenter;
                workSheet.get_Range($"A{j}", $"H{j}").VerticalAlignment = EXL.Constants.xlCenter;
                workSheet.get_Range($"A{j}", $"H{j}").WrapText = true;
                workSheet.get_Range($"A{j}", $"H{j++}").Borders.Color = System.Drawing.Color.Black.ToArgb();

                foreach (DataRow row in requestDT.Rows)
                {
                    workSheet.get_Range($"A{j}", $"B{j}").Merge(Type.Missing);
                    workSheet.get_Range($"C{j}", $"E{j}").Merge(Type.Missing);
                    workSheet.get_Range($"F{j}", $"H{j}").Merge(Type.Missing);
                    workSheet.get_Range($"A{j}", $"H{j}").Cells.HorizontalAlignment = EXL.Constants.xlLeft;
                    workSheet.Cells[j, 1] = row[0].ToString().Split('"')[1];
                    if (row[0].ToString().Split('"')[3].Contains("В инвентаре") == false)
                    {
                        wpCount++;
                        workSheet.Cells[j, 3] = row[0].ToString().Split('"')[3];
                        workSheet.Cells[j, 6] = row[0].ToString().Split('"')[5];
                    }
                    else
                    {
                        facInventoryCount++;
                        workSheet.Cells[j, 3] = row[0].ToString().Split('"')[3];
                    }
                    j++;
                }
                j++;

                workSheet.get_Range($"A{j}", $"E{j + 4}").Cells.Font.Bold = true;
                workSheet.get_Range($"F{j}", $"F{j + 4}").HorizontalAlignment = EXL.Constants.xlCenter;
                workSheet.get_Range($"F{j}", $"F{j + 4}").Cells.Font.Size = 12;

                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Всего рабочих мест:";
                workSheet.Cells[j++, 6] = wpCount;
                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Всего единиц оборудования:";
                workSheet.Cells[j++, 6] = requestDT.Rows.Count;
                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Кол-во единиц оборудования на рабочих местах:";
                workSheet.Cells[j++, 6] = requestDT.Rows.Count - facInventoryCount;
                workSheet.get_Range($"A{j}", $"E{j}").Merge(Type.Missing);
                workSheet.Cells[j, 1] = "Кол-во единиц оборудования в инвентаре:";
                workSheet.Cells[j++, 6] = facInventoryCount;


                workSheet.Application.ActiveWorkbook.SaveAs($"{SFD.FileName}.xlsx");

                workBook.Close(false, Type.Missing, Type.Missing);

                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                excelApp = null;
                workBook = null;
                workSheet = null;

                System.GC.Collect();
                //excelProc.Kill();
            }
        }
        private void SetFontProps(string range1,string range2,string family,int size,bool bold, System.Drawing.Color color)
        {
            workSheet.get_Range(range1, range2).Cells.Font.Name = family;
            workSheet.get_Range(range1, range2).Cells.Font.Size = size;
            workSheet.get_Range(range1, range2).Cells.Font.Bold = bold;
            workSheet.get_Range(range1, range2).Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
    }
}
