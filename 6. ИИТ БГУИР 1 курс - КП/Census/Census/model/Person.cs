using Census.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Census.model
{
    #region enum-ы
    public enum Gender
    {
        Male = 1, Female = 2
    }
    public enum HouseholdRelations//Отношение к лицу, указанному первым в домохозяйстве
    {
        ThisPerson = 1,//лицо, указанное первым в домохозяйстве
        WifeOrHusband = 2,//жена, муж
        DaughterSonStepdaughterStepson = 3,//дочь, сын, падчерица, пасынок
        MotherOrFather = 4,//мать, отец
        SisterOrBrother = 5,//сестра, брат
        InLawRelatives = 6,//свекровь, свекор, теща, тесть
        DaughterInLawOrSonInLaw = 7,//невестка, зять
        GrandmotherOrGrandfather = 8,//бабка, дед
        GranddaughterOrGrandson = 9,//внучка, внук
        Other = 10,//другая степень родства, свойства
        NotRelative = 11,//другая степень родства, свойства
    }

    public enum MaritalStatus//Ваше семейное положение
    {
        NeverMarried = 1,//никогда не состоял(а) в браке
        MarriedNow = 2,//состою в браке
        UnregisteredRelationship = 3,//состою в незарегистрированных отношениях
        WidowerWidow = 4,//вдовец, вдова
        Divorced = 5,//разведен(а)
        BrokeUp = 6,//разошелся(лась)
    }

    public enum BirthCountry //Место Вашего рождения
    {
        RepublicOfBelarus = 1,//Республика Беларусь
        OtherCountry = 2,//другая страна
    }

    public enum PreviousLivingPlace //предыдущее место жительства
    {
        RepublicOfBelarus = 1,//Республика Беларусь
        OtherCountry = 2,//другая страна
    }

    public enum Citizenship//гражданство
    {
        RepublicOfBelarus = 1,//Республика Беларусь
        OtherCountry = 2,//другая страна
        None = 3,//без гражданства
    }

    public enum Nationality//национальность/этническая принадлежность
    {
        Belarusian = 1,//белорус(ка)
        Russian = 2,//русский(ая)
        Pole = 3,//поляк(полька)
        Ukrainian = 4,//украинец(ка)
        None = 5,//не сообщил(а)
        Other = 6,//другая
    }

    public enum NativeLanguage//Ваш родной язык (язык, усвоенный первым в раннем детстве)
    {
        Belarusian = 1,//белорусский
        Russian = 2,//русский
        Polish = 3,//польский
        Ukrainian = 4,//украинский
        Other = 5,//другой
    }

    public enum SpeakingLanguage//На каком языке Вы обычно разговариваете дома
    {
        Belarusian = 1,//белорусский
        Russian = 2,//русский
        Polish = 3,//польский
        Ukrainian = 4,//украинский
        Other = 5,//другой
    }

    public enum SourceOfResources//Укажите источники средств к существованию
    {
        Work = 1,                   //работа по найму (кроме работы в личном подсобном хозяйстве)
        SelfEmployment = 2,         //самозанятость (работа не по найму)
        HouseholdWork = 201,         //работа в личном подсобном хозяйстве
        GoodsProduction = 3,        //производство товаров для собственного использования
        Pension = 4,                //пенсия
        Scholarship = 5,            //стипендия
        UnemploymentBenefit = 6,    //пособие по безработице
        GovernmentBenefits = 7,     //государственные пособия (кроме пособия по безработице)
        OtherBenefits = 8,          //иные виды пособий и помощи
        AssetsActivity = 9,         //доходы от сдачи в аренду имущества, дивиденды либо другие выплаты по вкладам и ценным бумагам
        SelfSources = 10,           //ссуды или использование сбережений, реализация имущества
        Dependent = 11,             //на иждивении другого лица
        Other = 12,                 //прочие источники

    }

    #region Для работы

    public enum LocationOfWork//Где находилось место Вашей работы
    {
        WhereYouLive = 1,//в населенном пункте по месту жительства
        AnotherLocality = 2,//в другом населенном пункте Республики Беларусь
        AnotherCountry = 3,//в другой стране
    }
    public enum DepartureFrequencyToWork//Как часто Вы выезжали на работу на территорию другого государства?
    {
        Daily = 1,//ежедневно
        FewDays = 2,//на несколько дней в течение недели
        Other = 3,//на иной период
    }
    public enum UnemploymentReason//Укажите основную причину, по которой Вы не работаете в населенном пункте по месту жительства
    {
        DidntFindHere = 1,//не нашел(нашла) работу в данном населенном пункте
        FindMoreWellPaid = 2,//нашел(нашла) работу с более высоким заработком
        Other = 3,//другие причины
    }
    public enum TypeOfWorkplace// Укажите, где осуществлялась Ваша работа
    {
        Organization = 1,//в организации (структурном подразделении организации)
        FarmHousehold = 101,//в крестьянском (фермерском) хозяйстве
        Agroecotourism = 2,//в сфере агроэкотуризма
        WithSoleTrader = 3,//у индивидуального предпринимателя или физического лица, производящего товары на продажу или оказывающего платные услуги
        HouseholdEmployee = 4,//в домашнем хозяйстве наемным работником
        SoleTrader = 5,//в качестве индивидуального предпринимателя
        UnregisteredSoleTrader = 6,//на индивидуальной основе без регистрации в качестве индивидуального предпринимателя
        Craftsman = 601,//в качестве ремесленника
        SelfHousehold = 7,//в личном подсобном хозяйстве, по производству (переработке) продукции сельского, лесного хозяйства, охоты и рыболовства, предназначенной для реализации
        Other = 8,//другое
    }
    public enum TypeOfWorkPosition// Кем Вы являлись на работе (указать только один вариант ответа)
    {
        Employee = 1,//наемным работником
        SelfEmployed = 2,//самозанятым (работающим не по найму)
        Employer = 201,//работодателем (в том числе собственником, учредителем)
        IndividualPerson = 202,//лицом, работающем на индивидуальной основе
        RelativeWorker = 203,//работающим (помогающим в работе) без оплаты труда у родственника (члена домохозяйства)
        WithoutClassification = 3//лицом, не поддающимся классификации по статусу
    }
    public enum WhyYouCantWorkOrStopedSearch//Укажите основную причину, по которой Вы отказались от поиска работы (не готовы приступить к работе)
    {
        GetAnotherJob = 1,//получил(а) работу, организовал(а) собственное дело и приступлю к ней в ближайшие две недели
        FindAnotherJob = 2,//нашел (нашла) работу и ожидаю ответа
        WaitingForSeasonStart = 3,//ожидаю начала сезона
        LostHope = 4,//потерял(а) надежду найти работу
        DontHaveOpportunity = 5,//нет возможности найти работу
        DontKnowWhereToLook = 6,//не знаю, где и как искать работу
        Retired = 7,//вышел (вышла) на пенсию, в отставку
        BecauseOfHealth = 8,//по состоянию здоровья
        StudyingNow = 9,//обучаюсь в учреждении образования в дневной форме получения образования
        BecauseOfHousework = 10,//веду домашнее хозяйство, ухаживаю за детьми, другими членами семьи
        DontNeedOrWant = 11,//нет необходимости или желания работать
        Other = 12,//другая причина
    }

    #endregion

    #region Для образования

    public enum LvlOfEducation//Ваш уровень (ступень) образования (для лиц в возрасте 10 лет и старше)
    {
        Basic = 1,//начальное
        GeneralBasic = 2,//общее базовое
        GeneralAverage = 3,//общее среднее
        Vocational = 4,//профессионально-техническое
        MiddleSpecialized = 5,//среднее специальное
        Higher = 6,//высшее
        Magistracy = 61,//магистратура
        Postgraduate = 7,//послевузовское
        AdjunctureOrTraineeship = 701,//аспирантура, адъюнктура
        Doctoral = 702,//докторантура
        None = 8//не имею
    }
    public enum AcademicDegree//Ученая степень
    {
        ScienceCandidate = 1,//кандидат наук
        ScienceDoctor = 2,//общее базовое
        None = 3//не имею
    }
    public enum GettingBasicEducation //Получали ли Вы образование на дату проведения переписи (ТУТ только базовое)?
    {
        Basic = 1,
        GeneralAverage = 101,// общее среднее
        Vocational = 102,// профессионально-техническое
        MiddleSpecialized = 103,// среднее специальное
        Higher = 104,//высшее
        Postgraduate = 105,//послевузовское
        No = 2//нет (не получал)
    }

    #endregion

    #region Для информации о предыдущих местах проживания

    public enum ReasonForMigration//Укажите основную причину, по которой Вы прибыли в данный населенный пункт/в Республику Беларусь на постоянное место жительства
    {
        Work = 1,//работа
        Aducation = 2,//получение образования
        FamilyConditions = 3,//семейные обстоятельства
        MedicalPurposes = 4,//медицинское лечение, реабилитация
        ReturnToHomeLivingPlace = 5,//возвращение на прежнее место жительства
        ChangeLivingConditions = 6,//изменение жилищных условий
        Shelter = 7,//поиск убежища
        OtherReason = 8,//другая цель
        None = 9,//причина не указана
    }

    public enum DoYouWantToLeaveBelarus//Планируете ли Вы выехать из Республики Беларусь
    {
        Yes = 1,//да
        LessThanYear = 101,//на срок менее 1 года
        MoreThanYear = 102,//на срок более 1 года
        ForConstantLiving = 103,//на постоянное место жительства
        No = 2,//нет
    }

    #endregion

    #region для иностранцев
    public enum ReasonForMigrationForForeign//Укажите основную причину приезда в Республику Беларусь
    {
        Work = 1,//работа
        Aducation = 2,//получение образования
        OfficeOrBusinessMeeting = 3,//служебная или деловая встреча
        VisitingRelatives = 4,//посещение родственников
        TourismOrRecreationOrHealing = 5,//туризм, отдых или лечение
        Transit = 6,//транзит
        Shelter = 7,//поиск убежища
        OtherReason = 8,//другая цель
    }

    #endregion

    #region Для детей

    public enum ChildrenPlans//Планируете ли Вы рождение детей? (для женщин в возрасте 18-49 лет)
    {
        Yes = 1,//да
        No = 2,//нет
        XZ = 3,//затрудняюсь ответить
    }

    #endregion

    #endregion

    public class Person
    {
        public Person()
        {
            LivingPlaceInfo = new LivingPlaceInfo();
            LivingCountryInfo = new LivingCountryInfo();
            EducationInfo = new EducationInfo();
            SourceOfResources = new List<SourceOfResources>();
            WorkInfo = new WorkInfo();
            ChildrenInfo = new ChildrenInfo();
        }
        private static bool FailMessage(string message)
        {
            MessageBox.Show(message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Hand);
            return false;
        }
        public bool ValidateForeign()
        {
            int parseTemp;

            if (Surname.Equals("")) { return FailMessage("Не указана фамилия."); }
            if (Name.Equals("")) { return FailMessage("Не указано имя."); }
            if (BirthdayDate == null) { return FailMessage("Не указана дата рождения."); }
            if (Age.Equals("")) { return FailMessage("Не указано число полных лет."); }
            else { if (int.TryParse(Age, out parseTemp) == false) return FailMessage("число полных лет должно быть числом(цифрой)."); }
            if (Gender == 0) { return FailMessage("Не указан пол."); }
            if (BirthCountry == 0) { return FailMessage("Не указана информация о стране рождения."); }
            else { if((int)BirthCountry == 2 & NameOfBirthCountry.Equals(""))  return FailMessage("Не указано название страны рождения."); }
            if (Citizenship == 0) { return FailMessage("Не указана информация о гражданстве."); }
            else
            {
                if ((int)Citizenship == 1 & PassportID.Equals("")) { return FailMessage("Не указан идентификационный номер."); }
                if ((int)Citizenship == 3 & NameOfCitizenshipCountry.Equals("")) { return FailMessage("Не указана информация о гражданстве (страна)."); }
            }
            if (HomeCountry.Equals("")) { return FailMessage("Не указана страна постоянного проживания."); }
            if (ReasonForMigrationForForeign == 0) { return FailMessage("Не указана причина приезда в РБ."); }
            return true;
        }
        public bool ValidateOne()
        {
            int parseTemp;

            if (Surname.Equals("")) { return FailMessage("Не указана фамилия."); }
            if (Name.Equals("")) { return FailMessage("Не указано имя."); }
            if (PassportID.Equals("")) { return FailMessage("Не указан идентификационный номер."); }
            if (BirthdayDate == null) { return FailMessage("Не указана дата рождения."); }
            if (Age.Equals("")) { return FailMessage("Не указано число полных лет."); }
            else { if (int.TryParse(Age, out parseTemp) == false) return FailMessage("число полных лет должно быть числом(цифрой)."); }
            if (Gender == 0) { return FailMessage("Не указан пол."); }
            if (HouseholdRelations == 0) { return FailMessage("Не указана информация об отношении к лицу, первому в домохозяйстве."); }
            if (int.Parse(Age) > 15 & MaritalStatus == 0) { return FailMessage("Не указана информация о семейном положении."); }

            if (BirthCountry == 0) { return FailMessage("Не указана информация о месте рождения."); }
            else { if ((int)BirthCountry == 2 & NameOfBirthCountry.Equals("")) return FailMessage("Не указано название страны рождения."); }

            if (LivingPlaceInfo.DoYouLiveHereFromBirth == null) { return FailMessage("Не указана информация о месте проживания."); }
            else
            {
                if(LivingPlaceInfo.DoYouLiveHereFromBirth == false)
                {
                    if (LivingPlaceInfo.ArrivalPeriod == null) { return FailMessage("Не указана дата прибытия."); }
                    if (LivingPlaceInfo.PreviousLivingPlace == 0) { return FailMessage("Не указана информация о предыдущем месте жительства."); }
                    else
                    {
                        if ((int)LivingPlaceInfo.PreviousLivingPlace == 1)
                        {
                            if (LivingPlaceInfo.RegionOrDistrictName.Equals("")) { return FailMessage("Не указано название области/района предыдущего места жительства."); }
                            if (LivingPlaceInfo.IsItVillage == false && LivingPlaceInfo.CityOrPGTName.Equals("")) { return FailMessage("Не указано место проживания (город/ПГТ/деревня)."); }
                            if (LivingPlaceInfo.IsItVillage == true && !LivingPlaceInfo.CityOrPGTName.Equals("")) { return FailMessage("Должно быть указано только одно место проживания (города/ПГТ или деревня)."); }
                        }
                        if ((int)LivingPlaceInfo.PreviousLivingPlace == 2)
                        {
                            if (LivingPlaceInfo.NameOfPreviousCountry.Equals("")) { return FailMessage("Не указано название страны предыдущего места жительства."); }
                        }
                    }
                    if (LivingPlaceInfo.ReasonForArrivalAtPlace == 0) { return FailMessage("Не указана причина прибытия."); }
                }
            }

            return true;
        }
        public bool ValidateTwo()
        {
            if (LivingPlaceInfo.DoYouLiveHereFromBirth == false)
            {
                if (LivingCountryInfo.DidYouLiveInOtherCountry == null) { return FailMessage("Не указана информация о сроке проживания в другой стране."); }
                if (LivingCountryInfo.DidYouLiveInOtherCountry == true)
                {
                    if (LivingCountryInfo.NameOfCountryYouCameFrom.Equals("")) { return FailMessage("Не указана информация о стране, из которой вы прибыли в РБ на ПМЖ."); }
                    if (LivingCountryInfo.ArrivalPeriod == null) { return FailMessage("Не указан период прибытия."); }
                    if (LivingCountryInfo.ReasonForArrivalAtRB == 0) { return FailMessage("Не указана причина прибытия."); }
                }
            }
            if (LivingCountryInfo.DoYouWantToLeaveBelarus == 0 && int.Parse(Age) >= 15 && int.Parse(Age) < 74) { return FailMessage("Не указана информация о желании покинуть РБ."); }
            if ((int)LivingCountryInfo.DoYouWantToLeaveBelarus != 2 && (int)LivingCountryInfo.ReasonForLeaveBelarus == 0)
            {
                return FailMessage("Не указана причина выезда из РБ.");
            }

            if (Citizenship == 0) { return FailMessage("Не указана информация о гражданстве."); }
            if ((int)Citizenship == 2 && NameOfCitizenshipCountry.Equals(""))
            {
                return FailMessage("Не указана информация о гражданстве (название страны).");
            }
            if (Nationality == 0) { return FailMessage("Не указана информация о национальности."); }
            if ((int)Nationality == 6 && NameOfNationality.Equals(""))
            {
                return FailMessage("Не указана информация о национальности (название).");
            }
            if (NativeLanguage == 0) { return FailMessage("Не указана информация о родном языке."); }
            if ((int)NativeLanguage == 5 && NameOfNativeLanguage.Equals(""))
            {
                return FailMessage("Не указана информация о родном языке (название).");
            }
            if (SpeakingLanguage == 0) { return FailMessage("Не указана информация о разговорном языке."); }
            if ((int)SpeakingLanguage == 5 && NameOfSpeakingLanguage.Equals(""))
            {
                return FailMessage("Не указана информация о разговорном языке (название).");
            }
            if (EducationInfo.LvlOfEducation == 0 && int.Parse(Age) >= 10) { return FailMessage("Не указана информация об образовании."); }
            if ((int)EducationInfo.LvlOfEducation == 8 && EducationInfo.CanYouReadAndWrite == null) 
            {
                return FailMessage("Не указана информация об умении читать/писать.");
            }
            return true;
        }
        public bool ValidateThree()
        {
            if (int.Parse(Age) > 6 && EducationInfo.GettingBasicEducation == 0) { return FailMessage("Не указана информация о получении основного образования."); }
            if (int.Parse(Age) > 14 && int.Parse(Age) < 66 && EducationInfo.GettingAdditionalEducation == null) { return FailMessage("Не указана информация о получении дополнительного образования."); }
            if (int.Parse(Age) < 8 && EducationInfo.DoesChildAttendPreschool == null) { return FailMessage("Не указана информация о получении дошкольного образования."); }
            if (SourceOfResources.Count == 0) { return FailMessage("Не указаны источники средств к существованию."); }
            if (MainSourceOfResources == 0) { return FailMessage("Не указан основной источник средств к существованию."); }
            if (((int)MainSourceOfResources > 12 && (int)MainSourceOfResources != 21) || !SourceOfResources.Contains(MainSourceOfResources)) { return FailMessage("Некорректно указан основной источник средств к существованию."); }
            if (int.Parse(Age) > 15 && int.Parse(Age) < 75)
            {
                if (WorkInfo.DoYouHaveWork == null) { return FailMessage("Не указана информация о наличии работы."); }
                if (WorkInfo.DoYouHaveWork == false)
                {
                    if (WorkInfo.WhyYouDontHaveWork == null) { return FailMessage("Не указана причина отсутствия работы."); }
                }
                if (WorkInfo.DoYouHaveWork == true || WorkInfo.WhyYouDontHaveWork == true)
                {
                    switch ((int)WorkInfo.LocationOfWork)
                    {
                        case 0:
                            return FailMessage("Не указана информация о месте работы.");
                        case 1:
                            return true;
                        case 2:
                            if (WorkInfo.RegionOrDistrictName.Equals("")) { return FailMessage("Не указано название области/района, где находилось место вашей работы."); }
                            if (WorkInfo.CityOrPGTName.Equals("") && WorkInfo.IsItVillage == null) { return FailMessage("Не указана информация о месте работы (город/ПГТ/деревня)."); }
                            if (!WorkInfo.CityOrPGTName.Equals("") && WorkInfo.IsItVillage == true) { return FailMessage("Некорректно указана информация о месте работы (город/ПГТ ИЛИ деревня)."); }
                            break;
                        case 3:
                            if (WorkInfo.NameOfCountry.Equals("")) { return FailMessage("Не указано название страны, где находилось место вашей работы."); }
                            if (WorkInfo.DepartureFrequencyToWork == 0) { return FailMessage("Не указана информация о частоте выездов на территорию другого гос-ва для работы."); }
                            break;
                    }
                    if (WorkInfo.UnemploymentReason == 0) { return FailMessage("Не указана причина, по которой вы не работаете в населенном пункте ПМЖ."); }
                }
            }
            return true;
        }
        public bool ValidateFour()
        {
            int parseTemp;

            if (WorkInfo.WhyYouDontHaveWork != false)
            {
                if (WorkInfo.TypeOfWorkplace == 0) { return FailMessage("Не указана информация о месте вашей работы."); }
                if (WorkInfo.TypeOfWorkPosition == 0) { return FailMessage("Не указана информация о должности на работы."); }
            }
            else
            {
                if (WorkInfo.DidYouLookedForAJob == null) { return FailMessage("Не указана информация о поиске работы."); }
                if (WorkInfo.CanYouStarWorkingInTwoWeeks == null) { return FailMessage("Не указана информация о возможности начать работать."); }
                if (!(WorkInfo.DidYouLookedForAJob == WorkInfo.CanYouStarWorkingInTwoWeeks == true) || WorkInfo.CanYouStarWorkingInTwoWeeks == false)
                {
                    if (WorkInfo.WhyYouCantWorkOrStopedSearch == 0) { return FailMessage("Не указана информация о причине отказа от трудоустройства."); }
                }
            }
            if ((int)Gender == 2)
            {
                if (int.Parse(Age) >= 15)
                {
                    if (ChildrenInfo.HowManyChildrenDidYouHave.Equals("") && ChildrenInfo.NoChildren == false) { return FailMessage("Не указана информация о количестве имеющихся детей."); }
                    if (!ChildrenInfo.HowManyChildrenDidYouHave.Equals("") && ChildrenInfo.NoChildren == true) { return FailMessage("Некорректно указана информация о количестве имеющихся детей."); }
                    if (!ChildrenInfo.HowManyChildrenDidYouHave.Equals("") && int.TryParse(ChildrenInfo.HowManyChildrenDidYouHave, out parseTemp) == false) { return FailMessage("Количество имеющихся детей должно быть числом/цифрой."); }
                }
                if (int.Parse(Age) >= 18 && int.Parse(Age) <= 49)
                {
                    if (ChildrenInfo.ChildrenPlans == 0) { return FailMessage("Не указана информация о планах на рождение детей."); }
                    if ((int)ChildrenInfo.ChildrenPlans == 1)
                    {
                        if (ChildrenInfo.HowManyChildrenDoYouWant.Equals("") && ChildrenInfo.DontKnowHowMany == false) { return FailMessage("Не указана информация о количестве желаемых детей."); }
                        if (!ChildrenInfo.HowManyChildrenDoYouWant.Equals("") && ChildrenInfo.DontKnowHowMany == true) { return FailMessage("Некорректно указана информация о количестве желаемых детей."); }
                        if (int.TryParse(ChildrenInfo.HowManyChildrenDoYouWant, out parseTemp) == false) { return FailMessage("Количество желаемых детей должно быть числом/цифрой."); }
                    }
                }
            }
            
            return true;
        }
        public void ClearUselessValues()
        {
            #region Foreign
            if (IsForeign == false)
            {
                HomeCountry = null;
                ReasonForMigrationForForeign = 0;
            }
            #endregion
            #region BlankBase1 и BlankBase2
            if (int.Parse(Age) < 15)
            {
                MaritalStatus = 0;
            }
            if ((int)BirthCountry == 1)
            {
                NameOfBirthCountry = null;
            }

            if (LivingPlaceInfo.DoYouLiveHereFromBirth == true)
            {
                LivingPlaceInfo.ArrivalPeriod = null;
                LivingPlaceInfo.PreviousLivingPlace = 0;
                LivingPlaceInfo.ReasonForArrivalAtPlace = 0;
                LivingPlaceInfo.RegionOrDistrictName = null;
                LivingPlaceInfo.CityOrPGTName = null;
                LivingPlaceInfo.IsItVillage = null;
                LivingCountryInfo.DidYouLiveInOtherCountry = null;
                LivingCountryInfo.NameOfCountryYouCameFrom = null;
                LivingCountryInfo.ArrivalPeriod = null;
                LivingCountryInfo.ReasonForArrivalAtRB = 0;
            }
            else
            {
                switch ((int)LivingPlaceInfo.PreviousLivingPlace)
                {
                    case 1:
                        LivingPlaceInfo.NameOfPreviousCountry = null;
                        if (LivingCountryInfo.DidYouLiveInOtherCountry == false)
                        {
                            LivingCountryInfo.NameOfCountryYouCameFrom = null;
                            LivingCountryInfo.ArrivalPeriod = null;
                            LivingCountryInfo.ReasonForArrivalAtRB = 0;
                        }
                        break;
                    case 2:
                        LivingPlaceInfo.RegionOrDistrictName = null;
                        LivingPlaceInfo.CityOrPGTName = null;
                        LivingPlaceInfo.IsItVillage = null;
                        LivingCountryInfo.DidYouLiveInOtherCountry = null;
                        LivingCountryInfo.NameOfCountryYouCameFrom = null;
                        LivingCountryInfo.ArrivalPeriod = null;
                        LivingCountryInfo.ReasonForArrivalAtRB = 0;
                        break;
                }
            }

            if (!(int.Parse(Age) >= 15 & int.Parse(Age) < 74))
            {
                LivingCountryInfo.ReasonForLeaveBelarus = 0;
            }

            if ((int)Citizenship != 2) { NameOfCitizenshipCountry = null; }
            if ((int)Nationality != 6) { NameOfNationality = null; }
            if ((int)NativeLanguage != 5) { NameOfNativeLanguage = null; }
            if ((int)SpeakingLanguage != 5) { NameOfSpeakingLanguage = null; }

            if (int.Parse(Age) < 10)
            {
                EducationInfo.LvlOfEducation = 0;
                EducationInfo.AcademicDegree = 0;
                EducationInfo.CanYouReadAndWrite = null;
            }
            else
            {
                switch ((int)EducationInfo.LvlOfEducation)
                {
                    case 8:
                        EducationInfo.AcademicDegree = 0;
                        break;
                    default:
                        EducationInfo.CanYouReadAndWrite = null;
                        break;
                }
            }
            #endregion
            #region BlankBase3 и BlankBase4
            if (int.Parse(Age) < 6) { EducationInfo.GettingBasicEducation = 0; }

            if (!(int.Parse(Age) >= 15 && int.Parse(Age) <= 65)) { EducationInfo.GettingAdditionalEducation = null; }

            if (int.Parse(Age) > 7) { EducationInfo.DoesChildAttendPreschool = null; }

            if (int.Parse(Age) < 15 || int.Parse(Age) > 74)
            {
                WorkInfo = null;
            }
            else
            {
                switch (WorkInfo.DoYouHaveWork)
                {
                    case true:
                        WorkInfo.WhyYouDontHaveWork = null;
                        WorkInfo.DidYouLookedForAJob = null;
                        WorkInfo.CanYouStarWorkingInTwoWeeks = null;
                        break;
                    case false:
                        if (WorkInfo.WhyYouDontHaveWork == false)
                        {
                            WorkInfo.LocationOfWork = 0;
                            WorkInfo.RegionOrDistrictName = null;
                            WorkInfo.CityOrPGTName = null;
                            WorkInfo.IsItVillage = null;
                            WorkInfo.NameOfCountry = null;
                            WorkInfo.DepartureFrequencyToWork = 0;
                            WorkInfo.UnemploymentReason = 0;
                            WorkInfo.TypeOfWorkplace = 0;
                            WorkInfo.TypeOfWorkPosition = 0;
                            break;
                        }
                        WorkInfo.DidYouLookedForAJob = null;
                        WorkInfo.CanYouStarWorkingInTwoWeeks = null;
                        break;
                }

                switch ((int)WorkInfo.LocationOfWork)
                {
                    case 1:
                        WorkInfo.RegionOrDistrictName = null;
                        WorkInfo.CityOrPGTName = null;
                        WorkInfo.IsItVillage = null;
                        WorkInfo.NameOfCountry = null;
                        WorkInfo.DepartureFrequencyToWork = 0;
                        WorkInfo.UnemploymentReason = 0;
                        break;
                    case 2:
                        WorkInfo.NameOfCountry = null;
                        WorkInfo.DepartureFrequencyToWork = 0;
                        break;
                    case 3:
                        WorkInfo.RegionOrDistrictName = null;
                        WorkInfo.CityOrPGTName = null;
                        WorkInfo.IsItVillage = null;
                        break;
                }
                if (WorkInfo.DidYouLookedForAJob == WorkInfo.CanYouStarWorkingInTwoWeeks == true)
                {
                    WorkInfo.WhyYouCantWorkOrStopedSearch = 0;
                }
            }
            if ((int)Gender == 1)
            {
                ChildrenInfo.HowManyChildrenDidYouHave = null;
                ChildrenInfo.NoChildren = null;
                ChildrenInfo.ChildrenPlans = 0;
                ChildrenInfo.HowManyChildrenDoYouWant = null;
                ChildrenInfo.DontKnowHowMany = null;
            }
            else
            {
                if (int.Parse(Age) < 15)
                {
                    ChildrenInfo.HowManyChildrenDidYouHave = null;
                    ChildrenInfo.NoChildren = null;
                }
                else
                {
                    if (ChildrenInfo.NoChildren == true) { ChildrenInfo.HowManyChildrenDidYouHave = null; }
                }
                if (!(int.Parse(Age) >= 18 && int.Parse(Age) <= 49))
                {
                    ChildrenInfo.ChildrenPlans = 0;
                    ChildrenInfo.HowManyChildrenDoYouWant = null;
                    ChildrenInfo.DontKnowHowMany = null;
                }
                else 
                {
                    if ((int)ChildrenInfo.ChildrenPlans != 1)
                    {
                        ChildrenInfo.HowManyChildrenDoYouWant = null;
                        ChildrenInfo.DontKnowHowMany = null;
                    }
                    else
                    {
                        if(ChildrenInfo.DontKnowHowMany == true) { ChildrenInfo.HowManyChildrenDoYouWant = null; }
                    } 
                }
            }
            #endregion
        }

        //==================================\
        public bool? IsForeign { get; set; } = null;//ПЕРЕПИСНОЙ ЛИСТ НА ВРЕМЕННО ПРОЖИВАЮЩИХ (ПРЕБЫВАЮЩИХ) В РЕСПУБЛИКЕ БЕЛАРУСЬ
        public string HomeCountry { get; set; } = "";//Страна постоянного проживания
        public ReasonForMigrationForForeign ReasonForMigrationForForeign { get; set; }//Укажите основную причину приезда в Республику Беларусь
        //==================================/

        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Fathername { get; set; } = "";

        public string PassportID { get; set; } = "";
        public DateTime? BirthdayDate { get; set; } = null;
        public string Age { get; set; }
        public Gender Gender { get; set; }

        public HouseholdRelations HouseholdRelations { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        //==============================\
        public BirthCountry BirthCountry { get; set; }
        public string NameOfBirthCountry { get; set; } = "";
        //==============================/
        public LivingPlaceInfo LivingPlaceInfo { get; set; }
        public LivingCountryInfo LivingCountryInfo { get; set; }

        //==============================\
        public Citizenship Citizenship { get; set; }
        public string NameOfCitizenshipCountry { get; set; } = "";
        //==============================/

        //==============================\
        public Nationality Nationality { get; set; }
        public string NameOfNationality { get; set; } = "";
        //==============================/

        //==============================\
        public NativeLanguage NativeLanguage { get; set; }/* родной язык (язык, усвоенный первым в раннем детстве)*/
        public string NameOfNativeLanguage { get; set; } = "";
        //==============================/

        //==============================\
        public SpeakingLanguage SpeakingLanguage { get; set; }/* родной язык (язык, усвоенный первым в раннем детстве)*/
        public string NameOfSpeakingLanguage { get; set; } = "";
        //==============================/

        public EducationInfo EducationInfo { get; set; }/**/

        public List<SourceOfResources> SourceOfResources { get; set; }/*Укажите источники средств к существованию*/
        public SourceOfResources MainSourceOfResources { get; set; }/*основной источник*/
        public WorkInfo WorkInfo { get; set; }/**/
        public ChildrenInfo ChildrenInfo { get; set; }/**/

        public string GetFieldsForBD()
        {
            string stringSourceOfResources = "";
            foreach (var item in SourceOfResources)
            {
                stringSourceOfResources += (int)item + " ";
            }
            return
                DBUtil.ConvertToSqlType(IsForeign) + ", " +
                DBUtil.ConvertToSqlType(HomeCountry) + ", " +
                DBUtil.ConvertToSqlType(ReasonForMigrationForForeign) + ", " +
                DBUtil.ConvertToSqlType(Name) + ", " +
                DBUtil.ConvertToSqlType(Surname) + ", " +
                DBUtil.ConvertToSqlType(Fathername) + ", " +
                DBUtil.ConvertToSqlType(PassportID) + ", " +
                DBUtil.ConvertToSqlType(((DateTime)BirthdayDate).ToString("dd.MM.yyyy")) + ", " +
                DBUtil.ConvertToSqlType(Age) + ", " +
                DBUtil.ConvertToSqlType(Gender) + ", " +
                DBUtil.ConvertToSqlType(HouseholdRelations) + ", " +
                DBUtil.ConvertToSqlType(MaritalStatus) + ", " +
                DBUtil.ConvertToSqlType(BirthCountry) + ", " +
                DBUtil.ConvertToSqlType(NameOfBirthCountry) + ", " +
                DBUtil.ConvertToSqlType(Citizenship) + ", " +
                DBUtil.ConvertToSqlType(NameOfCitizenshipCountry) + ", " +
                DBUtil.ConvertToSqlType(Nationality) + ", " +
                DBUtil.ConvertToSqlType(NameOfNationality) + ", " +
                DBUtil.ConvertToSqlType(NativeLanguage) + ", " +
                DBUtil.ConvertToSqlType(NameOfNativeLanguage) + ", " +
                DBUtil.ConvertToSqlType(SpeakingLanguage) + ", " +
                DBUtil.ConvertToSqlType(NameOfSpeakingLanguage) + ", " +
                DBUtil.ConvertToSqlType(stringSourceOfResources) + ", " +
                DBUtil.ConvertToSqlType(MainSourceOfResources);
        }
    }

    public class LivingPlaceInfo
    {
        public bool? DoYouLiveHereFromBirth { get; set; } = null;// 1  В этом городе, поселке городского типа или сельском населенном пункте этого административного района Вы проживаете непрерывно с рождения?
        //=====================Если=нет=====================\
        public DateTime? ArrivalPeriod { get; set; } = null;
        public PreviousLivingPlace PreviousLivingPlace { get; set; }//Укажите Ваше предыдущее место жительства
        ///---------1----------\
        public string RegionOrDistrictName { get; set; } = "";// наименования области, района
        public string CityOrPGTName { get; set; } = "";// наименования города, поселка городского типа
        public bool? IsItVillage { get; set; }// сельский населенный пункт
        ///---------2----------
        public string NameOfPreviousCountry { get; set; } = "";// название страны
        ///--------------------/
        public ReasonForMigration ReasonForArrivalAtPlace { get; set; }//Укажите основную причину, по которой Вы прибыли в данный населенный пункт
        //==================================================/
        //если да, то это не здесь
        public string GetFieldsForBD()
        {
            return
                DBUtil.ConvertToSqlType(DoYouLiveHereFromBirth) + ", " +
                DBUtil.ConvertToSqlType(ArrivalPeriod == null ? ArrivalPeriod : ((DateTime)ArrivalPeriod).ToShortDateString()) + ", " +
                DBUtil.ConvertToSqlType(PreviousLivingPlace) + ", " +
                DBUtil.ConvertToSqlType(RegionOrDistrictName) + ", " +
                DBUtil.ConvertToSqlType(CityOrPGTName) + ", " +
                DBUtil.ConvertToSqlType(IsItVillage) + ", " +
                DBUtil.ConvertToSqlType(NameOfPreviousCountry) + ", " +
                DBUtil.ConvertToSqlType(ReasonForArrivalAtPlace);
        }
    }

    public class LivingCountryInfo
    {
        public bool? DidYouLiveInOtherCountry { get; set; } = null;// Проживали ли Вы непрерывно 1 год и более в другой стране?
        //=====================Если=да=====================\
        public string NameOfCountryYouCameFrom { get; set; } = "";// Из какой страны Вы прибыли в Республику Беларусь на постоянное место жительства
        public DateTime? ArrivalPeriod { get; set; } = null;// период прибытия
        public ReasonForMigration ReasonForArrivalAtRB { get; set; }/* причина, по которой Вы прибыли в Республику Беларусь на постоянное место жительства*/
        //=================================================/

        /*Сюда из формы LivingPlaceInfo в случае положительного ответа на "DoYouLiveHereFromBirth" или на выбор другой страны в "PreviousLivingPlace"  */
        public DoYouWantToLeaveBelarus DoYouWantToLeaveBelarus { get; set; }// Планируете ли Вы выехать из Республики Беларусь
        //=====================Если=да=====================\
        public ReasonForMigration ReasonForLeaveBelarus { get; set; }/*По какой причине Вы планируете выехать из Республики Беларусь*/
        //=================================================/

        public string GetFieldsForBD()
        {
            return
                DBUtil.ConvertToSqlType(DidYouLiveInOtherCountry) + ", " +
                DBUtil.ConvertToSqlType(NameOfCountryYouCameFrom) + ", " +
                DBUtil.ConvertToSqlType(ArrivalPeriod == null ? ArrivalPeriod : ((DateTime)ArrivalPeriod).ToShortDateString()) + ", " +
                DBUtil.ConvertToSqlType(ReasonForArrivalAtRB) + ", " +
                DBUtil.ConvertToSqlType(DoYouWantToLeaveBelarus) + ", " +
                DBUtil.ConvertToSqlType(ReasonForLeaveBelarus);
        }
    }

    public class EducationInfo
    {
        public LvlOfEducation LvlOfEducation { get; set; }//Ваш уровень (ступень) образования (для лиц в возрасте 10 лет и старше)
        //=====================Если=крутой=================\
        public AcademicDegree AcademicDegree { get; set; }//Имеете ли ученую степень?
        //=================================================/

        //=====================Если=тупой==================\
        public bool? CanYouReadAndWrite { get; set; } = null;/*Умеете ли Вы читать и писать?*/
        //=================================================/

        //===Получали ли Вы образование на дату проведения переписи (4 октября 2019 г.)?===\
        public GettingBasicEducation GettingBasicEducation { get; set; }/*Умеете ли Вы читать и писать?*/
        public bool? GettingAdditionalEducation { get; set; } = null;   /*Умеете ли Вы читать и писать?*/
        public bool? DoesChildAttendPreschool { get; set; } = null; /*Посещает ли ребенок учреждение дошкольного образования (для детей в возрасте 1 - 7 лет, не посещающих школу) */
        //=================================================================================/

        public string GetFieldsForBD()
        {
            return
                DBUtil.ConvertToSqlType(LvlOfEducation) + ", " +
                DBUtil.ConvertToSqlType(AcademicDegree) + ", " +
                DBUtil.ConvertToSqlType(CanYouReadAndWrite) + ", " +
                DBUtil.ConvertToSqlType(GettingBasicEducation) + ", " +
                DBUtil.ConvertToSqlType(GettingAdditionalEducation) + ", " +
                DBUtil.ConvertToSqlType(DoesChildAttendPreschool);
        }
    }

    public class WorkInfo
    {
        public bool? DoYouHaveWork { get; set; } = null;//Была ли у Вас (сроки) оплачиваемая работа, занятие, приносящее доход, или работа без оплаты труда у родственника (далее - работа) (для лиц в возрасте 15-74 лет)
        ///-------если нет---------\
        public bool? WhyYouDontHaveWork { get; set; } = null;//Вы временно не работали по разным причинам (болезнь,отпуск,вахтовые работы, курсы и т.д.)
        /*Если нет - то это к другому месту*/
        ///------------------------/

        public LocationOfWork LocationOfWork { get; set; }//Где находилось место Вашей работы
        //===========Если 1 -> это не здесь===========\
        ///--------если 2--------\
        public string RegionOrDistrictName { get; set; } = "";// наименования области, района
        public string CityOrPGTName { get; set; } = "";// наименования города, поселка городского типа
        public bool? IsItVillage { get; set; } = null;// сельский населенный пункт
        ///-------если 3---------\
        public string NameOfCountry { get; set; } = "";// название страны
        public DepartureFrequencyToWork DepartureFrequencyToWork { get; set; }// Как часто Вы выезжали на работу на территорию другого государства?
        //============================================/
        public UnemploymentReason UnemploymentReason { get; set; }// Укажите основную причину, по которой Вы не работаете в населенном пункте по месту жительства


        public TypeOfWorkplace TypeOfWorkplace { get; set; }// Укажите, где осуществлялась Ваша работа
        public TypeOfWorkPosition TypeOfWorkPosition { get; set; }// Кем Вы являлись на работе (указать только один вариант ответа)

        ///----------------------\
        public bool? DidYouLookedForAJob { get; set; } = null;// Искали ли Вы работу в течение последнего месяца до начала переписи
        ///----------------------/
        public bool? CanYouStarWorkingInTwoWeeks { get; set; } = null;//Если бы Вы получили подходящую работу, готовы ли Вы приступить к ней в ближайшие две недели
        /*От этого вопроса очень много ответвлений, зависящих еще и от ответов на некоторые другие вопросы*/


        public WhyYouCantWorkOrStopedSearch WhyYouCantWorkOrStopedSearch { get; set; }/*Укажите основную причину, по которой Вы отказались от поиска работы (не готовы приступить к работе)*/

        public string GetFieldsForBD()
        {
            return
                DBUtil.ConvertToSqlType(DoYouHaveWork) + ", " +
                DBUtil.ConvertToSqlType(WhyYouDontHaveWork) + ", " +
                DBUtil.ConvertToSqlType(LocationOfWork) + ", " +
                DBUtil.ConvertToSqlType(RegionOrDistrictName) + ", " +
                DBUtil.ConvertToSqlType(CityOrPGTName) + ", " +
                DBUtil.ConvertToSqlType(IsItVillage) + ", " +
                DBUtil.ConvertToSqlType(NameOfCountry) + ", " +
                DBUtil.ConvertToSqlType(DepartureFrequencyToWork) + ", " +
                DBUtil.ConvertToSqlType(UnemploymentReason) + ", " +
                DBUtil.ConvertToSqlType(TypeOfWorkplace) + ", " +
                DBUtil.ConvertToSqlType(TypeOfWorkPosition) + ", " +
                DBUtil.ConvertToSqlType(DidYouLookedForAJob) + ", " +
                DBUtil.ConvertToSqlType(CanYouStarWorkingInTwoWeeks) + ", " +
                DBUtil.ConvertToSqlType(WhyYouCantWorkOrStopedSearch);
        }
    }

    public class ChildrenInfo
    {
        public string HowManyChildrenDidYouHave { get; set; } = "";// Сколько детей Вы родили? (для женщин в возрасте 15 лет и старше)
        public bool? NoChildren { get; set; } = null;

        public ChildrenPlans ChildrenPlans { get; set; }// Планируете ли Вы рождение детей? (для женщин в возрасте 18-49 лет)
        //=====================Если=да=====================\
        public string HowManyChildrenDoYouWant { get; set; } = "";//Укажите число планируемых детей
        public bool? DontKnowHowMany { get; set; }// число планируемых детей не определено
        //==================================================/

        public string GetFieldsForBD()
        {
            return
                DBUtil.ConvertToSqlType(HowManyChildrenDidYouHave) + ", " +
                DBUtil.ConvertToSqlType(NoChildren) + ", " +
                DBUtil.ConvertToSqlType(ChildrenPlans) + ", " +
                DBUtil.ConvertToSqlType(HowManyChildrenDoYouWant) + ", " +
                DBUtil.ConvertToSqlType(DontKnowHowMany);
        }
    }
}