using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button statusButton;
    [SerializeField] Button inventoryButton;

    Character player;

    void Start()
    {
        if (statusButton)
            statusButton.onClick.AddListener(OnStatusButtonClick);

        if (inventoryButton)
            inventoryButton.onClick.AddListener(OnInventoryButtonClick);
    }

    private void OnStatusButtonClick()
    {
        UIManager.Instance.OpenStatus();
    }

    private void OnInventoryButtonClick()
    {
        UIManager.Instance.OpenInventory();
    }
}
