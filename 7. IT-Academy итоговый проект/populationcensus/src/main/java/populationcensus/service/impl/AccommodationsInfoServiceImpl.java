package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import populationcensus.dto.AccommodationsInfoDto;
import populationcensus.dto.HouseholdDto;
import populationcensus.dto.mapper.AccommodationsInfoMapper;
import populationcensus.dto.mapper.HouseholdMapper;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.repository.entity.Household;
import populationcensus.repository.repositories.AccommodationsInfoRep;
import populationcensus.repository.repositories.HouseholdRep;
import populationcensus.service.interfaces.AccommodationsInfoService;

import java.util.List;
import java.util.Optional;

@Service("accommodationsInfoService")
@AllArgsConstructor
public class AccommodationsInfoServiceImpl implements AccommodationsInfoService {

    private final AccommodationsInfoRep accommodationsInfoRep;
    private final HouseholdRep householdRep;
    private final HouseholdMapper householdMapper;
    private final AccommodationsInfoMapper accommodationsInfoMapper;

    @Override
    @Transactional
    public void saveAccommodationsInfo(long householdId, AccommodationsInfo entity) {
        Household household = householdRep.findById(householdId).orElse(null);
        Household householdWithEntity = householdRep.findHouseholdByAccommodationsInfoId(entity.getId() == null ? 0 : entity.getId()).orElse(null);
        if (household != null && householdWithEntity == null) {
            entity.setHouseholdInAccommodationsInfo(household);
            accommodationsInfoRep.save(entity);
        }
    }

    @Override
    public void saveAccommodationsInfo(Household household, AccommodationsInfo entity) {
        saveAccommodationsInfo(household.getId() == null? 0: household.getId(), entity);
    }

    @Override
    public void saveAccommodationsInfo(AccommodationsInfo entity) {
        if (entity.getHouseholdInAccommodationsInfo() != null) {
            saveAccommodationsInfo(entity.getHouseholdInAccommodationsInfo(),entity);
        }
    }


    @Override
    public AccommodationsInfoDto findAccommodationsInfo(long accommodationsInfoId) {
        Optional<AccommodationsInfo> result = accommodationsInfoRep.findById(accommodationsInfoId);
        return result
                .map(accommodationsInfoMapper::toAccommodationsInfoDto)
                .orElse(null);
    }

    @Override
    public AccommodationsInfoDto findAccommodationsInfoByHouseholdId(long householdId) {
        return accommodationsInfoRep.findAccommodationsInfoByHouseholdInAccommodationsInfoId(householdId)
                .map(accommodationsInfoMapper::toAccommodationsInfoDto)
                .orElse(null);
    }

    @Override
    public AccommodationsInfoDto findAccommodationsInfoByHousehold(Household household) {
        return accommodationsInfoRep.findAccommodationsInfoByHouseholdInAccommodationsInfo(household)
                .map(accommodationsInfoMapper::toAccommodationsInfoDto)
                .orElse(null);
    }


    @Override
    public void deleteAccommodationsInfoById(long accommodationsInfoId) {
        accommodationsInfoRep.deleteById(accommodationsInfoId);
    }

    @Override
    public void deleteAccommodationsInfo(AccommodationsInfo accommodationsInfo) {
        accommodationsInfoRep.delete(accommodationsInfo);
    }

    @Override
    public void deleteAccommodationsInfoByHouseholdId(long householdId) {
        accommodationsInfoRep.deleteAccommodationsInfoByHouseholdInAccommodationsInfoId(householdId);
    }

    @Override
    public void deleteAccommodationsInfoByHousehold(Household household) {
        accommodationsInfoRep.deleteAccommodationsInfoByHouseholdInAccommodationsInfo(household);
    }


    @Override
    public HouseholdDto findLinkedHousehold(long accommodationsInfoId) {
        return householdRep.findHouseholdByAccommodationsInfoId(accommodationsInfoId)
                .map(householdMapper::toHouseholdDto)
                .orElse(null);
    }

    @Override
    public HouseholdDto findLinkedHousehold(AccommodationsInfo accommodationsInfo) {
        return householdRep.findHouseholdByAccommodationsInfo(accommodationsInfo)
                .map(householdMapper::toHouseholdDto)
                .orElse(null);
    }


    @Override
    @Transactional
    public void updateAccommodationsInfo(AccommodationsInfo accommodationsInfo) {
        Optional<AccommodationsInfo> accommodationsInfoFromDB = accommodationsInfoRep.findById(accommodationsInfo.getId() == null? 0:accommodationsInfo.getId());
        if (accommodationsInfoFromDB.isPresent())
        {
            accommodationsInfo.setHouseholdInAccommodationsInfo(accommodationsInfoFromDB.get().getHouseholdInAccommodationsInfo());
            accommodationsInfoRep.save(accommodationsInfo);
        }
    }
}
