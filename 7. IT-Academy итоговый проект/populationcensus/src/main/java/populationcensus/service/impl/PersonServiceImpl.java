package populationcensus.service.impl;

import lombok.AllArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.interfaces.PersonService;

import java.util.Collections;
import java.util.List;

@Service("personService")
@AllArgsConstructor
public class PersonServiceImpl implements PersonService {

    private final PersonRep personRep;
    private final PersonMapper personMapper;

    @Override
    public List<Person> findAll() {
        return personRep.findAll();
    }

    @Override
    public Page<Person> findAllAndPaginate(Pageable pageable) {
        int pageSize = pageable.getPageSize();
        int currentPage = pageable.getPageNumber();
        int startItem = currentPage * pageSize;
        List<Person> list;
        List<Person> persons = personRep.findAll();

        if (persons.size() < startItem) {
            list = Collections.emptyList();
        } else {
            int toIndex = Math.min(startItem + pageSize, persons.size());
            list = persons.subList(startItem, toIndex);
        }

        Page<Person> personPage = new PageImpl<>(list, PageRequest.of(currentPage, pageSize), persons.size());

        return personPage;
    }

    @Override
    public PersonDto findPerson(long personId) {
        return personRep.findById(personId)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }

    @Override
    public PersonDto findPersonByPassportID(String passportID) {
        return personRep.findByPassportID(passportID)
                .map(personMapper::toPersonDto)
                .orElse(null);
    }
}
