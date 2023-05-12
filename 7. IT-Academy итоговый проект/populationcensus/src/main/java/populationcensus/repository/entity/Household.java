package populationcensus.repository.entity;

import lombok.*;

import javax.persistence.*;
import java.util.List;

@Builder
@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@ToString
@EqualsAndHashCode

@Entity
@Table(name = "household")
public class Household {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "number_of_members")
    private Integer numberOfMembers;

    @Column(name = "rooms_count")
    private Integer roomsCount;

    @Column(name = "part_of_room")
    private String partOfRoom;

    @Column(name = "has_foreign_residents")
    private Boolean hasForeignResidents;

    @Column(name = "has_farm_facilities")
    private Boolean hasFarmFacilities;

    @OneToOne(mappedBy = "householdInFullAddressInfo", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private FullAddressInfo fullAddressInfo;

    @OneToOne(mappedBy = "householdInAccommodationsInfo", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private AccommodationsInfo accommodationsInfo;

    @OneToMany(mappedBy = "householdField", fetch = FetchType.EAGER, cascade = CascadeType.ALL)
    private List<Person> persons;
}
