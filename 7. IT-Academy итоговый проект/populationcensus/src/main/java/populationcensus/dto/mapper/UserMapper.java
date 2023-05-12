package populationcensus.dto.mapper;

import org.mapstruct.Mapper;
import populationcensus.dto.security.SystemUserDto;
import populationcensus.dto.security.UserDto;
import populationcensus.repository.entity.security.User;

@Mapper(componentModel = "spring")
public interface UserMapper {
    UserDto toUserDto(User entity);
    SystemUserDto toSystemUserDto(User entity);

    User toUser(UserDto entityDto);
    User toUser(SystemUserDto entityDto);
}
