using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILeftInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text expText;

    public void SetInfo(Character character)
    {
        nameText.text = character.ID;
        levelText.text = $"Lv {character.Level}";
        descriptionText.text = character.Description;

        expSlider.value = (float)character.CurrentExp / character.MaxExp;
        expText.text = $"{character.CurrentExp} / {character.MaxExp}";
    }
}
