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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        batteryScript = GetComponent<p_Battery>();
        skillManagerScript = SkillManager.Instance;
        ApplySkillLevel();
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
        GameObject player = transform.root.gameObject;

        int collectable_HP = collectableScript.currentHP();
        Debug.Log("TRASH HP: " + collectable_HP);
        while (verify && collectable_HP > 0)
        {
            yield return new WaitForSeconds(collectTimer);
            collectableScript.TakeDamage(damage, player);
            batteryScript.UseAbility(batteryCost);
        }
        if (!verify)
        {
            skillManagerScript.skillStatus(false);
            Debug.Log("Verifiy: " + verify);
        }


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

}
