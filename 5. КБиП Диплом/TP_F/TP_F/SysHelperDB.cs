using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Windows;

//< supportedRuntime version = "v4.0" sku = ".NETFramework,Version=v4.6.1"/>

//строка для работы с файлом базы данных:
//"Data Source=.\\SQLEXPRESS;AttachDbFilename=D:\\яПРАКТОСЫ\\ТП\\EXE\\SysHelper.mdf;Initial Catalog=SysHelper;Integrated Security=True"
//"Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\User\\source\\repos\\Технический_проект_F\\Технический_проект_F\\bin\\Debug\\SysHelper.mdf;Initial Catalog=SysHelper;Integrated Security=True"
//строка для работы с базой данных в SQLServer:
//"Data Source =.\\SQLEXPRESS;Database=SysHelper;Integrated Security = True"

namespace TP_F
{
    public class SysHelperDB
    {
        private static SqlConnection con;
        private static SqlDataAdapter dec = new SqlDataAdapter();
        DataTable fullMaster;//используется в функции ReadAll
        string commandTemp;
        public List<Kabinet> kabinets { get; set; }
        public Inventory Inventory { get; set; }
        
        public SysHelperDB()
        {
            kabinets = new List<Kabinet>();
            Inventory = new Inventory();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

                //connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ElectroWorld;Integrated Security=True";
                //connectionStringsSection.ConnectionStrings["ElectroWorld.Properties.Settings.ElectroWorldConnectionString"].ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ElectroWorld;Integrated Security=True";
                //config.Save();
                //ConfigurationManager.RefreshSection("connectionStrings");

            string conStr = "Data Source=.;Database=Master;Integrated Security=True";
            try
            {
                using (con = new SqlConnection(conStr))
                { con.Open(); }
                connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString = "Data Source=.;Initial Catalog=SysHelper;Integrated Security=True";
            }
            catch
            {
                try
                {
                    conStr = "Data Source=.\\SQLEXPRESS;Database=Master;Integrated Security=True";
                    using (con = new SqlConnection(conStr))
                    { con.Open(); }
                    connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=SysHelper;Integrated Security=True";
                }
                catch
                {
                    MessageBox.Show("Не удалось установить соединение с базой данных.", "ВНИМАНИЕ!",MessageBoxButton.OK,MessageBoxImage.Error);
                    App.Current.Shutdown();
                }
            }
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            string[] ass = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.Split(';');

            try
            {
                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                { con.Open(); }//exception class 20 - борохлит подключение к серверу
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.IO.File.Delete($"{Environment.CurrentDirectory}\\SysHelper_log.ldf");
                using (con = new SqlConnection($"{ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.Split(';')[0]};Database=Master;Integrated Security = True"))
                {
                    con.Open();
                    dec.SelectCommand = new SqlCommand($"CREATE DATABASE SysHelper ON PRIMARY(FILENAME='{Environment.CurrentDirectory}\\SysHelper.mdf') FOR ATTACH", con);
                    dec.SelectCommand.ExecuteNonQuery();
                }
            }

            try
            {
                using (con = new SqlConnection("Data Source =.\\SQLEXPRESS;Database=SysHelper;Integrated Security = True"))
                { con.Open(); }
            }
            catch (Exception)
            {
                return;
            }
        }
        public void ReadAll()
        {
            //con.ConnectionString = "Data Source =.\\SQLEXPRESS;Database=SysHelper;Integrated Security = True";
            fullMaster = new DataTable();
            DataTable oberSturmBanMaster;

            // создание кабинетов
            ExecCom("SELECT * FROM Кабинет", fullMaster);
            for (int i = 0; i < fullMaster.Rows.Count; i++)
            {
                kabinets.Add(new Kabinet(fullMaster.Rows[i][0].ToString(), (int)fullMaster.Rows[i][1], (string)fullMaster.Rows[i][2]));
            }

            // создание рабочих мест
            for (int i = 0; i < kabinets.Count; i++)
            {
                oberSturmBanMaster = new DataTable();
                ExecCom($"SELECT * FROM РабочееМесто WHERE ID_Кабинета_F = {kabinets[i].kabinetID}", oberSturmBanMaster);
                for (int j = 0; j < oberSturmBanMaster.Rows.Count; j++)
                {
                    kabinets[i].workPlaces.Add(new WorkPlace(oberSturmBanMaster.Rows[j]));
                }
                kabinets[i].workPlaceCount = kabinets[i].workPlaces.Count;
            }

            // создание мониторов
            List<Monitor> monitorsTemp = new List<Monitor>();
            oberSturmBanMaster = new DataTable();
            ExecCom($"SELECT * from Монитор", oberSturmBanMaster);
            for (int i = 0; i < oberSturmBanMaster.Rows.Count; i++)
            {
                monitorsTemp.Add(new Monitor(oberSturmBanMaster.Rows[i]));
            }

            //создание компьютеров
            List<Komp> kompsTemp = new List<Komp>();
            fullMaster = new DataTable();
            ExecCom($"SELECT * FROM Компьютер", fullMaster);
            for (int i = 0; i < fullMaster.Rows.Count; i++)
            {
                kompsTemp.Add(new Komp(fullMaster.Rows[i]));
            }

            // создание процессоров
            for (int i = 0; i < kompsTemp.Count; i++)
            {
                commandTemp = "SELECT ЦПУ.* FROM СписокЦПУ JOIN ЦПУ ON ID_ЦПУ_F = ID_Процессора_P";

                fullMaster = new DataTable();
                ExecCom($"{commandTemp} WHERE ID_Компьютера_F = {kompsTemp[i].kompID}", fullMaster);
                if (fullMaster.Rows.Count != 0)
                { kompsTemp[i].cpu = new CPU(fullMaster.Rows[0]); }
            }

            // создание графических адаптеров
            for (int i = 0; i < kompsTemp.Count; i++)
            {
                commandTemp = "SELECT ГрафическийАдаптер.* FROM СписокГрафАдаптеров JOIN ГрафическийАдаптер ON ID_графАдаптера_F = ID_графАдаптера_P";

                fullMaster = new DataTable();
                ExecCom($"{commandTemp} WHERE ID_Компьютера_F = {kompsTemp[i].kompID}", fullMaster);
                for (int k = 0; k < fullMaster.Rows.Count; k++)
                {
                    kompsTemp[i].videoAdapters.Add(new VideoAdapter(fullMaster.Rows[k]));
                }

            }

            // создание ОС
            for (int i = 0; i < kompsTemp.Count; i++)
            {
                commandTemp = "SELECT ОперационнаяСистема.* FROM СписокОС JOIN ОперационнаяСистема ON ID_ОС_F = ID_ОС_P";

                fullMaster = new DataTable();
                ExecCom($"{commandTemp} WHERE ID_Компьютера_F = {kompsTemp[i].kompID}", fullMaster);
                for (int k = 0; k < fullMaster.Rows.Count; k++)
                {
                    kompsTemp[i].systems.Add(new OS(fullMaster.Rows[k]));
                }
            }

            // создание дисков
            for (int i = 0; i < kompsTemp.Count; i++)
            {
                commandTemp = "SELECT Диск.* FROM СписокДисков JOIN Диск ON ID_Диска_F = ID_Диска_P";

                fullMaster = new DataTable();
                ExecCom($"{commandTemp} WHERE ID_Компьютера_F = {kompsTemp[i].kompID}", fullMaster);
                for (int k = 0; k < fullMaster.Rows.Count; k++)
                {
                    kompsTemp[i].disks.Add(new Disk(fullMaster.Rows[k]));
                }
            }

            //распределение
            for (int i = 0; i < kabinets.Count; i++)
            {
                for (int j = 0; j < kabinets[i].workPlaces.Count; j++)
                {
                    if (kabinets[i].workPlaces[j].kompukter != null)
                    {
                        kabinets[i].workPlaces[j].kompukter = FindKomp(kompsTemp, kabinets[i].workPlaces[j].kompukter.kompID);
                        kompsTemp.Remove(FindKomp(kompsTemp, kabinets[i].workPlaces[j].kompukter.kompID));
                    }
                    if (kabinets[i].workPlaces[j].monitor != null)
                    {
                        kabinets[i].workPlaces[j].monitor = FindMonitor(monitorsTemp, kabinets[i].workPlaces[j].monitor.monitorID);
                        monitorsTemp.Remove(FindMonitor(monitorsTemp, kabinets[i].workPlaces[j].monitor.monitorID));
                    }
                }
            }

            //заполнение инвентаря
            foreach (var item in kompsTemp)
            {
                Inventory.Facilities.Add(item);
            }
            foreach (var item in monitorsTemp)
            {
                Inventory.Facilities.Add(item);
            }
            kompsTemp.Clear();
            monitorsTemp.Clear();
        }

        public static void ExecCom(string com)
        {
            using (con = new SqlConnection("Data Source =.\\SQLEXPRESS;Database=SysHelper;Integrated Security = True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand(com, con);
                dec.SelectCommand.ExecuteNonQuery();
            }
        }
        public static void ExecCom(string com,DataTable filler)
        {
            using (con = new SqlConnection("Data Source =.\\SQLEXPRESS;Database=SysHelper;Integrated Security = True"))
            {
                con.Open();
                dec.SelectCommand = new SqlCommand(com, con);
                dec.Fill(filler);
            }
        }

        public Kabinet FindKabinet(string index)
        {
            return kabinets.Find(new Predicate<Kabinet>(delegate (Kabinet kab) { return kab.kabinetID == index; }));
        }
        public WorkPlace FindWorkPlace(int id)
        {
            WorkPlace temp;
            foreach (var item in kabinets)
            {
                temp = item.workPlaces.Find(new Predicate<WorkPlace>(delegate (WorkPlace wp) { return wp.workPlaceID == id; }));
                if (temp != null) { return temp; }
            }
            return null;
        }
        public WorkPlace FindWorkPlace(int id, Kabinet source)
        {
            return source.workPlaces.Find(new Predicate<WorkPlace>(delegate (WorkPlace wp) { return wp.workPlaceID == id; }));
        }
        public Facility FindFacility(string index,string fType)
        {
            return Inventory.Facilities.Find(new Predicate<Facility>(delegate (Facility fc) { return fc.uID == int.Parse(index) & fc.fType == fType; }));
        }

        public Komp FindKomp(List<Komp> list, int index)
        {
            return list.Find(new Predicate<Komp>(delegate (Komp komp) { return komp.uID == index; }));
        }
        public Komp FindKomp(int index)
        {
            foreach(Kabinet item1 in kabinets)
            {
                foreach(WorkPlace item2 in item1.workPlaces)
                {
                    try
                    { if (item2.kompukter.kompID == index) { return item2.kompukter; } }
                    catch (Exception) { }
                }
            }
            return null;
        }
        public Monitor FindMonitor(List<Monitor> list, int index)
        {
            return list.Find(new Predicate<Monitor>(delegate (Monitor mon) { return mon.uID == index; }));
        }
        public Monitor FindMonitor(int index)
        {
            foreach (Kabinet item1 in kabinets)
            {
                foreach (WorkPlace item2 in item1.workPlaces)
                {
                    try
                    { if (item2.monitor.monitorID == index) { return item2.monitor; } }
                    catch (Exception) { }
                }
            }
            return null;
        }

        public void DeleteKomputer(int id)
        {
            fullMaster = new DataTable();
            ExecCom($"SELECT ID_РабМеста_P,ID_Кабинета_P FROM РабочееМесто JOIN Кабинет ON ID_Кабинета_P = ID_Кабинета_F WHERE ID_компьютера_F = {id}", fullMaster);
            if(Inventory.Facilities.Remove((Komp)FindFacility(id.ToString(), "K")) == false)
            {
                FindWorkPlace(int.Parse(fullMaster.Rows[0][0].ToString()), FindKabinet(fullMaster.Rows[0][1].ToString())).kompukter = null;
                ExecCom($"UPDATE РабочееМесто SET ID_компьютера_F = NULL WHERE ID_компьютера_F = {id}");
            }
            ExecCom($"EXEC dbo.DeletingKomp {id}");
        }
        public void DeleteMonitor(int id)
        {
            fullMaster = new DataTable();
            ExecCom($"SELECT ID_РабМеста_P,ID_Кабинета_P FROM РабочееМесто JOIN Кабинет ON ID_Кабинета_P = ID_Кабинета_F WHERE ID_монитора_F = {id}", fullMaster);
            if (Inventory.Facilities.Remove((Monitor)FindFacility(id.ToString(), "M")) == false)
            {
                FindWorkPlace(int.Parse(fullMaster.Rows[0][0].ToString()), FindKabinet(fullMaster.Rows[0][1].ToString())).monitor = null;
                ExecCom($"UPDATE РабочееМесто SET ID_монитора_F = NULL WHERE ID_монитора_F = {id}");
            }
            ExecCom($"DELETE FROM Монитор WHERE ID_монитора_P = {id}");
        }

        public void WhenExit(object sender, ExitEventArgs e)
        {
            ExecCom($"ALTER DATABASE SysHelper set single_user with rollback immediate {Environment.NewLine} ALTER DATABASE SysHelper SET OFFLINE {Environment.NewLine} EXEC sp_detach_db 'SysHelper'");
        }
    }
}