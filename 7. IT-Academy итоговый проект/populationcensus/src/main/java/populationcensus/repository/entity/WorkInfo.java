package populationcensus.repository.entity;

import lombok.*;

import javax.persistence.*;

@Builder
@AllArgsConstructor
@NoArgsConstructor
@Getter
@Setter
@ToString
@EqualsAndHashCode
@Entity
@Table(name = "work_info")
public class WorkInfo {
    @Id
    private Long id;

    @Column(name = "do_you_have_work")
    private Boolean doYouHaveWork;

    @Column(name = "why_didnt_you_work_till_now")
    private Boolean whyDidntYouWorkTillNow;

    @Column(name = "location_of_work")
    private Integer locationOfWork;

    @Column(name = "region_or_district_name")
    private String regionOrDistrictName;

    @Column(name = "city_or_pgt_name")
    private String cityOrPGTName;

    @Column(name = "is_it_village")
    private Boolean isItVillage;

    @Column(name = "name_of_country")
    private String nameOfCountry;

    @Column(name = "departure_frequency_to_work")
    private Integer departureFrequencyToWork;

    @Column(name = "unemployment_reason")
    private Integer unemploymentReason;

    @Column(name = "type_of_workplace")
    private Integer typeOfWorkplace;

    @Column(name = "type_of_work_position")
    private Integer typeOfWorkPosition;

    @Column(name = "did_you_looked_for_a_job")
    private Boolean didYouLookedForAJob;

    @Column(name = "can_you_star_working_in_two_weeks")
    private Boolean canYouStarWorkingInTwoWeeks;

    @Column(name = "why_you_cant_work_or_stopped_search")
    private Integer whyYouCantWorkOrStoppedSearch;

    @ToString.Exclude
    @EqualsAndHashCode.Exclude
    @OneToOne(fetch = FetchType.LAZY)
    @MapsId
    @JoinColumn(name = "linked_person_id")
    private Person personInWorkInfo;
}
