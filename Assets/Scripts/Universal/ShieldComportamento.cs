using UnityEngine;

public class ShieldPart : MonoBehaviour
{
    private Transform parentTransform;
    private string entityTag;

    void Start()
    {
        parentTransform = transform.parent;

        if (parentTransform == null)
        {
            Debug.LogError("ShieldPart: Nenhum objeto pai encontrado!");
        }
    }

    void Update()
    {
        if (parentTransform == null) return;

        if(entityTag == "Boss" || entityTag == "Enemy")
        {
            float rotationSpeed = 1080f; // ajuste conforme desejar
            transform.RotateAround(parentTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            return;
        }

        // Converte a posi��o do mouse para o mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Calcula a dire��o do pai at� o mouse
        Vector3 direction = mousePos - parentTransform.position;

        // Calcula o �ngulo em graus
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica a rota��o (com ajuste se o sprite estiver orientado para cima em vez da direita)
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // para sincronizar a posi��o em rela��o ao ponteiro do mouse
    }


    public void setTag(string tag)
    {
        entityTag = tag;
    }
}
