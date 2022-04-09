using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemPopupScript : MonoBehaviour
{

    public GameObject parentHolder;
    public int itemIndex;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void hideItemImage()
    {
        parentHolder.transform.GetChild(itemIndex).GetComponent<Animator>().SetTrigger("fadeOut");
    }

}
