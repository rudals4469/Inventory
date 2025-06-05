using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject equipMark;

    public void SetItem(Item item, bool isEquipped = false)
    {
        itemIcon.sprite = null;
        itemIcon.sprite = item.Icon;
        itemIcon.enabled = item.Icon != null;

        equipMark.SetActive(isEquipped);
    }
    
}