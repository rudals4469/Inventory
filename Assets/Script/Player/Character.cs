using System.Collections.Generic;

public class Character
{
    public string ID { get; private set; }
    public int Level { get; private set; }
    public int CurrentHP { get; private set; }
    public int MaxHP { get; private set; }
    public string Description { get; private set; }

    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int HP { get; private set; }
    public int CriticalRate { get; private set; }
    
    public int CurrentExp { get; private set; }
    public int MaxExp { get; private set; }
    public List<Item> Inventory { get; private set; }

    public Character(string id, int level, int curHp, int maxHp, string desc, int atk, int def, int hp, int critRate, int curExp, int maxExp)
    {
        ID = id;
        Level = level;
        CurrentHP = curHp;
        MaxHP = maxHp;
        Description = desc;

        Attack = atk;
        Defense = def;
        HP = hp;
        CriticalRate = critRate;
        
        CurrentExp = curExp;
        MaxExp = maxExp;

        Inventory = new List<Item>();
    }
}