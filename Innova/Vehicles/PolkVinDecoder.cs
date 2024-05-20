using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Innova.Vehicles
{
    public class PolkVinDecoder
    {
        private static Hashtable allPolkVehicleYMMEs;
        private static IEnumerable<string> allPolkVehicleVinPatternMasks; //Added on 2018-06-20 by Nam Lu - INNOVA Dev Team

        private static bool isBusy = false;
        private Registry registry;
        private PolkVehicleYMME decodedPolkVehicle;

        #region Data Layout Documentation

        /*
        Index   Location              Name                                  Description
        1       1-64                  VIN_SIGNI_PATTRN_MASK                 VIN Prefix that includes the significant digits of the VIN
        2       66-69                 NCI_MAK_ABBR_CD                       Contains the Polk standardized abbreviation for the OEMs vehicle make.  The vehicle make generally contains what the general public usually considers to be a vehicle brand name, for example, Chrysler, Dodge, Ford, Mercury, Toyota, GMC, Chevy, Cadillac, a
        3       71-74                 MDL_YR                                Contains the marketing year defined by the OEM within which the vehicle was produced.  The value contained in this attribute may not always exactly match the calendar year in which the vehicle was actually manufactured.  For example, many large North Amer
        4       76-95                 VEH_TYP_CD                            A Polk assigned code that defines the type of a vehicle represented by a specific VIN.  For example:  M,P,C or T.
        5       97-346                VEH_TYP_DESC                          The description of the Polk assigned code for the vehicle type code. For example: passenger, truck, motorcycle, commercial trailer.
        6       348-397               MAK_NM                                (Make - Name) Full name of the make (i.e. Chevrolet)
        7       399-648               MDL_DESC                              (Model Code) medium description
        8       650-899               TRIM_DESC                             The Trim of the vehicle
        9       901-1150              OPT1_TRIM_DESC                         The trim of the vehicle.  This field is used when a VIN Pattern could have more than 1 trim assigned.
        10      1152-1401             OPT2_TRIM_DESC                         The trim of the vehicle.  This field is used when a VIN Pattern could have more than 2 trims assigned.
        11      1403-1652             OPT3_TRIM_DESC                         The trim of the vehicle.  This field is used when a VIN Pattern could have more than 3 trims assigned.
        12      1654-1903             OPT4_TRIM_DESC                         The trim of the vehicle.  This field is used when a VIN Pattern could have more than 4 trims assigned.
        13      1905-1924             BODY_STYLE_CD                         A Polk assigned code that describes the specification of how a trailer is to be used, based on the body style. For example, DB=Dump Bottom.
        14      1926-2175             BODY_STYLE_DESC                       The description of the availability of Running Lights.  For example Not Available, Optional, etc.
        15      2177-2178             DOOR_CNT                              The description of the Polk assigned code that indicates the availability of Power Windows, based on Make, Year, and Model Trim assigned by the VIN Team based on OEM vehicle specifications or secondary research.  For example Not Available, Optional, etc.
        16      2180-2181             WHL_CNT                               The number of wheel ends on the vehicle.  For example in a 6x4 configuration this would be the 6.
        17      2183-2184             WHL_DRVN_CNT                          Number of wheels driven by the power train.  For example in a 6x4 configuration this would be the 4.
        18      2186-2205             MFG_CD                                (Vehicle Manufacturer Name) Standard abbreviation of the name of the vehicle manufacturer, i.e. General Motors, as defined by the National Crime Information Center
        19      2207-2456             MFG_DESC                              (Vehicle Manufacturer Name) The name of the vehicle manufacturer, i.e. General Motors, as defined by the National Crime Information Center
        20      2458-2462             ENG_DISPLCMNT_CI                      (Displacement CID) displacement in cubic inches. This is a rounded, marketing value, like 302 cubic inches, instead of 4967 cc.
        21      2464-2468             ENG_DISPLCMNT_CC                      (Displacement CC) displacement in cubic centimeters. We intend to use this as the definitive, exact diplacement value, i.e. 4967 cc.
        22      2470-2474             ENG_CLNDR_RTR_CNT                     Contains a code that represents the number of cylinders a vehicles combustion engine can have.
        23      2476-2476             ENG_CYCL_CNT                          (Cycle Count) Refers to the cycle or stroke of an engine. 2-strokes are lightweight and simpler, but they burn oil, by design. Few cars on the road in North America are two-strokes, the last one offered was a 1967 Saab. These days, usually used in chainsa
        24      2478-2478             ENG_FUEL_CD                           (Fuel) What an internal combustion burns to move a piston in a cylinder
        25      2480-2729             ENG_FUEL_DESC                         (Fuel) description
        26      2731-2733             ENG_FUEL_INJ_TYP_CD                   The type of fuel injection
        27      2735-2984             ENG_FUEL_INJ_TYP_DESC                 The type of fuel injection used by a vehicle.  For example, Direct, Throttlebody
        28      2986-3235             ENG_CBRT_TYP_CD                       The description for the manufacturers assigned Gross Vehicle Weight (GVW) for trucks.  This rating may or may not equal the actual GVW.
        29      3237-3486             ENG_CBRT_TYP_DESC                     The description of the Polk assigned code which identifies the vehicle carburetion type.   For example Carburator, Fuel Injection, Unknown or Electric n/a.
        30      3488-3507             ENG_CBRT_BRLS                         The description for the availability of Tilt Wheel Steering.  For example Not Available, Optional, etc.
        31      3509-3509             TRK_GRSS_VEH_WGHT_RATG_CD             Contains a code that identifies the Polk standard groupings of gross vehicle weights to which a vehicle may belong.  This information is typically captured only for trucks.  Gross vehicle weight is defined as the empty weight of the vehicle, plus the maxi
        32      3511-3760             TRK_GRSS_VEH_WGHT_RATG_DESC           (Weight - GVW Rating) description
        33      3762-3767             WHL_BAS_SHRST_INCHS                   Contains the distance between the front and rear axles of a vehicle in inches of the base model of the vehicle.
        34      3769-3774             WHL_BAS_LNGST_INCHS                   Contains the longest distance between the front and rear axles of a vehicle in inches for a particular series of that vehicle.
        35      3776-4025             FRNT_TYRE_DESC                        more specific tire description (ex. Michelin Eagle P245/40ZR)¿
        36      4027-4029             FRNT_TYRE_PRSS_LBS                    (Front Tire Pressure) Vehicle Mfr reccomendation for tire pressure, in pounds/sq. in.
        37      4031-4037             FRNT_TYRE_SIZE_CD                     Describes the size of the front tire.  For example "17R245"
        38      4039-4288             FRNT_TYRE_SIZE_DESC                   (Front Tire Size Description) As in "17R245"
        39      4290-4539             REAR_TIRE_DESC                        more specific tire description (ex. Michelin Eagle P245/40ZR)¿
        40      4541-4543             REAR_TIRE_PRSS_LBS                    (Rear Tire Pressure) Vehicle Mfr reccomendation for tire pressure, in pounds/sq. in.
        41      4545-4551             REAR_TIRE_SIZE_CD                     The size of the rear tires.  example 17R245
        42      4553-4802             REAR_TIRE_SIZE_DESC                   (Rear Tire Size Description) As in "17R245"
        43      4804-4805             TRK_TNG_RAT_CD                        Contains a code that represents the towing / payload capacity of the vehicle as defined in the OEM specifications.  This information is typically only captured for vehicles classified as trucks.(Tonnage Rating) This attribute records the Truck payload
        44      4807-5056             TRK_TNG_RAT_DESC                      (Tonnage Rating) description
        45      5058-5063             SHIP_WGHT_LBS                         Contains the base weight of the vehicle, rounded to the nearest one hundred pounds, as defined in the OEM specifications.  The base weight of a vehicle is the empty weight of the base model of the vehicle (i.e., the stripped down version of the vehicle
        46      5065-5070             SHIP_WGHT_VAR_LBS                     (Weight - Shipping Weight Variance) The difference between the shipping weights of the shortest and longest wheelbase for the model. Optional equipment weight is not included and currently shipping weight variance is only used on trucks.
        47      5072-5086             MFG_BAS_MSRP                          Contains the base price of the vehicle as designated by the OEM specifications.  Base price includes only the price for the base model of the vehicle, excluding any optional equipment that may have been added as a result of the vehicle trim level
        48      5088-5102             PRIC_VAR                              (Price Variance) This is the difference between the prices of the shortest wheel base and longest wheel base for the model. Incremental costs for optional equipment is not included. This is the result of a formula of average prices across models and how m
        49      5104-5106             DRV_TYP_CD                            (Drive Type) This element describes type of driving configuration for cars and trucks such as FWD, AWD, RWD.
        50      5108-5357             DRV_TYP_DESC                          (Drive Type) description
        51      5359-5360             ISO_BASE_2010                         Contains the two position ISO Unadjusted Base Symbol for 2010 and older model years
        52      5362-5363             ISO_BASE_2011                         Contains the two position ISO Unadjusted Base Symbol for 2011 and newer model years
        53      5365-5366             ISO_COLL_SYMBOL                       Contains the two position ISO Collision Symbol for 2011 and newer model years
        54      5368-5369             ISO_COMP_SYMBOL                       Contains the two position ISO Comprehensive Symbol for 2011 and newer model years
        55      5371-5371             ISO_ROLL_IND                          It is common insurance industry practice to ¿roll over¿ insurance symbols from the previous model year into the new model year. An ¿R¿ is placed in this field to indicate that the ISO symbol for an existing series / model has been continued into the new model year and is not yet in the ISO country-wide book for the new model year. These symbols are phased out via normal software updates as the ISO symbols are updated and published.  This code is provided as a service to users, and use of the associated symbol is optional
        56      5373-5374             ISO_VSR_SYMBOL                        Contains the two position ISO VSR Symbol for 2010 and older model years
        57      5376-5376             ISO_PERFORMANCE_IND                   Contains the one position ISO High Performance Code 4 = Sporty premium (This previously coded as a ¿P¿)2 = High Performance (This previously coded as an ¿H¿)1= Intermediate (This previously coded as an ¿I¿)3= Sporty (This previously coded as an ¿S¿)
        58      5378-5380             SALE_CNTRY_CD                         (Country Sold / Specific Market) Country where the vehicle is planned to be sold (may have different emissions standards).
        59      5382-5631             SALE_CNTRY_DESC                       (Country Sold / Specific Market) description
        60      5633-5633             AIR_COND_OPT_CD                       (Air Conditioning) A single position code that indicates the availability of Air Conditioning, based on Make, Year, and Model TrimUsed in VIN Plus
        61      5635-5884             AIR_COND_OPT_DESC                     (Air Conditioning) description
        62      5886-5886             PWR_STRNG_OPT_CD                      (Steering - Power Steering) A single position code that indicates availability of Power Steering. This is based on Trim Level
        63      5888-6137             PWR_STRNG_OPT_DESC                    The description of the Polk assigned code that indicates the availability of Power Steering.  For example Not Available, Optional, etc.
        64      6139-6139             PWR_BRK_OPT_CD                        (Brakes- Power Brakes) A code that indicates the availability of power brakes as a vehicle option.
        65      6141-6390             PWR_BRK_OPT_DESC                      The description of the Polk assigned code that indicates the availability of Power Brakes.  For example Not Available, Optional, etc.
        66      6392-6392             PWR_WNDW_OPT_CD                       (Windows- Power Windows) A list of Polk assigned codes that describe the standard and optional power windows for this model(if any).
        67      6394-6643             PWR_WNDW_OPT_DESC                     The description of the Polk assigned code that indicates the availability of Power Windows, based on Make, Year, and Model Trim assigned by the VIN Team based on OEM vehicle specifications or secondary research.  For example Not Available, Optional, etc.
        68      6645-6645             TLT_STRNG_WHL_OPT_CD                  (Steering - Tilt Steering Wheel) A single position code that indicates availability of Tilt Steering.
        69      6647-6896             TLT_STRNG_WHL_OPT_DESC                The description for the availability of Tilt Wheel Steering.  For example Not Available, Optional, etc.
        70      6898-6917             ROOF_CD                               Code that represents the  type of roof that is standard on the base model/trim of the vehicle. 1= None / not available2 = Manual sun / moon roof3= Power sun / moon roof4= Removable panels5= Removable roof6= Retractable roof panel7= Other / unknown
        71      6919-7168             ROOF_DESC                             Description of ROOF_CD that represents the  type of roof that is standard on the base model/trim of the vehicle.
        72      7170-7189             OPT1_ROOF_CD                          Code that represents the  type of roof that is optional of the vehicle.
        73      7191-7440             OPT1_ROOF_DESC                        Description of  OPT1_ROOF_CD that represents the  type of roof that is optional on the vehicle.
        74      7442-7461             OPT2_ROOF_CD                          Code that represents the  type of roof that is optional of the vehicle.
        75      7463-7712             OPT2_ROOF_DESC                        Description of  OPT2_ROOF_CD that represents the  type of roof that is optional on the vehicle.
        76      7714-7733             OPT3_ROOF_CD                          Code that represents the  type of roof that is optional of the vehicle.
        77      7735-7984             OPT3_ROOF_DESC                        Description of  OPT3_ROOF_CD that represents the  type of roof that is optional on the vehicle.
        78      7986-8005             OPT4_ROOF_CD                          Code that represents the  type of roof that is optional of the vehicle.
        79      8007-8256             OPT4_ROOF_DESC                        Description of  OPT4_ROOF_CD that represents the  type of roof that is optional on the vehicle.
        80      8258-8277             ENTERTAIN_CD                          Code that represents the type of entertainment system that is standard on the base model/trim of the vehicle 1 = None2 = AM3 = AM / FM4 = AM / FM Cassette5 = AM / FM CD6 = Unknown7 = Satellite8 = AM/FM CASS/CD9 = AM/FM CD/DVDA = AM/FM CD/MP3
        81      8279-8528             ENTERTAIN_DESC                        Description of ENTERTAIN_CD; Indicates the type of entertainment system that is standard on the base model/trim of the vehicle
        82      8530-8549             OPT1_ENTERTAIN_CD                     Code that represents the type of entertainment system that is optional on the model/trim of the vehicle
        83      8551-8800             OPT1_ENTERTAIN_DESC                   Description of OPT1_ENTERTAIN_CD; Indicates the type of entertainment system that is optional on the model/trim of the vehicle
        84      8802-8821             OPT2_ENTERTAIN_CD                     Code that represents the type of entertainment system that is optional on the model/trim of the vehicle
        85      8823-9072             OPT2_ENTERTAIN_DESC                   Description of OPT2_ENTERTAIN_CD; Indicates the type of entertainment system that is optional on the model/trim of the vehicle
        86      9074-9093             OPT3_ENTERTAIN_CD                     Code that represents the type of entertainment system that is optional on the model/trim of the vehicle
        87      9095-9344             OPT3_ENTERTAIN_DESC                   Description of OPT3_ENTERTAIN_CD; Indicates the type of entertainment system that is optional on the model/trim of the vehicle
        88      9346-9365             OPT4_ENTERTAIN_CD                     Code that represents the type of entertainment system that is optional on the model/trim of the vehicle
        89      9367-9616             OPT4_ENTERTAIN_DESC                   Description of OPT4_ENTERTAIN_CD; Indicates the type of entertainment system that is optional on the model/trim of the vehicle
        90      9618-9637             TRANS_CD                              Code that represents the  type of transmission that is standard on the base model/trim of the vehicle. (A = Automatic, M= Manual, U= Unknown)
        91      9639-9888             TRANS_DESC                            Description of TRANS_CD that represents the  type of transmission that is standard on the base model/trim of the vehicle.
        92      9890-9909             TRANS_OVERDRV_IND                     Indicates whether or not TRANS_CD has overdrive (Y,N)
        93      9911-9930             TRANS_SPEED_CD                        Code value that signifies the speed of TRANS_CD( 5 = 5 Speed)
        94      9932-9951             TRANS_OPT1_CD                         Code that represents the  type of transmission that is optional on the vehicle.
        95      9953-10202            TRANS_OPT1_DESC                       Description of  TRANS_OPT1_DESC that represents the  type of transmission that is optional on the vehicle.
        96      10204-10223           TRANS_OPT1_OVERDRV_IND                Indicates whether or not TRANS_OPT1_CD has overdrive (Y,N)
        97      10225-10244           TRANS_OPT1_SPEED_CD                   Code value that signifies the speed of TRANS_OPT1_CD( 5 = 5 Speed)
        98      10246-10265           TRANS_OPT2_CD                         Code that represents the  type of transmission that is optional on the vehicle.
        99      10267-10516           TRANS_OPT2_DESC                       Description of  TRANS_OPT2_DESC that represents the  type of transmission that is optional on the vehicle.
        100     10518-10537           TRANS_OPT2_OVERDRV_IND                Indicates whether or not TRANS_OPT2_CD has overdrive (Y,N)
        101     10539-10558           TRANS_OPT2_SPEED_CD                   Code value that signifies the speed of TRANS_OPT2_CD( 5 = 5 Speed)
        102     10560-10579           TRANS_OPT3_CD                         Code that represents the  type of transmission that is optional on the vehicle.
        103     10581-10830           TRANS_OPT3_DESC                       Description of  TRANS_OPT3_DESC that represents the  type of transmission that is optional on the vehicle.
        104     10832-10851           TRANS_OPT3_OVERDRV_IND                Indicates whether or not TRANS_OPT3_CD has overdrive (Y,N)
        105     10853-10872           TRANS_OPT3_SPEED_CD                   Code value that signifies the speed of TRANS_OPT3_CD( 5 = 5 Speed)
        106     10874-10893           TRANS_OPT4_CD                         Code that represents the  type of transmission that is optional on the vehicle.
        107     10895-11144           TRANS_OPT4_DESC                       Description of  TRANS_OPT4_DESC that represents the  type of transmission that is optional on the vehicle.
        108     11146-11165           TRANS_OPT4_OVERDRV_IND                Indicates whether or not TRANS_OPT4_CD has overdrive (Y,N)
        109     11167-11186           TRANS_OPT4_SPEED_CD                   Code value that signifies the speed of TRANS_OPT4_CD( 5 = 5 Speed)
        110     11188-11188           ABS_BRK_CD                            (Brakes- ABS Code) A code that describes wether a vehicle has or does not have anti-lock brakes, and what kind of brakes they are. (Not coded for heavy truck).This is based on the series code that is assigned the vehicle from VINA.
        111     11190-11439           ABS_BRK_DESC                          (Brakes- ABS Code) description
        112     11441-11441           SECUR_TYP_CD                          (Security Type) Describes the security system (if any) installed on this model.
        113     11443-11692           SECUR_TYP_DESC                        (Security Type) description
        114     11694-11694           DR_LGHT_OPT_CD                        The description for the availability of Tilt Wheel Steering.  For example Not Available, Optional, etc.
        115     11696-11945           DR_LGHT_OPT_DESC                      The description for the manufacturers assigned Gross Vehicle Weight (GVW) for trucks.  This rating may or may not equal the actual GVW.
        116     11947-11947           RSTRNT_TYP_CD                         (Restraint Type) A Polk assigned code that identifies the type of restraints that a vehicle has based on VIN.
        117     11949-12198           RSTRNT_TYP_DESC                       (Restraint Type) description
        118     12200-12202           TRK_CAB_CNFG_CD                       (Cab Configuration) Cab Type describes the physical configuration of a truck cabin.
        119     12204-12453           TRK_CAB_CNFG_DESC                     (Cab Configuration) medium description
        120     12455-12456           TRK_FRNT_AXL_CD                       (Axle- Type, Front Axle) The location of the front axle of a truck tractor. Setforward increases stabiliy on the highway, Setback increases maneuverability in tight spaces.
        121     12458-12707           TRK_FRNT_AXL_DESC                     (Axle- Type, Front Axle) short description
        122     12709-12709           TRK_REAR_AXL_CD                       (Axle- Type, Rear Axle) Represents rear axle configuration on a truck tractor. Tandem axles increase load bearing capability.
        123     12711-12960           TRK_REAR_AXL_DESC                     (Axle- Type, Rear Axle) short description
        124     12962-12964           TRK_BRK_TYP_CD                        (Brake Type) The type of brakes on the Vehicle (currently commercial truck only). Truck VIN determines this currently
        125     12966-13215           TRK_BRK_TYP_DESC                      (Brake Type) description
        126     13217-13236           ENG_MFG_CD                            (Mfr.) A Polk assigned code given to the orginal equipment manufacture of the within a vehicle
        127     13238-13487           ENG_MFG_DESC                          (Mfr.) description
        128     13489-13494           ENG_MDL_CD                            (Model) A Polk assigned code given to the name that an manufacturer applies to a family of engines of the same type.
        129     13496-13745           ENG_MDL_DESC                          (Model) description
        130     13747-13748           ENG_TRK_DUTY_TYP_CD                   (Duty Type) A Polk assigned code that represents the duty type of a truck engine, based on manufacturer information
        131     13750-13999           ENG_TRK_DUTY_TYP_DESC                 (Duty Type) medium description
        132     14001-14001           TRK_BED_LEN_CD                        (Bed Length) Code representing the manufacturers description of the relative size of the cargo area of a pickup truck or van. A "long" Ford Ranger bed (compact pickup) may well be shorter than a "short" bed on an F350. (large industrial pickup).
        133     14003-14252           TRK_BED_LEN_DESC                      (Bed Length) description
        134     14254-14256           CMMRCL_TRLR_BODY_SPC_CD               Indicates if the engine has a supercharger or not.  Yes, No or Unknown.
        135     14258-14507           CMMRCL_TRLR_BODY_SPC_DESC             Indicates if the engine has a turbocharger. Yes, No or Unknown.
        136     14509-14513           TRK_TRLR_AXL_CNT                      (Axle- Count) Number of axles on a (truck or) commercial trailer.
        137     14515-14524           CMMRCL_TRLR_LEN                       The description of the Polk assigned code which identifies the vehicle carburetion type.   For example Carburator, Fuel Injection, Unknown or Electric n/a.
        138     14526-14530           CMMRCL_TRLR_VSL_CPCT                  The type of fuel injection used by a vehicle.  For example, Direct, Throttlebody
        139     14532-14532           CMMRCL_TRLR_VSL_MATR_CD               The description of the Polk assigned code that indicates the availability of Power Brakes.  For example Not Available, Optional, etc.
        140     14534-14783           CMMRCL_TRLR_VSL_MATR_DESC             The description of the Polk assigned code that indicates the availability of Power Steering.  For example Not Available, Optional, etc.
        141     14785-14801           VEH_VIN_POS_DESC                      The names of the specific positions of the VIN pattern.  For example: OMVCSBREKYPNNNNNN, or  OMVSGEXBKYPNNNNNN.  VIN patterns vary based on manufacturer and vehicle type.  The names of the positions do not vary.  Where OMV    = WMI CODE (O OEM; M Make; V Vehicle)C          = CARLINE       S          = SERIESB          = BODYR          = RESTRAINTE          = ENGINEK         = CHECK DIGITY          =YEAR     P          =PLANT   G          =GVWN          =BASE NUMBER
        142     14803-14803           SEGMENTATION_CD                       The Polk standard segmentation code
        143     14805-14854           SEGMENTATION_DESC                     Description of SEGMENTATION_CODE that represents the Polk Standard Segmentation applied.
        144     14856-14857           PLNT_CD                               (Plant Code) Plant code where vehicle was manufactured.
        145     14859-14908           PLNT_CNTRY_NM                         (Country) This is the country where the plant is located. Example values are USA, Canada and Japan.
        146     14910-14959           PLNT_CITY_NM                          (City) This is the city where the plant is located.
        147     14961-14963           PLNT_ISO_CNTRY_CD                     A code value which is maintained in the RDM application.
        148     14965-14967           PLNT_STAT_PROV_CD                     A code value which is maintained in the RDM application.
        149     14969-15018           PLNT_STAT_PROV_NM                     (State or Province) This is the state or province (Canada) location of the plant.
        150     15020-15020           ORGN_CD                               (Origin) A code that indicates the origin of a vehicle.
        151     15022-15271           ORGN_DESC                             (Origin) description
        152     15273-15277           ENG_DISPLCMNT_CL                      (Displacement Liters) displacement in rounded Liters, where 1,000 cubic centimeters = 1 liter. Even domestic makes will advertise displacement in terms of liters (e.g. 5.0 liter mustang, which equates to a 302 CID or 4967 cc displacement).
        153     15279-15282           ENG_BLCK_TYP_CD                       The description of the Polk assigned code that indicates the availability of Power Steering.  For example Not Available, Optional, etc.
        154     15284-15533           ENG_BLCK_TYP_DESC                     The description of the Polk assigned code that indicates the availability of Power Windows, based on Make, Year, and Model Trim assigned by the VIN Team based on OEM vehicle specifications or secondary research.  For example Not Available, Optional, etc.
        155     15535-15538           ENG_HEAD_CNFG_CD                      (Head Configuration) Describes the cylinder heads camshaft/valve configuration.
        156     15540-15789           ENG_HEAD_CNFG_DESC                    (Head Configuration) description
        157     15791-15792           ENG_VLVS_PER_CLNDR                    (Valves Per Cylinder) Number of intake/exhaust valves per cylinder.
        158     15794-15795           ENG_VLVS_TOTL                         (Valves Total) Total number of intake/exhaust valves.
        159     15797-15806           ENG_VIN_CD                            (Code) Code derived from the VIN (not the secondary VIN for a motorcycle). Usually a single character,  some manufactures give full positions 4-8 and engine information from that; they do not break it down any further.
        160     15808-15808           VRG_VIS_THEFT
        161     15810-15820           NADA_ID1                              NADA Key - Internal use
        162     15822-15857           NADA_SERIES1                          NADA Series description of the vehicle
        163     15859-15908           NADA_BODY1                            NADA Bodystyle for SERIES1
        164     15910-15925           NADA_MSRP1                            NADA MSRP value for SERIES1
        165     15927-15942           NADA_GCW1                             NADA Gross Combined Weight Rating (GCWR) for SERIES1.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        166     15944-15959           NADA_GVWC1                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES1.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        167     15961-15971           NADA_ID2                              NADA Key for the second series option - Internal use
        168     15973-16008           NADA_SERIES2                          NADA Series description of the vehicle  - Option2
        169     16010-16059           NADA_BODY2                            NADA Bodystyle for SERIES2
        170     16061-16076           NADA_MSRP2                            NADA MSRP value for SERIES2
        171     16078-16093           NADA_GCW2                             NADA Gross Combined Weight Rating (GCWR) for SERIES2.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        172     16095-16110           NADA_GVWC2                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES2.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        173     16112-16122           NADA_ID3                              NADA Key for the third series option - Internal use
        174     16124-16159           NADA_SERIES3                          NADA Series description of the vehicle  - Option3
        175     16161-16210           NADA_BODY3                            NADA Bodystyle for SERIES3
        176     16212-16227           NADA_MSRP3                            NADA MSRP value for SERIES3
        177     16229-16244           NADA_GCW3                             NADA Gross Combined Weight Rating (GCWR) for SERIES3.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        178     16246-16261           NADA_GVWC3                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES3.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        179     16263-16273           NADA_ID4                              NADA Key for thefourth series option - Internal use
        180     16275-16310           NADA_SERIES4                          NADA Series description of the vehicle  - Option4
        181     16312-16361           NADA_BODY4                            NADA Bodystyle for SERIES4
        182     16363-16378           NADA_MSRP4                            NADA MSRP value for SERIES4
        183     16380-16395           NADA_GCW4                             NADA Gross Combined Weight Rating (GCWR) for SERIES4.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        184     16397-16412           NADA_GVWC4                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES4.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        185     16414-16424           NADA_ID5                              NADA Key for the fifth series option - Internal use
        186     16426-16461           NADA_SERIES5                          NADA Series description of the vehicle  - Option5
        187     16463-16512           NADA_BODY5                            NADA Bodystyle for SERIES5
        188     16514-16529           NADA_MSRP5                            NADA MSRP value for SERIES5
        189     16531-16546           NADA_GCW5                             NADA Gross Combined Weight Rating (GCWR) for SERIES5.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        190     16548-16563           NADA_GVWC5                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES5.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        191     16565-16575           NADA_ID6                              NADA Key for the sixth series option - Internal use
        192     16577-16612           NADA_SERIES6                          NADA Series description of the vehicle  - Option6
        193     16614-16663           NADA_BODY6                            NADA Bodystyle for SERIES6
        194     16665-16680           NADA_MSRP6                            NADA MSRP value for SERIES6
        195     16682-16697           NADA_GCW6                             NADA Gross Combined Weight Rating (GCWR) for SERIES6.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        196     16699-16714           NADA_GVWC6                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES6.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        197     16716-16726           NADA_ID7                              NADA Key for the seventh series option - Internal use
        198     16728-16763           NADA_SERIES7                          NADA Series description of the vehicle  - Option7
        199     16765-16814           NADA_BODY7                            NADA Bodystyle for SERIES7
        200     16816-16831           NADA_MSRP7                            NADA MSRP value for SERIES7
        201     16833-16848           NADA_GCW7                             NADA Gross Combined Weight Rating (GCWR) for SERIES7.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        202     16850-16865           NADA_GVWC7                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES7.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        203     16867-16877           NADA_ID8                              NADA Key for the eighth series option - Internal use
        204     16879-16914           NADA_SERIES8                          NADA Series description of the vehicle  - Option8
        205     16916-16965           NADA_BODY8                            NADA Bodystyle for SERIES8
        206     16967-16982           NADA_MSRP8                            NADA MSRP value for SERIES8
        207     16984-16999           NADA_GVWC8                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES8.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        208     17001-17016           NADA_GCW8                             NADA Gross Combined Weight Rating (GCWR) for SERIES8.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        209     17018-17028           NADA_ID9                              NADA Key for the ninth series option - Internal use
        210     17030-17065           NADA_SERIES9                          NADA Series description of the vehicle  - Option9
        211     17067-17116           NADA_BODY9                            NADA Bodystyle for SERIES9
        212     17118-17133           NADA_MSRP9                            NADA MSRP value for SERIES9
        213     17135-17150           NADA_GCW9                             NADA Gross Combined Weight Rating (GCWR) for SERIES9.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        214     17152-17167           NADA_GVWC9                            NADA Gross Vehicle Weight Rating (GVWR) for SERIES9.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        215     17169-17179           NADA_ID10                             NADA Key for the tenth series option - Internal use
        216     17181-17216           NADA_SERIES10                         NADA Series description of the vehicle  - Option10
        217     17218-17267           NADA_BODY10                           NADA Bodystyle for SERIES10
        218     17269-17284           NADA_MSRP10                           NADA MSRP value for SERIES10
        219     17286-17301           NADA_GCW10                            NADA Gross Combined Weight Rating (GCWR) for SERIES10.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        220     17303-17318           NADA_GVWC10                           NADA Gross Vehicle Weight Rating (GVWR) for SERIES10.  Only populated for NADA USED Commerical Guide Vehicles 1992 - Current
        221     17320-17320           INCOMPLETE_IND                        Indicator that signifies whether the vehicle is consider "incomplete" (Y/N)
        222     17322-17341           BAT_TYP_CD                            A value that identifies the kind of battery in the vehicle. For example: PbA - Lead Acid,NMH - Nickel Metal Hydride.
        223     17343-17592           BAT_TYP_DESC                          The description of the Polk assigned code for the Battery Type Code. For example:PbA - Lead Acid,NMH - Nickel Metal Hydride.
        224     17594-17613           BAT_KW_RATG_CD                        The measure of total battery power expressed in kilowatts.   For example:  71KW, 85KW, 75KW, 67KW.
        225     17615-17864           BAT_VLT_DESC                          The voltage rating of the battery as provided by the manufactuer.
        226     17866-17866           ENG_ASP_SUP_CHGR_CD                   Indicates if the engine has a supercharger or not.
        227     17868-18117           ENG_ASP_SUP_CHGR_DESC                 Indicates if the engine has a supercharger or not.  Yes, No or Unknown.
        228     18119-18119           ENG_ASP_TRBL_CHGR_CD                  Indicates if the engine has a turbocharger. Yes, No or Unknown.
        229     18121-18370           ENG_ASP_TRBL_CHGR_DESC                The description of the Polk assigned code which identifies the vehicle carburetion type.   For example Carburator, Fuel Injection, Unknown or Electric n/a.
        230     18372-18391           ENG_ASP_VVTL_CD                       The type of fuel injection used by a vehicle.  For example, Direct, Throttlebody
        231     18393-18642           ENG_ASP_VVTL_DESC                     The description of the Polk assigned code that indicates the availability of Power Brakes.  For example Not Available, Optional, etc.
        232     18644-18646           LPM_LIABILITY_SYMBOL                  ISO three position Liability Symbol
        233     18648-18650           LPM_PIP_MED_PAY_SYMBOL                ISO three position PIP / Medical Symbol
        234     18652-18652           LPM_ROLL_IND                          It is common insurance industry practice to ¿roll over¿ insurance symbols from the previous model year into the new model year. An ¿R¿ is placed in this field to indicate that the ISO symbol for an existing series / model has been continued into the new model year and is not yet in the ISO country-wide book for the new model year. These symbols are phased out via normal software updates as the ISO symbols are updated and published.  This code is provided as a service to users, and use of the associated symbol is optional
        235     18654-18659           ACES_VEHICLE_ID
        236     18661-18666           ACES_BASE_VEHICLE
        237     18668-18672           ACES_MAKE_ID
        238     18674-18678           ACES_MDL_ID
        239     18680-18684           ACES_SUB_MDL_ID
        240     18686-18687           ACES_VEH_TYP_ID
        241     18689-18692           ACES_YEAR_ID
        242     18694-18695           ACES_FUEL
        243     18697-18698           ACES_FUEL_DELIVERY
        244     18700-18702           ACES_ENG_VIN_ID
        245     18704-18705           ACES_ASP_ID
        246     18707-18708           ACES_DRIVE_ID
        247     18710-18711           ACES_BODY_NMBR_DR
        248     18713-18714           ACES_BODY_TYPE
        249     18716-18717           ACES_REGION_ID
        250     18719-18722           ACES_LITERS
        251     18724-18728           ACES_CC_DISPLACEMENT
        252     18730-18733           ACES_CI_DISPLACEMENT
        253     18735-18736           ACES_CYLINDERS
        254     18738-18738           ACES_RESERVED
        255     18740-18743           VRG_RESERVED
        256     18745-18746           VRG_LIABILITY
        257     18748-18749           VRG_PIP_MED_PAY
        258     18751-18752           VRG_COLLISION
        259     18754-18755           VRG_OTHER
        260     18757-18757           VRG_INTERNAL
        261     18759-18822           VIN_PATRN                             Contains the pattern of a VIN established by the Original Equipment Manufacturer (OEM) that is used to identify the characteristics of a given class of vehicle as they were defined by the OEM.  It does not generally identify a specific instance of a vehicle
        262     18824-18826           MOTR_CYCL_USAG_CD                     A further breakdown of body style for motorcycles to indicate if is it On-Road or Off-Road.
        263     18828-19077           MOTR_CYCL_USAG_DESC                   A further breakdown of body style for motorcycles to indicate if is it On-Road or Off-Road.
        264     19079-19079           PS_LEVEL_IND
        265     19081-19090           PS_MDL_PHOTO_CD                       Identifies the model photo associated with the vehicle
        266     19092-19101           PS_TRIM_PHOTO_NM                      Identifies the trim photo associated with the vehicle
        267     19103-19152           PS_MKTG_DIV_NM                        OEM division that produces the vehicle
        268     19154-19203           PS_VEH_MDL_NM                         model name of the vehicle
        269     19205-19454           PS_VEH_MFG_DESC                       OEM that produces the vehicle
        270     19456-19459           PS_VEH_MDL_YR                         model year of the vehicle
        271     19461-19470           PS_TRIM_ID                            Key
        272     19472-19521           PS_TRIM_NM                            The name of the trim used by AIC.  Includes additional information to uniquely identify the trim, such as, wheelbase, entertainment, etc.
        273     19523-19523           PS_ABS_BRAKES_CD                      Identifies if ABS Brakes are available for the vehicle, i.e., A computer-controlled system that senses when one or more of the tires are skidding during heavy braking and rapidly modulates hydraulic brake pressure to reduce rear-wheel lock-up.
        274     19525-19774           PS_SEG_DESC                           Consumer Segment: The identification of a car or sport utility based on the combination of body style and family, sport or luxury classifications; the identification of a pickup as compact or full size; the identification of a van as full size or mini.
        275     19776-19777           PS_SEG_ONE_CD                         Consumer Segment: The identification of a car or sport utility based on the combination of body style and family, sport or luxury classifications; the identification of a pickup as compact or full size; the identification of a van as full size or mini.
        276     19779-19780           PS_SEG_THREE_CD                       Consumer Segment: The identification of a car or sport utility based on the combination of body style and family, sport or luxury classifications; the identification of a pickup as compact or full size; the identification of a van as full size or mini.
        277     19782-19783           PS_SEG_TWO_CD                         Consumer Segment: The identification of a car or sport utility based on the combination of body style and family, sport or luxury classifications; the identification of a pickup as compact or full size; the identification of a van as full size or mini.
        278     19785-19794           PS_STD_ENG_DESC                       Standard Engine: The engine included in a vehicle at no extra charge.
        279     19796-19815           PS_STD_TRANS_DESC                     Standard Transmission: The transmission included in a vehicle at no extra charge.
        280     19817-19836           PS_STYLE_STAT_CD                      Destination Charge: The cost of shipping and handling of a vehicle.
        281     19838-19840           PS_VEH_BODY_STYLE_CD                  Body Style: The identification of a vehicle based on the shape of the exterior body configuration.  Body Styles include Coupe, Sedan, Convertible, Wagon, Pick-Up Trick, Passenger Van or Cargo Van.
        282     19842-19842           PS_ANTI_THEFT_CD                      Summary Identifier
        283     19844-19844           PS_AUTO_LOCKING_RETRACTOR_CD          Identifies if an Automatic Locking Retractor is available for the vehicle, i.e. A type of restraint system found on some 3-point passenger seatbelts that allows the securing of a child seat without the use of a seatbelt locking anchor.
        284     19846-19851           PS_BASE_MSRP_AMT                      Equipped Price: The total of the Manufacturer Suggested Retail Price (MSRP) and Optional Equipment
        285     19853-19856           PS_BOX_CARGO_HGT_INCH                 Box/Cargo Height: For trucks: Distance from the floor of the cargo bed to the top of the bed wall. For vans and sport utilities: Distance from the floor of the cargo area to the ceiling.
        286     19858-19862           PS_BOX_CARGO_LEN_INCH                 Box/Cargo Length: For Trucks: The distance from back of cab to tailgate (at floor), measured within bed walls. For cargo vans: The distance from back of seat to tailgate (at floor). For passenger vans and sport utilities: Distance from back of rear-most s
        287     19864-19867           PS_BOX_CARGO_WALL_WID_INCH            Box/Cargo Width (Wall): For trucks: Distance from side to side, exclusive of wheel wells, measured within bed walls (at floor). For cargo vans and sport utility vehicles: Distance from side to side at floor, exclusive of wheel wells, measured in interior
        288     19869-19872           PS_BOX_CARGO_WHL_WID_INCH             Box/Cargo Width (Wheel): For trucks, cargo vans, and sport utility vehicles: Distance from side to side, between wheel wells (at floor).
        289     19874-19874           PS_BRK_RESIST_SECUR_GLASS_CD          Identifies if Break Resistant Security Glass is available for the vehicle, i.e., A theft deterrent feature that prevents unwanted entry into a vehicle through breaking of the windshield, rear window or side windows.
        290     19876-19885           PS_BSC_WRTY_MILES                     Warranty Basic Time (Miles): The length of the manufacturer basic warranty expressed in miles.
        291     19887-19891           PS_BSC_WRTY_TM_MTHS                   Warranty Basic Time (Months): The length of the manufacturer basic warranty expressed in months.
        292     19893-19895           PS_CARGO_VOL_EPA                      Cargo Volume (EPA): The cargo volume (in cubic feet) as reported by the EPA.
        293     19897-19901           PS_CARGO_VOL_MFG                      Cargo Volume (MFR): The cargo volume (in cubic feet) as reported by the manufacturer.
        294     19903-19903           PS_CHILD_SFTY_DOOR_LOCKS_CD           Identifies if Child Safety Door Locks are available for the vehicle, i.e., A child protection device that, when engaged, prevents opening a rear door from inside the vehicle.
        295     19905-19914           PS_CRASH_TEST_RTNG_CD                 Crash Test Rating: Conducted by the National Highway Traffic Safety Administration (NHTSA), Crash Test Ratings demonstrate the relative crash protection provided to front seat occupants in a head-on collision when all of the vehicles occupant protection
        296     19916-19916           PS_CRNRNG_LIGHTS_CD                   Identifies if Cornering Lights are available for the vehicle, i.e., Exterior lights mounted on the side of a vehicle that are activated with the directionals to illuminate the road in the direction the vehicle is turning.
        297     19918-19922           PS_CURB_WT_AUTO_TRANS_LBS             Curb Weight (Manual Transmission): The weight of the empty vehicle with standard equipment and with fuel tank, cooling system and crankcase filled.
        298     19924-19928           PS_CURB_WT_MAN_TRANS_LBS              Curb Weight (Automatic Transmission): The weight of the empty vehicle with standard equipment and with fuel tank, cooling system and crankcase filled.
        299     19930-19935           PS_DLR_INVC_AMT                       Dealer Invoice: The price a dealership pays to purchase a vehicle directly from a manufacturer.
        300     19937-19937           PS_DRIVER_AIRBAG_CD                   Identifies if a Driver Airbag is available for the vehicle
        301     19939-19939           PS_DRL_CD                             Identifies if a Daytime Running Lights are available for the vehicle
        302     19941-19945           PS_DSTINT_CHRG_AMT                    Destination Charge: The cost of shipping and handling of a vehicle.
        303     19947-19947           PS_DVD_PLAYER_CD                      Identifies if a DVD Player is available for the vehicle
        304     19949-19949           PS_ELECT_BRK_ASST_CD                  Identifies if Electronic Brake Assistance is available for the vehicle, i.e., A system that measures the speed and force with which the brake pedal has been pushed, determining whether the driver has attempted an emergency stop.
        305     19951-19951           PS_ELECT_PARKING_AID_CD               Identifies if an electronic parking aid is available for the system, i.e. A system that utilizes ultrasonic sensors located in the front and/or rear bumper(s) to detect nearby objects.
        306     19953-19953           PS_EMRGY_FUEL_SHUTOFF_DEV_CD          Identifies if an Emergency Fuel Shutoff Device is available for the vehicle, i.e. A safety device that measures a specific G-force impact and disables power to the fuel pump in the case of an accident.
        307     19955-19956           PS_ENG_LOC_CD                         Engine Location: The location of the engine.
        308     19958-19967           PS_EPA_CLS_CD                         EPA Class: The classification of a vehicle by the Environmental Protection Agency based on either the combined passenger and cargo/luggage volume or gross vehicle weight rating of the vehicle.
        309     19969-19969           PS_FIRST_AID_KIT_CD                   Identifies if a First Aid Kit is available for the vehicle
        310     19971-19971           PS_FOG_LIGHTS_CD                      Identifies if Foglights are available for the vehicle.
        311     19973-19973           PS_FOUR_WHL_ABS_BRAKES_CD             Identifies if 4-Wheel ABS Brakes are available for the vehicle, i.e., A computer-controlled system that senses when one or more of the tires are skidding during heavy braking and rapidly modulates hydraulic brake pressure to reduce wheel lock-up.
        312     19975-19978           PS_FRONT_BRK_TYPE_CD                  Brakes (Front): Brakes are classified as disc or drum for the front and rear wheels.  Most vehicles have Front Disc Brakes.
        313     19980-19983           PS_FRONT_HEADROOM_INCH                Headroom (Front): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position.
        314     19985-19988           PS_FRONT_HIPROOM_INCH                 Hiproom (Front): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position
        315     19990-19993           PS_FRONT_LEGROOM_INCH                 Legroom (Front): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position
        316     19995-19998           PS_FRONT_SHOULDER_ROOM_INCH           Shoulder Room (Front): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position
        317     20000-20000           PS_FRONT_SIDE_AIRBAG_CD               Identifies if a Front Side Airbag is available for the vehicle
        318     20002-20002           PS_FRONT_SIDE_AIRBAG_HEAD_CD          Identifies if a Front Side Row Airbag with Head Protection is available for the vehicle., i.e., A passive restraint safety device consisting of one or two airbags typically mounted in the seat back of a front seat, front side door or mounted in the front
        319     20004-20008           PS_FRONT_SUSPENSION_TYPE_CD           Front Suspension: The components and specific design configuration that determines the overall ride and handling characteristics for the front of the vehicle.
        320     20010-20013           PS_FRONT_TRACK_INCH                   Track (Front): The measured length (in inches) from the center of the front left tire to the center of the front right tire. Also called "Front Tread".
        321     20015-20016           PS_FRONT_WT_DIST_AUTO_PCT             Weight Distribution AT (Front): The portion of total weight for supported by the front and rear wheels, expressed as a percentage (with automatic transmission)
        322     20018-20019           PS_FRONT_WT_DIST_MAN_PCT              Weight Distribution MT (Front): The portion of total weight for supported by the front and rear wheels, expressed as a percentage (with manual transmission)
        323     20021-20024           PS_FUEL_CPCTY_GAL                     Fuel Capacity: The total fuel capacity (in gallons) for the vehicle with standard equipment. The Fuel Capacity measurement does not include optional second fuel tanks for trucks.
        324     20026-20030           PS_GAS_GUZZLER_TAX_AMT
        325     20032-20035           PS_GROUND_CLRNC_INCH                  Ground Clearance: The height, in inches, of the lowest point of the vehicle to the ground.  Also known as minimum running ground clearance
        326     20037-20041           PS_GVWR_MAX                           GVWR (Maximum): The maximum loaded weight possible when certain option packages are added that increase the allowed weight.
        327     20043-20047           PS_GVWR_STD                           GVWR (Standard): The maximum loaded weight of the base vehicle including driver and passenger.
        328     20049-20049           PS_HANDS_FREE_TEL_CD                  Identifies if a Hands Free or Voice Activated Telephone is available for the vehicle
        329     20051-20051           PS_HEATED_EXTERIOR_MIRROR_CD          Identifies if a Heated Exterior Mirror is available for the vehicle
        330     20053-20053           PS_HEATED_WINDSHIELD_GLASS_CD         Identifies if Heated Windshield Glass is available for the vehicle, i.e., A specially designed windshield that has micro-filament heating wires imbedded in the glass to defrost and defog the windshield.
        331     20055-20055           PS_INTELLIGENT_CRUISE_CTL_CD          Identifies if Intelligent Cruise Control is available for the vehicle, i.e. An advanced cruise control system that allows a vehicle to keep moving at a set speed without use of the gas pedal and maintains a set interval from the vehicle traveling in front
        332     20057-20057           PS_INTERNET_ACCESS_CD                 Identifies if E-Mail/Internet Access is available for the vehicle
        333     20059-20059           PS_LOCKING_DIFF_CD                    Identifies if a Locking Differential is available for the vehicle, i.e., A differential splits and distributes engine power between wheels on the drive axle(s) or between the axles if equipped with a Center Differential.
        334     20061-20063           PS_MAX_DOOR_CNT                       Doors (Maximum): The highest number of doors available on the vehicle, taking into account any optional doors. Optional doors may be available on trucks or vans.
        335     20065-20067           PS_MAX_PAX_SEATING_CNT                Seating (Maximum): The highest number of passengers which can be accommodated according to optional seating configurations.
        336     20069-20078           PS_MAX_TOW_CPCTY                      Tow Capacity (Maximum): The highest possible weight that could be pulled, taking into account any optional features that could increase towing capacity, i.e. a larger engine or a special towing package.
        337     20080-20097           PS_MFG_TRIM_ID                        Chrome defined trim ID
        338     20099-20099           PS_NAV_AID_CD                         Identifies if a navigational aid is available for the vehicle, i.e. a system that provides navigational instructions via a visual display using Global Positioning Satellite (GPS) technology and/or CD-ROM based map databases.
        339     20101-20101           PS_NIGHT_VISION_CD                    Identifies if Night Vision is available for the vehicle, i.e. A system that utilizes thermal imaging technology to sense heat energy emitted by objects, displaying the information as a black and white picture to assist driver vision when in poor visibilit
        340     20103-20103           PS_OPTL_AXL_RATIO_CD                  Identifies if an Optional Axle Ratio is available for the vehicle, i.e., An axle ratio available as an option that will replace the standard configuration.
        341     20105-20109           PS_OPTL_PAX_SEATING_CD                Seating (Optional): The number of passengers that can be accommodated given seating configurations that can be purchased as options on the vehicle. If more than one optional seating arrangement is available, the highest capacity is listed first.
        342     20111-20111           PS_OVERSIZE_EXT_MIRROR_CD             Identifies if an Oversize or Wide Angle Exterior Mirror is available for the vehicle.
        343     20113-20113           PS_PAX_AIRBAG_CD                      Identifies if a Passenger Airbag is available for the vehicle
        344     20115-20115           PS_PAX_AIRBAG_CUTOFF_CD               Identifies if a Passenger Airbag Cutoff is available for the vehicle, i.e. A safety device that allows the front passenger airbag to be manually turned off via the ignition key.
        345     20117-20121           PS_PAX_VOL_EPA                        Passenger Volume (EPA): The passenger volume (in cubic feet) as reported by the EPA.
        346     20123-20127           PS_PAX_VOL_MFG                        Passenger Volume (MFR): The passenger volume (in cubic feet) as reported by the manufacturer.
        347     20129-20133           PS_PAYLOAD_MAX                        Payload (Maximum): The maximum weight of people, cargo and body equipment that is allowed when certain option packages are purchased that would allow more weight to be carried, I.e. heavy duty suspension and shock absorbers.
        348     20135-20135           PS_PAYLOAD_PKG_CD                     Identifies if a Payload Package is available for the system, i.e., A package that upgrades the vehicle to carry a higher payload.
        349     20137-20141           PS_PAYLOAD_STD                        Payload (Standard): The weight of the actual cargo and occupant(s) that can be carried by the base vehicle.
        350     20143-20143           PS_PERIMETER_LIGHTING_CD              Identifies if Perimeter Lighting is available for the vehicle, i.e., Exterior lights that illuminate the area around a vehicle while entering and exiting at nighttime.
        351     20145-20148           PS_PKG_REQ_CD                         Indicates if an associated package is required for the vehicle
        352     20150-20150           PS_PORTABLE_TEL_CD                    Identifies if a Portable Telephone is available for the vehicle
        353     20152-20161           PS_POWERTRAIN_WRTY_MILES              Warranty Powertrain Time (Miles): The length of the manufacturer powertrain warranty expressed in miles.
        354     20163-20167           PS_POWERTRAIN_WRTY_TM_MTHS            Warranty Powertrain Time (Months): The length of the manufacturer powertrain warranty expressed in months.
        355     20169-20169           PS_RADIO_ANTI_THEFT_CD                Identifeis if Radio Anti-Theft is available for the vehicle, i.e., A system that provides a strong deterrent to theft of the audio system by relying on a combination of features that will render the audio system inoperative if stolen.
        356     20171-20174           PS_REAR_BRK_TYPE_CD                   Brakes (Rear):  Brakes are classified as disc or drum for the front and rear wheels.
        357     20176-20180           PS_REAR_SUSPENSION_TYPE_CD            Rear Suspension: The components and specific design configuration that determines the overall ride and handling characteristics for the rear of the vehicle.
        358     20182-20185           PS_REAR_TRACK_INCH                    Track (Rear): The measured length (in inches) from the center of the rear left tire to the center of the rear right tire. Also called "Rear Tread".
        359     20187-20188           PS_REAR_WT_DIST_AUTO_PCT              Weight Distribution AT (Rear): The portion of total weight for supported by the front and rear wheels, expressed as a percentage (with automatic transmission)
        360     20190-20191           PS_REAR_WT_DIST_MAN_PCT               Weight Distribution MT (Rear): The portion of total weight for supported by the front and rear wheels, expressed as a percentage (with manual transmission)
        361     20193-20202           PS_RUST_WRTY_MILES_CD                 Warranty Rust (Miles): The length of the manufacturer rust/corrosion warranty expressed in miles.
        362     20204-20208           PS_RUST_WRTY_TM_MTHS                  Warranty Rust Time (Months): The length of the manufacturer rust/corrosion warranty expressed in months.
        363     20210-20210           PS_SEAT_BELT_HGT_ADJR_CD              Identifies if a Seat Belt Height Adjuster is available for the vehicle
        364     20212-20212           PS_SEAT_BELT_PRETENSIONER_CD          Identifies if Seatbelt Pretensioners are available for the vehicle, i.e. A type of restraint system that allows a seatbelt to rapidly take up slack during certain types of frontal impacts.
        365     20214-20217           PS_SEC_ROW_HEADROOM_INCH              Headroom (Row 2): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position.
        366     20219-20222           PS_SEC_ROW_HIPROOM_INCH               Hiproom (Row 2): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position
        367     20224-20227           PS_SEC_ROW_LEGROOM_INCH               Legroom (Row 2): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position
        368     20229-20232           PS_SEC_ROW_SHOULDER_ROOM_INCH         Shoulder Room (Row 2): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position.
        369     20234-20234           PS_SEC_ROW_SIDE_AIRBAG_CD             Identifies if a Second Row Side Airbag is available for the vehicle
        370     20236-20236           PS_SEC_ROW_SIDE_AIRBAG_HD_CD          Identifies if a Second Row Airbag with Head Protection is available for the vehicle., i.e., A passive restraint safety device consisting of one or two airbags typically mounted in the seat back of a front seat, front side door or mounted in the front seat
        371     20238-20238           PS_SFTY_ROLLBAR_CD                    Identifies if a Safety Rollbar is available for the vehicle, i.e., A safety device designed to protect vehicle occupants during an accident involving a roll over.
        372     20240-20240           PS_SIDE_GUARD_DOOR_BEAMS_CD           Identifeis if a Side Guard Door Beam is available for the vehicle, i.e., Crossbars, or heavy metal beams located in the inner panel of a vehicle door that provides added strength and protects a passenger from injury in the event of a collision.
        373     20242-20242           PS_SIDE_HEAD_CRTN_AIRBAGS_CD          Identifeis if Side Head Curtain Airbags are available for the vehicle
        374     20244-20244           PS_SIGNALING_EXT_MIRRORS_CD           Identifeis if Signaling Exterior Mirrors are available for the vehicle, i.e., Exterior rearview mirrors with integral turn signal indicators that help other vehicles see a drivers turning intentions, especially when traveling within that vehicles blind spot
        375     20246-20246           PS_SNOW_PLOW_PREP_PKG_CD              Identifies if a Snow Plow Prep Package is available for the vehicle, i.e., A package that equips a truck or sport utility vehicle for snow plowing.
        376     20248-20252           PS_STD_PAX_SEATING_CNT                Seating (Standard): The number of passengers that can be accommodated given the standard seating configuration for the vehicle.
        377     20254-20263           PS_STD_TOW_CPCTY                      Tow Capacity (Standard): The weight that could be towed given the standard powertrain (engine, transmission, driveline and axle ratio) that is standard on the vehicle. The figure represents the trailer plus any cargo that is carried in the trailer.
        378     20265-20268           PS_STRNG_DIA_LEFT                     Steering Diameter (Left): The distance required when turning the car tightly (in feet). The outside front, curb-to-curb measurement is used.
        379     20270-20273           PS_STRNG_DIA_RIGHT                    Steering Diameter (Right): The distance required when turning the car tightly (in feet). The outside front, curb-to-curb measurement is used.
        380     20275-20284           PS_STRNG_TYPE_CD                      Steering Type: Rack and Pinion uses a horizontal toothed rack that is meshed with a pinion gear. Recirculating Ball uses steel ball bearings in a gearbox.  Worm and Roller is currently found only in the Land Rover Discovery.
        381     20286-20286           PS_STRNG_WHL_MNTD_CONTROLS_CD         Identifies if Steering Wheel Mounted Controls are available for the vehicle
        382     20288-20288           PS_SVC_MAINT_INTVL_IND_CD             Identifies if a Service or Maintenance Interval Indicator is available for the vehicle, i.e., an audible or visual device that indicates that a vehicle requires service or maintenance at a defined interval.
        383     20290-20290           PS_TEL_CD                             Identifies if a telephone is available for the vehicle.
        384     20292-20292           PS_TELEMATIC_SYS_CD                   Identifies if Telematic Systems are available for the vehicles, i.e. vehicle monitoring systems, wireless voice/data communications and GPS (Global Positioning System) location capabilities.
        385     20294-20294           PS_TELEPHONES_CD                      Summary Identifier
        386     20296-20299           PS_THRD_ROW_HEADROOM_INCH             Headroom (Row 3): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position.
        387     20301-20304           PS_THRD_ROW_HIPROOM_INCH              Hiproom (Row 3): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position
        388     20306-20309           PS_THRD_ROW_LEGROOM_INCH              Legroom (Row 3): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position
        389     20311-20314           PS_THRD_ROW_SHLDR_ROOM_INCH           Shoulder Room (Row 3): All interior measurements are based on a standardized method of measuring angles in a seat which is in the rear-most sitting position
        390     20316-20316           PS_THREE_PT_CTR_SEAT_BELT_CD          Identifies if a 3-Point Center Seatbelt is available for the vehicle, i.e., A Front, Second or Third Row Bench Seat that is equipped with a center lap and shoulder seatbelt.
        391     20318-20320           PS_TIRE_ASPECT_RATIO                  Tire Aspect Ratio: The sidewall height of a tire based on the percentage of the width.
        392     20322-20323           PS_TIRE_CNSTRCN_CD                    Tire Construction: The internal composition of a tire typically radial.
        393     20325-20325           PS_TIRE_PRES_MONTR_CD                 Identifies if a Tire Inflation/Pressure Monitor is available for the vehicle
        394     20327-20328           PS_TIRE_TYPE_CD                       Tire Type: The designation of tire according to its intended usage as either passenger (P) or light truck duty (LT).
        395     20330-20332           PS_TIRE_WHL_DIA_INCH                  Tire Wheel Diameter: The wheel rim diameter measured in inches that the tire is designed to fit.
        396     20334-20336           PS_TIRE_WID_MM                        Tire Width: The width of a tire measured in millimeters.
        397     20338-20339           PS_TOP_TYPE_OPTL_CD                   Top Type (Optional): The type of roof that is optional on the vehicle. Roof types include soft, hard, fixed, removable, partial and complete.
        398     20341-20342           PS_TOP_TYPE_STD_CD                    Top Type (Standard): The type of roof which is standard on the vehicle.  Roof types include soft, hard, fixed, removable, partial and complete.
        399     20344-20344           PS_TOW_HITCH_RCVR_CD                  Identifies if a Tow Hitch Receiver is available for the vehicle, i.e., A tow hitch that is typically attached to the frame of the vehicle and accepts a hitch/ball assembly into a ¿receiver¿ slot.
        400     20346-20346           PS_TOWING_PREP_PKG_CD                 Identifies if a Towing Prep Package is available for the vehicle, i.e., A package that equips a vehicle for towing.
        401     20348-20348           PS_TRUNK_ANTI_TRAP_DEVICE_CD          Identifies if a Trunk Anti-Trap Device is available for the vehicle, i.e., A safety device designed to prevent children from accidentally trapping themselves within the trunk.
        402     20350-20350           PS_TWO_WHL_ABS_BRAKES_CD              Identifies if 2-Wheel ABS Brakes are available for the vehicle, i.e., A computer-controlled system that senses when one or more of the tires are skidding during heavy braking and rapidly modulates hydraulic brake pressure to reduce rear-wheel lock-up.
        403     20352-20352           PS_AUTO_TRANS_OIL_COOLER_CD           Identifies if an Upgraded Automatic Transmission Cooler is available for the vehicle, i.e., An optional fluid cooler that is an auxiliary radiator designed to remove excess heat from an automatic transmission during heavy-duty use.
        404     20354-20354           PS_VEH_ANTI_THEFT_CD                  Identifies if Vehicle Anti-Theft is available for the vehicle, i.e., A system that provides anti-theft protection to the entire vehicle.
        405     20356-20375           PS_VEH_BUILT_CNTRY_CD                 The country the vehicle is produced in.
        406     20377-20377           PS_VEH_CLRNC_MARKER_LIGHTS_CD         Identifies if Pickup/SUV Clearance or Cab Marker Lights are available for the vehicle, i.e., Several exterior mounted lights found on the front edge of the roof or on the mirrors of a sport utility vehicle or Pickup Truck.
        407     20379-20381           PS_VEH_DOOR_CNT                       Doors (Standard): The standard number of doors available on the vehicle.  The number of doors excludes liftgate or hatchback door.
        408     20383-20387           PS_VEH_HGT_INCH                       Height: The height from the vehicle roof to the ground.
        409     20389-20393           PS_VEH_LEN_INCH                       Length: The length from bumper to bumper.
        410     20395-20395           PS_VEH_LOC_SYS_CD                     Identifies if a Vehicle Location System is available for the vehicle, i.e., a system based on Global Positioning Satellites (GPS) that typically provides vehicle location to assist in vehicle theft recovery.
        411     20397-20397           PS_VEH_LOCKS_CD                       Summary Identifier
        412     20399-20408           PS_VEH_OPTL_DRV_TYPE_CD               Driveline (Optional): An available driveline that differs from the standard configuration. FWD = Front Wheel Drive, RWD = Rear Wheel Drive, 4WD-PT = 4WD Part Time, 4WD-FT = 4WD Full Time, AWD = All Wheel Drive and 4WD-SEL = 4WD Selectable.
        413     20410-20419           PS_VEH_STD_DRV_TYPE_CD                Driveline: The standard location of the driven wheels. FWD = Front Wheel Drive, RWD = Rear Wheel Drive, 4WD-PT = 4WD Part Time, 4WD-FT = 4WD Full Time, AWD = All Wheel Drive and 4WD-SEL = 4WD Selectable.
        414     20421-20421           PS_VEH_THEFT_TRACKING_CD              Identifies if Vehicle Theft Tracking/Notification is available for the vehicle, i.e., A system based upon Global Positioning System(GPS), cell phone technology and/or in-vehicle radio signal transmitters that can assist authorities to track and pin-point
        415     20423-20427           PS_VEH_WHLBS_INCH                     Wheelbase: A measured length (in inches) from the center of the front wheel to the center of the rear wheel.
        416     20429-20432           PS_VEH_WID_INCH                       Width: The measured length of the widest point on the left side to the widest point on the right side of the vehicle body.
        417     20434-20434           PS_VIDEO_PLAYER_CD                    Identifies if a Video Player is available for the vehicle
        418     20436-20436           PS_VOICE_MAIL_CD                      Identifies if Voice Mail is available for the vehicle
        419     20438-20438           PS_WHL_LOCKS_CD                       Identifies if Wheel Locks are available for the vehicle, i.e., A special lug nut that serves to deter the theft of a wheel by requiring a specific "key" for installation and removal.
        420     20440-20440           PS_WINDSHIELD_WIPER_DEICER_CD         Identifies if a Windshield Wiper De-Icer is available for the vehicle, i.e., A heating element that prevents the windshield wipers from freezing to the windshield when they are not in use.
        421     20442-20451           PS_ENG_NM                             Engine Name: The engine displacement in liters, followed by the number of cylinders and the configuration of the cylinders.
        422     20453-20456           PS_BATRY_OUTPUT_CD                    Battery Output
        423     20458-20462           PS_CRUISING_RNG                       Cruising Range: A measurement of the number of miles a vehicle can drive on a full tank of gas. (Combined Fuel Economy X Fuel Capacity).
        424     20464-20468           PS_ELEC_HP_BHP                        A standard measurement of an electric engine ability to perform work over a given amount of time, measured in BHP (boiler horsepower unit)
        425     20470-20472           PS_ELEC_HP_RPM                        Hybrid Horsepower RPM
        426     20474-20476           PS_ELEC_TORQUE_FT_LBS                 Torque Feet Per Pound
        427     20478-20482           PS_ELEC_TORQUE_RPM                    RPM at Peak Torque
        428     20484-20493           PS_ENG_AVAIL_CD                       Defines if the engine is the standard engine or an optional engine
        429     20495-20499           PS_ENG_BORE_SIZE                      Bore: The diameter of the cylinder.
        430     20501-20504           PS_ENG_CARB_TYPE_CD                   Fuel System: The type of mechanisms that delivers fuel to the engine. Typical fuel systems are MPFI, EFI, SEFI, etc.
        431     20506-20509           PS_ENG_CMPRSN                         Compression Ratio: The ratio between an engine cylinder volume when the piston is at the bottom of its stroke and the volume when the piston is at the top of its stroke.
        432     20511-20513           PS_ENG_CYL_RTR_CNT                    Cylinders The internal openings of an engine block where the pistons travel and combustion occurs.
        433     20515-20519           PS_ENG_DSPLCMNT_CC                    Displacement CC: When the pistons of an internal combustion engine move, they displace a volume of air-fuel mixture. Generally, the larger the displacement, the larger the engine, the more air-fuel is displaced, and the result is greater power.
        434     20521-20525           PS_ENG_DSPLCMNT_CI                    Displacement CI: When the pistons of an internal combustion engine move, they displace a volume of air-fuel mixture. Generally, the larger the displacement, the larger the engine, the more air-fuel is displaced, and the result is greater power.
        435     20527-20536           PS_ENG_FUEL_DESC                      Fuel Type: The fuel source typically Gas, Diesel, Flexible Fuel (Ethanol), Compressed Natural Gas, Electric or Hybrid.
        436     20538-20543           PS_ENG_HEAD_CONFG_CD                  Valve Configuration: The arrangement of the camshaft(s) in relationship to their number and their location. Typical valve configurations are SOHC, DOHC, OHV, etc.
        437     20545-20549           PS_ENG_STROKE_SIZE                    Stroke: The maximum distance traveled by the head of the piston within the cylinder.
        438     20551-20552           PS_ENG_SUPERCHARGER_CD                Supercharged: An air pump that is mechanically driven by the engine and is designed to force more air into the engine in order to produce higher power. Designers often use Superchargers to produce power levels similar to larger engines while still retaini
        439     20554-20555           PS_ENG_TURBOCHARGER_CD                Turbocharged:  A centrifugal air pump driven by the engine¿s exhaust designed to force more air into the engine in order to produce higher power.
        440     20557-20559           PS_ENG_VALVES_TOT_CNT                 Valves: Trumpet-shaped valves open and close to control intake and exhaust of fuel and air to the cylinder chambers, where combustion occurs.
        441     20561-20565           PS_FNL_DRV_RATIO                      Final Drive Ratio: Measures the number of turns the driveshaft must make per turn of the axle shafts which drive the wheels.
        442     20567-20571           PS_FUEL_ECNY_CITY                     Fuel Economy City: The EPA fuel mileage of a vehicle when driven in city conditions.
        443     20573-20577           PS_FUEL_ECNY_CMBND                    Combined Fuel Economy: The EPA fuel mileage of a vehicle based on the average of both city and highway fuel economies.
        444     20579-20583           PS_FUEL_ECNY_HIGHWAY                  Fuel Economy Highway: The EPA fuel mileage of a vehicle when driven in highway conditions.
        445     20585-20587           PS_HP_BASE                            Horsepower: Horsepower is a measure of engine output, typically expressed as the amount of work needed to lift a 550 pound mass at a rate of one foot per second.
        446     20589-20593           PS_HP_RPM                             RPM at Peak Horsepower: The measurable point at which a given engine is producing its maximum horsepower measured in revolutions per minute.
        447     20595-20598           PS_MFG_ENG_CD                         Manufacturer Engine Code: The Manufacturer Code of the Engine (taken from price list or order guide).  Note: If the Price List or Order Guide does not include a Manufacturer Code, AIC may insert a code that is preceded by a pound sign (#).
        448     20600-20603           PS_MFG_TRANS_CD                       Manufacturer Transmission Code: The Manufacturer Code of the Transmission (taken from Price List or Order Guide). Note: If the Price List or Order Guide does not include a Manufacturer Code, AIC may insert a code that is preceded by a pound sign (#).
        449     20605-20614           PS_OPTL_FNL_DRV_GEAR_RATIO_CD         Final Drive Ratio (Optional): Measures the number of turns the driveshaft must make per turn of the axle shafts which drive the wheels.
        450     20616-20619           PS_PWR_TO_WT_RATIO                    Power To Weight Ratio: The ratio of the standard horsepower to the standard curb weight of a vehicle -- read as the number of pounds per 1 horsepower (Curb Weight / Horsepower).
        451     20621-20623           PS_TORQUE_FT_LBS                      Torque Feet Per Pound
        452     20625-20629           PS_TORQUE_RPM                         RPM at Peak Torque
        453     20631-20632           PS_TRANS_OVERDRIVE_CD                 Overdrive Transmission: A transmission that features a gear ratio that is less than 1:1 and typically provides for better fuel economy and reduces engine wear.
        454     20634-20653           PS_TRANS_SPEED_CD                     Transmission Speeds: The number of available forward speeds typically 3 through 6 or variable .
        455     20655-20656           PS_TRANS_TYPE_CD                      Transmission (Automatic/Manual): Transmissions are classified as either manual (M) or automatic (A), with or without an overdrive gear (OD) which allows the engine to turn at a slower rate than the drive axle and improves fuel efficiency.
        456     20658-20658           RAPA_PERFORMANCE_IND                  Rapa performance indicator
        457     20660-20661           RAPA_COLL_SYMBOL                      Rapa collision symbol
        458     20663-20664           RAPA_COMP_SYMBOL                      Rapa comprehensive symbol
        459     20666-20666           RAPA_ROLL_IND                         RAPA SYMBOL ROLLED
        460     20668-20670           LPM_PIP_MED_PAY_SYMBOL_2008           ISO three position PIP / Medical Symbol
        461     20672-20674           LPM_LIABILITY_SYMBOL_2008             ISO three position Liability Symbol
        462     20676-20676           LPM_ROLL_IND_2008                     It is common insurance industry practice to ¿roll over¿ insurance symbols from the previous model year into the new model year. An ¿R¿ is placed in this field to indicate that the ISO symbol for an existing series / model has been continued into the new model year and is not yet in the ISO country-wide book for the new model year. These symbols are phased out via normal software updates as the ISO symbols are updated and published.  This code is provided as a service to users, and use of the associated symbol is optional
        463     20678-20680           LPM_PIP_MED_PAY_SYMBOL_2010           ISO three position PIP / Medical Symbol
        464     20682-20684           LPM_LIABILITY_SYMBOL_2010             ISO three position Liability Symbol
        465     20686-20686           LPM_ROLL_IND_2010                     It is common insurance industry practice to ¿roll over¿ insurance symbols from the previous model year into the new model year. An ¿R¿ is placed in this field to indicate that the ISO symbol for an existing series / model has been continued into the new model year and is not yet in the ISO country-wide book for the new model year. These symbols are phased out via normal software updates as the ISO symbols are updated and published.  This code is provided as a service to users, and use of the associated symbol is optional
        */

        #endregion Data Layout Documentation

        /// <summary>
        /// The contructor for the VinDecoder class
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> object currently in use.</param>
        public PolkVinDecoder(Registry registry)
        {
            this.registry = registry;

            if (allPolkVehicleYMMEs == null)
            {
                this.LoadPolkVehicles();
            }
        }

        /// <summary>
        /// Gets the <see cref="PolkVehicleYMME"/> that was decoded from calling the DecodeVin method.
        /// </summary>
        public PolkVehicleYMME DecodedPolkVehicle
        {
            get
            {
                return this.decodedPolkVehicle;
            }
        }

        private void LoadPolkVehicles()
        {
            isBusy = true;

            SqlProcedureCommand command = new SqlProcedureCommand();
            command.ProcedureName = "PolkVehicleYMME_LoadAll";

            PolkVehicleYMMECollection polkVehicles = new PolkVehicleYMMECollection(this.registry);
            polkVehicles.Load(command, "PolkVehicleYMMEId", true, true, false);

            Hashtable temp_allPolkVehicleYMMEs = new Hashtable();

            foreach (PolkVehicleYMME v in polkVehicles)
            {
                if (!temp_allPolkVehicleYMMEs.ContainsKey(v.VinPatternMaskLowercase))
                {
                    temp_allPolkVehicleYMMEs[v.VinPatternMaskLowercase] = v;
                }
            }

            allPolkVehicleYMMEs = temp_allPolkVehicleYMMEs;

            isBusy = false;
        }

        private void EnsureIsReady()
        {
            if (isBusy)
            {
                throw new ApplicationException("The PolkVinDecoder is currently busy loading data from the database. Please try again later.");
            }
        }

        private Hashtable PolkVehicleYMMEs
        {
            get
            {
                this.EnsureIsReady();
                if (allPolkVehicleYMMEs == null)
                {
                    this.LoadPolkVehicles();
                }

                return allPolkVehicleYMMEs;
            }
        }

        private bool IsVinValid(string vin)
        {
            bool isValid = true;

            if (vin.Length == 17)
            {
                string vinCheckDigit = vin.Substring(8, 1).ToUpper();
                int vinCheckDigitValue = -1;

                if (vinCheckDigit == "X")
                {
                    vinCheckDigitValue = 10;
                }
                else
                {
                    Int32.TryParse(vinCheckDigit, out vinCheckDigitValue);
                }

                if (vinCheckDigitValue > -1)
                {
                    int checksum = GetVinCharacterChecksumValue(vin.Substring(0, 1), 1);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(1, 1), 2);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(2, 1), 3);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(3, 1), 4);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(4, 1), 5);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(5, 1), 6);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(6, 1), 7);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(7, 1), 8);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(9, 1), 10);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(10, 1), 11);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(11, 1), 12);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(12, 1), 13);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(13, 1), 14);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(14, 1), 15);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(15, 1), 16);
                    checksum += GetVinCharacterChecksumValue(vin.Substring(16, 1), 17);

                    int remainder = checksum % 11;

                    isValid = (vinCheckDigitValue == remainder);
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private int GetVinCharacterChecksumValue(string c, int position)
        {
            int characterValue = -1;
            int checksumValue = -1;

            Int32.TryParse(c, out characterValue);

            if (characterValue > -1)
            {
                switch (c.ToUpper())
                {
                    case "A":
                    case "J":
                        characterValue = 1;
                        break;

                    case "B":
                    case "K":
                    case "S":
                        characterValue = 2;
                        break;

                    case "C":
                    case "L":
                    case "T":
                        characterValue = 3;
                        break;

                    case "D":
                    case "M":
                    case "U":
                        characterValue = 4;
                        break;

                    case "E":
                    case "N":
                    case "V":
                        characterValue = 5;
                        break;

                    case "F":
                    case "W":
                        characterValue = 6;
                        break;

                    case "G":
                    case "P":
                    case "X":
                        characterValue = 7;
                        break;

                    case "H":
                    case "Y":
                        characterValue = 8;
                        break;

                    case "R":
                    case "Z":
                        characterValue = 9;
                        break;
                }
            }

            if (characterValue > -1)
            {
                switch (position)
                {
                    case 1:
                    case 11:
                        checksumValue = characterValue * 8;
                        break;

                    case 2:
                    case 12:
                        checksumValue = characterValue * 7;
                        break;

                    case 3:
                    case 13:
                        checksumValue = characterValue * 6;
                        break;

                    case 4:
                    case 14:
                        checksumValue = characterValue * 5;
                        break;

                    case 5:
                    case 15:
                        checksumValue = characterValue * 4;
                        break;

                    case 6:
                    case 16:
                        checksumValue = characterValue * 3;
                        break;

                    case 7:
                    case 17:
                        checksumValue = characterValue * 2;
                        break;

                    case 8:
                        checksumValue = characterValue * 10;
                        break;

                    case 10:
                        checksumValue = characterValue * 9;
                        break;
                }
            }

            return checksumValue;
        }

        /// <summary>
        /// Decodes a VIN
        /// </summary>
        /// <param name="vin">The <see cref="string"/> VIN to be decoded.</param>
        /// <returns>The <see cref="PolkVehicle"/> for the decoded VIN.</returns>
        public PolkVehicleYMME DecodeVIN(string vin)
        {
            return DecodeVIN(vin, true);
        }

        /// <summary>
        /// Decodes a VIN
        /// </summary>
        /// <param name="vin">The <see cref="string"/> VIN to be decoded.</param>
        /// <param name="validateVin">A <see cref="bool"/> indicating if the VIN should be valdated.</param>
        /// <returns>The <see cref="PolkVehicle"/> for the decoded VIN.</returns>
        public PolkVehicleYMME DecodeVIN(string vin, bool validateVin)
        {
            this.EnsureIsReady();
            List<string> vinsAttempted = new List<string>();

            //Added on 2017-06-16 8:50 AM by INNOVA Dev Team. Remove all the white space characters
            vin = vin.Replace(" ", string.Empty);

            object v = null;
            /*
			The following patterns will be matched where "A" represents the specific character from the VIN and "*" represents a wildcard.
			Brackets mean the characters are optional and need not be provided when decoding.

			4			AAAA[******]
			5			AAAAA[******]
			6			AAAAAA[******]
			7           AAA****				AAAAAAA[******]
			8           AAA*****			AAAAAAAA[******]
			10          AAAA******			AAAAAAAA*A[*******]
			11          AAAAA******			AAAAAAAA*A*[******]	AAAAAAAA*AA[******]
			12          AAAAA*******		AAAAAA******
			13          AAAAAAA******		AAAAAA*******
			14          AAAAAAAA******		AAAAAA********
			17          AAAAAAAA*AA******	AAAAAAAA*AAA*****	AAAAAAAA*AAAA****	AAAAAAAA*AAAAA***
			*/
            if (!validateVin || this.IsVinValid(vin))
            {
                string vinLowercase = vin.ToLower();

                switch (vin.Length)
                {
                    case 4:
                    case 5:
                    case 6:
                        v = this.PolkVehicleYMMEs[vinLowercase + "******"];
                        break;

                    case 7:
                        v = this.PolkVehicleYMMEs[vinLowercase + "******"];
                        if (v == null)
                        {
                            v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 3) + "****"];
                        }
                        break;

                    case 8:
                        v = this.PolkVehicleYMMEs[vinLowercase + "******"];
                        if (v == null)
                        {
                            v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 3) + "*****"];
                        }
                        break;

                    case 10:
                        v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 8) + "*" + vinLowercase.Substring(9, 1) + "******"];
                        if (v == null)
                        {
                            v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 4) + "******"];
                        }
                        break;

                    case 11:
                        v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 8) + "*" + vinLowercase.Substring(9, 2) + "******"];
                        if (v == null)
                        {
                            v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 8) + "*" + vinLowercase.Substring(9, 1) + "*******"];
                        }
                        if (v == null)
                        {
                            v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 5) + "******"];
                        }
                        break;

                    case 12:
                        v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 6) + "******"];
                        if (v == null)
                        {
                            v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 5) + "*******"];
                        }
                        break;

                    case 13:
                        v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 7) + "******"];
                        if (v == null)
                        {
                            v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 6) + "*******"];
                        }
                        break;

                    case 14:
                        v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 8) + "******"];
                        if (v == null)
                        {
                            v = this.PolkVehicleYMMEs[vinLowercase.Substring(0, 6) + "********"];
                        }
                        break;

                    case 17:
                        string firstEightCharacters = vinLowercase.Substring(0, 8);

                        // First try to find an exact VIN pattern mask match . . .
                        for (int i = 5; i > 0; i--)
                        {
                            string maskPadding = "".PadRight(8 - i, '*');
                            string vinPatternToFind = firstEightCharacters + "*" + vinLowercase.Substring(9, i) + maskPadding;
                            vinsAttempted.Add(vinPatternToFind);
                            v = this.PolkVehicleYMMEs[vinPatternToFind];
                            if (v != null)
                            {
                                break;
                            }
                        }

                        // If we still don't have a vehicle and got a masked VIN then try to find a vehicle by matching the first 14, 13, 12, 11, 10, or 9 characters of the Polk VIN pattern mask, in that order.
                        if (vinLowercase.Contains("*") && v == null)
                        {
                            for (int i = 5; i > 0; i--)
                            {
                                string regExPattern = "^" + firstEightCharacters + "\\*" + vinLowercase.Substring(9, i).Replace("*", "\\*");
                                Regex rx = new Regex(regExPattern);

                                string matchingKey = (from string key in this.PolkVehicleYMMEs.Keys where rx.IsMatch(key) orderby key descending select key).FirstOrDefault();

                                if (!String.IsNullOrEmpty(matchingKey))
                                {
                                    vinsAttempted.Add(matchingKey);
                                    v = this.PolkVehicleYMMEs[matchingKey];
                                    break;
                                }
                            }
                        }

                        break;
                }
            }

            //Added on 2017-06-15 1:10PM by INNOVA Dev Team to cover for a case when we still don't have a vehicle.
            if (v == null && !IsVinAValidMaskPattern(vin))
            {
                //Clear the object list
                vinsAttempted = new List<string>();

                var patternMasks = GenerateVinPatternMasks(vin);
                foreach (var pm in patternMasks)
                {
                    vinsAttempted.Add(pm);
                    v = this.PolkVehicleYMMEs[pm.ToLower()];

                    if (v != null)
                        break;
                }
            }

            if (v == null)
            {
                //Added on 2017-06-15 1:30PM by INNOVA Dev Team to get the VIN when it was unable to be added from all above processes.
                if (vinsAttempted.Count < 1)
                    vinsAttempted.Add(vin);

                throw new ApplicationException("The following VINs were tried and failed: " + String.Join(", ", vinsAttempted));
            }

            this.decodedPolkVehicle = (v != null) ? (PolkVehicleYMME)v : null;

            return this.DecodedPolkVehicle;
        }

        /// <summary>
        /// Gets a <see cref="bool"/> indicating if the supplied VIN is a valid pattern mask.
        /// </summary>
        /// <param name="vin">The <see cref="string"/> VIN to be checked.</param>
        /// <returns>A <see cref="bool"/> indicating if the supplied VIN is a valid pattern mask.</returns>
        public bool IsVinAValidMaskPattern(string vin)
        {
            /*
			The following patterns will be matched where "A" represents the specific character from the VIN and "*" represents a wildcard.
			Brackets mean the characters are optional and need not be provided when decoding.

			4			AAAA[******]
			5			AAAAA[******]
			6			AAAAAA[******]
			7           AAA****				AAAAAAA[******]
			8           AAA*****			AAAAAAAA[******]
			10          AAAA******			AAAAAAAA*A[*******]
			11          AAAAA******			AAAAAAAA*A*[******]	AAAAAAAA*AA[******]
			12          AAAAA*******		AAAAAA******
			13          AAAAAAA******		AAAAAA*******
			14          AAAAAAAA******		AAAAAA********
			17          AAAAAAAA*AA******	AAAAAAAA*AAA*****	AAAAAAAA*AAAA****	AAAAAAAA*AAAAA***
			*/

            bool isValidPattern = false;

            switch (vin.Length)
            {
                case 4:
                case 5:
                case 6:
                    if (vin.IndexOf("*") > 0)
                    {
                        isValidPattern = true;
                    }
                    break;

                case 7:
                    if (vin.IndexOf("*") > 0
                        || vin == vin.Substring(0, 3) + "****")
                    {
                        isValidPattern = true;
                    }
                    break;

                case 8:
                    if (vin.IndexOf("*") > 0
                        || vin == vin.Substring(0, 3) + "*****")
                    {
                        isValidPattern = true;
                    }
                    break;

                case 10:
                    if (vin == vin.Substring(0, 4) + "******"
                        || vin == vin.Substring(0, 8) + "*" + vin.Substring(9, 1) + "*******")
                    {
                        isValidPattern = true;
                    }
                    break;

                case 11:
                    if (vin == vin.Substring(0, 8) + "*" + vin.Substring(9, 2) + "******"
                        || vin == vin.Substring(0, 8) + "*" + vin.Substring(9, 1) + "*******"
                        || vin == vin.Substring(0, 5) + "******")
                    {
                        isValidPattern = true;
                    }
                    break;

                case 12:
                    if (vin == vin.Substring(0, 6) + "******"
                        || vin == vin.Substring(0, 5) + "*******")
                    {
                        isValidPattern = true;
                    }
                    break;

                case 13:
                    if (vin == vin.Substring(0, 7) + "******"
                        || vin == vin.Substring(0, 6) + "*******")
                    {
                        isValidPattern = true;
                    }
                    break;

                case 14:
                    if (vin == vin.Substring(0, 8) + "******"
                        || vin == vin.Substring(0, 6) + "********")
                    {
                        isValidPattern = true;
                    }
                    break;

                case 17:
                    string firstEightCharacters = vin.Substring(0, 8);
                    if (vin == firstEightCharacters + "*" + vin.Substring(9, 5) + "***"
                        || vin == firstEightCharacters + "*" + vin.Substring(9, 4) + "****"
                        || vin == firstEightCharacters + "*" + vin.Substring(9, 3) + "*****"
                        || vin == firstEightCharacters + "*" + vin.Substring(9, 2) + "******"
                        || vin == firstEightCharacters + "*" + vin.Substring(9, 1) + "*******"
                        || vin == firstEightCharacters + vin.Substring(8, 5) + "****"
                        || vin == firstEightCharacters + vin.Substring(8, 4) + "*****"
                        || vin == firstEightCharacters + vin.Substring(8, 3) + "******"
                        || vin == firstEightCharacters + vin.Substring(8, 2) + "*******")
                    {
                        isValidPattern = true;
                    }
                    break;
            }

            return isValidPattern;
        }

        /// <summary>
		/// Generate a list of pattern masks for a VIN.
		/// </summary>
		/// <param name="vin">The <see cref="string"/> VIN to be used to generate a list of pattern masks.</param>
        /// <returns></returns>
        public List<string> GenerateVinPatternMasks(string vin)
        {
            List<string> patternMaskList = new List<string>();

            switch (vin.Length)
            {
                case 7:
                    patternMaskList.Add(vin.Substring(0, 3) + "****");
                    break;

                case 8:
                    patternMaskList.Add(vin.Substring(0, 3) + "*****");
                    break;

                case 10:
                    patternMaskList.Add(vin.Substring(0, 4) + "******");
                    patternMaskList.Add(vin.Substring(0, 8) + "*" + vin.Substring(9, 1) + "*******");
                    break;

                case 11:
                    patternMaskList.Add(vin.Substring(0, 8) + "*" + vin.Substring(9, 2) + "******");
                    patternMaskList.Add(vin.Substring(0, 8) + "*" + vin.Substring(9, 1) + "*******");
                    patternMaskList.Add(vin.Substring(0, 5) + "******");
                    break;

                case 12:
                    patternMaskList.Add(vin.Substring(0, 6) + "******");
                    patternMaskList.Add(vin.Substring(0, 5) + "*******");
                    break;

                case 13:
                    patternMaskList.Add(vin.Substring(0, 7) + "******");
                    patternMaskList.Add(vin.Substring(0, 6) + "*******");
                    break;

                case 14:
                    patternMaskList.Add(vin.Substring(0, 8) + "******");
                    patternMaskList.Add(vin.Substring(0, 6) + "********");
                    break;

                case 17:
                    string firstEightCharacters = vin.Substring(0, 8);
                    patternMaskList.Add(firstEightCharacters + "*" + vin.Substring(9, 5) + "***");
                    patternMaskList.Add(firstEightCharacters + "*" + vin.Substring(9, 4) + "****");
                    patternMaskList.Add(firstEightCharacters + "*" + vin.Substring(9, 3) + "*****");
                    patternMaskList.Add(firstEightCharacters + "*" + vin.Substring(9, 2) + "******");
                    patternMaskList.Add(firstEightCharacters + "*" + vin.Substring(9, 1) + "*******");
                    break;
            }

            return patternMaskList;
        }
    }
}