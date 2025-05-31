using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class p_Battery : MonoBehaviour
{
    private float defaultBattery = 1000f; // Pontos de carga iniciais
    private float currentBattery;         // Pontos de carga atuais
    private bool robotActivated = false;
    private float batteryPercent;

    [SerializeField] private TextMeshProUGUI batteryOutput;
    [SerializeField] private Slider batteryBar;

    private GameOverManager gameOverManager;

    private void Start()
    {
        currentBattery = defaultBattery;
        UpdateBatteryUI(); // Atualiza os valores na UI

        gameOverManager = GetComponent<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager não encontrado no mesmo GameObject.");
        }
    }

    private void Update()
    {
        // Simula uma habilidade que consome 990 pontos ao pressionar H
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseAbility(990f);
        }

        if (currentBattery > 0)
        {
            robotActivated = true;
        }
        else
        {
            robotActivated = false;

            if (gameOverManager != null && !gameOverManager.triggerGameOver)
            {
                gameOverManager.reason = GameOverReason.BatteryDepleted;
                gameOverManager.triggerGameOver = true;
            }
        }
    }

    public void UseAbility(float charge)
    {
        if (currentBattery >= charge)
        {
            currentBattery -= charge;
            batteryPercent = (currentBattery / defaultBattery) * 100f;
            UpdateBatteryUI();
        }
        else
        {
            Debug.Log("Bateria insuficiente.");
        }
    }

    private void UpdateBatteryUI()
    {
        batteryBar.maxValue = defaultBattery;
        batteryBar.value = currentBattery;
        batteryOutput.text = $"{currentBattery} / {defaultBattery} - ({batteryPercent:F1}%)";
    }

    public bool robotStatus()
    {
        return robotActivated;
    }

    public float currentCharge()
    {
        return currentBattery;
    }
}
