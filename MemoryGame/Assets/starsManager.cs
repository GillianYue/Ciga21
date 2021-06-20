using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starsManager : MonoBehaviour
{
    public Animator[] stars;
    public int currActiveStarIndex;

    public globalStateStore globalState;

    void Start()
    {
        if (globalState == null) globalState = FindObjectOfType<globalStateStore>();
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

        yield return new WaitForSeconds(1);

        Transform s = globalState.seaScene.transform.Find("sea/night/starsBatch"+which);
        s.gameObject.SetActive(true);
        s.GetComponent<Animator>().SetTrigger("fadeInSlow");
        globalState.globalClickable = false;

        yield return new WaitForSeconds(3);

        globalState.globalClickable = true;

    }
}
