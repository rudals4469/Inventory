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

        AddStat("공격력", character.GetTotalAttack().ToString());
        AddStat("방어력", character.GetTotalDefense().ToString());
        AddStat("체력", character.GetTotalHP().ToString());
        AddStat("치명타", character.GetTotalCrit() + "%");
    }

    private void AddStat(string label, string value)
    {
        GameObject go = Instantiate(statItemPrefab, statListParent);

        foreach (TMP_Text text in go.GetComponentsInChildren<TMP_Text>())
        {
            if (text.name == "StatLabel")
                text.text = label;
            else if (text.name == "StatValue")
                text.text = value;
        }
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenMainMenu();
        });
    }
}