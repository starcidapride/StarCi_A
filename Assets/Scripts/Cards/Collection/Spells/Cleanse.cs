using UnityEngine;
using static Constants.CardImages.Spells;
public class Cleanse : ISpellCard
{
    public Texture2D Image { get; } = Resources.Load<Texture2D>(CLEANSE_IMAGE);

    public string Description { get; } = "Cleanse";

}