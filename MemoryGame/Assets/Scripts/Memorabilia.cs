using System.Collections.Generic;
using UnityEngine;

public class Memorabilia : MonoBehaviour
{
    public Animator itemDetail;
    public bool showingDetail;

    public GameObject ContentGO; //parent of all items
    private List<MemorabiliaItem> itemList;

    public MemorabiliaItem itemOnDisplay;

    private void Awake()
    {
        itemList = new List<MemorabiliaItem>();

        foreach (Transform i in ContentGO.transform)
        {
            MemorabiliaItem itm = i.GetComponent<MemorabiliaItem>();
            itm.mm = this;
            itemList.Add(itm);

        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void setAllItemClickable(bool to)
    {
        foreach (MemorabiliaItem itm in itemList)
        {
            itm.myItrRef.clickable = to;
        }
    }
}
