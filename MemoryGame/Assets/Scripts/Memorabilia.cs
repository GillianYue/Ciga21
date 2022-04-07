using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Memorabilia : MonoBehaviour
{
    public Animator itemDetail;
    public bool showingDetail;

    [Inject(InjectFrom.Anywhere)]
    public enabler enable;

    public GameObject ContentGO; //parent of all items
    private List<MemorabiliaItem> itemList;
    private List<string> itemTitle_eng, itemTitle_chn, itemDescrip_eng, itemDescrip_chn;
    public Text itemTitle, itemDescription;

    private int item1Rand; //0 btfl, 1 pepper btl

    public MemorabiliaItem itemOnDisplay;
    public ScrollRect scrollRect;

    public float itemMoveSeconds;
    public Image itemDetailBg1;

    public Animator mmNewItemPopUp;

    private void Awake()
    {

        itemTitle_eng = new List<string>(); itemTitle_chn = new List<string>();
        itemDescrip_eng = new List<string>(); itemDescrip_chn = new List<string>();

        itemTitle_chn.Add("奇怪的药罐"); itemTitle_chn.Add("小蝴蝶/胡椒粉"); itemTitle_chn.Add("拼接好的碎花瓶");
        itemTitle_chn.Add("松鼠"); itemTitle_chn.Add("收音机"); itemTitle_chn.Add("啤酒");
        itemTitle_chn.Add("命运之球"); itemTitle_chn.Add("活动宣传单"); itemTitle_chn.Add("一瓣橘子");
        itemTitle_chn.Add("宇宙"); itemTitle_chn.Add("花束"); itemTitle_chn.Add("眼镜");
        itemTitle_chn.Add("我们的照片");

        itemDescrip_chn.Add("医生的嘱咐是每天睡前两粒，喝温水吞服。今天...今天我吃了药了吧？"); 
        itemDescrip_chn.Add("餐厅里怎么会出现蝴蝶呢？/胡椒粉洒出来，就像小蝴蝶抖了抖翅膀上的麟粉。"); 
        itemDescrip_chn.Add("明明都放在一起了，还是会看出有缝隙。哎呀！");
        itemDescrip_chn.Add("树上的好朋友。"); itemDescrip_chn.Add("咔呲咔呲，旋钮每转一下就会出现一个新的世界。");
        itemDescrip_chn.Add("夕阳的余晖照在瓶子上，映出橙红色的反光。那天的啤酒格外好喝。");
        itemDescrip_chn.Add("后来我就时常想，那天怎么就会我在那里，它在那里，球就滚啊滚啊，滚到了她那里？"); 
        itemDescrip_chn.Add("当时那里有很多花。"); 
        itemDescrip_chn.Add("其实都不是很好吃，甚至格外地酸；但还是一瓣一瓣全部吃掉了。");
        itemDescrip_chn.Add("在这里，时间不复存在，我们也都停止了呼吸。"); 
        itemDescrip_chn.Add("一些花。"); 
        itemDescrip_chn.Add("世界朦朦胧胧的，什么都看不清了。戴上了眼镜，还是模模糊糊。");
        itemDescrip_chn.Add("我和她和它的，金色的岁月。");

        //

        itemTitle_eng.Add("Strange Pill Bottle"); itemTitle_eng.Add("Little Butterfly/Pepper"); 
        itemTitle_eng.Add("Glued Broken Vase");
        itemTitle_eng.Add("Squirrel"); itemTitle_eng.Add("Radio"); itemTitle_eng.Add("Beer");
        itemTitle_eng.Add("Ball of Fate"); itemTitle_eng.Add("Pamphlet"); itemTitle_eng.Add("A Slice of Mandarin");
        itemTitle_eng.Add("The Universe"); itemTitle_eng.Add("Bouquet"); itemTitle_eng.Add("Glasses");
        itemTitle_eng.Add("Our Photo");

        itemDescrip_eng.Add("Doctor said to take two pills daily before sleep, with warm water. I…I did take them today, I think.");
        itemDescrip_eng.Add("Where could it have come from? / Shake it, and pixie dust comes out.");
        itemDescrip_eng.Add("Even with the pieces put back together, cracks can still be seen. Sigh!");
        itemDescrip_eng.Add("A good friend on the tree."); itemDescrip_eng.Add("Every turn on the knob brings out a new world.");
        itemDescrip_eng.Add("Sunset lingers on the bottle and creates an orangey glow. The beer that day was unforgettably good.");
        itemDescrip_eng.Add("I would often look back and think, how is it that I was there that day, and the ball rolled over to her?");
        itemDescrip_eng.Add("A lot of flowers.");
        itemDescrip_eng.Add("It was rather sour, actually; still, I finished it all.");
        itemDescrip_eng.Add("Time no longer exists here, as we stopped breathing.");
        itemDescrip_eng.Add("Some flowers.");
        itemDescrip_eng.Add("Even with glasses on, the world looks blurry and unclear.");
        itemDescrip_eng.Add("This is our golden ages together.");

        //

        if (enable == null) enable = FindObjectOfType<enabler>();

        itemList = new List<MemorabiliaItem>();
        itemDetail.gameObject.SetActive(true);

        itemDetailBg1 = itemDetail.transform.Find("bg").GetComponent<Image>();

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
        GetComponent<Animator>().SetTrigger("hide");

        this.gameObject.SetActive(false); //hide UI on start
    }

    void Update()
    {

    }

    public void updateItemDetailTexts(int idx)
    {
        List<string> title = enable.language == 1 ? itemTitle_chn : itemTitle_eng,
            descrip = enable.language == 1 ? itemDescrip_chn : itemDescrip_eng;

        string ttl = title[idx], desc = descrip[idx];

        if(idx == 1)
        {
            ttl = ttl.Split('/')[item1Rand];
            desc = desc.Split('/')[item1Rand];
        }

        itemTitle.text = ttl;
        itemDescription.text = desc;

    }

    public void randomizeItem1()
    {
        if (itemList[1].unlocked)
        {
            item1Rand = Random.Range(0f, 1f) > 0.5f ? 0 : 1;

            itemList[1].item.transform.Find("btfl").gameObject.SetActive(item1Rand == 0);
            itemList[1].item.transform.Find("pepperBtl").gameObject.SetActive(item1Rand == 1);
        }
    }

    public void unlockItem(int itemIndex)
    {
        PlayerPrefs.SetInt("item" + itemIndex, 1);
        itemList[itemIndex].unlockItemVisuals();

        //change pop up image to the right one
        mmNewItemPopUp.gameObject.SetActive(true);
        Transform img = mmNewItemPopUp.transform.Find("itemImage");

        Sprite tobe;

        if(itemList[itemIndex].item.transform.childCount == 0)
        tobe = itemList[itemIndex].item.GetComponent<Image>().sprite;
        else if(itemIndex == 1)
        tobe = itemList[itemIndex].item.transform.GetChild(1).GetComponent<Image>().sprite;
        else
        tobe = itemList[itemIndex].item.transform.GetChild(0).GetComponent<Image>().sprite;



        //img.GetComponent<Image>().SetNativeSize(); //set to right size of the new sprite
        //img.transform.localScale = img.transform.localScale * 0.625f;
        img.GetComponent<RectTransform>().sizeDelta = new Vector2(tobe.rect.width * 0.625f,
            tobe.rect.height * 0.625f);

        img.GetComponent<Image>().sprite = tobe;

        //popup
        mmNewItemPopUp.Play("newMmItemUnlock");

        gameObject.SetActive(false);
        //StartCoroutine(waitAndDisablePopUp());
    }

    //disable pop up when not using it
    IEnumerator waitAndDisablePopUp()
    {
        yield return new WaitForSeconds(5);
        mmNewItemPopUp.gameObject.SetActive(false);
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
