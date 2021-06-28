using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starsManager : MonoBehaviour
{
    public Animator[] stars;
    public int currActiveStarIndex;
    public CamMovement camMovement;

    public globalStateStore globalState;

    void Start()
    {
        if (globalState == null) globalState = FindObjectOfType<globalStateStore>();
        if (camMovement == null) camMovement = FindObjectOfType<CamMovement>();
    }

    void Update()
    {
        
    }

    public void startStarCheck()
    {
        if(currActiveStarIndex == 2)
        {
            //two stars lit, reveal first group of stars
            StartCoroutine(revealStarBatch(1));

        }
        else if(currActiveStarIndex == 5)
        {
            StartCoroutine(revealStarBatch(2));
        }

        if (currActiveStarIndex < stars.Length)
        {
            stars[currActiveStarIndex].gameObject.SetActive(true);
            stars[currActiveStarIndex].GetComponent<interactable>().clickable = true;

            stars[currActiveStarIndex].GetComponent<Animator>().SetInteger("shineType", Random.Range(0, 3)); //auto transitions to shine 1/2/3 after fade in upon entry

        }
        else
        {
            //last star activated, reveal third group of stars
            StartCoroutine(revealStarBatch(3));

            Animator hand = globalState.seaScene.transform.Find("hand_point").GetComponent<Animator>();
            hand.enabled = true;
            hand.Play("handWithdraw");

        }
    }

    public IEnumerator revealStarBatch(int which)
    {
        //TODO bgm
        yield return new WaitForSeconds(1);

        Transform s = globalState.seaScene.transform.Find("sea/night/starsBatch"+which);
        s.gameObject.SetActive(true);
        s.GetComponent<Animator>().SetTrigger("fadeInSlow");
        globalState.globalClickable = false;

        yield return new WaitForSeconds(3);

        globalState.globalClickable = true;

        if (which == 3)
        {//ending

            yield return new WaitForSeconds(8);



            yield return new WaitForSeconds(1);

            camMovement.camHolder.gameObject.SetActive(true);
            camMovement.camHolder.Play("camShiftRight");

            yield return new WaitForSeconds(3);

            camMovement.vfx.Play("blink");

            yield return new WaitForSeconds(1);


            //setup friends stuff
            imgSwitcher fds = globalState.seaScene.transform.Find("beach/friends_away").GetComponent<imgSwitcher>();
            fds.gameObject.SetActive(true);
            fds.GetComponent<Animator>().SetTrigger("fadeIn");

            yield return new WaitForSeconds(2);

            camMovement.vfx.Play("blink");

            yield return new WaitForSeconds(2);

            fds.switchToImgState(1);

            yield return new WaitForSeconds(2);

            fds.switchToImgState(2);

            yield return new WaitForSeconds(3);

            camMovement.vfx.Play("blink");

            yield return new WaitForSeconds(3);

            camMovement.vfx.Play("blink");

            yield return new WaitForSeconds(2);

            //end of scene
            globalState.GetComponent<BlurManager>().levelPassEffect(5);
        }
    }
}
