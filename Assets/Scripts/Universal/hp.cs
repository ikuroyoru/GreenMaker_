using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class hp : MonoBehaviour
{
    [Header("HP Config")]
    public float maxHP;
    public float currentHP;

    [Header("UI References")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI hpOutput;

    private GameObject entity; // Entidade (player, inimigo ou boss)
    public BossHealthUI bossUI; // Referência à UI do boss (opcional)

    private void Start()
    {
        entity = transform.parent.gameObject;
        currentHP = maxHP;

        if (hpBar != null)
        {
            hpBar.maxValue = maxHP;
            hpBar.value = currentHP;
        }

        if (hpOutput != null)
        {
            hpOutput.text = $"{currentHP} / {maxHP}";
        }
    }

    public void takeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // Evita HP negativo

        // Atualiza barra normal (ex: inimigo comum)
        if (hpBar != null)
            hpBar.value = currentHP;

        if (hpOutput != null)
            hpOutput.text = $"{currentHP} / {maxHP}";

        // Atualiza barra do boss, se for um boss
        if (entity.CompareTag("Boss"))
        {
            bossUI?.UpdateUI();
        }

        // Destroi se morreu
        if (currentHP <= 0)
        {
            Destroy(entity);
        }
    }

    private void Update()
    {
        // Teste rápido: tecla P só afeta Player
        if (Input.GetKeyDown(KeyCode.P) && entity.CompareTag("Player"))
        {
            takeDamage(20);
        }
    }
}
