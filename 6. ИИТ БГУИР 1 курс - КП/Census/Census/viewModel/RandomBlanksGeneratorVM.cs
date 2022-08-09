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
using System.Windows;
using System.Windows.Input;

namespace Census.viewModel
{
    class RandomBlanksGeneratorVM : ViewModelBase
    {
        #region СВОЙСТВА
        private int generatedBlanksCount; public int GeneratedBlanksCount
        {
            get => generatedBlanksCount;
            set => Set(ref generatedBlanksCount, value);
        }

        #endregion
        #region КОМАНДЫ
        public ICommand GenerateBlanksCommand { get; }
        private void OnGenerateBlanksCommandExecuted(object p)
        {
            GenerateBlanks(GeneratedBlanksCount);
            if(MessageBox.Show("Генерация прошла успешно. Желаете продолжить?","Ы",MessageBoxButton.YesNo,MessageBoxImage.Information) == MessageBoxResult.No)
            {
                TypicalActions.CloseWindow<RandomBlanksGeneratorWindow>();
            }
        }
        private bool CanGenerateBlanksCommandExecute(object p) => true;


        public ICommand CancelCommand { get; }
        private void OnCancelCommandExecuted(object p)
        {
            TypicalActions.CloseWindow<RandomBlanksGeneratorWindow>();
        }
        private bool CanCancelCommandExecute(object p) => true;
        #endregion
        #region МОДЕЛЬ

        #endregion
        public RandomBlanksGeneratorVM()
        {
            GenerateBlanksCommand = new LambdaCommand(OnGenerateBlanksCommandExecuted, CanGenerateBlanksCommandExecute);
            CancelCommand = new LambdaCommand(OnCancelCommandExecuted, CanCancelCommandExecute);
        }

        private void GenerateBlanks(int personCount)
        {
            IEnumerable<OwnerOfApartment> ownerOfApartment = Enum.GetValues(OwnerOfApartment.PrivateIndividual.GetType()).Cast<OwnerOfApartment>();
            IEnumerable<ApartmentType> apartmentType = Enum.GetValues(ApartmentType.AnotherHostel.GetType()).Cast<ApartmentType>();
            IEnumerable<WaterPipes> waterPipes = Enum.GetValues(WaterPipes.None.GetType()).Cast<WaterPipes>();
            IEnumerable<Сanalization> canalization = Enum.GetValues(Сanalization.None.GetType()).Cast<Сanalization>();
            IEnumerable<HotWaterSupply> hotWaterSupply = Enum.GetValues(HotWaterSupply.None.GetType()).Cast<HotWaterSupply>();
            IEnumerable<Heating> heating = Enum.GetValues(Heating.None.GetType()).Cast<Heating>();
            IEnumerable<CookingEquipment> cookingEquipment = Enum.GetValues(CookingEquipment.None.GetType()).Cast<CookingEquipment>();


            Gender[] gender = Enum.GetValues<Gender>();
            HouseholdRelations[] householdRelations = Enum.GetValues<HouseholdRelations>();
            MaritalStatus[] maritalStatus = Enum.GetValues<MaritalStatus>();
            BirthCountry[] birthCountry = Enum.GetValues<BirthCountry>();
            PreviousLivingPlace[] previousLivingPlace = Enum.GetValues<PreviousLivingPlace>();
            Citizenship[] citizenship = Enum.GetValues<Citizenship>();
            Nationality[] nationality = Enum.GetValues<Nationality>();
            NativeLanguage[] nativeLanguage = Enum.GetValues<NativeLanguage>();
            SpeakingLanguage[] speakingLanguage = Enum.GetValues<SpeakingLanguage>();
            SourceOfResources[] sourceOfResources = Enum.GetValues<SourceOfResources>();

            LocationOfWork[] locationOfWork = Enum.GetValues<LocationOfWork>();
            DepartureFrequencyToWork[] departureFrequencyToWork = Enum.GetValues<DepartureFrequencyToWork>();
            UnemploymentReason[] unemploymentReason = Enum.GetValues<UnemploymentReason>();
            TypeOfWorkplace[] typeOfWorkplace = Enum.GetValues<TypeOfWorkplace>();
            TypeOfWorkPosition[] typeOfWorkPosition = Enum.GetValues<TypeOfWorkPosition>();
            WhyYouCantWorkOrStopedSearch[] whyYouCantWorkOrStopedSearch = Enum.GetValues<WhyYouCantWorkOrStopedSearch>();

            LvlOfEducation[] lvlOfEducation = Enum.GetValues<LvlOfEducation>();
            AcademicDegree[] academicDegree = Enum.GetValues<AcademicDegree>();
            GettingBasicEducation[] gettingBasicEducation = Enum.GetValues<GettingBasicEducation>();

            ReasonForMigration[] reasonForMigration = Enum.GetValues<ReasonForMigration>();
            DoYouWantToLeaveBelarus[] doYouWantToLeaveBelarus = Enum.GetValues<DoYouWantToLeaveBelarus>();

            ReasonForMigrationForForeign[] reasonForMigrationForForeign = Enum.GetValues<ReasonForMigrationForForeign>();

            ChildrenPlans[] childrenPlans = Enum.GetValues<ChildrenPlans>();


            Random r = new Random();
            int tempPersonCount = personCount;
            int tempPersonsInHousehold = 0;
            Household tempHousehold;
            Person tempPerson;

            while (tempPersonCount > 0)
            {
                tempPersonsInHousehold = r.Next(1, 5 + 1);
                if(tempPersonsInHousehold > tempPersonCount) { tempPersonsInHousehold = tempPersonCount; }
                tempHousehold = new Household();
                tempHousehold.NumberOfMembers = tempPersonsInHousehold.ToString();
                tempHousehold.RoomsCount = r.Next(1, 5 + 1).ToString();
                tempHousehold.PartOfRoom = "1/" + r.Next(1, 5 + 1);
                tempHousehold.HasForeighResidents = r.Next(2 + 1) == 1 ? true : false;
                tempHousehold.HasFarmFacilities = r.Next(2 + 1) == 1 ? true : false;


                tempHousehold.FullAdressInfo.Region = "Область " + r.Next(1, 6 + 1);
                tempHousehold.FullAdressInfo.RegionDistrict = "Район области " + r.Next(1, 117 / 6 + 1);
                switch (r.Next(1 + 1))
                {
                    case 0:
                        tempHousehold.FullAdressInfo.Sity = "Город/ПГТ " + r.Next(1, 203 + 1);
                        tempHousehold.FullAdressInfo.SityDistrict = "Район города " + r.Next(1, 113 + 1);
                        break;
                    case 1:
                        tempHousehold.FullAdressInfo.VillageCouncil = "Сельсовет " + r.Next(1, 1151 + 1);
                        tempHousehold.FullAdressInfo.VillageName = "Сельский населенный пункт " + r.Next(1, 23_200 + 1);
                        break;
                }
                tempHousehold.FullAdressInfo.StreetAvenueOther = "Улица, проспект и др. " + r.Next(1, 1000 + 1);
                switch (r.Next(1 + 1))
                {
                    case 0:
                        tempHousehold.FullAdressInfo.HouseNumber = r.Next(1, 1000 + 1).ToString();
                        break;
                    case 1:
                        tempHousehold.FullAdressInfo.OwnerFIO = "Собственник " + r.Next(1, 1000 + 1);
                        break;
                }

                tempHousehold.FullAdressInfo.KorpNumber = r.Next(1, 500 + 1).ToString();
                tempHousehold.FullAdressInfo.FlatOrRoomNumber = r.Next(1, 200 + 1).ToString();

                tempHousehold.HouseholdAccommodationsInfo.OwnerOfApartment = ownerOfApartment.ElementAt(r.Next(ownerOfApartment.Count()));
                tempHousehold.HouseholdAccommodationsInfo.IsAppartmentRented = r.Next(1,2 + 1) == 1 ? true : false;
                tempHousehold.HouseholdAccommodationsInfo.ApartmentType = apartmentType.ElementAt(r.Next(apartmentType.Count()));
                tempHousehold.HouseholdAccommodationsInfo.AreaOfFlat = r.Next(1, 70 + 1).ToString();
                tempHousehold.HouseholdAccommodationsInfo.WaterPipes = waterPipes.ElementAt(r.Next(waterPipes.Count()));
                tempHousehold.HouseholdAccommodationsInfo.Сanalization = canalization.ElementAt(r.Next(canalization.Count()));
                tempHousehold.HouseholdAccommodationsInfo.HasBathOrShower = r.Next(2 + 1) == 1 ? true : false;
                tempHousehold.HouseholdAccommodationsInfo.HotWaterSupply = hotWaterSupply.ElementAt(r.Next(hotWaterSupply.Count()));
                tempHousehold.HouseholdAccommodationsInfo.Heating = heating.ElementAt(r.Next(heating.Count()));
                tempHousehold.HouseholdAccommodationsInfo.CookingEquipment = cookingEquipment.ElementAt(r.Next(cookingEquipment.Count()));

                tempHousehold.ClearUselessValues();

                for (int i = 0; i < tempPersonsInHousehold; i++)
                {
                    tempPerson = new Person();

                    tempPerson.IsForeign = r.Next(5 + 1) == 1 ? true : false;
                    tempPerson.Name = "Имя " + r.Next(1, 600 + 1);
                    tempPerson.Surname = "Фамилия " + r.Next(1, 6000 + 1);
                    tempPerson.Fathername = "Отчество " + r.Next(1, 600 + 1);
                    tempPerson.BirthdayDate = DateTime.Today.AddDays(-r.Next(366, 25000 + 1));
                    tempPerson.Age = (DateTime.Today.Year - ((DateTime)tempPerson.BirthdayDate).Year).ToString();
                    tempPerson.Gender = gender[r.Next(gender.Length)];
                    tempPerson.PassportID = "ID" + r.Next(1000000, 10000000 + 1);
                    tempPerson.BirthCountry = birthCountry[r.Next(1, birthCountry.Length)];
                    tempPerson.NameOfBirthCountry = "Страна" + r.Next(1, 100 + 1);
                    tempPerson.Citizenship = citizenship[r.Next(citizenship.Length)];
                    tempPerson.NameOfCitizenshipCountry = "Страна" + r.Next(1, 100 + 1);

                    if (tempPerson.IsForeign == true)
                    {
                        tempPerson.HomeCountry = "Страна" + r.Next(1, 100 + 1);
                        tempPerson.ReasonForMigrationForForeign = reasonForMigrationForForeign[r.Next(reasonForMigrationForForeign.Length)];
                    }
                    else
                    {
                        tempPerson.HouseholdRelations = householdRelations[r.Next(householdRelations.Length)];
                        tempPerson.MaritalStatus = maritalStatus[r.Next(maritalStatus.Length)];
                        tempPerson.Nationality = nationality[r.Next(nationality.Length)];
                        tempPerson.NameOfNationality = "Национальность " + r.Next(1, 100 + 1);
                        tempPerson.NativeLanguage = nativeLanguage[r.Next(nativeLanguage.Length)];
                        tempPerson.NameOfNativeLanguage = "Язык " + r.Next(1, 100 + 1);
                        tempPerson.SpeakingLanguage = speakingLanguage[r.Next(speakingLanguage.Length)];
                        tempPerson.NameOfSpeakingLanguage = "Язык " + r.Next(1, 100 + 1);

                        SourceOfResources temp;
                        for (int t = 0; t < r.Next(1, 9 + 1); t++)
                        {
                            temp = sourceOfResources[r.Next(sourceOfResources.Length)];
                            if (!tempPerson.SourceOfResources.Contains(temp) && temp != 0)
                            {
                                tempPerson.SourceOfResources.Add(temp);
                            }
                        }
                        tempPerson.MainSourceOfResources = tempPerson.SourceOfResources[r.Next(tempPerson.SourceOfResources.Count)];

                        tempPerson.LivingPlaceInfo.DoYouLiveHereFromBirth = r.Next(5 + 1) == 1 ? true : false;
                        tempPerson.LivingPlaceInfo.ArrivalPeriod = DateTime.Today.AddDays(-r.Next(365, 50000 + 1));
                        tempPerson.LivingPlaceInfo.PreviousLivingPlace = previousLivingPlace[r.Next(previousLivingPlace.Length)];
                        tempPerson.LivingPlaceInfo.RegionOrDistrictName = "Область/район " + r.Next(1, 123 / 6 + 1);
                        tempPerson.LivingPlaceInfo.CityOrPGTName = "Город/ПГТ " + r.Next(1, 203 + 1);
                        tempPerson.LivingPlaceInfo.IsItVillage = r.Next(5 + 1) == 1 ? true : false;
                        tempPerson.LivingPlaceInfo.NameOfPreviousCountry = "Страна" + r.Next(1, 100 + 1);
                        tempPerson.LivingPlaceInfo.ReasonForArrivalAtPlace = reasonForMigration[r.Next(reasonForMigration.Length)];

                        tempPerson.LivingCountryInfo.DidYouLiveInOtherCountry = r.Next(5 + 1) == 1 ? true : false;
                        tempPerson.LivingCountryInfo.NameOfCountryYouCameFrom = "Страна" + r.Next(1, 100 + 1);
                        tempPerson.LivingCountryInfo.ArrivalPeriod = DateTime.Today.AddDays(-r.Next(365, 50000 + 1));
                        tempPerson.LivingCountryInfo.ReasonForArrivalAtRB = reasonForMigration[r.Next(reasonForMigration.Length)];
                        tempPerson.LivingCountryInfo.DoYouWantToLeaveBelarus = doYouWantToLeaveBelarus[r.Next(1, doYouWantToLeaveBelarus.Length)];
                        tempPerson.LivingCountryInfo.ReasonForLeaveBelarus = reasonForMigration[r.Next(reasonForMigration.Length)];

                        tempPerson.EducationInfo.LvlOfEducation = lvlOfEducation[r.Next(lvlOfEducation.Length)];
                        tempPerson.EducationInfo.AcademicDegree = r.Next(5 + 1) == 1 ? academicDegree[r.Next(1, academicDegree.Length)] : 0;
                        tempPerson.EducationInfo.CanYouReadAndWrite = r.Next(5 + 1) == 1 ? true : false;
                        tempPerson.EducationInfo.GettingBasicEducation = gettingBasicEducation[r.Next(gettingBasicEducation.Length)];
                        tempPerson.EducationInfo.GettingAdditionalEducation = r.Next(5 + 1) == 1 ? true : false;
                        tempPerson.EducationInfo.DoesChildAttendPreschool = r.Next(1 + 1) == 1 ? true : false;

                        tempPerson.WorkInfo.DoYouHaveWork = r.Next(1 + 1) == 1 ? true : false;
                        tempPerson.WorkInfo.WhyYouDontHaveWork = r.Next(1 + 2) == 1 ? true : false;
                        tempPerson.WorkInfo.LocationOfWork = locationOfWork[r.Next(locationOfWork.Length)];
                        tempPerson.WorkInfo.RegionOrDistrictName = "Область/район " + r.Next(1, 123 / 6 + 1);
                        tempPerson.WorkInfo.CityOrPGTName = "Город/ПГТ " + r.Next(1, 203 + 1);
                        tempPerson.WorkInfo.IsItVillage = r.Next(5 + 1) == 1 ? true : false;
                        tempPerson.WorkInfo.NameOfCountry = "Страна" + r.Next(1, 100 + 1);
                        tempPerson.WorkInfo.DepartureFrequencyToWork = departureFrequencyToWork[r.Next(departureFrequencyToWork.Length)];
                        tempPerson.WorkInfo.UnemploymentReason = unemploymentReason[r.Next(unemploymentReason.Length)];
                        tempPerson.WorkInfo.TypeOfWorkplace = typeOfWorkplace[r.Next(typeOfWorkplace.Length)];
                        tempPerson.WorkInfo.TypeOfWorkPosition = typeOfWorkPosition[r.Next(typeOfWorkPosition.Length)];
                        tempPerson.WorkInfo.DidYouLookedForAJob = r.Next(1 + 1) == 1 ? true : false;
                        tempPerson.WorkInfo.CanYouStarWorkingInTwoWeeks = r.Next(1 + 1) == 1 ? true : false;
                        tempPerson.WorkInfo.WhyYouCantWorkOrStopedSearch = whyYouCantWorkOrStopedSearch[r.Next(whyYouCantWorkOrStopedSearch.Length)];

                        tempPerson.ChildrenInfo.HowManyChildrenDidYouHave = r.Next(1, 3 + 1).ToString();
                        tempPerson.ChildrenInfo.NoChildren = r.Next(2 + 1) == 1 ? true : false;
                        tempPerson.ChildrenInfo.ChildrenPlans = childrenPlans[r.Next(childrenPlans.Length)];
                        tempPerson.ChildrenInfo.HowManyChildrenDoYouWant = r.Next(3 + 1).ToString();
                        tempPerson.ChildrenInfo.DontKnowHowMany = r.Next(2 + 1) == 1 ? true : false;
                    }

                    tempPerson.ClearUselessValues();
                    tempHousehold.HouseholdMembers.Add(tempPerson);
                    tempPersonCount--;
                }

                CensusDBUtil.WriteResultsToDatabase(tempHousehold);
            }
        }
    }
}
