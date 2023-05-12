package populationcensus.dto;

import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.FullAddressInfo;

@Data
@NoArgsConstructor
public class FullAddressInfoDto {
    private Long id;
    private String region;
    private String regionDistrict;
    private String city;
    private String cityDistrict;
    private String villageCouncil;
    private String villageName;
    private String streetAvenueOther;
    private String houseNumber;
    private String frameNumber;
    private String flatOrRoomNumber;
    private String ownerFIO;


    public FullAddressInfoDto(FullAddressInfo entity) {
        this.id = entity.getId();
        this.city = entity.getCity();
        this.cityDistrict = entity.getCityDistrict();
        this.region = entity.getRegion();
        this.regionDistrict = entity.getRegionDistrict();
        this.villageCouncil = entity.getVillageCouncil();
        this.villageName = entity.getVillageName();
        this.streetAvenueOther = entity.getStreetAvenueOther();
        this.houseNumber = entity.getHouseNumber();
        this.ownerFIO = entity.getOwnerFIO();
        this.frameNumber = entity.getFrameNumber();
        this.flatOrRoomNumber = entity.getFlatOrRoomNumber();
    }
}
