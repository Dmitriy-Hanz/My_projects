using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using Census.utils;

namespace Census.model
{
    #region enum-ы

    public enum OwnerOfApartment //В чьей собственности находится помещение, используемое домохозяйством для проживания
    {
        State = 1, //государственная собственность
        PrivateIndividual = 2, //частной собственности физического лица
        PrivateLegalEntity = 3//частной собственности негосударственного юридического лица
    }
    public enum ApartmentType //Тип помещения, используемого для проживания
    {
        HouseWithFlat = 1, //одноквартирный жилой дом
        Flat = 2, //квартира
        StudentsHostel = 3, //общежитие для учащихся и студентов
        AnotherHostel = 4, //другое общежитие (кроме общежития для учащихся и студентов)
        GardenHouseOrCountryHouse = 5, //садовый домик (дача)
        Hotel = 6, //гостиница
        Other = 7, //иное помещение
        NonResidentialPremises = 8 //нежилое помещение
    }

    #region Виды благоустройства в помещении, используемом для проживания
    public enum WaterPipes //Водопровод
    {
        Central = 1, //центральный
        Local = 2, //локальный
        None = 3 //отсутствует
    }
    public enum Сanalization //кANALизация
    {
        Central = 1, //центральный
        Local = 2, //локальный
        None = 3 //отсутствует
    }
    public enum HotWaterSupply //горячее водоснабжение
    {
        Central = 1, //центральное
        FromWaterHeaters = 2, //от индивидуальных водонагревателей
        FromGasWaterHeaters = 201, //газовых
        FromElectricWaterHeaters = 202, //электрических
        FromOtherWaterHeaters = 203, //других
        None = 3 //отсутствует
    }
    public enum Heating //отопление
    {
        Central = 1, //центральное
        FromHeaters = 2, //от индивидуальных отопительных приборов
        FromGasHeaters = 201, //газовых
        FromElectricHeaters = 202, //электрических
        FromOtherHeaters = 203, //других
        Stove = 3, //печное
        Other = 4, //другое
        None = 5 //отсутствует
    }
    public enum CookingEquipment //оборудование для приготовления пищи
    {
        PlateGasOven = 1, //напольная, настольная газовая плита, варочная панель, подключенная к газу
        PlateGasOvenWithNetworkGas = 101, //сетевому
        PlateGasOvenWithBallonGas = 102, //сжиженному (в баллоне)
        PlateElectricOven = 2, //напольная, настольная электрическая плита, варочная панель
        Other = 3, //другое
        None = 4 //отсутствует
    }

    #endregion

    #endregion
    public class Household
    {
        public Household()
        {
            FullAdressInfo = new FullAdressInfo();
            HouseholdAccommodationsInfo = new HouseholdAccommodationsInfo();
            HouseholdMembers = new List<Person>();
        }
        private static bool FailMessage(string message)
        {
            MessageBox.Show(message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Hand);
            return false;
        }
        public bool MiniValidate()
        {
            int parseTemp;

            if (FullAdressInfo.Region.Equals("")) { return FailMessage("Не указана городская область."); }
            if (FullAdressInfo.RegionDistrict.Equals("")) { return FailMessage("Не указана район области."); }

            if (!FullAdressInfo.Sity.Equals("") && (!FullAdressInfo.VillageName.Equals("") || !FullAdressInfo.VillageCouncil.Equals("")))
            {
                return FailMessage("город/район города и сельсовет/сельский НП не могут быть указаны одновременно.");
            }
            else
            {
                if (!FullAdressInfo.SityDistrict.Equals("") && (!FullAdressInfo.VillageName.Equals("") || !FullAdressInfo.VillageCouncil.Equals("")))
                {
                    return FailMessage("город/район города и сельсовет/сельский НП не могут быть указаны одновременно.");
                }
            }

            if (FullAdressInfo.Sity.Equals("") && FullAdressInfo.VillageName.Equals("") && FullAdressInfo.VillageCouncil.Equals("")) { return FailMessage("Не указано место проживания (город/сельсовет/сельский НП)."); }
            else { if (!FullAdressInfo.VillageCouncil.Equals("") && FullAdressInfo.VillageName.Equals("")) return FailMessage("Не указан сельский населенный пункт."); }

            if (FullAdressInfo.StreetAvenueOther.Equals("")) { return FailMessage("Не указана улица, проспект, ... ."); }

            if (FullAdressInfo.HouseNumber.Equals("") && FullAdressInfo.OwnerFIO.Equals("")) { return FailMessage("Не указан номер дома или собственник."); }
            if (!FullAdressInfo.HouseNumber.Equals("") && !FullAdressInfo.OwnerFIO.Equals("")) { return FailMessage("номер дома и собственник не могут быть указаны одновременно."); }

            if (!FullAdressInfo.KorpNumber.Equals("") && int.TryParse(FullAdressInfo.KorpNumber,out parseTemp) == false) { return FailMessage("Номер корпуса должен содержать только цифры."); }

            if (FullAdressInfo.FlatOrRoomNumber.Equals("")) { return FailMessage("Не указан номер квартиры/комнаты"); }
            else { if (int.TryParse(FullAdressInfo.FlatOrRoomNumber,out parseTemp) == false) return FailMessage("Номер квартиры/комнаты должен содержать только цифры."); }

            if (NumberOfMembers.Equals("")) { return FailMessage("Не указано количество членов домохозяйства."); }
            else { if (int.TryParse(NumberOfMembers, out parseTemp) == false) return FailMessage("Количество членов домохозяйства должно быть цифрой(числом)."); }

            return true;
        }
        public bool Validate()
        {
            int parseTemp;

            if (RoomsCount.Equals("") && PartOfRoom.Equals("")) { return FailMessage("Не указано кол-во комнат или часть комнаты, занимаемых домохозяйством."); }
            else { if (!RoomsCount.Equals("") && int.TryParse(RoomsCount, out parseTemp) == false) return FailMessage("количество комнат должно быть цифрой(числом)."); }

            if (HouseholdAccommodationsInfo.OwnerOfApartment == 0) { return FailMessage("Не указана информация о собственнике помещения для проживания."); }
            //HouseholdAccommodationsInfo.IsAppartmentRented пропускаем...
            if (HasForeighResidents == null) { return FailMessage("Не указана информация о иностранных респондентах."); }
            if (HasFarmFacilities == null) { return FailMessage("Не указана информация о сельскохозяйственной деятельности."); }
            if (HouseholdAccommodationsInfo.ApartmentType == 0) { return FailMessage("Не указан тип помещения, используемого для проживания."); }

            if ((int)HouseholdAccommodationsInfo.ApartmentType < 3)
            {
                if (HouseholdAccommodationsInfo.AreaOfFlat.Equals("")) { return FailMessage("Не указана площадь дома/квартиры."); }
                else { if (int.TryParse(HouseholdAccommodationsInfo.AreaOfFlat, out parseTemp) == false) return FailMessage("площадь дома/квартиры должна быть числом(цифрой)."); }

                if (HouseholdAccommodationsInfo.WaterPipes == 0) { return FailMessage("Не указана информация о водопроводе."); }
                if (HouseholdAccommodationsInfo.Сanalization == 0) { return FailMessage("Не указана информация о канализации."); }
                if (HouseholdAccommodationsInfo.HasBathOrShower == null) { return FailMessage("Не указана информация о ванне/душе."); }
                if (HouseholdAccommodationsInfo.HotWaterSupply == 0) { return FailMessage("Не указана информация о горячем водоснабжении."); }
                if (HouseholdAccommodationsInfo.Heating == 0) { return FailMessage("Не указана информация об отоплении."); }
                if (HouseholdAccommodationsInfo.CookingEquipment == 0) { return FailMessage("Не указана информация о кухонном оборудовании."); }
            }
            return true;
        }
        public void ClearUselessValues()
        {
            if ((int)HouseholdAccommodationsInfo.ApartmentType > 2)
            {
                HouseholdAccommodationsInfo.AreaOfFlat = null;
                HouseholdAccommodationsInfo.WaterPipes = 0;
                HouseholdAccommodationsInfo.Сanalization = 0;
                HouseholdAccommodationsInfo.HasBathOrShower = null;
                HouseholdAccommodationsInfo.HotWaterSupply = 0;
                HouseholdAccommodationsInfo.Heating = 0;
                HouseholdAccommodationsInfo.CookingEquipment = 0;
            }
        }

        public string NumberOfMembers { get; set; } = "";
        public List<Person> HouseholdMembers { get; set; }
        public FullAdressInfo FullAdressInfo { get; set; }

        //Количество жилых комнат, занимаемых домохозяйством:

        public string RoomsCount { get; set; } = "";//1, число комнат
        public string PartOfRoom { get; set; } = "";//2, часть комнаты

        public bool? HasForeighResidents { get; set; } = null;//Находились ли временно в данном домохозяйстве в ночь на 4 октября 2019 года респонденты, постоянно проживающие за пределами Республики Беларусь?
        public bool? HasFarmFacilities { get; set; } = null;
        public HouseholdAccommodationsInfo HouseholdAccommodationsInfo { get; set; }//ХАРАКТЕРИСТИКА ПОМЕЩЕНИЯ, ИСПОЛЬЗУЕМОГО ДЛЯ ПРОЖИВАНИЯ

        public string GetFieldsForBD()
        {
            return
                DBUtil.ConvertToSqlType(NumberOfMembers) + ", " +
                DBUtil.ConvertToSqlType(RoomsCount) + ", " +
                DBUtil.ConvertToSqlType(PartOfRoom) + ", " +
                DBUtil.ConvertToSqlType(HasForeighResidents) + ", " +
                DBUtil.ConvertToSqlType(HasFarmFacilities);
        }
    }

    public class HouseholdAccommodationsInfo
    {
        public OwnerOfApartment OwnerOfApartment { get; set; }

        public bool IsAppartmentRented { get; set; } = false;//Указать, если владение и пользование осуществляется по договору найма(поднайма) жилого помещения у физического лица


        public ApartmentType ApartmentType { get; set; }// Тип помещения, используемого для проживания

        public string AreaOfFlat { get; set; } = "";//Размер общей площади одноквартирного жилого дома или квартиры (метры кв., целое число)

        public WaterPipes WaterPipes { get; set; }//Водопровод
        public Сanalization Сanalization { get; set; }//кANALизация
        public bool? HasBathOrShower { get; set; } = null;// есть ли ванна и (или) душ
        public HotWaterSupply HotWaterSupply { get; set; }//горячее водоснабжение
        public Heating Heating { get; set; }//отопление
        public CookingEquipment CookingEquipment { get; set; }//оборудование для приготовления пищи

        public string GetFieldsForBD()
        {
            return
                DBUtil.ConvertToSqlType(OwnerOfApartment) + ", " +
                DBUtil.ConvertToSqlType(IsAppartmentRented) + ", " +
                DBUtil.ConvertToSqlType(ApartmentType) + ", " +
                DBUtil.ConvertToSqlType(AreaOfFlat) + ", " +
                DBUtil.ConvertToSqlType(WaterPipes) + ", " +
                DBUtil.ConvertToSqlType(Сanalization) + ", " +
                DBUtil.ConvertToSqlType(HasBathOrShower) + ", " +
                DBUtil.ConvertToSqlType(HotWaterSupply) + ", " +
                DBUtil.ConvertToSqlType(Heating) + ", " +
                DBUtil.ConvertToSqlType(CookingEquipment);
        }
    }

    public class FullAdressInfo
    {
        public string Sity { get; set; } = "";
        public string SityDistrict { get; set; } = "";

        public string Region { get; set; } = "";
        public string RegionDistrict { get; set; } = "";

        public string VillageCouncil { get; set; } = "";
        public string VillageName { get; set; } = "";

        public string StreetAvenueOther { get; set; } = "";//Улица, проспект и др.

        public string HouseNumber { get; set; } = "";
        public string OwnerFIO { get; set; } = "";//в случае отсутствия номера дома укажите фамилию и инициалы собственника, нанимателя(поднанимателя)

        public string KorpNumber { get; set; } = "";

        public string FlatOrRoomNumber { get; set; } = "";//№ квартиры (комнаты)

        public string GetFieldsForBD()
        {
            return
                DBUtil.ConvertToSqlType(Sity) + ", " +
                DBUtil.ConvertToSqlType(SityDistrict) + ", " +
                DBUtil.ConvertToSqlType(Region) + ", " +
                DBUtil.ConvertToSqlType(RegionDistrict) + ", " +
                DBUtil.ConvertToSqlType(VillageCouncil) + ", " +
                DBUtil.ConvertToSqlType(VillageName)  + ", " +
                DBUtil.ConvertToSqlType(StreetAvenueOther) + ", " +
                DBUtil.ConvertToSqlType(HouseNumber) + ", " +
                DBUtil.ConvertToSqlType(OwnerFIO) + ", " +
                DBUtil.ConvertToSqlType(KorpNumber) + ", " +
                DBUtil.ConvertToSqlType(FlatOrRoomNumber);
        }
    }
}
