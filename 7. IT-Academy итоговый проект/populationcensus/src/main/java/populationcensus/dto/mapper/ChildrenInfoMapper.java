package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.repository.entity.ChildrenInfo;
import populationcensus.dto.ChildrenInfoDto;

@Mapper(componentModel = "spring")
public interface ChildrenInfoMapper {
    ChildrenInfoDto toChildrenInfoDto(ChildrenInfo entity);

    ChildrenInfo toChildrenInfo(ChildrenInfoDto entityDto);
}
