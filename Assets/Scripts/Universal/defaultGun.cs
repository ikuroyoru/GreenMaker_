using System.Collections;
using UnityEngine;

public class defaultGun : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileLifetime = 3f;
    [SerializeField] float projectileDamage = 20f;
    [SerializeField] float cooldown = 0.5f;
    private bool activeCooldown = false;

    private GameObject targetBoss; // Refer�ncia ao boss clicado

    [SerializeField] private GameObject targetIconPrefab;
    private GameObject currentTargetIcon;

    [SerializeField] private BossHealthUI bossUI; // Refer�ncia no inspector
    [SerializeField] private GameObject bossUIObject; // UI GameObject


    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Bot�o direito
        {
            DetectBossUnderMouse();
        }

        if (Input.GetMouseButtonDown(0) && !activeCooldown && targetBoss != null)
        {
            ShootAtBoss();
        }
    }

    void DetectBossUnderMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Boss"))
        {
            targetBoss = hit.collider.gameObject;
            Debug.Log("Boss selecionado: " + targetBoss.name);

            Transform scriptChild = targetBoss.transform.Find("Script");
            if (scriptChild != null)
            {
                hp bossHP = scriptChild.GetComponent<hp>();
                if (bossHP != null)
                {
                    bossUIObject.SetActive(true);
                    bossUI.SetBoss(bossHP);
                    bossHP.bossUI = bossUI; // Associa a UI ao HP
                }
            }

            Vector3 center = hit.collider.bounds.center;

            if (currentTargetIcon != null)
            {
                Destroy(currentTargetIcon);
            }

            currentTargetIcon = Instantiate(targetIconPrefab, center, Quaternion.identity, targetBoss.transform);
        }

    }


    void ShootAtBoss()
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

        StartCoroutine(CooldownCoroutine());
    }


    IEnumerator CooldownCoroutine()
    {
        activeCooldown = true;
        yield return new WaitForSeconds(cooldown);
        activeCooldown = false;
    }
}
