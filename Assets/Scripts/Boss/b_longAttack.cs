using System.Collections;
using UnityEngine;

public class b_longAttack : MonoBehaviour
{
    public float attackRange = 5f;
    public float fireCooldown = 10f;

    public LayerMask playerLayer;
    public GameObject missilePrefab;

    private bool playerInside = false;
    private GameObject currentPlayer;
    private bool isShooting = false;

    private GameObject trigger;
    [SerializeField] GameObject triggerPrefab;

    private float animationTimer = 5f;

    public float skillCooldown = 10f;
    private bool cooldownStatus;

    // REMOVIDO: Sprites e SpriteRenderer
    // public Sprite spriteFrente;
    // public Sprite spriteCostas;
    // public Sprite spriteLado;
    // private SpriteRenderer bossRenderer;

    private FollowPlayerWithDistance bossMovement;
    float defaultSpeed;

    private BossAnimationController bossAnimator; // ADICIONADO

    void Awake()
    {
        // bossRenderer = GetComponentInParent<SpriteRenderer>(); // REMOVIDO
        bossMovement = GetComponentInParent<FollowPlayerWithDistance>();
        bossAnimator = GetComponentInParent<BossAnimationController>(); // ADICIONADO
        defaultSpeed = bossMovement.speed;
        cooldownStatus = false;
    }

    void Update()
    {
        Collider2D playerDetector = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (playerDetector != null && !playerInside)
        {
            playerInside = true;
            currentPlayer = playerDetector.gameObject;
            trigger = Instantiate(triggerPrefab, currentPlayer.transform);
        }
        else if (playerDetector == null && playerInside)
        {
            playerInside = false;
            currentPlayer = null;
            Destroy(trigger);
            trigger = null;
            bossMovement.updateSpeed(defaultSpeed);
        }

        if (playerInside && !isShooting && !cooldownStatus)
        {
            isShooting = true;
            StartCoroutine(Shoot());
        }

        if (isShooting)
        {
            if (bossAnimator != null && currentPlayer != null) // ADICIONADO
            {
                bossAnimator.UpdateSprite(transform.position, currentPlayer.transform.position, true); // true = atirando
            }
        }
        else
        {
            if (bossAnimator != null && currentPlayer != null) // ADICIONADO
            {
                bossAnimator.UpdateSprite(transform.position, currentPlayer.transform.position, false); // true = atirando
            }
        }
    }


    private IEnumerator Shoot()
    {
        // Debug.Log("shoot? : " + isShooting);

        float count = 0;
        float interval = animationTimer / 10f;

        bossMovement.updateSpeed(-defaultSpeed);

        bool triggerActivation = false;
        if (trigger != null && trigger.GetComponent<Renderer>() != null)
            triggerActivation = trigger.GetComponent<Renderer>().enabled;

        while (count < animationTimer)
        {
            yield return new WaitForSecondsRealtime(interval);
            count += interval;

            if (!playerInside || trigger == null || currentPlayer == null)
            {
                isShooting = false;
                yield break;
            }

            Renderer triggerRenderer = trigger.GetComponent<Renderer>();
            if (triggerRenderer != null)
            {
                triggerActivation = !triggerActivation;
                triggerRenderer.enabled = triggerActivation;
            }
        }

        if (trigger != null)
        {
            Renderer triggerRenderer = trigger.GetComponent<Renderer>();
            if (triggerRenderer != null)
                triggerRenderer.enabled = true;
        }

        if (currentPlayer != null)
        {
            ShootMissile();
        }
        else
        {
            isShooting = false;
            yield break;
        }

        int cooldownCount = 0;
        while (cooldownCount < fireCooldown)
        {
            yield return new WaitForSecondsRealtime(1f);
            cooldownCount++;
        }

        StartCoroutine(cooldowndUpdate());
    }

    private IEnumerator cooldowndUpdate()
    {
        isShooting = false;
        cooldownStatus = true;

        float currentCooldown = 0;

        while (currentCooldown < skillCooldown)
        {
            yield return new WaitForSecondsRealtime(1f);
            currentCooldown += 1f;
        }

        cooldownStatus = false;
    }

    private void ShootMissile()
    {
        if (missilePrefab != null && currentPlayer != null)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);

            Missile missileScript = missile.GetComponentInChildren<Missile>();
            if (missileScript != null)
            {
                missileScript.SetTarget(currentPlayer);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
