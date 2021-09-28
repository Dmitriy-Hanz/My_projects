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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Word = Microsoft.Office.Interop.Word;

namespace KAPEYAP_KURS
{
    public partial class CalcWin1 : Window
    {
        public static WorkersTabWin WWin;
        public static ZPTab ZPWin;
        public CalcWin1()
        {
            InitializeComponent();
            WorkersGrid.ItemsSource = MainMenuWin.workerstable.DefaultView;
            //WorkersGrid.Columns[WorkersGrid.Columns.Count - 1].Header = "Дата принятия";
        }

        private void BackToMenuB_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWin.MAIN.Show();
            MainMenuWin.CalcWindow.Hide();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string s = "SELECT * FROM [dbo].[WorkersList] WHERE ";
            ClearButton_Click(null, null);
            ComboBoxItem si = (ComboBoxItem)SearchMode.SelectedItem;
            if (si == null) { MessageBox.Show("Не выбрана категория для поиска"); return; }
            if (si.Content.ToString() == "Имя") { s = s + $"Имя = '{SearchPole.Text}'"; }
            if (si.Content.ToString() == "Фамилия") { s = s + $"Фамилия = '{SearchPole.Text}'"; }
            if (si.Content.ToString() == "Отчество") { s = s + $"Отчество = '{SearchPole.Text}'"; }
            if (si.Content.ToString() == "Должность") { s = s + $"Должность = '{SearchPole.Text}'"; }
            if (si.Content.ToString() == "Личный номер") { s = s + $"ID = '{SearchPole.Text}'"; }
            MainMenuWin.workerstable = DBSelector.Select("WorkersList", s);
            WorkersGrid.ItemsSource = MainMenuWin.workerstable.DefaultView;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            WorkersGrid.Items.Refresh();
        }

        private void SearchMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        DataTable C;
        object[] A;
        private void GridRowHandler(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView g1;
            try
            {
            if (e.AddedCells[0].Item is DataRowView)
            { g1= (DataRowView)(e.AddedCells[0].Item); } else { return; }
            }
            catch (Exception)
            { return; }
            
            int WINDEX = (int)g1.Row.ItemArray[0];
            string zapros = $"SELECT dbo.WorkersList.ID,dbo.WorkersList.Фамилия, dbo.WorkersList.Имя,  dbo.WorkersList.Отчество,  dbo.WorkersList.Должность, dbo.WorkersList.[Дата принятия], dbo.TimeNormList.[Норма времени], dbo.ZPDataList.[Кол-во отр дней], dbo.ZPDataList.Оклад, dbo.ZPDataList.Премия, dbo.ZPDataList.Надбавка, dbo.ZPDataList.Доплата, dbo.ZPDataList.[под-ный налог], dbo.ZPDataList.Начислено FROM dbo.WorkersList CROSS JOIN dbo.ZPDataList CROSS JOIN dbo.TimeNormList WHERE dbo.WorkersList.ID = '{WINDEX}' and dbo.ZPDataList.ID = '{WINDEX}'";
            C = DBSelector.Select("", zapros);
            A = C.Rows[0].ItemArray;
            ФормаТруда_CB.SelectedItem = ФормаТруда_CB.Items[0];
            СистемаТруда_CB.SelectedItem = СистемаТруда_CB.Items[0];
            ТабельныйID_TB.Text = A[0].ToString();
            Фамилия_TB.Text = A[1].ToString();
            Имя_TB.Text = A[2].ToString();
            Отчество_TB.Text = A[3].ToString();
            Должность_TB.Text = A[4].ToString();
            ДатаПринятия_TB.Text = A[5].ToString();
            НормаВремени_TB.Text = A[6].ToString();
            КолВоОтрДней_TB.Text = A[7].ToString();
            Оклад_TB.Text = A[8].ToString();

            if (A[9] is DBNull != true) { if (Convert.ToDouble(A[9]) != 0) { Премия_TB.Text = A[9].ToString(); } }
            if (A[10] is DBNull != true) { if (Convert.ToDouble(A[10]) != 0) { Надбавка_TB.Text = A[10].ToString(); } }
            if (A[11] is DBNull != true) { if (Convert.ToDouble(A[11]) != 0) { Доплата_TB.Text = A[11].ToString(); } }
            if (A[12] is DBNull != true) { if (Convert.ToDouble(A[12]) != 0) { ПодоходныйНалог_TB.Text = A[12].ToString(); } }
            Начислено_TB.Text = A[13].ToString();
        }
        private void ClearFieldsB_Click(object sender, RoutedEventArgs e)
        {
            ТабельныйID_TB.Text = "";
            Фамилия_TB.Text = "";
            Имя_TB.Text = "";
            Отчество_TB.Text = "";
            Должность_TB.Text = "";
            ДатаПринятия_TB.Text = "";
            НормаВремени_TB.Text = "";
            КолВоОтрДней_TB.Text = "";
            Оклад_TB.Text = "";
            Премия_TB.Text = "";
            Надбавка_TB.Text = "";
            Доплата_TB.Text = "";
            Начислено_TB.Text = "";
        }
        private bool Validator()
        {
            if (Char.IsUpper(Фамилия_TB.Text[0]) == false || Char.IsUpper(Фамилия_TB.Text[0]) == false || Char.IsUpper(Отчество_TB.Text[0]) == false)
            { MessageBox.Show("Поля ФИО должны быть с большой буквы"); return false; }
            if (DateTime.TryParse(ДатаПринятия_TB.Text, out DateTime gg1) == false)
            { MessageBox.Show("Дата принятия имеет не верный формат"); return false; }
            if (ФормаТруда_CB.SelectedItem == null) { MessageBox.Show("Не выбрана форма оплаты труда"); return false; }
            if (СистемаТруда_CB.SelectedItem == null) { MessageBox.Show("Не выбрана система оплаты труда"); return false; }

            if (НормаВремени_TB.Text != "" & int.TryParse(НормаВремени_TB.Text, out int gg2) == false)
            { MessageBox.Show("Данные в поле Норма времени имеют неверный формат"); return false; }
            if (КолВоОтрДней_TB.Text != "" & int.TryParse(НормаВремени_TB.Text, out int gg3) == false)
            { MessageBox.Show("Данные в поле Кол-во отр. дней имеют неверный формат"); return false; }
            if (Оклад_TB.Text != "" & double.TryParse(Оклад_TB.Text, out double gg4) == false)
            { MessageBox.Show("Данные в поле Оклад имеют неверный формат"); return false; }

            if (Надбавка_TB.Text != "" & double.TryParse(Надбавка_TB.Text, out double gg5) == false)
            { MessageBox.Show("Данные в поле Надбавка имеют неверный формат"); return false; }
            if (Доплата_TB.Text != "" & double.TryParse(Доплата_TB.Text, out double gg6) == false)
            { MessageBox.Show("Данные в поле Надбавка имеют неверный формат"); return false; }
            if (Премия_TB.Text != "" & double.TryParse(Премия_TB.Text, out double gg7) == false)
            { MessageBox.Show("Данные в поле Премия имеют неверный формат"); return false; }
            if (ПодоходныйНалог_TB.Text != "" & double.TryParse(ПодоходныйНалог_TB.Text, out double gg8) == false)
            { MessageBox.Show("Данные в поле Подоходный налог имеют неверный формат"); return false; }

            if (Начислено_TB.Text != "" & double.TryParse(Начислено_TB.Text, out double gg9) == false)
            { MessageBox.Show("Данные в поле Начислено имеют неверный формат"); return false; }
            return true;
        }
        private void Calculate_B_Click(object sender, RoutedEventArgs e)
        {
            if (Validator() == false) { return; }
            double fakOklad = (double.Parse(Оклад_TB.Text) / int.Parse(НормаВремени_TB.Text)) * int.Parse(КолВоОтрДней_TB.Text);
            double zp = fakOklad;
            if (Надбавка_TB.Text != "") { zp = zp + double.Parse(Надбавка_TB.Text); }
            if (Доплата_TB.Text != "") { zp = zp + double.Parse(Доплата_TB.Text); }
            if (Премия_TB.Text != "") { zp = zp + double.Parse(Премия_TB.Text); }
            if (ПодоходныйНалог_TB.Text != "") { zp = zp - double.Parse(ПодоходныйНалог_TB.Text); }
            Начислено_TB.Text = zp.ToString();
        }
        private void RememberData_B_Click(object sender, RoutedEventArgs e)
        {
            if (Validator() == false) { return; }

            SqlConnection con = new SqlConnection(MainMenuWin.connectionString);
            SqlCommand cmd;
            con.Open();
            
            cmd = new SqlCommand($"UPDATE WorkersList SET Имя = '{Имя_TB.Text}', Фамилия = '{Фамилия_TB.Text}', Отчество = '{Отчество_TB.Text}',Должность = '{Должность_TB.Text}',[Дата принятия] = '{DateTime.Parse(ДатаПринятия_TB.Text)}' WHERE ID = '{ТабельныйID_TB.Text}'", con);
            cmd.ExecuteNonQuery();

            string z = $"UPDATE ZPDataList SET [Кол-во отр дней] = '{КолВоОтрДней_TB.Text}', Оклад = '{Оклад_TB.Text.Replace(",", ".")}', ";
            if (Премия_TB.Text == "") { z = z + "Премия = '0', "; } else { z = z + $"Премия = '{Премия_TB.Text.Replace(",", ".")}', "; }
            if (Надбавка_TB.Text == "") { z = z + "Надбавка = '0', "; } else { z = z + $"Надбавка = '{Надбавка_TB.Text.Replace(",", ".")}', "; }
            if (Доплата_TB.Text == "") { z = z + "Доплата = '0', "; } else { z = z + $"Доплата = '{Доплата_TB.Text.Replace(",", ".")}', "; }
            if (ПодоходныйНалог_TB.Text == "") { z = z + "[под-ный налог] = '0', "; } else { z = z + $"[под-ный налог] = '{ПодоходныйНалог_TB.Text.Replace(",",".")}', "; }
            z = z + $"Начислено = '{Начислено_TB.Text.Replace(",", ".")}' WHERE ID = '{ТабельныйID_TB.Text}'";

            cmd = new SqlCommand(z, con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"UPDATE TimeNormList SET [Норма времени] = '{int.Parse(НормаВремени_TB.Text)}'", con);
            cmd.ExecuteNonQuery();
            MainMenuWin.workerstable = DBSelector.Select("WorkersList", "SELECT * FROM [dbo].[WorkersList]");
            WorkersGrid.ItemsSource = MainMenuWin.workerstable.DefaultView;
        }

        private void WorkersTab_B_Click(object sender, RoutedEventArgs e)
        {
            WWin = new WorkersTabWin();
            WWin.Show();
        }

        private void ZPTab_B_Click(object sender, RoutedEventArgs e)
        {
            ZPWin = new ZPTab();
            ZPWin.Show();
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            string s = "SELECT * FROM [dbo].[WorkersList]";
            MainMenuWin.workerstable = DBSelector.Select("", s);
            WorkersGrid.ItemsSource = MainMenuWin.workerstable.DefaultView;
        }

        private void ExportWord(object sender, RoutedEventArgs e)
        {
            Word._Application application = new Word.Application();
            application.Documents.Add();
            application.ActiveDocument.Select();
            application.Selection.TypeText("Расчетный листок\r");
            application.Selection.TypeText($"Табельный номер: {ТабельныйID_TB.Text}\r");
            application.Selection.TypeText($"Должность: {Должность_TB.Text}\r");
            application.Selection.TypeText($"Оклад: {Оклад_TB.Text}\r");
            application.Selection.TypeText($"Норма времени: {НормаВремени_TB.Text}\r");
            application.Selection.TypeText($"Надбавка: {Надбавка_TB.Text}\rДоплата: {Доплата_TB.Text}\rПремия: {Премия_TB.Text}\r"); 
            application.Selection.TypeText($"К выдаче: {Начислено_TB.Text}\r");
            application.Application.ActiveDocument.SaveAs2($@"C:\Users\User\source\repos\KAPEYAP_KURS\KAPEYAP_KURS\Doc\РР{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}.docx");

            application.Visible = true;
        }
    }
}
