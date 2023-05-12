function validationFail(message){
    alert(message);
    return false;
}

function getValueByName(name){
    return document.getElementsByName(name)[0].value;
}

function isChecked(name){
    return document.getElementsByName(name)[0].checked;
}
function isEmptyString(name){
    return getValueByName(name) === "";
}

function isNotIntegerPositiveNumber(name) {
    if (Number.isInteger(+getValueByName(name)) === false){
        return true;
    }
    if (+getValueByName(name) < 0){
        return true;
    }
}
function isNotIntegerMoreThenOneNumber(name) {
    if (Number.isInteger(+getValueByName(name)) === false){
        return true;
    }
    if (+getValueByName(name) < 1){
        return true;
    }
}

function isDropdownZero(name) {
    return getValueByName(name) == 0;
}
function isDropdownEqual(name,value) {
    return getValueByName(name) == value;
}
function isAgeMoreThan(value) {
    return +getValueByName("age") > value;
}
function isAgeInArea(down,up) {
    return +getValueByName("age") > down && +getValueByName("age") < up;
}



function validateHousehold() {

    if (isEmptyString("fullAddressInfo.region")) { return validationFail("Не указана городская область."); }
    if (isEmptyString("fullAddressInfo.regionDistrict")) { return validationFail("Не указана район области.") }

    if (!isEmptyString("fullAddressInfo.city") && (!isEmptyString("fullAddressInfo.villageName") || !isEmptyString("fullAddressInfo.villageCouncil")))
    {
        return validationFail("Город/район города и сельсовет/сельский НП не могут быть указаны одновременно.")
    }
    else
    {
        if (!isEmptyString("fullAddressInfo.cityDistrict") && (!isEmptyString("fullAddressInfo.villageName") || !isEmptyString("fullAddressInfo.villageCouncil")))
        {
            return validationFail("город/район города и сельсовет/сельский НП не могут быть указаны одновременно.");
        }
    }

    if (isEmptyString("fullAddressInfo.city") && isEmptyString("fullAddressInfo.villageName") && isEmptyString("fullAddressInfo.villageCouncil"))
    {
        return validationFail("Не указано место проживания (город/сельсовет/сельский НП).");
    }
    else
    {
        if (!isEmptyString("fullAddressInfo.villageCouncil") && isEmptyString("fullAddressInfo.villageName"))
            return validationFail("Не указан сельский населенный пункт.");
    }

    if (isEmptyString("fullAddressInfo.streetAvenueOther")) { return validationFail("Не указана улица, проспект, ... ."); }

    if (isEmptyString("fullAddressInfo.houseNumber") && isEmptyString("fullAddressInfo.ownerFIO")) { return validationFail("Не указан номер дома или собственник."); }
    if (!isEmptyString("fullAddressInfo.houseNumber") && !isEmptyString("fullAddressInfo.ownerFIO")) { return validationFail("Номер дома и собственник не могут быть указаны одновременно."); }

    if (isEmptyString("fullAddressInfo.flatOrRoomNumber")) { return validationFail("Не указан номер квартиры/комнаты"); }



    if (isEmptyString("numberOfMembers")) { return validationFail("Не указано количество членов домохозяйства."); }
    else
    { if (isNotIntegerMoreThenOneNumber("numberOfMembers")) { return validationFail("Количество членов домохозяйства должно быть целой положительной цифрой(целым положительным числом), не равным 0."); } }

    if (isEmptyString("roomsCount") && isEmptyString("partOfRoom")) { return validationFail("Не указано кол-во комнат или часть комнаты, занимаемых домохозяйством."); }
    else
    { if (!isEmptyString("roomsCount") && isNotIntegerMoreThenOneNumber("roomsCount")) return validationFail("количество комнат должно быть целой положительной цифрой(целым положительным числом), не равным 0."); }

    if (getValueByName("accommodationsInfo.ownerOfApartment") == 0) { return validationFail("Не указана информация о собственнике помещения для проживания."); }
    if (getValueByName("accommodationsInfo.apartmentType") == 0) { return validationFail("Не указан тип помещения, используемого для проживания."); }

    if (getValueByName("accommodationsInfo.apartmentType") < 3)
    {
        if (isEmptyString("accommodationsInfo.areaOfFlat")) { return validationFail("Не указана площадь дома/квартиры."); }
        else 
        { if (isNotIntegerPositiveNumber("accommodationsInfo.areaOfFlat")) return validationFail("Площадь дома/квартиры должна быть целым положительным числом(целой положительной цифрой)."); }

        if (getValueByName("accommodationsInfo.waterPipes") == 0) { return validationFail("Не указана информация о водопроводе."); }
        if (getValueByName("accommodationsInfo.canalization") == 0) { return validationFail("Не указана информация о канализации."); }
        if (getValueByName("accommodationsInfo.hotWaterSupply") == 0) { return validationFail("Не указана информация о горячем водоснабжении."); }
        if (getValueByName("accommodationsInfo.heating") == 0) { return validationFail("Не указана информация об отоплении."); }
        if (getValueByName("accommodationsInfo.cookingEquipment") == 0) { return validationFail("Не указана информация о кухонном оборудовании."); }
    }
    else{
        if (!isEmptyString("accommodationsInfo.areaOfFlat")) { return validationFail("Поле «Площадь дома/квартиры» должно быть пустым."); }
        if (getValueByName("accommodationsInfo.waterPipes") != 0) { return validationFail("Поле «Водопровод» должно быть пустым."); }
        if (getValueByName("accommodationsInfo.canalization") != 0) { return validationFail("Поле «Канализация» должно быть пустым."); }
        if (isChecked("accommodationsInfo.hasBathOrShower") === true) { return validationFail("Поле «Ванна и (или) душ» должно быть пустым."); }

        if (getValueByName("accommodationsInfo.hotWaterSupply") != 0) { return validationFail("Поле «Горячее водоснабжение» должно быть пустым."); }
        if (getValueByName("accommodationsInfo.heating") != 0) { return validationFail("Поле «Отопление» должно быть пустым."); }
        if (getValueByName("accommodationsInfo.cookingEquipment") != 0) { return validationFail("Поле «Кухонное оборудование» должно быть пустым."); }
    }

}

function validatePerson() {

    if (isEmptyString("surname")) { return validationFail("Не указана фамилия."); }
    if (isEmptyString("name")) { return validationFail("Не указано имя."); }
    if (isEmptyString("passportID")) { return validationFail("Не указан идентификационный номер."); }
    if (isEmptyString("stringBirthdayDate")) { return validationFail("Не указана дата рождения."); }

    if (isEmptyString("age")) { return validationFail("Не указано число полных лет."); }
    else
    { if (isNotIntegerPositiveNumber("age")) return validationFail("Число полных лет должно быть целым положительным числом(целой положительной цифрой)."); }

    if (isDropdownZero("gender")) { return validationFail("Не указан пол."); }
    if (isDropdownZero("householdRelations")) { return validationFail("Не указана информация об отношении к первому лицу в домохозяйстве."); }
    if (isAgeMoreThan(15) && isDropdownZero("maritalStatus")) { return validationFail("Не указана информация о семейном положении."); }

    if (isDropdownZero("birthCountry")) { return validationFail("Не указана информация о стране рождения."); }
    else
    { if(isDropdownEqual("birthCountry",2) && isEmptyString("nameOfBirthCountry"))  return validationFail("Не указано название страны рождения."); }

    if (isDropdownZero("citizenship")) { return validationFail("Не указана информация о гражданстве."); }
    else
    {
        if (isDropdownEqual("citizenship",2) && isEmptyString("nameOfCitizenshipCountry")) { return validationFail("Не указана информация о гражданстве (название страны)."); }
    }


    if (isDropdownZero("nationality")) { return validationFail("Не указана информация о национальности."); }
    else{
        if (isDropdownEqual("nationality",6) && isEmptyString("nameOfNationality")){return validationFail("Не указана информация о национальности (название).");}
    }

    if (isDropdownZero("nativeLanguage")) { return validationFail("Не указана информация о родном языке."); }
    else{
        if (isDropdownEqual("nativeLanguage",5) && isEmptyString("nameOfNativeLanguage")){return validationFail("Не указана информация о родном языке (название).");}
    }

    if (isDropdownZero("speakingLanguage")) { return validationFail("Не указана информация о разговорном языке."); }
    else{
        if (isDropdownEqual("speakingLanguage",5) && isEmptyString("nameOfSpeakingLanguage")){return validationFail("Не указана информация о разговорном языке (название).");}
    }

    if (isDropdownZero("mainSourceOfResources")) { return validationFail("Не указан основной источник средств к существованию."); }


    ///////////////////
    //LivingPlaceInfo//

    if (!isChecked("livingPlaceInfo.doYouLiveHereFromBirth"))
    {
        if (isEmptyString("livingPlaceInfo.stringArrivalPeriod")) { return validationFail("Не указана дата прибытия."); }
        if (isDropdownZero("livingPlaceInfo.previousLivingPlace")) { return validationFail("Не указана информация о предыдущем месте жительства."); }
        else
        {
            if (isDropdownEqual("livingPlaceInfo.previousLivingPlace",1))
            {
                if (isEmptyString("livingPlaceInfo.regionOrDistrictName")) { return validationFail("Не указано название области/района предыдущего места жительства."); }
                if (!isChecked("livingPlaceInfo.isItVillage") && isEmptyString("livingPlaceInfo.cityOrPGTName")) { return validationFail("Не указано место проживания (город/ПГТ/деревня)."); }
                if (isChecked("livingPlaceInfo.isItVillage") && !isEmptyString("livingPlaceInfo.cityOrPGTName")) { return validationFail("Должно быть указано только одно место проживания (города/ПГТ или деревня)."); }
            }
            if (isDropdownEqual("livingPlaceInfo.previousLivingPlace",2))
            {
                if (isEmptyString("livingPlaceInfo.nameOfPreviousCountry")) { return validationFail("Не указано название страны предыдущего места жительства."); }
            }
        }
        if (isDropdownZero("livingPlaceInfo.reasonForArrivalAtPlace")) { return validationFail("Не указана причина прибытия."); }
    }


    /////////////////////
    //LivingCountryInfo//

    if (!isChecked("livingPlaceInfo.doYouLiveHereFromBirth"))
    {
        if (isChecked("livingCountryInfo.didYouLiveInOtherCountry"))
        {
            if (isEmptyString("livingCountryInfo.nameOfCountryYouCameFrom")) { return validationFail("Не указана информация о стране, из которой вы прибыли в РБ на ПМЖ."); }
            if (isEmptyString("livingCountryInfo.stringArrivalPeriod")) { return validationFail("Не указан период прибытия."); }
            if (isDropdownZero("livingCountryInfo.reasonForArrivalAtRB")) { return validationFail("Не указана причина прибытия."); }
        }
        if (isDropdownZero("livingCountryInfo.infoAboutLeavingBelarus") && isAgeInArea(14,74)) { return validationFail("Не указана информация о желании покинуть РБ."); }
        if (!isDropdownEqual("livingCountryInfo.infoAboutLeavingBelarus",4) && isDropdownZero("livingCountryInfo.reasonForLeavingBelarus")){return validationFail("Не указана причина выезда из РБ.");}
    }


    /////////////////
    //EducationInfo//

    if (isAgeMoreThan(9)){
        if (isDropdownZero("educationInfo.lvlOfEducation")) { return validationFail("Не указана информация об образовании."); }
        if (isDropdownZero("educationInfo.academicDegree")) { return validationFail("Не указана информация о наличии ученой степени."); }
    }
    if (isAgeMoreThan(6) && isDropdownZero("educationInfo.basicEducationInfo")) { return validationFail("Не указана информация о получении основного образования."); }


    ////////////
    //WorkInfo//

    if (isAgeInArea(15,75))
    {
        if (isChecked("workInfo.doYouHaveWork") || isChecked("workInfo.whyDidntYouWorkTillNow"))
        {
            switch (+getValueByName("workInfo.locationOfWork"))
            {
                case 0:
                    return validationFail("Не указана информация о месте работы.");
                case 2:
                    if (isEmptyString("workInfo.regionOrDistrictName")) { return validationFail("Не указано название области/района, где находилось место вашей работы."); }
                    if (!isChecked("workInfo.isItVillage") && isEmptyString("workInfo.cityOrPGTName")) { return validationFail("Не указана информация о месте работы (город/ПГТ/деревня)."); }
                    if (isChecked("workInfo.isItVillage") && !isEmptyString("workInfo.cityOrPGTName")) { return validationFail("Некорректно указана информация о месте работы (город/ПГТ ИЛИ деревня)."); }
                    break;
                case 3:
                    if (isEmptyString("workInfo.nameOfCountry")) { return validationFail("Не указано название страны, где находилось место вашей работы."); }
                    if (isDropdownZero("workInfo.departureFrequencyToWork")) { return validationFail("Не указана информация о частоте выездов на территорию другого гос-ва для работы."); }
                    break;
            }
            if (isDropdownZero("workInfo.unemploymentReason")) { return validationFail("Не указана причина, по которой вы не работаете в населенном пункте ПМЖ."); }
        }
        if (isDropdownEqual("workInfo.locationOfWork", 1) || !isDropdownZero("workInfo.unemploymentReason"))
        {
            if (isDropdownZero("workInfo.typeOfWorkplace")) { return validationFail("Не указана информация о месте вашей работы."); }
            if (isDropdownZero("workInfo.typeOfWorkPosition")) { return validationFail("Не указана информация о должности на работы."); }
        }

        if (isDropdownZero("workInfo.typeOfWorkPosition"))
        {
            if (!(isChecked("workInfo.didYouLookedForAJob") && isChecked("workInfo.canYouStarWorkingInTwoWeeks")) || !isChecked("workInfo.canYouStarWorkingInTwoWeeks"))
            {
                if (isDropdownZero("workInfo.whyYouCantWorkOrStoppedSearch")) { return validationFail("Не указана информация о причине отказа от трудоустройства."); }
            }
        }
    }


    ////////////////
    //ChildrenInfo//

    if (isDropdownEqual("gender",2))
    {
        if (isAgeMoreThan(14))
        {
            if (isEmptyString("childrenInfo.howManyChildrenDoYouHave") && !isChecked("childrenInfo.noChildren")) { return validationFail("Не указана информация о количестве имеющихся детей."); }
            if (!isEmptyString("childrenInfo.howManyChildrenDoYouHave") && isChecked("childrenInfo.noChildren")) { return validationFail("Некорректно указана информация о количестве имеющихся детей."); }
            if (!isEmptyString("childrenInfo.howManyChildrenDoYouHave") && isNotIntegerMoreThenOneNumber("childrenInfo.howManyChildrenDoYouHave")) { return validationFail("Количество имеющихся детей должно быть целым положительным числом (целой положительной цифрой), не равной 0."); }
        }
        if (isAgeInArea(17,50))
        {
            if (isDropdownZero("childrenInfo.childrenPlans")) { return validationFail("Не указана информация о планах на рождение детей."); }
            if (isDropdownEqual("childrenInfo.childrenPlans",1))
            {
                if (isEmptyString("childrenInfo.howManyChildrenDoYouWant") && !isChecked("childrenInfo.dontKnowHowMany")) { return validationFail("Не указана информация о количестве желаемых детей."); }
                if (!isEmptyString("childrenInfo.howManyChildrenDoYouWant") && isChecked("childrenInfo.dontKnowHowMany")) { return validationFail("Некорректно указана информация о количестве желаемых детей."); }
                if (isNotIntegerMoreThenOneNumber("childrenInfo.howManyChildrenDoYouWant")) { return validationFail("Количество желаемых детей должно быть целым положительным числом (целой положительной цифрой), не равной 0."); }
            }
        }
    }
}

function validateForeignPerson() {
    if (isEmptyString("surname")) { return validationFail("Не указана фамилия."); }
    if (isEmptyString("name")) { return validationFail("Не указано имя."); }
    if (isEmptyString("stringBirthdayDate")) { return validationFail("Не указана дата рождения."); }

    if (isEmptyString("age")) { return validationFail("Не указано число полных лет."); }
    else
    { if (isNotIntegerPositiveNumber("age")) return validationFail("Число полных лет должно быть целым положительным числом(целой положительной цифрой)."); }

    if (isDropdownZero("gender")) { return validationFail("Не указан пол."); }
    if (isDropdownZero("birthCountry")) { return validationFail("Не указана информация о стране рождения."); }
    else
    { if(isDropdownEqual("birthCountry",2) && isEmptyString("nameOfBirthCountry"))  return validationFail("Не указано название страны рождения."); }

    if (isDropdownZero("citizenship")) { return validationFail("Не указана информация о гражданстве."); }
    else
    { if (isDropdownEqual("citizenship",2) && isEmptyString("nameOfCitizenshipCountry")) { return validationFail("Не указана информация о гражданстве (название страны)."); }}

    if (isEmptyString("passportID")) { return validationFail("Не указан идентификационный номер."); }

    if (isEmptyString("homeCountry")) { return validationFail("Не указана страна постоянного проживания."); }
    if (isDropdownZero("reasonForMigration")) { return validationFail("Не указана причина приезда в РБ."); }
}


function  validatePassportID(){
    if (isEmptyString("passportID")) { return validationFail("Вы должны указать номер паспорта."); }
}




