using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody2D body;
    private Vector2 movement;
    private Transform playerTransform;

    private bool isShooting;
    public bool locked = false;

    private void Awake()
    {
        // O script está no filho "Script", então pegamos o pai real ("Player")
        playerTransform = transform.parent;

        // Pegamos o Rigidbody2D do pai
        body = playerTransform.GetComponent<Rigidbody2D>();

        isShooting = false;
    }

    private void Update()
    {
        if (InventoryUI.IsInventoryOpen)
        {
            movement.x = 0;
            movement.y = 0;
            return;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        
        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        if (locked || isShooting)
        {
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.linearVelocity = movement * speed;
        }
    }

    public void updateMovement(bool shooting)
    {
        isShooting = shooting;
    }
}
