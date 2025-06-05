using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class collecting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI NAME;
    [SerializeField] private TextMeshProUGUI TIMER;

    [SerializeField] private Slider progressSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpdateTimer(float progress)
    {
        progressSlider.value = progress;
        TIMER.text = progress.ToString("F1") + "s";

    }

    public void UpdateUI(int _hpC, int _hpM, float _timer, string _name)
    {
        HP.text = "Coletando... " + _hpC + " / " + _hpM;
        NAME.text = _name;

        progressSlider.maxValue = _timer;
    }
}
