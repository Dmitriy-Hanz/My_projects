package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import populationcensus.dto.WorkInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.WorkInfoMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.WorkInfo;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.WorkInfoRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.interfaces.WorkInfoService;

import java.util.Optional;

@Service("workInfoService")
@AllArgsConstructor
public class WorkInfoServiceImpl implements WorkInfoService {

    private final WorkInfoRep workInfoRep;
    private final PersonRep personRep;
    private final PersonMapper personMapper;
    private final WorkInfoMapper workInfoMapper;

    @Override
    @Transactional
    public void saveWorkInfo(long personId, WorkInfo entity) {
        Person person = personRep.findById(personId).orElse(null);
        Person personWithEntity = personRep.findByWorkInfoId(entity.getId() == null ? 0 : entity.getId()).orElse(null);
        if (person != null && personWithEntity == null) {
            entity.setPersonInWorkInfo(person);
            workInfoRep.save(entity);
        }
    }

    @Override
    public void saveWorkInfo(Person person, WorkInfo entity) {
        saveWorkInfo(person.getId() == null? 0: person.getId(), entity);
    }

    @Override
    public void saveWorkInfo(WorkInfo entity) {
        if (entity.getPersonInWorkInfo() != null) {
            saveWorkInfo(entity.getPersonInWorkInfo(),entity);
        }
    }


    @Override
    public WorkInfoDto findWorkInfo(long workInfoId) {
        Optional<WorkInfo> result = workInfoRep.findById(workInfoId);
        return result
                .map(workInfoMapper::toWorkInfoDto)
                .orElse(null);
    }

    @Override
    public WorkInfoDto findWorkInfoByPersonId(long personId) {
        return workInfoRep.findByPersonInWorkInfoId(personId)
                .map(workInfoMapper::toWorkInfoDto)
                .orElse(null);
    }

    @Override
    public WorkInfoDto findWorkInfoByPerson(Person person) {
        return workInfoRep.findByPersonInWorkInfo(person)
                .map(workInfoMapper::toWorkInfoDto)
                .orElse(null);
    }


    @Override
    public void deleteWorkInfoById(long workInfoId) {
        workInfoRep.deleteById(workInfoId);
    }

    @Override
    public void deleteWorkInfo(WorkInfo workInfo) {
        workInfoRep.delete(workInfo);
    }

    @Override
    public void deleteWorkInfoByPersonId(long personId) {
        workInfoRep.deleteByPersonInWorkInfoId(personId);
    }

    @Override
    public void deleteWorkInfoByPerson(Person person) {
        workInfoRep.deleteByPersonInWorkInfo(person);
    }


    @Override
    public PersonDto findLinkedPerson(long workInfoId) {
        return personRep.findByWorkInfoId(workInfoId)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }

    @Override
    public PersonDto findLinkedPerson(WorkInfo workInfo) {
        return personRep.findByWorkInfo(workInfo)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }


    @Override
    @Transactional
    public void updateWorkInfo(WorkInfo workInfo) {
        Optional<WorkInfo> workInfoFromDB = workInfoRep.findById(workInfo.getId() == null? 0:workInfo.getId());
        if (workInfoFromDB.isPresent())
        {
            workInfo.setPersonInWorkInfo(workInfoFromDB.get().getPersonInWorkInfo());
            workInfoRep.save(workInfo);
        }
    }
}
