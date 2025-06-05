using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Character Player { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetData();
    }

    private void SetData()
    {
        Player = new Character(
            id: "Chad",
            level: 10,
            curHp: 9,
            maxHp: 12,
            desc: "코딩의 노예가 된 지 10년째...",
            atk: 35,
            def: 40,
            hp: 100,
            critRate: 25,
            curExp: 30,
            maxExp: 100
            
        );
        
        Sprite dummyIcon = Resources.Load<Sprite>("test");

        Player.Inventory.Add(new Item("강철검", dummyIcon));
        Player.Inventory.Add(new Item("방패", dummyIcon));
        Player.Inventory.Add(new Item("마법책", dummyIcon));
        Player.Inventory.Add(new Item("수정검", dummyIcon));

        UIManager.Instance.InitAllUI(Player);
    }
}