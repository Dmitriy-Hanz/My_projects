package populationcensus.dto.security;

import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.security.User;

@Data
@NoArgsConstructor
public class SystemUserDto {
    private Long id;

    private String username;
    private String decodedPassword;

    public SystemUserDto(User entity) {
        id = entity.getId();
        username = entity.getUsername();
        decodedPassword = entity.getDecodedPassword();
    }
}
