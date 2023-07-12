using UnityEngine;
using static Constants.CardImages.Spells;
public class Barrier : ISpellCard
{
    public Texture2D Image { get; } = Resources.Load<Texture2D>(BARRIER_IMAGE);

    public string Description { get; } = "Barrier";

}