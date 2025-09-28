using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    Collider2D collider2D;

    public enum TrashType { Organic, Anorganic, B3 }
    public TrashType trashType;

    string[] validTags;

    void Awake()
    {
        collider2D = GetComponent<Collider2D>();

        // menyetel sampah melalui tags bedasarkan jenis sampah
        switch (trashType)
        {
            case TrashType.Organic:
                validTags = new string[] { "DropAreaOrganic" };
                break;
            case TrashType.Anorganic:
                validTags = new string[] { "DropAreaAnorganic" };
                break;
            case TrashType.B3:
                validTags = new string[] { "DropAreaB3" };
                break;
        }
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        collider2D.enabled = false;

        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;

        RaycastHit2D hitInfo;
        if (hitInfo = Physics2D.Raycast(rayOrigin, rayDirection))
        {
            foreach (string tag in validTags)
            {
                if (hitInfo.transform.CompareTag(tag))
                {
                    transform.position = hitInfo.transform.position + new Vector3(0, 0, -0.01f);
                    break;
                }
            }
        }

        collider2D.enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
