using UnityEngine;

public class MoveTampinha : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 offset;

    private Transform objetivoAtual;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        // Evita conflito com UI
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        isDragging = true;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = (Vector2)transform.position - mousePos;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Encaixa no objetivo se houver
        if (objetivoAtual != null)
        {
            rb.position = objetivoAtual.position;
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPos = mousePos + offset;

            rb.MovePosition(targetPos);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Objetivo"))
        {
            ObjetivoCheck objetivo = other.GetComponent<ObjetivoCheck>();

            if (objetivo != null && objetivo.EstaDisponivelPara(this))
            {
                objetivoAtual = other.transform;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Objetivo"))
        {
            if (objetivoAtual == other.transform)
            {
                objetivoAtual = null;
            }
        }
    }

    // usado pelo botão
    public void StartDragFromMouse()
    {
        isDragging = true;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.position = mousePos;

        offset = Vector2.zero;
    }

    // usado pelo objetivo
    public bool IsDragging()
    {
        return isDragging;
    }
}