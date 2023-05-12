package populationcensus.repository.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.EducationInfo;
import populationcensus.repository.entity.Person;

import java.util.Optional;

@Repository
public interface EducationInfoRep extends JpaRepository<EducationInfo, Long> {
    Optional<EducationInfo> findEducationInfoByPersonInEducationInfoId(long personId);
    Optional<EducationInfo> findEducationInfoByPersonInEducationInfo(Person person);
    void deleteEducationInfoByPersonInEducationInfoId(long personId);
    void deleteEducationInfoByPersonInEducationInfo(Person person);
}
