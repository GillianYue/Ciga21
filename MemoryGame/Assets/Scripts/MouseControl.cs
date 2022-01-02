using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MouseControl : MonoBehaviour
{
    public Texture2D mouseCursor, handCursor;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(27, 14);

    [Inject(InjectFrom.Anywhere)]
    public globalStateStore globalState;

    public enum MouseMode { hand, cursor }
    public MouseMode mouseMode;




    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;



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
            Collider2D hit = globalState.globalUIClickOnly ? Physics2D.OverlapPoint(mouseWorldPos, LayerMask.GetMask("UI")) : 
                Physics2D.OverlapPoint(mouseWorldPos);

/*            if (globalState.globalUIClickOnly)
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the game object
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                if (results.Count != 0)
                {
                    print("hit is not null!!!!!");
                    hit = results[0].
                }
            }*/

            
            if (hit != null)
            {

                interactable itr = hit.GetComponent<interactable>();

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

                }
                else //not hovering on clickable obj
                {
                    //print("not hovering");
                    if (mouseMode == MouseMode.hand) toCursor();
                }

            }
            else //not hovering on clickable obj
            {
                //print("not hovering");
                if (mouseMode == MouseMode.hand) toCursor();
            }

        }
        else
        {
            if (mouseMode == MouseMode.hand) toCursor();
        }

    }

    public void toHand()
    {
        mouseMode = MouseMode.hand;
        Cursor.SetCursor(handCursor, hotSpot, cursorMode);

        //Cursor.SetCursor(handCursor, new Vector2(0, 0), cursorMode);
    }

    public void toCursor()
    {
        mouseMode = MouseMode.cursor;
        Cursor.SetCursor(null, hotSpot, cursorMode); //passing null will reset to system default

        //Cursor.SetCursor(null, new Vector2(0,0), cursorMode);
    }
}