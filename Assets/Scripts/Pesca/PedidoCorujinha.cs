using System.Collections;
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

    [Header("Áudio")]
    public AudioSource audioSource;

    public AudioClip audioPadrao;
    public AudioClip audioOps;
    public AudioClip audioObrigado;

    // tamanho
    public AudioClip pequenoClip;
    public AudioClip medioClip;
    public AudioClip grandeClip;

    // cor
    public AudioClip verdeClip;
    public AudioClip vermelhoClip;
    public AudioClip azulClip;
    public AudioClip amareloClip;

    private Tipagem objetoEscolhido;

    private bool jogoFinalizado = false;
    private bool podeClicar = true;

    private int Tentativas = 0;

    void Start()
    {
        if (menuPause != null)
            menuPause.SetActive(false);

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

        // toca áudio do pedido
        TocarFrase(audioPadrao);
    }

    string GetDescricaoPedido()
    {
        return $"{objetoEscolhido.corSelecionada} {objetoEscolhido.tamanhoSelecionado}";
    }

    public void VerificarResposta(Tipagem objetoClicado)
    {
        if (!podeClicar || jogoFinalizado) return;

        if (
            objetoClicado.corSelecionada == objetoEscolhido.corSelecionada &&
            objetoClicado.tamanhoSelecionado == objetoEscolhido.tamanhoSelecionado
        )
        {
            textoPedido.text = "Muito obrigado!";

            // áudio de acerto
            TocarAudioSimples(audioObrigado);

            podeClicar = false;

            objetos.Remove(objetoClicado);
            objetoClicado.gameObject.SetActive(false);

            Invoke(nameof(ProximoPasso), 2f);
        }
        else
        {
            textoPedido.text = $"Ops! Eu quero um peixe {GetDescricaoPedido()}";

            // áudio de erro com descrição
            TocarFrase(audioOps);
        }

        Tentativas++;
    }

    void ProximoPasso()
    {
        GerarPedido();
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
        if (menuPause.activeSelf == false)
        {
            podeClicar = true;
        }
        else
        {
            podeClicar = false;
        }
    }

    // =========================
    // SISTEMA DE ÁUDIO
    // =========================

    void TocarFrase(AudioClip audioBase)
    {
        if (audioSource == null) return;

        audioSource.Stop();
        StopAllCoroutines();
        StartCoroutine(TocarFraseAudio(audioBase));
    }

    void TocarAudioSimples(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;

        audioSource.Stop();
        StopAllCoroutines();

        audioSource.clip = clip;
        audioSource.Play();
    }

    IEnumerator TocarFraseAudio(AudioClip audioBase)
    {
        // 1. base
        if (audioBase != null)
        {
            audioSource.clip = audioBase;
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        // 2. cor (ANTES do tamanho agora)
        AudioClip corClip = objetoEscolhido.corSelecionada switch
        {
            Tipagem.Cor.Verde => verdeClip,
            Tipagem.Cor.Vermelho => vermelhoClip,
            Tipagem.Cor.Azul => azulClip,
            Tipagem.Cor.Amarelo => amareloClip,
            _ => null
        };

        if (corClip != null)
        {
            audioSource.clip = corClip;
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        // 3. tamanho (DEPOIS da cor)
        AudioClip tamanhoClip = objetoEscolhido.tamanhoSelecionado switch
        {
            Tipagem.Tamanho.Pequeno => pequenoClip,
            Tipagem.Tamanho.Medio => medioClip,
            Tipagem.Tamanho.Grande => grandeClip,
            _ => null
        };

        if (tamanhoClip != null)
        {
            audioSource.clip = tamanhoClip;
            audioSource.Play();
        }
    }
}