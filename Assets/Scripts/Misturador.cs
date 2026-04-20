using System.Collections.Generic;
using UnityEngine;

public class Misturador : MonoBehaviour
{
    [Header("Lista de objetos para posicionar")]
    public List<GameObject> objetos;

    [Header("Lista de locais (Transforms)")]
    public List<Transform> locais;

    void Start()
    {
        DistribuirObjetos();
    }

    void DistribuirObjetos()
    {
        // Verificação básica
        if (objetos.Count > locais.Count)
        {
            Debug.LogError("Há mais objetos do que locais disponíveis!");
            return;
        }

        // Criar uma cópia dos locais para não modificar a lista original
        List<Transform> locaisDisponiveis = new List<Transform>(locais);

        foreach (GameObject obj in objetos)
        {
            // Escolhe um índice aleatório
            int index = Random.Range(0, locaisDisponiveis.Count);

            // Pega o local correspondente
            Transform localEscolhido = locaisDisponiveis[index];

            // Move o objeto para o local
            obj.transform.position = localEscolhido.position;

            // Remove o local usado para não repetir
            locaisDisponiveis.RemoveAt(index);
        }
    }
}
