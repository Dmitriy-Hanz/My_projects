package populationcensus.dto.mapper;

import org.mapstruct.*;
import populationcensus.repository.entity.Person;
import populationcensus.dto.PersonDto;
import populationcensus.repository.entity.Person.PersonBuilder;

import java.time.Instant;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.Date;
import java.util.List;

@Mapper(componentModel = "spring", uses = {LivingPlaceInfoMapper.class, LivingCountryInfoMapper.class, EducationInfoMapper.class, WorkInfoMapper.class, ChildrenInfoMapper.class})
public abstract class PersonMapper {
    @BeforeMapping
    protected void dateParsing(PersonDto entityDto) {
        if (entityDto.getStringBirthdayDate().equals("")) {
            entityDto.setBirthdayDate(null);
        } else {
            entityDto.setBirthdayDate(LocalDate.parse(entityDto.getStringBirthdayDate()));
        }

        if (entityDto.getLivingPlaceInfo().getStringArrivalPeriod() != null) {
            if (entityDto.getLivingPlaceInfo().getStringArrivalPeriod().equals("")) {
                entityDto.getLivingPlaceInfo().setArrivalPeriod(null);
            } else {
                entityDto.getLivingPlaceInfo().setArrivalPeriod(LocalDate.parse(entityDto.getLivingPlaceInfo().getStringArrivalPeriod()));
            }
        }

        if (entityDto.getLivingCountryInfo().getStringArrivalPeriod() != null) {
            if (entityDto.getLivingCountryInfo().getStringArrivalPeriod().equals("")) {
                entityDto.getLivingCountryInfo().setArrivalPeriod(null);
            } else {
                entityDto.getLivingCountryInfo().setArrivalPeriod(LocalDate.parse(entityDto.getLivingCountryInfo().getStringArrivalPeriod()));
            }
        }
    }

    public abstract PersonDto toPersonDto(Person entity);

    public abstract Person toPerson(PersonDto entityDto);


    public abstract List<Person> toPersonList(List<PersonDto> entityDtoList);

    public abstract List<PersonDto> toPersonDtoList(List<Person> entityList);
}
