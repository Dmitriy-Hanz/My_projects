package populationcensus.dto.security;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.security.User;

import javax.validation.constraints.NotEmpty;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class UserDto {
    private Long id;
    @NotEmpty(message = "Поле «Логин» должно быть заполнено")
    private String username;
    @NotEmpty(message = "Поле «Пароль» должно быть заполнено")
    private String decodedPassword;

    private String name;
    private String surname;
    private String fathername;

    public UserDto(User entity) {
        id = entity.getId();
        username = entity.getUsername();
        decodedPassword = entity.getDecodedPassword();
        name = entity.getName();
        surname = entity.getSurname();
        fathername = entity.getFathername();
    }
}
