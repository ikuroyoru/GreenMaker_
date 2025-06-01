using UnityEngine;

public class projectileHit : MonoBehaviour
{
    private string shooterTag;
    private float damage;
    void Start()
    {

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
                    hpScript.takeDamage(damage);
                }
                destroyMissile();
            }

            if (other.CompareTag("Shield"))
            {
                shieldHP hpScript = scriptChild.GetComponent<shieldHP>();

                if (hpScript != null)
                {
                    hpScript.takeDamage(damage);
                    Debug.Log("Dano causado ao escudo: " + damage);
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
                    hpScript.takeDamage(damage);
                }
            }
            if (other.CompareTag("Shield"))
            {
                shieldHP hpScript = scriptChild.GetComponent<shieldHP>();

                if (hpScript != null)
                {
                    hpScript.takeDamage(damage);
                }
            }

            destroyMissile();
        }
    }

    public void shooter(string tag)
    {
        shooterTag = tag;
        // Debug.Log("Atirador: " + shooterTag);
    }

    public void setDamage(float _damage)
    {
        damage = _damage;
    }

    void destroyMissile()
    {
        GameObject parent = transform.parent.gameObject;
        Destroy(parent); // Destrói o objeto do missil, e não apenas o objeto filho "Script"
    }
}
