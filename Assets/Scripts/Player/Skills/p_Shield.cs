using UnityEngine;
using UnityEngine.UI;

public class p_Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab; // Prefab do escudo
    private GameObject activeShield;
    private SkillManager skillManagerScript;

    [SerializeField] public Slider slider;

    void Start()
    {
        skillManagerScript = GetComponent<SkillManager>();
    }

    void Update()
    {
        
    }

    public void Activate()
    {
        Debug.Log("Skill do Escudo Ativada");

        if (activeShield == null && transform.parent.tag == "Player")
        {
            if (transform.parent != null)
            {
                activeShield = Instantiate(shieldPrefab, transform.parent.position, Quaternion.identity, transform.parent);
                shieldHP shieldScript = activeShield.GetComponentInChildren<shieldHP>();

                if (shieldScript != null)
                {
                    Debug.Log("Passando slider...");
                    shieldScript.defUI(slider);
                    shieldScript.InitializeShield(); // <-- garante que os valores estão atualizados
                    shieldScript.getReference(skillManagerScript);
                }
                else
                {
                    Debug.LogWarning("shieldScript é null. Verifique se o prefab tem o script shieldHP.");
                }



                if (shieldScript != null) shieldScript.getReference(skillManagerScript);
                else Debug.Log("Sem referencias");
            }
        }
    }
}
