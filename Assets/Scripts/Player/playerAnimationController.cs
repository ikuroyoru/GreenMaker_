using UnityEngine;

public class playerAnimationController : MonoBehaviour
{
    [Header("Sprites Andando")]
    public Sprite walkFrente;
    public Sprite walkCostas;
    public Sprite walkLado;

    [Header("Sprites Atacando")]
    public Sprite attackFrente;
    public Sprite attackCostas;
    public Sprite attackLado;

    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        UpdateSpriteWithMouse(Input.GetMouseButton(0)); // Exemplo: ataca se clicar com o bot�o esquerdo
    }

    public void UpdateSpriteWithMouse(bool isAttacking)
    {
        // Pega a posi��o do mouse em coordenadas de mundo
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 playerPos = transform.position;
        Vector3 direction = mouseWorldPos - playerPos;
        float angle = Vector2.SignedAngle(Vector2.up, direction);

        // Zera o flip
        spriteRenderer.flipX = false;

        if (Mathf.Abs(angle) < 45f)
        {
            // Olhando para tr�s (costas)
            spriteRenderer.sprite = isAttacking ? attackCostas : walkCostas;
        }
        else if (Mathf.Abs(angle) > 135f)
        {
            // Olhando para frente
            spriteRenderer.sprite = isAttacking ? attackFrente : walkFrente;
        }
        else
        {
            // Olhando para o lado
            spriteRenderer.sprite = isAttacking ? attackLado : walkLado;

            // Se o mouse estiver � esquerda, espelha o sprite
            if (angle > 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
