using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurManager : MonoBehaviour
{
    public blurEffectAnim backBlur, centerBlur; //, headBlur;
    public imgSwitcher table;
    public Animator mainCam, darkCover, her, myHand;
    public interactable leaf;

    public Animator scene3bg;
    public enabler enablr;

    void Start()
    {
        if (enablr == null) enablr = GetComponent<enabler>();
    }

    void Update()
    {

    }


    //when pasta first appears
    public void scene1Clear1()
    {
        backBlur.setNewScale(0.2f, 0.1f);
    }

    //when pasta is eaten
    public void scene1Clear2()
    {
        centerBlur.setNewScale(0.01f, 0.01f);
    }

    //plays the transition effects and proceeds to next level
    public void levelPassEffect(int level)
    {
        StartCoroutine(levelPassEffectCoroutine(level));
    }

    IEnumerator levelPassEffectCoroutine(int level)
    {
        switch (level)
        {
            case 1:
                yield return StartCoroutine(level1Clear());
                enablr.setUpLevel(2);
                break;
            case 2:
                yield return StartCoroutine(level2Clear());
                enablr.setUpLevel(3);
                break;
            case 3:
                yield return StartCoroutine(level3Clear());
                //set up level 4
                break;

            case 9:
                //
                GetComponent<enabler>().gamePass();
                break;
        }
    }


    public IEnumerator level1Clear()
    {

        backBlur.lerpTime = 0.5f;

        table.switchToImgState(1);
        yield return new WaitForSeconds(2f);

        backBlur.setNewScale(0.8f, 0.1f);

        table.switchToImgState(2);
        yield return new WaitForSeconds(0.9f);

        backBlur.setNewScale(0.1f, 0.1f);
        table.switchToImgState(3);
        yield return new WaitForSeconds(1.8f);

        table.switchToImgState(0);
        yield return new WaitForSeconds(1);

        table.switchToImgState(3);
        yield return new WaitForSeconds(0.7f);

        backBlur.setNewScale(1.8f, 0.1f);
        table.switchToImgState(0);
        yield return new WaitForSeconds(1.5f);

        table.switchToImgState(2);
        yield return new WaitForSeconds(2.4f);

        table.switchToImgState(1);
        yield return new WaitForSeconds(0.5f);

        centerBlur.setNewScale(20, 0.1f);
        yield return new WaitForSeconds(2);

        darkCover.SetTrigger("fadeIn");
        GetComponent<globalStateStore>().playSFX(3, 3);

        Destroy(GameObject.Find("Pasta(Clone)"));
        Destroy(GameObject.Find("Pepper(Clone)"));

        yield return new WaitForSeconds(3);
        centerBlur.setNewScale(0.2f, 0.1f);
        backBlur.setNewScale(0.1f, 0.1f);
    }

    IEnumerator level2Clear()
    {
        yield return new WaitForSeconds(3);

        mainCam.SetTrigger("camShift");
        yield return new WaitForSeconds(10);
        //audio stuff

        GetComponent<globalStateStore>().playSFX(2, 5);
        centerBlur.setNewScale(20, 0.1f);
        yield return new WaitForSeconds(5);

        darkCover.SetTrigger("fadeIn");
        GetComponent<globalStateStore>().playSFX(3, 3);
        yield return new WaitForSeconds(3);
        centerBlur.setNewScale(0.2f, 0.1f);
        backBlur.setNewScale(0.1f, 0.1f);

        Destroy(GameObject.Find("radio(Clone)"));
        Destroy(GameObject.Find("telephone(Clone)"));

    }

    IEnumerator level3Clear()
    {
        yield return new WaitForSeconds(0.5f);
        centerBlur.setNewScale(3, 0.1f);
        yield return new WaitForSeconds(1);
        centerBlur.setNewScale(0.2f, 0.1f);
        yield return new WaitForSeconds(1.2f);
        centerBlur.setNewScale(2.7f, 0.1f);
        yield return new WaitForSeconds(1.5f);
        centerBlur.setNewScale(0.5f, 0.1f);

        GetComponent<globalStateStore>().playSFX(3, 2);

        her.SetTrigger("clothMask" );
        yield return new WaitForSeconds(3.2f);
        //her.SetTrigger("eyeMask");
        yield return new WaitForSeconds(2);
        scene3bg.SetInteger("state", 1);
        centerBlur.setNewScale(2.7f, 0.1f);
        yield return new WaitForSeconds(0.7f);
        centerBlur.setNewScale(0.2f, 0.1f);
        scene3bg.SetInteger("state", 0);
        yield return new WaitForSeconds(2);
        scene3bg.SetTrigger("action2");
        centerBlur.setNewScale(3f, 0.1f);
        yield return new WaitForSeconds(6f);
        her.SetTrigger("special");
        centerBlur.setNewScale(0.2f, 0.1f);
        yield return new WaitForSeconds(6f);

        her.SetTrigger("clothMask");
        centerBlur.setNewScale(2.7f, 0.1f);
        yield return new WaitForSeconds(1f);
        centerBlur.setNewScale(1.2f, 0.1f);

        yield return new WaitForSeconds(3.2f);
        //her.SetTrigger("eyeMask");
        yield return new WaitForSeconds(2);
        centerBlur.setNewScale(3f, 0.1f);
        yield return new WaitForSeconds(3.1f);
        her.SetTrigger("special");
        centerBlur.setNewScale(6f, 0.1f);

        her.SetTrigger("clothMask");
        centerBlur.setNewScale(2.7f, 0.1f);
        yield return new WaitForSeconds(3f);
        centerBlur.setNewScale(1.2f, 0.1f);

        //        her.SetTrigger("eyeMask");
        yield return new WaitForSeconds(3);
        centerBlur.setNewScale(2.7f, 0.1f);
        yield return new WaitForSeconds(3);
        her.SetTrigger("specialEnd");
        centerBlur.setNewScale(2f, 0.1f);

        myHand.gameObject.SetActive(true);
        myHand.SetTrigger("action1");
        centerBlur.setNewScale(0.2f, 0.1f);
        yield return new WaitForSeconds(12);

        darkCover.SetTrigger("fadeIn");
        GetComponent<globalStateStore>().playSFX(3, 3);
        
    }


}
