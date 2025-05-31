using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameOverReason
{
    None,
    BatteryDepleted,
    Destroyed
}

public class GameOverManager : MonoBehaviour
{
    public bool triggerGameOver;
    private bool hasGameOverTriggered;

    public GameOverReason reason = GameOverReason.None;

    void Start()
    {
        triggerGameOver = false;
        hasGameOverTriggered = false;
        reason = GameOverReason.None;
    }

    void Update()
    {
        if (triggerGameOver && !hasGameOverTriggered)
        {
            hasGameOverTriggered = true;
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        switch (reason)
        {
            case GameOverReason.BatteryDepleted:
                Debug.LogWarning("Game Over! You got 0% battery.");
                break;
            case GameOverReason.Destroyed:
                Debug.LogWarning("Game Over! You got destroyed.");
                break;
            default:
                Debug.LogWarning("Game Over! Unknown reason.");
                break;
        }

        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Se quiser pausar o jogo, use Time.timeScale = 0; (mas aí tem que gerenciar reinício depois)
    }

}
