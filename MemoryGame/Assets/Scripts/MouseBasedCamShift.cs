using UnityEngine;

public class MouseBasedCamShift : MonoBehaviour
{
    // Transform of the GameObject you want to shake (Our Camera in this case)
    public Transform shakeTransform;
    public Vector2 moveCapacity; //cam will shift at most +moveCapacity.x upward, -moveCapacity.x downward, same for y
    public Vector2 screenDimension; //width*height; will only cause the effect to be within range of screen, if outside will curb to max/min

    public bool active;
    public Vector2 startLocalPos;

    public enabler enable;
    public bool useAccl; //if on mobile devices, will use phone's accelerometer as input as opposed to mouse
    Matrix4x4 baseMatrix = Matrix4x4.identity;

    //only edit disableMouse
    public bool disableMouse;

    public float pressTime;

    private void Awake()
    {
        if (!enable) enable = FindObjectOfType<enabler>();

        if (enabler.isMobile())
        {
            //mobile
            useAccl = true;

            if (name.Equals("dark_cover"))
            {
                moveCapacity += new Vector2(100, 300);
            }
            else if (name.Equals("newspaper_closeup"))
            {
                moveCapacity += new Vector2(200, 100);
            }
        }

       // FindObjectOfType<MopubManager>().t2.text = useAccl.ToString();
    }

    void Start()
    {
        //startCamShift();
        scaleValuesBasedOnScreen();

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

           if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    pressTime = 0; //reset press time
                }
                else
                {
                    pressTime += Time.deltaTime;
                }
            }


#if UNITY_STANDALONE
            if (!disableMouse && (!useAccl 
                //&& touch.phase == TouchPhase.Moved
                )) //mobile if touch will override accl
            {
#else
            if (!disableMouse && (!useAccl || (Input.touchCount > 0
     && pressTime > 0.3f //not a tap
                         //&& touch.phase == TouchPhase.Moved
     ) || enable.test.testMobile))  //if in editor, testing and testing mobile
     //mobile if touch will override accl
            {

#endif

                Vector2 mousePos = (Vector2)Input.mousePosition - screenDimension / 2; //center point will be (0,0)

                    mousePos = new Vector2(Mathf.Clamp(mousePos.x, -screenDimension.x / 2, screenDimension.x / 2),
                        Mathf.Clamp(mousePos.y, -screenDimension.y / 2, screenDimension.y / 2));

                    offset = new Vector2(moveCapacity.x * (mousePos.x / (screenDimension.x / 2)), moveCapacity.y * (mousePos.y / (screenDimension.y / 2)));

            }
            
            else if(useAccl){
                Vector3 accl = AdjustedAccelerometer;

                //print(Input.acceleration + " adjusted: "+ accl);

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

    public void setActive(bool a)
    {
        if (a) startCamShift();
        else endCamShift();
    }
}
