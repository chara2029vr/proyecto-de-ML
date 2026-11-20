using UnityEngine;

public class CamaraSeguir : MonoBehaviour
{
    public Transform objetivo;
    public float suavizado = 5f;
    public float alturaExtra = 2f;

    // limites para que la camara no se salga del nivel
    public float limiteIzquierdo = 0f;
    public float limiteDerecho = 100f;
    public float limiteAbajo = 0f;

    void LateUpdate()
    {
        Vector3 destino = new Vector3(objetivo.position.x, objetivo.position.y + alturaExtra, transform.position.z);

        destino.x = Mathf.Clamp(destino.x, limiteIzquierdo, limiteDerecho);
        if (destino.y < limiteAbajo)
        {
            destino.y = limiteAbajo;
        }

        transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);
    }
}
