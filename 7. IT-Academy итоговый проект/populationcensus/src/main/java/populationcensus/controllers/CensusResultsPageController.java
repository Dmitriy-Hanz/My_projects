package populationcensus.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import populationcensus.repository.entity.Person;
import populationcensus.service.interfaces.PersonService;

import java.util.List;
import java.util.stream.Collectors;

@Controller
@RequiredArgsConstructor
public class CensusResultsPageController {

    private final PersonService personService;

    @GetMapping("/adminMain/censusResults")
    public String load(Model model) {
        List<Person> allDatabase = personService.findAll();

        boolean hasPersons = !allDatabase.isEmpty();
        model.addAttribute("hasPersons", hasPersons);
        if (hasPersons) {
            model.addAttribute("populationCountAndDensity", populationCountAndDensity(allDatabase));
            model.addAttribute("populationCountByAgeGroups", populationCountByAgeGroups(allDatabase));
            model.addAttribute("populationCountByMaritalStatus", populationCountByMaritalStatus(allDatabase));
            model.addAttribute("populationCountByBirthrate", populationCountByBirthrate(allDatabase));
            model.addAttribute("populationCountByEducationLvl", populationCountByEducationLvl(allDatabase));
            model.addAttribute("populationCountByNationality", populationCountByNationality(allDatabase));
            model.addAttribute("populationCountByNativeLanguage", populationCountByNativeLanguage(allDatabase));
        }
        return "censusResultsPage";
    }

    private Number[] populationCountAndDensity(List<Person> persons) {
        Number[] results = new Number[5];
        results[0] = persons.size();
        results[1] = (int) persons.stream()
                .map(Person::getHouseholdField)
                .filter(household -> !household.getFullAddressInfo().getCity().equals(""))
                .count();
        results[2] = (int) persons.stream()
                .map(Person::getHouseholdField)
                .filter(household -> household.getFullAddressInfo().getVillageName() != null)
                .filter(household -> !household.getFullAddressInfo().getVillageName().equals(""))
                .count();
        results[3] = (100 * (double) (int) results[1]) / (double) (int) results[0];
        results[4] = (100 * (double) (int) results[2]) / (double) (int) results[0];
        return results;
    }

    private Number[][] populationCountByAgeGroups(List<Person> persons) {
        Number[][] results = new Number[3][5];

        List<Person> cityPersons = persons.stream()
                .filter(person -> !person.getHouseholdField().getFullAddressInfo().getCity().equals(""))
                .collect(Collectors.toList());
        List<Person> villagePersons = persons.stream()
                .filter(person -> person.getHouseholdField().getFullAddressInfo().getVillageName() != null)
                .filter(person -> !person.getHouseholdField().getFullAddressInfo().getVillageName().equals(""))
                .collect(Collectors.toList());

        results[0][0] = persons.size();
        results[0][1] = (int) persons.stream()
                .filter(p -> p.getAge() < 16)
                .count();
        results[0][2] = (int) persons.stream()
                .filter(p -> (p.getGender() == 1 && p.getAge() < 63) || (p.getGender() == 2 && p.getAge() < 58))
                .count();
        results[0][3] = (int) persons.stream()
                .filter(p -> (p.getGender() == 1 && p.getAge() > 62) || (p.getGender() == 2 && p.getAge() > 57))
                .count();
        results[0][4] = (double) persons.stream()
                .map(Person::getAge)
                .reduce(Integer::sum)
                .orElse(0) / (persons.size() == 0 ? 1 : persons.size());

        results[1][0] = cityPersons.size();
        results[1][1] = (int) cityPersons.stream()
                .filter(p -> p.getAge() < 16)
                .count();
        results[1][2] = (int) cityPersons.stream()
                .filter(p -> (p.getGender() == 1 && p.getAge() < 63) || (p.getGender() == 2 && p.getAge() < 58))
                .count();
        results[1][3] = (int) cityPersons.stream()
                .filter(p -> (p.getGender() == 1 && p.getAge() > 62) || (p.getGender() == 2 && p.getAge() > 57))
                .count();
        results[1][4] = (double) cityPersons.stream()
                .map(Person::getAge)
                .reduce(Integer::sum)
                .orElse(0) / (cityPersons.size() == 0 ? 1 : cityPersons.size());

        results[2][0] = villagePersons.size();
        results[2][1] = (int) villagePersons.stream()
                .filter(person -> person.getAge() < 16)
                .count();
        results[2][2] = (int) villagePersons.stream()
                .filter(p -> (p.getGender() == 1 && p.getAge() < 63) || (p.getGender() == 2 && p.getAge() < 58))
                .count();
        results[2][3] = (int) villagePersons.stream()
                .filter(p -> (p.getGender() == 1 && p.getAge() > 62) || (p.getGender() == 2 && p.getAge() > 57))
                .count();
        results[2][4] = (double) villagePersons.stream()
                .map(Person::getAge)
                .reduce(Integer::sum)
                .orElse(0) / (villagePersons.size() == 0 ? 1 : villagePersons.size());

        return results;
    }

    private Number[][] populationCountByMaritalStatus(List<Person> persons) {
        Number[][] results = new Number[4][2];

        List<Person> onlyMen = persons.stream()
                .filter(p -> p.getGender() == 1)
                .filter(p -> p.getMaritalStatus() != null)
                .collect(Collectors.toList());

        List<Person> onlyWomen = persons.stream()
                .filter(p -> p.getGender() == 2)
                .filter(p -> p.getMaritalStatus() != null)
                .collect(Collectors.toList());


        results[0][0] = onlyMen.stream()
                .filter(p -> p.getMaritalStatus() == 1)
                .count();
        results[1][0] = onlyMen.stream()
                .filter(p -> p.getMaritalStatus() == 2)
                .count();
        results[2][0] = onlyMen.stream()
                .filter(p -> p.getMaritalStatus() == 3)
                .count();
        results[3][0] = onlyMen.stream()
                .filter(p -> p.getMaritalStatus() == 4)
                .count();

        results[0][1] = onlyWomen.stream()
                .filter(p -> p.getMaritalStatus() == 1)
                .count();
        results[1][1] = onlyWomen.stream()
                .filter(p -> p.getMaritalStatus() == 2)
                .count();
        results[2][1] = onlyWomen.stream()
                .filter(p -> p.getMaritalStatus() == 3)
                .count();
        results[3][1] = onlyWomen.stream()
                .filter(p -> p.getMaritalStatus() == 4)
                .count();

        return results;
    }

    private Number[][] populationCountByBirthrate(List<Person> persons) {
        Number[][] results = new Number[2][2];

        List<Person> onlyWomen = persons.stream()
                .filter(person -> person.getGender() == 2)
                .collect(Collectors.toList());

        results[0][0] = (int)onlyWomen.stream()
                .filter(p -> !p.getChildrenInfo().getNoChildren())
                .count();
        results[0][1] = (int)onlyWomen.stream()
                .filter(p -> p.getChildrenInfo().getChildrenPlans() == 1)
                .count();

        results[1][0] = (double)(int)results[0][0]*100 / (double)(onlyWomen.size() == 0 ? 1 : onlyWomen.size());
        results[1][1] = (double)(int)results[0][1]*100 / (double)(onlyWomen.size() == 0 ? 1 : onlyWomen.size());

        return results;
    }

    private Number[][] populationCountByEducationLvl(List<Person> persons) {
        Number[][] results = new Number[5][2];

        List<Person> onlyOlderThan15 = persons.stream()
                .filter(person -> person.getAge() > 14)
                .filter(person -> person.getEducationInfo().getLvlOfEducation() != null)
                .collect(Collectors.toList());

        results[0][0] = (int)onlyOlderThan15.stream()
                .filter(p -> p.getEducationInfo().getLvlOfEducation() == 5)
                .count();
        results[1][0] = (int)onlyOlderThan15.stream()
                .filter(p -> p.getEducationInfo().getLvlOfEducation() == 4)
                .count();
        results[2][0] = (int)onlyOlderThan15.stream()
                .filter(p -> p.getEducationInfo().getLvlOfEducation() == 3)
                .count();
        results[3][0] = (int)onlyOlderThan15.stream()
                .filter(p -> p.getEducationInfo().getLvlOfEducation() == 2)
                .count();
        results[4][0] = (int)onlyOlderThan15.stream()
                .filter(p -> p.getEducationInfo().getLvlOfEducation() == 1)
                .count();

        for (int i = 0; i < 5; i++) {
            results[i][1] = (double)(int)results[i][0]*100 / (double)(onlyOlderThan15.size() == 0 ? 1 : onlyOlderThan15.size());
        }

        return results;
    }

    private Number[][] populationCountByNationality(List<Person> persons) {
        Number[][] results = new Number[6][2];

        List<Person> onlyWithNationality = persons.stream()
                .filter(person -> person.getNationality() != null)
                .collect(Collectors.toList());

        results[0][0] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == 1)
                .count();
        results[1][0] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == 2)
                .count();
        results[2][0] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == 3)
                .count();
        results[3][0] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == 4)
                .count();
        results[4][0] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == 5)
                .count();
        results[5][0] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == 6)
                .count();

        for (int i = 0; i < 6; i++) {
            results[i][1] = (double)(int)results[i][0]*100 / (double)(onlyWithNationality.size() == 0 ? 1 : onlyWithNationality.size());
        }

        return results;
    }

    private Number[][] populationCountByNativeLanguage(List<Person> persons) {
        Number[][] nationalitiesCounts = populationCountByNationality(persons);
        Number[][] results = new Number[6][3];

        for (int i = 0; i < 6; i++) {
            results[i][0] = nationalitiesCounts[i][0];
        }

        List<Person> onlyWithNationality = persons.stream()
                .filter(person -> person.getNationality() != null)
                .collect(Collectors.toList());

        results[0][1] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == p.getNativeLanguage() & p.getNationality() == 1)
                .count();
        results[1][1] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == p.getNativeLanguage() & p.getNationality() == 2)
                .count();
        results[2][1] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == p.getNativeLanguage() & p.getNationality() == 3)
                .count();
        results[3][1] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == p.getNativeLanguage() & p.getNationality() == 4)
                .count();
        results[4][1] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == p.getNativeLanguage() & p.getNationality() == 5)
                .count();
        results[5][1] = (int)onlyWithNationality.stream()
                .filter(p -> p.getNationality() == p.getNativeLanguage() & p.getNationality() == 6)
                .count();

        for (int i = 0; i < 6; i++) {
            results[i][2] = (int)results[i][0] - (int)results[i][1];
        }

        return results;
    }
}
