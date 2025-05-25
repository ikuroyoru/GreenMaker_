using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class shieldHP : MonoBehaviour
{
    [SerializeField] float hp;
    private float maxHP;
    private SkillManager skillScript;

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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            takeDamage(10f);
        }
    }

    public void takeDamage(float amount)
    {
        hp -= amount;
        // Debug.Log("hp: " + hp + "/" + maxHP);
        updateShieldUI();

        if (hp <= 0)
        {
            if(transform.parent.gameObject.tag == "Player")
            {
                skillScript.skillStatus(false);
            }
            Destroy(transform.parent.gameObject);
        }
    }

    public void getReference(SkillManager script)
    {
        skillScript = script;
    }

    void updateShieldUI()
    {
        slider.value = hp;
        shieldOutput.text = hp + " / " + maxHP;
    }

    public void defUI(Slider _slider)
    {
        slider = _slider;
        shieldOutput = slider.GetComponentInChildren<TextMeshProUGUI>();

        if (shieldOutput == null)
            Debug.LogWarning("Nenhum TextMeshProUGUI encontrado como filho do Slider.");

        if (slider != null)
        {
            slider.maxValue = maxHP;
            slider.value = hp;
            shieldOutput.text = hp + " / " + maxHP;
        }
        else
        {
            Debug.LogWarning("Slider está null na defUI!");
        }
    }

}
