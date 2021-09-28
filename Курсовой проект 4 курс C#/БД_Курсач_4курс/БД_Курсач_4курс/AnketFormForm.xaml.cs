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
    public partial class AnketFormForm : Window
    {
        public AnketFormForm()
        {
            InitializeComponent();
            ChangeAnket();
        }
        private void ChangeAnket()
        {
            SqlDataAdapter dec = new SqlDataAdapter();
            System.Data.DataTable SemiFullmaster = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                System.Data.DataRowView drv = (System.Data.DataRowView)MainWindow.AnketStorage_F.Ankets_DG.SelectedItem;
                string ss = $"USE Jabroni SELECT вид_анкеты,город,улица,номер_дома,номер_корпуса,номер_квартир,колво_комнат,этаж,площадь,наличие_балконов " +
                $"FROM Анкета INNER JOIN Квартира ON ID_квартиры_F = ID_квартиры_P INNER JOIN Адрес ON ID_адреса_F = ID_адреса_P WHERE ID_анкеты_P = {drv.Row[3]}";

                dec.SelectCommand = new SqlCommand(ss, con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(SemiFullmaster);
                AdrHouseNum_L.Content = SemiFullmaster.Rows[0][3].ToString();
            }

            AdrHouseNum_L.Content = "";
            AdrKorp_L.Content = "";
            AnkType_L.Content = SemiFullmaster.Rows[0][0].ToString();
            AdrCity_L.Content = SemiFullmaster.Rows[0][1].ToString();
            AdrStreet_L.Content = SemiFullmaster.Rows[0][2].ToString();
            if (int.Parse(SemiFullmaster.Rows[0][3].ToString()) != 0) { AdrHouseNum_L.Content = SemiFullmaster.Rows[0][3].ToString(); }
            if (int.Parse(SemiFullmaster.Rows[0][4].ToString()) != 0) { AdrKorp_L.Content = SemiFullmaster.Rows[0][4].ToString(); }
            AdrFlatNum_L.Content = SemiFullmaster.Rows[0][5].ToString();
            KvRoomCount_L.Content = SemiFullmaster.Rows[0][6].ToString();
            KvFloor_L.Content = SemiFullmaster.Rows[0][7].ToString();
            KvSquare_L.Content = SemiFullmaster.Rows[0][8].ToString();
            if (int.Parse(SemiFullmaster.Rows[0][9].ToString()) == 1) { KvBalcon_L.Content = "Есть"; }
            else { KvBalcon_L.Content = "Нет"; }
        }

        private void CloseForm_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
