using UnityEngine;

public class BossAnimationController : MonoBehaviour
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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(Vector3 bossPosition, Vector3 playerPosition, bool isAttacking)
    {
        Vector3 directionToPlayer = playerPosition - bossPosition;
        float angle = Vector2.SignedAngle(Vector2.up, directionToPlayer);

        // Zera o flip
        spriteRenderer.flipX = false;

        if (Mathf.Abs(angle) < 45f)
        {
            // Boss olhando para trás (costas)
            spriteRenderer.sprite = isAttacking ? attackCostas : walkCostas;
        }
        else if (Mathf.Abs(angle) > 135f)
        {
            // Boss olhando para frente
            spriteRenderer.sprite = isAttacking ? attackFrente : walkFrente;
        }
        else
        {
            // Boss olhando para o lado
            spriteRenderer.sprite = isAttacking ? attackLado : walkLado;

            // Se o jogador estiver à esquerda (ângulo negativo), espelha o sprite
            if (angle > 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

}
