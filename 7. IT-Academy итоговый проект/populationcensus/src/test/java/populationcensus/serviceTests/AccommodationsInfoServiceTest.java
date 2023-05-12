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
import populationcensus.dto.AccommodationsInfoDto;
import populationcensus.dto.HouseholdDto;
import populationcensus.dto.mapper.AccommodationsInfoMapper;
import populationcensus.dto.mapper.HouseholdMapper;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.repository.entity.Household;
import populationcensus.repository.repositories.AccommodationsInfoRep;
import populationcensus.repository.repositories.HouseholdRep;
import populationcensus.service.impl.AccommodationsInfoServiceImpl;

import java.util.Optional;

import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
@RunWith(SpringRunner.class)
@TestPropertySource(locations = "classpath:applicationTest.yml")
public class AccommodationsInfoServiceTest {

    @Mock
    private AccommodationsInfoRep accommodationsInfoRep;
    @Mock
    private HouseholdRep householdRep;
    @Mock
    private AccommodationsInfoMapper accommodationsInfoMapper;
    @Mock
    private HouseholdMapper householdMapper;

    @InjectMocks
    private AccommodationsInfoServiceImpl accommodationsInfoService;

    private Household household;
    private HouseholdDto householdDto;
    private AccommodationsInfo accommodationsInfo;
    private AccommodationsInfoDto accommodationsInfoDto;

    @Before
    public void presets(){
        household = Household.builder()
                .id(Consts.Test.HOUSEHOLD_ID)
                .roomsCount(Consts.Test.HOUSEHOLD_ROOMS_COUNT)
                .numberOfMembers(Consts.Test.HOUSEHOLD_NUMBER_OF_MEMBERS)
                .build();

        accommodationsInfo = AccommodationsInfo.builder()
                .id(Consts.Test.ACCOMMODATIONS_INFO_ID)
                .areaOfFlat(Consts.Test.ACCOMMODATIONS_INFO_AREA_OF_FLAT)
                .householdInAccommodationsInfo(household)
                .build();

        household.setAccommodationsInfo(accommodationsInfo);

        householdDto = new HouseholdDto(household);
        accommodationsInfoDto = new AccommodationsInfoDto(accommodationsInfo);
    }

    @Test
    public void saveAccommodationsInfoWithEntityTest() {
        try {
            accommodationsInfoService.saveAccommodationsInfo(accommodationsInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveAccommodationsInfoWithIdAndEntityTest() {
        try {
            accommodationsInfoService.saveAccommodationsInfo(household.getId(),accommodationsInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveAccommodationsInfoWithEntityAndEntityTest() {
        try {
            accommodationsInfoService.saveAccommodationsInfo(household,accommodationsInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findAccommodationsInfoByIdTest() {
        when(accommodationsInfoRep.findById(any())).thenReturn(Optional.of(accommodationsInfo));
        when(accommodationsInfoMapper.toAccommodationsInfoDto(any())).thenReturn(accommodationsInfoDto);

        AccommodationsInfoDto result = accommodationsInfoService.findAccommodationsInfo(accommodationsInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,accommodationsInfoDto);
    }

    @Test
    public void findAccommodationsInfoByHouseholdIdTest() {
        when(accommodationsInfoRep.findAccommodationsInfoByHouseholdInAccommodationsInfoId(Consts.Test.HOUSEHOLD_ID)).thenReturn(Optional.of(accommodationsInfo));
        when(accommodationsInfoMapper.toAccommodationsInfoDto(any())).thenReturn(accommodationsInfoDto);

        AccommodationsInfoDto result = accommodationsInfoService.findAccommodationsInfoByHouseholdId(accommodationsInfo.getHouseholdInAccommodationsInfo().getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,accommodationsInfoDto);
    }

    @Test
    public void findAccommodationsInfoByHouseholdTest() {
        when(accommodationsInfoRep.findAccommodationsInfoByHouseholdInAccommodationsInfo(any())).thenReturn(Optional.of(accommodationsInfo));
        when(accommodationsInfoMapper.toAccommodationsInfoDto(any())).thenReturn(accommodationsInfoDto);

        AccommodationsInfoDto result = accommodationsInfoService.findAccommodationsInfoByHousehold(accommodationsInfo.getHouseholdInAccommodationsInfo());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,accommodationsInfoDto);
    }


    @Test
    public void deleteAccommodationsInfoByIdTest() {
        try {
            accommodationsInfoService.deleteAccommodationsInfoById(accommodationsInfo.getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteAccommodationsInfoByEntityTest() {
        try {
            accommodationsInfoService.deleteAccommodationsInfo(accommodationsInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteAccommodationsInfoByHouseholdIdTest() {
        try {
            accommodationsInfoService.deleteAccommodationsInfoByHouseholdId(accommodationsInfo.getHouseholdInAccommodationsInfo().getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteAccommodationsInfoByHouseholdTest() {
        try {
            accommodationsInfoService.deleteAccommodationsInfoByHousehold(accommodationsInfo.getHouseholdInAccommodationsInfo());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findLinkedHouseholdByIdTest() {
        when(householdRep.findHouseholdByAccommodationsInfoId(any())).thenReturn(Optional.of(household));
        when(householdMapper.toHouseholdDto(any())).thenReturn(householdDto);

        HouseholdDto result = accommodationsInfoService.findLinkedHousehold(accommodationsInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,householdDto);
    }

    @Test
    public void findLinkedHouseholdByEntityTest() {
        when(householdRep.findHouseholdByAccommodationsInfo(any())).thenReturn(Optional.of(household));
        when(householdMapper.toHouseholdDto(any())).thenReturn(householdDto);

        HouseholdDto result = accommodationsInfoService.findLinkedHousehold(accommodationsInfo);

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,householdDto);
    }


    @Test
    public void updateAccommodationsInfoTest() {
        when(accommodationsInfoRep.findById(any())).thenReturn(Optional.of(accommodationsInfo));
        try {
            accommodationsInfoService.updateAccommodationsInfo(accommodationsInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }
}
