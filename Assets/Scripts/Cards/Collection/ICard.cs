using UnityEngine;

public enum CardType
{
    Character = 1,

    Equipment = 2,

    Spell = 3,

    Other = 4
}
public interface ICard
{
    public Texture2D Image { get; }
}