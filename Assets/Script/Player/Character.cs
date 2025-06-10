using System.Collections.Generic;

public class Character
{
    public static readonly int MaxInventorySize = 120;
    public string ID { get; private set; }
    public int Level { get; private set; }
    public int CurrentHP { get; private set; }
    public int MaxHP { get; private set; }
    public string Description { get; private set; }

    public int BaseAttack { get; private set; }
    public int BaseDefense { get; private set; }
    public int BaseHP { get; private set; }
    public int BaseCrit { get; private set; }

    public int CurrentExp { get; private set; }
    public int MaxExp { get; private set; }

    public List<Item> Inventory { get; private set; } = new();
    public Dictionary<EquipSlot, Item> EquippedItems { get; private set; } = new();
    
    public int CurrentInventoryCount
    {
        get
        {
            int count = 0;
            foreach (var item in Inventory)
                if (item != null)
                    count++;
            return count;
        }
    }
    
    
    // public Character(string id, int level, int curHp, int maxHp, string desc, int atk, int def, int hp, int critRate, int curExp, int maxExp)
    // {
    //     ID = id;
    //     Level = level;
    //     CurrentHP = curHp;
    //     MaxHP = maxHp;
    //     Description = desc;
    //
    //     BaseAttack = atk;
    //     BaseDefense = def;
    //     BaseHP = hp;
    //     BaseCrit = critRate;
    //
    //     CurrentExp = curExp;
    //     MaxExp = maxExp;
    // }
    public Character(CharacterData data)
    {
        ID = data.characterId;
        Level = data.level;
        CurrentHP = data.currentHP;
        MaxHP = data.maxHP;
        Description = data.description;

        BaseAttack = data.attack;
        BaseDefense = data.defense;
        BaseHP = data.hp;
        BaseCrit = data.critRate;

        CurrentExp = data.currentExp;
        MaxExp = data.maxExp;

        Inventory = new List<Item>(new Item[MaxInventorySize]);
        EquippedItems = new Dictionary<EquipSlot, Item>();

        if (data.inventoryItems != null)
        {
            for (int i = 0; i < data.inventoryItems.Length && i < MaxInventorySize; i++)
            {
                if (data.inventoryItems[i] != null)
                    Inventory[i] = new Item(data.inventoryItems[i]);
            }
        }
    }

    public void EquipItem(Item item)
    {
        if (item == null || item.Data == null) return;

        item.Equip();
        EquippedItems[item.Data.slot] = item;
    }

    public void Unequip(EquipSlot slot)
    {
        if (EquippedItems.TryGetValue(slot, out var equippedItem))
            equippedItem.UnEquip();
        
        EquippedItems.Remove(slot);
    }

    public int GetTotalAttack()
    {
        int value = BaseAttack;
        foreach (var pair in EquippedItems)
            value += pair.Value.Data.bonusAtk;
        return value;
    }

    public int GetTotalDefense()
    {
        int value = BaseDefense;
        foreach (var pair in EquippedItems)
            value += pair.Value.Data.bonusDef;
        return value;
    }

    public int GetTotalHP()
    {
        int value = BaseHP;
        foreach (var pair in EquippedItems)
            value += pair.Value.Data.bonusHP;
        return value;
    }

    public int GetTotalCrit()
    {
        int value = BaseCrit;
        foreach (var pair in EquippedItems)
            value += pair.Value.Data.bonusCrit;
        return value;
    }
}
