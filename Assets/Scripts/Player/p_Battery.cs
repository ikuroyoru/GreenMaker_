using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class p_Battery : MonoBehaviour
{
    private float defaultBattery = 100f;
    private float currentBattery;

    [SerializeField] private TextMeshProUGUI batteryOutput;
    [SerializeField] private Slider batteryBar;

    private Coroutine batteryDrainCoroutine;

    private void Start()
    {
        currentBattery = defaultBattery;
        batteryBar.maxValue = defaultBattery;
        UpdateBatteryUI();
        batteryDrainCoroutine = StartCoroutine(DrainBatteryOverTime());
    }

    private void Update()
    {
        // Simula uma habilidade que consome 10% ao pressionar H
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseAbility(10f); // 10% do total
        }
    }

    private IEnumerator DrainBatteryOverTime()
    {
        while (true)
        {
            if (currentBattery > 0)
            {
                currentBattery -= defaultBattery * 0.01f; // 1% por segundo
                currentBattery = Mathf.Max(currentBattery, 0f);
                UpdateBatteryUI();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void UseAbility(float percentCost)
    {
        float cost = defaultBattery * (percentCost / 100f);

        if (currentBattery >= cost)
        {
            currentBattery -= cost;
            UpdateBatteryUI();
            Debug.Log($"Habilidade usada: -{percentCost}%");
        }
        else
        {
            Debug.Log("Bateria insuficiente.");
        }
    }

    private void UpdateBatteryUI()
    {
        batteryBar.value = currentBattery;
        batteryOutput.text = $"{Mathf.RoundToInt(currentBattery)}%";
    }
}
