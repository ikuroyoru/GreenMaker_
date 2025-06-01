using System.Collections;
using UnityEngine;

public class defaultAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private GameObject targetIconPrefab;

    [SerializeField] private BossHealthUI bossUI;
    [SerializeField] private GameObject bossUIObject;

    private float projectileSpeed = 20f;
    private float projectileLifetime = 3f;

    // Variáveis que serão atualizadas pelo SkillManager
    private float projectileDamage;
    private float cooldown;
    private int projectileQTD;
    private float fireInterval;
    private float charge;

    private bool activeCooldown;
    private bool isShooting;

    private SkillManager skillManagerScript;

    private p_Battery batteryScript;

    private void Start()
    {
        activeCooldown = false;
        isShooting = false;

        // Procura o SkillManager na cena
        skillManagerScript = SkillManager.Instance;
        batteryScript = GetComponent<p_Battery>();

        if (skillManagerScript != null)
        {
            AtualizarDadosDaSkill();
        }
        else
        {
            Debug.LogWarning("SkillManager não encontrado.");
        }

        if (batteryScript == null)
        {
            Debug.LogWarning("BatteryScript não encontrado.");
        }
    }

    private void AtualizarDadosDaSkill()
    {
        var skillStats = skillManagerScript.GetSkillStats("default");

        if (skillStats != null)
        {
            projectileDamage = skillStats.damage;
            cooldown = skillStats.cooldown;
            projectileQTD = skillStats.quantity;
            charge = skillStats.charge;
            fireInterval = 0.1f; // Se quiser tornar isso também variável, adicione ao SkillLevelStats
        }
        else
        {
            Debug.LogWarning("Dados da skill 'default' não encontrados no SkillManager.");
        }
    }

    public void Shoot()
    {
        if (!activeCooldown && !isShooting)
        {
            isShooting = true;

            if (skillManagerScript != null)
                AtualizarDadosDaSkill(); // Garante que esteja usando os valores mais atuais

            StartCoroutine(shooting());
        }
    }

    IEnumerator shooting()
    {
        int count = 0;

        while (count < projectileQTD)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector3 direction3D = mouseWorldPos - transform.position;
            Vector2 direction = direction3D.normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

            defaultProjectile pScript = projectile.GetComponent<defaultProjectile>();
            pScript.SetValues(projectileSpeed, projectileLifetime, projectileDamage, direction);

            batteryScript.UseAbility(charge);

            yield return new WaitForSeconds(fireInterval);
            count++;


        }

        StartCoroutine(applyCooldown());
        isShooting = false;
    }

    IEnumerator applyCooldown()
    {
        activeCooldown = true;
        yield return new WaitForSeconds(cooldown);
        activeCooldown = false;

        if (skillManagerScript != null)
        {
            skillManagerScript.skillStatus(false);
        }
    }
}
