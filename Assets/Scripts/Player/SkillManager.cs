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
            if (Input.GetMouseButton(0))
            {
                defaculAttackScript.Shoot();
                skillStatus(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                longAttackScript.Activate();
                skillStatus(true);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // Código que será executado ao pressionar a tecla "2"
                skillStatus(true);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                shieldScript.Activate();
                skillStatus(true);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                collectScript.Activate();
                skillStatus(true);
            }
        }
    }

    public void skillStatus(bool status)
    {
        activatedSkill = status; // Atualiza o status de execução de skill, caso a condição esteja ativada, não será possível executar outras skills
        Debug.Log("Executando skill: " + activatedSkill);
    }
}
