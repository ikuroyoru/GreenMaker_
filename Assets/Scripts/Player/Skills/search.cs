using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class search : MonoBehaviour
{
    /*
    [SerializeField] private GameObject progressBarPrefab; // atribuído no Inspector
    private GameObject currentSlider;
    private Slider progressBar;

    private score scoreScript;
    private float searchAmount = 10;
    private float searchTimer = 5f;

    private float searchedPoints;

    
    private void Start()
    {
        scoreScript = GetComponent<score>();

        searchedPoints = searchAmount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (scoreScript.gScore() >= searchAmount)
            {
                currentSlider = Instantiate(progressBarPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity, transform);
                progressBar = currentSlider.GetComponentInChildren<Slider>();
                progressBar.maxValue = searchAmount;
                progressBar.value = 0;

                StartCoroutine(searchTrash());
            }

            else Debug.LogWarning("Pontos insuficientes");
        }
    }

    public IEnumerator searchTrash()
    {
        float timer = 0f;
        float damage = searchAmount / searchTimer;
        

        scoreScript.updateGeneralPoints(searchAmount * -1); // Custo de pontos para o uso da habilidade, transforma o valor em pontos negativos e envia o valor para a função

        while (timer < searchTimer)
        {
            yield return new WaitForSeconds(1f);
            progressBar.value += damage;
            scoreScript.updateTrashPoints(damage);
            timer += 1f;
        }

        // scoreScript.updateTrashPoints(searchedPoints); // Pontos transformados

        yield return new WaitForSeconds(0.5f);
        Destroy(currentSlider);
        currentSlider = null;
    }
   */

}
