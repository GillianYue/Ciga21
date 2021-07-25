using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBasedCamShift : MonoBehaviour
{
    // Transform of the GameObject you want to shake (Our Camera in this case)
    public Transform shakeTransform;
    public Vector2 moveCapacity; //cam will shift at most +moveCapacity.x upward, -moveCapacity.x downward, same for y
    public Vector2 screenDimension; //width*height; will only cause the effect to be within range of screen, if outside will curb to max/min

    bool active;
    public Vector2 startLocalPos;

    void Start()
    {
        //startCamShift();
        scaleValuesBasedOnScreen();
        
    }


    void Update()
    {
        if (active)
        {
            if (name.Equals("dark_cover")) { print(Input.mousePosition); }
            Vector2 mousePos = (Vector2)Input.mousePosition - screenDimension / 2; //center point will be (0,0)

            Vector2 offset = new Vector2(moveCapacity.x * (mousePos.x / (screenDimension.x / 2)), moveCapacity.y * (mousePos.y / (screenDimension.y / 2)));

            shakeTransform.localPosition = startLocalPos + offset;
        }
    }

    public void scaleValuesBasedOnScreen()
    {
        Vector2 origScreenDimension = screenDimension,
            origRatio = new Vector2(moveCapacity.x / origScreenDimension.x, moveCapacity.y / origScreenDimension.y);

        screenDimension = new Vector2(Screen.width, Screen.height);
        moveCapacity = new Vector2(screenDimension.x * origRatio.x, screenDimension.y * origRatio.y);

    }


    public void startCamShift()
    {
        startLocalPos = shakeTransform.localPosition;
        active = true;
    }

    public void endCamShift()
    {
        if (active == true)
        {
            active = false;
            shakeTransform.localPosition = startLocalPos;
        }
    }

    public bool getActive() { return active; }

    public void setActive(bool a) {
        if (a) startCamShift();
        else endCamShift();
    }
}
