using UnityEngine;

public class b_Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab; // Prefab do escudo
    private GameObject activeShield1;
    private GameObject activeShield2;
    private GameObject activeShield3;

    private bool Activated;

    private float rotationSpeed = 360f;
    private int maxHP = 30;
    private int currentHP;
    private float Activation = 0.5f; // Porcentagem da vida do boss em que o escudo é ativado
    
    void Start()
    {
        Activated = false;

        if (transform.parent != null)
        {
            currentHP = maxHP;
        }
    }

    void Update()
    {
        float bossHP = GetComponent<hp>().currentHP;
        float bossMaxHP = GetComponent<hp>().maxHP;
        float shieldActivation = bossMaxHP * Activation; // Define o valor da ativação do escudo para 40% da vida do boss, ou seja, assim que a vida chegar a 40%, o escudo é ativado.

        if (bossHP <= shieldActivation && Activated == false && transform.parent.tag == "Boss" || Input.GetKeyDown(KeyCode.R)) // Condição de ativação do escudo
        {

            activeShield1 = Instantiate(shieldPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f), transform);
            activeShield2 = Instantiate(shieldPrefab, transform.position, Quaternion.Euler(0f, 0f, 120f), transform);
            activeShield3 = Instantiate(shieldPrefab, transform.position, Quaternion.Euler(0f, 0f, 240f), transform);

            Activated = true;
        }


        if (activeShield1 != null) activeShield1.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        if (activeShield2 != null) activeShield2.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        if (activeShield3 != null) activeShield3.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);


    }

    public void TakeDamage(int damage)
    {
        if (activeShield1 != null)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                Destroy(activeShield1);
                activeShield1 = null;
                Debug.Log("Escudo destruído");
            }
        }
    }
}
