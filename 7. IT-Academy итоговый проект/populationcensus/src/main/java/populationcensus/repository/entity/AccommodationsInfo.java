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
@Table(name = "accommodations_info")
public class AccommodationsInfo {
    @Id
    private Long id;

    @Column(name = "owner_of_apartment")
    private Integer ownerOfApartment;

    @Column(name = "is_apartment_rented")
    private Boolean isApartmentRented;

    @Column(name = "apartment_type")
    private Integer apartmentType;

    @Column(name = "area_of_flat")
    private Integer areaOfFlat;

    @Column(name = "water_pipes")
    private Integer waterPipes;

    @Column(name = "canalization")
    private Integer canalization;

    @Column(name = "has_bath_or_shower")
    private Boolean hasBathOrShower;

    @Column(name = "hot_water_supply")
    private Integer hotWaterSupply;

    @Column(name = "heating")
    private Integer heating;

    @Column(name = "cooking_equipment")
    private Integer cookingEquipment;

    @ToString.Exclude
    @EqualsAndHashCode.Exclude
    @OneToOne(fetch = FetchType.LAZY)
    @MapsId
    @JoinColumn(name = "linked_household_id")
    private Household householdInAccommodationsInfo;
}
