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
import populationcensus.dto.LivingPlaceInfoDto;
import populationcensus.dto.PersonDto;
import populationcensus.dto.mapper.LivingPlaceInfoMapper;
import populationcensus.dto.mapper.PersonMapper;
import populationcensus.repository.entity.LivingPlaceInfo;
import populationcensus.repository.entity.Person;
import populationcensus.repository.repositories.LivingPlaceInfoRep;
import populationcensus.repository.repositories.PersonRep;
import populationcensus.service.impl.LivingPlaceInfoServiceImpl;

import java.util.Optional;

import static org.mockito.Mockito.any;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
@RunWith(SpringRunner.class)
@TestPropertySource(locations = "classpath:applicationTest.yml")
public class LivingPlaceInfoServiceTest {
    @Mock
    private LivingPlaceInfoRep livingPlaceInfoRep;
    @Mock
    private PersonRep personRep;
    @Mock
    private LivingPlaceInfoMapper livingPlaceInfoMapper;
    @Mock
    private PersonMapper personMapper;

    @InjectMocks
    private LivingPlaceInfoServiceImpl livingPlaceInfoService;

    private Person person;
    private PersonDto personDto;
    private LivingPlaceInfo livingPlaceInfo;
    private LivingPlaceInfoDto livingPlaceInfoDto;
    
    @Before
    public void presets(){
        person = Person.builder()
                .id(Consts.Test.PERSON_ID)
                .age(Consts.Test.PERSON_AGE)
                .name(Consts.Test.PERSON_NAME)
                .build();

        livingPlaceInfo = LivingPlaceInfo.builder()
                .id(Consts.Test.LIVING_PLACE_INFO_ID)
                .isItVillage(Consts.Test.LIVING_PLACE_INFO_IS_IT_VILLAGE)
                .personInLivingPlaceInfo(person)
                .build();

        person.setLivingPlaceInfo(livingPlaceInfo);

        personDto = new PersonDto(person);
        livingPlaceInfoDto = new LivingPlaceInfoDto(livingPlaceInfo);
    }

    @Test
    public void saveLivingPlaceInfoWithEntityTest() {
        try {
            livingPlaceInfoService.saveLivingPlaceInfo(livingPlaceInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveLivingPlaceInfoWithIdAndEntityTest() {
        try {
            livingPlaceInfoService.saveLivingPlaceInfo(person.getId(),livingPlaceInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveLivingPlaceInfoWithEntityAndEntityTest() {
        try {
            livingPlaceInfoService.saveLivingPlaceInfo(person,livingPlaceInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findLivingPlaceInfoByIdTest() {
        when(livingPlaceInfoRep.findById(any())).thenReturn(Optional.of(livingPlaceInfo));
        when(livingPlaceInfoMapper.toLivingPlaceInfoDto(any())).thenReturn(livingPlaceInfoDto);

        LivingPlaceInfoDto result = livingPlaceInfoService.findLivingPlaceInfo(livingPlaceInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,livingPlaceInfoDto);
    }

    @Test
    public void findLivingPlaceInfoByPersonIdTest() {
        when(livingPlaceInfoRep.findByPersonInLivingPlaceInfoId(Consts.Test.HOUSEHOLD_ID)).thenReturn(Optional.of(livingPlaceInfo));
        when(livingPlaceInfoMapper.toLivingPlaceInfoDto(any())).thenReturn(livingPlaceInfoDto);

        LivingPlaceInfoDto result = livingPlaceInfoService.findLivingPlaceInfoByPersonId(livingPlaceInfo.getPersonInLivingPlaceInfo().getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,livingPlaceInfoDto);
    }

    @Test
    public void findLivingPlaceInfoByPersonTest() {
        when(livingPlaceInfoRep.findByPersonInLivingPlaceInfo(any())).thenReturn(Optional.of(livingPlaceInfo));
        when(livingPlaceInfoMapper.toLivingPlaceInfoDto(any())).thenReturn(livingPlaceInfoDto);

        LivingPlaceInfoDto result = livingPlaceInfoService.findLivingPlaceInfoByPerson(livingPlaceInfo.getPersonInLivingPlaceInfo());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,livingPlaceInfoDto);
    }


    @Test
    public void deleteLivingPlaceInfoByIdTest() {
        try {
            livingPlaceInfoService.deleteLivingPlaceInfoById(livingPlaceInfo.getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteLivingPlaceInfoByEntityTest() {
        try {
            livingPlaceInfoService.deleteLivingPlaceInfo(livingPlaceInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteLivingPlaceInfoByPersonIdTest() {
        try {
            livingPlaceInfoService.deleteLivingPlaceInfoByPersonId(livingPlaceInfo.getPersonInLivingPlaceInfo().getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteLivingPlaceInfoByPersonTest() {
        try {
            livingPlaceInfoService.deleteLivingPlaceInfoByPerson(livingPlaceInfo.getPersonInLivingPlaceInfo());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findLinkedPersonByIdTest() {
        when(personRep.findByLivingPlaceInfoId(1L)).thenReturn(Optional.of(person));
        when(personMapper.toPersonDto(any())).thenReturn(personDto);

        PersonDto result = livingPlaceInfoService.findLinkedPerson(livingPlaceInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,personDto);
    }

    @Test
    public void findLinkedPersonByEntityTest() {
        when(personRep.findByLivingPlaceInfo(any())).thenReturn(Optional.of(person));
        when(personMapper.toPersonDto(any())).thenReturn(personDto);

        PersonDto result = livingPlaceInfoService.findLinkedPerson(livingPlaceInfo);

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,personDto);
    }


    @Test
    public void updateLivingPlaceInfoTest() {
        when(livingPlaceInfoRep.findById(any())).thenReturn(Optional.of(livingPlaceInfo));
        try {
            livingPlaceInfoService.updateLivingPlaceInfo(livingPlaceInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }
}
