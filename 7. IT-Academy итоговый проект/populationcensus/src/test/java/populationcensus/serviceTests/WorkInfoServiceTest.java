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
import populationcensus.dto.WorkInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.WorkInfoMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.WorkInfo;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.WorkInfoRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.impl.WorkInfoServiceImpl;

import java.util.Optional;

import static org.mockito.Mockito.any;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
@RunWith(SpringRunner.class)
@TestPropertySource(locations = "classpath:applicationTest.yml")
public class WorkInfoServiceTest {
    @Mock
    private WorkInfoRep workInfoRep;
    @Mock
    private PersonRep personRep;
    @Mock
    private WorkInfoMapper workInfoMapper;
    @Mock
    private PersonMapper personMapper;

    @InjectMocks
    private WorkInfoServiceImpl workInfoService;

    private Person person;
    private PersonDto personDto;
    private WorkInfo workInfo;
    private WorkInfoDto workInfoDto;

    @Before
    public void presets(){
        person = Person.builder()
                .id(Consts.Test.PERSON_ID)
                .age(Consts.Test.PERSON_AGE)
                .name(Consts.Test.PERSON_NAME)
                .build();

        workInfo = WorkInfo.builder()
                .id(Consts.Test.WORK_INFO_ID)
                .typeOfWorkplace(Consts.Test.WORK_INFO_TYPE_OF_WORKPLACE)
                .personInWorkInfo(person)
                .build();

        person.setWorkInfo(workInfo);

        personDto = new PersonDto(person);
        workInfoDto = new WorkInfoDto(workInfo);
    }

    @Test
    public void saveWorkInfoWithEntityTest() {
        try {
            workInfoService.saveWorkInfo(workInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveWorkInfoWithIdAndEntityTest() {
        try {
            workInfoService.saveWorkInfo(person.getId(),workInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveWorkInfoWithEntityAndEntityTest() {
        try {
            workInfoService.saveWorkInfo(person,workInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findWorkInfoByIdTest() {
        when(workInfoRep.findById(any())).thenReturn(Optional.of(workInfo));
        when(workInfoMapper.toWorkInfoDto(any())).thenReturn(workInfoDto);

        WorkInfoDto result = workInfoService.findWorkInfo(workInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,workInfoDto);
    }

    @Test
    public void findWorkInfoByPersonIdTest() {
        when(workInfoRep.findByPersonInWorkInfoId(Consts.Test.HOUSEHOLD_ID)).thenReturn(Optional.of(workInfo));
        when(workInfoMapper.toWorkInfoDto(any())).thenReturn(workInfoDto);

        WorkInfoDto result = workInfoService.findWorkInfoByPersonId(workInfo.getPersonInWorkInfo().getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,workInfoDto);
    }

    @Test
    public void findWorkInfoByPersonTest() {
        when(workInfoRep.findByPersonInWorkInfo(any())).thenReturn(Optional.of(workInfo));
        when(workInfoMapper.toWorkInfoDto(any())).thenReturn(workInfoDto);

        WorkInfoDto result = workInfoService.findWorkInfoByPerson(workInfo.getPersonInWorkInfo());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,workInfoDto);
    }


    @Test
    public void deleteWorkInfoByIdTest() {
        try {
            workInfoService.deleteWorkInfoById(workInfo.getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteWorkInfoByEntityTest() {
        try {
            workInfoService.deleteWorkInfo(workInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteWorkInfoByPersonIdTest() {
        try {
            workInfoService.deleteWorkInfoByPersonId(workInfo.getPersonInWorkInfo().getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteWorkInfoByPersonTest() {
        try {
            workInfoService.deleteWorkInfoByPerson(workInfo.getPersonInWorkInfo());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findLinkedPersonByIdTest() {
        when(personRep.findByWorkInfoId(1L)).thenReturn(Optional.of(person));
        when(personMapper.toPersonDto(any())).thenReturn(personDto);

        PersonDto result = workInfoService.findLinkedPerson(workInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,personDto);
    }

    @Test
    public void findLinkedPersonByEntityTest() {
        when(personRep.findByWorkInfo(any())).thenReturn(Optional.of(person));
        when(personMapper.toPersonDto(any())).thenReturn(personDto);

        PersonDto result = workInfoService.findLinkedPerson(workInfo);

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,personDto);
    }


    @Test
    public void updateWorkInfoTest() {
        when(workInfoRep.findById(any())).thenReturn(Optional.of(workInfo));
        try {
            workInfoService.updateWorkInfo(workInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }
}

