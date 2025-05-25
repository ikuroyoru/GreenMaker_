using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image bossIcon;

    [Tooltip("Tempo em segundos que a UI ficará visível após selecionar um boss")]
    [SerializeField] private float timing = 5f;

    private hp bossHP;
    private b_Info bossInfo;
    private Coroutine hideCoroutine;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (bossHP != null && transform.parent.gameObject.activeSelf)
        {
            UpdateUI();
        }
    }

    public void SetBoss(hp newBossHP, b_Info newBossInfo)
    {
        bossHP = newBossHP;
        bossInfo = newBossInfo;
        Show();

        // Reinicia o timer de esconder, se já estiver rodando
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterDelay());

        UpdateUI(); // Atualiza imediatamente ao selecionar
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(timing);
        Hide();
    }

    public void UpdateUI()
    {
        if (bossHP == null) return;

        float nowHP = bossHP.currentHP;
        float totalHP = bossHP.maxHP;

        if (bossInfo != null && bossIcon != null)
            bossIcon.sprite = bossInfo.icon; // CORRIGIDO

        if (totalHP <= 0) return;

        healthBarFill.maxValue = totalHP;
        healthBarFill.value = Mathf.Lerp(healthBarFill.value, nowHP, Time.deltaTime * 10f);
        healthText.text = $"{Mathf.CeilToInt(nowHP)} / {Mathf.CeilToInt(totalHP)}";
    }

    public void Hide()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void Show()
    {
        transform.parent.gameObject.SetActive(true);
    }
}
