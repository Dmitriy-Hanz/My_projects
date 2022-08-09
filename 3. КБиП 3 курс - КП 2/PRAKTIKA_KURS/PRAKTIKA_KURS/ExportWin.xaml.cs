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
using Word = Microsoft.Office.Interop.Word;
using EXL = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;

namespace PRAKTIKA_KURS
{
    public partial class ExportWin : Window
    {
        public ExportWin()
        {
            InitializeComponent();
        }
        private void ExportWord_Click(object sender, RoutedEventArgs e)
        {
            Word._Application application = new Word.Application();
            application.Documents.Add();
            application.ActiveDocument.Select();
            application.Selection.TypeText($"Общее количество поездок: {MainWindow.TaR.ridesCount}\rКоличество \"холостых\" поездок (пустой лифт): {MainWindow.TaR.emptyRidesCount}\r");
            application.Selection.TypeText($"Суммарный перемещенный вес: {MainWindow.TaR.generalWeight}\rКол-во созданных объектов \"человек\": {MainWindow.TaR.genPasCount}");
            application.Application.ActiveDocument.SaveAs2(@"C:\Users\User\source\repos\PRAKTIKA_KURS\PRAKTIKA_KURS\Doc\Report.docx");
            application.Visible = true;
        }
        private void ExportExele_Click(object sender, RoutedEventArgs e)
        {
            EXL.Application exele = new EXL.Application();
            exele.SheetsInNewWorkbook = 1;
            EXL.Workbook workBook = exele.Workbooks.Add(Type.Missing);
            exele.DisplayAlerts = false;
            EXL.Worksheet sheet = (EXL.Worksheet)exele.Worksheets.get_Item(1);
            sheet.Name = "Отчет";

            sheet.Cells[1, 1] = "Общее количество поездок:";sheet.Cells[1, 2] = MainWindow.TaR.ridesCount;
            sheet.Cells[2, 1] = "Количество \"холостых\" поездок (пустой лифт):"; sheet.Cells[2, 2] = MainWindow.TaR.emptyRidesCount;
            sheet.Cells[3, 1] = "Суммарный перемещенный вес:"; sheet.Cells[3, 2] = MainWindow.TaR.generalWeight;
            sheet.Cells[4, 1] = "Кол-во созданных объектов \"человек\":"; sheet.Cells[4, 2] = MainWindow.TaR.genPasCount;

            sheet.get_Range("A1", "A5").Select();
            sheet.UsedRange.EntireColumn.AutoFit();

            exele.Application.ActiveWorkbook.SaveAs(@"C:\Users\User\source\repos\PRAKTIKA_KURS\PRAKTIKA_KURS\Doc\Report.xlsx");
            exele.Visible = true;
        }
    }
}
