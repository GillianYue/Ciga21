using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// used for GOs with sprites that have multiple visual states
/// 
/// default starts with the first pair (state 0)
/// </summary>
public class imgSwitcher : MonoBehaviour
{
    //each state has two
    public Sprite[] stateImgs;
    public Collider2D[] stateColliders; //2d colliders for different states, if any; should be half the size of stateImgs or fewer
    public Vector2[] stateOffsets; //same as above; non-additive; is relative to the localPosition origin (0, 0)

    private Image img1, img2;
    private int currIndex = 0;

    GameObject gameControl;
    globalStateStore globalState;

    void Awake()
    {
        Transform i1 = transform.Find("Image"), i2 = transform.Find("Image (1)");

        if (i1 != null) img1 = i1.GetComponent<Image>();
        if (i2 != null) img2 = i2.GetComponent<Image>();

        gameControl = GameObject.FindGameObjectWithTag("GameController");
        globalState = gameControl.GetComponent<globalStateStore>();

        if (name.Equals("dad"))
        {
            GetComponent<Animator>().SetTrigger("action1"); //default state
        }

        if (img1 != null && img2 != null && stateImgs.Length >= 2) switchToImgState(0);
    }

    void Update()
    {

    }

    /// <param name="s"> which "pair" of images to use for this state s </param>
    public void switchToImgState(int s)
    {

        int idx1 = 2 * s, idx2 = 2 * s + 1;

        //in case img1 and 2 are not assigned on start
        if (img1 == null && transform.childCount > 0)
        {
            img1 = transform.GetChild(0).GetComponent<Image>();
        }

        if (img2 == null && transform.childCount > 1)
        {
            img2 = transform.GetChild(1).GetComponent<Image>();
        }

        //the actual swap
        if (img1 != null) img1.sprite = stateImgs[idx1];
        if (img2 != null) img2.sprite = stateImgs[idx2];
        //img1.overrideSprite = stateImgs[idx1];
        //img2.overrideSprite = stateImgs[idx2];

        //setting pivots to sprite
        if (img1 != null) syncImagePivotWithSprite(img1);
        if (img2 != null) syncImagePivotWithSprite(img2);

        //switching colliders if has different ones (if not, will keep prev collider)
        if (stateColliders.Length > s && stateColliders[s] != null)
        {
            for (int sc = 0; sc < stateColliders.Length; sc++)
            {
                if (stateColliders[sc] == null) continue;

                if (sc == s) stateColliders[sc].enabled = true;
                else stateColliders[sc].enabled = false;
            }
        }

        if (stateOffsets.Length > s)
        {
            if (img1 != null) img1.transform.localPosition = (Vector3)stateOffsets[s];
            if (img2 != null) img2.transform.localPosition = (Vector3)stateOffsets[s];
        }

    }

    public void switchToNextImgState()
    {

        if (stateImgs.Length >= (currIndex + 1) * 2 + 1)
        {
            currIndex += 1;
            switchToImgState(currIndex);
        }
    }

    public void myTriggerAction()
    {
        switch (name)
        {
            case "dad":
                if (currIndex == 1)
                {
                    GameObject pasta = GameObject.Find("Pasta(Clone)");
                    if (pasta != null) pasta.GetComponent<interactable>().clickable = true;
                    StartCoroutine(pointAtPasta());
                    currIndex = 2;
                }
                else if (currIndex == 0)
                {
                    GetComponent<Animator>().SetTrigger("action2"); //normal state
                    currIndex = 1;
                }
                else GetComponent<Animator>().SetTrigger("action4"); //smile

                break;
            case "Pasta(Clone)":
                switchToNextImgState(); //bit
                gameControl.GetComponent<AudioManager>().playSFX(1, 6);
                if (currIndex == 3) StartCoroutine(pastaFinish());
                break;
            case "sofa":
                switchToNextImgState();
                globalState.audio.playSFX(2, 4);
                GetComponent<Collider2D>().enabled = false;

                StartCoroutine(coverBlanket());

                break;
        }
    }

    IEnumerator pointAtPasta()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetTrigger("action3"); //point action
    }

    IEnumerator pastaFinish()
    {
        print("pasta finished");
        GetComponent<Collider2D>().enabled = false; //make unclickable

        yield return new WaitForSeconds(1);
        GameObject.Find("dad").GetComponent<imgSwitcher>().myTriggerAction();

        //TODO more
        gameControl.GetComponent<BlurManager>().scene1Clear2();
        yield return new WaitForSeconds(4);
        gameControl.GetComponent<BlurManager>().levelPassEffect(1);
    }

    IEnumerator coverBlanket()
    {
        yield return new WaitForSeconds(1);
        //show blanket on top of broken vase
        GameObject blkt = globalState.vaseScene.transform.Find("blanket").gameObject;

        //TODO sfx cover
        blkt.SetActive(true);
        blkt.GetComponent<Animator>().SetTrigger("fadeIn");
        globalState.audio.playSFX(2, 5);

        globalState.vaseScene.transform.Find("soccer").GetComponent<interactable>().var1 = 1; //enable next stage of interaction

    }

    //to keep different sprites of the same object appear at the same locations
    public static void syncImagePivotWithSprite(Image img)
    {
        img.SetNativeSize();

        Vector2 size = img.GetComponent<RectTransform>().sizeDelta;
        Vector2 pixelPivot = img.sprite.pivot;
        Vector2 percentPivot = new Vector2(pixelPivot.x / size.x, pixelPivot.y / size.y);
        img.GetComponent<RectTransform>().pivot = percentPivot;

    }

    public void setGlobalClickableTrue() { globalState.globalClickable = true; }

    public void setGlobalClickableFalse() { globalState.globalClickable = false; }

}
