using System.Collections;
using UnityEngine;

public class b_longAttack : MonoBehaviour
{
    // VARIAVEIS QUE PODEM SE ALTERAR QUANDO O JOGADOR EVOLUIR DE NÍVEL
    public float attackRange = 5f;
    public float fireCooldown = 10f;


    public LayerMask playerLayer;
    public GameObject missilePrefab;

    private bool playerInside = false;
    private GameObject currentPlayer;
    private bool isShooting = false;

    private GameObject trigger;
    [SerializeField] GameObject triggerPrefab;

    private float animationTimer = 5f; //TEMPO FIXO, É PARA SIMULAR O TEMPO DE ANIMAÇÃO QUANDO O BOSS FOR ATIRAR O MISSIL, ELE PODE SER ALTERADO, MAS NÃO MUDA CONFORME O JOGADOR EVOLUI.

    void Update()
    {
        Collider2D playerDetector = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (playerDetector != null && !playerInside)
        {
            playerInside = true;
            currentPlayer = playerDetector.gameObject;
            Debug.LogWarning("Player Triggered: " + playerInside);
            trigger = Instantiate(triggerPrefab, currentPlayer.transform);
        }
        else if (playerDetector == null && playerInside)
        {
            playerInside = false;
            currentPlayer = null;
            Debug.LogWarning("Player Triggered: " + playerInside);
            Destroy(trigger);
            trigger = null;
        }

        if (playerInside && !isShooting)// Só atira se o cooldown não estiver ativo e o Player estiver no alcance
        {
            StartCoroutine(Shoot()); 
        }
    }

    private IEnumerator Shoot()
    {
        if (isShooting) yield break; // Garante que não rode múltiplas vezes
        isShooting = true;

        float count = 0;
        float interval = animationTimer / 10f;

        bool triggerActivation = false;
        if (trigger != null && trigger.GetComponent<Renderer>() != null)
            triggerActivation = trigger.GetComponent<Renderer>().enabled;

        Debug.Log("Travando Mira");

        while (count < animationTimer)
        {
            yield return new WaitForSecondsRealtime(interval);
            count += interval;
            Debug.Log("PI");

            if (!playerInside || trigger == null || currentPlayer == null)
            {
                Debug.Log("Alvo perdido");
                isShooting = false;
                yield break;
            }

            // Pisca trigger
            Renderer triggerRenderer = trigger.GetComponent<Renderer>();
            if (triggerRenderer != null)
            {
                triggerActivation = !triggerActivation;
                triggerRenderer.enabled = triggerActivation;
            }
        }

        Debug.Log("PIIIIIIIIIIIIII");

        if (trigger != null)
        {
            Renderer triggerRenderer = trigger.GetComponent<Renderer>();
            if (triggerRenderer != null)
                triggerRenderer.enabled = true;
        }

        if (currentPlayer != null)
        {
            ShootMissile();
            Debug.Log("Míssil Disparado");
        }
        else
        {
            Debug.Log("Alvo perdido");
            isShooting = false;
            yield break;
        }

        int cooldownCount = 0;

        while (cooldownCount < fireCooldown) // Espera antes do próximo míssil
        {
            yield return new WaitForSecondsRealtime(1f);
            cooldownCount++;
            Debug.Log("Cooldown Ativo: " + cooldownCount + " Segundos");
        }

        isShooting = false;
        Debug.LogWarning("Atirando: " + isShooting);
    }


    private void ShootMissile()
    {
        if (missilePrefab != null && currentPlayer != null)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);

            // Corrige: chama SetTarget com o GameObject
            Missile missileScript = missile.GetComponent<Missile>();
            if (missileScript != null)
            {
                missileScript.SetTarget(currentPlayer);
            }

            // Debug.Log("Disparou míssil!");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }



}
