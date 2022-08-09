using Census.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Census.utils
{
    public class CensusDBUtil
    {
        private static string[] householdFields;
        private static string[] householdAccommodationsInfoFields;
        private static string[] fullAdressInfoFields;
        private static string[] personFields;
        private static string[] livingPlaceInfoFields;
        private static string[] livingCountryInfoFields;
        private static string[] educationInfoFields;
        private static string[] workInfoFields;
        private static string[] childrenInfoFields;

        public static string[] HouseholdFields { get => householdFields; }
        public static string[] HouseholdAccommodationsInfoFields { get => householdAccommodationsInfoFields; }
        public static string[] FullAdressInfoFields { get => fullAdressInfoFields; }
        public static string[] PersonFields { get => personFields; }
        public static string[] LivingPlaceInfoFields { get => livingPlaceInfoFields; }
        public static string[] LivingCountryInfoFields { get => livingCountryInfoFields; }
        public static string[] EducationInfoFields { get => educationInfoFields; }
        public static string[] WorkInfoFields { get => workInfoFields; }
        public static string[] ChildrenInfoFields { get => childrenInfoFields; }

        static CensusDBUtil()
        {
            householdFields = new string[]
            {
                "id_p",
                "numberOfMembers",
                "roomsCount",
                "partOfRoom",
                "hasForeighResidents",
                "hasFarmFacilities"
            };
            fullAdressInfoFields = new string[]
            {
                "id_p",
                "id_Household_f",
                "sity",
                "sityDistrict",
                "region",
                "regionDistrict",
                "villageCouncil",
                "villageName",
                "streetAvenueOther",
                "houseNumber",
                "ownerFIO",
                "korpNumber",
                "flatOrRoomNumber"
            };
            householdAccommodationsInfoFields = new string[]
            {
                "id_p",
                "id_Household_f",
                "ownerOfApartment",
                "isAppartmentRented",
                "apartmentType",
                "areaOfFlat",
                "waterPipes",
                "canalization",
                "hasBathOrShower",
                "hotWaterSupply",
                "heating",
                "cookingEquipment"
            };
            personFields = new string[]
            {
                "id_p",
                "id_Household_f",
                "isForeign",
                "homeCountry",
                "reasonForMigrationForForeign",
                "p_name",
                "p_surname",
                "p_fathername",
                "passportID",
                "birthdayDate",
                "age",
                "gender",
                "householdRelations",
                "maritalStatus",
                "birthCountry",
                "nameOfBirthCountry",
                "citizenship",
                "nameOfCitizenshipCountry",
                "nationality",
                "nameOfNationality",
                "nativeLanguage",
                "nameOfNativeLanguage",
                "speakingLanguage",
                "nameOfSpeakingLanguage",
                "sourceOfResources",
                "mainSourceOfResources"
            };
            livingPlaceInfoFields = new string[]
            {
                "id_p",
                "id_Person_f",
                "doYouLiveHereFromBirth",
                "arrivalPeriod",
                "previousLivingPlace",
                "regionOrDistrictName",
                "cityOrPGTName",
                "isItVillage",
                "nameOfPreviousCountry",
                "reasonForArrivalAtPlace"
            };
            livingCountryInfoFields = new string[]
            {
                "id_p",
                "id_Person_f",
                "didYouLiveInOtherCountry",
                "nameOfCountryYouCameFrom",
                "arrivalPeriod",
                "reasonForArrivalAtRB",
                "doYouWantToLeaveBelarus",
                "reasonForLeaveBelarus"
            };
            educationInfoFields = new string[]
            {
                "id_p",
                "id_Person_f",
                "lvlOfEducation",
                "academicDegree",
                "canYouReadAndWrite",
                "gettingBasicEducation",
                "gettingAdditionalEducation",
                "doesChildAttendPreschool"
            };
            workInfoFields = new string[]
            {
                "id_p",
                "id_Person_f",
                "doYouHaveWork",
                "whyYouDontHaveWork",
                "locationOfWork",
                "regionOrDistrictName",
                "cityOrPGTName",
                "isItVillage",
                "nameOfCountry",
                "departureFrequencyToWork",
                "unemploymentReason",
                "typeOfWorkplace",
                "typeOfWorkPosition",
                "didYouLookedForAJob",
                "canYouStarWorkingInTwoWeeks",
                "whyYouCantWorkOrStopedSearch"
            };
            childrenInfoFields = new string[] {
                "id_p",
                "id_Person_f",
                "howManyChildrenDidYouHave",
                "noChildren",
                "childrenPlans",
                "howManyChildrenDoYouWant",
                "dontKnowHowMany"
            };
        }

        public static string FieldsToString(string[] fields)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendJoin(", ", fields);
            return sb.ToString();
        }
        public static string FieldsToStringNOID(string[] fields)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendJoin(", ", fields.Skip(1));
            return sb.ToString();
        }

        public static void WriteResultsToDatabase(Household household)
        {
            DBUtil.Execute($"INSERT INTO Household({FieldsToStringNOID(HouseholdFields)}) VALUES ({household.GetFieldsForBD()})");

            int householdTableId = int.Parse(DBUtil.ExecuteReturn("SELECT IDENT_CURRENT('Household')").Rows[0][0].ToString());
            DBUtil.Execute($"INSERT INTO FullAdressInfo({FieldsToStringNOID(FullAdressInfoFields)}) VALUES ({householdTableId}, {household.FullAdressInfo.GetFieldsForBD()})");
            DBUtil.Execute($"INSERT INTO HouseholdAccommodationsInfo({FieldsToStringNOID(HouseholdAccommodationsInfoFields)}) VALUES ({householdTableId}, {household.HouseholdAccommodationsInfo.GetFieldsForBD()})");

            foreach (Person item in household.HouseholdMembers)
            {
                DBUtil.Execute($"INSERT INTO Person({FieldsToStringNOID(PersonFields)}) VALUES ({householdTableId}, {item.GetFieldsForBD()})");
                if (item.IsForeign == false)
                {
                    int personTableId = int.Parse(DBUtil.ExecuteReturn("SELECT IDENT_CURRENT('Person')").Rows[0][0].ToString());
                    DBUtil.Execute($"INSERT INTO LivingPlaceInfo({FieldsToStringNOID(LivingPlaceInfoFields)}) VALUES ({personTableId}, {item.LivingPlaceInfo.GetFieldsForBD()})");
                    DBUtil.Execute($"INSERT INTO LivingCountryInfo({FieldsToStringNOID(LivingCountryInfoFields)}) VALUES ({personTableId}, {item.LivingCountryInfo.GetFieldsForBD()})");
                    DBUtil.Execute($"INSERT INTO EducationInfo({FieldsToStringNOID(EducationInfoFields)}) VALUES ({personTableId}, {item.EducationInfo.GetFieldsForBD()})");
                    if (item.WorkInfo != null)
                    {
                        DBUtil.Execute($"INSERT INTO WorkInfo({FieldsToStringNOID(WorkInfoFields)}) VALUES ({personTableId}, {item.WorkInfo.GetFieldsForBD()})");
                    }
                    if ((int)item.Gender != 1 && int.Parse(item.Age) > 14)
                    {
                        DBUtil.Execute($"INSERT INTO ChildrenInfo({FieldsToStringNOID(ChildrenInfoFields)}) VALUES ({personTableId}, {item.ChildrenInfo.GetFieldsForBD()})");
                    }
                }
            }
        }
        public static Person ReadPersonFromDatabase(int id)
        {
            Person result = new Person();

            DataRow personDR;
            DataRow livingPlaceInfoDR = null;
            DataRow livingCountryInfoDR = null;
            DataRow educationInfoDR = null;
            DataRow workInfoDR = null;
            DataRow childrenInfoDR = null;

            personDR = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(PersonFields)} FROM Person WHERE id_p = {id}").Rows[0];
            if (personDR.Field<bool?>("isForeign") == false)
            {
                livingPlaceInfoDR = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(LivingPlaceInfoFields)} FROM LivingPlaceInfo WHERE id_Person_f = {id}").Rows[0];
                livingCountryInfoDR = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(LivingCountryInfoFields)} FROM LivingCountryInfo WHERE id_Person_f = {id}").Rows[0];
                educationInfoDR = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(EducationInfoFields)} FROM EducationInfo WHERE id_Person_f = {id}").Rows[0];
                workInfoDR = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(WorkInfoFields)} FROM WorkInfo WHERE id_Person_f = {id}").Rows[0];
                if (personDR.Field<int>("gender") != 1 && int.Parse(personDR.Field<string>("age")) > 14)
                {
                    childrenInfoDR = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(ChildrenInfoFields)} FROM ChildrenInfo WHERE id_Person_f = {id}").Rows[0];
                }
            }

            result.IsForeign = personDR.Field<bool?>("isForeign");
            result.Name = personDR.Field<string>("p_name");
            result.Surname = personDR.Field<string>("p_surname");
            result.Fathername = personDR.Field<string>("p_fathername");
            result.BirthdayDate = personDR.Field<DateTime>("birthdayDate");
            result.Age = personDR.Field<string>("age");
            result.Gender = personDR.Field<Gender>("gender");
            result.PassportID = personDR.Field<string>("passportID");
            result.BirthCountry = personDR.Field<BirthCountry>("birthCountry");
            result.NameOfBirthCountry = personDR.Field<string>("nameOfBirthCountry");
            result.Citizenship = personDR.Field<Citizenship>("citizenship");
            result.NameOfCitizenshipCountry = personDR.Field<string>("nameOfCitizenshipCountry");
            if (result.IsForeign == true)
            {
                result.HomeCountry = personDR.Field<string>("homeCountry");
                result.ReasonForMigrationForForeign = personDR.Field<ReasonForMigrationForForeign>("reasonForMigrationForForeign");
                return result;
            }
            else
            {
                result.HouseholdRelations = personDR.Field<HouseholdRelations>("householdRelations");
                result.MaritalStatus = personDR.Field<MaritalStatus>("maritalStatus");
                result.Nationality = personDR.Field<Nationality>("nationality");
                result.NameOfNationality = personDR.Field<string>("nameOfNationality");
                result.NativeLanguage = personDR.Field<NativeLanguage>("nativeLanguage");
                result.NameOfNativeLanguage = personDR.Field<string>("nameOfNativeLanguage");
                result.SpeakingLanguage = personDR.Field<SpeakingLanguage>("speakingLanguage");
                result.NameOfSpeakingLanguage = personDR.Field<string>("nameOfSpeakingLanguage");
                result.SourceOfResources = personDR.Field<string>("sourceOfResources").Split(" ",StringSplitOptions.RemoveEmptyEntries).ToList().ConvertAll((string i) => (SourceOfResources)int.Parse(i));
                result.MainSourceOfResources = personDR.Field<SourceOfResources>("mainSourceOfResources");

                result.LivingPlaceInfo.DoYouLiveHereFromBirth = livingPlaceInfoDR.Field<bool?>("doYouLiveHereFromBirth");
                result.LivingPlaceInfo.ArrivalPeriod = livingPlaceInfoDR.Field<DateTime?>("arrivalPeriod");
                result.LivingPlaceInfo.PreviousLivingPlace = livingPlaceInfoDR.Field<PreviousLivingPlace>("previousLivingPlace");
                result.LivingPlaceInfo.RegionOrDistrictName = livingPlaceInfoDR.Field<string>("regionOrDistrictName");
                result.LivingPlaceInfo.CityOrPGTName = livingPlaceInfoDR.Field<string>("cityOrPGTName");
                result.LivingPlaceInfo.IsItVillage = livingPlaceInfoDR.Field<bool?>("isItVillage");
                result.LivingPlaceInfo.NameOfPreviousCountry = livingPlaceInfoDR.Field<string>("nameOfPreviousCountry");
                result.LivingPlaceInfo.ReasonForArrivalAtPlace = livingPlaceInfoDR.Field<ReasonForMigration>("reasonForArrivalAtPlace");

                result.LivingCountryInfo.DidYouLiveInOtherCountry = livingCountryInfoDR.Field<bool?>("didYouLiveInOtherCountry");
                result.LivingCountryInfo.NameOfCountryYouCameFrom = livingCountryInfoDR.Field<string>("nameOfCountryYouCameFrom");
                result.LivingCountryInfo.ArrivalPeriod = livingCountryInfoDR.Field<DateTime?>("arrivalPeriod");
                result.LivingCountryInfo.ReasonForArrivalAtRB = livingCountryInfoDR.Field<ReasonForMigration>("reasonForArrivalAtRB");
                result.LivingCountryInfo.DoYouWantToLeaveBelarus = livingCountryInfoDR.Field<DoYouWantToLeaveBelarus>("doYouWantToLeaveBelarus");
                result.LivingCountryInfo.ReasonForLeaveBelarus = livingCountryInfoDR.Field<ReasonForMigration>("reasonForLeaveBelarus");

                result.EducationInfo.LvlOfEducation = educationInfoDR.Field<LvlOfEducation>("lvlOfEducation");
                result.EducationInfo.AcademicDegree = educationInfoDR.Field<AcademicDegree>("academicDegree");
                result.EducationInfo.CanYouReadAndWrite = educationInfoDR.Field<bool?>("canYouReadAndWrite");
                result.EducationInfo.GettingBasicEducation = educationInfoDR.Field<GettingBasicEducation>("gettingBasicEducation");
                result.EducationInfo.GettingAdditionalEducation = educationInfoDR.Field<bool?>("gettingAdditionalEducation");
                result.EducationInfo.DoesChildAttendPreschool = educationInfoDR.Field<bool?>("doesChildAttendPreschool");

                result.WorkInfo.DoYouHaveWork = workInfoDR.Field<bool?>("doYouHaveWork");
                result.WorkInfo.WhyYouDontHaveWork = workInfoDR.Field<bool?>("whyYouDontHaveWork");
                result.WorkInfo.LocationOfWork = workInfoDR.Field<LocationOfWork>("locationOfWork");
                result.WorkInfo.RegionOrDistrictName = workInfoDR.Field<string>("regionOrDistrictName");
                result.WorkInfo.CityOrPGTName = workInfoDR.Field<string>("cityOrPGTName");
                result.WorkInfo.IsItVillage = workInfoDR.Field<bool?>("isItVillage");
                result.WorkInfo.NameOfCountry = workInfoDR.Field<string>("nameOfCountry");
                result.WorkInfo.DepartureFrequencyToWork = workInfoDR.Field<DepartureFrequencyToWork>("departureFrequencyToWork");
                result.WorkInfo.UnemploymentReason = workInfoDR.Field<UnemploymentReason>("unemploymentReason");
                result.WorkInfo.TypeOfWorkplace = workInfoDR.Field<TypeOfWorkplace>("typeOfWorkplace");
                result.WorkInfo.TypeOfWorkPosition = workInfoDR.Field<TypeOfWorkPosition>("typeOfWorkPosition");
                result.WorkInfo.DidYouLookedForAJob = workInfoDR.Field<bool?>("didYouLookedForAJob");
                result.WorkInfo.CanYouStarWorkingInTwoWeeks = workInfoDR.Field<bool?>("canYouStarWorkingInTwoWeeks");
                result.WorkInfo.WhyYouCantWorkOrStopedSearch = workInfoDR.Field<WhyYouCantWorkOrStopedSearch>("whyYouCantWorkOrStopedSearch");

                if ((int)result.Gender != 1 && int.Parse(result.Age) > 14)
                {
                    result.ChildrenInfo.HowManyChildrenDidYouHave = childrenInfoDR.Field<string>("howManyChildrenDidYouHave");
                    result.ChildrenInfo.NoChildren = childrenInfoDR.Field<bool?>("noChildren");
                    result.ChildrenInfo.ChildrenPlans = childrenInfoDR.Field<ChildrenPlans>("childrenPlans");
                    result.ChildrenInfo.HowManyChildrenDoYouWant = childrenInfoDR.Field<string>("howManyChildrenDoYouWant");
                    result.ChildrenInfo.DontKnowHowMany = childrenInfoDR.Field<bool?>("dontKnowHowMany");
                }
            }
            return result;
        }
        public static Household ReadEmptyHouseholdFromDatabase(int id)
        {
            Household result = new Household();

            DataTable householdDT = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(HouseholdFields)} FROM Household WHERE id_p = {id}");
            DataTable fullAdressInfoDT = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(FullAdressInfoFields)} FROM FullAdressInfo WHERE id_Household_f = {id}");
            DataTable householdAccommodationsInfoDT = DBUtil.ExecuteReturn($"SELECT {FieldsToStringNOID(HouseholdAccommodationsInfoFields)} FROM HouseholdAccommodationsInfo WHERE id_Household_f = {id}");

            result.NumberOfMembers = householdDT.Rows[0].Field<string>("numberOfMembers");
            result.RoomsCount = householdDT.Rows[0].Field<string>("roomsCount");
            result.PartOfRoom = householdDT.Rows[0].Field<string>("partOfRoom");
            result.HasForeighResidents = householdDT.Rows[0].Field<bool?>("hasForeighResidents");
            result.HasFarmFacilities = householdDT.Rows[0].Field<bool?>("hasFarmFacilities");

            result.FullAdressInfo.Region = fullAdressInfoDT.Rows[0].Field<string>("region");
            result.FullAdressInfo.RegionDistrict = fullAdressInfoDT.Rows[0].Field<string>("regionDistrict");
            result.FullAdressInfo.Sity = fullAdressInfoDT.Rows[0].Field<string>("sity");
            result.FullAdressInfo.SityDistrict = fullAdressInfoDT.Rows[0].Field<string>("sityDistrict");
            result.FullAdressInfo.VillageCouncil = fullAdressInfoDT.Rows[0].Field<string>("villageCouncil");
            result.FullAdressInfo.VillageName = fullAdressInfoDT.Rows[0].Field<string>("villageName");
            result.FullAdressInfo.StreetAvenueOther = fullAdressInfoDT.Rows[0].Field<string>("streetAvenueOther");
            result.FullAdressInfo.HouseNumber = fullAdressInfoDT.Rows[0].Field<string>("houseNumber");
            result.FullAdressInfo.OwnerFIO = fullAdressInfoDT.Rows[0].Field<string>("ownerFIO");
            result.FullAdressInfo.KorpNumber = fullAdressInfoDT.Rows[0].Field<string>("korpNumber");
            result.FullAdressInfo.FlatOrRoomNumber = fullAdressInfoDT.Rows[0].Field<string>("flatOrRoomNumber");

            result.HouseholdAccommodationsInfo.OwnerOfApartment = householdAccommodationsInfoDT.Rows[0].Field<OwnerOfApartment>("ownerOfApartment");
            result.HouseholdAccommodationsInfo.IsAppartmentRented = householdAccommodationsInfoDT.Rows[0].Field<bool>("isAppartmentRented");
            result.HouseholdAccommodationsInfo.ApartmentType = householdAccommodationsInfoDT.Rows[0].Field<ApartmentType>("apartmentType");
            result.HouseholdAccommodationsInfo.AreaOfFlat = householdAccommodationsInfoDT.Rows[0].Field<string>("areaOfFlat");
            result.HouseholdAccommodationsInfo.WaterPipes = householdAccommodationsInfoDT.Rows[0].Field<WaterPipes>("waterPipes");
            result.HouseholdAccommodationsInfo.Сanalization = householdAccommodationsInfoDT.Rows[0].Field<Сanalization>("canalization");
            result.HouseholdAccommodationsInfo.HasBathOrShower = householdAccommodationsInfoDT.Rows[0].Field<bool?>("hasBathOrShower");
            result.HouseholdAccommodationsInfo.HotWaterSupply = householdAccommodationsInfoDT.Rows[0].Field<HotWaterSupply>("hotWaterSupply");
            result.HouseholdAccommodationsInfo.Heating = householdAccommodationsInfoDT.Rows[0].Field<Heating>("heating");
            result.HouseholdAccommodationsInfo.CookingEquipment = householdAccommodationsInfoDT.Rows[0].Field<CookingEquipment>("cookingEquipment");

            return result;
        }
        public static void ClearDatabase()
        {
            DBUtil.Execute
                (
                    "TRUNCATE TABLE LivingPlaceInfo \r\n" +
                    "TRUNCATE TABLE LivingCountryInfo \r\n" +
                    "TRUNCATE TABLE EducationInfo \r\n" +
                    "TRUNCATE TABLE WorkInfo \r\n" +
                    "TRUNCATE TABLE ChildrenInfo \r\n" +
                    "DELETE FROM CensusDB.dbo.Person \r\n" +
                    "DBCC CHECKIDENT ('Person', RESEED, 0) \r\n" +
                    "TRUNCATE TABLE FullAdressInfo \r\n" +
                    "TRUNCATE TABLE HouseholdAccommodationsInfo \r\n" +
                    "DELETE FROM CensusDB.dbo.Household \r\n" +
                    "DBCC CHECKIDENT ('Household', RESEED, 0)"
                );
        }
        public static void RegistrateMainAdmin()
        {
            DBUtil.Execute($"INSERT INTO CensusDB.dbo.Administrator(accountName,accountPassword) VALUES ('admin','{DateTime.Now:yyyyMMddHmmss}')");
        }
    }
}
