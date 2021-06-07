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
    public GameObject changeIntoPrefab, affectsGO; //applies when type is animThenImgChange

    public enum InteractType { animThenImgChange, anim, imgSwitcher, instrument, clickInspect };
    public InteractType interactType;

    GameObject gameControl;
    MouseControl mouseControl;
    globalStateStore globalStates;

    void Awake()
    {
        myAnimator = GetComponent<Animator>(); 

        if (gameControl == null) gameControl = GameObject.FindGameObjectWithTag("GameController");
        mouseControl = gameControl.GetComponent<MouseControl>();
        globalStates = gameControl.GetComponent<globalStateStore>();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
     //   print("clicked: " + eventData.pointerPress.name);
        if (globalStates.globalClickable && clickable) 
        {
            timesClicked += 1;
            checkBehavior();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // if (clickable) mouseControl.toHand();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      //  if (clickable) mouseControl.toMouse();
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
                myAnimator.SetTrigger("action1"); //play animation (only one in total)
                break;
            case InteractType.animThenImgChange: //play animation (multiple in total)
                if (timesClicked <= numInteractions)
                {
                    myAnimator.SetTrigger("action" + timesClicked.ToString());

                }
                break;
            case InteractType.imgSwitcher: //change base images to the next pair (with effects)
                GetComponent<imgSwitcher>().myTriggerAction();
                break;
            case InteractType.instrument: 
                if(gameControl.GetComponent<globalStateStore>().globalCounter < 2)
                {
                    //play sound effect
                    switch (name)
                    {
                        case "guitar":
                            gameControl.GetComponent<AudioManager>().playSFX(2, 0);
                            break;
                        case "drums":
                            gameControl.GetComponent<AudioManager>().playSFX(2, 1);
                            break;
                        case "accordion":
                            gameControl.GetComponent<AudioManager>().playSFX(2, 2);
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
            case InteractType.clickInspect:
                //focus on object clicked (cam focus + pos shift)
                CamMovement cam = gameControl.GetComponent<enabler>().cam;
                cam.camFocusOnObject(transform.position);


                break;

        }

    }

    //in some cases called from the end of animation; fades out current sprite and fades in newly instantiated changeIntoPrefab (e.g. butterfly -> pepper, smartphone -> telephone)
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

    //triggers actions that should be done on other game objects, action triggered based on availability of certain scripts
    public void triggerTargetAction()
    {
        if (affectsGO.GetComponent<interactable>() != null)
        {
            affectsGO.GetComponent<interactable>().myTriggerAction();
        }
        else if (affectsGO.GetComponent<imgSwitcher>() != null)
        {
            affectsGO.GetComponent<imgSwitcher>().myTriggerAction();
        }
        else
        {
            affectsGO.GetComponent<globalStateStore>().myTriggerAction();
        }
    }

    //called in animations to prevent new actions triggered during animations
    public void setClickableTrue() { clickable = true; }

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
            case "Her":
                myAnimator.Play("awayFadeIn");
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

        switch (name)
        {
            case "guitar": gs.guitar = true;
                gameControl.GetComponent<AudioManager>().playSFX(2, 9);
                break;
            case "drums": gs.drums = true; gameControl.GetComponent<AudioManager>().playSFX(2, 11); break;
            case "accordion": gs.accordion = true; gameControl.GetComponent<AudioManager>().playSFX(2, 10); break;
        }

        if(gs.drums && gs.guitar && gs.accordion && !gs.hasScrolled) //everything triggered for l2
        {
            gameControl.GetComponent<BlurManager>().levelPassEffect(2);
            gs.hasScrolled = true;
        }
    }

    public void awayFadeInFinished()
    {
        clickable = true;
        timesClicked = 1;
    }

    public void lookOverFinished()
    {
        gameControl.GetComponent<BlurManager>().levelPassEffect(3);
    }















}
