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
}