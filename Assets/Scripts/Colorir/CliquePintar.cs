using UnityEngine;

public class CliquePintar : MonoBehaviour
{
    [HideInInspector]
    public SistemaPintura2D sistema;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color =
                sistema.PegarCorSelecionada();
        }
    }
}