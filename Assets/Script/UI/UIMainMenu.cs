using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;
    
    private void Start()
    {
        statusButton.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenStatus();
        });

        inventoryButton.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenInventory();
        });
    }
}