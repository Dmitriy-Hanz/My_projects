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
    public partial class LoginForm : Window
    {
        System.Data.DataTable password_dt;
        SqlDataAdapter dec;
        public static RegForm Reg_F;
        public LoginForm()
        {
            InitializeComponent();
            dec = new SqlDataAdapter();
        }

        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Reg_B_Click(object sender, RoutedEventArgs e)
        {
            Reg_F = new RegForm();
            Reg_F.ShowDialog();
        }

        private void Login_B_Click(object sender, RoutedEventArgs e)
        {
            password_dt = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand("USE Jabroni SELECT Пароль FROM Клиент", con);
                dec.SelectCommand.ExecuteNonQuery(); dec.Fill(password_dt);
            }
            ErrorString_L.Content = "";
            if (Password_TB.Text == "") { ErrorString_L.Content += "Поле ввода пароля не должно быть пустым\n"; return; }
            if (Password_TB.Text.Length < 8 || Password_TB.Text.Length > 15) { ErrorString_L.Content += "Пароль не должнен быть корече 8 знаков или больше 15\n"; return; }
            foreach (System.Data.DataRow i in password_dt.Rows)
            {
                if (i[0].ToString() == Password_TB.Text)
                {
                    MainWindow.PASSWORD = Password_TB.Text;
                    MainWindow.Login_F.Hide();
                    MainWindow.MainWindow_F.Show();
                    MainWindow.MainWindow_F.Owner = null;
                    MainWindow.Login_F.Owner = MainWindow.MainWindow_F;
                    MainWindow.Login_F.Close();
                    return;
                }
            }
            ErrorString_L.Content = "";
            string mes = "Отсутствует учетная запись по данному паролю. Если вы тут впервые, зарегестрируйтесь.\n\nЖелаете пройти процедуру регистрации?";
            MessageBoxResult ms_res = MessageBox.Show(mes, "Предупреждение", MessageBoxButton.YesNo);
            if (ms_res == MessageBoxResult.Yes) { Reg_F = new RegForm(); Reg_F.ShowDialog(); }
        }
    }
}
