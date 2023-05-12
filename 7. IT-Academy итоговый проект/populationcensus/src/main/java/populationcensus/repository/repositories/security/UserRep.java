package populationcensus.repository.repositories.security;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.security.User;

@Repository
public interface UserRep extends JpaRepository<User, Long> {
    User findByUsername(String username);
}
