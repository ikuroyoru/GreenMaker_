using UnityEngine;

public class b_defaultGun : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    GameObject projectile;

    private float shotsPerMinute = 120;
    private float cooldown;
    private float currentCooldown;

    private bool activeCooldown;

    private void Start()
    {
        cooldown = 60 / shotsPerMinute; //1 minuto dividido pela quantidade de tiros por minuto, resultando no cooldown por tiro ( em segundos ).
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && activeCooldown == false)
        {
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            currentCooldown = cooldown;
            activeCooldown = true;
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                currentCooldown = 0;
                activeCooldown = false;
            }
        }
    }

}
