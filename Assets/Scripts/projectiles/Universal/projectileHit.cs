using UnityEngine;

public class projectileHit : MonoBehaviour
{
    private projectileStats projectileStatsScript;
    private string shooterTag;
    private float _damage;
    void Start()
    {
        projectileStatsScript = GetComponent<projectileStats>();
        _damage = projectileStatsScript.damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Transform scriptChild = other.transform.Find("Script");

        if (shooterTag == "Player")
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                hp hpScript = scriptChild.GetComponent<hp>();

                if (hpScript != null)
                {
                    hpScript.takeDamage(_damage);
                }
                destroyMissile();
            }

            if (other.CompareTag("Shield"))
            {
                shieldHP hpScript = scriptChild.GetComponent<shieldHP>();

                if (hpScript != null)
                {
                    hpScript.takeDamage(_damage);
                    Debug.Log("Dano causado ao escudo: " + _damage);
                }
                destroyMissile();
            }
        }

        else
        {
            if (other.CompareTag("Player"))
            {
                hp hpScript = scriptChild.GetComponent<hp>();

                if (hpScript != null)
                {
                    hpScript.takeDamage(_damage);
                }
            }
            if (other.CompareTag("Shield"))
            {
                shieldHP hpScript = scriptChild.GetComponent<shieldHP>();

                if (hpScript != null)
                {
                    hpScript.takeDamage(_damage);
                }
            }

            destroyMissile();
        }
    }

    public void shooter(string tag)
    {
        shooterTag = tag;
        Debug.Log("Atirador: " + shooterTag);
    }

    void destroyMissile()
    {
        GameObject parent = transform.parent.gameObject;
        Destroy(parent); // Destrói o objeto do missil, e não apenas o objeto filho "Script"
    }
}
