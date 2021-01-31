using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalStateStore : MonoBehaviour
{
    public int globalCounter;
    public GameObject audioL1, audioL2, audioL3;
    public GameObject[] l1Stuff, l2Stuff, l3Stuff;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

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
