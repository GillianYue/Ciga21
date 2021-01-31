using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurManager : MonoBehaviour
{
    public blurEffectAnim backBlur, centerBlur; //, headBlur;
    public imgSwitcher table;
    public Animator mainCam, darkCover;

    void Start()
    {

    }

    void Update()
    {

    }

    public void levelPassEffect(int level)
    {
        switch (level)
        {
            case 1:
                StartCoroutine(level1Clear());
                break;
            case 2:

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

        Destroy(GameObject.Find("Pasta(Clone)"));
        Destroy(GameObject.Find("Pepper(Clone)"));

        yield return new WaitForSeconds(3);
        centerBlur.setNewScale(0.2f, 0.1f);
        backBlur.setNewScale(0.1f, 0.1f);
        setUpLevel(2);
    }

    public void scene1Clear1()
    {
        backBlur.setNewScale(0.2f, 0.1f);
    }

    public void scene1Clear2()
    {
        centerBlur.setNewScale(0.01f, 0.01f);
    }

    public void setUpLevel(int l)
    {
        globalStateStore gs = GetComponent<globalStateStore>();
        gs.globalCounter = 0;
        if (l!= 1) gs.revealAndHideStuff(l-1, false);
        gs.revealAndHideStuff(l, true);

        darkCover.SetTrigger("fadeOut");
    }

    public void level2Clear()
    {
        StartCoroutine(level2ClearEffects());
    }

    IEnumerator level2ClearEffects()
    {
        yield return new WaitForSeconds(3);

        mainCam.SetTrigger("camShift");
        yield return new WaitForSeconds(6);
        //audio stuff

        centerBlur.setNewScale(20, 0.1f);
        yield return new WaitForSeconds(5);

        darkCover.SetTrigger("fadeIn");
        yield return new WaitForSeconds(3);
        centerBlur.setNewScale(0.2f, 0.1f);
        backBlur.setNewScale(0.1f, 0.1f);
        setUpLevel(3);
    }

}
