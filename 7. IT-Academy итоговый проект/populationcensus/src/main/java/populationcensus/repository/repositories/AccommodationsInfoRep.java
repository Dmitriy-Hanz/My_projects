package populationcensus.repository.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.repository.entity.Household;

import java.util.Optional;

@Repository
public interface AccommodationsInfoRep extends JpaRepository<AccommodationsInfo,Long> {
    Optional<AccommodationsInfo> findAccommodationsInfoByHouseholdInAccommodationsInfoId(long householdId);
    Optional<AccommodationsInfo> findAccommodationsInfoByHouseholdInAccommodationsInfo(Household household);
    void deleteAccommodationsInfoByHouseholdInAccommodationsInfoId(long householdId);
    void deleteAccommodationsInfoByHouseholdInAccommodationsInfo(Household household);
}
