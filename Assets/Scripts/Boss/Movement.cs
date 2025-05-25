using UnityEngine;

public class FollowPlayerWithDistance : MonoBehaviour
{
    public float speed = 5f;
    public float minDistance = 1.5f;
    public float maxDistance = 4f;
    public float returnSpeed = 3f;
    private float maxSpeed;

    private Transform target;
    private bool isPlayerInRange = false;
    private Vector3 initialPosition;
    private Rigidbody2D rb;

    private BossAnimationController bossAnimator; // ADICIONADO
    private GameObject currentPlayer;
    void Awake()
    {
        maxSpeed = speed;

        bossAnimator = GetComponentInParent<BossAnimationController>(); // ADICIONADO

        if (transform.parent == null)
        {
            Debug.LogError("Este objeto precisa ser filho de um objeto com Rigidbody2D e Collider2D.");
            return;
        }

        rb = transform.parent.GetComponent<Rigidbody2D>();
       
        if (rb == null)
            Debug.LogError("Rigidbody2D não encontrado no objeto pai.");

        if (transform.GetComponent<Collider2D>() == null)
            Debug.LogError("Collider2D não encontrado.");

            initialPosition = transform.parent.position;
    }


    void FixedUpdate()
    {
        if (isPlayerInRange && target != null)
        {
            float distance = Vector2.Distance(transform.parent.position, target.position);

            if (distance > maxDistance)
            {
                MoveTowards(target.position, speed);
            }
            // Entre min e max, o boss só "olha", você pode adicionar algo se quiser
        }
        else
        {
            float distanceToOrigin = Vector2.Distance(transform.parent.position, initialPosition);
            if (distanceToOrigin > 0.1f)
            {
                MoveTowards(initialPosition, returnSpeed);
            }
        }

        if(currentPlayer != null)
        {
            bossAnimator.UpdateSprite(transform.position, currentPlayer.transform.position, false);
        }
        
    }

    private void MoveTowards(Vector3 targetPosition, float moveSpeed)
    {
        Vector2 direction = ((Vector2)targetPosition - rb.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            isPlayerInRange = true;
            currentPlayer = other.gameObject;
            // Debug.LogWarning("Player Entrou");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            isPlayerInRange = true;
            currentPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            target = null;
            currentPlayer = null;
        }
    }

    public void updateSpeed(float amount)
    {
        speed += amount;
        Debug.Log("Speed: " + speed);

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
    }
}
