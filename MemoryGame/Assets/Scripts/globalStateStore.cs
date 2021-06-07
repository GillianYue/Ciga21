using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages variables and objects on a global level
public class globalStateStore : MonoBehaviour
{
    public int globalCounter;
    public bool drums, guitar, accordion, hasScrolled;

    public GameObject[] l1Stuff, l2Stuff, l3Stuff;

    public bool globalClickable;

    private void Awake()
    {
        globalClickable = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //gameControl's response to triggerTargetAction() called from some other gameobject
    public void myTriggerAction()
    {
        globalCounter += 1;
    }

    public void revealAndHideStuff(int l, bool to)
    {
        switch (l)
        {
            case 1:
                foreach (GameObject go in l1Stuff)
                {
                    go.SetActive(to);
                }
                break;
            case 2:
                foreach (GameObject go in l2Stuff)
                {
                    go.SetActive(to);
                }
                break;
            case 3:
                foreach (GameObject go in l3Stuff)
                {
                    go.SetActive(to);
                }
                break;
        }
    }



}
