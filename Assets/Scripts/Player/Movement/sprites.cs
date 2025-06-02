using UnityEngine;

public class sprites : MonoBehaviour
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
        spriteRenderer = transform.parent.Find("Sprite").GetComponent<SpriteRenderer>();

        mainCamera = Camera.main;
    }

    void Update()
    {
        if (InventoryUI.IsInventoryOpen) return;


        bool isAttacking = Input.GetMouseButton(0); // Exemplo: atacando se clicar com o botão esquerdo
        UpdateSpriteWithMouse(isAttacking);
    }

    void UpdateSpriteWithMouse(bool isAttacking)
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 playerPos = transform.position;
        Vector3 direction = mouseWorldPos - playerPos;

        float angle = Vector2.SignedAngle(Vector2.up, direction);
        spriteRenderer.flipX = false;

        if (Mathf.Abs(angle) < 45f)
        {
            // Costas
            spriteRenderer.sprite = isAttacking ? attackCostas : walkCostas;
        }
        else if (Mathf.Abs(angle) > 135f)
        {
            // Frente
            spriteRenderer.sprite = isAttacking ? attackFrente : walkFrente;
        }
        else
        {
            // Lado
            spriteRenderer.sprite = isAttacking ? attackLado : walkLado;

            if (angle > 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
