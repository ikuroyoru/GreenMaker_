using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int plastico;

    public void updatePlastico(int amount, GameObject player)
    {
        plastico += amount;
        Debug.Log("Plastico: " + plastico);
    }
}
