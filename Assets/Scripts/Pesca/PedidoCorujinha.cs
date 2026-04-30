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

    [Header("Menu Final")]
    public GameObject menuPause;

    [Header("Menu Vitoria")]
    public GameObject menuVitoria;

    [Header("Texto Tentativas")]
    public TMP_Text textoTentativas;

    private Tipagem objetoEscolhido;

    private bool jogoFinalizado = false;
    private bool podeClicar = true; //controle de spam
    private int Tentativas = 0;

    void Start()
    {
        if (menuPause != null)
            menuPause.SetActive(false);
        podeClicar = true;

        GerarPedido();
    }

    void GerarPedido()
    {
        if (jogoFinalizado) return;

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
    private void Update()
    {
       /* if(Time.timeScale == 0f && )
        {

        } */
    }
    public void VerificarResposta(Tipagem objetoClicado)
    {
        // bloqueia clique durante delay ou fim
        if (!podeClicar || jogoFinalizado) return;

        if (
            objetoClicado.corSelecionada == objetoEscolhido.corSelecionada &&
            objetoClicado.tamanhoSelecionado == objetoEscolhido.tamanhoSelecionado
        )
        {
            textoPedido.text = "Muito obrigado!";

            //trava clique
            podeClicar = false;

            //Remove e desativa
            objetos.Remove(objetoClicado);
            objetoClicado.gameObject.SetActive(false);

            //espera 2 segundos antes do próximo pedido
            Invoke(nameof(ProximoPasso), 2f);
        }
        else
        {
            textoPedido.text = $"Ops! Eu quero um peixe {GetDescricaoPedido()}";
        }

        Tentativas++;
    }

    void ProximoPasso()
    {
        GerarPedido();

        //libera clique novamente
        podeClicar = true;
    }

    void FinalizarJogo()
    {
        jogoFinalizado = true;

        textoPedido.text = "Acabaram os peixes, Paraben!";

        if (menuVitoria != null)
        {
            textoTentativas.text = $"Voce jogou sua vara {Tentativas} vezes";
            menuVitoria.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Menu final não atribuído!");
        }
    }

    void CarregarCenaFinal()
    {
        SceneManager.LoadScene(nomeDaCenaFinal);
    }

    public void Pausar()
    {
        //menuPause.SetActive(!menuPause.activeSelf);
        if (menuPause.activeSelf == false)
        {
            podeClicar = true;

            //Se pausado entre o tempo de cada pedido pode fazer as tampinhas sejam clicaveis antes da hora
        }
        else
        {
            podeClicar = false;
        }
    }
}