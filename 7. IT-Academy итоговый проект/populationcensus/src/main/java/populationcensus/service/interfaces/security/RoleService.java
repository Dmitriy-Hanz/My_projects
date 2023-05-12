package populationcensus.service.interfaces.security;

import populationcensus.repository.entity.security.Role;

public interface RoleService {
    Role findByName(String name);
}
