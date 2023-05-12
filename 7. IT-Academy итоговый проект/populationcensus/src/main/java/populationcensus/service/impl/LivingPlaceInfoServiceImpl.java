package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import populationcensus.dto.LivingPlaceInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.LivingPlaceInfoMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.LivingPlaceInfo;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.LivingPlaceInfoRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.interfaces.LivingPlaceInfoService;

import java.util.Optional;

@Service("livingPlaceInfoService")
@AllArgsConstructor
public class LivingPlaceInfoServiceImpl implements LivingPlaceInfoService {

    private final LivingPlaceInfoRep livingPlaceInfoRep;
    private final PersonRep personRep;
    private final PersonMapper personMapper;
    private final LivingPlaceInfoMapper livingPlaceInfoMapper;

    @Override
    @Transactional
    public void saveLivingPlaceInfo(long personId, LivingPlaceInfo entity) {
        Person person = personRep.findById(personId).orElse(null);
        Person personWithEntity = personRep.findByLivingPlaceInfoId(entity.getId() == null ? 0 : entity.getId()).orElse(null);
        if (person != null && personWithEntity == null) {
            entity.setPersonInLivingPlaceInfo(person);
            livingPlaceInfoRep.save(entity);
        }
    }

    @Override
    public void saveLivingPlaceInfo(Person person, LivingPlaceInfo entity) {
        saveLivingPlaceInfo(person.getId() == null? 0: person.getId(), entity);
    }

    @Override
    public void saveLivingPlaceInfo(LivingPlaceInfo entity) {
        if (entity.getPersonInLivingPlaceInfo() != null) {
            saveLivingPlaceInfo(entity.getPersonInLivingPlaceInfo(),entity);
        }
    }


    @Override
    public LivingPlaceInfoDto findLivingPlaceInfo(long livingPlaceInfoId) {
        Optional<LivingPlaceInfo> result = livingPlaceInfoRep.findById(livingPlaceInfoId);
        return result
                .map(livingPlaceInfoMapper::toLivingPlaceInfoDto)
                .orElse(null);
    }

    @Override
    public LivingPlaceInfoDto findLivingPlaceInfoByPersonId(long personId) {
        return livingPlaceInfoRep.findByPersonInLivingPlaceInfoId(personId)
                .map(livingPlaceInfoMapper::toLivingPlaceInfoDto)
                .orElse(null);
    }

    @Override
    public LivingPlaceInfoDto findLivingPlaceInfoByPerson(Person person) {
        return livingPlaceInfoRep.findByPersonInLivingPlaceInfo(person)
                .map(livingPlaceInfoMapper::toLivingPlaceInfoDto)
                .orElse(null);
    }


    @Override
    public void deleteLivingPlaceInfoById(long livingPlaceInfoId) {
        livingPlaceInfoRep.deleteById(livingPlaceInfoId);
    }

    @Override
    public void deleteLivingPlaceInfo(LivingPlaceInfo livingPlaceInfo) {
        livingPlaceInfoRep.delete(livingPlaceInfo);
    }

    @Override
    public void deleteLivingPlaceInfoByPersonId(long personId) {
        livingPlaceInfoRep.deleteByPersonInLivingPlaceInfoId(personId);
    }

    @Override
    public void deleteLivingPlaceInfoByPerson(Person person) {
        livingPlaceInfoRep.deleteByPersonInLivingPlaceInfo(person);
    }


    @Override
    public PersonDto findLinkedPerson(long livingPlaceInfoId) {
        return personRep.findByLivingPlaceInfoId(livingPlaceInfoId)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }

    @Override
    public PersonDto findLinkedPerson(LivingPlaceInfo livingPlaceInfo) {
        return personRep.findByLivingPlaceInfo(livingPlaceInfo)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }


    @Override
    @Transactional
    public void updateLivingPlaceInfo(LivingPlaceInfo livingPlaceInfo) {
        Optional<LivingPlaceInfo> livingPlaceInfoFromDB = livingPlaceInfoRep.findById(livingPlaceInfo.getId() == null? 0:livingPlaceInfo.getId());
        if (livingPlaceInfoFromDB.isPresent())
        {
            livingPlaceInfo.setPersonInLivingPlaceInfo(livingPlaceInfoFromDB.get().getPersonInLivingPlaceInfo());
            livingPlaceInfoRep.save(livingPlaceInfo);
        }
    }
}
