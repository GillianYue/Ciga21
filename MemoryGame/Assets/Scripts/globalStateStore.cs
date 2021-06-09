using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages variables and objects on a global level
public class globalStateStore : MonoBehaviour
{
    public int globalCounter;
    public bool drums, guitar, accordion, hasScrolled;

    public GameObject l1Scene, vaseScene, treeScene, bandScene, seaScene, pupScene, gardenScene, gardenCloseupScene, 
        bickerScene, parkScene, graveyardScene, homeScene, mirrorScene;

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
        revealAndHideStuff(l, to, false);
    }

    public void revealAndHideStuff(int l, bool to, bool subScene)
    {
        switch (l)
        {
            case 1:
                l1Scene.SetActive(to);
                foreach (GameObject go in l1Scene.transform)
                {
                    go.SetActive(to);
                }
                break;
            case 2:
                vaseScene.SetActive(to);
                foreach (GameObject go in vaseScene.transform)
                {
                    go.SetActive(to);
                }
                break;
            case 3:
                treeScene.SetActive(to);
                foreach (GameObject go in treeScene.transform)
                {
                    go.SetActive(to);
                }
                break;

            case 4:
                bandScene.SetActive(to);
                foreach (GameObject go in bandScene.transform)
                {
                    go.SetActive(to);
                }
                break;
            case 5:
                seaScene.SetActive(to);
                foreach (GameObject go in seaScene.transform)
                {
                    go.SetActive(to);
                }
                break;
            case 6:
                pupScene.SetActive(to);
                foreach (GameObject go in pupScene.transform)
                {
                    go.SetActive(to);
                }
                break;

            case 7:
                if (subScene)
                {
                    gardenCloseupScene.SetActive(to);
                    foreach (GameObject go in gardenCloseupScene.transform)
                    {
                        go.SetActive(to);
                    }
                    break;
                }
                else
                {
                    gardenScene.SetActive(to);
                    foreach (GameObject go in gardenScene.transform)
                    {
                        go.SetActive(to);
                    }
                    break;
                }

            case 8:
                bickerScene.SetActive(to);
                foreach (GameObject go in bickerScene.transform)
                {
                    go.SetActive(to);
                }
                break;
            case 9:
                parkScene.SetActive(to);
                foreach (GameObject go in parkScene.transform)
                {
                    go.SetActive(to);
                }
                break;

            case 10:
                graveyardScene.SetActive(to);
                foreach (GameObject go in graveyardScene.transform)
                {
                    go.SetActive(to);
                }
                break;
            case 11:
                if (subScene)
                {
                    homeScene.SetActive(to);
                    foreach (GameObject go in homeScene.transform)
                    {
                        go.SetActive(to);
                    }
                    break;
                }
                else
                {
                    mirrorScene.SetActive(to);
                    foreach (GameObject go in mirrorScene.transform)
                    {
                        go.SetActive(to);
                    }
                    break;
                }
        }
    }



}
