package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.dto.LivingPlaceInfoDto;
import populationcensus.repository.entity.LivingPlaceInfo;

@Mapper(componentModel = "spring")
public interface LivingPlaceInfoMapper {
    LivingPlaceInfoDto toLivingPlaceInfoDto(LivingPlaceInfo entity);

    LivingPlaceInfo toLivingPlaceInfo(LivingPlaceInfoDto entityDto);
}
