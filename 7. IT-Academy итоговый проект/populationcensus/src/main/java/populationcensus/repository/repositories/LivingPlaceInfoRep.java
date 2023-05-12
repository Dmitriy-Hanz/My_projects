package populationcensus.repository.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.LivingPlaceInfo;
import populationcensus.repository.entity.Person;

import java.util.Optional;

@Repository
public interface LivingPlaceInfoRep extends JpaRepository<LivingPlaceInfo, Long> {
    Optional<LivingPlaceInfo> findByPersonInLivingPlaceInfoId(long personId);
    Optional<LivingPlaceInfo> findByPersonInLivingPlaceInfo(Person person);
    void deleteByPersonInLivingPlaceInfoId(long personId);
    void deleteByPersonInLivingPlaceInfo(Person person);
}
