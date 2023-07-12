using System.ComponentModel;

public enum EquipmentClass
{
    Attack = 1,
    Magic = 2,
    Defense = 3
}

public interface IEquipmentCard : ICard
{   
    public EquipmentClass Class { get; }
    public int Price { get; }
    public string Description { get; }
}