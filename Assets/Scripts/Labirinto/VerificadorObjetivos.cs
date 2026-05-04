using UnityEngine;

public class VerificadorObjetivos : MonoBehaviour
{
    public ObjetivoCheck[] objetivos;

    public GameObject telaVitoria;
    public GameObject telaDerrota;

    private bool jaVerificado = false;

    void Update()
    {
        if (jaVerificado) return;

        Verificar();
    }

    void Verificar()
    {
        bool todosPreenchidos = true;
        bool todosCorretos = true;

        foreach (ObjetivoCheck obj in objetivos)
        {
            if (!obj.TemObjeto())
            {
                todosPreenchidos = false;
                break;
            }

            if (!obj.EstaCorreto())
            {
                todosCorretos = false;
            }
        }

        if (todosPreenchidos)
        {
            jaVerificado = true;

            if (todosCorretos)
            {
                if (telaVitoria != null)
                    telaVitoria.SetActive(true);
            }
            else
            {
                if (telaDerrota != null)
                    telaDerrota.SetActive(true);
            }

            Time.timeScale = 0f;
        }
    }
}