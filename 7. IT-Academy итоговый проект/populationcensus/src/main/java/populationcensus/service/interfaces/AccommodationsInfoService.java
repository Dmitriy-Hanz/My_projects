package populationcensus.service.interfaces;

import populationcensus.dto.AccommodationsInfoDto;
import populationcensus.dto.HouseholdDto;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.repository.entity.Household;

public interface AccommodationsInfoService {

    void saveAccommodationsInfo(AccommodationsInfo entity);
    void saveAccommodationsInfo(long householdId, AccommodationsInfo entity);
    void saveAccommodationsInfo(Household household, AccommodationsInfo entity);

    AccommodationsInfoDto findAccommodationsInfo(long accommodationsInfoId);
    AccommodationsInfoDto findAccommodationsInfoByHouseholdId(long householdId);
    AccommodationsInfoDto findAccommodationsInfoByHousehold(Household household);

    void deleteAccommodationsInfoById(long accommodationsInfoId);
    void deleteAccommodationsInfo(AccommodationsInfo accommodationsInfo);
    void deleteAccommodationsInfoByHouseholdId(long householdId);
    void deleteAccommodationsInfoByHousehold(Household household);

    HouseholdDto findLinkedHousehold(long accommodationsInfoId);
    HouseholdDto findLinkedHousehold(AccommodationsInfo accommodationsInfo);

    void updateAccommodationsInfo(AccommodationsInfo entity);
}
