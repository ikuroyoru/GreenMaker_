using UnityEngine;
using System.Collections;

public class collect : MonoBehaviour
{
    public GameObject areaVisualPrefab; // ← arraste o prefab com SpriteRenderer aqui no Inspector

    [SerializeField] private float scanRadius = 5f;
    [SerializeField] private float skillTimer = 5f;
    [SerializeField] private float damage = 25f;
    private float damagePerSecond;
    private score scoreScript;
    private SkillManager skillManagerScript;

    private void Start()
    {
        scoreScript = GetComponent<score>();
        damagePerSecond = damage / skillTimer;
        skillManagerScript = GetComponent<SkillManager>();
    }

    public void Activate()
    {
        StartCoroutine(collectSkill());
    }

    public IEnumerator collectSkill()
    {
        Debug.LogWarning("Skill de Coleta Ativada");

        // Instancia o círculo de visualização
        GameObject areaVisual = null;
        if (areaVisualPrefab != null)
        {
            areaVisual = Instantiate(areaVisualPrefab, transform.position, Quaternion.identity);
            areaVisual.transform.localScale = new Vector3(scanRadius * 2, scanRadius * 2, 1); // Ajusta o tamanho com base no raio
            areaVisual.transform.parent = transform; // Faz com que siga o jogador, se necessário
        }

        float count = 0f;
        float points = damagePerSecond;

        while (count < skillTimer)
        {
            Collider2D[] lixos = Physics2D.OverlapCircleAll(transform.position, scanRadius);

            if (lixos != null)
            {
                foreach (Collider2D lixo in lixos)
                {
                    if (lixo.CompareTag("trash"))
                    {
                        trash trashScript = lixo.GetComponent<trash>();
                        GameObject player = transform.root.gameObject;

                        if (trashScript != null)
                        {
                            trashScript.TakeDamage(damagePerSecond, player);
                            scoreScript.updateGeneralPoints(points);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(1f);
            count += 1;
        }

        // Destroi o círculo de visualização ao final da skill
        if (areaVisual != null)
        {
            Destroy(areaVisual);
        }

        skillManagerScript.skillStatus(false);
    }
}
