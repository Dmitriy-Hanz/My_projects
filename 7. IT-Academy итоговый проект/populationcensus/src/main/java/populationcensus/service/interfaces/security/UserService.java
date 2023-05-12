package populationcensus.service.interfaces.security;

import populationcensus.repository.entity.security.User;

import java.util.List;

public interface UserService {
    List<User> findAll();
    List<User> findOnlyCustom();
    User findById(long entityId);

    void saveUser(User entity);

    void deleteUserById(long entityId);
    void deleteUser(User entity);

    void updateUser(User entity);
}
