package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.dto.WorkInfoDto;
import populationcensus.repository.entity.WorkInfo;

@Mapper(componentModel = "spring")
public interface WorkInfoMapper {
    WorkInfoDto toWorkInfoDto(WorkInfo entity);

    WorkInfo toWorkInfo(WorkInfoDto entityDto);
}
