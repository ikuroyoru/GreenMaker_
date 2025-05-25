using UnityEngine;
using UnityEngine.UI;

public class selectEnemy : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private GameObject triggerIconPrefab;
    private GameObject triggerIcon;

    private longAttack longAttackScript;

    [SerializeField] private Slider hpBar;
    [SerializeField] private BossHealthUI enemyUIScript;

    private hp selectedBossHP;
    private b_Info selectedBossInfo;

    void Start()
    {
        longAttackScript = GetComponent<longAttack>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DetectBossUnderMouse();
        }

        if (selectedBossHP != null)
        {
            enemyUIScript.UpdateUI();
        }
    }

    void DetectBossUnderMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null)
        {
            // Clicou fora de inimigo
            selectedBossHP = null;
            enemyUIScript.Hide();
            if (triggerIcon != null)
            {
                Destroy(triggerIcon);
                triggerIcon = null;
            }
            return;
        }

        GameObject hitObject = hit.collider.gameObject;

        if (hitObject.CompareTag("Boss"))
        {
            target = hitObject;
        }
        else if (hitObject.CompareTag("Select"))
        {
            target = hitObject.transform.parent.gameObject;
        }
        else
        {
            return;
        }

        Debug.Log("Boss selecionado: " + target.name);

        Transform scriptChild = target.transform.Find("Script");

        if (scriptChild != null)
        {
            selectedBossHP = scriptChild.GetComponent<hp>();
            selectedBossInfo = scriptChild.GetComponent<b_Info>();
            enemyUIScript.SetBoss(selectedBossHP, selectedBossInfo);
            selectedBossHP.setEnemyUI(enemyUIScript);
        }

        Collider2D bossCollider = target.GetComponent<Collider2D>();
        Vector3 center = bossCollider != null ? bossCollider.bounds.center : target.transform.position;

        if (triggerIcon != null)
        {
            Destroy(triggerIcon);
        }
        triggerIcon = Instantiate(triggerIconPrefab, center, Quaternion.identity, target.transform);

        longAttackScript.setTarget(target);
    }
}
