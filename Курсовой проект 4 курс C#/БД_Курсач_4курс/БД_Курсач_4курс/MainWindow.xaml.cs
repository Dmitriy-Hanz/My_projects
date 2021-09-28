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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace БД_Курсач_4курс
{
    public partial class MainWindow : Window
    {
        public static MainWindow MainWindow_F;
        public static LoginForm Login_F;
        public static NewAnketaForm NewAnketa_F;
        public static AnketStorageForm AnketStorage_F;
        public static AnketDeallerForm AnketDealler_F;
        public static ManualSystem_F ManualSystemForm;
        public static string PASSWORD;

        SqlCommand SqlCom;
        public MainWindow()
        {
            InitializeComponent();
            MainWindow_F = this;
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
                { con.Open(); SqlCom = new SqlCommand("USE Jabroni", con); SqlCom.ExecuteNonQuery(); }
            }
            catch (Exception)
            {
                SqlDataAdapter dicks = new SqlDataAdapter();
                
                StreamReader script = new StreamReader("Создание_БД.txt", Encoding.GetEncoding(1251));
                string BDstr = script.ReadToEnd();
                script = new StreamReader("Создание_процедур.txt", Encoding.GetEncoding(1251));
                string PROCstr = script.ReadToEnd();
                script.Close();
                script.Dispose();
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
                {
                    con.Open();
                    SqlCom = new SqlCommand("CREATE DATABASE Jabroni", con);
                    SqlCom.ExecuteNonQuery();
                    SqlCom = new SqlCommand(BDstr, con);
                    SqlCom.ExecuteNonQuery();
                    Procedures.CreateProcs(SqlCom, con);
                }
            }
            Login_F = new LoginForm();
            MainWindow_F.Hide();
            Login_F.Show();
            MainWindow_F.Owner = Login_F;
        }

        private void NewAnketa_B_Click(object sender, RoutedEventArgs e)
        {
            NewAnketa_F = new NewAnketaForm();
            MainWindow_F.Hide();
            NewAnketa_F.Show();
            MainWindow_F.Owner = NewAnketa_F;
        }
        private void AnketList_B_Click(object sender, RoutedEventArgs e)
        {
            AnketStorage_F = new AnketStorageForm();
            MainWindow_F.Hide();
            AnketStorage_F.Show();
            AnketStorage_F.ResizeColumns();
            MainWindow_F.Owner = AnketStorage_F;
        }
        private void AnketSearch_B_Click(object sender, RoutedEventArgs e)
        {
            AnketDealler_F = new AnketDeallerForm();
            MainWindow_F.Hide();
            AnketDealler_F.Show();
            AnketDealler_F.ResizeColumns();
            MainWindow_F.Owner = AnketDealler_F;
        }

        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Spravka_B_Click(object sender, RoutedEventArgs e)
        {
            SqlDataAdapter dec = new SqlDataAdapter();
            System.Data.DataTable fullMaster = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand("USE Jabroni exec SELECTАдресID_P 19", con);
                dec.SelectCommand.ExecuteNonQuery();
                dec.Fill(fullMaster);
            }
            ManualSystemForm = new ManualSystem_F();
            ManualSystemForm.ShowDialog();
        }
    }
}
