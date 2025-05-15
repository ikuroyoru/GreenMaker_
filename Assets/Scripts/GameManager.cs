using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Inst�ncia do GameManager
    private Materials materialManager; // Refer�ncia ao EnemyManager

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  // Atribui a inst�ncia
            DontDestroyOnLoad(gameObject);  // N�o destruir o GameManager entre as cenas
        }
        else
        {
            Destroy(gameObject);  // Se j� houver uma inst�ncia, destrua este objeto
        }
    }

    void Start()
    {
        materialManager = GetComponent<Materials>();
    }

    // M�todo para acessar o GameManager
    public void SomeGameFunction()
    {
        Debug.Log("Fun��o chamada pelo GameManager");
    }
}
