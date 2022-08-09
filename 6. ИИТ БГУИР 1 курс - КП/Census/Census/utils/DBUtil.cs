using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Resources;
using System.IO;
using System.Threading;

namespace Census.utils
{
    class DBUtil
    {
        private const string DATABASE_NAME = "CensusDB";
        private static SqlConnection con;
        private static SqlDataAdapter sda = new SqlDataAdapter();

        public static void Initialize()
        {
            StringBuilder connectionString = new StringBuilder("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Database=master;Integrated Security=True");
            try
            {
                con = new SqlConnection(connectionString.ToString());
                con.Open();
                con.Close();
            }
            catch
            {
                try
                {
                    connectionString.Replace("\\SQLEXPRESS", "");
                    con = new SqlConnection(connectionString.ToString());
                    con.Open();
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Не удалось установить соединение с базой данных.", "Ошибка подключения!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
            }

            if (File.Exists($"{Environment.CurrentDirectory}\\{DATABASE_NAME}.mdf") == false)
            {
                if (File.Exists($"{Environment.CurrentDirectory}\\{DATABASE_NAME}_log.ldf"))
                {
                    File.Delete($"databaseFiles/{DATABASE_NAME}_log.ldf");
                }
                StreamResourceInfo dbFileResourse = Application.GetResourceStream(new Uri($"databaseFiles/{DATABASE_NAME}.mdf", UriKind.Relative));
                StreamResourceInfo dbLogResourse = Application.GetResourceStream(new Uri($"databaseFiles/{DATABASE_NAME}_log.ldf", UriKind.Relative));
                FileStream fs1 = new FileStream($"{Environment.CurrentDirectory}/CensusDB.mdf", FileMode.Create);
                FileStream fs2 = new FileStream($"{Environment.CurrentDirectory}/CensusDB_log.ldf", FileMode.Create);
                dbFileResourse.Stream.CopyTo(fs1);
                dbLogResourse.Stream.CopyTo(fs2);
                fs1.Close();
                fs2.Close();
                dbFileResourse.Stream.Close();
                dbLogResourse.Stream.Close();
                //Thread.Sleep(1000);
                AttachCurrentDatabase();
                CensusDBUtil.RegistrateMainAdmin();//Запись в новосозданную базу данных базового администратора
            }
            else
            {
                try { AttachCurrentDatabase(); } catch { }
            }
            connectionString.Replace("master", DATABASE_NAME);

            try
            {
                con = new SqlConnection(connectionString.ToString());
                con.Open();
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                Application.Current.Shutdown();
            }
        }

        public static void Execute(string com)
        {
            con.Open();
            sda.SelectCommand = new SqlCommand(com, con);
            sda.SelectCommand.ExecuteNonQuery();
            con.Close();
        }

        public static void Execute(string com, DataTable target)
        {
            con.Open();
            sda.SelectCommand = new SqlCommand(com, con);
            sda.Fill(target);
            con.Close();
        }
        public static DataTable ExecuteReturn(string com)
        {
            DataTable result = new DataTable();
            con.Open();
            sda.SelectCommand = new SqlCommand(com, con);
            try
            {
                sda.Fill(result);
            }
            catch (SqlException) { }

            con.Close();
            return result;
        }

        public static void DetachCurrentDatabase()
        {
            Execute($"use Master ALTER DATABASE {DATABASE_NAME} set single_user with rollback immediate {Environment.NewLine} ALTER DATABASE {DATABASE_NAME} SET OFFLINE {Environment.NewLine} EXEC sp_detach_db '{DATABASE_NAME}'");
        }
        public static void AttachCurrentDatabase()
        {
            Execute($"CREATE DATABASE {DATABASE_NAME} ON PRIMARY(FILENAME='{Environment.CurrentDirectory}\\{DATABASE_NAME}.mdf'),(FILENAME ='{Environment.CurrentDirectory}\\{DATABASE_NAME}_log.ldf') FOR ATTACH");
        }

        public static object ConvertToSqlType(object p)
        {
            if(p == null || p.Equals("")) { return "NULL"; }
            if(p is Enum) { return (int)p; }
            if(p is bool) { return p.Equals(false)? 0 : 1; }
            //if (p is DateTime) { return "'" + ((DateTime)p).ToShortDateString() + "'"; }
            return "'" + p + "'";//if(p is string || p is DateTime) {  }
        }
    }
}
