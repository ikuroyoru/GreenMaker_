using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Instância do GameManager
    private Materials materialManager; // Referência ao EnemyManager

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
