using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MemorabiliaItem : MonoBehaviour
{
    public Vector3 moveToPosGlobal;
    private Vector3 startPosLocal; //original start position (local) relative to parent holder
    public float moveSeconds;

    public bool unlocked;

    private Tween itemMoveTween; //keeps track of the tween that moves item from menu to details page

    void Start()
    {
        DOTween.Init();
        DOTween.defaultAutoKill = false;
        startPosLocal = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            itemReturnToStartPos();
        }
    }

    public void itemOnClick()
    {
        
        if (unlocked)
        {
            print("button clicked, moving to " + moveToPosGlobal);

            Tweener tweener = transform.DOMove(moveToPosGlobal, moveSeconds);
            tweener.SetUpdate(true);
            
            itemMoveTween = tweener;

            //show item detail


        }
        
    }

    public void itemReturnToStartPos()
    {
        if (itemMoveTween != null)
        {
            print("returning to start pos");
            itemMoveTween.PlayBackwards(); //reverse/put back item
        }
    }
}
