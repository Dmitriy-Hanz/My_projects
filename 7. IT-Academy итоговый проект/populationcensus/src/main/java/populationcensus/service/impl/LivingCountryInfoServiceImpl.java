package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import populationcensus.dto.LivingCountryInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.LivingCountryInfoMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.LivingCountryInfo;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.LivingCountryInfoRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.interfaces.LivingCountryInfoService;

import java.util.Optional;

@Service("livingCountryInfoService")
@AllArgsConstructor
public class LivingCountryInfoServiceImpl implements LivingCountryInfoService {

    private final LivingCountryInfoRep livingCountryInfoRep;
    private final PersonRep personRep;
    private final PersonMapper personMapper;
    private final LivingCountryInfoMapper livingCountryInfoMapper;

    @Override
    @Transactional
    public void saveLivingCountryInfo(long personId, LivingCountryInfo entity) {
        Person person = personRep.findById(personId).orElse(null);
        Person personWithEntity = personRep.findByLivingCountryInfoId(entity.getId() == null ? 0 : entity.getId()).orElse(null);
        if (person != null && personWithEntity == null) {
            entity.setPersonInLivingCountryInfo(person);
            livingCountryInfoRep.save(entity);
        }
    }

    @Override
    public void saveLivingCountryInfo(Person person, LivingCountryInfo entity) {
        saveLivingCountryInfo(person.getId() == null? 0: person.getId(), entity);
    }

    @Override
    public void saveLivingCountryInfo(LivingCountryInfo entity) {
        if (entity.getPersonInLivingCountryInfo() != null) {
            saveLivingCountryInfo(entity.getPersonInLivingCountryInfo(),entity);
        }
    }


    @Override
    public LivingCountryInfoDto findLivingCountryInfo(long livingCountryInfoId) {
        Optional<LivingCountryInfo> result = livingCountryInfoRep.findById(livingCountryInfoId);
        return result
                .map(livingCountryInfoMapper::toLivingCountryInfoDto)
                .orElse(null);
    }

    @Override
    public LivingCountryInfoDto findLivingCountryInfoByPersonId(long personId) {
        return livingCountryInfoRep.findByPersonInLivingCountryInfoId(personId)
                .map(livingCountryInfoMapper::toLivingCountryInfoDto)
                .orElse(null);
    }

    @Override
    public LivingCountryInfoDto findLivingCountryInfoByPerson(Person person) {
        return livingCountryInfoRep.findByPersonInLivingCountryInfo(person)
                .map(livingCountryInfoMapper::toLivingCountryInfoDto)
                .orElse(null);
    }


    @Override
    public void deleteLivingCountryInfoById(long livingCountryInfoId) {
        livingCountryInfoRep.deleteById(livingCountryInfoId);
    }

    @Override
    public void deleteLivingCountryInfo(LivingCountryInfo livingCountryInfo) {
        livingCountryInfoRep.delete(livingCountryInfo);
    }

    @Override
    public void deleteLivingCountryInfoByPersonId(long personId) {
        livingCountryInfoRep.deleteByPersonInLivingCountryInfoId(personId);
    }

    @Override
    public void deleteLivingCountryInfoByPerson(Person person) {
        livingCountryInfoRep.deleteByPersonInLivingCountryInfo(person);
    }


    @Override
    public PersonDto findLinkedPerson(long livingCountryInfoId) {
        return personRep.findByLivingCountryInfoId(livingCountryInfoId)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }

    @Override
    public PersonDto findLinkedPerson(LivingCountryInfo livingCountryInfo) {
        return personRep.findByLivingCountryInfo(livingCountryInfo)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }


    @Override
    @Transactional
    public void updateLivingCountryInfo(LivingCountryInfo livingCountryInfo) {
        Optional<LivingCountryInfo> livingCountryInfoFromDB = livingCountryInfoRep.findById(livingCountryInfo.getId() == null? 0:livingCountryInfo.getId());
        if (livingCountryInfoFromDB.isPresent())
        {
            livingCountryInfo.setPersonInLivingCountryInfo(livingCountryInfoFromDB.get().getPersonInLivingCountryInfo());
            livingCountryInfoRep.save(livingCountryInfo);
        }
    }
}
