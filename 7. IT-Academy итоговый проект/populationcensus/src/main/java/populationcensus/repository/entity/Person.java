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
@Table(name = "person")
public class Person {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column
    private Long id;


    @Column(name = "is_foreign")
    private Boolean isForeign;

    @Column(name = "home_country")
    private String homeCountry;

    @Column(name = "reason_for_migration")
    private Integer reasonForMigration;


    @Column(name = "p_name")
    private String name;

    @Column(name = "p_surname")
    private String surname;

    @Column(name = "p_fathername")
    private String fathername;

    @Column(name = "passport_id")
    private String passportID;

    @Column(name = "birthday_date")
    private LocalDate birthdayDate;

    @Column(name = "age")
    private Integer age;

    @Column(name = "gender")
    private Integer gender;

    @Column(name = "household_relations")
    private Integer householdRelations;

    @Column(name = "marital_status")
    private Integer maritalStatus;

    @Column(name = "birth_country")
    private Integer birthCountry;

    @Column(name = "name_of_birth_country")
    private String nameOfBirthCountry;

    @Column(name = "citizenship")
    private Integer citizenship;

    @Column(name = "name_of_citizenship_country")
    private String nameOfCitizenshipCountry;

    @Column(name = "nationality")
    private Integer nationality;

    @Column(name = "name_of_nationality")
    private String nameOfNationality;

    @Column(name = "native_language")
    private Integer nativeLanguage;

    @Column(name = "name_of_native_language")
    private String nameOfNativeLanguage;

    @Column(name = "speaking_language")
    private Integer speakingLanguage;

    @Column(name = "name_of_speaking_language")
    private String nameOfSpeakingLanguage;

    @Column(name = "main_source_of_resources")
    private Integer mainSourceOfResources;

    @ToString.Exclude
    @ManyToOne(fetch = FetchType.EAGER, cascade = CascadeType.REFRESH)
    @JoinColumn(name = "id_household")
    private Household householdField;

    @OneToOne(mappedBy = "personInLivingPlaceInfo", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
    private LivingPlaceInfo livingPlaceInfo;

    @OneToOne(mappedBy = "personInLivingCountryInfo", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
    private LivingCountryInfo livingCountryInfo;

    @OneToOne(mappedBy = "personInEducationInfo", fetch = FetchType.EAGER, cascade = CascadeType.ALL)
    private EducationInfo educationInfo;

    @OneToOne(mappedBy = "personInWorkInfo", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
    private WorkInfo workInfo;

    @OneToOne(mappedBy = "personInChildrenInfo", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
    private ChildrenInfo childrenInfo;
}
