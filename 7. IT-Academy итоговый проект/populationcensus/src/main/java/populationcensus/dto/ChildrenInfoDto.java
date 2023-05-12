package populationcensus.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import populationcensus.repository.entity.ChildrenInfo;
import populationcensus.repository.entity.LivingCountryInfo;

import javax.persistence.Column;
import java.util.Date;

@Data
@NoArgsConstructor
public class ChildrenInfoDto {
    private Long id;
    private Integer howManyChildrenDoYouHave;
    private Boolean noChildren;
    private Integer childrenPlans;
    private Integer howManyChildrenDoYouWant;
    private Boolean dontKnowHowMany;

    public ChildrenInfoDto(ChildrenInfo entity) {
        id = entity.getId();
        howManyChildrenDoYouHave = entity.getHowManyChildrenDoYouHave();
        noChildren = entity.getNoChildren();
        childrenPlans = entity.getChildrenPlans();
        howManyChildrenDoYouWant = entity.getHowManyChildrenDoYouWant();
        dontKnowHowMany = entity.getDontKnowHowMany();
    }
}
