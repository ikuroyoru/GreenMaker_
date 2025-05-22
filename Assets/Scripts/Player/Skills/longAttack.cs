using System.Collections;
using UnityEngine;

public class longAttack : MonoBehaviour
{
    public float attackRange = 5f;
    public float fireCooldown = 10f;
    public float skillCooldown = 10f;

    private bool isShooting = false;
    
    private PlayerMovement movementScript;
    private playerAnimationController animatorScript;
    float defaultSpeed;

    

    void Awake()
    {
        movementScript = GetComponent<PlayerMovement>();
        animatorScript = GetComponent<playerAnimationController>();
        defaultSpeed = movementScript.speed;
    }

    public void Activate()
    {
            Debug.LogWarning("Skill de Longo Alcance Ativada");
            isShooting = !isShooting;
            updateSpeed();
    }

    void updateSpeed()
    {
        movementScript.updateMovement(isShooting);
    }


}
