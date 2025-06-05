using System.Collections;
using UnityEngine;

public class Materials : MonoBehaviour
{
    private Inventory inventoryScript;
    private const int qtd = 1;

    [SerializeField] private Sprite plasticIcon;
    [SerializeField] private Sprite eletronicIcon;
    [SerializeField] private Sprite metalIcon;
    [SerializeField] private Sprite mechanicIcon;

    public IEnumerator generate(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("Sem player");
            yield break;
        }

        if (inventoryScript == null)
        {
            inventoryScript = player.GetComponentInChildren<Inventory>();

            if (inventoryScript == null)
            {
                Debug.LogWarning("Inventory n�o encontrado no player.");
                yield break;
            }
        }

        int roll = Random.Range(1, 116); // de 1 at� 115 inclusivo

        if (roll <= 50)
        {
            inventoryScript.resetFeedback();
            inventoryScript.updatePlastico(qtd, player, plasticIcon);
            Debug.Log("Pl�stico gerado!");
        }
        else if (roll <= 80)
        {
            inventoryScript.resetFeedback();
            inventoryScript.updateMetal(qtd, player, metalIcon);
            Debug.Log("Metal gerado!");
        }
        else if (roll <= 95)
        {
            inventoryScript.resetFeedback();
            inventoryScript.updateEletronico(qtd, player, eletronicIcon);
            Debug.Log("Eletr�nico gerado!");
        }
        else
        {
            inventoryScript.resetFeedback();
            inventoryScript.updateMecanica(qtd, player, mechanicIcon);
            Debug.Log("Parte Mec�nica gerada!");
        }

        yield break;
    }
}
