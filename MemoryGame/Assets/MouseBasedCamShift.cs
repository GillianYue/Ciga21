using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBasedCamShift : MonoBehaviour
{
    // Transform of the GameObject you want to shake (Our Camera in this case)
    public Transform shakeTransform;
    public Vector2 moveCapacity; //cam will shift at most +moveCapacity.x upward, -moveCapacity.x downward, same for y
    public Vector2 screenDimension; //width*height; will only cause the effect to be within range of screen, if outside will curb to max/min

    public bool active;
    public Vector2 startLocalPos;

    void Start()
    {
        //startCamShift();
    }


    void Update()
    {
        if (active)
        {
            Vector2 mousePos = (Vector2)Input.mousePosition - screenDimension / 2; //center point will be (0,0)

            Vector2 offset = new Vector2(moveCapacity.x * (mousePos.x / (screenDimension.x / 2)), moveCapacity.y * (mousePos.y / (screenDimension.y / 2)));

            shakeTransform.localPosition = startLocalPos + offset;
        }
    }


    public void startCamShift()
    {
        startLocalPos = shakeTransform.localPosition;
        active = true;


    }

    public void endCamShift()
    {
        active = false;
        shakeTransform.localPosition = startLocalPos;
    }


}
