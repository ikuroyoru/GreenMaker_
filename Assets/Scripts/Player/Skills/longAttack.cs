using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;

public class longAttack : MonoBehaviour
{
    public float attackRange = 5f;
    public float fireCooldown = 10f;
    public float skillCooldown = 10f;

    private bool isShooting = false;

    private PlayerMovement movementScript;

    [SerializeField] private GameObject missilePrefab;
    private GameObject target;

    private projectileHit projectileHitScript;

    void Awake()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    public void Activate()
    {
        Debug.LogWarning("Skill de Longo Alcance Ativada");
        isShooting = !isShooting;

        movementScript.locked = isShooting; // Desativa o movimento enquanto atira

        Debug.Log("Ativado? " + isShooting);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && movementScript.locked) // Apenas atira m�sseis se a skill estiver ativada
        {
            StartCoroutine(shooting());
        }
    }

    IEnumerator shooting()
    {
        if (target == null)
        {
            Debug.LogWarning("Nenhum alvo definido para o m�ssil.");
            yield break;
        }

        // Instancia o m�ssil na posi��o do jogador
        GameObject projectile = Instantiate(missilePrefab, transform.position, Quaternion.identity);

        // Pega o script do m�ssil e define o alvo
        Missile missileScript = projectile.GetComponentInChildren<Missile>();
        if (missileScript != null)
        {
            missileScript.SetTarget(target);
        }
        else
        {
            Debug.LogWarning("Script 'Missile' n�o encontrado no proj�til.");
        }

        // Envia a tag do atirador (opcional, caso esteja usando em `projectileHit`)
        projectileHit projectileHitScript = projectile.GetComponentInChildren<projectileHit>();
        if (projectileHitScript != null)
        {
            projectileHitScript.shooter(transform.parent.gameObject.tag);
        }

        yield break;
    }



    public void setTarget(GameObject _target)
    {
        target = _target;
    }
}
