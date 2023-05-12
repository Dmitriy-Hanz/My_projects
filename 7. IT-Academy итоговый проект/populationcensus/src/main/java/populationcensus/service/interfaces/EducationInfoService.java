package populationcensus.service.interfaces;

import populationcensus.dto.EducationInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.repository.entity.EducationInfo;
import populationcensus.repository.entity.Person;

public interface EducationInfoService {

    void saveEducationInfo(EducationInfo entity);
    void saveEducationInfo(long personId, EducationInfo entity);
    void saveEducationInfo(Person person, EducationInfo entity);

    EducationInfoDto findEducationInfo(long educationInfoId);
    EducationInfoDto findEducationInfoByPersonId(long personId);
    EducationInfoDto findEducationInfoByPerson(Person person);

    void deleteEducationInfoById(long educationInfoId);
    void deleteEducationInfo(EducationInfo educationInfo);
    void deleteEducationInfoByPersonId(long personId);
    void deleteEducationInfoByPerson(Person person);

    PersonDto findLinkedPerson(long educationInfoId);
    PersonDto findLinkedPerson(EducationInfo educationInfo);

    void updateEducationInfo(EducationInfo entity);
}
