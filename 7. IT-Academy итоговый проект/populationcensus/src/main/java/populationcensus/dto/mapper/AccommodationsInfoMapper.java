package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.dto.AccommodationsInfoDto;

@Mapper(componentModel = "spring")
public interface AccommodationsInfoMapper {
    AccommodationsInfoDto toAccommodationsInfoDto(AccommodationsInfo entity);

    AccommodationsInfo toAccommodationsInfo(AccommodationsInfoDto entityDto);
}
