package populationcensus.serviceTests;

import org.junit.Before;
import org.junit.Test;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.extension.ExtendWith;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.context.junit4.SpringRunner;
import populationcensus.Consts;
import populationcensus.dto.EducationInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.EducationInfoMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.EducationInfo;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.EducationInfoRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.impl.EducationInfoServiceImpl;

import java.util.Optional;

import static org.mockito.Mockito.any;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
@RunWith(SpringRunner.class)
@TestPropertySource(locations = "classpath:applicationTest.yml")
public class EducationInfoServiceTest {
    @Mock
    private EducationInfoRep educationInfoRep;
    @Mock
    private PersonRep personRep;
    @Mock
    private EducationInfoMapper educationInfoMapper;
    @Mock
    private PersonMapper personMapper;

    @InjectMocks
    private EducationInfoServiceImpl educationInfoService;

    private Person person;
    private PersonDto personDto;
    private EducationInfo educationInfo;
    private EducationInfoDto educationInfoDto;

    @Before
    public void presets(){
        person = Person.builder()
                .id(Consts.Test.PERSON_ID)
                .age(Consts.Test.PERSON_AGE)
                .name(Consts.Test.PERSON_NAME)
                .build();

        educationInfo = EducationInfo.builder()
                .id(Consts.Test.EDUCATION_INFO_ID)
                .lvlOfEducation(Consts.Test.EDUCATION_INFO_LVL_OF_EDUCATION)
                .personInEducationInfo(person)
                .build();

        person.setEducationInfo(educationInfo);

        personDto = new PersonDto(person);
        educationInfoDto = new EducationInfoDto(educationInfo);
    }

    @Test
    public void saveEducationInfoWithEntityTest() {
        try {
            educationInfoService.saveEducationInfo(educationInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveEducationInfoWithIdAndEntityTest() {
        try {
            educationInfoService.saveEducationInfo(person.getId(),educationInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveEducationInfoWithEntityAndEntityTest() {
        try {
            educationInfoService.saveEducationInfo(person,educationInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findEducationInfoByIdTest() {
        when(educationInfoRep.findById(any())).thenReturn(Optional.of(educationInfo));
        when(educationInfoMapper.toEducationInfoDto(any())).thenReturn(educationInfoDto);

        EducationInfoDto result = educationInfoService.findEducationInfo(educationInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,educationInfoDto);
    }

    @Test
    public void findEducationInfoByPersonIdTest() {
        when(educationInfoRep.findEducationInfoByPersonInEducationInfoId(Consts.Test.HOUSEHOLD_ID)).thenReturn(Optional.of(educationInfo));
        when(educationInfoMapper.toEducationInfoDto(any())).thenReturn(educationInfoDto);

        EducationInfoDto result = educationInfoService.findEducationInfoByPersonId(educationInfo.getPersonInEducationInfo().getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,educationInfoDto);
    }

    @Test
    public void findEducationInfoByPersonTest() {
        when(educationInfoRep.findEducationInfoByPersonInEducationInfo(any())).thenReturn(Optional.of(educationInfo));
        when(educationInfoMapper.toEducationInfoDto(any())).thenReturn(educationInfoDto);

        EducationInfoDto result = educationInfoService.findEducationInfoByPerson(educationInfo.getPersonInEducationInfo());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,educationInfoDto);
    }


    @Test
    public void deleteEducationInfoByIdTest() {
        try {
            educationInfoService.deleteEducationInfoById(educationInfo.getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteEducationInfoByEntityTest() {
        try {
            educationInfoService.deleteEducationInfo(educationInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteEducationInfoByPersonIdTest() {
        try {
            educationInfoService.deleteEducationInfoByPersonId(educationInfo.getPersonInEducationInfo().getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteEducationInfoByPersonTest() {
        try {
            educationInfoService.deleteEducationInfoByPerson(educationInfo.getPersonInEducationInfo());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findLinkedPersonByIdTest() {
        when(personRep.findPersonByEducationInfoId(Consts.Test.EDUCATION_INFO_ID)).thenReturn(Optional.of(person));
        when(personMapper.toPersonDto(any())).thenReturn(personDto);

        PersonDto result = educationInfoService.findLinkedPerson(educationInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,personDto);
    }

    @Test
    public void findLinkedPersonByEntityTest() {
        when(personRep.findPersonByEducationInfo(any())).thenReturn(Optional.of(person));
        when(personMapper.toPersonDto(any())).thenReturn(personDto);

        PersonDto result = educationInfoService.findLinkedPerson(educationInfo);

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,personDto);
    }


    @Test
    public void updateEducationInfoTest() {
        when(educationInfoRep.findById(any())).thenReturn(Optional.of(educationInfo));
        try {
            educationInfoService.updateEducationInfo(educationInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }
}
