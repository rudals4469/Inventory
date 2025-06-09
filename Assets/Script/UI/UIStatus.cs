using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private Transform statListParent;
    [SerializeField] private GameObject statItemPrefab;
    [SerializeField] private Button backButton;

    // 각 스탯에 해당하는 아이콘 스프라이트
    [SerializeField] private Sprite attackIcon;
    [SerializeField] private Sprite defenseIcon;
    [SerializeField] private Sprite hpIcon;
    [SerializeField] private Sprite critIcon;

    public void SetData(Character character)
    {
        foreach (Transform child in statListParent)
        {
            Destroy(child.gameObject);
        }

        AddStat("공격력", character.GetTotalAttack().ToString(), attackIcon);
        AddStat("방어력", character.GetTotalDefense().ToString(), defenseIcon);
        AddStat("체력", character.GetTotalHP().ToString(), hpIcon);
        AddStat("치명타", character.GetTotalCrit() + "%", critIcon);
    }

    private void AddStat(string label, string value, Sprite icon)
    {
        GameObject go = Instantiate(statItemPrefab, statListParent);

        foreach (TMP_Text text in go.GetComponentsInChildren<TMP_Text>())
        {
            if (text.name == "StatLabel")
                text.text = label;
            else if (text.name == "StatValue")
                text.text = value;
        }

        Transform iconTransform = go.transform.Find("Icon");
        if (iconTransform != null)
        {
            Image iconImage = iconTransform.GetComponent<Image>();
            if (iconImage != null)
                iconImage.sprite = icon;
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