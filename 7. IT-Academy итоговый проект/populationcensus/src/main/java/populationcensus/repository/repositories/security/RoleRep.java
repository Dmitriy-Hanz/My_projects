package populationcensus.repository.repositories.security;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.security.Role;

@Repository
public interface RoleRep extends JpaRepository<Role, Long> {
    Role findByName(String name);
}
