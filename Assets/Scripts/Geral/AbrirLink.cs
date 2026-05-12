using UnityEngine;

public class AbrirLink : MonoBehaviour
{
    public string url;

    public void IrParaPagina()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("O campo de URL está vazio!");
        }
    }
}
