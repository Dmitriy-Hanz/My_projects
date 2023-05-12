package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import populationcensus.dto.FullAddressInfoDto;
import populationcensus.dto.HouseholdDto;
import populationcensus.dto.mapper.FullAddressInfoMapper;
import populationcensus.dto.mapper.HouseholdMapper;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.repository.entity.FullAddressInfo;
import populationcensus.repository.entity.FullAddressInfo;
import populationcensus.repository.entity.Household;
import populationcensus.repository.repositories.FullAddressInfoRep;
import populationcensus.repository.repositories.HouseholdRep;
import populationcensus.service.interfaces.FullAddressInfoService;

import java.util.Optional;

@Service("fullAddressInfoService")
@AllArgsConstructor
public class FullAddressInfoServiceImpl implements FullAddressInfoService {

    private final FullAddressInfoRep fullAddressInfoRep;
    private final HouseholdRep householdRep;
    private final HouseholdMapper householdMapper;
    private final FullAddressInfoMapper fullAddressInfoMapper;

    @Override
    @Transactional
    public void saveFullAddressInfo(long householdId, FullAddressInfo entity) {
        Household household = householdRep.findById(householdId).orElse(null);
        Household householdWithEntity = householdRep.findHouseholdByFullAddressInfoId(entity.getId() == null ? 0 : entity.getId()).orElse(null);
        if (household != null && householdWithEntity == null) {
            entity.setHouseholdInFullAddressInfo(household);
            fullAddressInfoRep.save(entity);
        }
    }

    @Override
    public void saveFullAddressInfo(Household household, FullAddressInfo entity) {
        saveFullAddressInfo(household.getId() == null? 0: household.getId(), entity);
    }
    
    @Override
    public void saveFullAddressInfo(FullAddressInfo entity) {
        if (entity.getHouseholdInFullAddressInfo() != null) {
            saveFullAddressInfo(entity.getHouseholdInFullAddressInfo(),entity);
        }
    }

    
    @Override
    public FullAddressInfoDto findFullAddressInfo(long fullAddressInfoId) {
        Optional<FullAddressInfo> result = fullAddressInfoRep.findById(fullAddressInfoId);
        return result
                .map(fullAddressInfoMapper::toFullAddressInfoDto)
                .orElse(null);
    }
    
    @Override
    public FullAddressInfoDto findFullAddressInfoByHouseholdId(long householdId) {
        return fullAddressInfoRep.findFullAddressInfoByHouseholdInFullAddressInfoId(householdId)
                .map(fullAddressInfoMapper::toFullAddressInfoDto)
                .orElse(null);
    }

    @Override
    public FullAddressInfoDto findFullAddressInfoByHousehold(Household household) {
        return fullAddressInfoRep.findFullAddressInfoByHouseholdInFullAddressInfo(household)
                .map(fullAddressInfoMapper::toFullAddressInfoDto)
                .orElse(null);
    }


    @Override
    public void deleteFullAddressInfoById(long fullAddressInfo) {
        fullAddressInfoRep.deleteById(fullAddressInfo);
    }

    @Override
    public void deleteFullAddressInfo(FullAddressInfo fullAddressInfo) {
        fullAddressInfoRep.delete(fullAddressInfo);
    }

    @Override
    public void deleteFullAddressInfoByHouseholdId(long householdId) {
        fullAddressInfoRep.deleteFullAddressInfoByHouseholdInFullAddressInfoId(householdId);
    }

    @Override
    public void deleteFullAddressInfoByHousehold(Household household) {
        fullAddressInfoRep.deleteFullAddressInfoByHouseholdInFullAddressInfo(household);
    }


    @Override
    public HouseholdDto findLinkedHousehold(long fullAddressInfoId) {
        return householdRep.findHouseholdByFullAddressInfoId(fullAddressInfoId)
                .map(householdMapper::toHouseholdDto)
                .orElse(null);
    }

    @Override
    public HouseholdDto findLinkedHousehold(FullAddressInfo fullAddressInfo) {
        return householdRep.findHouseholdByFullAddressInfo(fullAddressInfo)
                .map(householdMapper::toHouseholdDto)
                .orElse(null);
    }

    @Override
    @Transactional
    public void updateFullAddressInfo(FullAddressInfo fullAddressInfo) {
        Optional<FullAddressInfo> accommodationsInfoFromDB = fullAddressInfoRep.findById(fullAddressInfo.getId() == null? 0:fullAddressInfo.getId());
        if (accommodationsInfoFromDB.isPresent())
        {
            fullAddressInfo.setHouseholdInFullAddressInfo(accommodationsInfoFromDB.get().getHouseholdInFullAddressInfo());
            fullAddressInfoRep.save(fullAddressInfo);
        }
    }
}