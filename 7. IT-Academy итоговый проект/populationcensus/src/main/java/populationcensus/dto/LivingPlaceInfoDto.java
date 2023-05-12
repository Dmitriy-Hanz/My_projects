package populationcensus.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.FullAddressInfo;
import populationcensus.repository.entity.LivingPlaceInfo;

import javax.persistence.Column;
import java.time.LocalDate;
import java.util.Date;

@Data
@NoArgsConstructor
public class LivingPlaceInfoDto {
    private Long id;
    private Boolean doYouLiveHereFromBirth;
    private LocalDate arrivalPeriod; private String stringArrivalPeriod;
    private Integer previousLivingPlace;
    private String regionOrDistrictName;
    private String cityOrPGTName;
    private Boolean isItVillage;
    private String nameOfPreviousCountry;
    private Integer reasonForArrivalAtPlace;


    public LivingPlaceInfoDto(LivingPlaceInfo entity) {
        id = entity.getId();
        doYouLiveHereFromBirth = entity.getDoYouLiveHereFromBirth();
        arrivalPeriod = entity.getArrivalPeriod(); stringArrivalPeriod = arrivalPeriod == null? "" : arrivalPeriod.toString();
        previousLivingPlace = entity.getPreviousLivingPlace();
        regionOrDistrictName = entity.getRegionOrDistrictName();
        cityOrPGTName = entity.getCityOrPGTName();
        isItVillage = entity.getIsItVillage();
        nameOfPreviousCountry = entity.getNameOfPreviousCountry();
        reasonForArrivalAtPlace = entity.getReasonForArrivalAtPlace();
    }
}
