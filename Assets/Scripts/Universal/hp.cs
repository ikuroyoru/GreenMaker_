using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class hp : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    [Header("UI References")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI hpOutput;

    private GameObject entity;
    private BossHealthUI bossUI;

    private void Start()
    {
        entity = transform.parent.gameObject;

        if (currentHP <= 0)
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
        hpBar.maxValue = maxHP;

        Debug.Log("BOSS: Dano Recebido: " + damage);
        Debug.LogWarning("HP: " + currentHP + " / " + maxHP);

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (entity.CompareTag("Boss"))
        {
            if (hpBar != null)
                
                hpBar.value = currentHP;
        }

        if (hpBar != null)
            hpBar.value = currentHP;

        if (hpOutput != null)
            hpOutput.text = $"{currentHP} / {maxHP}";

        if (entity.CompareTag("Boss"))
        {
            bossUI?.UpdateUI();
        }

        if (currentHP <= 0)
        {
            Debug.Log(entity.name + " morreu.");
            Destroy(entity);
        }

        Debug.Log("Slider real: " + hpBar.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && entity.CompareTag("Player"))
        {
            takeDamage(20);
        }
    }

    public void setHP(float hp)
    {
        maxHP = hp;
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

    public void setEnemyUI(BossHealthUI ui)
    {
        bossUI = ui;
    }
}
