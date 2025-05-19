using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FollowPlayerWithDistance : MonoBehaviour
{
    public float speed = 5f;
    public float minDistance = 1.5f;
    public float maxDistance = 4f;
    public float returnSpeed = 3f;

    [Header("Sprites")]
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite sideSprite;

    private Transform target;
    private bool isPlayerInRange = false;
    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        // Busca o SpriteRenderer do irmão "Sprite"
        var parent = transform;
        if (parent == null)
        {
            Debug.LogError("O objeto Boss deve ter um pai para encontrar o Sprite irmão.");
            return;
        }

        var spriteTransform = parent.Find("Sprite");
        if (spriteTransform == null)
        {
            Debug.LogError("Objeto 'Sprite' não encontrado como irmão no mesmo nível do Boss.");
            return;
        }

        spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogWarning("SpriteRenderer não encontrado no objeto 'Sprite'.");
    }

    void FixedUpdate()
    {
        if (isPlayerInRange && target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance > maxDistance)
            {
                MoveTowards(target.position, speed);
            }
            else if (distance < minDistance)
            {
                // Aqui você pode colocar comportamento de ataque, se quiser
                UpdateSprite(Vector2.zero); // parado
            }
            else
            {
                LookAtTarget(target.position);
                UpdateSprite(target.position - transform.position);
            }
        }
        else
        {
            float distanceToOrigin = Vector2.Distance(transform.position, initialPosition);
            if (distanceToOrigin > 0.1f)
            {
                MoveTowards(initialPosition, returnSpeed);
            }
            else
            {
                UpdateSprite(Vector2.zero); // parado
            }
        }
    }

    private void MoveTowards(Vector3 targetPosition, float moveSpeed)
    {
        Vector2 direction = ((Vector2)targetPosition - rb.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        LookAtTarget(targetPosition);
        UpdateSprite(direction);
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        float directionX = targetPosition.x - transform.position.x;

        if (Mathf.Abs(directionX) > 0.01f && spriteRenderer != null)
        {
            Vector3 scale = spriteRenderer.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(directionX);
            spriteRenderer.transform.localScale = scale;
        }
    }

    private void UpdateSprite(Vector2 dir)
    {
        if (spriteRenderer == null)
            return;

        if (dir == Vector2.zero)
            return; // evita troca desnecessária

        if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
        {
            spriteRenderer.sprite = (dir.y > 0) ? backSprite : frontSprite;
        }
        else
        {
            spriteRenderer.sprite = sideSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            isPlayerInRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            target = null;
        }
    }
}
