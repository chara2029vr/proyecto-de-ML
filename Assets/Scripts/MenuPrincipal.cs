using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject panelCreditos;
    public GameObject panelConfiguracion;
    public Text textoRecord;

    void Start()
    {
        Time.timeScale = 1;
        panelCreditos.SetActive(false);
        panelConfiguracion.SetActive(false);

        // muestra el mejor puntaje guardado
        textoRecord.text = "Récord: " + PlayerPrefs.GetInt("Record", 0);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void AbrirCreditos()
    {
        panelCreditos.SetActive(true);
    }

    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
    }

    public void AbrirConfiguracion()
    {
        panelConfiguracion.SetActive(true);
    }

    public void CerrarConfiguracion()
    {
        panelConfiguracion.SetActive(false);
    }

    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
