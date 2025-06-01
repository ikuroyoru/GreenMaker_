using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField] private GameObject pauseMenuUI;

    private void Start()
    {
        ResumeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }

    public void OnResumeButtonPressed()
    {
        ResumeGame();
    }
}
