using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{

    Animator cam;

    private void Awake()
    {
        if (cam == null) cam = GetComponent<Animator>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    //given world pos, cam will zoom in on the object for a few secs and zoom out
    public void camFocusOnObject(interactable obj, Vector2 pos)
    {
        StartCoroutine(camFocusOnObjectCoroutine(obj, pos));
    }

    IEnumerator camFocusOnObjectCoroutine(interactable obj, Vector2 pos)
    {
        obj.clickable = false;

        cam.Play("generalCamZoomIn");
        Vector2 originalPos = transform.position;
        
        yield return moveToInSecs(gameObject, pos, 2, new bool[1]);
        yield return new WaitForSeconds(2);

        cam.Play("generalCamZoomOut");
        yield return moveToInSecs(gameObject, pos, 2, new bool[1]);
        yield return new WaitForSeconds(1);

        obj.clickable = true;
    }


    public static IEnumerator moveToInSecs(GameObject e, int x, int y, float sec, bool[] done)
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

    public static IEnumerator moveToInSecs(GameObject e, Vector2 dest, float sec, bool[] done)
    {
        return moveToInSecs(e, (int)dest.x, (int)dest.y, sec, done);
    }
}
