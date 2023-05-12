package populationcensus.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.repository.entity.FullAddressInfo;

import javax.persistence.Column;

@Data
@NoArgsConstructor
public class AccommodationsInfoDto {
    private Long id;
    private Integer ownerOfApartment;
    private Boolean isApartmentRented;
    private Integer apartmentType;
    private Integer areaOfFlat;
    private Integer waterPipes;
    private Integer canalization;
    private Boolean hasBathOrShower;
    private Integer hotWaterSupply;
    private Integer heating;
    private Integer cookingEquipment;

    public AccommodationsInfoDto(AccommodationsInfo entity) {
        this.id = entity.getId();
        this.ownerOfApartment = entity.getOwnerOfApartment();
        this.isApartmentRented = entity.getIsApartmentRented();
        this.apartmentType = entity.getApartmentType();
        this.areaOfFlat = entity.getAreaOfFlat();
        this.waterPipes = entity.getWaterPipes();
        this.canalization = entity.getCanalization();
        this.hasBathOrShower = entity.getHasBathOrShower();
        this.hotWaterSupply = entity.getHotWaterSupply();
        this.heating = entity.getHeating();
        this.cookingEquipment = entity.getCookingEquipment();
    }
}
