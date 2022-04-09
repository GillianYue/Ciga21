using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDependentAdjustments : MonoBehaviour
{
    public enum adjustmentType { colliderSize, enable };
    public adjustmentType myAdjustmentType;

    public float param1, param2, param3, param4; 

    void Awake()
    {
        switch (myAdjustmentType)
        {
            case adjustmentType.colliderSize:
                //param 1 pc collider size, param 2 mobile
                Collider2D myCollide = GetComponent<Collider2D>();

                CircleCollider2D circ = myCollide as CircleCollider2D;
                if(circ != null)
                {
                    circ.radius = enabler.isMobile() ? param2 : param1;
                    break;
                }

                //param 1, 2 is pc collider size x, y; 3, 4 is mobile 
                CapsuleCollider2D caps = myCollide as CapsuleCollider2D;
                if (caps != null)
                {
                    caps.size = enabler.isMobile() ? new Vector2(param1, param2) : new Vector2(param3, param4);
                    break;
                }


                break;
            case adjustmentType.enable:
                //param 1 is pc enable, param 2 is mobile enable (0 false 1 true)
                this.gameObject.SetActive(enabler.isMobile() ? (param1==1) : (param2==1));
                break;
        }
    }


    void Update()
    {
        
    }




}
