using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    // el jugador lee esta velocidad para moverse junto con la plataforma
    public Vector2 velocidadActual;

    private Rigidbody2D rb;
    private Vector2 posicionA;
    private Vector2 posicionB;
    private Vector2 destino;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicionA = puntoA.position;
        posicionB = puntoB.position;
        destino = posicionB;
    }

    void FixedUpdate()
    {
        Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, destino, velocidad * Time.fixedDeltaTime);
        velocidadActual = (nuevaPosicion - rb.position) / Time.fixedDeltaTime;
        rb.MovePosition(nuevaPosicion);

        if (Vector2.Distance(nuevaPosicion, destino) < 0.01f)
        {
            if (destino == posicionA)
            {
                destino = posicionB;
            }
            else
            {
                destino = posicionA;
            }
        }
    }
}
