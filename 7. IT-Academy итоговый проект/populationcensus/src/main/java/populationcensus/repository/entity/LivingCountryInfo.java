package populationcensus.repository.entity;

import lombok.*;

import javax.persistence.*;
import java.time.LocalDate;
import java.util.Date;

@Builder
@AllArgsConstructor
@NoArgsConstructor
@Getter
@Setter
@ToString
@EqualsAndHashCode
@Entity
@Table(name = "living_country_info")
public class LivingCountryInfo {
    @Id
    private Long id;

    @Column(name = "did_you_live_in_other_country")
    private Boolean didYouLiveInOtherCountry;

    @Column(name = "name_of_country_you_came_from")
    private String nameOfCountryYouCameFrom;

    @Column(name = "arrival_period")
    private LocalDate arrivalPeriod;

    @Column(name = "reason_for_arrival_at_rb")
    private Integer reasonForArrivalAtRB;

    @Column(name = "info_about_leaving_belarus")
    private Integer infoAboutLeavingBelarus;

    @Column(name = "reason_for_leaving_belarus")
    private Integer reasonForLeavingBelarus;

    @ToString.Exclude
    @EqualsAndHashCode.Exclude
    @OneToOne(fetch = FetchType.LAZY)
    @MapsId
    @JoinColumn(name = "linked_person_id")
    private Person personInLivingCountryInfo;
}
