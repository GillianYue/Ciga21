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

    private void Awake()
    {
        c = GetComponent<Canvas>();
        myItrRef = transform.Find("itemImg").GetComponent<interactable>();
    }

    void Start()
    {
        if (mm == null) mm = FindObjectOfType<Memorabilia>();

        DOTween.Init();
        DOTween.defaultAutoKill = false;
        startPosLocal = transform.localPosition;
    }

    void Update()
    {
    }

    public void itemOnClick()
    {

        if (unlocked)
        {

            if (!mm.showingDetail) //to show detail page
            {
                mm.setAllItemClickable(false);
                myItrRef.clickable = true; //all other items except this one not clickable
                mm.itemOnDisplay = this;

                //will bring item to top of itemDetail panel (else is underneath)
                c.overrideSorting = true;
                c.sortingOrder = 10;

                Tweener tweener = transform.DOMove(moveToPosGlobal, moveSeconds);
                tweener.SetUpdate(true);

                itemMoveTween = tweener;

                //show item detail
                mm.showingDetail = true;
                mm.itemDetail.Play("showItemDetails");

                mm.itemDetail.GetComponent<Image>().raycastTarget = true;
            }
            else
            { //go back to item list page
                mm.setAllItemClickable(true);
                mm.itemOnDisplay = null;

                mm.showingDetail = false;
                StartCoroutine(itemReturnToStartPos());
                mm.itemDetail.Play("hideItemDetails");
                mm.itemDetail.GetComponent<Image>().raycastTarget = false;
            }

        }

    }

    //animates item to start pos
    private IEnumerator itemReturnToStartPos()
    {
        if (itemMoveTween != null)
        {
            itemMoveTween.PlayBackwards(); //reverse/put back item
        }

        yield return new WaitForSecondsRealtime(moveSeconds);

        c.overrideSorting = false;
    }
}
