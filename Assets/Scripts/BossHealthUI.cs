using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image bossIcon;

    private hp bossHP;

    public void SetBoss(hp newBossHP)
    {
        bossHP = newBossHP;
       // bossIcon.sprite = icon;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (bossHP == null) return;

        float nowHP = bossHP.currentHP;
        float totalHP = bossHP.maxHP;

        Debug.LogWarning("hp: " + bossHP.currentHP);

        healthBarFill.maxValue = totalHP;
        healthBarFill.value = nowHP;
        healthText.text = $"{nowHP} / {totalHP}";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
