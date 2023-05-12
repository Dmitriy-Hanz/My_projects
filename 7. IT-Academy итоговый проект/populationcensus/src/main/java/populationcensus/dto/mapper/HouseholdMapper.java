package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.repository.entity.Household;
import populationcensus.dto.HouseholdDto;

@Mapper(componentModel = "spring", uses = {AccommodationsInfoMapper.class, FullAddressInfoMapper.class})
public interface HouseholdMapper {
    HouseholdDto toHouseholdDto(Household entity);

    Household toHousehold(HouseholdDto entityDto);
}
