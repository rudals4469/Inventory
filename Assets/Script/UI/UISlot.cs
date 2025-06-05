using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject equipMark;

    private Item _item;
    private Character _character;

    private float _lastClickTime;
    private const float DoubleClickThreshold = 0.25f;

    public void SetItem(Item item, bool isEquipped = false)
    {
        _item = item;
        
        if (_character == null)
            _character = GameManager.Instance.Player;

        itemIcon.sprite = item?.Icon;
        itemIcon.enabled = item?.Icon != null;
        equipMark.SetActive(isEquipped);
    }

    public void Clear()
    {
        _item = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
        equipMark.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_item == null) return;

        float timeSinceLastClick = Time.time - _lastClickTime;

        if (timeSinceLastClick <= DoubleClickThreshold)
        {
            ToggleEquip();
        }

        _lastClickTime = Time.time;
    }

    private void ToggleEquip()
    {
        if (_item == null || _character == null)
        {
            return;
        }

        if (_item.IsEquipped)
        {
            _item.UnEquip();
            _character.Unequip(_item.Data.slot);
        }
        else
        {

            if (_character.EquippedItems.TryGetValue(_item.Data.slot, out Item equipped))
            {
                equipped.UnEquip();
            }

            _character.EquipItem(_item);
        }

        UIManager.Instance.InitAllUI(_character);
        UIManager.Instance.OpenInventory();
    }
}