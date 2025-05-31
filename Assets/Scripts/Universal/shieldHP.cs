using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class shieldHP : MonoBehaviour
{
    [SerializeField] private float hp;
    private float maxHP;

    private SkillManager skillScript;
    private p_Shield shieldParent;

    private Slider slider;
    private TextMeshProUGUI shieldOutput;

    public void InitializeShield()
    {
        maxHP = hp;
        if (slider != null)
        {
            slider.maxValue = maxHP;
            slider.value = hp;
            shieldOutput.text = hp + " / " + maxHP;
        }
    }

    void Update()
    {
        // Debug/teste local – remova em produção
        if (Input.GetKeyDown(KeyCode.O))
        {
            takeDamage(10f);
        }
    }

    public void takeDamage(float amount)
    {
        hp -= amount;
        updateShieldUI();

        if (hp <= 0)
        {
            HandleShieldBreak();
        }
    }

    void HandleShieldBreak()
    {
        if (shieldParent != null)
        {
            shieldParent.NotifyShieldDestroyed();
        }
        else
        {
            Debug.LogWarning("p_Shield (shieldParent) não atribuído.");
            if (transform.parent != null && transform.parent.gameObject.CompareTag("Player"))
            {
                skillScript?.skillStatus(false);
            }
        }

        Destroy(transform.parent.gameObject); // Destrói o escudo (filho do jogador)
    }

    public void getReference(SkillManager script)
    {
        skillScript = script;
    }

    public void SetShieldParent(p_Shield parent)
    {
        shieldParent = parent;
    }

    void updateShieldUI()
    {
        if (slider != null)
        {
            slider.value = hp;
            if (shieldOutput != null)
            {
                shieldOutput.text = hp + " / " + maxHP;
            }
        }
    }

    public void defUI(Slider _slider)
    {
        slider = _slider;
        shieldOutput = slider.GetComponentInChildren<TextMeshProUGUI>();

        if (shieldOutput == null)
        {
            Debug.LogWarning("Nenhum TextMeshProUGUI encontrado como filho do Slider.");
        }

        if (slider != null)
        {
            slider.maxValue = maxHP;
            slider.value = hp;
            if (shieldOutput != null)
            {
                shieldOutput.text = hp + " / " + maxHP;
            }
        }
        else
        {
            Debug.LogWarning("Slider está null na defUI!");
        }
    }
}
