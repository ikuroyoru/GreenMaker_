using System.Collections;
using UnityEngine;

public class defaultAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileLifetime = 3f;
    [SerializeField] float projectileDamage = 20f;
    [SerializeField] float cooldown = 0.1f;
    [SerializeField] int projectileQTD = 10;
    [SerializeField] float fireInterval = 0.1f; // Intervalo entre os disparos da rajada

    private bool activeCooldown;

    private GameObject targetBoss; // Referência ao boss clicado

    [SerializeField] private GameObject targetIconPrefab;
    private GameObject currentTargetIcon;

    [SerializeField] private BossHealthUI bossUI; // Referência no inspector
    [SerializeField] private GameObject bossUIObject; // UI GameObject

    private SkillManager skillManagerScript;

    private void Start()
    {
        activeCooldown = false;
        skillManagerScript = GetComponent<SkillManager>();
    }

    void Update()
    {
    }

    public void Shoot()
    {
        if (!activeCooldown)
        {
            StartCoroutine(shooting());
        }
    }

    IEnumerator shooting()
    {
        int count = 0;
        float interval = fireInterval; //Intervalo entre os disparos da rajada.

        while (count < projectileQTD)
        {
            // Obtém a posição do mouse no mundo
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            // Calcula a direção entre o player e o mouse
            Vector3 direction3D = mouseWorldPos - transform.position;

            Vector2 direction = direction3D.normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Rotaciona o projétil na direção correta
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

            // Aplica os valores ao script do projétil
            defaultProjectile pScript = projectile.GetComponent<defaultProjectile>();
            pScript.SetValues(projectileSpeed, projectileLifetime, projectileDamage, direction);

            yield return new WaitForSeconds(interval);
            count += 1;
        }

        StartCoroutine(applyCooldown());
    }

    IEnumerator applyCooldown()
    {
        activeCooldown = true;
        yield return new WaitForSeconds(cooldown);
        activeCooldown = false;
        skillManagerScript.skillStatus(false);
    }

    /*
     
    public void ShootAtBoss()
    {
        Vector3 bossCenter = targetBoss.GetComponent<Collider2D>().bounds.center;
        Vector3 direction3D = bossCenter - transform.position;
        direction3D.z = 0f;
        Vector2 direction = direction3D.normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        defaultProjectile pScript = projectile.GetComponent<defaultProjectile>();
        pScript.SetValues(projectileSpeed, projectileLifetime, projectileDamage, direction);

        StartCoroutine(applyCooldown());
    }

    */


}
