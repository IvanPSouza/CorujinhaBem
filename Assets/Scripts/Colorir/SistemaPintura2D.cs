using System.Collections.Generic;
using UnityEngine;

public class SistemaPintura2D : MonoBehaviour
{
    [Header("Objetos que escolhem a cor")]
    public List<GameObject> botoesDeCor;

    [Header("Objetos que serão pintados")]
    public List<GameObject> objetosPintaveis;

    [Header("Objeto marcador da cor selecionada")]
    public GameObject marcadorSelecao;

    private Color corSelecionada = Color.white;

    void Start()
    {
        // Adiciona os scripts automaticamente
        foreach (GameObject obj in botoesDeCor)
        {
            CliqueCor clique = obj.AddComponent<CliqueCor>();
            clique.sistema = this;
        }

        foreach (GameObject obj in objetosPintaveis)
        {
            CliquePintar clique = obj.AddComponent<CliquePintar>();
            clique.sistema = this;
        }

        // Esconde o marcador no início
        if (marcadorSelecao != null)
        {
            marcadorSelecao.SetActive(false);
        }
    }

    public void SelecionarCor(Color novaCor, GameObject objetoCor)
    {
        corSelecionada = novaCor;

        // Move o marcador para cima do objeto selecionado
        if (marcadorSelecao != null)
        {
            marcadorSelecao.SetActive(true);

            marcadorSelecao.transform.position =
                objetoCor.transform.position;
        }
    }

    public Color PegarCorSelecionada()
    {
        return corSelecionada;
    }
}