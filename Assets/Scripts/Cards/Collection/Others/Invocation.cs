using UnityEngine;

using static Constants.CardImages.Others;

public class Invocation : IOtherCard
{
    public Texture2D Image { get; } = Resources.Load<Texture2D>(INVOCATION_IMAGE);

    public string Description { get; } = "Invocation";

}