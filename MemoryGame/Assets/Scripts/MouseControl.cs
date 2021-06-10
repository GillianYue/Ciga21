using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour
{
    public Texture2D mouseCursor, handCursor;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public globalStateStore globalState;

    public enum MouseMode { hand, cursor }
    public MouseMode mouseMode;

    private void Awake()
    {
        if (globalState == null) globalState = FindObjectOfType<globalStateStore>();
        mouseMode = MouseMode.cursor;
    }

    void Update()
    {

        if (globalState.globalClickable)
        {

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.transform != null)
            {

                interactable itr = hit.transform.GetComponent<interactable>();

                if (itr != null && itr.clickable) //object is interactable
                {

                    //hovering on a clickable obj
                   // print("hovering on " + itr.name);
                    if (mouseMode == MouseMode.cursor) toHand();

                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        itr.onClick();
                        print("clicked on " + itr.name);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        itr.onExit();
                    }

                    return;

                }else //not hovering on clickable obj
                {
                  //  print("not hovering");
                    if (mouseMode == MouseMode.hand) toCursor();
                }


            }
            else //not hovering on clickable obj
            {
                //  print("not hovering");
                if (mouseMode == MouseMode.hand) toCursor();
            }


        }


    }

    public void toHand()
    {
        mouseMode = MouseMode.hand;
        Cursor.SetCursor(handCursor, hotSpot, cursorMode);
    }

    public void toCursor()
    {
        mouseMode = MouseMode.cursor;
        Cursor.SetCursor(null, hotSpot, cursorMode); //passing null will reset to system default
    }
}