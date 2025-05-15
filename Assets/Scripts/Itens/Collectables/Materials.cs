using UnityEngine;

public class Materials : MonoBehaviour
{
    private Inventory inventoryScript;
    private int qtd;

    private void Start()
    {
        qtd = 1;
    }
    public void gerarPlastico(GameObject player)
    {
        if (player != null)
        {

            inventoryScript = player.GetComponentInChildren<Inventory>();

            int dropChance = 500; // Chance de gerar um plastico
            int chance = Random.Range(1, 1001); // Gera um número entre 1 e 1000

            // Exibir o número aleatório gerado no console
            Debug.Log($"Chance gerada: {chance}%");

            if (chance <= dropChance)
            {
                inventoryScript.updatePlastico(qtd, player);
            }
        }
        else Debug.LogWarning("Sem player");
    }
}
