using UnityEngine;
using static ICharacterCard;
using static Constants.CardImages.Characters;

public class Baldum : ICharacterCard
{
    public Texture2D Image { get; } = Resources.Load<Texture2D>(BALDUM_IMAGE + "Image");
    public int Experience { get; } = 1;

    public CharacterRole Role { get; } = CharacterRole.Tank;

    public int MaxHealth { get; } = 1;

    public int AttackDamage { get; } = 1;

    public int Armor { get; } = 1;

    public int MagicResistance { get; } = 1;

    public string PassiveName { get; } = "The Morning Star";

    public Texture2D PassiveImage { get; } = Resources.Load<Texture2D>(BALDUM_IMAGE + "Passive");

    public string PassiveDescription { get; } = "TC";

    public string QName { get; } = "Eagle Eye";

    public Texture2D QImage { get; } = Resources.Load<Texture2D>(BALDUM_IMAGE + "Q");

    public string QDescription { get; } = "ASTT";

    public string EName { get; } = "Penetrating Shot";

    public Texture2D EImage { get; } = Resources.Load<Texture2D>(BALDUM_IMAGE + "E");

    public string EDescription { get; } = "CMP";

    public string RName { get; } = "Arrow Of Chaos";

    public Texture2D RImage { get; } = Resources.Load<Texture2D>(BALDUM_IMAGE + "R");

    public string RDescription { get; } = "KGSM";
}