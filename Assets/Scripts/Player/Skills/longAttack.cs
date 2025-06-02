using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class longAttack : MonoBehaviour
{
    private int projectileLimit;
    private float batteryCost;
    private float damage;
    private float skillCooldown;

    private bool cooldownActivated;
    private bool isShooting = false;
    private int shotsFired = 0;

    [SerializeField] private GameObject missilePrefab;

    private GameObject target;

    private PlayerMovement movementScript;
    private SkillManager skillManagerScript;
    private p_Battery batteryScript;

    private void Awake()
    {
        movementScript = GetComponent<PlayerMovement>();
        skillManagerScript = SkillManager.Instance;
        batteryScript = GetComponent<p_Battery>();

        cooldownActivated = false;

        ApplySkillLevel(); // ← inicializa com o nível atual da skill
    }

    private void Update()
    {
        if (!isShooting) return;

        if (Input.GetMouseButtonDown(0) && shotsFired < projectileLimit)
        {
            if (batteryScript.currentCharge() < batteryCost)
            {
                Debug.LogWarning("Bateria insuficiente para atirar.");
                return;
            }

            batteryScript.UseAbility(batteryCost);

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
                projectileHitScript.setDamage(damage);
            }

            shotsFired++;

            if (shotsFired >= projectileLimit)
            {
                StartCoroutine(setCooldown());
            }
        }
    }

    public void Activate()
    {
        if (!cooldownActivated)
        {
            ApplySkillLevel(); // ← reaplica os valores ao ativar (caso o nível tenha mudado)
            Debug.LogWarning("Skill de Longo Alcance Ativada");

            isShooting = true;
            shotsFired = 0;
            movementScript.locked = true;
            skillManagerScript.skillStatus(true);
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
        yield return new WaitForSeconds(0.5f); //Para evitar execuções simultaneas de habilidades

        EndSkill();

        cooldownActivated = true;

        yield return new WaitForSeconds(skillCooldown);

        cooldownActivated = false;
    }

    // PEGA os valores da skill baseada no nível atual definido no SkillManager
    private void ApplySkillLevel()
    {
        var stats = SkillManager.Instance.GetSkillStats("long");

        projectileLimit = stats.quantity;
        batteryCost = stats.charge; // pode ajustar para usar outro campo
        // attackRange = stats.duration;      // se quiser, pode usar um campo específico
        damage = stats.damage;
        skillCooldown = stats.cooldown;
    }
}
