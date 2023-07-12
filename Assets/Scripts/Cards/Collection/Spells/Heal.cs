using UnityEngine;

using static Constants.CardImages.Spells;
public class Heal : ISpellCard
{
    public Texture2D Image { get; } = Resources.Load<Texture2D>(HEAL_IMAGE);

    public string Description { get; } = "Heal";

}