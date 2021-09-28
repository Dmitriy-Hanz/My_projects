using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace KAPEYAP_KURS
{
    public class Worker
    {
        private string name;
        private string surname;
        private string fathername;
        private int id;
        private double oklad;
        private double premiya;
        private double nadbavka;
        private double doplata;
        private double podNalog;
        private double sumZP;
        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Fathername { get { return fathername; } set { fathername = value; } }
        public int Id { get { return id; } set { id = value; } }
        public double Oklad { get { return oklad; } set { oklad = value; } }
        public double Premiya { get { return premiya; } set { premiya = value; } }
        public double Nadbavka { get { return nadbavka; } set { nadbavka = value; } }
        public double Doplata { get { return doplata; } set { doplata = value; } }
        public double PodNalog { get { return podNalog; } set { podNalog = value; } }
        public double SumZP { get { return sumZP; } set { sumZP = value; } }
        public Worker() { }

        public Worker(string name, string surname, string fathername, int id, double oklad, double premiya, double nadbavka, double doplata)
        {
            Name = name;
            Surname = surname;
            Fathername = fathername;
            Id = id;
            Oklad = oklad;
            Premiya = premiya;
            Nadbavka = nadbavka;
            Doplata = doplata;
        }
        public Worker(string name, string surname, string fathername, int id)
        {
            Name = name;
            Surname = surname;
            Fathername = fathername;
            Id = id;
        }
        public Worker(object[] mas)
        {
            Name = (string)mas[0];
            Surname = (string)mas[1];
            Fathername = (string)mas[2];
            Id = (int)mas[3];
            SumZP = (double)mas[4];
        }
    }
    public class DBSelector
    {
        public static DataTable Select(string tableName, string selectSQL)// функция подключения к базе данных и обработка запросов
        {
            DataTable gamestable = new DataTable();// создаём таблицу в приложении                                                                  // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection(MainMenuWin.connectionString);
            sqlConnection.Open();// открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();// создаём команду
            sqlCommand.CommandText = selectSQL;// присваиваем команде текст
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);// создаём обработчик
            adapter.Fill(gamestable);// возращаем таблицу с результатом
            return gamestable;
        }
        public static void UpdateDB(DataTable tb)
        {
            SqlConnection sqlConnection = new SqlConnection(MainMenuWin.connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = MainMenuWin.connectionString;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            //adapter.UpdateCommand = sqlCommand;
            //adapter.Fill(tb);
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(MainMenuWin.adapter);
            adapter.Update(tb);
        }
    }
}
