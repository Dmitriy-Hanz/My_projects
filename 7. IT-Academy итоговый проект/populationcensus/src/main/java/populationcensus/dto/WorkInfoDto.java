package populationcensus.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.EducationInfo;
import populationcensus.repository.entity.WorkInfo;

import javax.persistence.Column;

@Data
@NoArgsConstructor
public class WorkInfoDto {
    private Long id;
    private Boolean doYouHaveWork;
    private Boolean whyDidntYouWorkTillNow;
    private Integer locationOfWork;
    private String regionOrDistrictName;
    private String cityOrPGTName;
    private Boolean isItVillage;
    private String nameOfCountry;
    private Integer departureFrequencyToWork;
    private Integer unemploymentReason;
    private Integer typeOfWorkplace;
    private Integer typeOfWorkPosition;
    private Boolean didYouLookedForAJob;
    private Boolean canYouStarWorkingInTwoWeeks;
    private Integer whyYouCantWorkOrStoppedSearch;


    public WorkInfoDto(WorkInfo entity) {
        id = entity.getId();
        doYouHaveWork = entity.getDoYouHaveWork();
        whyDidntYouWorkTillNow = entity.getWhyDidntYouWorkTillNow();
        locationOfWork = entity.getLocationOfWork();
        regionOrDistrictName = entity.getRegionOrDistrictName();
        cityOrPGTName = entity.getCityOrPGTName();
        isItVillage = entity.getIsItVillage();
        nameOfCountry = entity.getNameOfCountry();
        departureFrequencyToWork = entity.getDepartureFrequencyToWork();
        unemploymentReason = entity.getUnemploymentReason();
        typeOfWorkplace = entity.getTypeOfWorkplace();
        typeOfWorkPosition = entity.getTypeOfWorkPosition();
        didYouLookedForAJob = entity.getDidYouLookedForAJob();
        canYouStarWorkingInTwoWeeks = entity.getCanYouStarWorkingInTwoWeeks();
        whyYouCantWorkOrStoppedSearch = entity.getWhyYouCantWorkOrStoppedSearch();
    }
}
