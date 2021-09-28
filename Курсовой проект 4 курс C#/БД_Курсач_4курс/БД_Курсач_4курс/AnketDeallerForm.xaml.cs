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
    public partial class AnketDeallerForm : Window
    {
        public System.Data.DataTable MiniAnketsDB;
        public SqlDataAdapter dec;
        public ManageForm ManageForm_F;
        public AnketDeallerForm()
        {
            InitializeComponent();
            CreateDGView();
            Ankets_DG.CanUserAddRows = false;
            Ankets_DG.CanUserDeleteRows = false;
            Ankets_DG.CanUserReorderColumns = false;
            Ankets_DG.CanUserSortColumns = false;
            Ankets_DG.IsReadOnly = true;
        }

        public void ResizeColumns()
        {
            Ankets_DG.Columns[3].Visibility = Visibility.Hidden;
            Ankets_DG.Columns[0].Width = (150);
            Ankets_DG.Columns[1].Width = (300);
            Ankets_DG.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            return;
        }
        public void CreateDGView()
        {
            dec = new SqlDataAdapter();
            System.Data.DataTable fullMaster = new System.Data.DataTable();

            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                string zaproz = $"USE Jabroni SELECT CONCAT_WS(' ',фамилия, SUBSTRING(имя,0,2) + '.' ,SUBSTRING(отчество,0,2)+ '.') AS 'ФИО клиента'," +
                                "CONCAT_WS(' ','г.',город,'ул.',улица,'д №' + CONVERT(varchar,номер_дома),'К',номер_корпуса,'кв.',номер_квартир) AS 'Адрес'," +
                                $"вид_анкеты AS 'Тип анкеты', ID_анкеты_P FROM Клиент INNER JOIN Анкета ON ID_клиента_F = ID_клиента_P INNER JOIN" +
                                $" Квартира ON ID_квартиры_F = ID_квартиры_P INNER JOIN Адрес ON ID_адреса_F = ID_адреса_P WHERE пароль != '{MainWindow.PASSWORD}' and статус = 1";
                dec.SelectCommand = new SqlCommand(zaproz, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(fullMaster);
            }

            for (int i = 0; i < fullMaster.Rows.Count; i++)
            {
                if (fullMaster.Rows[i][1].ToString().Contains("К 0") == true)
                { fullMaster.Rows[i][1] = fullMaster.Rows[i][1].ToString().Remove(fullMaster.Rows[i][1].ToString().IndexOf("К 0"), 3); }
            }
            Ankets_DG.ItemsSource = fullMaster.DefaultView;
            ReloadView();
        }
        public void ReloadView()
        {
            Ankets_LB.Items.Clear();
            MiniAnketsDB = new System.Data.DataTable();
            string ss = $"USE Jabroni SELECT CONCAT('г. ',город,' ул. ',улица, ' д №', номер_дома, ' К ',номер_корпуса, ' кв. ',номер_квартир),вид_анкеты,статус,ID_анкеты_P " +
                        $"FROM Адрес INNER JOIN Квартира ON ID_адреса_F = ID_адреса_P INNER JOIN Анкета ON ID_квартиры_F = ID_квартиры_P INNER JOIN Клиент ON ID_клиента_P = ID_клиента_F WHERE пароль = '{MainWindow.PASSWORD}'";
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand(ss, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(MiniAnketsDB);
            }
            string listS="";
            foreach (System.Data.DataRow row in MiniAnketsDB.Rows)
            {
                listS = row[0].ToString();
                if (listS.Contains("К 0") == true) { listS = listS.Remove(listS.IndexOf("К 0"), 3); }
                if ((int)row[2] != 1) { listS = $"(НЕ АКТИВНА) {listS}"; } else { listS += " " + row[1]; }
                
                Ankets_LB.Items.Add(new ListBoxItem { Content = listS, Name = $"i{row[3]}" });
            }
            return;
        }

        private void FindAnkets_B_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem LBI = (ListBoxItem)Ankets_LB.SelectedItem;
            MiniAnketsDB = new System.Data.DataTable();
            string message = "Выберите анкету из списка ваших анкет для поиска подходящих вариантов в базе данных";
            if (Ankets_LB.SelectedItem == null) { MessageBox.Show(message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand($"USE Jabroni SELECT вид_анкеты,площадь from Анкета INNER JOIN Квартира ON ID_квартиры_P = ID_квартиры_F INNER JOIN Адрес ON ID_адреса_P = ID_адреса_F WHERE ID_анкеты_P = '{LBI.Name.Replace("i","")}'", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(MiniAnketsDB);

                string vid1 = "";
                if (MiniAnketsDB.Rows[0][0].ToString().Contains("Покупка") == true) { vid1 = $"(вид_анкеты = 'Продажа') or (вид_анкеты = 'Продажа/обмен' and (ABS(площадь - {MiniAnketsDB.Rows[0][1]}) < (площадь/100)*10))"; }
                if (MiniAnketsDB.Rows[0][0].ToString().Contains("Продажа") == true) { vid1 = "вид_анкеты = 'Покупка'"; }
                if (MiniAnketsDB.Rows[0][0].ToString().Contains("Обмен") == true) { vid1 = $"(вид_анкеты = 'Продажа' or вид_анкеты = 'Продажа/обмен') and (ABS(площадь - {MiniAnketsDB.Rows[0][1]}) < (площадь/100)*10)"; }
                if (MiniAnketsDB.Rows[0][0].ToString().Contains("Продажа/обмен") == true) { vid1 = $"(вид_анкеты = 'Покупка') or (вид_анкеты = 'Обмен' and (ABS(площадь - {MiniAnketsDB.Rows[0][1]}) < (площадь/100)*10))"; }

                MiniAnketsDB = new System.Data.DataTable();
                string zaproz = $"USE Jabroni SELECT CONCAT_WS(' ',фамилия, SUBSTRING(имя,0,2) + '.' ,SUBSTRING(отчество,0,2)+ '.') AS 'ФИО клиента'," +
                                "CONCAT_WS(' ','г.',город,'ул.',улица,'д №' + CONVERT(varchar,номер_дома),'К',номер_корпуса,'кв.',номер_квартир) AS 'Адрес'," +
                                $"вид_анкеты AS 'Тип анкеты', ID_анкеты_P FROM Клиент INNER JOIN Анкета ON ID_клиента_F = ID_клиента_P INNER JOIN" +
                                $" Квартира ON ID_квартиры_F = ID_квартиры_P INNER JOIN Адрес ON ID_адреса_F = ID_адреса_P WHERE пароль != '{MainWindow.PASSWORD}' and ({vid1}) and статус = 1";
                dec.SelectCommand = new SqlCommand(zaproz, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(MiniAnketsDB);

                for (int i = 0; i < MiniAnketsDB.Rows.Count; i++)
                {
                    if (MiniAnketsDB.Rows[i][1].ToString().Contains("К 0") == true)
                    { MiniAnketsDB.Rows[i][1] = MiniAnketsDB.Rows[i][1].ToString().Remove(MiniAnketsDB.Rows[i][1].ToString().IndexOf("К 0"), 3); }
                }
                Ankets_DG.ItemsSource = MiniAnketsDB.DefaultView;
                ReloadView();
            }
        }

        private void Back_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AnketDealler_F.Hide();
            MainWindow.MainWindow_F.Show();
            MainWindow.MainWindow_F.Owner = null;
            MainWindow.AnketDealler_F.Owner = MainWindow.MainWindow_F;
            MainWindow.AnketDealler_F.Close();
        }
        private void CreateNew_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NewAnketa_F = new NewAnketaForm { Width = 500, ResizeMode = ResizeMode.NoResize };
            MainWindow.NewAnketa_F.CloseForm_B.Visibility = Visibility.Visible;
            MainWindow.NewAnketa_F.ShowDialog();
        }

        private void MakeDeal_B_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem LBI = (ListBoxItem)Ankets_LB.SelectedItem;
            string message = "Вы должны выбрать анкету человека, с которым хотите заключить сделку, из таблицы анкет и свою анкету";
            if (Ankets_DG.SelectedItem == null || Ankets_LB.SelectedItem == null)
            { MessageBox.Show(message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }
            if (LBI.Content.ToString().Contains("(НЕ АКТИВНА)") == true)
            { MessageBox.Show("Вы не можете использовать анкету, по которой уже заключена сделка", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }

            ManageForm_F = new ManageForm();
            ManageForm_F.ShowDialog();
        }

        private void ShowAll_B_Click(object sender, RoutedEventArgs e)
        {
            CreateDGView();
            ResizeColumns();
        }
    }
}
