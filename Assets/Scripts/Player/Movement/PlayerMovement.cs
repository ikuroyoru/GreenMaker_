using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D body;
    private Vector2 movement;

    private void Awake()
    {
        body = GetComponentInParent<Rigidbody2D>();
    }


    private void Update()
    {
        // Captura o input do jogador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normaliza o vetor para evitar andar mais rápido na diagonal
        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        // Move o personagem
        body.linearVelocity = movement * speed;
    }
}
