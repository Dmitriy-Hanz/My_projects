package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import populationcensus.dto.ChildrenInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.ChildrenInfoMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.ChildrenInfo;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.ChildrenInfoRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.interfaces.ChildrenInfoService;

import java.util.Optional;

@Service("childrenInfoService")
@AllArgsConstructor
public class ChildrenInfoServiceImpl implements ChildrenInfoService {

    private final ChildrenInfoRep childrenInfoRep;
    private final PersonRep personRep;
    private final PersonMapper personMapper;
    private final ChildrenInfoMapper childrenInfoMapper;

    @Override
    @Transactional
    public void saveChildrenInfo(long personId, ChildrenInfo entity) {
        Person person = personRep.findById(personId).orElse(null);
        Person personWithEntity = personRep.findPersonByChildrenInfoId(entity.getId() == null ? 0 : entity.getId()).orElse(null);
        if (person != null && personWithEntity == null) {
            entity.setPersonInChildrenInfo(person);
            childrenInfoRep.save(entity);
        }
    }

    @Override
    public void saveChildrenInfo(Person person, ChildrenInfo entity) {
        saveChildrenInfo(person.getId() == null? 0: person.getId(), entity);
    }

    @Override
    public void saveChildrenInfo(ChildrenInfo entity) {
        if (entity.getPersonInChildrenInfo() != null) {
            saveChildrenInfo(entity.getPersonInChildrenInfo(),entity);
        }
    }


    @Override
    public ChildrenInfoDto findChildrenInfo(long childrenInfoId) {
        Optional<ChildrenInfo> result = childrenInfoRep.findById(childrenInfoId);
        return result
                .map(childrenInfoMapper::toChildrenInfoDto)
                .orElse(null);
    }

    @Override
    public ChildrenInfoDto findChildrenInfoByPersonId(long personId) {
        return childrenInfoRep.findChildrenInfoByPersonInChildrenInfoId(personId)
                .map(childrenInfoMapper::toChildrenInfoDto)
                .orElse(null);
    }

    @Override
    public ChildrenInfoDto findChildrenInfoByPerson(Person person) {
        return childrenInfoRep.findChildrenInfoByPersonInChildrenInfo(person)
                .map(childrenInfoMapper::toChildrenInfoDto)
                .orElse(null);
    }


    @Override
    public void deleteChildrenInfoById(long childrenInfoId) {
        childrenInfoRep.deleteById(childrenInfoId);
    }

    @Override
    public void deleteChildrenInfo(ChildrenInfo childrenInfo) {
        childrenInfoRep.delete(childrenInfo);
    }

    @Override
    public void deleteChildrenInfoByPersonId(long personId) {
        childrenInfoRep.deleteChildrenInfoByPersonInChildrenInfoId(personId);
    }

    @Override
    public void deleteChildrenInfoByPerson(Person person) {
        childrenInfoRep.deleteChildrenInfoByPersonInChildrenInfo(person);
    }


    @Override
    public PersonDto findLinkedPerson(long childrenInfoId) {
        return personRep.findPersonByChildrenInfoId(childrenInfoId)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }

    @Override
    public PersonDto findLinkedPerson(ChildrenInfo childrenInfo) {
        return personRep.findPersonByChildrenInfo(childrenInfo)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }


    @Override
    @Transactional
    public void updateChildrenInfo(ChildrenInfo childrenInfo) {
        Optional<ChildrenInfo> childrenInfoFromDB = childrenInfoRep.findById(childrenInfo.getId() == null? 0:childrenInfo.getId());
        if (childrenInfoFromDB.isPresent())
        {
            childrenInfo.setPersonInChildrenInfo(childrenInfoFromDB.get().getPersonInChildrenInfo());
            childrenInfoRep.save(childrenInfo);
        }
    }
}
