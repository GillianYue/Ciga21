using UnityEngine;

//manages variables and objects on a global level
public class globalStateStore : MonoBehaviour
{
    public int globalCounter;
    public bool drums, guitar, accordion, hasScrolled;
    public int gardenSceneFlowerCount, mandarinConsumed, klimtRotate, leavesFall, rosesRotate, graveyardLeaves, leavesColored;
    public bool screenInteract2On, holdingPuzzlePiece, graveClicked, graveyardConditionMetTriggered;

    public bool penTriggered, mugTriggered, mobileTriggered;

    public GameObject l1Scene, vaseScene, treeScene, treeBottomScene, bandScene, seaScene, pupScene, gardenScene, gardenCloseupScene,
        bickerScene, parkScene, graveyardScene, homeScene, mirrorScene, streetScene;
    public GameObject l1Scene_pfb, vaseScene_pfb, treeScene_pfb, treeBottomScene_pfb, bandScene_pfb, seaScene_pfb, pupScene_pfb, gardenScene_pfb, gardenCloseupScene_pfb,
    bickerScene_pfb, parkScene_pfb, graveyardScene_pfb, homeScene_pfb, mirrorScene_pfb, streetScene_pfb;
    public GameObject levelParent; //canvas

    public bool globalClickable, globalUIClickOnly;

    public BlurManager blurManager;
    public enabler enable;

    public AudioManager audio;

    public bool puzzleRightClickHintShown, bandHintButtonPresented;

    public Animator interactHintAnimator;

    public int menuCapsuleSelectedIndex;

    private void Awake()
    {
        globalClickable = true;

        if (blurManager == null) blurManager = FindObjectOfType<BlurManager>();
        if (enable == null) enable = GetComponent<enabler>();
        if (audio == null) audio = GetComponent<AudioManager>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void interactHint(bool click) //if false, move hint
    {
        if (click) interactHintAnimator.Play("interactHintPopIn");
        else interactHintAnimator.Play("interactMoveHint");
    }

    //gameControl's response to triggerTargetAction() called from some other gameobject
    public void myTriggerAction()
    {
        globalCounter += 1;
    }

    public void revealAndHideStuff(int l, bool to)
    {
        revealAndHideStuff(l, to, false);
    }

    public void revealAndHideStuff(int l, bool to, bool subScene)
    {
        switch (l)
        {
            case 1:
                if (l1Scene != null)
                {
                    l1Scene.SetActive(to);
                }
                else if(to) //if null and want to set to active (instantiate)
                {
                    l1Scene = Instantiate(l1Scene_pfb, levelParent.transform);
                    l1Scene.transform.SetAsFirstSibling();
                }

                break;
            case 2:
                if (vaseScene != null){ vaseScene.SetActive(to);
                }
                else if (to)
                {
                    vaseScene = Instantiate(vaseScene_pfb, levelParent.transform);
                    vaseScene.transform.SetAsFirstSibling();
                }
                break;
            case 3:
                if (subScene)
                {
                    if (treeBottomScene != null){ treeBottomScene.SetActive(to);
                    }
                    else if (to)
                    {
                        treeBottomScene = Instantiate(treeBottomScene_pfb, levelParent.transform);
                        treeBottomScene.transform.SetAsFirstSibling();
                    }
                }
                else
                    if (treeScene != null){ treeScene.SetActive(to);
                    }
                else if (to)
                {
                        treeScene = Instantiate(treeScene_pfb, levelParent.transform);
                        treeScene.transform.SetAsFirstSibling();
                }
                break;

            case 4:
                    if (bandScene != null){ bandScene.SetActive(to);
                    }
                else if (to)
                {
                        bandScene = Instantiate(bandScene_pfb, levelParent.transform);
                        bandScene.transform.SetAsFirstSibling();
                }
                break;
            case 5:
                if (seaScene != null){ seaScene.SetActive(to);
                    }
                else if (to)
                {
                        seaScene = Instantiate(seaScene_pfb, levelParent.transform);
                        seaScene.transform.SetAsFirstSibling();
                }
                break;
            case 6:
                if (pupScene != null){ pupScene.SetActive(to);
                    }
                else if (to)
                {
                        pupScene = Instantiate(pupScene_pfb, levelParent.transform);
                        pupScene.transform.SetAsFirstSibling();
                }
                break;

            case 7:
                if (subScene)
                {
                    if (gardenCloseupScene != null){ gardenCloseupScene.SetActive(to);
                    }
                    else if (to)
                    {
                        gardenCloseupScene = Instantiate(gardenCloseupScene_pfb, levelParent.transform);
                        gardenCloseupScene.transform.SetAsFirstSibling();
                    }
                break;
                }
                else
                {
                    if (gardenScene != null){ gardenScene.SetActive(to);
                        }
                    else if (to)
                    {
                            gardenScene = Instantiate(gardenScene_pfb, levelParent.transform);
                            gardenScene.transform.SetAsFirstSibling();
                    }
                    break;
                }

            case 8:
                if (bickerScene != null){ bickerScene.SetActive(to);
                    }
                else if (to)
                {
                        bickerScene = Instantiate(bickerScene_pfb, levelParent.transform);
                        bickerScene.transform.SetAsFirstSibling();
                }
                break;
            case 9:
                if (parkScene != null){ parkScene.SetActive(to);
                }
                else if (to)
                {
                    parkScene = Instantiate(parkScene_pfb, levelParent.transform);
                    parkScene.transform.SetAsFirstSibling();
                }
        break;

            case 10:
                if (graveyardScene != null){ graveyardScene.SetActive(to);
                }
                else if (to)
                {
                    graveyardScene = Instantiate(graveyardScene_pfb, levelParent.transform);
                    graveyardScene.transform.SetAsFirstSibling();
                }
        break;
            case 11:
                if (subScene)
                {
                    if (streetScene != null){ streetScene.SetActive(to);
                    }
                    else if (to)
                    {
                        streetScene = Instantiate(streetScene_pfb, levelParent.transform);
                        streetScene.transform.SetAsFirstSibling();
                    }
                break;
                }
                else
                {
                    if (homeScene != null){ homeScene.SetActive(to);
                    }
                    else if (to)
                    {
                        homeScene = Instantiate(homeScene_pfb, levelParent.transform);
                        homeScene.transform.SetAsFirstSibling();
                    }
                break;
                }
        }
    }

    public bool checkHomeSceneItemCondition() { return penTriggered && mugTriggered && mobileTriggered; }

}
