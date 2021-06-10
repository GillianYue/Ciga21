using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//follows targets in world position
public class CamMovement : MonoBehaviour
{
    public GameObject gameControl;
    globalStateStore globalStates;

    public Screenshake screenshake;
    public MouseBasedCamShift mouseBasedCamShift; //natural breathing effect for cam

    public Animator cam;
    public Animator vfx;

    public Vector3 destPos; //cam will always move towards this pos
    public float closeEnoughDist = 5;

    public float followSpeedPercent, linearSpeed;

    public bool followActive; //will always be following destPos



    private void Awake()
    {
        if (gameControl == null) gameControl = GameObject.FindGameObjectWithTag("GameController");
        if (globalStates == null) globalStates = gameControl.GetComponent<globalStateStore>();
        if (screenshake == null) screenshake = GetComponent<Screenshake>();
        if (mouseBasedCamShift == null) mouseBasedCamShift = GetComponent<MouseBasedCamShift>();

        if (cam == null) cam = transform.GetChild(0).GetComponent<Animator>();
        if (vfx == null) Debug.LogError("vfx animator not assigned");

        if (followSpeedPercent == 0) followSpeedPercent = 0.05f;

        followActive = true;
    }

    void Start()
    {

    }

    // moving cam using a mixed approach (if linear speed = 0, then fully non-linear)
    void FixedUpdate()
    {
        if (followActive)
        {
            Vector2 delta = new Vector2(), linearDelta = new Vector2();

            delta = (destPos - transform.position) * followSpeedPercent; //non-linear part
            linearDelta = (destPos - transform.position).normalized * linearSpeed; //linear part

            transform.position += (Vector3)delta;

            Vector2 distLeft = ((destPos - transform.position) * (1 - followSpeedPercent));
            if (linearDelta.magnitude > distLeft.magnitude) linearDelta = distLeft;

            transform.position += (Vector3)linearDelta;
        }
    }


    //given world pos, cam will zoom in on the object for a few secs and zoom out
    public void camFocusOnObject(Vector2 pos)
    {
        StartCoroutine(camFocusOnObjectCoroutine(pos));
    }

    IEnumerator camFocusOnObjectCoroutine(Vector2 pos)
    {
        globalStates.globalClickable = false;

        float dist = Vector2.Distance(pos, transform.position);

        float scale = dist / 85f; //the further the target, the longer is gonna take
        scale = Mathf.Max(1, scale / 4f);

        cam.Play("generalCamZoomIn");

        Vector2 originalPos = transform.position;
        //vfx.Play("blink");

        //yield return moveToLinearInSecs(gameObject, pos, 1f * scale, new bool[1]);
        yield return moveWorldDestAccl(pos);

        yield return new WaitForSeconds(3); //linger time

        cam.Play("generalCamZoomOut");
        //yield return moveToLinearInSecs(gameObject, originalPos, 1.5f * scale, new bool[1]);
        yield return moveWorldDestAccl(originalPos);
        yield return new WaitForSeconds(1);

        globalStates.globalClickable = true;
    }


    public bool destReached()
    {
        float d = Vector2.Distance(new Vector2(destPos.x, destPos.y), new Vector2(transform.position.x, transform.position.y));
        return d < closeEnoughDist;
    }

    public IEnumerator glanceAndMoveBack(Vector2 dst, float lingerTime, Vector2 returnTo)///
    {
        yield return moveWorldDestAccl(dst);
        yield return new WaitForSeconds(lingerTime);
        yield return moveWorldDestAccl(returnTo);
    }

    public IEnumerator glanceAndMoveBack(Vector2 dst, float lingerTime)
    {
        yield return glanceAndMoveBack(dst, lingerTime, new Vector2(0, 0));
    }


    //non linear cam movement (linear movement mixed in if set so)
    public IEnumerator moveWorldDestAccl(Vector3 dest)
    {
        //store current cam movement data
        CamMovementData data = new CamMovementData(followActive, mouseBasedCamShift.active, destPos);

        followActive = true;
        mouseBasedCamShift.endCamShift();

        destPos = new Vector3(dest.x, dest.y, destPos.z);

        float startTime = Time.time;

        yield return new WaitUntil(() => {
            if (destReached())
            {
                restoreData(data); //restore to prev states

                //print("time taken: " + (Time.time - startTime));
                return true;
            }
            if (Time.time - startTime > 10f)
            {
                Debug.LogError("dest not reached within 10 seconds, skipped");

                restoreData(data); //restore to prev states
                return true;
            }

            return false;
        });
    }

    public IEnumerator moveWorldDestAcclForSecs(Vector3 dest, float duration)
    {

        //store current cam movement data
        CamMovementData data = new CamMovementData(this.followActive, mouseBasedCamShift.active, destPos);


        destPos = new Vector3(dest.x, dest.y, destPos.z);

        float startTime = Time.time;

        yield return new WaitForSeconds(duration);

        restoreData(data); //restore to prev states
    }



    //linear cam movement
    public static IEnumerator moveCamToLinearInSecs(CamMovement cam, GameObject e, int x, int y, float sec, bool[] done)
    {
        //store current cam movement data
        CamMovementData data = new CamMovementData(cam.followActive, cam.mouseBasedCamShift.active, cam.destPos);


        float xDist = x - e.transform.position.x;
        float yDist = y - e.transform.position.y;
        float dx = xDist / sec;
        float dy = yDist / sec;

        e.GetComponent<Rigidbody2D>().velocity = new Vector2(dx, dy);

        yield return new WaitForSeconds(sec);

        e.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); //stops the GO at dest

        cam.restoreData(data); //restore to prev states
        done[0] = true;
    }

    public static IEnumerator moveCamToLinearInSecs(CamMovement cam, GameObject e, Vector2 dest, float sec, bool[] done)
    {
        return moveCamToLinearInSecs(cam, e, (int)dest.x, (int)dest.y, sec, done);
    }


    public void restoreData(CamMovementData dt)
    {
        followActive = dt.pFollowActive;
        if (dt.pBreatheActive) mouseBasedCamShift.startCamShift();
        if (dt.pFollowActive) destPos = dt.pDest;
    }
}

//temporarily stores data on camMovement before a (non)linear movement is about to begin
//cam movement will restore to prev settings based on this
public class CamMovementData
{
    public bool pFollowActive,
    pBreatheActive;

    public Vector3 pDest;

    public CamMovementData(bool prevFollowActive, bool prevBreatheActive, Vector3 prevDest)
    {
        pFollowActive = prevFollowActive;
        pBreatheActive = prevBreatheActive;
        pDest = prevDest;
    }


}
