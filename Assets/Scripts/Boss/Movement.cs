using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FollowPlayerWithDistance : MonoBehaviour
{
    public float speed = 5f;
    public float minDistance = 1.5f;       // Distância mínima até o player
    public float maxDistance = 4f;         // Distância máxima antes de começar a seguir
    public float returnSpeed = 3f;         // Velocidade ao retornar para a origem

    private Transform target;
    private bool isPlayerInRange = false;
    private Vector3 initialPosition;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (isPlayerInRange && target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance > maxDistance) // Muito longe, persegue
            {
                MoveTowards(target.position, speed);
            }
            else if (distance < minDistance) // Muito perto, para
            {
                // Pode adicionar comportamento de ataque aqui
            }
            else
            {
                // Dentro da zona ideal: observa o jogador
                LookAtTarget(target.position);
            }
        }
        else
        {
            // Player saiu, retorna para a posição inicial
            float distanceToOrigin = Vector2.Distance(transform.position, initialPosition);
            if (distanceToOrigin > 0.1f)
            {
                MoveTowards(initialPosition, returnSpeed);
            }
        }
    }

    private void MoveTowards(Vector3 targetPosition, float moveSpeed)
    {
        Vector2 direction = ((Vector2)targetPosition - rb.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        LookAtTarget(targetPosition);
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        float directionX = targetPosition.x - transform.position.x;

        if (Mathf.Abs(directionX) > 0.01f) // Evita flip constante em zero
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(directionX);
            transform.localScale = scale;
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
