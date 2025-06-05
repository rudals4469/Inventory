using Unity.VisualScripting;
using UnityEngine;
public class Item
{
    public ItemData Data { get; private set; }
    public bool IsEquipped { get; private set; }

    public string Name => Data.itemName;
    public Sprite Icon => Data.icon;
    public int BonusAttack => Data.bonusAtk;
    public int BonusDefense => Data.bonusDef;
    public int BonusHP => Data.bonusHP;
    public int BonusCritRate => Data.bonusCrit;

    public Item(ItemData data)
    {
        Data = data;
    }

    public void Equip() => IsEquipped = true;
    public void UnEquip() => IsEquipped = false;
}