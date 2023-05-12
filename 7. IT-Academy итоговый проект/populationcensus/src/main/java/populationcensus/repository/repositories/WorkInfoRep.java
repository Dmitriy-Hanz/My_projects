package populationcensus.repository.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.WorkInfo;
import populationcensus.repository.entity.Person;

import java.util.Optional;

@Repository
public interface WorkInfoRep extends JpaRepository<WorkInfo, Long> {
    Optional<WorkInfo> findByPersonInWorkInfoId(long personId);
    Optional<WorkInfo> findByPersonInWorkInfo(Person person);
    void deleteByPersonInWorkInfoId(long personId);
    void deleteByPersonInWorkInfo(Person person);
}
