using System.Collections;
using UnityEngine;

public class b_longAttack : MonoBehaviour
{
    public int attackRange = 5;
    public LayerMask playerLayer; // Defina no Inspector o layer do player

    private bool playerInside = false;

    [SerializeField] private float triggerTimer = 3f;
    [SerializeField] private float cooldown = 10f;

    private bool isTriggerActivated = false;
    private bool isCooldownActivated = false;

    [SerializeField] private GameObject missilePrefab;
    private GameObject missile;

    private GameObject target; // Agora n�o � mais SerializeField

    private GameObject currentPlayer; // <- Armazena o jogador detectado


    void Start()
    {
        if (transform.parent != null)
        {
            target = transform.parent.gameObject;
        }
        else
        {
            Debug.LogError("b_longAttack: Objeto pai n�o encontrado. O target ser� nulo.");
        }
    }

    void Update()
    {
        Collider2D playerDetector = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (playerDetector != null && !playerInside && !isTriggerActivated && !isCooldownActivated)
        {
            playerInside = true;
            currentPlayer = playerDetector.gameObject; // <- Salva o jogador
            Debug.Log("Player entrou na �rea");
            StartCoroutine(TriggeringPlayer(currentPlayer));
        }
        else if (playerDetector == null && playerInside)
        {
            playerInside = false;
            currentPlayer = null; // <- Limpa a refer�ncia
            Debug.Log("Player saiu da �rea");
        }

    }

    IEnumerator TriggeringPlayer(GameObject player)
    {
        isTriggerActivated = true;

        yield return new WaitForSeconds(triggerTimer);

        isTriggerActivated = false;

        if (!playerInside)
            yield break;

        if (missilePrefab == null || target == null)
        {
            Debug.LogError("missilePrefab ou target n�o atribu�do!");
            yield break;
        }

        missile = Instantiate(missilePrefab, target.transform.position, Quaternion.identity, target.transform);

        Missile missileScript = missile.GetComponent<Missile>();
        if (missileScript != null)
        {
            missileScript.SetTarget(player); // O m�ssil persegue o jogador
        }

        isCooldownActivated = true;
        yield return new WaitForSeconds(cooldown);
        isCooldownActivated = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (currentPlayer != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(currentPlayer.transform.position, Vector3.one);
        }
    }

}
