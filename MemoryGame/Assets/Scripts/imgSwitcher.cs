using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imgSwitcher : MonoBehaviour
{
    //each state has two
    public Sprite[] stateImgs;

    private Image img1, img2;

    void Start()
    {
        img1 = transform.GetChild(0).GetComponent<Image>();
        img2 = transform.GetChild(1).GetComponent<Image>();
    }


    void Update()
    {
        
    }

    public void switchToImgState(int s)
    {
        int idx1 = 2 * s, idx2 = 2 * s + 1;

        img1.sprite = stateImgs[idx1];
        img2.sprite = stateImgs[idx2];
    }
}
