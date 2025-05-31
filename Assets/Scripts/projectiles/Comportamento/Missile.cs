using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target;
    private projectileStats projectileStatsScript;

    void Start()
    {
        projectileStatsScript = GetComponent<projectileStats>();
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Move na direção do alvo
        transform.parent.position += direction * projectileStatsScript.speed * Time.deltaTime;

        // Alinha a rotação com a direção
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;

        string alvo = target.tag;

        /*
        if (alvo != null)
        {
            Debug.Log("O alvo é: " + alvo);
        }
        else Debug.Log("Sem alvo definido");
        */
    }
}


