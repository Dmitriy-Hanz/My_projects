using Census.infrastructure.commands.Base;
using Census.model;
using Census.utils;
using Census.view;
using Census.viewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Census.viewModel
{
    class CreateEditProfileVM : ViewModelBase
    {
        #region ХРЕНЬ
        public static bool Mode { get; set; }
        #endregion
        #region КОМАНДЫ
        #region Две кнопки

        public ICommand SaveProfileCommand { get; }
        private void OnSaveProfileCommandExecuted(object p)
        {
            if (Mode)
            {
                Administrator.AddNewAdministrator(Admin);
            }
            else
            {
                Administrator.СurrentAdmin = (Administrator)Admin.Clone();
                Administrator.UpdateCurrentAdministrator();
            }
            TypicalActions.CloseWindow<CreateEditProfileWindow>();
        }
        private bool CanSaveProfileCommandExecute(object p) => true;


        public ICommand CancelCommand { get; }
        private void OnCancelCommandExecuted(object p)
        {
            TypicalActions.CloseWindow<CreateEditProfileWindow>();
        }
        private bool CanCancelCommandExecute(object p) => true;
        #endregion
        #endregion
        #region МОДЕЛЬ
        public Administrator Admin { get; set; }
        #endregion

        public CreateEditProfileVM()
        {
            SaveProfileCommand = new LambdaCommand(OnSaveProfileCommandExecuted, CanSaveProfileCommandExecute);
            CancelCommand = new LambdaCommand(OnCancelCommandExecuted, CanCancelCommandExecute);
            if(Administrator.СurrentAdmin == null) { return; }
            if (Mode)
            {
                Admin = new Administrator();
                return;
            }
            Admin = (Administrator)Administrator.СurrentAdmin.Clone();
        }
    }
}
