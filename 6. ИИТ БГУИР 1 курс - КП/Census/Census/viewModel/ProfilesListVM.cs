using Census.model;
using Census.viewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Census.viewModel
{
    class ProfilesListVM : ViewModelBase
    {
        #region МОДЕЛЬ
        public List<Administrator> Profiles { get; set; }
        #endregion

        public ProfilesListVM()
        {
            Profiles = new List<Administrator>(Administrator.Profiles);
            Administrator temp;
            for (int i = 0; i < Profiles.Count; i++)
            {
                for (int j = i; j < Profiles.Count; j++)
                {
                    if (Profiles[i].AccountName.CompareTo(Profiles[j].AccountName) == -1)
                    {
                        temp = Profiles[i];
                        Profiles[i] = Profiles[j];
                        Profiles[j] = temp;
                    }
                }
            }
        }
    }
}