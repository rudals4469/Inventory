using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private Transform statListParent;
    [SerializeField] private GameObject statItemPrefab;
    [SerializeField] private Button backButton;

    public void SetData(Character character)
    {
        foreach (Transform child in statListParent)
        {
            Destroy(child.gameObject);
        }

        AddStat("공격력", character.Attack.ToString());
        AddStat("방어력", character.Defense.ToString());
        AddStat("체력", character.HP.ToString());
        AddStat("치명타", character.CriticalRate + "%");
    }

    private void AddStat(string label, string value)
    {
        GameObject go = Instantiate(statItemPrefab, statListParent);
        TMP_Text[] texts = go.GetComponentsInChildren<TMP_Text>();
        texts[0].text = label;
        texts[1].text = value;
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenMainMenu();
        });
    }
}