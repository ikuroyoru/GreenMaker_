using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("level 01");
    }
}
