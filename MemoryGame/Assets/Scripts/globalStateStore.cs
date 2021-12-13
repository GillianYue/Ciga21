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
                if (l1Scene != null) l1Scene.SetActive(to);
                /*                foreach (Transform go in l1Scene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;
            case 2:
                if (vaseScene != null) vaseScene.SetActive(to);
                /*                foreach (Transform go in vaseScene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;
            case 3:
                if (subScene)
                {
                    if (treeBottomScene != null) treeBottomScene.SetActive(to);
                }
                else
                    if (treeScene != null) treeScene.SetActive(to);
                /*                foreach (Transform go in treeScene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;

            case 4:
                if (bandScene != null) bandScene.SetActive(to);
                /*                foreach (Transform go in bandScene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;
            case 5:
                if (seaScene != null) seaScene.SetActive(to);
                /*                foreach (Transform go in seaScene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;
            case 6:
                if (pupScene != null) pupScene.SetActive(to);
                /*                foreach (Transform go in pupScene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;

            case 7:
                if (subScene)
                {
                    if (gardenCloseupScene != null) gardenCloseupScene.SetActive(to);
                    /*                    foreach (Transform go in gardenCloseupScene.transform)
                                        {
                                            go.gameObject.SetActive(to);
                                        }*/
                    break;
                }
                else
                {
                    if (gardenScene != null) gardenScene.SetActive(to);
                    /*                    foreach (Transform go in gardenScene.transform)
                                        {
                                            go.gameObject.SetActive(to);
                                        }*/
                    break;
                }

            case 8:
                if (bickerScene != null) bickerScene.SetActive(to);
                /*                foreach (Transform go in bickerScene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;
            case 9:
                if (parkScene != null) parkScene.SetActive(to);
                /*                foreach (Transform go in parkScene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;

            case 10:
                if (graveyardScene != null) graveyardScene.SetActive(to);
                /*                foreach (Transform go in graveyardScene.transform)
                                {
                                    go.gameObject.SetActive(to);
                                }*/
                break;
            case 11:
                if (subScene)
                {
                    if (streetScene != null) streetScene.SetActive(to);
                    /*                    foreach (Transform go in homeScene.transform)
                                        {
                                            go.gameObject.SetActive(to);
                                        }*/
                    break;
                }
                else
                {
                    if (homeScene != null) homeScene.SetActive(to);
                    /*                    foreach (Transform go in mirrorScene.transform)
                                        {
                                            go.gameObject.SetActive(to);
                                        }*/
                    break;
                }
        }
    }

    public bool checkHomeSceneItemCondition() { return penTriggered && mugTriggered && mobileTriggered; }

}
