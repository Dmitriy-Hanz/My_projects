package populationcensus.service.interfaces;

import populationcensus.dto.PersonDto;
import populationcensus.dto.WorkInfoDto;
import populationcensus.repository.entity.*;

public interface WorkInfoService {

    void saveWorkInfo(WorkInfo entity);
    void saveWorkInfo(long personId, WorkInfo entity);
    void saveWorkInfo(Person person, WorkInfo entity);

    WorkInfoDto findWorkInfo(long workInfoId);
    WorkInfoDto findWorkInfoByPersonId(long personId);
    WorkInfoDto findWorkInfoByPerson(Person person);

    void deleteWorkInfoById(long workInfoId);
    void deleteWorkInfo(WorkInfo workInfo);
    void deleteWorkInfoByPersonId(long personId);
    void deleteWorkInfoByPerson(Person person);

    PersonDto findLinkedPerson(long workInfoId);
    PersonDto findLinkedPerson(WorkInfo workInfo);

    void updateWorkInfo(WorkInfo entity);
}
