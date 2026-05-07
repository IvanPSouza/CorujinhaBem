using UnityEngine;

public class CliqueCor : MonoBehaviour
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
            sistema.SelecionarCor(
                spriteRenderer.color,
                gameObject
            );
        }
    }
}