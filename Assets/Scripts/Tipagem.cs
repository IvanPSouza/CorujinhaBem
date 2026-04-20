using UnityEngine;

public class Tipagem : MonoBehaviour
{
    public enum Cor
    {
        Verde,
        Vermelho,
        Azul,
        Amarelo
    }

    public enum Tamanho
    {
        Pequeno,
        Medio,
        Grande
    }

    [Header("Configurações do Objeto")]
    public Cor corSelecionada;
    public Tamanho tamanhoSelecionado;

    public PedidoCorujinha gerenciador;

    void Start()
    {
        AplicarTamanho();
        AplicarCor();
    }

    void AplicarTamanho()
    {
        switch (tamanhoSelecionado)
        {
            case Tamanho.Pequeno:
                transform.localScale = Vector3.one * 0.1f;
                break;
            case Tamanho.Medio:
                transform.localScale = Vector3.one * 0.2f;
                break;
            case Tamanho.Grande:
                transform.localScale = Vector3.one * 0.3f;
                break;
        }
    }

    void AplicarCor()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogWarning("Objeto sem SpriteRenderer!");
            return;
        }

        switch (corSelecionada)
        {
            case Cor.Verde:
                sr.color = Color.green;
                break;
            case Cor.Vermelho:
                sr.color = Color.red;
                break;
            case Cor.Azul:
                sr.color = Color.blue;
                break;
            case Cor.Amarelo:
                sr.color = Color.yellow;
                break;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (gerenciador != null)
                {
                    gerenciador.VerificarResposta(this);
                }
            }
        }
    }
}