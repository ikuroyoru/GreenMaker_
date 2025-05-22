using UnityEngine;

public class SkillManager : MonoBehaviour
{

    private longAttack longAttackScript;
    private defaultAttack defaculAttackScript;
    private collect collectScript;
    private p_Shield shieldScript;

    private bool activatedSkill;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaculAttackScript = GetComponent<defaultAttack>();
        longAttackScript = GetComponent<longAttack>();
        collectScript = GetComponent<collect>();
        shieldScript = GetComponent<p_Shield>();

        activatedSkill = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activatedSkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                defaculAttackScript.Shoot();
                activatedSkill = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                longAttackScript.Activate();
                activatedSkill = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // Código que será executado ao pressionar a tecla "1"
                activatedSkill = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                shieldScript.Activate();
                activatedSkill = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                collectScript.Activate();
                activatedSkill = true;
            }
        }
    }

    public void skillStatus(bool status)
    {
        activatedSkill = status;
        Debug.Log("Nenhuma Skill Ativa");
    }
}
