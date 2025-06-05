using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private int plastico;
    private int metal;
    private int eletronico;
    private int parteMecanica;

    [Header("UI Texts")]
    public TextMeshProUGUI plasticoText;
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI eletronicoText;
    public TextMeshProUGUI parteMecanicaText;

    private bool activeFeedback;
    private float feedbackTimer = 3f;

    [SerializeField] private GameObject feedbackPanel;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI qtdTEXT;

    private Coroutine feedbackCoroutine; // <- adiciona isso

    private void Start()
    {
        activeFeedback = false;

        if (feedbackPanel != null) feedbackPanel.SetActive(activeFeedback);

        plastico = 0;
        metal = 0;
        parteMecanica = 0;
        eletronico = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        plasticoText.text = "x" + plastico;
        metalText.text = "x" + metal;
        eletronicoText.text = "x" + eletronico;
        parteMecanicaText.text = "x" + parteMecanica;
    }

    public void updatePlastico(int amount, GameObject player, Sprite icon)
    {
        plastico += amount;
        RestartFeedback(icon, amount);
        UpdateUI();
    }

    public void updateMetal(int amount, GameObject player, Sprite icon)
    {
        metal += amount;
        RestartFeedback(icon, amount);
        UpdateUI();
    }

    public void updateEletronico(int amount, GameObject player, Sprite icon)
    {
        eletronico += amount;
        RestartFeedback(icon, amount);
        UpdateUI();
    }

    public void updateMecanica(int amount, GameObject player, Sprite icon)
    {
        parteMecanica += amount;
        RestartFeedback(icon, amount);
        UpdateUI();
    }

    private void RestartFeedback(Sprite icon, int _qtd)
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        feedbackCoroutine = StartCoroutine(collectFeedback(icon, _qtd));
    }

    public IEnumerator collectFeedback(Sprite _icon, int _qtd)
    {
        activeFeedback = true;
        resourceIcon.sprite = _icon;
        qtdTEXT.text = "+" + _qtd.ToString();

        if (feedbackPanel != null)
            feedbackPanel.SetActive(activeFeedback);

        yield return new WaitForSeconds(feedbackTimer);

        resetFeedback();
    }

    public void resetFeedback()
    {
        activeFeedback = false;
        if (feedbackPanel != null)
            feedbackPanel.SetActive(activeFeedback);
    }
}
