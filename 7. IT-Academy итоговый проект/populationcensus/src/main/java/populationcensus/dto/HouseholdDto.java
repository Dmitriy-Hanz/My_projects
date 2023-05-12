package populationcensus.dto;

import lombok.Data;
import populationcensus.repository.entity.Household;

import javax.validation.Valid;
import javax.validation.constraints.NotEmpty;
import javax.validation.constraints.NotNull;

@Data
public class HouseholdDto {
    private Long id;
    private Integer numberOfMembers;
    private Integer roomsCount;
    private String partOfRoom;
    private Boolean hasForeignResidents;
    private Boolean hasFarmFacilities;

    private AccommodationsInfoDto accommodationsInfo;
    private FullAddressInfoDto fullAddressInfo;

    public HouseholdDto() {
        accommodationsInfo = new AccommodationsInfoDto();
        fullAddressInfo = new FullAddressInfoDto();
    }

    public HouseholdDto(Household entity) {
        this.id = entity.getId();
        this.numberOfMembers = entity.getNumberOfMembers();
        this.roomsCount = entity.getRoomsCount();
        this.partOfRoom = entity.getPartOfRoom();
        this.hasForeignResidents = entity.getHasForeignResidents();
        this.hasFarmFacilities = entity.getHasFarmFacilities();

        if (entity.getAccommodationsInfo() != null){
            this.accommodationsInfo = new AccommodationsInfoDto(entity.getAccommodationsInfo());
        } else {
            this.accommodationsInfo = new AccommodationsInfoDto();
        }

        if (entity.getFullAddressInfo() != null){
            this.fullAddressInfo = new FullAddressInfoDto(entity.getFullAddressInfo());
        } else {
            this.fullAddressInfo = new FullAddressInfoDto();
        }

    }
}
