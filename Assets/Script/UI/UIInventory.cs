using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Button backButton;

    private List<UISlot> _slotList = new List<UISlot>();

    private int _maxSlotCount = Character.MaxInventorySize;

    public void SetData(Character character)
    {
        if (_slotList.Count == 0)
        {
            for (int i = 0; i < _maxSlotCount; i++)
            {
                GameObject go = Instantiate(slotPrefab, slotParent);
                UISlot slot = go.GetComponent<UISlot>();
                slot.SetIndex(i);
                _slotList.Add(slot);
            }
        }

        for (int i = 0; i < _slotList.Count; i++)
        {
            if (i < character.Inventory.Count && character.Inventory[i] != null)
            {
                _slotList[i].SetItem(character.Inventory[i], character.Inventory[i].IsEquipped);
            }
            else
            {
                _slotList[i].Clear();
            }
        }

        titleText.text = $"Inventory {character.Inventory.Count} / {_maxSlotCount}";
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenMainMenu();
        });
    }
}