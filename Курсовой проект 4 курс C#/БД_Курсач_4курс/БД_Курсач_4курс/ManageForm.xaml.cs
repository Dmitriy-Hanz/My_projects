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
using System.Data.SqlClient;

namespace БД_Курсач_4курс
{
    public partial class ManageForm : Window
    {
        SqlDataAdapter dec;
        public ManageForm()
        {
            InitializeComponent();
            dec = new SqlDataAdapter();
            System.Data.DataTable fullManager = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand("USE Jabroni SELECT CONCAT_WS(' ',фамилия,имя,отчество),ID_менеджера_P from Менеджер", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(fullManager);
            }
            foreach (System.Data.DataRow row in fullManager.Rows)
            {
                Managers_LB.Items.Add(new ListBoxItem { Content = row[0].ToString(), Name = $"i{row[1].ToString()}" });
            }
            return;
        }

        private void Confirm_LB_Click(object sender, RoutedEventArgs e)
        {
            if (Managers_LB.SelectedItem == null)
            { MessageBox.Show("Вы должны выбрать менеджера из списка доступных", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }

            System.Data.DataRowView dick = (System.Data.DataRowView)MainWindow.AnketDealler_F.Ankets_DG.SelectedItem;
            ListBoxItem LBI = (ListBoxItem)MainWindow.AnketDealler_F.Ankets_LB.SelectedItem;
            ListBoxItem manageLBI = (ListBoxItem)Managers_LB.SelectedItem;
            System.Data.DataTable miniStatus = new System.Data.DataTable();

            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand($"USE Jabroni SELECT (SELECT вид_анкеты FROM Анкета WHERE ID_анкеты_P = {LBI.Name.Replace("i","")}),вид_анкеты FROM Анкета WHERE ID_анкеты_P = {dick[3]}", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(miniStatus);
                int status=0;
                string qPart="";
                string nowDate = $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
                if (miniStatus.Rows[0][0].ToString() == "Покупка" || miniStatus.Rows[0][0].ToString() == "Продажа") { qPart = $" дата_продажи = '{nowDate}'"; status = 2; }
                if (miniStatus.Rows[0][0].ToString() == "Обмен") { qPart = $" дата_обмена = '{nowDate}'"; status = 3; }
                if (miniStatus.Rows[0][0].ToString() == "Продажа/обмен" & miniStatus.Rows[0][1].ToString() == "Продажа/обмен") { qPart = $" дата_обмена = '{nowDate}'"; status = 3; }
                if (miniStatus.Rows[0][0].ToString() == "Продажа/обмен" & miniStatus.Rows[0][1].ToString() == "Обмен") { qPart = $" дата_обмена = '{nowDate}'"; status = 3; }
                if (miniStatus.Rows[0][0].ToString() == "Продажа/обмен" & miniStatus.Rows[0][1].ToString() == "Продажа") { qPart = $" дата_продажи = '{nowDate}'"; status = 2; }

                miniStatus = new System.Data.DataTable();
                dec.SelectCommand = new SqlCommand($"USE Jabroni SELECT ID_квартиры_F FROM Анкета WHERE ID_анкеты_P = {LBI.Name.Replace("i", "")} or ID_анкеты_P = {dick[3]}", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(miniStatus);

                dec.SelectCommand = new SqlCommand($"USE Jabroni UPDATE Анкета SET статус = {status}, ID_менеджера_F = {manageLBI.Name.Replace("i", "")} WHERE ID_анкеты_P = {LBI.Name.Replace("i", "")} or ID_анкеты_P = {dick[3]}", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.SelectCommand = new SqlCommand($"USE Jabroni UPDATE Квартира SET {qPart} WHERE ID_квартиры_P = {miniStatus.Rows[0][0]} or ID_квартиры_P = {miniStatus.Rows[1][0]}", con);
                dec.SelectCommand.ExecuteNonQuery();
            }

            MessageBox.Show("Сделка прошла успешно","", MessageBoxButton.OK, MessageBoxImage.Information);

            MainWindow.AnketDealler_F.ReloadView();
        }
    }
}
