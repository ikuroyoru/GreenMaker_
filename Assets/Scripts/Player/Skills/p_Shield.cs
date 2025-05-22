using UnityEngine;

public class p_Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab; // Prefab do escudo
    private GameObject activeShield;

    private int shieldMaxHP = 30;
    private int currentHP;

    private SkillManager skillManagerScript;

    void Start()
    {
        currentHP = shieldMaxHP;

        skillManagerScript = GetComponent<SkillManager>();
    }

    void Update()
    {
        
    }

    public void Activate()
    {
        Debug.LogWarning("Skill do Escudo Ativada");

        if (activeShield == null && transform.parent.tag == "Player")
        {
            activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        }
        else
        {
            Destroy(activeShield); // Alterna/desativa se já estiver ativo
            activeShield = null;
            skillManagerScript.skillStatus(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (activeShield != null)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                Destroy(activeShield);
                activeShield = null;
                Debug.Log("Escudo destruído");

                skillManagerScript.skillStatus(false);
            }
        }
    }
}
