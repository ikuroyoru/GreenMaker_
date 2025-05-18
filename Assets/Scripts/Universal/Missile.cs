using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target;
    public float speed = 5f;
    [SerializeField] float damage = 20;

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Move na direção do alvo
        transform.position += direction * speed * Time.deltaTime;

        // Alinha a rotação com a direção
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            Transform scriptChild = other.transform.Find("Script");

            if (scriptChild != null)
            {
                hp hpScript = scriptChild.GetComponent<hp>();
                if (hpScript != null)
                {
                    hpScript.takeDamage(damage);
                }
            }

            // Debug.LogWarning("Colidiu com o Player");
            Destroy(gameObject);
        }
    }
}
