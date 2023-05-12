package populationcensus.service.interfaces;

import populationcensus.dto.FullAddressInfoDto;
import populationcensus.dto.HouseholdDto;
import populationcensus.repository.entity.*;

public interface FullAddressInfoService {

    void saveFullAddressInfo(long householdId, FullAddressInfo entity);
    void saveFullAddressInfo(Household household, FullAddressInfo entity);
    void saveFullAddressInfo(FullAddressInfo entity);

    FullAddressInfoDto findFullAddressInfo(long householdId);
    FullAddressInfoDto findFullAddressInfoByHouseholdId(long household);
    FullAddressInfoDto findFullAddressInfoByHousehold(Household household);

    void deleteFullAddressInfoById(long fullAddressInfo);
    void deleteFullAddressInfo(FullAddressInfo fullAddressInfo);
    void deleteFullAddressInfoByHouseholdId(long householdId);
    void deleteFullAddressInfoByHousehold(Household household);

    HouseholdDto findLinkedHousehold(long fullAddressInfoId);
    HouseholdDto findLinkedHousehold(FullAddressInfo fullAddressInfo);

    void updateFullAddressInfo(FullAddressInfo entity);
}
