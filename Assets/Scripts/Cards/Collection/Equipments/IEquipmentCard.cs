using System.ComponentModel;

public enum EquipmentClass
{
    [Description("Attack")]
    Attack = 1,

    [Description("Magic")]
    Magic = 2,

    [Description("Defense")]
    Defense = 3
}

public interface IEquipmentCard : ICard
{   
    public EquipmentClass EquipmentClass { get; }
    public int Price { get; }

    public string Stats { get; }
    public string Description { get; }
}