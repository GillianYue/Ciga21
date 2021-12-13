using System.Collections;
using UnityEngine;

public class HideAndSeek : MonoBehaviour
{
    public bool[] found; //0 bench, 1 bush, 2 sunflower, 3 sign
    public Animator girl_bench, girl_bush, girl_sunflower, girl_sign, her;
    public Transform currActive;
    public float evadeDist;

    public int hideStatus = -1; //-1 inactive, 0 in hiding, 1 peeking
    public bool active;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (active)
        {

            bool mouseCloseEnough = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), currActive.position) < evadeDist;

            //if peeking and close enough 
            if (hideStatus == 1 && mouseCloseEnough)
            { //mouse too close, animate accordingly
                if (currActive.name.Equals("her_bench"))
                {
                    girl_bench.Play("girl_bench_startle");
                    hideStatus = 0;
                }
                else if (currActive.name.Equals("her_flo"))
                {
                    currActive.GetComponent<Animator>().SetTrigger("action3");
                    hideStatus = 0;
                }
            }
            else if (hideStatus == 0 && !mouseCloseEnough)
            {
                if (currActive.name.Equals("her_bench"))
                {
                    girl_bench.Play("girl_bench_peek");
                    hideStatus = 1;
                }
                else if (currActive.name.Equals("her_flo"))
                {
                    currActive.GetComponent<Animator>().SetTrigger("action2");
                    hideStatus = 1;
                }

            }

        }
    }

    public void startHideAndSeek()
    {
        StartCoroutine(hideAndSeek());
    }

    IEnumerator hideAndSeek()
    {
        active = true;

        if (!found[0])
        {
            currActive = girl_bench.transform;
            hideStatus = 1;

            currActive.gameObject.SetActive(true);
            girl_bench.Play("girl_bench_peek");
        }

        yield return new WaitUntil(() => found[0]);

        if (!found[1])
        {
            currActive = girl_sunflower.transform;
            hideStatus = 1;

            currActive.gameObject.SetActive(true);
            girl_sunflower.SetTrigger("action2"); //peek
        }

        yield return new WaitUntil(() => found[1]);

        if (!found[2])
        {
            currActive = girl_bush.transform;
            hideStatus = 1;

            //wait for p2 to activate GO
        }

        yield return new WaitUntil(() => found[2]);

        if (!found[3])
        {
            currActive = girl_sign.transform;
            hideStatus = 1;

            currActive.gameObject.SetActive(true);
        }

        yield return new WaitUntil(() => found[3]);

        her.gameObject.SetActive(true);
        her.SetTrigger("fadeIn");
        her.GetComponent<Collider2D>().enabled = true;
        her.GetComponent<interactable>().clickable = true;

    }
}
