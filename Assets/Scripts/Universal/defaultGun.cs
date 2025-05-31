using System.Collections;
using UnityEngine;

public class defaultAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileLifetime = 3f;
    [SerializeField] float projectileDamage = 20f;
    [SerializeField] float cooldown = 0.2f;
    [SerializeField] int projectileQTD = 10;
    [SerializeField] float fireInterval = 0.1f;

    private bool activeCooldown;
    private bool isShooting;

    [SerializeField] private GameObject targetIconPrefab;

    [SerializeField] private BossHealthUI bossUI;
    [SerializeField] private GameObject bossUIObject;

    private SkillManager skillManagerScript;

    private void Start()
    {
        activeCooldown = false;
        skillManagerScript = GetComponent<SkillManager>();

        isShooting = false;
    }

    void Update()
    {
        // Nada necessário aqui por enquanto
    }

    public void Shoot()
    {
        if (!activeCooldown && !isShooting)
        {
            isShooting = true;
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
        else
        {
            Debug.LogWarning("SkillManager não encontrado para desativar o status da skill.");
        }
    }
}
