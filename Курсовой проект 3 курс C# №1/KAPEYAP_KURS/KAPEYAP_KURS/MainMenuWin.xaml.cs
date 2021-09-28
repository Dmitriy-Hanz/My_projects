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
using System.Data;

namespace KAPEYAP_KURS
{
    public partial class MainMenuWin : Window
    {
        public static MainMenuWin MAIN;
        public static CalcWin1 CalcWindow;
        public static Spravka_F FAQWin;

        public static string connectionString;
        public static SqlDataAdapter adapter;
        public static DataTable workerstable;
        public static DataTable workerstableFULL;

        public MainMenuWin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (new SqlConnection(connectionString))
            {
                string z = "SELECT * FROM [dbo].[ZPDataList]";
                //string z = "SELECT [dbo].[WorkersList].[ID], dbo.WorkersList.Имя, dbo.WorkersList.Фамилия, dbo.WorkersList.Отчество, dbo.WorkersList.Должность,dbo.WorkersList.[Дата принятия] ,[dbo].ZPDataList.Начислено FROM dbo.WorkersList INNER JOIN dbo.ZPDataList ON dbo.WorkersList.ID = dbo.ZPDataList.ID";
                workerstable = DBSelector.Select("WorkersList", "SELECT * FROM [dbo].[WorkersList]");
                workerstableFULL = DBSelector.Select("WorkersList", z);
            }
            MAIN = this;
            CalcWindow = new CalcWin1();
        }

        private void PersonCalcB_Click(object sender, RoutedEventArgs e)
        {
            MAIN.Hide();
            CalcWindow.Show();
        }
        private void FAQB_Click(object sender, RoutedEventArgs e)
        {
            MAIN.Hide();
            FAQWin = new Spravka_F();
            FAQWin.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
