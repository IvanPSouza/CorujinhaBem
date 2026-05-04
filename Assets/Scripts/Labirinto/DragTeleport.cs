using UnityEngine;
using UnityEngine.EventSystems;

public class DragTeleport : MonoBehaviour
{
    public MoveTampinha alvo;

    void OnMouseDown()
    {
        // Evita conflito com UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (alvo != null)
        {
            alvo.StartDragFromMouse();
        }
    }
}