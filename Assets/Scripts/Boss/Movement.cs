using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FollowPlayerWithDistance : MonoBehaviour
{
    public float speed = 5f;
    public float minDistance = 1.5f;

    private Transform target;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (!isPlayerInRange || target == null)
            return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > minDistance)
        {
            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    // Detecta entrada do player na trigger (círculo do boss)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            isPlayerInRange = true;
            // Debug.Log("PLAYER ENTROU");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Atualiza a referência, se necessário
            target = other.transform;

            // Garante que isPlayerInRange esteja ativo (caso entre por Stay, sem passar por Enter)
            isPlayerInRange = true;

            // Debug.Log("PLAYER CONTINUA DENTRO");
        }
    }


    // Detecta saída do player da trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            target = null;
            // Debug.Log("PLAYER SAIU");
        }
    }
}
