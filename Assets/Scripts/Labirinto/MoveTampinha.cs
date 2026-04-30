using UnityEngine;

public class MoveTampinha : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 offset;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        // Detecta clique em qualquer collider (inclusive trigger)
        isDragging = true;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = (Vector2)transform.position - mousePos;
    }

    void OnMouseUp()
    {
        isDragging = false;
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
}