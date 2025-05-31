using UnityEngine;
using System.Collections;

public class turnOnRobot : MonoBehaviour
{
    public float defaultConsume = 1f; // Consumo de carga por intervalo de tempo
    public float timeInterval = 1f;   // Intervalo entre os consumos

    private bool Activated;
    private p_Battery batteryScript;

    void Start()
    {
        batteryScript = GetComponent<p_Battery>();
        Activated = false;
    }

    void Update()
    {
        if (batteryScript.robotStatus() && !Activated)
        {
            Activated = true;
            StartCoroutine(DrainBatteryOverTime());
        }
    }

    private IEnumerator DrainBatteryOverTime()
    {
        Debug.Log("Robot activated? : " + batteryScript.robotStatus());

        while (batteryScript.robotStatus())
        {
            yield return new WaitForSeconds(timeInterval);

            if (batteryScript.currentCharge() >= defaultConsume)
            {
                batteryScript.UseAbility(defaultConsume);
            }
            else
            {
                Debug.Log("Battery too low. Stopping robot.");
                break;
            }
        }

        Debug.Log("Robot deactivated? : " + batteryScript.robotStatus());
        Activated = false;
    }
}
