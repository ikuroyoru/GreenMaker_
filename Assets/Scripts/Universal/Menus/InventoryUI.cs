using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    private bool isOpen = false;

    public static bool IsInventoryOpen { get; private set; } // ← Adicionado

    void Start()
    {
        inventoryPanel.SetActive(isOpen);
        IsInventoryOpen = isOpen;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);
            IsInventoryOpen = isOpen; // ← Atualiza o estado global
        }
    }
}
