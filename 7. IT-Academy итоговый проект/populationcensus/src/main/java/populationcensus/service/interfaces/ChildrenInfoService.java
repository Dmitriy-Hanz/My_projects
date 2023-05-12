package populationcensus.service.interfaces;

import populationcensus.dto.ChildrenInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.repository.entity.ChildrenInfo;
import populationcensus.repository.entity.Person;

public interface ChildrenInfoService {

    void saveChildrenInfo(ChildrenInfo entity);
    void saveChildrenInfo(long personId, ChildrenInfo entity);
    void saveChildrenInfo(Person person, ChildrenInfo entity);

    ChildrenInfoDto findChildrenInfo(long childrenInfoId);
    ChildrenInfoDto findChildrenInfoByPersonId(long personId);
    ChildrenInfoDto findChildrenInfoByPerson(Person person);

    void deleteChildrenInfoById(long childrenInfoId);
    void deleteChildrenInfo(ChildrenInfo childrenInfo);
    void deleteChildrenInfoByPersonId(long personId);
    void deleteChildrenInfoByPerson(Person person);

    PersonDto findLinkedPerson(long childrenInfoId);
    PersonDto findLinkedPerson(ChildrenInfo childrenInfo);

    void updateChildrenInfo(ChildrenInfo entity);
}
