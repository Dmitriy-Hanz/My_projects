package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.dto.EducationInfoDto;
import populationcensus.repository.entity.EducationInfo;

@Mapper(componentModel = "spring")
public interface EducationInfoMapper {
    EducationInfoDto toEducationInfoDto(EducationInfo entity);

    EducationInfo toEducationInfo(EducationInfoDto entityDto);
}
