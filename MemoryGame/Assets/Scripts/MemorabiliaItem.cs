using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemorabiliaItem : MonoBehaviour
{
    static Vector3 moveToPosGlobal = new Vector3(-480, 170, -44);

    private Vector3 startPosLocal; //original start position (local) relative to parent holder
    public float moveSeconds;

    public bool unlocked;

    private Tween itemMoveTween; //keeps track of the tween that moves item from menu to details page
    public Memorabilia mm;

    private Canvas c;
    public interactable myItrRef;
    public int itemIndex;

    public GameObject lines, item; //item is the itemImg gameobject child

    private void Awake()
    {
        c = GetComponent<Canvas>();
    }

    void Start()
    {
        if (mm == null) mm = FindObjectOfType<Memorabilia>();

        DOTween.Init();
        DOTween.defaultAutoKill = false;
        startPosLocal = transform.localPosition;

        lines.SetActive(!unlocked);
        item.SetActive(unlocked);

    }

    void Update()
    {
    }

    public void unlockItemVisuals()
    {
        unlocked = true;
        lines.SetActive(false);
        item.SetActive(true);
    }

    public void itemOnClick()
    {

        if (unlocked)
        {

            if (!mm.showingDetail) //to show detail page
            {
                StartCoroutine(itemDetailTransition(true));

                mm.setAllItemClickable(false);
                mm.itemOnDisplay = this;

                //set itemDetail info here
                mm.updateItemDetailTexts(itemIndex);
                
                mm.itemDetailBg1.raycastTarget = true;
            }
            else
            { //go back to item list page
                StartCoroutine(itemDetailTransition(false));

                mm.setAllItemClickable(true);
                myItrRef.clickable = false;

                mm.itemOnDisplay = null;

                mm.itemDetailBg1.raycastTarget = false;

            }

        }

    }
    
    private IEnumerator itemDetailTransition(bool showing) 
    {
        mm.enable.globalState.globalClickable = false;

        myItrRef.clickable = false;

        if (showing)
        {
            mm.scrollRect.enabled = false;

            //the move
            Tweener tweener = transform.DOMove(moveToPosGlobal, moveSeconds);
            tweener.SetUpdate(true);

            itemMoveTween = tweener;

            yield return new WaitForSecondsRealtime(moveSeconds-0.1f);

            //will bring item to top of itemDetail panel (else is underneath)
            c.overrideSorting = true;
            c.sortingOrder = 10;

            //show item detail
            mm.showingDetail = true;
            mm.itemDetail.Play("showItemDetails");

            yield return new WaitForSecondsRealtime(0.4f);

            mm.itemDetail.GetComponent<Button>().enabled = true;
        }
        else
        {
            mm.itemDetail.GetComponent<Button>().enabled = false;
            mm.itemDetail.Play("hideItemDetails");

            yield return new WaitForSecondsRealtime(0.3f);

            c.overrideSorting = false;

            mm.showingDetail = false;
            StartCoroutine(itemReturnToStartPos());

            yield return new WaitForSecondsRealtime(moveSeconds + 0.1f);

            mm.scrollRect.enabled = true;
        }

        myItrRef.clickable = true;

        mm.enable.globalState.globalClickable = true;
    }

    //animates item to start pos
    private IEnumerator itemReturnToStartPos()
    {
        if (itemMoveTween != null)
        {
            itemMoveTween.PlayBackwards(); //reverse/put back item
            
        }

        yield return new WaitForSecondsRealtime(0.5f);

        itemMoveTween = null;
        
        yield return new WaitForSecondsRealtime(moveSeconds);

    }
}
