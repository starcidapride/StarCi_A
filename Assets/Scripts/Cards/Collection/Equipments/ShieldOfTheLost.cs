using UnityEngine;
using static IEquipmentCard;
using static Constants.CardImages.Equipments;
public class ShieldOfTheLost : IEquipmentCard
{
    public Texture2D Image { get; } = Resources.Load<Texture2D>(SHIELD_OF_THE_LOST_IMAGE);
    public EquipmentClass Class { get; } = EquipmentClass.Defense;

    public int Price { get; } = 1000;

    public string Description { get; } = "Kiếm đẹp lắm";
}