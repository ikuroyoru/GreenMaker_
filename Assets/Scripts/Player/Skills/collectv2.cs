using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class collectv2 : MonoBehaviour
{
    private int damage; // dano inflingido ao coletável. [ Geralmente valores baixos ]
    private float collectTimer; // tempo de coleta para inflingir "dano" ao colletável
    private float batteryCost;  // bateria gasta por tempo de coleta

    private bool verify;

    private GameObject collectable;
    private SkillManager skillManagerScript;
    private trash collectableScript;
    private p_Battery batteryScript;

    [SerializeField] private GameObject uiPanel;
    private collecting UIcollectScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        batteryScript = GetComponent<p_Battery>();
        skillManagerScript = SkillManager.Instance;
        ApplySkillLevel();

        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("collectable"))
        {
            verify = true;
            Debug.Log("Objeto coletável detectado.");
        }
        else verify = false;
    }


    public void Activate()
    {
        if (verify)
        {
            skillManagerScript.skillStatus(true);
            StartCoroutine(collecting());
        }
        else
        {
            skillManagerScript.skillStatus(false);
            Debug.Log("Nenhum coletavel próximo");
            return;
        }
    }

    public IEnumerator collecting()
    {
        UIcollectScript = uiPanel.GetComponent<collecting>();

        if (UIcollectScript != null)
        {
            uiPanel.SetActive(true);
            chargeUI(collectableScript.currentHP());
            UIcollectScript.UpdateTimer(collectTimer); // Começa com valor cheio
        }
        else
        {
            Debug.Log("Script da UI de coleta NÃO ENCONTRADO");
            yield break;
        }

        GameObject player = transform.root.gameObject;

        while (verify && collectableScript.currentHP() > 0)
        {
            float timer = 0f;

            while (timer < collectTimer)
            {
                if (!verify) break;

                timer += Time.deltaTime;
                float remaining = Mathf.Clamp(collectTimer - timer, 0f, collectTimer); // Evita valores negativos
                UIcollectScript.UpdateTimer(remaining);

                yield return null;
            }

            if (!verify) break;

            collectableScript.TakeDamage(damage, player);
            batteryScript.UseAbility(batteryCost);

            int _hpC = collectableScript.currentHP();
            chargeUI(_hpC);
            UIcollectScript.UpdateTimer(collectTimer); // Reseta o timer para o valor cheio
        }

        skillManagerScript.skillStatus(false);
        uiPanel.SetActive(false);
        yield break;
    }




    private void ApplySkillLevel()
    {
        var stats = SkillManager.Instance.GetSkillStats("collect");

        collectTimer = stats.duration;
        batteryCost = stats.charge;
        damage = stats.damage;
    }

    public void verifyStatus(GameObject _collectable, bool state)
    {
        verify = state;
        collectable = _collectable;

        collectableScript = collectable.GetComponent<trash>();

        // Debug.Log($"[Collect] Status: {(verify ? "ATIVO" : "INATIVO")}, objeto: {_collectable.name}");
    }

    void chargeUI(int _hpC)
    {
        UIcollectScript.UpdateUI(_hpC, collectableScript.maxHealth, collectTimer, collectableScript.collectableName);
    }

}
