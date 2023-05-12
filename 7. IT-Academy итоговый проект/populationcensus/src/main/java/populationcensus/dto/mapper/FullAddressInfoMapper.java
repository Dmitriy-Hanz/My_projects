package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.repository.entity.FullAddressInfo;
import populationcensus.dto.FullAddressInfoDto;

@Mapper(componentModel = "spring")
public interface FullAddressInfoMapper {
    FullAddressInfoDto toFullAddressInfoDto(FullAddressInfo entity);

    FullAddressInfo toFullAddressInfo(FullAddressInfoDto entityDto);
}
