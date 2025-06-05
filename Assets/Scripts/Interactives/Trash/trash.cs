using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class trash : MonoBehaviour
{
    public string collectableName; 
    public int maxHealth;
    private int currentHealth;
    private Materials materialScript;

    private GameManager gameManager;

    private collectv2 collectScript;

    private void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager não encontrado.");
        }
        // else Debug.LogWarning("GameManager Encontrado");
    }


    public void TakeDamage(int amount, GameObject player)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Destruir");
        }

        // Buscar o script Materials no player
        if (player != null)
        {
            materialScript = gameManager.GetComponent<Materials>();

            if (materialScript != null)
            {
                materialScript.StartCoroutine(materialScript.generate(player));
            }
            else
            {
                Debug.LogWarning("gameManager não tem o script Materials.");
            }
        }
        else
        {
            Debug.LogWarning("Player é nulo.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollectTrigger(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        HandleCollectTrigger(other, false);
    }

    private void HandleCollectTrigger(Collider2D other, bool entering) // Atualiza o Status se tem ou não um coletável por perto do jogador
    {
        if (!other.CompareTag("Player")) return;

        collectv2 collectScript = other.GetComponentInChildren<collectv2>();

        if (collectScript != null)
        {
            collectScript.verifyStatus(gameObject, entering);

            // Debug.Log($"[Coleta] {(entering ? "Entrou" : "Saiu")} da área de coleta: {other.name}");
        }
        else
        {
            Debug.LogWarning($"[Coleta] Script 'collectv2' não encontrado em {other.name}.");
        }
    }

    public int currentHP()
    {
        return currentHealth;
    }


}
