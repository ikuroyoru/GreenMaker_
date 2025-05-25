using System.Collections;
using UnityEngine;

public class b_Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab; // Prefab do escudo
    private bool Activated;
    private float Activation = 0.5f; // Porcentagem da vida do boss em que o escudo é ativado

    void Start()
    {
        Activated = false;
    }

    void Update()
    {
        float bossHP = GetComponent<hp>().currentHP;
        float bossMaxHP = GetComponent<hp>().maxHP;
        float shieldActivation = bossMaxHP * Activation; // Define o valor da ativação do escudo para 50% da vida do boss, ou seja, assim que a vida chegar a 40%, o escudo é ativado.

        if (bossHP <= shieldActivation && !Activated && transform.parent.tag == "Boss" || Input.GetKeyDown(KeyCode.R)) // Condição de ativação do escudo
        {
            StartCoroutine(applyShield());
            Activated = true;
        }
    }

    public IEnumerator applyShield()
    {
        CreateShieldAtAngle(0f);
        CreateShieldAtAngle(180f);

        yield break;
    }

    private void CreateShieldAtAngle(float angle)
    {
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.Euler(0f, 0f, angle), transform);
        ShieldPart shieldScript = shield.GetComponentInChildren<ShieldPart>();

        if (shieldScript != null)
        {
            shieldScript.setTag(transform.parent.tag);
        }
        else
        {
            Debug.LogWarning("ShieldPart não encontrado no escudo instanciado.");
        }
    }
}