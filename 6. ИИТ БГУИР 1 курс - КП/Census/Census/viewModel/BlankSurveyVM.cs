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
using Census.model;
using System.Windows.Controls;
using System.Windows.Data;

namespace Census.viewModel
{
    class BlankSurveyVM : ViewModelBase
    {
        #region КОМАНДЫ
        #region Команды для переходов на другие формы и выхода

        public ICommand ShutdownAppCommand { get; }
        private void OnShutdownAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanShutdownAppCommandExecute(object p) => true;

        public ICommand BackToAuthorizationWindowCommand { get; }
        private void OnBackToAuthorizationWindowCommandExecuted(object p)
        {
            TypicalActions.GoToWindow<BlankSurveyWindow, AuthorizationWindow>();
        }
        private bool CanBackToAuthorizationWindowCommandExecute(object p) => true;

        #endregion
        #region Команды для переходов к другим опросным бланкам

        public ICommand BorderOneContinueCommand { get; }
        private void OnBorderOneContinueCommandExecuted(object p)
        {
            if (Household.MiniValidate()) { BorderOneVisibility = Visibility.Hidden; }
        }
        private bool CanBorderOneContinueCommandExecute(object p) => true;

        public ICommand BlankHousehold1ContinueCommand { get; }
        private void OnBlankHousehold1ContinueCommandExecuted(object p)
        {
            if (Household.Validate())
            {
                BlankHouseholdTabSelectedIndex = 1;
                BlankBasePersonNumInfo = $"{Household.HouseholdMembers.Count + 1}/{Household.NumberOfMembers}";
            }
        }
        private bool CanBlankHousehold1ContinueCommandExecute(object p) => true;

        public ICommand BorderTwoContinueCommand { get; }
        private void OnBorderTwoContinueCommandExecuted(object p)
        {
            Person.IsForeign = bool.Parse(p.ToString());
            if (Person.IsForeign == true)
            {
                BlankBaseTabSelectedIndex = 4;
                BlankBasePageNumInfo = "1/1";
            }
            else
            {
                BlankBasePageNumInfo = "1/4";
            }
            BorderTwoVisibility = Visibility.Hidden;
        }
        private bool CanBorderTwoContinueCommandExecute(object p) => true;

        public ICommand BlankBaseContinueCommand { get; }
        private void OnBlankBaseContinueCommandExecuted(object p)
        {
            switch (BlankBaseTabSelectedIndex)
            {
                case 0:
                    if (!Person.ValidateOne()) { return; }
                    BlankBaseTabSelectedIndex = 1;
                    BlankBasePageNumInfo = "2/4";
                    return;
                case 1:
                    if (!Person.ValidateTwo()) { return; }
                    BlankBaseTabSelectedIndex = 2;
                    BlankBasePageNumInfo = "3/4";
                    return;
                case 2:
                    if (!Person.ValidateThree()) { return; }
                    BlankBaseTabSelectedIndex = 3;
                    BlankBasePageNumInfo = "4/4";
                    return;
                case 3:
                    if (!Person.ValidateFour()) { return; }
                    break;
                case 4:
                    if (!Person.ValidateForeign()) { return; }
                    break;
            }

            if (BlankBasePersonNumInfo.Equals($"{Household.NumberOfMembers}/{Household.NumberOfMembers}"))
            {
                if ((int)MessageBox.Show("Желаете окончить опрос (данные из анкет более нельзя будет редактировать)?", "Конец опроса", (MessageBoxButton)4, (MessageBoxImage)32) == 6)
                {
                    AttachPersonToHousehold();
                    CensusDBUtil.WriteResultsToDatabase(Household);
                    Person = null;
                    Household = null;
                    BlankHouseholdTabSelectedIndex = 2;
                }
                return;
            }
            if ((int)MessageBox.Show("Перейти к следующему члену домохозяйства? (Данные о текущем больше нельзя будет редактировать)", "Переход", (MessageBoxButton)4, (MessageBoxImage)32) == 6)
            {
                AttachPersonToHousehold();
                Person = new Person();
                ClearSpecialProperties();
                BorderTwoVisibility = Visibility.Visible;
                BlankBasePageNumInfo = "1/4";
                BlankBasePersonNumInfo = $"{Household.HouseholdMembers.Count + 1}/{Household.NumberOfMembers}";
                BlankBaseTabSelectedIndex = 0;
            }
        }
        private bool CanBlankBaseContinueCommandExecute(object p) => true;


        public ICommand BlankBasePreviousCommand { get; }
        private void OnBlankBasePreviousCommandExecuted(object p)
        {
            if (BlankBasePageNumInfo.Contains("2"))
            {
                BlankBaseTabSelectedIndex = 0;
                BlankBasePageNumInfo = BlankBasePageNumInfo.Replace('2', '1');
                return;
            }
            if (BlankBasePageNumInfo.Contains("3"))
            {
                BlankBaseTabSelectedIndex = 1;
                BlankBasePageNumInfo = BlankBasePageNumInfo.Replace('3', '2');
                return;
            }
            if (BlankBasePageNumInfo.Contains("4"))
            {
                BlankBaseTabSelectedIndex = 2;
                BlankBasePageNumInfo = "3/4";
                return;
            }
        }
        private bool CanBlankBasePreviousCommandExecute(object p)
        {
            if (BlankBaseTabSelectedIndex == 4 || BlankBaseTabSelectedIndex == 0) { return false; }
            return true;
        }


        public ICommand StopSurveyCommand { get; }
        private void OnStopSurveyCommandExecuted(object p)
        {
            BlankHouseholdTabSelectedIndex = 3;
        }
        private bool CanStopSurveyCommandExecute(object p) => true;

        public ICommand RestartSurveyCommand { get; }
        private void OnRestartSurveyCommandExecuted(object p)
        {
            Person = new Person();
            Household = new Household();
            ClearSpecialProperties();
            BorderOneVisibility = BorderTwoVisibility = Visibility.Visible;
            BlankBaseTabSelectedIndex = BlankHouseholdTabSelectedIndex = 0;
        }
        private bool CanRestartSurveyCommandExecute(object p) => true;

        #endregion
        #endregion
        #region СВОЙСТВА
        #region Cвойства сокрытия окон (border) с вопросами

        private Visibility borderOneVisibility = Visibility.Visible;
        public Visibility BorderOneVisibility
        {
            get => borderOneVisibility;
            set => Set(ref borderOneVisibility, value);
        }

        private Visibility borderTwoVisibility = Visibility.Visible;
        public Visibility BorderTwoVisibility
        {
            get => borderTwoVisibility;
            set => Set(ref borderTwoVisibility, value);
        }

        #endregion
        #region Свойства для BlankHousehold

        private int blankHouseholdTabSelectedIndex = 0;
        public int BlankHouseholdTabSelectedIndex
        {
            get => blankHouseholdTabSelectedIndex;
            set => Set(ref blankHouseholdTabSelectedIndex, value);
        }

        #endregion
        #region Свойства для BlankBase (включая BlankForeign)

        private int blankBaseTabSelectedIndex = 0;
        public int BlankBaseTabSelectedIndex
        {
            get => blankBaseTabSelectedIndex;
            set => Set(ref blankBaseTabSelectedIndex, value);
        }

        private string blankBasePersonNumInfo = ""; // Информация о порядковом номере анкетируемого, который сейчас проходит опрос
        public string BlankBasePersonNumInfo
        {
            get => blankBasePersonNumInfo;
            set => Set(ref blankBasePersonNumInfo, value);
        }

        private string blankBasePageNumInfo = "1/4"; // Информация о порядковом номере опросного листа, который сейчас заполняется слэйвом
        public string BlankBasePageNumInfo
        {
            get => blankBasePageNumInfo;
            set => Set(ref blankBasePageNumInfo, value);
        }

        #endregion
        #region Свойства для настроек разветвления (чекбоксы и текстбоксы)
        #region BlankHousehold1
        private ApartmentType apartmentTypeCase; public ApartmentType ApartmentTypeCase
        {
            get => apartmentTypeCase;
            set
            {
                if ((int)value > 2) { FromBlankHousehold1Q6Enable = false; } else { FromBlankHousehold1Q6Enable = true; }
                Household.HouseholdAccommodationsInfo.ApartmentType = value;
                Set(ref apartmentTypeCase, value);
            }
        }
        private bool fromBlankHousehold1Q6Enable = false; public bool FromBlankHousehold1Q6Enable
        {
            get => fromBlankHousehold1Q6Enable;
            set => Set(ref fromBlankHousehold1Q6Enable, value);
        }

        #endregion
        #region BlankBase1
        private string blankBase1Q3AgeCase = ""; public string BlankBase1Q3AgeCase
        {
            get => blankBase1Q3AgeCase;
            set
            {
                int temp;
                if (int.TryParse(value, out temp) != false)
                {
                    if (temp >= 15 & temp < 74) { BlankBase1Q3AgeCaseForQ9p4 = true; BlankBase1Q3AgeCaseForQWork = true; }
                    else { BlankBase1Q3AgeCaseForQ9p4 = false; BlankBase1Q3AgeCaseForQWork = false; }

                    if (temp >= 10) { BlankBase1Q3AgeCaseForQ14 = true; } else { BlankBase1Q3AgeCaseForQ14 = false; }
                    if (temp >= 6) { BlankBase1Q3AgeCaseForQ15p1 = true; } else { BlankBase1Q3AgeCaseForQ15p1 = false; }
                    if (temp >= 15 && temp <= 65) { BlankBase1Q3AgeCaseForQ15p2 = true; } else { BlankBase1Q3AgeCaseForQ15p2 = false; }
                    if (temp <= 7) { BlankBase1Q3AgeCaseForQ15p3 = true; } else { BlankBase1Q3AgeCaseForQ15p3 = false; }
                    if (temp < 15) { BlankBase1Q6Enable = false; } else { BlankBase1Q6Enable = true; }

                    if ((int)Person.Gender == 1) { BlankBase4Q24Enable = BlankBase4Q25Enable = false; }
                    else
                    {
                        if (temp < 15) { BlankBase4Q24Enable = false; } else { BlankBase4Q24Enable = true; }
                        if (temp >= 18 && temp <= 49) { BlankBase4Q25Enable = true; } else { BlankBase4Q25Enable = false; }
                    }
                }

                Person.Age = value;
                Set(ref blankBase1Q3AgeCase, value);
            }
        }
        private bool blankBase1Q6Enable; public bool BlankBase1Q6Enable
        {
            get => blankBase1Q6Enable;
            set => Set(ref blankBase1Q6Enable, value);
        }
        private Gender genderCase; public Gender GenderCase
        {
            get => genderCase;
            set
            {
                if ((int)value == 1 || Person.Age.Equals("")) { BlankBase4Q24Enable = BlankBase4Q25Enable = false; }
                else
                {
                    if (int.Parse(Person.Age) >= 15) { BlankBase4Q24Enable = true; }
                    if (int.Parse(Person.Age) >= 18 && int.Parse(Person.Age) <= 49) { BlankBase4Q25Enable = true; }
                }
                Person.Gender = value;
                Set(ref genderCase, value);
            }
        }
        private bool? doYouLiveHereFromBirthCase; public bool? DoYouLiveHereFromBirthCase
        {
            get => doYouLiveHereFromBirthCase;
            set
            {
                //if (value == null) { Set(ref blankBase1Q8Case, false); return; }
                if (value == true) { FromBlankBase1Q8Enable = false; } else { FromBlankBase1Q8Enable = true; }
                Person.LivingPlaceInfo.DoYouLiveHereFromBirth = value;
                Set(ref doYouLiveHereFromBirthCase, value);
            }
        }
        private bool fromBlankBase1Q8Enable; public bool FromBlankBase1Q8Enable
        {
            get => fromBlankBase1Q8Enable;
            set => Set(ref fromBlankBase1Q8Enable, value);
        }

        #endregion
        #region BlankBase2

        private bool? didYouLiveInOtherCountryCase; public bool? DidYouLiveInOtherCountryCase
        {
            get => didYouLiveInOtherCountryCase;
            set
            {
                if (value == null) { FromBlankBase2Q9Enable = false; }
                else { FromBlankBase2Q9Enable = (bool)value; }
                Person.LivingCountryInfo.DidYouLiveInOtherCountry = value;
                Set(ref didYouLiveInOtherCountryCase, value);
            }
        }
        private bool fromBlankBase2Q9Enable; public bool FromBlankBase2Q9Enable
        {
            get => fromBlankBase2Q9Enable;
            set => Set(ref fromBlankBase2Q9Enable, value);
        }

        private DoYouWantToLeaveBelarus blankBase2Q9p4Case; public DoYouWantToLeaveBelarus BlankBase2Q9p4Case
        {
            get => blankBase2Q9p4Case;
            set
            {
                if ((int)value != 2) { BlankBase2Q9p5Enable = true; } else { BlankBase2Q9p5Enable = false; }
                Person.LivingCountryInfo.DoYouWantToLeaveBelarus = value;
                Set(ref blankBase2Q9p4Case, value);
            }
        }
        private bool blankBase2Q9p5Enable; public bool BlankBase2Q9p5Enable
        {
            get => blankBase2Q9p5Enable;
            set => Set(ref blankBase2Q9p5Enable, value);
        }
        private bool blankBase1Q3AgeCaseForQ9p4; public bool BlankBase1Q3AgeCaseForQ9p4
        {
            get => blankBase1Q3AgeCaseForQ9p4;
            set => Set(ref blankBase1Q3AgeCaseForQ9p4, value);
        }
        private bool blankBase1Q3AgeCaseForQ14; public bool BlankBase1Q3AgeCaseForQ14
        {
            get => blankBase1Q3AgeCaseForQ14;
            set => Set(ref blankBase1Q3AgeCaseForQ14, value);
        }

        #endregion
        #region BlankBase3

        private bool blankBase1Q3AgeCaseForQ15p1; public bool BlankBase1Q3AgeCaseForQ15p1
        {
            get => blankBase1Q3AgeCaseForQ15p1;
            set => Set(ref blankBase1Q3AgeCaseForQ15p1, value);
        }
        private bool blankBase1Q3AgeCaseForQ15p2; public bool BlankBase1Q3AgeCaseForQ15p2
        {
            get => blankBase1Q3AgeCaseForQ15p2;
            set => Set(ref blankBase1Q3AgeCaseForQ15p2, value);
        }
        private bool blankBase1Q3AgeCaseForQ15p3; public bool BlankBase1Q3AgeCaseForQ15p3
        {
            get => blankBase1Q3AgeCaseForQ15p3;
            set => Set(ref blankBase1Q3AgeCaseForQ15p3, value);
        }

        private SourceOfResources blankBase3Q16Case; public SourceOfResources BlankBase3Q16Case
        {
            get => blankBase3Q16Case;
            set
            {
                if (value != 0)
                {
                    if (Person.SourceOfResources.Contains(value))
                    {
                        Person.SourceOfResources.Remove(value);
                    }
                    else
                    {
                        Person.SourceOfResources.Add(value);
                    }
                }
                Set(ref blankBase3Q16Case, value);
            }
        }
        private string mainSourceOfResourcesCase = ""; public string MainSourceOfResourcesCase
        {
            get => mainSourceOfResourcesCase;
            set
            {
                if (int.TryParse(value, out int temp) == false)
                {
                    Person.MainSourceOfResources = (SourceOfResources)999;
                }
                Person.MainSourceOfResources = (SourceOfResources)temp;
                Set(ref mainSourceOfResourcesCase, value);
            }
        }

        private bool blankBase1Q3AgeCaseForQWork; public bool BlankBase1Q3AgeCaseForQWork
        {
            get => blankBase1Q3AgeCaseForQWork;
            set => Set(ref blankBase1Q3AgeCaseForQWork, value);
        }

        private bool? doYouHaveWorkCase; public bool? DoYouHaveWorkCase
        {
            get => doYouHaveWorkCase;
            set
            {
                if (value.Equals(true)) { BlankBase3Q17p1Enable = true; } else { BlankBase3Q17p1Enable = false; }
                Person.WorkInfo.DoYouHaveWork = value;
                Set(ref doYouHaveWorkCase, value);
            }
        }
        private bool? whyYouDontHaveWorkCase; public bool? WhyYouDontHaveWorkCase
        {
            get => whyYouDontHaveWorkCase;
            set
            {
                switch (value)
                {
                    case true:
                        BlankBase3Q17p1Enable = true;
                        BlankBase4Q21Enable = BlankBase4Q22Enable = BlankBase4Q23Enable = false;
                        break;
                    case false:
                        BlankBase3Q17p1Enable = BlankBase3Q18Enable = BlankBase4Q19Enable = BlankBase4Q20Enable = BlankBase4Q22Enable = false;
                        BlankBase4Q21Enable = BlankBase4Q23Enable = true;
                        break;
                }
                Person.WorkInfo.WhyYouDontHaveWork = value;
                Set(ref whyYouDontHaveWorkCase, value);
            }
        }
        private LocationOfWork locationOfWorkCase; public LocationOfWork LocationOfWorkCase
        {
            get => locationOfWorkCase;
            set
            {
                switch ((int)value)
                {
                    case 0:
                        BlankBase3Q18Enable = BlankBase4Q19Enable = BlankBase4Q20Enable = false; break;
                    case 1:
                        BlankBase3Q18Enable = false; BlankBase4Q19Enable = BlankBase4Q20Enable = true; break;
                    case 2:
                    case 3:
                        BlankBase3Q18Enable = BlankBase4Q19Enable = BlankBase4Q20Enable = true; break;
                }
                Person.WorkInfo.LocationOfWork = value;
                Set(ref locationOfWorkCase, value);
            }
        }

        private bool blankBase3Q17p1Enable; public bool BlankBase3Q17p1Enable
        {
            get => blankBase3Q17p1Enable;
            set => Set(ref blankBase3Q17p1Enable, value);
        }
        private bool blankBase3Q17p2Enable; public bool BlankBase3Q17p2Enable
        {
            get => blankBase3Q17p2Enable;
            set => Set(ref blankBase3Q17p2Enable, value);
        }
        private bool blankBase3Q18Enable; public bool BlankBase3Q18Enable
        {
            get => blankBase3Q18Enable;
            set => Set(ref blankBase3Q18Enable, value);
        }

        #endregion
        #region BlankBase4
        private bool blankBase4Q19Enable; public bool BlankBase4Q19Enable
        {
            get => blankBase4Q19Enable;
            set => Set(ref blankBase4Q19Enable, value);
        }
        private bool blankBase4Q20Enable; public bool BlankBase4Q20Enable
        {
            get => blankBase4Q20Enable;
            set => Set(ref blankBase4Q20Enable, value);
        }

        private bool? didYouLookedForAJobCase; public bool? DidYouLookedForAJobCase
        {
            get => didYouLookedForAJobCase;
            set
            {
                if (value == null) { BlankBase4Q22Enable = false; } else { BlankBase4Q22Enable = true; }
                if (!(value == CanYouStarWorkingInTwoWeeksCase == true) || CanYouStarWorkingInTwoWeeksCase == false) { BlankBase4Q23Enable = true; } else { BlankBase4Q23Enable = false; }

                Person.WorkInfo.DidYouLookedForAJob = value;
                Set(ref didYouLookedForAJobCase, value);
            }
        }
        private bool? canYouStarWorkingInTwoWeeksCase; public bool? CanYouStarWorkingInTwoWeeksCase
        {
            get => canYouStarWorkingInTwoWeeksCase;
            set
            {
                if (!(DidYouLookedForAJobCase == value == true) || value == false) { BlankBase4Q23Enable = true; } else { BlankBase4Q23Enable = false; }

                Person.WorkInfo.CanYouStarWorkingInTwoWeeks = value;
                Set(ref canYouStarWorkingInTwoWeeksCase, value);
            }
        }

        private bool blankBase4Q21Enable = false; public bool BlankBase4Q21Enable
        {
            get => blankBase4Q21Enable;
            set => Set(ref blankBase4Q21Enable, value);
        }
        private bool blankBase4Q22Enable = false; public bool BlankBase4Q22Enable
        {
            get => blankBase4Q22Enable;
            set => Set(ref blankBase4Q22Enable, value);
        }
        private bool blankBase4Q23Enable = false; public bool BlankBase4Q23Enable
        {
            get => blankBase4Q23Enable;
            set => Set(ref blankBase4Q23Enable, value);
        }
        private bool blankBase4Q24Enable = false; public bool BlankBase4Q24Enable
        {
            get => blankBase4Q24Enable;
            set => Set(ref blankBase4Q24Enable, value);
        }
        private bool blankBase4Q25Enable = false; public bool BlankBase4Q25Enable
        {
            get => blankBase4Q25Enable;
            set => Set(ref blankBase4Q25Enable, value);
        }

        #endregion
        #region BlankForeign
        private Gender genderForForeignCase; public Gender GenderForForeignCase
        {
            get => genderForForeignCase;
            set
            {
                Person.Gender = value;
                Set(ref genderForForeignCase, value);
            }
        }
        private BirthCountry birthCountryForForeignCase; public BirthCountry BirthCountryForForeignCase
        {
            get => birthCountryForForeignCase;
            set
            {
                Person.BirthCountry = value;
                Set(ref birthCountryForForeignCase, value);
            }
        }
        private Citizenship citizenshipForForeignCase; public Citizenship CitizenshipForForeignCase
        {
            get => citizenshipForForeignCase;
            set
            {
                Person.Citizenship = value;
                Set(ref citizenshipForForeignCase, value);
            }
        }
        #endregion
        #endregion
        #endregion
        #region МОДЕЛЬ

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
        public BlankSurveyVM()
        {
            StopSurveyCommand = new LambdaCommand(OnStopSurveyCommandExecuted, CanStopSurveyCommandExecute);
            ShutdownAppCommand = new LambdaCommand(OnShutdownAppCommandExecuted, CanShutdownAppCommandExecute);
            BackToAuthorizationWindowCommand = new LambdaCommand(OnBackToAuthorizationWindowCommandExecuted, CanBackToAuthorizationWindowCommandExecute);
            RestartSurveyCommand = new LambdaCommand(OnRestartSurveyCommandExecuted, CanRestartSurveyCommandExecute);


            BorderOneContinueCommand = new LambdaCommand(OnBorderOneContinueCommandExecuted, CanBorderOneContinueCommandExecute);
            BlankHousehold1ContinueCommand = new LambdaCommand(OnBlankHousehold1ContinueCommandExecuted, CanBlankHousehold1ContinueCommandExecute);
            BorderTwoContinueCommand = new LambdaCommand(OnBorderTwoContinueCommandExecuted, CanBorderTwoContinueCommandExecute);
            BlankBaseContinueCommand = new LambdaCommand(OnBlankBaseContinueCommandExecuted, CanBlankBaseContinueCommandExecute);
            BlankBasePreviousCommand = new LambdaCommand(OnBlankBasePreviousCommandExecuted, CanBlankBasePreviousCommandExecute);

            Household = new Household();
            Person = new Person();

            //Preset();
        }

        private void PresetHousehold()
        {
            Household.FullAdressInfo.Region = "afsfdg";
            Household.FullAdressInfo.RegionDistrict = "sgrh";
            Household.FullAdressInfo.VillageCouncil = "atsrhdtjyu";
            Household.FullAdressInfo.VillageName = "rtyujik";
            Household.FullAdressInfo.StreetAvenueOther = "srgt";
            Household.FullAdressInfo.HouseNumber = "67h";
            Household.FullAdressInfo.FlatOrRoomNumber = "56";
            Household.NumberOfMembers = "1";

            Household.RoomsCount = "2";
            Household.HouseholdAccommodationsInfo.OwnerOfApartment = (OwnerOfApartment)3;
            Household.HasForeighResidents = false;
            Household.HasFarmFacilities = false;
            ApartmentTypeCase = (ApartmentType)4;//Household.HouseholdAccommodationsInfo.ApartmentType
        }
        private void PresetForeign()
        {
            PresetHousehold();
            Person = new Person();
            Person.Surname = "Fgh";
            Person.Name = "kjhgytuh";
        }
        private void Preset()
        {
            PresetHousehold();
            Person = new Person();
            Person.Surname = "Fgh";
            Person.Name = "kjhgytuh";
            Person.PassportID = "fbgn";
            Person.BirthdayDate = DateTime.Parse("11.11.1111");
            BlankBase1Q3AgeCase = "22";//Person.Age
            GenderCase = (Gender)1;//Person.Gender
            Person.HouseholdRelations = (HouseholdRelations)1;
            Person.MaritalStatus = (MaritalStatus)1;
            Person.BirthCountry = (BirthCountry)1;
            DoYouLiveHereFromBirthCase = false;//Person.LivingPlaceInfo.DoYouLiveHereFromBirth
            Person.LivingPlaceInfo.ArrivalPeriod = DateTime.Parse("11.11.1111");
            Person.LivingPlaceInfo.PreviousLivingPlace = (PreviousLivingPlace)2;
            Person.LivingPlaceInfo.NameOfPreviousCountry = "gsrdhfjg";
            Person.LivingPlaceInfo.ReasonForArrivalAtPlace = (ReasonForMigration)1;

            DidYouLiveInOtherCountryCase = true;//Person.LivingCountryInfo.DidYouLiveInOtherCountry
            Person.LivingCountryInfo.NameOfCountryYouCameFrom = "SUCKsonia";
            Person.LivingCountryInfo.ArrivalPeriod = new DateTime(2022, 06, 12);
            Person.LivingCountryInfo.ReasonForArrivalAtRB = (ReasonForMigration)3;
            Person.LivingCountryInfo.DoYouWantToLeaveBelarus = (DoYouWantToLeaveBelarus)102;
            Person.LivingCountryInfo.ReasonForLeaveBelarus = (ReasonForMigration)2;
            Person.Citizenship = (Citizenship)1;
            Person.Nationality = (Nationality)5;
            Person.NativeLanguage = (NativeLanguage)2;
            Person.SpeakingLanguage = (SpeakingLanguage)4;
            Person.EducationInfo.LvlOfEducation = (LvlOfEducation)5;

            
            Person.EducationInfo.GettingBasicEducation = (GettingBasicEducation)103;
            Person.EducationInfo.GettingAdditionalEducation = true;
            BlankBase3Q16Case = (SourceOfResources)11;//Person.SourceOfResources.Add((SourceOfResources)11);
            MainSourceOfResourcesCase = "11";// Person.MainSourceOfResources = (SourceOfResources)11;

            Person.WorkInfo.DoYouHaveWork = false;
            Person.WorkInfo.WhyYouDontHaveWork = false;
        }
        private void ResetParams()
        {
            #region Household
            if (FromBlankHousehold1Q6Enable == false)
            {
                Household.HouseholdAccommodationsInfo.AreaOfFlat = null;
                Household.HouseholdAccommodationsInfo.WaterPipes = 0;
                Household.HouseholdAccommodationsInfo.Сanalization = 0;
                Household.HouseholdAccommodationsInfo.HasBathOrShower = null;
                Household.HouseholdAccommodationsInfo.HotWaterSupply = 0;
                Household.HouseholdAccommodationsInfo.Heating = 0;
                Household.HouseholdAccommodationsInfo.CookingEquipment = 0;
            }
            #endregion
            #region BlankBase1
            if (BlankBase1Q6Enable == false)
            {
                Person.MaritalStatus = 0;
            }
            if ((int)Person.BirthCountry == 1)
            {
                Person.NameOfBirthCountry = null;
            }
            if (DoYouLiveHereFromBirthCase == false)
            {
                Person.LivingPlaceInfo.ArrivalPeriod = null;
                Person.LivingPlaceInfo.PreviousLivingPlace = 0;
                Person.LivingPlaceInfo.ReasonForArrivalAtPlace = 0;
            }
            else
            {
                if ((int)Person.LivingPlaceInfo.PreviousLivingPlace != 1)
                {
                    Person.LivingPlaceInfo.RegionOrDistrictName = null;
                    Person.LivingPlaceInfo.CityOrPGTName = null;
                    Person.LivingPlaceInfo.IsItVillage = null;
                }
                if ((int)Person.LivingPlaceInfo.PreviousLivingPlace != 2)
                {
                    Person.LivingPlaceInfo.RegionOrDistrictName = null;
                }
            }
            #endregion
            #region BlankBase2
            if (DoYouLiveHereFromBirthCase == false)
            {
                Person.LivingCountryInfo.DidYouLiveInOtherCountry = null;//BlankBase2Q9Case
                Person.LivingCountryInfo.NameOfCountryYouCameFrom = null;
                Person.LivingCountryInfo.ArrivalPeriod = null;
                Person.LivingCountryInfo.ReasonForArrivalAtRB = 0;
            }
            else
            {
                if (DidYouLiveInOtherCountryCase == false)
                {
                    Person.LivingCountryInfo.NameOfCountryYouCameFrom = null;
                    Person.LivingCountryInfo.ArrivalPeriod = null;
                    Person.LivingCountryInfo.ReasonForArrivalAtRB = 0;
                }
            }
            if (BlankBase1Q3AgeCaseForQ9p4 == false)
            {
                Person.LivingCountryInfo.ReasonForLeaveBelarus = 0;
            }
            if ((int)Person.Citizenship != 2)
            {
                Person.NameOfCitizenshipCountry = null;
            }
            if ((int)Person.Nationality != 6)
            {
                Person.NameOfNationality = null;
            }
            if ((int)Person.NativeLanguage != 5)
            {
                Person.NameOfNativeLanguage = null;
            }
            if ((int)Person.SpeakingLanguage != 5)
            {
                Person.NameOfSpeakingLanguage = null;
            }
            if (BlankBase1Q3AgeCaseForQ14 == false)
            {
                Person.EducationInfo.LvlOfEducation = 0;
                Person.EducationInfo.AcademicDegree = 0;
                Person.EducationInfo.CanYouReadAndWrite = null;
            }
            else
            {
                if ((int)Person.EducationInfo.LvlOfEducation != 8)
                {
                    Person.EducationInfo.CanYouReadAndWrite = null;
                }
            }
            #endregion
            #region BlankBase3
            if (BlankBase1Q3AgeCaseForQ15p1 == false)
            {
                Person.EducationInfo.GettingBasicEducation = 0;
            }
            if (BlankBase1Q3AgeCaseForQ15p2 == false)
            {
                Person.EducationInfo.GettingAdditionalEducation = null;
            }
            if (BlankBase1Q3AgeCaseForQ15p3 == false)
            {
                Person.EducationInfo.DoesChildAttendPreschool = null;
            }
            if (BlankBase1Q3AgeCaseForQWork == false)
            {
                Person.WorkInfo.DoYouHaveWork = null;
                Person.WorkInfo.WhyYouDontHaveWork = null;
                Person.WorkInfo.LocationOfWork = 0;
                Person.WorkInfo.RegionOrDistrictName = null;
                Person.WorkInfo.CityOrPGTName = null;
                Person.WorkInfo.IsItVillage = null;
                Person.WorkInfo.NameOfCountry = null;
                Person.WorkInfo.DepartureFrequencyToWork = 0;
                Person.WorkInfo.UnemploymentReason = 0;
            }
            else
            {
                if (Person.WorkInfo.DoYouHaveWork != true)
                {
                    Person.WorkInfo.WhyYouDontHaveWork = null;
                }
                if (BlankBase3Q17p1Enable == false)
                {
                    Person.WorkInfo.LocationOfWork = 0;
                    Person.WorkInfo.RegionOrDistrictName = null;
                    Person.WorkInfo.CityOrPGTName = null;
                    Person.WorkInfo.IsItVillage = null;
                    Person.WorkInfo.NameOfCountry = null;
                    Person.WorkInfo.DepartureFrequencyToWork = 0;
                }
                else
                {
                    if ((int)Person.WorkInfo.LocationOfWork != 2)
                    {
                        Person.WorkInfo.RegionOrDistrictName = null;
                        Person.WorkInfo.CityOrPGTName = null;
                        Person.WorkInfo.IsItVillage = null;
                    }
                    if ((int)Person.WorkInfo.LocationOfWork != 3)
                    {
                        Person.WorkInfo.NameOfCountry = null;
                        Person.WorkInfo.DepartureFrequencyToWork = 0;
                    }
                }
                if (BlankBase3Q18Enable == false)
                {
                    Person.WorkInfo.UnemploymentReason = 0;
                }
            }
            #endregion
            #region BlankBase4
            if (BlankBase4Q19Enable == false)
            {
                Person.WorkInfo.TypeOfWorkplace = 0;
            }
            if (BlankBase4Q20Enable == false)
            {
                Person.WorkInfo.TypeOfWorkPosition = 0;
            }
            if (BlankBase4Q21Enable == false)
            {
                Person.WorkInfo.DidYouLookedForAJob = null;
            }
            if (BlankBase4Q22Enable == false)
            {
                Person.WorkInfo.CanYouStarWorkingInTwoWeeks = null;
            }
            if (BlankBase4Q23Enable == false)
            {
                Person.WorkInfo.WhyYouCantWorkOrStopedSearch = 0;
            }
            if (BlankBase4Q24Enable == false)
            {
                Person.ChildrenInfo.HowManyChildrenDidYouHave = null;
                Person.ChildrenInfo.NoChildren = null;
            }
            if (BlankBase4Q25Enable == false)
            {
                Person.ChildrenInfo.ChildrenPlans = 0;
                Person.ChildrenInfo.NoChildren = null;
                Person.ChildrenInfo.HowManyChildrenDoYouWant = null;
                Person.ChildrenInfo.DontKnowHowMany = null;
            }
            else
            {
                if ((int)Person.ChildrenInfo.ChildrenPlans != 1)
                {
                    Person.ChildrenInfo.HowManyChildrenDoYouWant = null;
                }
            }
            #endregion
            #region BlankBaseForeign
            if (Person.IsForeign == true)
            {
                if ((int)Person.BirthCountry != 2)
                {
                    Person.NameOfBirthCountry = null;
                }
                if ((int)Person.Citizenship != 1)
                {
                    Person.PassportID = null;
                }
                else
                {
                    if ((int)Person.Citizenship != 3)
                    {
                        Person.NameOfCitizenshipCountry = null;
                    }
                }
            }
            #endregion
        }
        private void ClearSpecialProperties()
        {
            ApartmentTypeCase = 0;
            BlankBase1Q3AgeCase = "";
            GenderCase = 0;
            DoYouLiveHereFromBirthCase = null;
            DidYouLiveInOtherCountryCase = null;
            BlankBase2Q9p4Case = 0;
            BlankBase3Q16Case = 0;
            DoYouHaveWorkCase = null;
            WhyYouDontHaveWorkCase = null;
            LocationOfWorkCase = 0;
            DidYouLookedForAJobCase = null;
            CanYouStarWorkingInTwoWeeksCase = null;

            GenderForForeignCase = 0;
            BirthCountryForForeignCase = 0;
            CitizenshipForForeignCase = 0;
        }
        private void AttachPersonToHousehold()
        {
            ResetParams();
            Household.HouseholdMembers.Add(Person);
            Person = null;
        }
    }
}
