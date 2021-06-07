using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public GameObject gameControl;
    globalStateStore globalStates;

    public Screenshake screenshake;

    Animator cam;
    public Animator vfx;

    public Vector3 destPos; //cam will always move towards this pos
    public float closeEnoughDist = 5;

    public float followSpeedPercent, linearSpeed;

    public bool followActive, //will always be following destPos
        randomOffsetActive; //natural breathing effect for cam



    private void Awake()
    {
        if (gameControl == null) gameControl = GameObject.FindGameObjectWithTag("GameController");
        if (globalStates == null) globalStates = gameControl.GetComponent<globalStateStore>();
        if (screenshake == null) screenshake = GetComponent<Screenshake>();

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

            transform.position += (Vector3) linearDelta;
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


    //non linear cam movement
    public IEnumerator moveWorldDestAccl(Vector3 dest)
    {
        bool prevFollowActive = followActive;

        followActive = true;
        destPos = new Vector3(dest.x, dest.y, destPos.z);

        float startTime = Time.time;

        yield return new WaitUntil(() => {
            if (destReached())
            {
                followActive = prevFollowActive;
                //print("time taken: " + (Time.time - startTime));
                return true;
            }
            if (Time.time - startTime > 10f)
            {
                Debug.LogError("dest not reached within 10 seconds, skipped");

                followActive = prevFollowActive;
                return true;
            }

            return false;
        });
    }

    public IEnumerator moveWorldDestAcclForSecs(Vector3 dest, float duration)
    {
        destPos = new Vector3(dest.x, dest.y, destPos.z);

        float startTime = Time.time;

        yield return new WaitForSeconds(duration);
    }



    //linear cam movement
    public static IEnumerator moveToLinearInSecs(GameObject e, int x, int y, float sec, bool[] done)
    {

        float xDist = x - e.transform.position.x;
        float yDist = y - e.transform.position.y;
        float dx = xDist / sec;
        float dy = yDist / sec;

        e.GetComponent<Rigidbody2D>().velocity = new Vector2(dx, dy);

        yield return new WaitForSeconds(sec);

        e.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); //stops the GO at dest
        done[0] = true;
    }

    public static IEnumerator moveToLinearInSecs(GameObject e, Vector2 dest, float sec, bool[] done)
    {
        return moveToLinearInSecs(e, (int)dest.x, (int)dest.y, sec, done);
    }
}
