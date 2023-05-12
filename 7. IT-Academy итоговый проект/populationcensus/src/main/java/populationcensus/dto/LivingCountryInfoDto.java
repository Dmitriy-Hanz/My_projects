package populationcensus.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.LivingCountryInfo;
import populationcensus.repository.entity.LivingPlaceInfo;

import javax.persistence.Column;
import java.time.LocalDate;
import java.util.Date;

@Data
@NoArgsConstructor
public class LivingCountryInfoDto {
    private Long id;
    private Boolean didYouLiveInOtherCountry;
    private String nameOfCountryYouCameFrom;
    private LocalDate arrivalPeriod; private String stringArrivalPeriod;
    private Integer reasonForArrivalAtRB;
    private Integer infoAboutLeavingBelarus;
    private Integer reasonForLeavingBelarus;

    public LivingCountryInfoDto(LivingCountryInfo entity) {
        id = entity.getId();
        didYouLiveInOtherCountry = entity.getDidYouLiveInOtherCountry();
        nameOfCountryYouCameFrom = entity.getNameOfCountryYouCameFrom();
        arrivalPeriod = entity.getArrivalPeriod(); stringArrivalPeriod = arrivalPeriod == null? "" : arrivalPeriod.toString();
        reasonForArrivalAtRB = entity.getReasonForArrivalAtRB();
        infoAboutLeavingBelarus = entity.getInfoAboutLeavingBelarus();
        reasonForLeavingBelarus = entity.getReasonForLeavingBelarus();
    }
}
