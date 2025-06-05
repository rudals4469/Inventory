using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character", fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterId;
    public string description;

    public int level;
    public int currentHP;
    public int maxHP;

    public int attack;
    public int defense;
    public int hp;
    public int critRate;
    public int currentExp;
    public int maxExp;

    public ItemData[] InventoryItems;
}