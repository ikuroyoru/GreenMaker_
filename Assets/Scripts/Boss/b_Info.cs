using UnityEngine;

public class b_Info : MonoBehaviour
{
    [SerializeField] public Sprite icon; // CORRIGIDO
    [SerializeField] public float health;

    private hp hpScript;

    void Start()
    {
        hpScript = GetComponent<hp>();
        hpScript.setHP(health);
    }
}
