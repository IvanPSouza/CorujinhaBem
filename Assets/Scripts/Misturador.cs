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

    public void DistribuirObjetos()
    {
        if (objetos == null || locais == null)
        {
            Debug.LogError("Listas não atribuídas!");
            return;
        }

        if (objetos.Count > locais.Count)
        {
            Debug.LogError("Há mais objetos do que locais disponíveis!");
            return;
        }

        List<Transform> locaisEmbaralhados = new List<Transform>(locais);
        Embaralhar(locaisEmbaralhados);

        for (int i = 0; i < objetos.Count; i++)
        {
            GameObject obj = objetos[i];
            Transform local = locaisEmbaralhados[i];

            obj.transform.SetParent(local);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }

    public void Embaralhar<T>(List<T> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int rand = Random.Range(i, lista.Count);
            (lista[i], lista[rand]) = (lista[rand], lista[i]);
        }
    }
}