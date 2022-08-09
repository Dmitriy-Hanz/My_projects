using Census.infrastructure.commands.Base;
using Census.view;
using Census.viewModel.Base;
using Census.utils;
using System.Windows;
using System.Windows.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Census.model;

namespace Census.viewModel
{
    class AuthorizationVM : ViewModelBase
    {
        #region СВОЙСТВА

        private Visibility passwordAreaVisibility = Visibility.Collapsed; public Visibility PasswordAreaVisibility
        {
            get => passwordAreaVisibility;
            set => Set(ref passwordAreaVisibility, value);
        }

        private string adminPassword; public string AdminPassword
        {
            get => adminPassword;
            set => Set(ref adminPassword, value);

        }

        #endregion
        #region КОМАНДЫ
        #region Команды для перехода к другим формам/выхода из приложения
        public ICommand AuthorizationCommand { get; }
        private void OnAuthorizationCommandExecuted(object p)
        {
            if (PasswordAreaVisibility == Visibility.Visible)
            {
                if (Administrator.AutorizationCheck() == false)
                {
                    MessageBox.Show("Неверно указано имя учетной записи или пароль", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                Administrator.СurrentAdmin = Administrator;
                Administrator.FillCurrentAdminInformation();
                TypicalActions.GoToWindow<AuthorizationWindow, MainMenuWindow>();
            }
            TypicalActions.GoToWindow<AuthorizationWindow, BlankSurveyWindow>();
        }
        private bool CanAuthorizationCommandExecute(object p) => true;


        public ICommand ShutdownAppCommand { get; }
        private void OnShutdownAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanShutdownAppCommandExecute(object p) => true;
        #endregion

        public ICommand SwitchUserTypeCommand { get; }
        private void OnSwitchUserTypeCommandExecuted(object p)
        {
            if (p.Equals("2"))
            {
                PasswordAreaVisibility = Visibility.Visible;
                return;
            }
            PasswordAreaVisibility = Visibility.Collapsed;
        }
        private bool CanSwitchUserTypeCommandExecute(object p) => true;
        #endregion
        #region МОДЕЛЬ
        public Administrator Administrator { get; set; } = new Administrator();
        #endregion

        public AuthorizationVM()
        {
            SwitchUserTypeCommand = new LambdaCommand(OnSwitchUserTypeCommandExecuted, CanSwitchUserTypeCommandExecute);
            AuthorizationCommand = new LambdaCommand(OnAuthorizationCommandExecuted, CanAuthorizationCommandExecute);
            ShutdownAppCommand = new LambdaCommand(OnShutdownAppCommandExecuted, CanShutdownAppCommandExecute);

            try
            {
                AdminPassword = DBUtil.ExecuteReturn("use CensusDB SELECT accountPassword FROM Administrator WHERE id_p = 1").Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.StackTrace);
                return;
            }
        }
    }
}
