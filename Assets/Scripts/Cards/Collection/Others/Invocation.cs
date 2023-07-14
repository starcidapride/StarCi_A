using UnityEngine;

using static Constants.CardImages.Others;
using static Constants.CardImages.Frames;

public class Invocation : IOtherCard
{
    public Texture2D Frame { get; } = Resources.Load<Texture2D>(INVOCATION_FRAME);
    public Texture2D Image { get; } = Resources.Load<Texture2D>(INVOCATION_IMAGE);
    public string Description { get; } = "Invocation";

}