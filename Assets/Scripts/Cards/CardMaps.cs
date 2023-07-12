using System;
using System.Collections.Generic;

using static Constants.CardNames.Characters;
using static Constants.CardNames.Equipments;
using static Constants.CardNames.Spells;
using static Constants.CardNames.Others;

public class CardMaps
{
    public static Dictionary<string, Type> GetCharactersMap()
    {
        return new Dictionary<string, Type>()
        {
            { ARTHUR, typeof(Arthur) },

            { BALDUM, typeof(Baldum) },

            { CRESHT, typeof(Cresht) },

            { PEURA, typeof(Peura) },

            { TEL_ANNAS, typeof(TelAnnas) },
        };
    }

    public static Dictionary<string, Type> GetEquipmentMap()
    {
        return new Dictionary<string, Type>()
        {
            { FAFNIRS_TALON, typeof(FafnirsTalon) },

            { SHIELD_OF_THE_LOST, typeof(ShieldOfTheLost) },
        };
    }

    public static Dictionary<string, Type> GetSpellMap()
    {
        return new Dictionary<string, Type>()
        {

            { BARRIER, typeof(Barrier) },

            { CLEANSE, typeof(Cleanse) },

            { HEAL, typeof(Heal) }
        };
    }

    public static Dictionary<string, Type> GetOtherMap()
    {
        return new Dictionary<string, Type>()
        {
            { INVOCATION, typeof(Invocation) },
        };
    }


    public static Dictionary<string, (CardType, Type)> GetMap()
    {
        var cardMap = new Dictionary<string, (CardType, Type)>();

        foreach (var champion in GetCharactersMap())
        {
            cardMap.Add(champion.Key, (CardType.Character, champion.Value));
        }

        foreach (var equipment in GetEquipmentMap())
        {
            cardMap.Add(equipment.Key, (CardType.Equipment, equipment.Value));
        }


        foreach (var spell in GetSpellMap())
        {
            cardMap.Add(spell.Key, (CardType.Spell, spell.Value));
        }

        foreach (var other in GetOtherMap())
        {
            cardMap.Add(other.Key, (CardType.Other, other.Value));
        }

        return cardMap;
    }



}
