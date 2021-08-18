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

    public enabler enable;
    private bool useAccl; //if on mobile devices, will use phone's accelerometer as input as opposed to mouse
    Matrix4x4 baseMatrix = Matrix4x4.identity;

    private void Awake()
    {
        if(!enable) enable = FindObjectOfType<enabler>();
    }

    void Start()
    {
        //startCamShift();
        scaleValuesBasedOnScreen();
        useAccl = enable.mobile;

        if (useAccl) Calibrate();
    }


    //if in mobile, calibrate baseMatrix for accl; TODO remember to call calibrate each time a new instance is activated
    public void Calibrate()
    {
        Quaternion rotate = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), Input.acceleration);

        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotate, new Vector3(1.0f, 1.0f, 1.0f));

        this.baseMatrix = matrix.inverse;
    }

    //adjusted acclerometer values
    public Vector3 AdjustedAccelerometer
    {
        get
        {
            return this.baseMatrix.MultiplyVector(Input.acceleration).normalized;
        }
    }

    void Update()
    {
        if (active)
        {
            //if (name.Equals("dark_cover")) { print(Input.mousePosition); }
            Vector2 offset = new Vector2();

            if (!useAccl)
            {
                Vector2 mousePos = (Vector2)Input.mousePosition - screenDimension / 2; //center point will be (0,0)

                mousePos = new Vector2(Mathf.Clamp(mousePos.x, -screenDimension.x / 2, screenDimension.x / 2),
                    Mathf.Clamp(mousePos.y, -screenDimension.y / 2, screenDimension.y / 2));


                offset = new Vector2(moveCapacity.x * (mousePos.x / (screenDimension.x / 2)), moveCapacity.y * (mousePos.y / (screenDimension.y / 2)));
            }
            else
            {
                Vector3 accl = AdjustedAccelerometer;

                print(Input.acceleration + " adjusted: "+ accl);

                offset = new Vector2(moveCapacity.x * accl.x, moveCapacity.y * accl.y);
            }

            shakeTransform.localPosition = startLocalPos + offset;
        }
    }

    public void scaleValuesBasedOnScreen()
    {
     //   Vector2 origScreenDimension = screenDimension,
     //      origRatio = new Vector2(moveCapacity.x / origScreenDimension.x, moveCapacity.y / origScreenDimension.y);

        screenDimension = new Vector2(Screen.width, Screen.height);
        //moveCapacity = new Vector2(screenDimension.x * origRatio.x, screenDimension.y * origRatio.y);

        //keep original moveCapacity since it stays the same regardless of screen dimension
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
