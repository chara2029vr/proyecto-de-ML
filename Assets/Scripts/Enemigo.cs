using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private Vector3 posicionA;
    private Vector3 posicionB;
    private Vector3 destino;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        // se guardan las posiciones al inicio porque los puntos son hijos del enemigo y se moverian con el
        posicionA = puntoA.position;
        posicionB = puntoB.position;
        destino = posicionB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        // cuando llega a un punto cambia de direccion
        if (Vector3.Distance(transform.position, destino) < 0.05f)
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

        if (destino.x > transform.position.x)
        {
            sprite.flipX = true;
        }
        else if (destino.x < transform.position.x)
        {
            sprite.flipX = false;
        }
    }
}
