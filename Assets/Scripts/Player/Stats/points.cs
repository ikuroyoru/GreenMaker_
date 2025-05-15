using UnityEngine;

public class score : MonoBehaviour
{
    private float generalScore = 0;
    private float trashScore = 0;

    public void updateGeneralPoints(float amount) // Pontos gerais recebidos por coletar o lixo, representa o lixo NÃO VASCULHADO
    {
        generalScore += amount;
        // Debug.LogWarning("general score: " + generalScore);
    }

    public void updateTrashPoints(float amount) // Pontos gerais recebidos por coletar o lixo, representa o lixo NÃO VASCULHADO
    {
        trashScore += amount;
        // Debug.LogWarning("trash score: " + trashScore);
    }

    public float gScore()
    {
        return generalScore;
    }

    public float tScore()
    {
        return trashScore;
    }
}

