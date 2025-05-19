using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D body;
    private Vector2 movement;
    private Transform playerTransform;

    private void Awake()
    {
        // O script está no filho "Script", então pegamos o pai real ("Player")
        playerTransform = transform.parent;
        body = playerTransform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        FlipPlayer(movement.x);
    }

    private void FixedUpdate()
    {
        body.linearVelocity = movement * speed;
    }

    private void FlipPlayer(float directionX)
    {
        if (Mathf.Abs(directionX) > 0.01f)
        {
            Vector3 scale = playerTransform.localScale;
            scale.x = Mathf.Sign(directionX) * Mathf.Abs(scale.x);
            playerTransform.localScale = scale;
        }
    }
}
