package populationcensus.repository.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.*;

import java.util.Optional;

@Repository
public interface PersonRep extends JpaRepository<Person, Long> {
    Optional<Person> findByPassportID(String passportID);

    Optional<Person> findByLivingPlaceInfoId(long livingPlaceInfoId);
    Optional<Person> findByLivingPlaceInfo(LivingPlaceInfo livingPlaceInfo);

    Optional<Person> findByLivingCountryInfoId(long livingCountryInfoId);
    Optional<Person> findByLivingCountryInfo(LivingCountryInfo livingCountryInfo);

    Optional<Person> findPersonByEducationInfoId(long educationInfoId);
    Optional<Person> findPersonByEducationInfo(EducationInfo educationInfo);

    Optional<Person> findByWorkInfoId(long workInfoId);
    Optional<Person> findByWorkInfo(WorkInfo workInfo);

    Optional<Person> findPersonByChildrenInfoId(long childrenInfoId);
    Optional<Person> findPersonByChildrenInfo(ChildrenInfo childrenInfo);
}
