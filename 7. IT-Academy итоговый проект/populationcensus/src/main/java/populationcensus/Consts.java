package populationcensus;

public class Consts {

    public static class Url{
        public static final String ADMIN_MAIN = "adminMain";
        public static final String $_ADMIN_MAIN_$_USERS_LIST = "/adminMain/usersList";
        public static final String $_ADMIN_MAIN_$_USERS_LIST_$_CREATE_USER = "/adminMain/usersList/createUser";
        public static final String $_ADMIN_MAIN_$_PERSONS_LIST = "/adminMain/personsList";
        public static final String $_CREATE_USER = "/createUser";
        public static final String $_MAIN = "/main";
        public static final String $_MAIN_$_SURVEY_PERSON = "/main/surveyPerson";
        public static final String MAIN_$_SURVEY_HOUSEHOLD = "main/surveyHousehold";
        public static final String $_MAIN_$_SURVEY_FOREIGN_PERSON = "/main/surveyForeignPerson";
        public static final String $_MAIN_$_SURVEY_FINISH = "/main/surveyFinish";
        public static final String MAIN_$_MY_BLANK = "main/myBlank";
        public static final String $_INTERRUPT = "/interrupt";
        public static final String $_HOUSEHOLD_NEXT = "/householdNext";
        public static final String $_PERSON_NEXT = "/personNext";


        public static final String $_MAIN_$_MY_BLANK_FAIL = "/main/myBlankFail";
        public static final String MAIN_$_MY_BLANK_FAIL = "/main/myBlankFail";
        public static final String $_MAIN_$_MY_BLANK_$_HOUSEHOLD = "/main/myBlank/household";
        public static final String $_SHOW_HOUSEHOLD = "/showHousehold";
        public static final String $_SHOW_PERSON = "/showPerson";
        public static final String $_BACK_TO_PAGE = "/backToPage";
        public static final String $_FOREIGN_PERSON = "/foreignPerson";
        public static final String $_FINISH = "/finish";
        public static final String $_SURVEY_HOUSEHOLD = "/surveyHousehold";
        public static final String $_MY_BLANK = "/myBlank";
        public static final String $_AUTHORISATION = "/authorization";
    }
    public static class Test{
        public static final long HOUSEHOLD_ID = 1L;
        public static final int HOUSEHOLD_ROOMS_COUNT = 3;
        public static final int HOUSEHOLD_NUMBER_OF_MEMBERS = 3;

        public static final long ACCOMMODATIONS_INFO_ID = 1L;
        public static final int ACCOMMODATIONS_INFO_AREA_OF_FLAT = 228;

        public static final long FULL_ADDRESS_INFO_ID = 1L;
        public static final String FULL_ADDRESS_INFO_VILLAGE_NAME = "Village";

        public static final long PERSON_ID = 1L;
        public static final int PERSON_AGE = 54;
        public static final String PERSON_NAME = "Person";

        public static final long EDUCATION_INFO_ID = 1L;
        public static final int EDUCATION_INFO_LVL_OF_EDUCATION = 7;

        public static final long LIVING_COUNTRY_INFO_ID = 1L;
        public static final int LIVING_COUNTRY_INFO_INFO_ABOUT_LEAVING_BELARUS = 2;

        public static final long LIVING_PLACE_INFO_ID = 1L;
        public static final boolean LIVING_PLACE_INFO_IS_IT_VILLAGE = true;

        public static final long WORK_INFO_ID = 1L;
        public static final int WORK_INFO_TYPE_OF_WORKPLACE = 4;
    }
}
