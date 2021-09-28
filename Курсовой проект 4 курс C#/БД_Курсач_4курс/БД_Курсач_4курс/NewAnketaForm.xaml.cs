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
    public partial class NewAnketaForm : Window
    {
        System.Data.DataTable password_dt;
        SqlDataAdapter dec;
        ListBoxItem LBI;
        public NewAnketaForm()
        {
            InitializeComponent();
            dec = new SqlDataAdapter();
            ReloadView();
        }
        public void ReloadView()
        {
            Ankets_LB.Items.Clear();
            password_dt = new System.Data.DataTable();
            string ss = $"USE Jabroni SELECT CONCAT('г. ',город,' ул. ',улица, ' д №', номер_дома, ' К ',номер_корпуса, ' кв. ',номер_квартир),вид_анкеты,статус,ID_анкеты_P " +
                        $"FROM Адрес INNER JOIN Квартира ON ID_адреса_F = ID_адреса_P INNER JOIN Анкета ON ID_квартиры_F = ID_квартиры_P INNER JOIN Клиент ON ID_клиента_P = ID_клиента_F WHERE пароль = '{MainWindow.PASSWORD}'";
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand(ss, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(password_dt);
            }
            string listS = "";
            foreach (System.Data.DataRow row in password_dt.Rows)
            {
                listS = row[0].ToString();
                if (listS.Contains("К 0") == true) { listS = listS.Remove(listS.IndexOf("К 0"), 3); }
                if ((int)row[2] != 1) { listS = $"(НЕ АКТИВНА) {listS}"; } else { listS += " " + row[1]; }

                Ankets_LB.Items.Add(new ListBoxItem { Content = listS, Name = $"i{row[3]}" });
            }
            return;
        }
        public bool ValidateAnket()
        {
            password_dt = new System.Data.DataTable();
            if (AnkType_CB.SelectedItem == null)
            { MessageBox.Show("Укажите тип анкеты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }
            if (AdrCity_TB.Text == "" || AdrStreet_TB.Text == "" || AdrFlatNum_TB.Text == "" || KvRoomsCount_TB.Text == "" || KvFloor_TB.Text == "" || KvSquare_TB.Text == "")
            { MessageBox.Show("Не все поля заполнены", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }

            if (int.TryParse(AdrFlatNum_TB.Text, out int r) == false || int.TryParse(KvRoomsCount_TB.Text, out r) == false || int.TryParse(KvFloor_TB.Text, out r) == false || double.TryParse(KvSquare_TB.Text, out double rr) == false)
            { MessageBox.Show("Введенные данные имеют неверный формат", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }
            if (AdrHouseNum_TB.Text != "" & int.TryParse(AdrHouseNum_TB.Text, out r) == false)
            { MessageBox.Show("Введенные данные имеют неверный формат", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }
            if (AdrKorpNum_TB.Text != "" & int.TryParse(AdrKorpNum_TB.Text, out r) == false)
            { MessageBox.Show("Введенные данные имеют неверный формат", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }
            LBI = (ListBoxItem)AnkType_CB.SelectedItem;
            int korpNum = 0;
            
            if(SaveAnket_B.Visibility == Visibility.Visible) { return true; }
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                if(AdrKorpNum_TB.Text != "") { korpNum = int.Parse(AdrKorpNum_TB.Text); }
                string sqlcom = $"USE Jabroni SELECT ID_анкеты_P FROM Клиент INNER JOIN Анкета ON ID_клиента_F = ID_клиента_P INNER JOIN " +
                                $"Квартира ON ID_квартиры_F = ID_квартиры_P INNER JOIN Адрес ON ID_адреса_F = ID_адреса_P " +
                                $"WHERE пароль = '{MainWindow.PASSWORD}' and вид_анкеты = '{LBI.Content}' and город = '{AdrCity_TB.Text}' and " +
                                $"улица = '{AdrStreet_TB.Text}' and номер_дома = '{AdrHouseNum_TB.Text}' and номер_корпуса = '{korpNum}' and номер_квартир = '{AdrFlatNum_TB.Text}'";

                dec.SelectCommand = new SqlCommand(sqlcom, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(password_dt);
                if(password_dt.Rows.Count != 0)
                { MessageBox.Show("У вас уже есть анкета данного вида", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }
            }
            return true;
        }

        private void CreateAnket_B_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateAnket() == false) { return; };
            password_dt = new System.Data.DataTable();
            int balkonsFlag=1;
            if (KvBalcon_CHB.IsChecked == false) { balkonsFlag = 0; }

            if (SaveAnket_B.Visibility == Visibility.Visible)
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
                {
                    password_dt = new System.Data.DataTable();
                    LBI = (ListBoxItem)Ankets_LB.SelectedItem;
                    string ss = $"USE Jabroni SELECT ID_анкеты_P,ID_квартиры_F,ID_адреса_F FROM Анкета INNER JOIN Квартира ON Анкета.ID_квартиры_F = Квартира.ID_квартиры_P WHERE ID_анкеты_P = { LBI.Name.Substring(1)}";
                    con.Open();
                    dec.SelectCommand = new SqlCommand(ss, con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.Fill(password_dt);
                    ComboBoxItem CBI = (ComboBoxItem)AnkType_CB.SelectedItem;
                    if (KvBalcon_CHB.IsChecked == false) { balkonsFlag = 0; } else { balkonsFlag = 1; }
                    dec.SelectCommand = new SqlCommand($"USE Jabroni UPDATE Анкета SET вид_анкеты = '{CBI.Content.ToString()}' WHERE ID_анкеты_P = {password_dt.Rows[0][0]}", con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.SelectCommand = new SqlCommand($"USE Jabroni UPDATE Квартира SET этаж='{KvFloor_TB.Text}',площадь = '{KvSquare_TB.Text}', колво_комнат='{KvRoomsCount_TB.Text}',наличие_балконов = '{balkonsFlag}' WHERE ID_квартиры_P = {password_dt.Rows[0][1]}", con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.SelectCommand = new SqlCommand($"USE Jabroni UPDATE Адрес SET город='{AdrCity_TB.Text}',улица='{AdrStreet_TB.Text}',номер_дома='{AdrHouseNum_TB.Text}',номер_корпуса='{AdrKorpNum_TB.Text}',номер_квартир = '{AdrFlatNum_TB.Text}' WHERE ID_адреса_P = {password_dt.Rows[0][2]}", con);
                    dec.SelectCommand.ExecuteNonQuery();
                    return;
                }
            }

            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                int klientID = 0, flatID = 0,adresID=0;
                con.Open();

                password_dt = new System.Data.DataTable();
                dec.SelectCommand = new SqlCommand($"USE Jabroni EXEC SELECTАдрес '{AdrCity_TB.Text}','{AdrStreet_TB.Text}','{AdrHouseNum_TB.Text}','{AdrKorpNum_TB.Text}','{AdrFlatNum_TB.Text}'", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(password_dt);
                if (password_dt.Rows.Count == 0)
                {
                    password_dt = new System.Data.DataTable();
                    dec.SelectCommand = new SqlCommand($"USE Jabroni EXEC INSERTАдрес_P '{AdrCity_TB.Text}','{AdrStreet_TB.Text}','{AdrHouseNum_TB.Text}','{AdrKorpNum_TB.Text}','{AdrFlatNum_TB.Text}'", con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.SelectCommand = new SqlCommand($"USE Jabroni EXEC SELECTАдрес '{AdrCity_TB.Text}','{AdrStreet_TB.Text}','{AdrHouseNum_TB.Text}','{AdrKorpNum_TB.Text}','{AdrFlatNum_TB.Text}'", con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.Fill(password_dt);
                    adresID = (int)password_dt.Rows[0][0];
                }
                password_dt = new System.Data.DataTable();
                dec.SelectCommand = new SqlCommand($"USE Jabroni SELECT ID_квартиры_P FROM Квартира WHERE колво_комнат={KvRoomsCount_TB.Text} and этаж={KvFloor_TB.Text} and площадь = {KvSquare_TB.Text} and наличие_балконов = {balkonsFlag} and ID_адреса_F = {adresID}", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(password_dt);
                if (password_dt.Rows.Count == 0)
                {
                    password_dt = new System.Data.DataTable();
                    dec.SelectCommand = new SqlCommand($"USE Jabroni INSERT INTO Квартира VALUES('{KvRoomsCount_TB.Text}','{KvFloor_TB.Text}','{KvSquare_TB.Text}','{balkonsFlag}',NULL,NULL,'{adresID}')", con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.SelectCommand = new SqlCommand($"USE Jabroni SELECT ID_квартиры_P FROM Квартира WHERE колво_комнат={KvRoomsCount_TB.Text} and этаж={KvFloor_TB.Text} and площадь = {KvSquare_TB.Text} and наличие_балконов = {balkonsFlag} and ID_адреса_F = {adresID}", con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.Fill(password_dt);
                    flatID = (int)password_dt.Rows[0][0];
                }
                password_dt = new System.Data.DataTable();
                dec.SelectCommand = new SqlCommand($"USE Jabroni SELECT ID_клиента_P FROM Клиент WHERE пароль = '{MainWindow.PASSWORD}'", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(password_dt);
                klientID = (int)password_dt.Rows[0][0];

                dec.SelectCommand = new SqlCommand($"USE Jabroni INSERT INTO Анкета VALUES('{LBI.Content.ToString()}','1','{flatID}','{klientID}',NULL)", con);
                dec.SelectCommand.ExecuteNonQuery();
            }
            ReloadView();
        }
        private void ClearFields_B_Click(object sender, RoutedEventArgs e)
        {
            AnkType_CB.SelectedItem = null;
            AdrCity_TB.Clear();
            AdrStreet_TB.Clear();
            AdrHouseNum_TB.Clear();
            AdrKorpNum_TB.Clear();
            AdrFlatNum_TB.Clear();
            KvRoomsCount_TB.Clear();
            KvFloor_TB.Clear();
            KvSquare_TB.Clear();
            KvBalcon_CHB.IsChecked = false;
        }

        private void DeleteAnket_B_Click(object sender, RoutedEventArgs e)
        {
            if(Ankets_LB.SelectedItem == null) { MessageBox.Show("Не выбрана анкета для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }
            string message = "Вы действительно хотите удалить данную анкету из базы данных. Удаленную анкету нельзя будет восстановить";
            MessageBoxResult ms_res = MessageBox.Show(message,"Предупреждение",MessageBoxButton.YesNo,MessageBoxImage.Information);
            if(ms_res == MessageBoxResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
                {
                    password_dt = new System.Data.DataTable();
                    LBI = (ListBoxItem)Ankets_LB.SelectedItem;
                    string ss = $"USE Jabroni SELECT ID_анкеты_P,ID_квартиры_F FROM Анкета INNER JOIN Квартира ON Анкета.ID_квартиры_F = Квартира.ID_квартиры_P WHERE ID_анкеты_P = { LBI.Name.Substring(1)}";
                    
                    con.Open();
                    dec.SelectCommand = new SqlCommand(ss, con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.Fill(password_dt);
                    int FlatID = (int)password_dt.Rows[0][1];
                    dec.SelectCommand = new SqlCommand($"USE Jabroni DELETE FROM Анкета WHERE ID_анкеты_P = {password_dt.Rows[0][0]}", con);
                    dec.SelectCommand.ExecuteNonQuery();

                    dec.SelectCommand = new SqlCommand($"USE Jabroni SELECT ID_анкеты_P FROM Анкета INNER JOIN Квартира ON ID_квартиры_F = ID_квартиры_P WHERE ID_квартиры_F = {FlatID}", con);
                    password_dt = new System.Data.DataTable();
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.Fill(password_dt);
                    if (password_dt.Rows.Count == 0)
                    {
                        dec.SelectCommand = new SqlCommand($"USE Jabroni DELETE FROM Квартира WHERE ID_квартиры_P = {FlatID}", con);
                        dec.SelectCommand.ExecuteNonQuery();
                    }
                }
            }
            ReloadView();
        }
        private void ChangeAnket_B_Click(object sender, RoutedEventArgs e)
        {
            if (Ankets_LB.SelectedItem == null) { MessageBox.Show("Не выбрана анкета для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation); }
            string message = "В случае изменения выбранной анкеты текущие данные на форме будут потеряны";
            MessageBoxResult ms_res = MessageBox.Show(message, "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (ms_res == MessageBoxResult.Yes)
            {
                ClearFields_B_Click(ClearFields_B,null);
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
                {
                    password_dt = new System.Data.DataTable();
                    LBI = (ListBoxItem)Ankets_LB.SelectedItem;
                    string ss = $"USE Jabroni SELECT вид_анкеты,город,улица,номер_дома,номер_корпуса,номер_квартир,колво_комнат,этаж,площадь,наличие_балконов " +
                    $"FROM Анкета INNER JOIN Квартира ON Анкета.ID_квартиры_F = Квартира.ID_квартиры_P INNER JOIN Адрес ON Квартира.ID_адреса_F = Адрес.ID_адреса_P WHERE ID_анкеты_P = { LBI.Name.Substring(1)}";
                    con.Open();
                    dec.SelectCommand = new SqlCommand(ss, con);
                    dec.SelectCommand.ExecuteNonQuery();
                    dec.Fill(password_dt);
                    for (int i = 0; i < AnkType_CB.Items.Count; i++)
                    {
                        LBI = (ListBoxItem)AnkType_CB.Items[i];
                        if (LBI.Content.ToString() == password_dt.Rows[0][0].ToString()) { AnkType_CB.SelectedItem = AnkType_CB.Items[i];break; }
                    }

                    AdrCity_TB.Text = password_dt.Rows[0][1].ToString();
                    AdrStreet_TB.Text = password_dt.Rows[0][2].ToString();
                    if (int.Parse(password_dt.Rows[0][3].ToString()) != 0) { AdrHouseNum_TB.Text = password_dt.Rows[0][3].ToString(); }
                    if (int.Parse(password_dt.Rows[0][4].ToString()) != 0) { AdrKorpNum_TB.Text = password_dt.Rows[0][4].ToString(); }
                    AdrFlatNum_TB.Text = password_dt.Rows[0][5].ToString();
                    KvRoomsCount_TB.Text = password_dt.Rows[0][6].ToString();
                    KvFloor_TB.Text = password_dt.Rows[0][7].ToString();
                    KvSquare_TB.Text = password_dt.Rows[0][8].ToString();
                    if (int.Parse(password_dt.Rows[0][9].ToString()) == 1) { KvBalcon_CHB.IsChecked = true; }
                    else { KvBalcon_CHB.IsChecked = false; }

                    CreateAnket_B.IsEnabled = false;
                    Ankets_LB.IsEnabled = false;
                    DeleteAnket_B.IsEnabled = false;
                    ChangeAnket_B.IsEnabled = false;
                    SaveAnket_B.Visibility = Visibility.Visible;
                }
            }
            return;
        }

        private void BackToMenu_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NewAnketa_F.Hide();
            MainWindow.MainWindow_F.Show();
            MainWindow.MainWindow_F.Owner = null;
            MainWindow.NewAnketa_F.Owner = MainWindow.MainWindow_F;
            MainWindow.NewAnketa_F.Close();
        }

        private void SaveAnket_B_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateAnket() == false) { return; };
            CreateAnket_B_Click(CreateAnket_B, null);
            CreateAnket_B.IsEnabled = true;
            Ankets_LB.IsEnabled = true;
            DeleteAnket_B.IsEnabled = true;
            ChangeAnket_B.IsEnabled = true;
            SaveAnket_B.Visibility = Visibility.Hidden;
            ReloadView();
        }

        private void CloseForm_B_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AnketDealler_F.ReloadView();
            this.Close();
        }
    }
}
