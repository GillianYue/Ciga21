using UnityEngine;

public class edgeScroller : MonoBehaviour
{

    public int currSubsceneIndex; //-1 left, 0 center, 1 right
    public float scrollActivateDist; //if within dist to either edge, will trigger scroll
    public CamMovement camMovement;
    public Vector2 screenDimension = new Vector2(1600, 900);

    public bool active;
    public globalStateStore globalState;

    [Inject(InjectFrom.Anywhere)]
    public SwipeManager swipeManager;

    private void Awake()
    {
        if (camMovement == null) camMovement = FindObjectOfType<CamMovement>();
        if (globalState == null) globalState = FindObjectOfType<globalStateStore>();
        if (swipeManager == null) swipeManager = FindObjectOfType<SwipeManager>();

        screenDimension = new Vector2(Screen.width, Screen.height);

        swipeManager.OnSwipeLeft.AddListener(scrollTowardsRight);
        swipeManager.OnSwipeRight.AddListener(scrollTowardsLeft);
    }

    void Start()
    {

    }

    public void enableEdgeScroller()
    {
        camMovement.camHolder.GetComponent<Animator>().enabled = true;
        active = true;
        if (enabler.isMobile())
        {
            //mobile
            swipeManager.active = true;
        }
    }

    public void disableEdgeScroller()
    {
        active = false;
        swipeManager.active = false;
    }

    void FixedUpdate()
    {
        if (active && globalState.globalClickable)
        {

            Vector2 mousePos = (Vector2)Input.mousePosition - screenDimension / 2; //center point will be (0,0)

            if (Mathf.Abs(-screenDimension.x / 2 - mousePos.x) < scrollActivateDist && currSubsceneIndex > -1) //if touching left edge
            {
                scrollTowardsLeft();
            }
            else if (Mathf.Abs(screenDimension.x / 2 - mousePos.x) < scrollActivateDist && currSubsceneIndex < 2)
            {
                scrollTowardsRight();
            }

        }
    }

    public void scrollTowardsLeft()
    {
        switch (currSubsceneIndex)
        {
            case 0:
                camMovement.camHolder.Play("camShift0to-1");
                active = false;
                //print("shift left to " + currSubsceneIndex + " " + mousePos);
                break;
            case 1:
                camMovement.camHolder.Play("camShift1to0");
                active = false;
                //print("shift left to " + currSubsceneIndex + " " + mousePos);
                break;
        }
    }

    public void scrollTowardsRight()
    {
        switch (currSubsceneIndex)
        {
            case -1:
                camMovement.camHolder.Play("camShift-1to0");
                active = false;
                //print("shift right to " + currSubsceneIndex + " " + mousePos);
                break;
            case 0:
                camMovement.camHolder.Play("camShift0to1");
                active = false;
                //print("shift right to " + currSubsceneIndex + " " + mousePos);
                break;
        }
    }

}
