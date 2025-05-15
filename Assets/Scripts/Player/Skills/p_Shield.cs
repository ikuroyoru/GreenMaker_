using UnityEngine;

public class p_Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab; // Prefab do escudo
    private GameObject activeShield;

    private int shieldMaxHP = 30;
    private int currentHP;

    void Start()
    {
        currentHP = shieldMaxHP;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (activeShield == null && transform.parent.tag == "Player")
            {
                activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
            }
            else
            {
                Destroy(activeShield); // Alterna/desativa se já estiver ativo
                activeShield = null;
            }
        }

        // if (activeShield != null) transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
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
            }
        }
    }
}
