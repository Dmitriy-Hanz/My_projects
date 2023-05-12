package populationcensus.service.interfaces;

import populationcensus.dto.HouseholdDto;
import populationcensus.dto.PersonDto;
import populationcensus.repository.entity.Household;

import java.util.List;

public interface HouseholdService {

    void saveHousehold(Household entity);
    void saveHousehold(HouseholdDto householdDto, List<PersonDto> persons);

    HouseholdDto findHousehold(long householdId);
    HouseholdDto findHouseholdByPersonId(long personId);

    void deleteHouseholdById(long householdId);
}
