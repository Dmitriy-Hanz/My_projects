using Census.infrastructure.commands.Base;
using Census.view;
using Census.viewModel.Base;
using Census.utils;
using Census.model;
using System.Windows;
using System.Windows.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Windows.Data;
using System.ComponentModel;
using System.Threading;

namespace Census.viewModel
{
    public class DataGridRowModel
    {
        public DataGridRowModel(DataRow sourse)
        {
            HouseholdId = sourse[0].ToString();
            PersonId = sourse[1].ToString();
            Name = sourse[2].ToString();
            Surname = sourse[3].ToString();
            Fathername = sourse[4].ToString();
            BirthdayDate = ((DateTime)sourse[5]).ToShortDateString();
            Age = sourse[6].ToString();
            Gender = sourse[7].Equals(2) ? "жен" : "муж";
        }

        public string HouseholdId { get; set; }
        public string PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fathername { get; set; }
        public string BirthdayDate { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
    }


    class DataBaseVM : ViewModelBase
    {
        #region СТАТИЧЕСКИЙ КЛАСС-ПЕРЕДАТЧИК
        public static class Transferer
        {
            private static int personId;
            private static int householdId;
            private static Dictionary<int, Person> personBuffer;
            private static Dictionary<int, Household> householdBuffer;

            public static int PersonId { get => personId; }
            public static int HouseholdId { get => householdId; }
            public static Dictionary<int, Person> PersonBuffer { get => personBuffer; }
            public static Dictionary<int, Household> HouseholdBuffer { get => householdBuffer; }

            public static void Write(int personId, int householdId, Dictionary<int, Person> personBuffer, Dictionary<int, Household> householdBuffer)
            {
                Transferer.personId = personId;
                Transferer.householdId = householdId;
                Transferer.personBuffer = personBuffer;
                Transferer.householdBuffer = householdBuffer;
            }
        }
        #endregion
        #region СВОЙСТВА
        private DataGridRowModel dataGridSelectedRow; public DataGridRowModel DataGridSelectedRow
        {
            get => dataGridSelectedRow;
            set => Set(ref dataGridSelectedRow, value);
        }


        private string nameForSort = ""; public string NameForSort
        {
            get => nameForSort;
            set => Set(ref nameForSort, value);
        }
        private string surnameForSort = ""; public string SurnameForSort
        {
            get => surnameForSort;
            set => Set(ref surnameForSort, value);
        }
        private string fathernameForSort = ""; public string FathernameForSort
        {
            get => fathernameForSort;
            set => Set(ref fathernameForSort, value);
        }
        private string birthdayDateForSort = ""; public string BirthdayDateForSort
        {
            get => birthdayDateForSort;
            set => Set(ref birthdayDateForSort, value);
        }
        private string ageForSort = ""; public string AgeForSort
        {
            get => ageForSort;
            set => Set(ref ageForSort, value);
        }
        private string gender = ""; public string Gender
        {
            get => gender;
            set => Set(ref gender, value);
        }


        private int birthdayDateCompareIndex; public int BirthdayDateCompareIndex
        {
            get => birthdayDateCompareIndex;
            set => Set(ref birthdayDateCompareIndex, value);
        }
        private int ageCompareIndex; public int AgeCompareIndex
        {
            get => ageCompareIndex;
            set => Set(ref ageCompareIndex, value);
        }
        private int genderSelectedIndex; public int GenderSelectedIndex
        {
            get => genderSelectedIndex;
            set => Set(ref genderSelectedIndex, value);
        }

        #endregion
        #region КОМАНДЫ


        public ICommand BackToMainMenuCommand { get; }
        private void OnBackToMainMenuCommandExecuted(object p)
        {
            TypicalActions.GoToWindow<DataBaseWindow, MainMenuWindow>();
        }
        private bool CanBackToMainMenuCommandExecute(object p) => true;


        public ICommand LoadRandomBlanksGeneratorWindowCommand { get; }
        private void OnLoadRandomBlanksGeneratorWindowCommandExecuted(object p)
        {
            RandomBlanksGeneratorWindow randomBlanksGeneratorW = new();
            randomBlanksGeneratorW.ShowDialog();
            SelectFromDBToDataGrid();
        }
        private bool CanLoadRandomBlanksGeneratorWindowCommandExecute(object p) => true;


        public ICommand ClearDatabaseCommand { get; }
        private void OnClearDatabaseCommandExecuted(object p)
        {
            if (MessageBox.Show("Все записи в базе данных будут удалены. Вы уверены?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CensusDBUtil.ClearDatabase();
                BasicDataGridRows.Clear();
                DataGridRows = new List<DataGridRowModel>();
                PersonBuffer = new Dictionary<int, Person>();
                HouseholdBuffer = new Dictionary<int, Household>();
            }
        }
        private bool CanClearDatabaseCommandExecute(object p) => true;


        public ICommand ShowFullBlankCommand { get; }
        private void OnShowFullBlankCommandExecuted(object p)
        {
            Transferer.Write(int.Parse(DataGridSelectedRow.PersonId), int.Parse(DataGridSelectedRow.HouseholdId), PersonBuffer, HouseholdBuffer);
            BlankViewFormWindow blankViewFormW = new BlankViewFormWindow();
            blankViewFormW.ShowDialog();
        }
        private bool CanShowFullBlankCommandExecute(object p) => true;


        public ICommand SearchCommand { get; }
        private void OnSearchCommandExecuted(object p)
        {
            if (!BirthdayDateForSort.Equals("") && DateTime.TryParse(BirthdayDateForSort, out DateTime ass) == false)
            {
                MessageBox.Show("Введенная дата имеет неверный формат", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            if (!AgeForSort.Equals("") && int.TryParse(AgeForSort, out int ass2) == false)
            {
                MessageBox.Show("Число полных лет должно быть числом/цифрой", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            Sort();
        }
        private bool CanSearchCommandExecute(object p) => true;


        public ICommand UpdateDataGridCommand { get; }
        private void OnUpdateDataGridCommandExecuted(object p)
        {
            if (DataGridRows.Count == BasicDataGridRows.Count) { return; }
            DataGridRows = new List<DataGridRowModel>(BasicDataGridRows);
        }
        private bool CanUpdateDataGridCommandExecute(object p) => true;


        #endregion
        #region МОДЕЛЬ

        public Dictionary<int, Person> PersonBuffer { get; set; }
        public Dictionary<int, Household> HouseholdBuffer { get; set; }
        public List<DataGridRowModel> BasicDataGridRows { get; set; }
        public List<DataGridRowModel> dataGridRows; public List<DataGridRowModel> DataGridRows
        {
            get => dataGridRows;
            set => Set(ref dataGridRows, value);
        }

        #endregion
        public DataBaseVM()
        {
            BackToMainMenuCommand = new LambdaCommand(OnBackToMainMenuCommandExecuted, CanBackToMainMenuCommandExecute);
            LoadRandomBlanksGeneratorWindowCommand = new LambdaCommand(OnLoadRandomBlanksGeneratorWindowCommandExecuted, CanLoadRandomBlanksGeneratorWindowCommandExecute);
            ClearDatabaseCommand = new LambdaCommand(OnClearDatabaseCommandExecuted, CanClearDatabaseCommandExecute);
            ShowFullBlankCommand = new LambdaCommand(OnShowFullBlankCommandExecuted, CanShowFullBlankCommandExecute);
            SearchCommand = new LambdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);
            UpdateDataGridCommand = new LambdaCommand(OnUpdateDataGridCommandExecuted, CanUpdateDataGridCommandExecute);

            PersonBuffer = new Dictionary<int, Person>();
            HouseholdBuffer = new Dictionary<int, Household>();
            BasicDataGridRows = new List<DataGridRowModel>();
            DataGridRows = new List<DataGridRowModel>();

            SelectFromDBToDataGrid();
        }
        private void SelectFromDBToDataGrid()
        {
            DataTable fullMaster = new DataTable();
            fullMaster = DBUtil.ExecuteReturn("SELECT id_Household_f, id_p, p_name, p_surname, p_fathername, birthdayDate, age, gender FROM Person");
            foreach (DataRow item in fullMaster.Rows)
            {
                try { BasicDataGridRows.Add(new DataGridRowModel(item)); }
                catch (Exception e) { MessageBox.Show(e.StackTrace); }
            }
            DataGridRows = new List<DataGridRowModel>(BasicDataGridRows);
        }
        private void Sort()
        {
            var filter = new Predicate<object>(obj =>
            {
                DataGridRowModel temp = obj as DataGridRowModel;
                if (temp.Name.Contains(NameForSort) == false) { return false; }
                if (temp.Surname.Contains(NameForSort) == false) { return false; }
                if (temp.Fathername.Contains(FathernameForSort) == false) { return false; }
                if (!BirthdayDateForSort.Equals(""))
                {
                    switch (BirthdayDateCompareIndex)
                    {
                        case 0:
                            if (DateTime.Parse(temp.BirthdayDate).CompareTo(DateTime.Parse(BirthdayDateForSort)) != 0) { return false; }
                            break;
                        case 1:
                            if (DateTime.Parse(temp.BirthdayDate).CompareTo(DateTime.Parse(BirthdayDateForSort)) != 1) { return false; }
                            break;
                        case 2:
                            if (DateTime.Parse(temp.BirthdayDate).CompareTo(DateTime.Parse(BirthdayDateForSort)) != -1) { return false; }
                            break;
                    }
                }
                if (!AgeForSort.Equals(""))
                {
                    switch (AgeCompareIndex)
                    {
                        case 0:
                            if (int.Parse(temp.Age).CompareTo(int.Parse(AgeForSort)) != 0) { return false; }
                            break;
                        case 1:
                            if (int.Parse(temp.Age).CompareTo(int.Parse(AgeForSort)) != 1) { return false; }
                            break;
                        case 2:
                            if (int.Parse(temp.Age).CompareTo(int.Parse(AgeForSort)) != -1) { return false; }
                            break;
                    }
                }
                switch (GenderSelectedIndex)
                {
                    case 1:
                        if (temp.Gender.Equals("жен")) { return false; }
                        break;
                    case 2:
                        if (temp.Gender.Equals("муж")) { return false; }
                        break;
                }
                return true;
            });
            List<DataGridRowModel> temp = BasicDataGridRows.FindAll(filter);
            DataGridRows = null;
            Thread.Sleep(100);
            DataGridRows = new List<DataGridRowModel>(BasicDataGridRows.FindAll(filter));
        }

    }
}
