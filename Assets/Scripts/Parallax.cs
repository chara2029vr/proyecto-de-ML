using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camara;

    // 0 = el fondo se queda quieto en el mundo, 1 = el fondo se mueve igual que la camara
    public float efecto = 0.5f;
    public float alturaExtra = 0f;

    private float inicioX;

    void Start()
    {
        inicioX = transform.position.x;
    }

    void LateUpdate()
    {
        // el fondo se mueve solo una parte de lo que se mueve la camara,
        // asi parece que esta mas lejos
        float x = inicioX + camara.position.x * efecto;
        transform.position = new Vector3(x, camara.position.y + alturaExtra, transform.position.z);
    }
}
