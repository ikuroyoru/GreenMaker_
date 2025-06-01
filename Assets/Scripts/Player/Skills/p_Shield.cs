using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class p_Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] public Slider slider;
    [SerializeField] private float skillCooldown = 10f;

    private GameObject activeShield;
    private SkillManager skillManagerScript;
    private bool cooldownActivated;
    private bool shieldActive;

    void Start()
    {
        skillManagerScript = SkillManager.Instance;
        cooldownActivated = false;
        shieldActive = false;
    }

    public void Activate()
    {
        if (cooldownActivated || shieldActive)
        {
            Debug.LogWarning("Escudo não ativado. Já está ativo ou em cooldown.");
            return;
        }

        Debug.Log("Skill do Escudo Ativada");

        if (transform.parent != null && transform.parent.tag == "Player")
        {
            activeShield = Instantiate(shieldPrefab, transform.parent.position, Quaternion.identity, transform.parent);
            shieldHP shieldScript = activeShield.GetComponentInChildren<shieldHP>();

            if (shieldScript != null)
            {
                shieldScript.defUI(slider);
                shieldScript.InitializeShield();
                shieldScript.getReference(skillManagerScript);
                shieldScript.SetShieldParent(this); // ← usado para desativar o cooldown quando o escudo é destruído
            }
            else
            {
                Debug.LogWarning("shieldScript é null. Verifique se o prefab tem o script shieldHP.");
            }

            shieldActive = true;
            skillManagerScript.skillStatus(true);
        }
    }

    public void NotifyShieldDestroyed()
    {
        shieldActive = false;
        skillManagerScript.skillStatus(false);
        StartCoroutine(SetCooldown());
    }

    private IEnumerator SetCooldown()
    {
        cooldownActivated = true;
        yield return new WaitForSeconds(skillCooldown);
        cooldownActivated = false;
    }
}
