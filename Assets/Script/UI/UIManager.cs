using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIMainMenu mainMenuUI;
    [SerializeField] private UIStatus statusUI;
    [SerializeField] private UIInventory inventoryUI;
    [SerializeField] private UILeftInfoPanel leftInfoPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void InitAllUI(Character character)
    {
        leftInfoPanel.SetInfo(character);
        statusUI.SetData(character);
        inventoryUI.SetData(character);

        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        mainMenuUI.gameObject.SetActive(true);
        statusUI.gameObject.SetActive(false);
        inventoryUI.gameObject.SetActive(false);
    }

    public void OpenStatus()
    {
        mainMenuUI.gameObject.SetActive(false);
        statusUI.gameObject.SetActive(true);
        inventoryUI.gameObject.SetActive(false);
    }

    public void OpenInventory()
    {
        mainMenuUI.gameObject.SetActive(false);
        statusUI.gameObject.SetActive(false);
        inventoryUI.gameObject.SetActive(true);
    }
}