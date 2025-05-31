using UnityEngine;
using System.Collections;

public class collect : MonoBehaviour
{
    public GameObject areaVisualPrefab; // ← arraste o prefab com SpriteRenderer aqui no Inspector

    [SerializeField] private float scanRadius = 5f;
    [SerializeField] private float skillTimer = 5f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private float batteryCost = 50f;
    [SerializeField] private float skillCooldown = 5f;
    private bool cooldownActivated;
    private bool collecting;

    private float damagePerSecond;
    private float batteryCostPerSecond;
    private score scoreScript;
    private SkillManager skillManagerScript;
    private p_Battery batteryScript;

    private void Start()
    {
        scoreScript = GetComponent<score>();
        damagePerSecond = damage / skillTimer;
        skillManagerScript = GetComponent<SkillManager>();
        batteryScript = GetComponent<p_Battery>();

        batteryCostPerSecond = batteryCost / skillTimer;
        cooldownActivated = false;
        collecting = false;
    }

    public void Activate()
    {
        if (!cooldownActivated && !collecting)
        {
            StartCoroutine(collectSkill());
        }
        else Debug.LogWarning("NÃO ATIVADA. Skill já ativa ou com cooldown ativo");
    }

    public IEnumerator collectSkill()
    {
        collecting = true;

        Debug.LogWarning("Skill de Coleta Ativada");

        GameObject areaVisual = null;
        if (areaVisualPrefab != null)
        {
            areaVisual = Instantiate(areaVisualPrefab, transform.position, Quaternion.identity);
            areaVisual.transform.localScale = new Vector3(scanRadius * 2, scanRadius * 2, 1);
            areaVisual.transform.parent = transform;
        }

        float count = 0f;
        float points = damagePerSecond;

        while (count < skillTimer)
        {
            if (batteryScript.currentCharge() < batteryCostPerSecond)
            {
                Debug.LogWarning("Skill cancelada por falta de bateria.");
                break;
            }

            batteryScript.UseAbility(batteryCostPerSecond); // Consome energia da skill

            Collider2D[] lixos = Physics2D.OverlapCircleAll(transform.position, scanRadius);
            if (lixos != null)
            {
                foreach (Collider2D lixo in lixos)
                {
                    if (lixo.CompareTag("trash"))
                    {
                        trash trashScript = lixo.GetComponent<trash>();
                        GameObject player = transform.root.gameObject;

                        if (trashScript != null)
                        {
                            trashScript.TakeDamage(damagePerSecond, player);
                            scoreScript.updateGeneralPoints(points);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(1f);
            count += 1f;
        }

        if (areaVisual != null)
        {
            Destroy(areaVisual);
        }

        skillManagerScript.skillStatus(false);
        StartCoroutine(setCooldown());
        collecting = false;
    }

    public IEnumerator setCooldown()
    {
        cooldownActivated = true;

        yield return new WaitForSeconds(skillCooldown);

        cooldownActivated = false;

    }
}
