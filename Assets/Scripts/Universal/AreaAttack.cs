using System.Collections;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    public Vector2 areaSize = new Vector2(3f, 3f);
    public LayerMask playerLayer;
    public int damage = 10;
    public Transform attackCenter;
    public GameObject areaIndicatorPrefab; // Prefab com contorno (ex: um quad com SpriteRenderer)
    public int attackRange = 5;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExecuteAreaAttack());
        }
    }

    IEnumerator ExecuteAreaAttack()
    {
        // Instancia o contorno branco
        GameObject indicator = Instantiate(areaIndicatorPrefab, attackCenter.position, Quaternion.identity);
        indicator.transform.localScale = areaSize;

        SpriteRenderer sr = indicator.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.white;
        }

        // Aguarda 2 segundos com contorno branco
        yield return new WaitForSeconds(2f);

        // Muda para vermelho
        if (sr != null)
        {
            sr.color = Color.red;
        }

        // Aplica o dano
        PerformAreaDamage();

        // Aguarda 1 segundo e destrói o indicador
        yield return new WaitForSeconds(1f);
        Destroy(indicator);
    }

    void PerformAreaDamage()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackCenter.position, areaSize, 0f, playerLayer);

        foreach (Collider2D hit in hits)
        {
            Debug.Log("Player atingido: " + hit.name);

            Transform scriptChild = hit.transform.Find("Script");
            if (scriptChild != null)
            {
                hp enemy = scriptChild.GetComponent<hp>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                }
            }
        }
    }
}
