package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import populationcensus.dto.HouseholdDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.HouseholdMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.Household;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.HouseholdRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.interfaces.HouseholdService;

import java.util.List;
import java.util.Optional;

@Service("householdService")
@AllArgsConstructor
public class HouseholdServiceImpl implements HouseholdService {

    private final HouseholdRep householdRep;
    private final PersonRep personRep;
    private final HouseholdMapper householdMapper;
    private final PersonMapper personMapper;

    @Override
    public void saveHousehold(Household entity) {
        if (entity.getAccommodationsInfo() != null){
            if(entity.getAccommodationsInfo().getHouseholdInAccommodationsInfo() == null){
                entity.getAccommodationsInfo().setHouseholdInAccommodationsInfo(entity);
            }
        }
        if (entity.getFullAddressInfo() != null){
            if(entity.getFullAddressInfo().getHouseholdInFullAddressInfo() == null){
                entity.getFullAddressInfo().setHouseholdInFullAddressInfo(entity);
            }
        }

        if(entity.getPersons() != null){
            List<Person> tempPersons = entity.getPersons();
            for (Person tempPerson : tempPersons) {
                if (tempPerson.getHouseholdField() == null) {
                    tempPerson.setHouseholdField(entity);
                }

                if (tempPerson.getLivingPlaceInfo() != null) {
                    if (tempPerson.getLivingPlaceInfo().getPersonInLivingPlaceInfo() == null) {
                        tempPerson.getLivingPlaceInfo().setPersonInLivingPlaceInfo(tempPerson);
                    }
                }
                if (tempPerson.getLivingCountryInfo() != null) {
                    if (tempPerson.getLivingCountryInfo().getPersonInLivingCountryInfo() == null) {
                        tempPerson.getLivingCountryInfo().setPersonInLivingCountryInfo(tempPerson);
                    }
                }
                if (tempPerson.getEducationInfo() != null) {
                    if (tempPerson.getEducationInfo().getPersonInEducationInfo() == null) {
                        tempPerson.getEducationInfo().setPersonInEducationInfo(tempPerson);
                    }
                }
                if (tempPerson.getWorkInfo() != null) {
                    if (tempPerson.getWorkInfo().getPersonInWorkInfo() == null) {
                        tempPerson.getWorkInfo().setPersonInWorkInfo(tempPerson);
                    }
                }
                if (tempPerson.getChildrenInfo() != null) {
                    if (tempPerson.getChildrenInfo().getPersonInChildrenInfo() == null) {
                        tempPerson.getChildrenInfo().setPersonInChildrenInfo(tempPerson);
                    }
                }
            }
        }
        householdRep.save(entity);
    }

    @Override
    public void saveHousehold(HouseholdDto householdDto, List<PersonDto> persons) {
        Household householdResult = householdMapper.toHousehold(householdDto);
        List<Person> personsResult = personMapper.toPersonList(persons);
        householdResult.setPersons(personsResult);

        saveHousehold(householdResult);
    }

    @Override
    public HouseholdDto findHousehold(long householdId) {
        Optional<Household> result = householdRep.findById(householdId);
        return result
                .map(householdMapper::toHouseholdDto)
                .orElse(null);
    }
    @Override
    @Transactional
    public HouseholdDto findHouseholdByPersonId(long personId) {
        Optional<Person> optionalPerson = personRep.findById(personId);
        if (optionalPerson.isPresent()){
            Optional<Household> result = householdRep.findHouseholdByPersonsContains(optionalPerson.get());
            return result
                    .map(householdMapper::toHouseholdDto)
                    .orElse(null);
        }
        return null;
    }

    @Override
    public void deleteHouseholdById(long householdId) {
        householdRep.deleteById(householdId);
    }
}
