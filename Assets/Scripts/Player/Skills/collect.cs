using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.PlayerSettings;

public class collect : MonoBehaviour
{
    private float scanRadius = 5f;  // Tornando o raio visível e editável no Inspector
    private float skillTimer = 5f;
    private float damage = 25f;
    private float damagePerSecond;
    private score scoreScript;

    private SkillManager skillManagerScript;

    private void Start()
    {
        scoreScript = GetComponent<score>();
        damagePerSecond = damage / skillTimer;

        skillManagerScript = GetComponent<SkillManager>();
    }


    void Update()
    {

    }

    public void Activate()
    {
        StartCoroutine(collectSkill());
    }

    public IEnumerator collectSkill()
    {
        Debug.LogWarning("Skill de Coleta Ativada");

        float count = 0f;
        float points = damagePerSecond;

        while (count < skillTimer)
        {
            Collider2D[]  lixos = Physics2D.OverlapCircleAll(transform.position, scanRadius);

            if (lixos != null)
            {
                foreach (Collider2D lixo in lixos)
                {
                    if (lixo.CompareTag("trash"))  // Verifica se o objeto tem a tag "Trash", que é o LIXO
                      // LÓGICA DE INTERAÇÃO COM O LIXO
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

        skillManagerScript.skillStatus(false);

        yield break;
    }


    /* Este método é chamado apenas no editor, para desenhar o gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;  // Cor do gizmo
        Gizmos.DrawWireSphere(transform.position, scanRadius);  // Desenha o círculo de detecção
    }
    */


}
