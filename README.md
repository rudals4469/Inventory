# Inventory - RPG 스타일 캐릭터 정보 & 인벤토리 UI 프로젝트

이 프로젝트는 Unity의 UGUI(User Interface System)를 활용하여 캐릭터 정보, 인벤토리, 스탯을 시각적으로 표현하는 간단한 RPG 스타일의 UI 시스템을 구현하는 것을 목표로 하였습니다. 

UI 구성부터 데이터 연동, 장비 시스템, 드래그 앤 드롭 기능까지 단계적으로 기능을 확장하는 방식으로 개발을 진행하였습니다.

---

## 📌 목차

- [STEP 1: UI 구성](#step-1-ui-구성)
- [STEP 2: 스크립트 구성 및 연결](#step-2-스크립트-구성-및-연결)
- [STEP 3: UI 전환 기능 구현](#step-3-ui-전환-기능-구현)
- [STEP 4: 캐릭터 정보 연동](#step-4-캐릭터-정보-연동)
- [STEP 5: 인벤토리 슬롯 동적 생성](#step-5-인벤토리-슬롯-동적-생성)
- [STEP 6: 아이템 데이터 및 장비 시스템](#step-6-아이템-데이터-및-장비-시스템)
- [STEP 7: 아이템 장착 및 해제](#step-7-아이템-장착-및-해제)
- [STEP 8: 스탯 반영](#step-8-스탯-반영)
- [🎁 부가 기능: 드래그 앤 드롭](#-부가-기능-드래그-앤-드롭)

---

## STEP 1: UI 구성
<details>
<summary>열고 닫기</summary>

UIMainMenu, UIStatus, UIInventory의 세 가지 주요 UI를 각각 Canvas 단위로 구성하였습니다.

각 UI에는 버튼, 텍스트, 이미지, 스크롤 뷰 등 필요한 UI 요소들을 배치하였으며,예시 스크린샷을 참고하여 시각적으로 완성도 있는 구성을 목표로 하였습니다. 

구성에 사용된 스프라이트는 임의로 준비한 이미지를 활용하였습니다.

![메인메뉴](https://github.com/user-attachments/assets/ced160b1-7c70-4e44-92a2-fde0ff0c0033)
![스텟](https://github.com/user-attachments/assets/5e87e80a-3834-45bd-aee9-dd563f362c9d)
![인벤토리](https://github.com/user-attachments/assets/e3708314-271c-4e22-a007-0fa98c1d6e43)


</details>

---

## STEP 2: 스크립트 구성 및 연결
<details>
<summary>열고 닫기</summary>

UI 기능을 제어하고 게임 데이터를 처리하기 위해 GameManager, UIManager, 각 UI에 대응하는 스크립트(UIMainMenu, UIStatus, UIInventory), 그리고 게임의 핵심 데이터 구조인 Character 클래스를 작성하였습니다. 

UIManager는 각 UI를 SerializedField로 받아 내부적으로 제어하며, Character 클래스는 플레이어의 스탯, 인벤토리, 장착 아이템 등의 정보를 포함하도록 구성하였습니다.

```csharp
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Character Player { get; private set; }

    [SerializeField] private CharacterData characterData;

    private void Awake()
    {
        Instance = this;
        SetData();
    }

    private void SetData()
    {
        Player = new Character(characterData);
        UIManager.Instance.InitAllUI(Player);
    }
}
```
</details>

---

## STEP 3: UI 전환 기능 구현
<details> 
<summary>열고 닫기</summary>

UI 간 전환이 가능하도록 UIManager를 싱글톤(Singleton) 패턴으로 구현하였으며, UI 전환을 위한 메서드를 추가하였습니다.

UIMainMenu에서는 버튼 클릭 시 UIManager의 전환 메서드를 호출하여 상태창(UIStatus) 또는 인벤토리창(UIInventory)으로 전환할 수 있도록 구현하였습니다. 

각 버튼은 Start() 메서드에서 AddListener()를 통해 이벤트에 등록됩니다.

![전환](https://github.com/user-attachments/assets/93bb8cd2-ada7-4f75-8ab2-38e3f78498de)

```csharp
// UIManager.cs
public void OpenInventory()
{
    mainMenuUI.gameObject.SetActive(false);
    statusUI.gameObject.SetActive(false);
    inventoryUI.gameObject.SetActive(true);

    inventoryUI.SetData(GameManager.Instance.Player);
}
```
```csharp
// UIMainMenu.cs
private void Start()
{
    inventoryButton.onClick.AddListener(() =>
    {
        UIManager.Instance.OpenInventory();
    });
}
```
</details>

---

## STEP 4: 캐릭터 정보 연동
<details> 
<summary>열고 닫기</summary>

Character 클래스는 주요 정보를 get; private set;으로 캡슐화하고, 생성자를 통해 초기 데이터를 전달받을 수 있도록 구성하였습니다.

GameManager의 SetData() 메서드에서 캐릭터 데이터를 초기화하며, 이를 UIManager가 받아 InitAllUI()를 통해 각 UI에 전달함으로써, UI에 실시간으로 캐릭터 정보가 반영되도록 하였습니다.

특히, UILeftInfoPanel과 UIStatus에서는 해당 정보를 토대로 UI 요소를 갱신합니다.
    
```csharp
public void SetInfo(Character character)
{
    nameText.text = character.ID;
    levelText.text = $"Lv {character.Level}";
    descriptionText.text = character.Description;
    expSlider.value = (float)character.CurrentExp / character.MaxExp;
    expText.text = $"{character.CurrentExp} / {character.MaxExp}";
}
```
</details>

---

## STEP 5: 인벤토리 슬롯 동적 생성
<details> 
    <summary>열고 닫기</summary>

UIInventory에서는 ScrollView를 활용하여 인벤토리 슬롯 영역을 구성하였고, 슬롯 프리팹(UISlot)을 인스턴스화하여 최대 인벤토리 크기만큼 동적으로 슬롯을 생성하였습니다. 

각 슬롯은 SetItem()과 Clear() 메서드를 통해 UI와 데이터를 연동하며, 캐릭터가 보유한 아이템 정보를 시각적으로 표현할 수 있도록 하였습니다.

![스크롤뷰](https://github.com/user-attachments/assets/58b75a0a-684e-4e27-a168-0b5d55ae5c3f)

```csharp
for (int i = 0; i < _maxSlotCount; i++)
{
    GameObject go = Instantiate(slotPrefab, slotParent);
    UISlot slot = go.GetComponent<UISlot>();
    slot.SetIndex(i);
    _slotList.Add(slot);
}
```
</details>

---

## STEP 6: 아이템 데이터 및 장비 시스템
<details>
    <summary>열고 닫기</summary>

아이템 데이터는 ScriptableObject(ItemData)로 정의하였으며, 이를 기반으로 인스턴스화된 Item 클래스는 아이템의 장착 상태, 보너스 스탯, 아이콘 등을 포함하도록 설계하였습니다.

Character 클래스는 인벤토리(List<Item>)와 장비 슬롯(Dictionary<EquipSlot, Item>)을 함께 관리하며, 아이템의 장착 및 해제를 위한 EquipItem(), UnEquip() 메서드를 제공합니다. 

또한, 캐릭터의 총 스탯을 계산할 수 있도록 관련 메서드들도 함께 작성하였습니다.

```csharp
// Item.cs
public class Item
{
    public ItemData Data { get; private set; }
    public bool IsEquipped { get; private set; }

    public void Equip() => IsEquipped = true;
    public void UnEquip() => IsEquipped = false;
}
```
```csharp
// Character.cs
public void EquipItem(Item item)
{
    item.Equip();
    EquippedItems[item.Data.slot] = item;
}
```
</details>

---

## STEP 7: 아이템 장착 및 해제
<details> 
    <summary>열고 닫기</summary>

UISlot 클래스에서는 슬롯에 등록된 아이템을 더블 클릭하면 장착 및 해제 기능이 토글되도록 구현하였습니다. 

동일한 장비 부위에 이미 장착된 아이템이 있는 경우에는 기존 아이템을 자동으로 해제한 후 새로운 아이템을 장착하도록 하였습니다. 

장착 상태는 UI 상의 마크(equipMark)로 시각적으로 구분되며, 장착/해제 이후에는 UIManager를 통해 UI가 자동으로 갱신됩니다.

![장착](https://github.com/user-attachments/assets/aa8a5f90-cfa4-4084-a072-f628c05a4f48)

```csharp
private void ToggleEquip()
{
    if (_item.IsEquipped)
    {
        _item.UnEquip();
        _character.Unequip(_item.Data.slot);
    }
    else
    {
        _character.EquipItem(_item);
    }

    UIManager.Instance.InitAllUI(_character);
    UIManager.Instance.OpenInventory();
}
```
</details>

---

## STEP 8: 스탯 반영
<details> <summary>열고 닫기</summary>

장착된 아이템이 캐릭터의 스탯에 실시간으로 반영되도록 하기 위해, Character 클래스 내의 GetTotalAttack(), GetTotalDefense() 등의 메서드에서는 장착된 아이템의 보너스 스탯을 포함하여 최종 값을 반환합니다.

해당 값은 UIStatus에서 UI에 표시되며, 아이템 장착 또는 해제 시 UI가 자동으로 갱신되어 변화를 즉시 확인할 수 있습니다.



```csharp
// Character.cs
public int GetTotalAttack()
{
    int value = BaseAttack;
    foreach (var pair in EquippedItems)
        value += pair.Value.BonusAttack;
    return value;
}
```
```csharp
// UIStatus.cs
AddStat("공격력", character.GetTotalAttack().ToString(), attackIcon);
```
</details>

---

## 🎁 부가 기능: 드래그 앤 드롭
<details> 
    <summary>열고 닫기</summary>

ItemDraggable 스크립트를 통해 인벤토리 내 아이템을 드래그 앤 드롭으로 이동할 수 있도록 구현하였습니다. 

드래그 시작 시 원래 슬롯의 위치를 저장하고, 임시 오브젝트를 생성하여 빈 자리를 유지합니다. 

드롭이 완료되면 아이템 간의 위치가 교환되거나 이동되며, 드래그 종료 시에는 UI 상태를 원래대로 복원하거나 변경된 상태로 유지합니다.

![스위칭](https://github.com/user-attachments/assets/dc33e5a7-a1e4-48b5-90ef-e56e33846707)

```csharp
public void OnDrop(PointerEventData eventData)
{
    var fromSlot = eventData.pointerDrag?.GetComponent<UISlot>();
    if (fromSlot == null || fromSlot == _slot) return;

    var inventory = GameManager.Instance.Player.Inventory;
    (inventory[_slot.Index], inventory[fromSlot.Index]) =
        (inventory[fromSlot.Index], inventory[_slot.Index]);
}
```
</details>
