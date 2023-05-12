package populationcensus.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.EducationInfo;
import populationcensus.repository.entity.LivingPlaceInfo;

import javax.persistence.Column;
import java.util.Date;

@Data
@NoArgsConstructor
public class EducationInfoDto {
    private Long id;
    private Integer lvlOfEducation;
    private Integer academicDegree;
    private Boolean canYouReadAndWrite;
    private Integer basicEducationInfo;
    private Boolean additionalEducationInfo;
    private Boolean doesChildAttendPreschool;


    public EducationInfoDto(EducationInfo entity) {
        id = entity.getId();
        lvlOfEducation = entity.getLvlOfEducation();
        academicDegree = entity.getAcademicDegree();
        canYouReadAndWrite = entity.getCanYouReadAndWrite();
        basicEducationInfo = entity.getBasicEducationInfo();
        additionalEducationInfo = entity.getAdditionalEducationInfo();
        doesChildAttendPreschool = entity.getDoesChildAttendPreschool();
    }
}
