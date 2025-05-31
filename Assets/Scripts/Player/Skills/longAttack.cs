using System.Collections;
using UnityEngine;

public class longAttack : MonoBehaviour
{
    [SerializeField] private int projectileLimit = 3;
    [SerializeField] private float batteryCostPerShot = 50f;

    public float attackRange = 5f;
    public float fireCooldown = 10f;
    public float skillCooldown = 10f;
    private bool cooldownActivated;

    [SerializeField] private GameObject missilePrefab;

    private bool isShooting = false;
    private int shotsFired = 0;

    private GameObject target;

    private PlayerMovement movementScript;
    private SkillManager skillManagerScript;
    private p_Battery batteryScript;

    private void Awake()
    {
        movementScript = GetComponent<PlayerMovement>();
        skillManagerScript = GetComponent<SkillManager>();
        batteryScript = GetComponent<p_Battery>();

        cooldownActivated = false;
    }

    private void Update()
    {
        if (!isShooting) return;

        if (Input.GetMouseButtonDown(0) && shotsFired < projectileLimit)
        {
            if (batteryScript.currentCharge() < batteryCostPerShot)
            {
                Debug.LogWarning("Bateria insuficiente para atirar.");
                return;
            }

            batteryScript.UseAbility(batteryCostPerShot);

            if (target == null)
            {
                Debug.LogWarning("Nenhum alvo definido para o míssil.");
                return;
            }

            GameObject projectile = Instantiate(missilePrefab, transform.position, Quaternion.identity);

            Missile missileScript = projectile.GetComponentInChildren<Missile>();
            if (missileScript != null)
            {
                missileScript.SetTarget(target);
            }

            projectileHit projectileHitScript = projectile.GetComponentInChildren<projectileHit>();
            if (projectileHitScript != null)
            {
                projectileHitScript.shooter(transform.parent.gameObject.tag);
            }

            shotsFired++;

            if (shotsFired >= projectileLimit)
            {
                EndSkill();
                StartCoroutine(setCooldown());
            }
        }


    }

    public void Activate()
    {
        if (!cooldownActivated)
        {
            Debug.LogWarning("Skill de Longo Alcance Ativada");

            isShooting = true;
            shotsFired = 0;
            movementScript.locked = true;
            skillManagerScript.skillStatus(true);
            Debug.LogWarning("Skill Ativada");
        }
        else
        {
            Debug.LogWarning("Longo Alcance NÃO ATIVADO, há cooldown ATIVO");
        }
        
    }

    private void EndSkill()
    {
        isShooting = false;
        movementScript.locked = false;
        skillManagerScript.skillStatus(false);

        Debug.Log("Skill de Longo Alcance finalizada");
    }

    public void setTarget(GameObject _target)
    {
        target = _target;
    }

    public IEnumerator setCooldown()
    {
        cooldownActivated = true;

        yield return new WaitForSeconds(skillCooldown);

        cooldownActivated = false;

    }
}
