using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public EquipSlot slot;

    public int bonusAtk;
    public int bonusDef;
    public int bonusHP;
    public int bonusCrit;
}