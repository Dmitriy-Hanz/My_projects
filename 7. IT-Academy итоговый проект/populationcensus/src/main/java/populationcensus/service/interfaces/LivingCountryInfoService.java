package populationcensus.service.interfaces;

import populationcensus.dto.LivingCountryInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.repository.entity.*;

public interface LivingCountryInfoService {

    void saveLivingCountryInfo(LivingCountryInfo entity);
    void saveLivingCountryInfo(long personId, LivingCountryInfo entity);
    void saveLivingCountryInfo(Person person, LivingCountryInfo entity);

    LivingCountryInfoDto findLivingCountryInfo(long livingCountryInfoId);
    LivingCountryInfoDto findLivingCountryInfoByPersonId(long personId);
    LivingCountryInfoDto findLivingCountryInfoByPerson(Person person);

    void deleteLivingCountryInfoById(long livingCountryInfoId);
    void deleteLivingCountryInfo(LivingCountryInfo livingCountryInfo);
    void deleteLivingCountryInfoByPersonId(long personId);
    void deleteLivingCountryInfoByPerson(Person person);

    PersonDto findLinkedPerson(long livingCountryInfoId);
    PersonDto findLinkedPerson(LivingCountryInfo livingCountryInfo);

    void updateLivingCountryInfo(LivingCountryInfo entity);
}
