using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages variables and objects on a global level
public class globalStateStore : MonoBehaviour
{
    public int globalCounter;
    public bool drums, guitar, accordion, hasScrolled;
    public int gardenSceneFlowerCount;

    public GameObject l1Scene, vaseScene, treeScene, treeBottomScene, bandScene, seaScene, pupScene, gardenScene, gardenCloseupScene, 
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
/*                foreach (Transform go in l1Scene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;
            case 2:
                vaseScene.SetActive(to);
/*                foreach (Transform go in vaseScene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;
            case 3:
                if (subScene)
                {
                    treeBottomScene.SetActive(to);
                }else
                    treeScene.SetActive(to);
/*                foreach (Transform go in treeScene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;

            case 4:
                bandScene.SetActive(to);
/*                foreach (Transform go in bandScene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;
            case 5:
                seaScene.SetActive(to);
/*                foreach (Transform go in seaScene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;
            case 6:
                pupScene.SetActive(to);
/*                foreach (Transform go in pupScene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;

            case 7:
                if (subScene)
                {
                    gardenCloseupScene.SetActive(to);
/*                    foreach (Transform go in gardenCloseupScene.transform)
                    {
                        go.gameObject.SetActive(to);
                    }*/
                    break;
                }
                else
                {
                    gardenScene.SetActive(to);
/*                    foreach (Transform go in gardenScene.transform)
                    {
                        go.gameObject.SetActive(to);
                    }*/
                    break;
                }

            case 8:
                bickerScene.SetActive(to);
/*                foreach (Transform go in bickerScene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;
            case 9:
                parkScene.SetActive(to);
/*                foreach (Transform go in parkScene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;

            case 10:
                graveyardScene.SetActive(to);
/*                foreach (Transform go in graveyardScene.transform)
                {
                    go.gameObject.SetActive(to);
                }*/
                break;
            case 11:
                if (subScene)
                {
                    homeScene.SetActive(to);
/*                    foreach (Transform go in homeScene.transform)
                    {
                        go.gameObject.SetActive(to);
                    }*/
                    break;
                }
                else
                {
                    mirrorScene.SetActive(to);
/*                    foreach (Transform go in mirrorScene.transform)
                    {
                        go.gameObject.SetActive(to);
                    }*/
                    break;
                }
        }
    }



}
