using Census.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Census.model
{
    public class Administrator : ICloneable
    {
        private const int DEFAULT_SYSTEM_ADMINISTRATOR_DATABASE_ID = 1;

        private int currentId;
        private string currentAccountName;
        private string currentName;
        private string currentSurname;
        private string currentFathername;
        private string currentPassword;

        private static Administrator currentAdmin;
        private static string systemAccountName;
        private static string systemPassword;

        public int Id { get => currentId; }
        public string AccountName { get => currentAccountName; set => currentAccountName = value; }
        public string Name { get => currentName; set => currentName = value; }
        public string Surname { get => currentSurname; set => currentSurname = value; }
        public string Fathername { get => currentFathername; set => currentFathername = value; }
        public string Password { get => currentPassword; set => currentPassword = value; }

        public static string SystemAccountName { get => systemAccountName; }
        public static string SystemPassword { get => systemPassword; }
        public static Administrator СurrentAdmin { get => currentAdmin; set => currentAdmin = value; }
        public static List<Administrator> Profiles { get; set; } = new List<Administrator>();

        public bool AutorizationCheck()
        {
            DataTable dt = DBUtil.ExecuteReturn($"SELECT * FROM Administrator WHERE accountName = '{AccountName}' and accountPassword = '{Password}'");
            if (dt.Rows.Count == 0) { return false; }
            return true;
        }

        public static void FillCurrentAdminInformation()
        {
            DataTable dt = DBUtil.ExecuteReturn($"SELECT * FROM Administrator WHERE accountName = '{СurrentAdmin.AccountName}' and accountPassword = '{СurrentAdmin.Password}'");
            СurrentAdmin.currentId = dt.Rows[0].Field<int>("id_p");
            СurrentAdmin.AccountName = dt.Rows[0].Field<string>("accountName");
            СurrentAdmin.Name = dt.Rows[0].Field<string>("p_name");
            СurrentAdmin.Surname = dt.Rows[0].Field<string>("p_surname");
            СurrentAdmin.Fathername = dt.Rows[0].Field<string>("p_fathername");
            СurrentAdmin.Password = dt.Rows[0].Field<string>("accountPassword");

            if (SystemAccountName == null)
            {
                dt = DBUtil.ExecuteReturn($"SELECT * FROM Administrator WHERE id_p = {DEFAULT_SYSTEM_ADMINISTRATOR_DATABASE_ID}");
                systemAccountName = dt.Rows[0].Field<string>("accountName");
                systemPassword = dt.Rows[0].Field<string>("accountPassword");
            }
            if (Profiles.Count == 0)
            {
                GetProfiles();
            }
        }

        private static void GetProfiles()
        {
            if (СurrentAdmin.Id != 1)
            {
                Profiles.Add(СurrentAdmin);
            }
            DataTable fullMaster = DBUtil.ExecuteReturn($"SELECT * FROM Administrator WHERE id_p != {DEFAULT_SYSTEM_ADMINISTRATOR_DATABASE_ID} and id_p != {СurrentAdmin.currentId}");
            if (fullMaster.Rows.Count == 0) { return; }
            Administrator tempAdmin;
            foreach(DataRow item in fullMaster.Rows)
            {
                tempAdmin = new Administrator();
                tempAdmin.AccountName = item.Field<string>("accountName");
                tempAdmin.Name = item.Field<string>("p_name");
                tempAdmin.Surname = item.Field<string>("p_surname");
                tempAdmin.Fathername = item.Field<string>("p_fathername");
                Profiles.Add(tempAdmin);
            }
        }
        public static void UpdateCurrentAdministrator()
        {
            DBUtil.Execute($"UPDATE Administrator set accountName = '{СurrentAdmin.AccountName}', p_name = '{СurrentAdmin.Name}', p_surname = '{СurrentAdmin.Surname}', p_fathername = '{СurrentAdmin.Fathername}', accountPassword = '{СurrentAdmin.Password}' where id_p = {СurrentAdmin.Id}");
        }
        public static void DeleteCurrentAdministrator()
        {
            DBUtil.Execute($"DELETE FROM Administrator WHERE id_p = {СurrentAdmin.currentId}");
            Profiles.Remove(СurrentAdmin);
            СurrentAdmin = null;
        }
        public static void AddNewAdministrator(Administrator administrator)
        {
            DBUtil.Execute($"INSERT INTO Administrator (accountName,p_name,p_surname,p_fathername,accountPassword) VALUES ('{administrator.AccountName}','{administrator.Name}','{administrator.Surname}','{administrator.Fathername}','{administrator.Password}')");
            Profiles.Add(administrator);
            currentAdmin = null;
            currentAdmin = administrator;
        }
        public object Clone()
        {
            Administrator result = new Administrator();
            result.AccountName = AccountName;
            result.Password = Password;
            result.Name = Name;
            result.Surname = Surname;
            result.Fathername = Fathername;
            result.currentId = Id;
            return result;
        }
    }
}
