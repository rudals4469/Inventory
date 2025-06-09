using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UISlot))]
public class ItemDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    /*
    인터페이스	        이벤트 발생 시점	                                    호출 대상
    IBeginDragHandler	마우스(또는 터치) 버튼을 누른 상태로 드래그를 시작할 때	    드래그 시작한 오브젝트
    IDragHandler	    드래그가 계속되는 동안 매 프레임마다	                    드래그 중인 오브젝트
    IEndDragHandler	    마우스를 놓거나 터치가 끝날 때	                        드래그 중이던 오브젝트
    IDropHandler	    드래그 중이던 오브젝트가 이 오브젝트 위에 놓였을 때	        드롭된 대상 (즉, 드래그 대상이 아닌 받는 쪽 오브젝트)
     */
    
    private RectTransform _rectTransform; // 위치 조절 용
    private CanvasGroup _canvasGroup; // 상호작용 막기 용
    private Canvas _canvas; // UI 위치 잡기 용
    private Transform _originalParent; // 부모 객체 저장
    private GameObject _placeholder; // 자리 유지용 임시 오브젝트
    private UISlot _slot; // 슬롯 참조

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
        _slot = GetComponent<UISlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 빈 슬롯은 드래그 x
        if (_slot.Item == null)
        {
            eventData.pointerDrag = null; // 드래그 자체를 안함
            return;
        }

        // 현재 부모 저장
        _originalParent = transform.parent;

        // 자리 유지용 임시 오브젝트 생성
        _placeholder = new GameObject("SlotPlaceholder");
        var rect = _placeholder.AddComponent<RectTransform>();
        rect.SetParent(_originalParent);
        rect.SetSiblingIndex(_slot.Index); // 현재 클릭 된 자리로 위치 고정
        rect.sizeDelta = _rectTransform.sizeDelta; // 사이즈로 같게 생성

        transform.SetParent(_canvas.transform); // 부모를 canvas로 옮겨서 자유롭게 움직이게 함
        _canvasGroup.blocksRaycasts = false; // 다른 슬롯이 드랍을 감지 할 수 있도록 설정
    }

    // 마우스 따라다니게 하기
    public void OnDrag(PointerEventData eventData)
    {
        if (_slot.Item == null) return;

        // 마우스 움직이 만큼 위치 갱신
        _rectTransform.anchoredPosition += eventData.delta / transform.lossyScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 정상적으로 드래그가 완료 되었을 때
        if (_placeholder != null)
        {
            transform.SetParent(_originalParent); // 원래 부모로 복귀
            transform.SetSiblingIndex(_placeholder.transform.GetSiblingIndex()); // 정확한 위치 복귀
            Destroy(_placeholder); // 임시 오브젝트 삭제
            _placeholder = null;
        }
        else
        {
            transform.SetParent(_originalParent); // 드래그 자체를 무효화
        }

        _rectTransform.anchoredPosition = Vector2.zero; // 위치 초기화
        _canvasGroup.blocksRaycasts = true; // 다시 상호작용 가능하게 설정
    }

    // 다른 슬롯 위에 드래그를 놓았을 때
    public void OnDrop(PointerEventData eventData)
    {
        var fromSlot = eventData.pointerDrag?.GetComponent<UISlot>();
        if (fromSlot == null || fromSlot == _slot || fromSlot.Item == null) return;

        var inventory = GameManager.Instance.Player.Inventory;

        var fromItem = inventory[fromSlot.Index];
        var toItem = inventory[_slot.Index];

        // 빈 칸 = 그냥 이동
        if (toItem == null)
        {
            inventory[_slot.Index] = fromItem;
            inventory[fromSlot.Index] = null;
        }
        // 아이템이 이미 있는 경우 = 서로 교환
        else
        {
            inventory[_slot.Index] = fromItem;
            inventory[fromSlot.Index] = toItem;
        }

        // UI 슬롯 상태 갱신
        _slot.SetItem(inventory[_slot.Index], inventory[_slot.Index]?.IsEquipped == true);
        fromSlot.SetItem(inventory[fromSlot.Index], inventory[fromSlot.Index]?.IsEquipped == true);

    }
}
