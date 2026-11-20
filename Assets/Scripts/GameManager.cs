using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public int puntosMoneda = 10;
    public int puntosGema = 50;

    [Header("HUD")]
    public Text textoMonedas;
    public Text textoGemas;
    public Text textoPuntos;

    [Header("Paneles de fin de partida")]
    public GameObject panelMuerte;
    public GameObject panelVictoria;
    public Text textoPuntosFinal;
    public Text textoRecord;

    public AudioClip sonidoVictoria;

    public bool juegoTerminado = false;

    private int puntos = 0;
    private int monedas = 0;
    private int gemas = 0;
    private int totalMonedas;
    private int totalGemas;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        // cuenta cuantos coleccionables hay en el nivel
        totalMonedas = GameObject.FindGameObjectsWithTag("Moneda").Length;
        totalGemas = GameObject.FindGameObjectsWithTag("Gema").Length;

        panelMuerte.SetActive(false);
        panelVictoria.SetActive(false);
        ActualizarHUD();
    }

    public void RecogerMoneda()
    {
        monedas++;
        puntos += puntosMoneda;
        ActualizarHUD();
    }

    public void RecogerGema()
    {
        gemas++;
        puntos += puntosGema;
        ActualizarHUD();
    }

    void ActualizarHUD()
    {
        textoMonedas.text = "Monedas: " + monedas + "/" + totalMonedas;
        textoGemas.text = "Gemas: " + gemas + "/" + totalGemas;
        textoPuntos.text = "Puntos: " + puntos;
    }

    public void Perder()
    {
        if (juegoTerminado)
        {
            return;
        }

        juegoTerminado = true;
        panelMuerte.SetActive(true);
        Time.timeScale = 0;
    }

    public void Ganar()
    {
        if (juegoTerminado)
        {
            return;
        }

        juegoTerminado = true;
        GetComponent<AudioSource>().PlayOneShot(sonidoVictoria);

        // guardar el record con PlayerPrefs
        int record = PlayerPrefs.GetInt("Record", 0);
        if (puntos > record)
        {
            record = puntos;
            PlayerPrefs.SetInt("Record", record);
            PlayerPrefs.Save();
        }

        textoPuntosFinal.text = "Puntos: " + puntos;
        textoRecord.text = "Récord: " + record;
        panelVictoria.SetActive(true);
        Time.timeScale = 0;
    }
}
