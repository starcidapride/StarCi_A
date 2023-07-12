public class Constants
{
    public class Apis
    {
        public static readonly string SERVER_URL = "https://starci-auth-server.cyclic.app/";

        public class Authentication
        {
            public static readonly string AUTHENTICATION = "api/auth/";

            public static readonly string INIT_API = SERVER_URL + AUTHENTICATION + "init";

            public static readonly string REFRESH_API = SERVER_URL + AUTHENTICATION + "refresh";

            public static readonly string SIGN_IN_API = SERVER_URL + AUTHENTICATION + "sign-in";

            public static readonly string SIGN_UP_API = SERVER_URL + AUTHENTICATION + "sign-up";
        }

        public class Profile
        {
            public static readonly string PROFILE = "api/profile/";

            public static readonly string SETUP_PROFILE_API = SERVER_URL + PROFILE + "setup-profile";

        }
        
    }

    public class ButtonNames
    {
        public static readonly string QUIT = "Quit";

        public static readonly string RECONNECT = "Reconnect";

        public static readonly string CANCEL = "Cancel";

        public static readonly string RESENT = "Resent";
    }

    public class CardNames
    {
        public class Characters
        {
            public static readonly string ARTHUR = "Arthur";

            public static readonly string BALDUM = "Baldum";

            public static readonly string CRESHT = "Cresht";

            public static readonly string PEURA = "Peura";

            public static readonly string TEL_ANNAS = "Tel'Annas";

        }

        public class Equipments
        {
            public static readonly string FAFNIRS_TALON = "Fafnir's Talon";

            public static readonly string SHIELD_OF_THE_LOST = "Shield Of The Lost";
        }

        public class Spells
        {
            public static readonly string BARRIER = "Barrier";

            public static readonly string CLEANSE = "Cleanse";

            public static readonly string HEAL = "Heal";
        }

        public class Others
        {
            public static readonly string INVOCATION = "Invocation";
        }
    }

    public class CardImages
    {
        public static readonly string CARDS_LOCATION = "Images/Cards/";

        public class Characters
        {
            public static readonly string CHARACTER_CARDS_LOCATION = CARDS_LOCATION + "Characters/";

            public static readonly string ARTHUR_IMAGE = CHARACTER_CARDS_LOCATION + "Arthur/";

            public static readonly string BALDUM_IMAGE = CHARACTER_CARDS_LOCATION + "Baldum/";

            public static readonly string CRESHT_IMAGE = CHARACTER_CARDS_LOCATION + "Cresht/";

            public static readonly string PEURA_IMAGE = CHARACTER_CARDS_LOCATION + "Peura/";

            public static readonly string TEL_ANNAS_IMAGE = CHARACTER_CARDS_LOCATION + "Tel'Annas/";
        }

        public class Equipments
        {
            public static readonly string EQUIPMENT_CARDS_LOCATION = CARDS_LOCATION + "Equipments/";

            public static readonly string FAFNIRS_TALON_IMAGE = EQUIPMENT_CARDS_LOCATION + "Fafnir's Talon";

            public static readonly string SHIELD_OF_THE_LOST_IMAGE = EQUIPMENT_CARDS_LOCATION + "Shield Of The Lost";
        }

        public class Spells
        {
            public static readonly string SPELL_CARDS_LOCATION = CARDS_LOCATION + "Spells/";

            public static readonly string BARRIER_IMAGE = SPELL_CARDS_LOCATION + "Barrier";

            public static readonly string CLEANSE_IMAGE = SPELL_CARDS_LOCATION + "Cleanse";

            public static readonly string HEAL_IMAGE = SPELL_CARDS_LOCATION + "Heal";
        }

        public class Others
        {
            public static readonly string OTHER_CARDS_LOCATION = CARDS_LOCATION + "Others/";

            public static readonly string INVOCATION_IMAGE = OTHER_CARDS_LOCATION + "Invocation";
        }
    }
}