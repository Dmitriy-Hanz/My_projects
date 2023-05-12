package populationcensus.service.impl.security;

import lombok.RequiredArgsConstructor;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import populationcensus.repository.entity.security.User;
import populationcensus.repository.repositories.security.UserRep;
import populationcensus.service.interfaces.security.UserService;

import javax.transaction.Transactional;
import java.util.List;

@Service
@RequiredArgsConstructor
public class UserDetailsServiceImpl implements UserDetailsService, UserService {

    private final UserRep userRep;
    private final BCryptPasswordEncoder bCryptPasswordEncoder;

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        User user = userRep.findByUsername(username);
        if (user == null) {
            throw new UsernameNotFoundException("Could not find user " + username + "!");
        }
        return new UserDetailsImpl(user);
    }


    @Override
    public List<User> findAll() {
        return userRep.findAll();
    }

    @Override
    public List<User> findOnlyCustom() {
        List<User> users = userRep.findAll();
        if (users.size() != 0){
            return userRep.findAll().stream()
                    .filter(user -> !user.getUsername().equals("admin"))
                    .toList();
        }
        return users;
    }

    @Override
    public User findById(long entityId) {
        return userRep.findById(entityId).orElse(null);
    }

    @Override
    public void saveUser(User entity) {
        entity.setPassword(bCryptPasswordEncoder.encode(entity.getDecodedPassword()));
        userRep.saveAndFlush(entity);
    }

    @Override
    public void deleteUserById(long entityId) {
        userRep.deleteById(entityId);
    }

    @Override
    public void deleteUser(User entity) {
        userRep.delete(entity);
    }

    @Override
    @Transactional
    public void updateUser(User entity) {
        entity.setPassword(bCryptPasswordEncoder.encode(entity.getDecodedPassword()));
        userRep.findById(entity.getId()).ifPresent(user -> {
            entity.setRoles(user.getRoles());
            userRep.saveAndFlush(entity);
        });
    }
}
