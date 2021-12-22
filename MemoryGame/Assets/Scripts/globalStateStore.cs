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
                else
                {
                    l1Scene = Instantiate(l1Scene_pfb, levelParent.transform); 
                }

                break;
            case 2:
                if (vaseScene != null){ vaseScene.SetActive(to);
                }
                else
                {
                    vaseScene = Instantiate(vaseScene_pfb, levelParent.transform);
                }
                break;
            case 3:
                if (subScene)
                {
                    if (treeBottomScene != null){ treeBottomScene.SetActive(to);
                    }
                    else
                    {
                        treeBottomScene = Instantiate(treeBottomScene_pfb, levelParent.transform);
                    }
                }
                else
                    if (treeScene != null){ treeScene.SetActive(to);
                    }
                    else
                    {
                        treeScene = Instantiate(treeScene_pfb, levelParent.transform);
                    }
                break;

            case 4:
                    if (bandScene != null){ bandScene.SetActive(to);
                    }
                    else
                    {
                        bandScene = Instantiate(bandScene_pfb, levelParent.transform);
                    }
                break;
            case 5:
                if (seaScene != null){ seaScene.SetActive(to);
                    }
                    else
                    {
                        seaScene = Instantiate(seaScene_pfb, levelParent.transform);
                    }
                break;
            case 6:
                if (pupScene != null){ pupScene.SetActive(to);
                    }
                    else
                    {
                        pupScene = Instantiate(pupScene_pfb, levelParent.transform);
                    }
                break;

            case 7:
                if (subScene)
                {
                    if (gardenCloseupScene != null){ gardenCloseupScene.SetActive(to);
                    }
                    else
                    {
                        gardenCloseupScene = Instantiate(gardenCloseupScene_pfb, levelParent.transform);
                    }
                break;
                }
                else
                {
                    if (gardenScene != null){ gardenScene.SetActive(to);
                        }
                        else
                        {
                            l1Scene = Instantiate(l1Scene_pfb, levelParent.transform);
                        }
                    break;
                }

            case 8:
                if (bickerScene != null){ bickerScene.SetActive(to);
                    }
                    else
                    {
                        bickerScene = Instantiate(bickerScene_pfb, levelParent.transform);
                    }
                break;
            case 9:
                if (parkScene != null){ parkScene.SetActive(to);
                }
                        else
                {
                    parkScene = Instantiate(parkScene_pfb, levelParent.transform);
                }
        break;

            case 10:
                if (graveyardScene != null){ graveyardScene.SetActive(to);
                }
                        else
                {
                    graveyardScene = Instantiate(graveyardScene_pfb, levelParent.transform);
                }
        break;
            case 11:
                if (subScene)
                {
                    if (streetScene != null){ streetScene.SetActive(to);
                    }
                    else
                    {
                        streetScene = Instantiate(streetScene_pfb, levelParent.transform);
                    }
                break;
                }
                else
                {
                    if (homeScene != null){ homeScene.SetActive(to);
                    }
                    else
                    {
                        homeScene = Instantiate(homeScene_pfb, levelParent.transform);
                    }
                break;
                }
        }
    }

    public bool checkHomeSceneItemCondition() { return penTriggered && mugTriggered && mobileTriggered; }

}
