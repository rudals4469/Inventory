using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Button backButton;

    public void SetData(Character character)
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        foreach (var item in character.Inventory)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            UISlot slot = go.GetComponent<UISlot>();
            slot.SetItem(item, false);
        }

        titleText.text = $"Inventory {character.Inventory.Count} / 120";
    }


    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenMainMenu();
        });
    }
}