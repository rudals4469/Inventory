using UnityEngine;

public class Item
{
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public bool IsEquipped { get; private set; }

    public Item(string name, Sprite icon)
    {
        Name = name;
        Icon = icon;
        IsEquipped = false;
    }

    public void Equip()
    {
        IsEquipped = true;
    }

    public void UnEquip()
    {
        IsEquipped = false;
    }
}
