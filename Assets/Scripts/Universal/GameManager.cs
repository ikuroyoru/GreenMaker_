using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Inst�ncia do GameManager
    private Materials materialManager; // Refer�ncia ao EnemyManager

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
