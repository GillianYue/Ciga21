using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class interactable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int timesClicked = 0; //this should keep track of effective clicks
    public bool clickable = true;
    Animator myAnimator;
    public int numInteractions;
    public GameObject changeIntoPrefab, relevantGO; //applies when type is animThenImgChange

    public enum InteractType { animThenImgChange, anim, imgSwitcher, instrument };
    public InteractType interactType;
    MouseControl mouseControl;
    GameObject gameControl;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        gameControl = GameObject.FindGameObjectWithTag("GameController");
        mouseControl = gameControl.GetComponent<MouseControl>();
    }

    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        print("clicked: " + eventData.pointerPress.name);
        if (clickable)
        {
            timesClicked += 1;
            checkBehavior();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseControl.toHand();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseControl.toMouse();
    }

    public void fadeIn()
    {
        myAnimator.SetTrigger("fadeIn");
    }

    public void fadeOut()
    {
        myAnimator.SetTrigger("fadeOut");
    }

    public static void fadeInSth(Animator a)
    {
        a.SetTrigger("fadeIn");
    }

    public static void fadeOutSth(Animator a)
    {
        a.SetTrigger("fadeOut");
    }

    //should be overidden by children?
    public void checkBehavior()
    {

        switch (interactType)
        {
            case InteractType.anim:
                myAnimator.SetTrigger("action1");
                break;
            case InteractType.animThenImgChange:
                if (timesClicked <= numInteractions)
                {
                    print("checking behavior: " + timesClicked);
                    myAnimator.SetTrigger("action" + timesClicked.ToString());

                }
                break;
            case InteractType.imgSwitcher:
                GetComponent<imgSwitcher>().myTriggerAction();
                break;
            case InteractType.instrument:
                if(gameControl.GetComponent<globalStateStore>().globalCounter < 2)
                {
                    //play sound effect
                    switch (name)
                    {
                        case "guitar":
                            gameControl.GetComponent<globalStateStore>().audioL2.GetComponents<AudioSource>()[0].Play();
                            break;
                        case "drums":
                            gameControl.GetComponent<globalStateStore>().audioL2.GetComponents<AudioSource>()[1].Play();
                            break;
                        case "accordion":
                            gameControl.GetComponent<globalStateStore>().audioL2.GetComponents<AudioSource>()[2].Play();
                            break;
                    }
                    timesClicked -= 1;
                }
                else
                {
                    if (timesClicked < numInteractions)
                    {
                        myAnimator.SetTrigger("action" + timesClicked.ToString());
                        //play sound effect
                    }
                    else
                    {
                        instrumentStartPlaying();
                        //sound
                    }
                }

                break;
        }

    }

    public GameObject swapSpriteToTarget()
    {
        GameObject target = Instantiate(changeIntoPrefab, transform.parent, false);
        target.transform.localPosition = transform.localPosition;
        target.SetActive(false);
        gameControl.GetComponent<BlurManager>().centerBlur.transform.SetAsLastSibling();
        setChildrenInvisible(target);

        myAnimator.SetTrigger("fadeOut");
        target.GetComponent<Animator>().SetTrigger("fadeIn");
        return target;
    }

    public void swapSpriteToTargetAndSetStateToOne()
    {
        GameObject target = swapSpriteToTarget();
        target.GetComponent<Animator>().SetInteger("state", 1);
    }

    public void triggerTargetAction()
    {
        if (relevantGO.GetComponent<interactable>() != null)
        {
            relevantGO.GetComponent<interactable>().myTriggerAction();
        }
        else if (relevantGO.GetComponent<imgSwitcher>() != null)
        {
            relevantGO.GetComponent<imgSwitcher>().myTriggerAction();
        }
        else
        {
            relevantGO.GetComponent<globalStateStore>().myTriggerAction();
        }
    }

    public void setClickableTrue() { clickable = true; print("actually being set"); }

    public void setClickableFalse() { clickable = false; }

    static void setChildrenInvisible(GameObject obj)
    {
        Color col0 = obj.transform.GetChild(0).GetComponent<Image>().color,
            col1 = obj.transform.GetChild(1).GetComponent<Image>().color;

        obj.transform.GetChild(0).GetComponent<Image>().color = new Color(col0.r, col0.g, col0.b, 0);
        obj.transform.GetChild(1).GetComponent<Image>().color = new Color(col1.r, col1.g, col1.b, 0);

        obj.SetActive(true);
    }


    //this gets called by other interactables, indicates some activity, should be overidden
    public void myTriggerAction()
    {
        switch (name)
        {
            case "Snakes":
                print("fork triggered snake");
                StartCoroutine(snakeDisappear());

                break;

        }
    }

    IEnumerator snakeDisappear()
    {
        myAnimator.SetTrigger("action1"); //hiss
        yield return new WaitForSeconds(2);
        GameObject pasta = swapSpriteToTarget();
        yield return new WaitForSeconds(3);
        gameControl.GetComponent<BlurManager>().scene1Clear1();
        gameControl.GetComponent<BlurManager>().backBlur.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void instrumentStartPlaying()
    {
        globalStateStore gs = gameControl.GetComponent<globalStateStore>(); 

        myAnimator.SetInteger("state", 2);
        gs.globalCounter += 1;

        if(gs.globalCounter >= 5) //everything triggered for l2
        {
            gameControl.GetComponent<BlurManager>().level2Clear();
        }
    }
}
