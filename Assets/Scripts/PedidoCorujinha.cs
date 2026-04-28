using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PedidoCorujinha : MonoBehaviour
{
    [Header("Lista de objetos")]
    public List<Tipagem> objetos;

    [Header("Texto UI (TMP)")]
    public TMP_Text textoPedido;

    [Header("Cena ao finalizar")]
    public string nomeDaCenaFinal;

    private Tipagem objetoEscolhido;

    void Start()
    {
        GerarPedido();
    }

    void GerarPedido()
    {
        if (objetos == null || objetos.Count == 0)
        {
            FinalizarJogo();
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
        if (
            objetoClicado.corSelecionada == objetoEscolhido.corSelecionada &&
            objetoClicado.tamanhoSelecionado == objetoEscolhido.tamanhoSelecionado
        )
        {
            textoPedido.text = "Boa!";

            // Remove da lista
            objetos.Remove(objetoClicado);

            // Desativa o objeto
            objetoClicado.gameObject.SetActive(false);

            // Próximo pedido ou fim
            GerarPedido();
        }
        else
        {
            textoPedido.text = $"Ops! Quero um peixe {GetDescricaoPedido()}";
        }
    }

    void FinalizarJogo()
    {
        textoPedido.text = "Acabaram os peixes!";

        if (!string.IsNullOrEmpty(nomeDaCenaFinal))
        {
            SceneManager.LoadScene(nomeDaCenaFinal);
        }
        else
        {
            Debug.LogWarning("Nome da cena final não definido!");
        }
    }
}