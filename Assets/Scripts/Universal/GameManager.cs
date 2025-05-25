using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Instância do GameManager
    private Materials materialManager; // Referência ao EnemyManager

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  // Atribui a instância
            DontDestroyOnLoad(gameObject);  // Não destruir o GameManager entre as cenas
        }
        else
        {
            Destroy(gameObject);  // Se já houver uma instância, destrua este objeto
        }
    }

    void Start()
    {
        materialManager = GetComponent<Materials>();
    }

    // Método para acessar o GameManager
    public void SomeGameFunction()
    {
        Debug.Log("Função chamada pelo GameManager");
    }
}
