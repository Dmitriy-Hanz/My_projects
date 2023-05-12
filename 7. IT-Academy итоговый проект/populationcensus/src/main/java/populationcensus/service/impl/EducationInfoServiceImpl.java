package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import populationcensus.dto.EducationInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.EducationInfoMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.EducationInfo;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.EducationInfoRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.interfaces.EducationInfoService;

import java.util.Optional;

@Service("educationInfoService")
@AllArgsConstructor
public class EducationInfoServiceImpl implements EducationInfoService {

    private final EducationInfoRep educationInfoRep;
    private final PersonRep personRep;
    private final PersonMapper personMapper;
    private final EducationInfoMapper educationInfoMapper;

    @Override
    @Transactional
    public void saveEducationInfo(long personId, EducationInfo entity) {
        Person person = personRep.findById(personId).orElse(null);
        Person personWithEntity = personRep.findPersonByEducationInfoId(entity.getId() == null ? 0 : entity.getId()).orElse(null);
        if (person != null && personWithEntity == null) {
            entity.setPersonInEducationInfo(person);
            educationInfoRep.save(entity);
        }
    }

    @Override
    public void saveEducationInfo(Person person, EducationInfo entity) {
        saveEducationInfo(person.getId() == null? 0: person.getId(), entity);
    }

    @Override
    public void saveEducationInfo(EducationInfo entity) {
        if (entity.getPersonInEducationInfo() != null) {
            saveEducationInfo(entity.getPersonInEducationInfo(),entity);
        }
    }


    @Override
    public EducationInfoDto findEducationInfo(long educationInfoId) {
        Optional<EducationInfo> result = educationInfoRep.findById(educationInfoId);
        return result
                .map(educationInfoMapper::toEducationInfoDto)
                .orElse(null);
    }

    @Override
    public EducationInfoDto findEducationInfoByPersonId(long personId) {
        return educationInfoRep.findEducationInfoByPersonInEducationInfoId(personId)
                .map(educationInfoMapper::toEducationInfoDto)
                .orElse(null);
    }

    @Override
    public EducationInfoDto findEducationInfoByPerson(Person person) {
        return educationInfoRep.findEducationInfoByPersonInEducationInfo(person)
                .map(educationInfoMapper::toEducationInfoDto)
                .orElse(null);
    }


    @Override
    public void deleteEducationInfoById(long educationInfoId) {
        educationInfoRep.deleteById(educationInfoId);
    }

    @Override
    public void deleteEducationInfo(EducationInfo educationInfo) {
        educationInfoRep.delete(educationInfo);
    }

    @Override
    public void deleteEducationInfoByPersonId(long personId) {
        educationInfoRep.deleteEducationInfoByPersonInEducationInfoId(personId);
    }

    @Override
    public void deleteEducationInfoByPerson(Person person) {
        educationInfoRep.deleteEducationInfoByPersonInEducationInfo(person);
    }


    @Override
    public PersonDto findLinkedPerson(long educationInfoId) {
        return personRep.findPersonByEducationInfoId(educationInfoId)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }

    @Override
    public PersonDto findLinkedPerson(EducationInfo educationInfo) {
        return personRep.findPersonByEducationInfo(educationInfo)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }


    @Override
    @Transactional
    public void updateEducationInfo(EducationInfo educationInfo) {
        Optional<EducationInfo> educationInfoFromDB = educationInfoRep.findById(educationInfo.getId() == null? 0:educationInfo.getId());
        if (educationInfoFromDB.isPresent())
        {
            educationInfo.setPersonInEducationInfo(educationInfoFromDB.get().getPersonInEducationInfo());
            educationInfoRep.save(educationInfo);
        }
    }
}
