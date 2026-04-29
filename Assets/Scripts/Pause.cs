using UnityEngine;

public class Pause : MonoBehaviour
{
    [Header("Menu de Pause")]
    public GameObject menuPause;

    private bool pausado = false;

    void Start()
    {
        if (menuPause != null)
            menuPause.SetActive(false);

        Time.timeScale = 1f;
    }

    public void AlternarPause()
    {
        pausado = !pausado;

        if (pausado)
        {
            Pausar();
        }
        else
        {
            Despausar();
        }
    }

    void Pausar()
    {
        if (menuPause != null)
            menuPause.SetActive(true);

        Time.timeScale = 0f; // pausa o jogo
    }

    void Despausar()
    {
        if (menuPause != null)
            menuPause.SetActive(false);

        Time.timeScale = 1f; // volta ao normal
    }
}
