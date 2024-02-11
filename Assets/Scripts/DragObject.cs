using UnityEngine;

public class DragObject : MonoBehaviour
{
    Vector3 mouseOffset = Vector3.zero;
    Vector3 mousePoint = Vector3.zero;
    public Transform parent;

    void OnMouseDown()
    {
        mouseOffset = parent.position - GetMouseWorldPosition();
    }

    Vector3 GetMouseWorldPosition()
    {
        //Screen coordinates(x, y)
        mousePoint = Input.mousePosition;
        mousePoint.y = 0;
        mousePoint.z = parent.position.z - Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        parent.position = GetMouseWorldPosition() + mouseOffset;
    }
}
