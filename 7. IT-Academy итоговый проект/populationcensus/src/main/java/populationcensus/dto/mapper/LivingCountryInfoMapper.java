package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.dto.LivingCountryInfoDto;
import populationcensus.repository.entity.LivingCountryInfo;

@Mapper(componentModel = "spring")
public interface LivingCountryInfoMapper {
    LivingCountryInfoDto toLivingCountryInfoDto(LivingCountryInfo entity);

    LivingCountryInfo toLivingCountryInfo(LivingCountryInfoDto entityDto);
}
