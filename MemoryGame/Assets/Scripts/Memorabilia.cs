using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memorabilia : MonoBehaviour
{
    public Animator itemDetail;
    public bool showingDetail;

    public GameObject ContentGO; //parent of all items
    private List<MemorabiliaItem> itemList;

    public MemorabiliaItem itemOnDisplay;
    public ScrollRect scrollRect;

    public float itemMoveSeconds;

    private void Awake()
    {
        itemList = new List<MemorabiliaItem>();
        itemDetail.gameObject.SetActive(true);

        int idx = 0;
        foreach (Transform i in ContentGO.transform)
        {
            MemorabiliaItem itm = i.GetComponentInChildren<MemorabiliaItem>();
            itm.itemIndex = idx;
            itm.mm = this;
            itm.item = i.transform.Find("itemImg").gameObject;
            itm.lines = i.transform.Find("lines").gameObject;
            itm.moveSeconds = itemMoveSeconds;

            itm.unlocked = PlayerPrefs.GetInt("item" + idx, 0) == 1;
            itm.myItrRef = i.transform.GetComponentInChildren<interactable>();
            itemList.Add(itm);

            idx++;
        }

        scrollRect = GetComponentInChildren<ScrollRect>();
    }

    void Start()
    {

        this.gameObject.SetActive(false); //hide UI on start
    }

    void Update()
    {

    }

    public void unlockItem(int itemIndex)
    {
        PlayerPrefs.SetInt("item" + itemIndex, 1);
        itemList[itemIndex].unlockItemVisuals();
    }

    public void setAllItemClickable(bool to)
    {
        foreach (MemorabiliaItem itm in itemList)
        {
            itm.myItrRef.clickable = to;
        }
    }

    public void onDisplayItemOnClick()
    {
        if(itemOnDisplay != null)
        itemOnDisplay.itemOnClick();
    }
}
