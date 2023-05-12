package populationcensus.service.impl.security;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import populationcensus.repository.entity.security.Role;
import populationcensus.repository.repositories.security.RoleRep;
import populationcensus.service.interfaces.security.RoleService;

@Service
@RequiredArgsConstructor
public class RoleServiceImpl implements RoleService {

    private final RoleRep roleRep;

    @Override
    public Role findByName(String name) {
        return roleRep.findByName(name);
    }
}
