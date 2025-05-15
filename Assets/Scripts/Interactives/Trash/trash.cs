using UnityEngine;
using UnityEngine.UI;

public class trash : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public Slider slider;
    private Materials materialScript;

    private GameManager gameManager;

    private void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;

        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager não encontrado.");
        }
    }


    public void TakeDamage(float amount, GameObject player)
    {
        currentHealth -= amount;
        slider.value = currentHealth;
        // Debug.LogWarning("HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        // Buscar o script Materials no player
        if (player != null)
        {
            materialScript = gameManager.GetComponent<Materials>();

            if (materialScript != null)
            {
                materialScript.gerarPlastico(player);
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
}
