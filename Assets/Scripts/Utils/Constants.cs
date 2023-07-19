using UnityEditor;
using UnityEngine;
using static Constants.Apis;

public class Constants
{
    public class Transforms
    {
        public static readonly string UI = "UI";
    }
    
    public class Colors
    {
        public static readonly Color GRAY = new (0.5f, 0.5f, 0.5f);
    }
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

            public static readonly string GET_PROFILE_API = SERVER_URL + PROFILE + "get-profile";

        }

        public class Deck
        {
            public static readonly string DECK = "api/deck/";

            public static readonly string ADD_DECK = SERVER_URL + DECK + "add-deck";

            public static readonly string SAVE_DECK = SERVER_URL + DECK + "save-deck";

            public static readonly string DEFAULT_DECK = SERVER_URL + DECK + "default-deck";

            public static readonly string RENAME_DECK = SERVER_URL + DECK + "rename-deck";

            public static readonly string DELETE_DECK = SERVER_URL + DECK + "delete-deck";
        }
    }

    public class Prefabs
    {
        public static readonly string PREFABS_PATH = "Prefabs/";

        public class Cards
        {
            public static readonly string CARDS_PATH = PREFABS_PATH + "Cards/";

            public static readonly string CHARACTER_CARD = CARDS_PATH + "Character Card";

            public static readonly string EQUIPMENT_CARD = CARDS_PATH + "Equipment Card";

            public static readonly string SPELL_CARD = CARDS_PATH + "Spell Card";

            public static readonly string OTHER_CARD = CARDS_PATH + "Other Card";
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

        public class Frames
        {
            public static readonly string CARD_FRAMES_LOCATION = CARDS_LOCATION + "Frames/";

            public static readonly string INVOCATION_FRAME = CARD_FRAMES_LOCATION + "Invocation Card";
    }

    }

    public class Triggers
    {
        public class Card
        {
            public static readonly string CARD_INSTANTIATION_TRIGGER = "Card Instantiation Trigger";
        }

        public class Modal
        {
            public class SetupProfileModal
            {
                public static readonly string SETUP_PROFILE_MODAL_TRANSITION_TRIGGER = "Setup Profile Modal Transition Trigger";
            }

        }
    }

    public class AbilityNames
    {
        public static readonly string PASSIVE = "Passive";

        public static readonly string Q = "Q";

        public static readonly string E = "E";

        public static readonly string R = "R";

    }

    public class DeckLimits 
    {
        public static int MAX_PLAY_CARDS = 40;

        public static int MAX_CHARACTER_CARDS = 10;

        public static int MAX_PLAY_OCCURRENCES = 3;

        public static int MAX_INVOCATION_OCCURRENCES = 6;

        public static int MAX_CHARACTER_OCCURRENCES = 1;
    }

    public class LobbyService
    {
        public static readonly string RELAY_CODE = "relayCode";

        public static readonly string HOST = "host";

        public static readonly string USERNAME = "username";

        public static readonly string DESCRIPTION = "description";

        public static readonly string STATUS = "status";

        public static readonly string RELAY_ALLOCATION_ID = "relayAllocationId";
    }
}