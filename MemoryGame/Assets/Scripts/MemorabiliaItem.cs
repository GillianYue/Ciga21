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

    public GameObject lines, item;

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

                //will bring item to top of itemDetail panel (else is underneath)
                c.overrideSorting = true;
                c.sortingOrder = 10;

                Tweener tweener = transform.DOMove(moveToPosGlobal, moveSeconds);
                tweener.SetUpdate(true);

                itemMoveTween = tweener;

                //TODO set itemDetail info here

                //show item detail
                mm.showingDetail = true;
                mm.itemDetail.Play("showItemDetails");
                

                mm.itemDetail.GetComponent<Image>().raycastTarget = true;
            }
            else
            { //go back to item list page
                StartCoroutine(itemDetailTransition(false));

                mm.setAllItemClickable(true);
                myItrRef.clickable = false;
                mm.itemOnDisplay = null;

                mm.showingDetail = false;
                StartCoroutine(itemReturnToStartPos());
                mm.itemDetail.GetComponent<Button>().enabled = false;
                mm.itemDetail.Play("hideItemDetails");
                mm.itemDetail.GetComponent<Image>().raycastTarget = false;

            }

        }

    }
    
    private IEnumerator itemDetailTransition(bool showing) 
    {
        myItrRef.clickable = false;

        if (showing)
        {
            mm.scrollRect.enabled = false;
        }
        else
        {
            mm.itemDetail.GetComponent<Button>().enabled = false;
        }

        yield return new WaitForSecondsRealtime(moveSeconds + 0.5f);

        if (showing)
        {
            mm.itemDetail.GetComponent<Button>().enabled = true;
        }
        else
        {
            mm.scrollRect.enabled = true;
        }

        myItrRef.clickable = true;
    }

    //animates item to start pos
    private IEnumerator itemReturnToStartPos()
    {
        if (itemMoveTween != null)
        {
            itemMoveTween.PlayBackwards(); //reverse/put back item
            itemMoveTween = null;
        }

        yield return new WaitForSecondsRealtime(moveSeconds);

        c.overrideSorting = false;
    }
}
