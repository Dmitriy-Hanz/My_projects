using Census.infrastructure.commands.Base;
using Census.view;
using Census.viewModel.Base;
using Census.utils;
using Census.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Census.viewModel
{
    class BlankViewFormVM : ViewModelBase
    {
        #region КОМАНДЫ

        public ICommand CloseWindowCommand { get; }
        private void OnCloseWindowCommandExecuted(object p)
        {
            TypicalActions.CloseWindow<BlankViewFormWindow>();
        }
        private bool CanCloseWindowCommandExecute(object p) => true;


        public ICommand ShowHouseholdCommand { get; }
        private void OnShowHouseholdCommandExecuted(object p)
        {
            BlankTabSelectedIndex = 0;
            HouseholdVisibility = Visibility.Visible;
            if (Person.IsForeign == true)
            {
                ForeignVisibility = Visibility.Collapsed;
                return;
            }
            PersonVisibility = Visibility.Collapsed;
        }
        private bool CanShowHouseholdCommandExecute(object p) => HouseholdVisibility == Visibility.Collapsed;


        public ICommand ShowPersonCommand { get; }
        private void OnShowPersonCommandExecuted(object p)
        {
            HouseholdVisibility = Visibility.Collapsed;
            if (Person.IsForeign == true)
            {
                BlankTabSelectedIndex = 6;
                ForeignVisibility = Visibility.Visible;
                return;
            }
            BlankTabSelectedIndex = 2;
            PersonVisibility = Visibility.Visible;
        }
        private bool CanShowPersonCommandExecute(object p) => HouseholdVisibility == Visibility.Visible;


        #endregion
        #region СВОЙСТВА
        #region Cвойства сокрытия пунктов анкет

        private Visibility householdVisibility; public Visibility HouseholdVisibility
        {
            get => householdVisibility;
            set => Set(ref householdVisibility, value);
        }

        private Visibility personVisibility; public Visibility PersonVisibility
        {
            get => personVisibility;
            set => Set(ref personVisibility, value);
        }

        private Visibility foreignVisibility; public Visibility ForeignVisibility
        {
            get => foreignVisibility;
            set => Set(ref foreignVisibility, value);
        }

        #endregion
        #region ОСОБЫЕ значения свойств

        private bool[] sourceOfResourcesValues = new bool[13]; public bool[] SourceOfResourcesValues
        {
            get => sourceOfResourcesValues;
            set => Set(ref sourceOfResourcesValues, value);
        }

        private string mainSourceOfResourcesValue = ""; public string MainSourceOfResourcesValue
        {
            get => mainSourceOfResourcesValue;
            set
            {
                if (int.Parse(value) == 201)
                {
                    Set(ref mainSourceOfResourcesValue, "2.1");
                }
                Set(ref mainSourceOfResourcesValue, value);
            }
        }

        #endregion


        private int blankTabSelectedIndex; public int BlankTabSelectedIndex
        {
            get => blankTabSelectedIndex;
            set => Set(ref blankTabSelectedIndex, value);
        }


        #endregion
        #region МОДЕЛЬ

        public Dictionary<int, Person> PersonBuffer { get; set; }
        public Dictionary<int, Household> HouseholdBuffer { get; set; }

        private Household household; public Household Household
        {
            get => household;
            set => Set(ref household, value);
        }
        private Person person; public Person Person
        {
            get => person;
            set => Set(ref person, value);
        }


        #endregion

        public BlankViewFormVM()
        {
            CloseWindowCommand = new LambdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            ShowHouseholdCommand = new LambdaCommand(OnShowHouseholdCommandExecuted, CanShowHouseholdCommandExecute);
            ShowPersonCommand = new LambdaCommand(OnShowPersonCommandExecuted, CanShowPersonCommandExecute);

            PersonBuffer = DataBaseVM.Transferer.PersonBuffer;
            HouseholdBuffer = DataBaseVM.Transferer.HouseholdBuffer;
            Bufferization(DataBaseVM.Transferer.PersonId, DataBaseVM.Transferer.HouseholdId);

            bool[] temp = new bool[13];
            foreach(SourceOfResources item in Person.SourceOfResources)
            {
                switch (item)
                {
                    case SourceOfResources.Work: { temp[0] = true; break; }
                    case SourceOfResources.SelfEmployment: { temp[1] = true; break; }
                    case SourceOfResources.HouseholdWork: { temp[2] = true; break; }
                    case SourceOfResources.GoodsProduction: { temp[3] = true; break; }
                    case SourceOfResources.Pension: { temp[4] = true; break; }
                    case SourceOfResources.Scholarship: { temp[5] = true; break; }
                    case SourceOfResources.UnemploymentBenefit: { temp[6] = true; break; }
                    case SourceOfResources.GovernmentBenefits: { temp[7] = true; break; }
                    case SourceOfResources.OtherBenefits: { temp[8] = true; break; }
                    case SourceOfResources.AssetsActivity: { temp[9] = true; break; }
                    case SourceOfResources.SelfSources: { temp[10] = true; break; }
                    case SourceOfResources.Dependent: { temp[11] = true; break; }
                    case SourceOfResources.Other: { temp[12] = true; break; }
                }
            }
            SourceOfResourcesValues = temp;
            MainSourceOfResourcesValue = ((int)Person.MainSourceOfResources).ToString();

            HouseholdVisibility = Visibility.Collapsed;
            if (Person.IsForeign == true)
            {
                BlankTabSelectedIndex = 6;
                ForeignVisibility = Visibility.Visible;
                PersonVisibility = Visibility.Collapsed;
            }
            else
            {
                BlankTabSelectedIndex = 2;
                ForeignVisibility = Visibility.Collapsed;
                PersonVisibility = Visibility.Visible;
            }
        }

        private void Bufferization(int personId,int householdId)
        {
            if (PersonBuffer.Keys.Contains(personId))
            {
                Person = PersonBuffer.GetValueOrDefault(personId);
            }
            else
            {
                Person = CensusDBUtil.ReadPersonFromDatabase(personId);
                PersonBuffer.Add(personId, Person);
            }
            if (HouseholdBuffer.Keys.Contains(householdId))
            {
                Household = HouseholdBuffer.GetValueOrDefault(householdId);
            }
            else
            {
                Household = CensusDBUtil.ReadEmptyHouseholdFromDatabase(householdId);
                HouseholdBuffer.Add(householdId, Household);
            }
        }
    }
}
