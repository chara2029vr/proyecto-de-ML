using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJuego : MonoBehaviour
{
    public GameObject panelInstrucciones;
    public GameObject panelPausa;
    public GameObject panelConfiguracion;

    void Start()
    {
        // al empezar el nivel el juego queda en pausa mostrando las instrucciones
        panelInstrucciones.SetActive(true);
        panelPausa.SetActive(false);
        panelConfiguracion.SetActive(false);
        Time.timeScale = 0;
    }

    void Update()
    {
        // tambien se puede pausar con la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelPausa.activeSelf)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void CerrarInstrucciones()
    {
        panelInstrucciones.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pausar()
    {
        if (panelInstrucciones.activeSelf || GameManager.instancia.juegoTerminado)
        {
            return;
        }

        panelPausa.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continuar()
    {
        panelPausa.SetActive(false);
        panelConfiguracion.SetActive(false);
        Time.timeScale = 1;
    }

    public void AbrirConfiguracion()
    {
        panelConfiguracion.SetActive(true);
    }

    public void CerrarConfiguracion()
    {
        panelConfiguracion.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
