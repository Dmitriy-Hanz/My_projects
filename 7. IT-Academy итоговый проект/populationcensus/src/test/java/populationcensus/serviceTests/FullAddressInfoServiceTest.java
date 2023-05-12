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
import populationcensus.dto.FullAddressInfoDto;
import populationcensus.dto.HouseholdDto;
import populationcensus.dto.mapper.FullAddressInfoMapper;
import populationcensus.dto.mapper.HouseholdMapper;
import populationcensus.repository.entity.FullAddressInfo;
import populationcensus.repository.entity.Household;
import populationcensus.repository.repositories.FullAddressInfoRep;
import populationcensus.repository.repositories.HouseholdRep;
import populationcensus.service.impl.FullAddressInfoServiceImpl;

import java.util.Optional;

import static org.mockito.Mockito.any;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
@RunWith(SpringRunner.class)
@TestPropertySource(locations = "classpath:applicationTest.yml")
public class FullAddressInfoServiceTest {

    @Mock
    private FullAddressInfoRep fullAddressInfoRep;
    @Mock
    private HouseholdRep householdRep;
    @Mock
    private FullAddressInfoMapper fullAddressInfoMapper;
    @Mock
    private HouseholdMapper householdMapper;

    @InjectMocks
    private FullAddressInfoServiceImpl fullAddressInfoService;

    private Household household;
    private HouseholdDto householdDto;
    private FullAddressInfo fullAddressInfo;
    private FullAddressInfoDto fullAddressInfoDto;

    @Before
    public void presets(){
        household = Household.builder()
                .id(Consts.Test.HOUSEHOLD_ID)
                .roomsCount(Consts.Test.HOUSEHOLD_ROOMS_COUNT)
                .numberOfMembers(Consts.Test.HOUSEHOLD_NUMBER_OF_MEMBERS)
                .build();

        fullAddressInfo = FullAddressInfo.builder()
                .id(Consts.Test.ACCOMMODATIONS_INFO_ID)
                .villageName(Consts.Test.FULL_ADDRESS_INFO_VILLAGE_NAME)
                .householdInFullAddressInfo(household)
                .build();

        household.setFullAddressInfo(fullAddressInfo);

        householdDto = new HouseholdDto(household);
        fullAddressInfoDto = new FullAddressInfoDto(fullAddressInfo);
    }

    @Test
    public void saveFullAddressInfoWithEntityTest() {
        try {
            fullAddressInfoService.saveFullAddressInfo(fullAddressInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveFullAddressInfoWithIdAndEntityTest() {
        try {
            fullAddressInfoService.saveFullAddressInfo(household.getId(),fullAddressInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void saveFullAddressInfoWithEntityAndEntityTest() {
        try {
            fullAddressInfoService.saveFullAddressInfo(household,fullAddressInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findFullAddressInfoByIdTest() {
        when(fullAddressInfoRep.findById(any())).thenReturn(Optional.of(fullAddressInfo));
        when(fullAddressInfoMapper.toFullAddressInfoDto(any())).thenReturn(fullAddressInfoDto);

        FullAddressInfoDto result = fullAddressInfoService.findFullAddressInfo(fullAddressInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,fullAddressInfoDto);
    }

    @Test
    public void findFullAddressInfoByHouseholdIdTest() {
        when(fullAddressInfoRep.findFullAddressInfoByHouseholdInFullAddressInfoId(Consts.Test.HOUSEHOLD_ID)).thenReturn(Optional.of(fullAddressInfo));
        when(fullAddressInfoMapper.toFullAddressInfoDto(any())).thenReturn(fullAddressInfoDto);

        FullAddressInfoDto result = fullAddressInfoService.findFullAddressInfoByHouseholdId(fullAddressInfo.getHouseholdInFullAddressInfo().getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,fullAddressInfoDto);
    }

    @Test
    public void findFullAddressInfoByHouseholdTest() {
        when(fullAddressInfoRep.findFullAddressInfoByHouseholdInFullAddressInfo(any())).thenReturn(Optional.of(fullAddressInfo));
        when(fullAddressInfoMapper.toFullAddressInfoDto(any())).thenReturn(fullAddressInfoDto);

        FullAddressInfoDto result = fullAddressInfoService.findFullAddressInfoByHousehold(fullAddressInfo.getHouseholdInFullAddressInfo());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,fullAddressInfoDto);
    }


    @Test
    public void deleteFullAddressInfoByIdTest() {
        try {
            fullAddressInfoService.deleteFullAddressInfoById(fullAddressInfo.getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteFullAddressInfoByEntityTest() {
        try {
            fullAddressInfoService.deleteFullAddressInfo(fullAddressInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteFullAddressInfoByHouseholdIdTest() {
        try {
            fullAddressInfoService.deleteFullAddressInfoByHouseholdId(fullAddressInfo.getHouseholdInFullAddressInfo().getId());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }

    @Test
    public void deleteFullAddressInfoByHouseholdTest() {
        try {
            fullAddressInfoService.deleteFullAddressInfoByHousehold(fullAddressInfo.getHouseholdInFullAddressInfo());
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }


    @Test
    public void findLinkedHouseholdByIdTest() {
        when(householdRep.findHouseholdByFullAddressInfoId(any())).thenReturn(Optional.of(household));
        when(householdMapper.toHouseholdDto(any())).thenReturn(householdDto);

        HouseholdDto result = fullAddressInfoService.findLinkedHousehold(fullAddressInfo.getId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,householdDto);
    }

    @Test
    public void findLinkedHouseholdByEntityTest() {
        when(householdRep.findHouseholdByFullAddressInfo(any())).thenReturn(Optional.of(household));
        when(householdMapper.toHouseholdDto(any())).thenReturn(householdDto);

        HouseholdDto result = fullAddressInfoService.findLinkedHousehold(fullAddressInfo);

        Assertions.assertNotNull(result);
        Assertions.assertEquals(result,householdDto);
    }


    @Test
    public void updateFullAddressInfoTest() {
        when(fullAddressInfoRep.findById(any())).thenReturn(Optional.of(fullAddressInfo));
        try {
            fullAddressInfoService.updateFullAddressInfo(fullAddressInfo);
        } catch (Exception e){
            Assertions.fail();
        }
        Assertions.assertTrue(true);
    }
}
