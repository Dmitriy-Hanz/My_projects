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
using Word = Microsoft.Office.Interop.Word;

namespace БД_Курсач_4курс
{
    public partial class AnketStorageForm : Window
    {
        public ReportForm Report_F;
        SqlDataAdapter dec;
        public static AnketFormForm AnketForm_F;
        public AnketStorageForm()
        {
            InitializeComponent();
            CreateDGView_All();
            Ankets_DG.CanUserAddRows = false;
            Ankets_DG.CanUserDeleteRows = false;
            Ankets_DG.CanUserReorderColumns = false;
            Ankets_DG.CanUserSortColumns = false;
            Ankets_DG.IsReadOnly = true;
        }
        public void CreateDGView_All()
        {
            dec = new SqlDataAdapter();
            System.Data.DataTable fullMaster = new System.Data.DataTable();

            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                string zaproz = $"USE Jabroni SELECT CONCAT_WS(' ',фамилия, SUBSTRING(имя,0,2) + '.' ,SUBSTRING(отчество,0,2)+ '.') AS 'ФИО клиента'," +
                                "CONCAT_WS(' ','г.',город,'ул.',улица,'д №' + CONVERT(varchar,номер_дома),'К',номер_корпуса,'кв.',номер_квартир) AS 'Адрес'," +
                                $"вид_анкеты AS 'Тип анкеты', ID_анкеты_P FROM Клиент INNER JOIN Анкета ON ID_клиента_F = ID_клиента_P INNER JOIN" +
                                " Квартира ON ID_квартиры_F = ID_квартиры_P INNER JOIN Адрес ON ID_адреса_F = ID_адреса_P";
                dec.SelectCommand = new SqlCommand(zaproz, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(fullMaster);
            }

            for (int i = 0; i < fullMaster.Rows.Count; i++)
            {
                fullMaster.Rows[i][1] = fullMaster.Rows[i][1].ToString().Replace("К 0", "");
            }
            Ankets_DG.ItemsSource = fullMaster.DefaultView;
        }
        public void CreateDGView_Active()
        {
            dec = new SqlDataAdapter();
            System.Data.DataTable fullMaster = new System.Data.DataTable();

            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                string zaproz = $"USE Jabroni SELECT CONCAT_WS(' ',фамилия, SUBSTRING(имя,0,2) + '.' ,SUBSTRING(отчество,0,2)+ '.') AS 'ФИО клиента'," +
                                "CONCAT_WS(' ','г.',город,'ул.',улица,'д №' + CONVERT(varchar,номер_дома),'К',номер_корпуса,'кв.',номер_квартир) AS 'Адрес'," +
                                $"вид_анкеты AS 'Тип анкеты', ID_анкеты_P FROM Клиент INNER JOIN Анкета ON ID_клиента_F = ID_клиента_P INNER JOIN" +
                                " Квартира ON ID_квартиры_F = ID_квартиры_P INNER JOIN Адрес ON ID_адреса_F = ID_адреса_P WHERE статус = 1";
                dec.SelectCommand = new SqlCommand(zaproz, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(fullMaster);
            }

            for (int i = 0; i < fullMaster.Rows.Count; i++)
            {
                fullMaster.Rows[i][1] = fullMaster.Rows[i][1].ToString().Replace("К 0", "");
            }
            Ankets_DG.ItemsSource = fullMaster.DefaultView;
        }
        public void CreateDGView_Sold()
        {
            dec = new SqlDataAdapter();
            System.Data.DataTable fullMaster = new System.Data.DataTable();

            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                string zaproz = $"USE Jabroni SELECT CONCAT_WS(' ',фамилия, SUBSTRING(имя,0,2) + '.' ,SUBSTRING(отчество,0,2)+ '.') AS 'ФИО клиента'," +
                                "CONCAT_WS(' ','г.',город,'ул.',улица,'д №' + CONVERT(varchar,номер_дома),'К',номер_корпуса,'кв.',номер_квартир) AS 'Адрес'," +
                                $"вид_анкеты AS 'Тип анкеты', ID_анкеты_P FROM Клиент INNER JOIN Анкета ON ID_клиента_F = ID_клиента_P INNER JOIN" +
                                " Квартира ON ID_квартиры_F = ID_квартиры_P INNER JOIN Адрес ON ID_адреса_F = ID_адреса_P WHERE статус != 1";
                dec.SelectCommand = new SqlCommand(zaproz, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(fullMaster);
            }

            for (int i = 0; i < fullMaster.Rows.Count; i++)
            {
                fullMaster.Rows[i][1] = fullMaster.Rows[i][1].ToString().Replace("К 0", "");
            }
            Ankets_DG.ItemsSource = fullMaster.DefaultView;
        }
        public void ResizeColumns()
        {
            Ankets_DG.Columns[3].Visibility = Visibility.Hidden;
            Ankets_DG.Columns[0].Width = (150);
            Ankets_DG.Columns[1].Width = (300);
            Ankets_DG.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            return;
        }

        private void ShowAnket_B_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.AnketStorage_F.Ankets_DG.SelectedItem == null)
            { MessageBox.Show("Не выбрана анкета для просмотра", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);return; }
            AnketForm_F = new AnketFormForm();
            AnketForm_F.ShowDialog();
        }

        private void Back_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AnketStorage_F.Hide();
            MainWindow.MainWindow_F.Show();
            MainWindow.MainWindow_F.Owner = null;
            MainWindow.AnketStorage_F.Owner = MainWindow.MainWindow_F;
            MainWindow.AnketStorage_F.Close();
        }

        private void SelectAll_B_Click(object sender, RoutedEventArgs e)
        {
            SelectAll_B.IsEnabled = false;
            SelectActive_B.IsEnabled = true;
            SelectUnactive_B.IsEnabled = true;
            CreateDGView_All();
            ResizeColumns();
        }
        private void SelectActive_B_Click(object sender, RoutedEventArgs e)
        {
            SelectActive_B.IsEnabled = false;
            SelectAll_B.IsEnabled = true;
            SelectUnactive_B.IsEnabled = true;
            CreateDGView_Active();
            ResizeColumns();
        }
        private void SelectUnactive_B_Click(object sender, RoutedEventArgs e)
        {
            SelectUnactive_B.IsEnabled = false;
            SelectAll_B.IsEnabled = true;
            SelectActive_B.IsEnabled = true;
            CreateDGView_Sold();
            ResizeColumns();
        }

        private void REPOOORT_B_Click(object sender, RoutedEventArgs e)
        {
            Report_F = new ReportForm();
            Report_F.ShowDialog();
        }

    }
}
