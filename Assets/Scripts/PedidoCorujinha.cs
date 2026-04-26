using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PedidoCorujinha : MonoBehaviour
{
    [Header("Lista de objetos")]
    public List<Tipagem> objetos;

    [Header("Texto UI pedido (TMP)")]
    public TMP_Text textoPedido;

    [Header("Texto UI score (TMP)")]
    public TMP_Text textoScore;

    [Header("Misturador")]
    public Misturador misturador;

    private Tipagem objetoEscolhido;

    private int Score;

    // Controle de clique
    private bool podeClicar = true;

    void Start()
    {
        Score = 0;
        GerarPedido();
    }

    void GerarPedido()
    {
        if (objetos == null || objetos.Count == 0)
        {
            Debug.LogError("Lista de objetos vazia!");
            return;
        }

        int index = Random.Range(0, objetos.Count);
        objetoEscolhido = objetos[index];

        textoPedido.text = $"Você pode pescar um peixe {GetDescricaoPedido()}?";
    }

    string GetDescricaoPedido()
    {
        return $"{objetoEscolhido.corSelecionada} {objetoEscolhido.tamanhoSelecionado}";
    }

    public void VerificarResposta(Tipagem objetoClicado)
    {
        // Bloqueia spam
        if (!podeClicar) return;

        if (
            objetoClicado.corSelecionada == objetoEscolhido.corSelecionada &&
            objetoClicado.tamanhoSelecionado == objetoEscolhido.tamanhoSelecionado
        )
        {
            textoPedido.text = "Obrigado!";

            podeClicar = false; // trava clique

            Score++;

            textoScore.text = $"{Score}";

            Invoke(nameof(NovoPedidoComMistura), 2f);
        }
        else
        {
            textoPedido.text = $"Ops! Quero um peixe {GetDescricaoPedido()}";
        }
    }

    void NovoPedidoComMistura()
    {
        // Reembaralha
        if (misturador != null)
        {
            misturador.DistribuirObjetos();
        }

        // Novo pedido
        GerarPedido();

        // Libera clique novamente
        podeClicar = true;
    }
}