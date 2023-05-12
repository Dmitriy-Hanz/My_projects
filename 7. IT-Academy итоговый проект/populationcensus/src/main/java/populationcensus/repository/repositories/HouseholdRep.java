package populationcensus.repository.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.repository.entity.FullAddressInfo;
import populationcensus.repository.entity.Household;
import populationcensus.repository.entity.Person;

import java.util.Optional;

@Repository
public interface HouseholdRep extends JpaRepository<Household, Long> {
    Optional<Household> findHouseholdByAccommodationsInfoId(Long accommodationsInfoId);
    Optional<Household> findHouseholdByAccommodationsInfo(AccommodationsInfo accommodationsInfo);

    Optional<Household> findHouseholdByFullAddressInfoId(Long fullAddressInfoId);
    Optional<Household> findHouseholdByFullAddressInfo(FullAddressInfo fullAddressInfo);

    Optional<Household> findHouseholdByPersonsContains(Person person);
}
