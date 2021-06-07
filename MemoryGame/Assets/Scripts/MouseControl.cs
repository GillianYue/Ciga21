using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour
{
    public Texture2D mouseCursor, handCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    public void toHand()
    {
        Cursor.SetCursor(handCursor, hotSpot, cursorMode);
    }

    public void toMouse()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}