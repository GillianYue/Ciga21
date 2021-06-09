using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour
{
    public Texture2D mouseCursor, handCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public globalStateStore globalState;

    private void Awake()
    {
        if (globalState == null) globalState = FindObjectOfType<globalStateStore>();
    }

    void Update()
    {

        if (globalState.globalClickable && Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.transform != null)
            {

                interactable itr = hit.transform.GetComponent<interactable>();

                if ( itr != null)
                {
                    //object is interactable
                    print("clicked on " + hit.transform.name);
                    itr.onClick();
                }
            }
        }


    }

    public void toHand()
    {
        Cursor.SetCursor(handCursor, hotSpot, cursorMode);
    }

    public void toMouse()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}