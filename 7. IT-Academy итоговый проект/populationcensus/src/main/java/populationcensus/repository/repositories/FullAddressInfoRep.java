package populationcensus.repository.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import populationcensus.repository.entity.FullAddressInfo;
import populationcensus.repository.entity.Household;

import java.util.Optional;

@Repository
public interface FullAddressInfoRep extends JpaRepository<FullAddressInfo,Long> {
    Optional<FullAddressInfo> findFullAddressInfoByHouseholdInFullAddressInfoId(long householdId);
    Optional<FullAddressInfo> findFullAddressInfoByHouseholdInFullAddressInfo(Household household);
    void deleteFullAddressInfoByHouseholdInFullAddressInfoId(long householdId);
    void deleteFullAddressInfoByHouseholdInFullAddressInfo(Household household);
}
