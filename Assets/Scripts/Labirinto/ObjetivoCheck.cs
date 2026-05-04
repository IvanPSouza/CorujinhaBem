using UnityEngine;

public class ObjetivoCheck : MonoBehaviour
{
    [Header("Configuração")]
    public MoveTampinha objetoCorreto;

    private MoveTampinha objetoAtual;

    // evita bug de se bloquear sozinho
    public bool EstaDisponivelPara(MoveTampinha obj)
    {
        return objetoAtual == null || objetoAtual == obj;
    }

    public bool TemObjeto()
    {
        return objetoAtual != null;
    }

    public bool EstaCorreto()
    {
        return objetoAtual != null && objetoAtual == objetoCorreto;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        MoveTampinha tampinha = other.GetComponent<MoveTampinha>();

        if (tampinha != null)
        {
            // Só aceita quando SOLTO
            if (tampinha.IsDragging()) return;

            // Se já tem outro objeto, ignora
            if (objetoAtual != null && objetoAtual != tampinha) return;

            objetoAtual = tampinha;

            if (tampinha == objetoCorreto)
            {
                Debug.Log($"[{gameObject.name}] Correto!");
            }
            else
            {
                Debug.Log($"[{gameObject.name}] Errado!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        MoveTampinha tampinha = other.GetComponent<MoveTampinha>();

        if (tampinha != null && tampinha == objetoAtual)
        {
            objetoAtual = null;
            Debug.Log($"[{gameObject.name}] Objeto removido");
        }
    }
}