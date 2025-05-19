using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    [Header("Sprites")]
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite sideSprite;

    private Rigidbody2D body;
    private Vector2 movement;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // O script está no filho "Script", então pegamos o pai real ("Player")
        playerTransform = transform.parent;

        // Pegamos o Rigidbody2D do pai
        body = playerTransform.GetComponent<Rigidbody2D>();

        // Pegamos o SpriteRenderer do filho chamado "Sprite"
        spriteRenderer = playerTransform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        UpdateSprite(movement);
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
            Vector3 scale = spriteRenderer.transform.localScale;
            scale.x = Mathf.Sign(directionX) * Mathf.Abs(scale.x);
            spriteRenderer.transform.localScale = scale;
        }
    }

    private void UpdateSprite(Vector2 dir)
    {
        if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
        {
            if (dir.y > 0)
                spriteRenderer.sprite = backSprite;
            else if (dir.y < 0)
                spriteRenderer.sprite = frontSprite;
        }
        else if (Mathf.Abs(dir.x) > 0)
        {
            spriteRenderer.sprite = sideSprite;
        }
    }
}
