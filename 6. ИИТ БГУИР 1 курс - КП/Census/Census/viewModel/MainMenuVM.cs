using Census.viewModel.Base;
using Census.utils;
using Census.view;
using Census.infrastructure.commands.Base;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Census.model;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;

namespace Census.viewModel
{
    class MainMenuVM : ViewModelBase
    {
        #region КОМАНДЫ
        #region Команды кнопОчек для перехода по окнам и выхода

        public ICommand LoadCensusResultsWindowCommand { get; }
        private void OnLoadCensusResultsWindowCommandExecuted(object p)
        {
            if(DBUtil.ExecuteReturn("use CensusDB SELECT TOP(1) * FROM Person").Rows.Count == 0)
            {
                MessageBox.Show("В базе данных нет ни одной анкеты");
                return;
            }
            TypicalActions.GoToWindow<MainMenuWindow, CensusResultsWindow>();
        }
        private bool CanLoadCensusResultsWindowCommandExecute(object p) => true;


        public ICommand LoadDataBaseWindowCommand { get; }
        private void OnLoadDataBaseWindowCommandExecuted(object p)
        {
            TypicalActions.GoToWindow<MainMenuWindow, DataBaseWindow>();
        }
        private bool CanLoadDataBaseWindowCommandExecute(object p) => true;


        public ICommand ShutdownAppCommand { get; }
        private void OnShutdownAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanShutdownAppCommandExecute(object p) => true;

        #endregion
        #region Команды для работы с профилем

        public ICommand ShowHideProfileCommand { get; }
        private void OnShowHideProfileCommandExecuted(object p)
        {
            if (ProfileHeightSize == 51)
            {
                ProfileHeightSize = Double.NaN;
                return;
            }
            ProfileHeightSize = 51;
        }
        private bool CanShowHideProfileCommandExecute(object p) => true;

        public ICommand DeleteProfileCommand { get; }
        private void OnDeleteProfileCommandExecuted(object p)
        {
            if (MessageBox.Show("Профиль будет удален. Вы уверены?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                Administrator.DeleteCurrentAdministrator();
                TypicalActions.GoToWindow<MainMenuWindow, AuthorizationWindow>();
            }
        }
        private bool CanDeleteProfileCommandExecute(object p) => true;

        public ICommand CreateNewProfileCommand { get; }
        private void OnCreateNewProfileCommandExecuted(object p)
        {
            CreateEditProfileVM.Mode = true;
            CreateEditProfileWindow createEditProfileWindow = new();
            createEditProfileWindow.ShowDialog();
        }
        private bool CanCreateNewProfileCommandExecute(object p) => true;

        public ICommand EditProfileCommand { get; }
        private void OnEditProfileCommandExecuted(object p)
        {
            CreateEditProfileVM.Mode = false;
            CreateEditProfileWindow createEditProfileWindow = new CreateEditProfileWindow();
            createEditProfileWindow.ShowDialog();
            Admin = (Administrator)Administrator.СurrentAdmin.Clone();
        }
        private bool CanEditProfileCommandExecute(object p) => true;

        public ICommand ShowAllProfilesCommand { get; }
        private void OnShowAllProfilesCommandExecuted(object p)
        {
            ProfilesListWindow profilesListWindow = new ProfilesListWindow();
            profilesListWindow.ShowDialog();
        }
        private bool CanShowAllProfilesCommandExecute(object p) => true;

        #endregion
        #region Справка
        public ICommand ManualCommand { get; }
        private void OnManualCommandExecuted(object p)
        {
            string text = "Справочная информация по использованию приложения\n\n" +
                "\tОКНО \"База данных\":\n" +
                "\tДля работы с базой данных на форме присутствуют кнопки отчистки базы данных, заполнения случайными значениями, а так-же окно поиска с настройками и кретериями" +
                "Для каждого респондента в базе данных, на форме существует возможность просмотра его анкеты. Окно с анкетными данными вызывается при нажатии кнопки \"Анкета\", расположенной напротив каждой записи в таблице просмотра базы данных\n\n" +
                "\tОКНО \"Результаты переписи\":\n" +
                "\tЕдинственная доступная функция окна результатов - просмотр диаграмм. Для этого необходимо выбрать из списка, расположенного в левой части формы, нужную вам диаграмму, после чего она появится в окне, расположенном в правой части формы\n\n" +
                "\tРАБОТА С ПРОФИЛЕМ:\n" +
                "\tНа главной форме, в ее левом верхнем углу, отображается текущий профиль. Для доступа к дополнительной информации о профиле и функциям необходимо нажать кнопку \"Профиль\"." +
                "В появившемся окне присутствуют кнопки \"Редактировать профиль\",\"Удалить профиль\",\"Создать новый профиль\",\"Список существующих профилей\" (кнопки \"Редактировать профиль\" и \"Удалить профиль\" доступны только в том случае, если текущий профиль не является системным)." +
                "Каждая из кнопок отвечает за одноименный функционал." +
                "Для того, чтобы сменить профиль, необходимо перезайти в приложение и в окне авторизации, выбрав способ авторизации, как администратор, ввести соответствующие желаемому профилю логин и пароль";

            MessageBox.Show(text, "Справочная информация по использованию приложения", MessageBoxButton.OK);
        }
        private bool CanManualCommandExecute(object p) => true;
        #endregion
        #endregion
        #region СВОЙСТВА

        private Double profileHeightSize = 51; public Double ProfileHeightSize
        {
            get => profileHeightSize;
            set => Set(ref profileHeightSize, value);
        }
        private SolidColorBrush profileTypeCollor = Brushes.White; public SolidColorBrush ProfileTypeCollor
        {
            get => profileTypeCollor;
            set => Set(ref profileTypeCollor, value);
        }
        private Visibility profileExtraInfo = Visibility.Visible; public Visibility ProfileExtraInfo
        {
            get => profileExtraInfo;
            set => Set(ref profileExtraInfo, value);
        }
        private Visibility profileButtonsVisibility = Visibility.Visible; public Visibility ProfileButtonsVisibility
        {
            get => profileButtonsVisibility;
            set => Set(ref profileButtonsVisibility, value);

        }
        #endregion
        #region МОДЕЛЬ
        private Administrator admin;
        public Administrator Admin
        {
            get => admin;
            set => Set(ref admin, value);
        }
        #endregion

        public MainMenuVM()
        {
            ShutdownAppCommand = new LambdaCommand(OnShutdownAppCommandExecuted, CanShutdownAppCommandExecute);
            LoadCensusResultsWindowCommand = new LambdaCommand(OnLoadCensusResultsWindowCommandExecuted, CanLoadCensusResultsWindowCommandExecute);
            LoadDataBaseWindowCommand = new LambdaCommand(OnLoadDataBaseWindowCommandExecuted, CanLoadDataBaseWindowCommandExecute);
            ShowHideProfileCommand = new LambdaCommand(OnShowHideProfileCommandExecuted, CanShowHideProfileCommandExecute);
            DeleteProfileCommand = new LambdaCommand(OnDeleteProfileCommandExecuted, CanDeleteProfileCommandExecute);
            CreateNewProfileCommand = new LambdaCommand(OnCreateNewProfileCommandExecuted, CanCreateNewProfileCommandExecute);
            ShowAllProfilesCommand = new LambdaCommand(OnShowAllProfilesCommandExecuted, CanShowAllProfilesCommandExecute);
            EditProfileCommand = new LambdaCommand(OnEditProfileCommandExecuted, CanEditProfileCommandExecute);
            ManualCommand = new LambdaCommand(OnManualCommandExecuted, CanManualCommandExecute);

            try
            {
                if (Administrator.СurrentAdmin == null) { Admin = new Administrator(); return; }
                Admin = (Administrator)Administrator.СurrentAdmin.Clone();

                if (Admin.AccountName == null || Administrator.SystemAccountName == null) { return; }
                if (Admin.AccountName.Equals(Administrator.SystemAccountName))
                {
                    ProfileTypeCollor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFE647"));
                    ProfileExtraInfo = Visibility.Collapsed;
                    ProfileButtonsVisibility = Visibility.Collapsed;
                }
                else
                {
                    ProfileTypeCollor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF91BCF9"));

                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.StackTrace);
                return;
            }

        }
    }
}

