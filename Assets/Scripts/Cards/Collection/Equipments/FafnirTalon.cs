using UnityEngine;
using static IEquipmentCard;
using static Constants.CardImages.Equipments;
public class FafnirsTalon : IEquipmentCard
{
    public Texture2D Image { get; } = Resources.Load<Texture2D>(FAFNIRS_TALON_IMAGE);
    public EquipmentClass Class { get; } = EquipmentClass.Attack;

    public int Price { get; } = 1000;

    public string Description { get; } = "Kiếm đẹp lắm";
}