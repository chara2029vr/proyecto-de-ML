using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidad = 7f;
    public float fuerzaSalto = 13f;
    public Transform pies;
    public LayerMask capaSuelo;

    public AudioClip sonidoSalto;
    public AudioClip sonidoMoneda;
    public AudioClip sonidoGema;
    public AudioClip sonidoMuerte;

    public GameObject efectoMoneda;
    public GameObject efectoGema;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private AudioSource audioSource;

    private float movimiento;
    private bool enSuelo;
    private bool muerto = false;
    private PlataformaMovil plataforma;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // si el juego esta en pausa o el jugador murio no se lee el teclado
        if (muerto || Time.timeScale == 0)
        {
            return;
        }

        movimiento = Input.GetAxisRaw("Horizontal");

        // revisa si los pies estan tocando el suelo
        Collider2D suelo = Physics2D.OverlapCircle(pies.position, 0.2f, capaSuelo);
        enSuelo = suelo != null;

        if (suelo != null)
        {
            plataforma = suelo.GetComponent<PlataformaMovil>();
        }
        else
        {
            plataforma = null;
        }

        bool saltar = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        bool soltar = Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);

        if (saltar && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            audioSource.PlayOneShot(sonidoSalto);
        }

        // si suelta la tecla antes de tiempo el salto es mas bajito
        if (soltar && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // voltear el sprite hacia donde camina
        if (movimiento > 0)
        {
            sprite.flipX = false;
        }
        else if (movimiento < 0)
        {
            sprite.flipX = true;
        }

        animator.SetFloat("Velocidad", Mathf.Abs(movimiento));
        animator.SetBool("EnSuelo", enSuelo);

        // si se cae al agua
        if (transform.position.y < -5)
        {
            Morir();
        }
    }

    void FixedUpdate()
    {
        if (muerto)
        {
            return;
        }

        // si esta parado en una plataforma movil se le suma la velocidad de la plataforma
        float velocidadExtra = 0;
        if (plataforma != null)
        {
            velocidadExtra = plataforma.velocidadActual.x;
        }

        rb.linearVelocity = new Vector2(movimiento * velocidad + velocidadExtra, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (muerto)
        {
            return;
        }

        if (otro.CompareTag("Moneda"))
        {
            audioSource.PlayOneShot(sonidoMoneda);
            Instantiate(efectoMoneda, otro.transform.position, Quaternion.identity);
            GameManager.instancia.RecogerMoneda();
            Destroy(otro.gameObject);
        }
        else if (otro.CompareTag("Gema"))
        {
            audioSource.PlayOneShot(sonidoGema);
            Instantiate(efectoGema, otro.transform.position, Quaternion.identity);
            GameManager.instancia.RecogerGema();
            Destroy(otro.gameObject);
        }
        else if (otro.CompareTag("Peligro"))
        {
            Morir();
        }
        else if (otro.CompareTag("Meta"))
        {
            GameManager.instancia.Ganar();
        }
    }

    public void Morir()
    {
        if (muerto)
        {
            return;
        }

        muerto = true;
        rb.linearVelocity = Vector2.zero;
        sprite.color = Color.red;
        audioSource.PlayOneShot(sonidoMuerte);
        GameManager.instancia.Perder();
    }
}
