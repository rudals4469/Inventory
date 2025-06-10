# Inventory# RPG 스타일 캐릭터 정보 & 인벤토리 UI 프로젝트

이 프로젝트는 UGUI를 활용하여 RPG 스타일의 캐릭터 정보창, 인벤토리 시스템, 스탯 관리 UI를 구현한 예제입니다.  
단계별로 기능을 확장해나가는 방식으로 구성되며, 드래그 앤 드롭, 장비 시스템, 스탯 연동 등 다양한 UI 상호작용을 포함합니다.

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

UIMainMenu, UIStatus, UIInventory의 세 가지 UI 화면을 각각 Canvas로 만들고, 하위에 필요한 UI 요소(Button, Image, TMP_Text, ScrollView 등)를 배치하였습니다.  
각 UI는 예시 스크린샷을 참고하여 구성되며, 임의의 스프라이트를 사용해 시각적 요소를 꾸몄습니다.

> 이 단계는 Unity 에디터에서 작업하며 별도 코드 스크립트는 없습니다.

</details>

---

## STEP 2: 스크립트 구성 및 연결
<details>
<summary>열고 닫기</summary>

UI를 제어하고 캐릭터 데이터를 다루기 위한 핵심 스크립트를 작성합니다.

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
</details>
