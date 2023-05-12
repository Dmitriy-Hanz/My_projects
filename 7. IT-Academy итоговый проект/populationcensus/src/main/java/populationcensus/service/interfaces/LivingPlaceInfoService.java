package populationcensus.service.interfaces;

import populationcensus.dto.LivingPlaceInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.repository.entity.*;

public interface LivingPlaceInfoService {

    void saveLivingPlaceInfo(LivingPlaceInfo entity);
    void saveLivingPlaceInfo(long personId, LivingPlaceInfo entity);
    void saveLivingPlaceInfo(Person person, LivingPlaceInfo entity);

    LivingPlaceInfoDto findLivingPlaceInfo(long livingPlaceInfoId);
    LivingPlaceInfoDto findLivingPlaceInfoByPersonId(long personId);
    LivingPlaceInfoDto findLivingPlaceInfoByPerson(Person person);

    void deleteLivingPlaceInfoById(long livingPlaceInfoId);
    void deleteLivingPlaceInfo(LivingPlaceInfo livingPlaceInfo);
    void deleteLivingPlaceInfoByPersonId(long personId);
    void deleteLivingPlaceInfoByPerson(Person person);

    PersonDto findLinkedPerson(long livingPlaceInfoId);
    PersonDto findLinkedPerson(LivingPlaceInfo livingPlaceInfo);

    void updateLivingPlaceInfo(LivingPlaceInfo entity);
}
