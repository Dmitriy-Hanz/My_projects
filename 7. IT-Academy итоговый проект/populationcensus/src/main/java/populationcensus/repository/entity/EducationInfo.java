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
@Table(name = "education_info")
public class EducationInfo {
    @Id
    private Long id;

    @Column(name = "lvl_of_education")
    private Integer lvlOfEducation;

    @Column(name = "academic_degree")
    private Integer academicDegree;

    @Column(name = "can_you_read_and_write")
    private Boolean canYouReadAndWrite;

    @Column(name = "basic_education_info")
    private Integer basicEducationInfo;

    @Column(name = "additional_education_info")
    private Boolean additionalEducationInfo;

    @Column(name = "does_child_attend_preschool")
    private Boolean doesChildAttendPreschool;

    @ToString.Exclude
    @EqualsAndHashCode.Exclude
    @OneToOne(fetch = FetchType.LAZY)
    @MapsId
    @JoinColumn(name = "linked_person_id")
    private Person personInEducationInfo;
}
