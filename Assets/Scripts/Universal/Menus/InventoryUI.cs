using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // Arraste aqui o painel do inventário na Unity
    private bool isOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(isOpen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);
        }
    }
}
