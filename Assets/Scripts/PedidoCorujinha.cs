using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PedidoCorujinha : MonoBehaviour
{
    [Header("Lista de objetos")]
    public List<Tipagem> objetos;

    [Header("Texto UI (TMP)")]
    public TMP_Text textoPedido;

    private Tipagem objetoEscolhido;

    void Start()
    {
        GerarPedido();
    }

    void GerarPedido()
    {
        int index = Random.Range(0, objetos.Count);
        objetoEscolhido = objetos[index];

        string cor = objetoEscolhido.corSelecionada.ToString();
        string tamanho = objetoEscolhido.tamanhoSelecionado.ToString();

        textoPedido.text = $"Poderia pescar um peixe {cor} {tamanho}?";
    }

    public void VerificarResposta(Tipagem objetoClicado)
    {
        if (objetoClicado == objetoEscolhido)
        {
            textoPedido.text = "Obrigado!";
        }
        else
        {
            string cor = objetoEscolhido.corSelecionada.ToString();
            string tamanho = objetoEscolhido.tamanhoSelecionado.ToString();

            textoPedido.text = $"Ops, não é esse. Eu quero um peixe {cor} {tamanho}";
        }
    }
}