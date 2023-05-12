package populationcensus.dto;

import lombok.Data;
import populationcensus.repository.entity.*;

import java.time.LocalDate;
import java.util.Date;

@Data
public class PersonDto {
    private Long id;
    private Boolean isForeign;
    private String homeCountry;
    private Integer reasonForMigration;

    private String name;
    private String surname;
    private String fathername;
    private String passportID;
    private LocalDate birthdayDate; private String stringBirthdayDate;
    private Integer age;
    private Integer gender;
    private Integer householdRelations;
    private Integer maritalStatus;
    private Integer birthCountry;
    private String nameOfBirthCountry;
    private Integer citizenship;
    private String nameOfCitizenshipCountry;
    private Integer nationality;
    private String nameOfNationality;
    private Integer nativeLanguage;
    private String nameOfNativeLanguage;
    private Integer speakingLanguage;
    private String nameOfSpeakingLanguage;
    private Integer mainSourceOfResources;

    private LivingPlaceInfoDto livingPlaceInfo;
    private LivingCountryInfoDto livingCountryInfo;
    private EducationInfoDto educationInfo;
    private WorkInfoDto workInfo;
    private ChildrenInfoDto childrenInfo;

    public PersonDto(boolean isForeign) {
        livingPlaceInfo = new LivingPlaceInfoDto();
        livingCountryInfo = new LivingCountryInfoDto();
        educationInfo = new EducationInfoDto();
        workInfo = new WorkInfoDto();
        childrenInfo = new ChildrenInfoDto();
        this.isForeign = isForeign;
    }

    public PersonDto() {
        livingPlaceInfo = new LivingPlaceInfoDto();
        livingCountryInfo = new LivingCountryInfoDto();
        educationInfo = new EducationInfoDto();
        workInfo = new WorkInfoDto();
        childrenInfo = new ChildrenInfoDto();
    }

    public PersonDto(Person entity) {
        id = entity.getId();
        isForeign = entity.getIsForeign();
        homeCountry = entity.getHomeCountry();
        reasonForMigration = entity.getReasonForMigration();

        name = entity.getName();
        surname = entity.getSurname();
        fathername = entity.getFathername();
        passportID = entity.getPassportID();
        birthdayDate = entity.getBirthdayDate(); stringBirthdayDate = birthdayDate == null ? "" : birthdayDate.toString();
        age = entity.getAge();
        gender = entity.getGender();
        householdRelations = entity.getHouseholdRelations();
        maritalStatus = entity.getMaritalStatus();
        birthCountry = entity.getBirthCountry();
        nameOfBirthCountry = entity.getNameOfBirthCountry();
        citizenship = entity.getCitizenship();
        nameOfCitizenshipCountry = entity.getNameOfCitizenshipCountry();
        nationality = entity.getNationality();
        nameOfNationality = entity.getNameOfNationality();
        nativeLanguage = entity.getNativeLanguage();
        nameOfNativeLanguage = entity.getNameOfNativeLanguage();
        speakingLanguage = entity.getSpeakingLanguage();
        nameOfSpeakingLanguage = entity.getNameOfSpeakingLanguage();
        mainSourceOfResources = entity.getMainSourceOfResources();

        if (entity.getLivingPlaceInfo() != null){
            this.livingPlaceInfo = new LivingPlaceInfoDto(entity.getLivingPlaceInfo());
        } else {
            this.livingPlaceInfo = new LivingPlaceInfoDto();
        }

        if (entity.getLivingCountryInfo() != null){
            this.livingCountryInfo = new LivingCountryInfoDto(entity.getLivingCountryInfo());
        } else {
            this.livingCountryInfo = new LivingCountryInfoDto();
        }

        if (entity.getEducationInfo() != null){
            this.educationInfo = new EducationInfoDto(entity.getEducationInfo());
        } else {
            this.educationInfo = new EducationInfoDto();
        }

        if (entity.getWorkInfo() != null){
            this.workInfo = new WorkInfoDto(entity.getWorkInfo());
        } else {
            this.workInfo = new WorkInfoDto();
        }

        if (entity.getChildrenInfo() != null){
            this.childrenInfo = new ChildrenInfoDto(entity.getChildrenInfo());
        } else {
            this.childrenInfo = new ChildrenInfoDto();
        }
    }
}
