using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemIcon; // 아이템 이미지
    [SerializeField] private GameObject equipMark; // 장착 마크

    private int _index; // 인벤토리 내 인덱스
    private Item _item; // 슬롯에 들어있는 아이탬
    private Character _character; 

    private float _lastClickTime; // 마지막 클릭 시간 저장
    private const float DoubleClickThreshold = 0.25f; // 더블클릭 인식 간격

    // 외부에서 인덱스나 아이탬을 읽을 수 있게 프로퍼티 제공
    public int Index => _index;
    public Item Item => _item;

    private void Awake()
    {
        if (_character == null)
            _character = GameManager.Instance.Player;
    }

    // 인벤토리에서 슬롯을 초기화 할 때 인덱스 지정
    public void SetIndex(int index)
    {
        _index = index;
    }

    // 슬롯에 아이템을 설정하고, 장착 여부를 반영하여 UI 업데이트
    public void SetItem(Item item, bool isEquipped = false)
    {
        _item = item;
        
        itemIcon.sprite = item?.Icon;
        itemIcon.enabled = item?.Icon != null;
        
        equipMark.SetActive(isEquipped);
    }

    // 슬롯을 비우고 UI 초기화
    public void Clear()
    {
        _item = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
        equipMark.SetActive(false);
    }

    // 마우스 클릭 시 실행되는 메서드
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_item == null) 
            return;

        // 마지막 클릭과의 시간차 계산
        float timeSinceLastClick = Time.time - _lastClickTime;
        // 더블 클릭으로 간주되면 장착/해제 토글
        if (timeSinceLastClick <= DoubleClickThreshold)
        {
            ToggleEquip();
        }

        // 클릭한 시간 저장 (다음 클릭과 비교하기 위함)
        _lastClickTime = Time.time;
    }

    // 장착/해제를 처리하는 내부 메서드
    private void ToggleEquip()
    {
        if (_item == null || _character == null)
            return;

        //해제
        if (_item.IsEquipped)
        {
            _item.UnEquip();
            _character.Unequip(_item.Data.slot);
        }
        // 장착
        else
        {
            // 같은 부위면 끼고 있던 아이템 해제
            if (_character.EquippedItems.TryGetValue(_item.Data.slot, out Item equipped))
            {
                equipped.UnEquip();
            }

            _character.EquipItem(_item);
        }
        
        // UI 전체 갱신 후 인벤토리 창 다시 열어주기 위함
        UIManager.Instance.InitAllUI(_character);
        UIManager.Instance.OpenInventory();
    }
}