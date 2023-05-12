package populationcensus.repository.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.ChildrenInfo;
import populationcensus.repository.entity.Person;

import java.util.Optional;

@Repository
public interface ChildrenInfoRep extends JpaRepository<ChildrenInfo, Long> {
    Optional<ChildrenInfo> findChildrenInfoByPersonInChildrenInfoId(long personId);
    Optional<ChildrenInfo> findChildrenInfoByPersonInChildrenInfo(Person person);
    void deleteChildrenInfoByPersonInChildrenInfoId(long personId);
    void deleteChildrenInfoByPersonInChildrenInfo(Person person);
}
