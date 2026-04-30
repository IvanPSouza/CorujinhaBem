using UnityEngine;

public class Tipagem : MonoBehaviour
{
    public enum Cor { Verde, Vermelho, Azul, Amarelo }
    public enum Tamanho { Pequeno, Medio, Grande }

    [Header("Configurações do Objeto")]
    public Cor corSelecionada;
    public Tamanho tamanhoSelecionado;

    public PedidoCorujinha gerenciador;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        AplicarTamanho();
        AplicarCor();
    }

    void AplicarTamanho()
    {
        transform.localScale = tamanhoSelecionado switch
        {
            Tamanho.Pequeno => Vector3.one * 0.1f,
            Tamanho.Medio => Vector3.one * 0.15f,
            Tamanho.Grande => Vector3.one * 0.2f,
            _ => Vector3.one
        };
    }

    void AplicarCor()
    {
        if (sr == null)
        {
            Debug.LogWarning("Objeto sem SpriteRenderer!");
            return;
        }

        sr.color = corSelecionada switch
        {
            Cor.Verde => Color.green,
            Cor.Vermelho => Color.red,
            Cor.Azul => Color.blue,
            Cor.Amarelo => Color.yellow,
            _ => Color.white
        };
    }

    void OnMouseDown()
    {
        if (gerenciador != null)
        {
            gerenciador.VerificarResposta(this);
        }
    }
}