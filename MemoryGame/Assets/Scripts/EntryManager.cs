using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntryManager : MonoBehaviour
{
    public Animator headphoneScreen;

    private AsyncOperation asyncLoad;

    public GameObject[] activateUponSceneLoad;
    public GameObject prevEventSystem;

    private void Awake()
    {

    }

    void Start()
    {
        StartCoroutine(headphoneScreenEntry());

    }

    IEnumerator headphoneScreenEntry()
    {
        yield return new WaitForSeconds(0);

/*        headphoneScreen.gameObject.SetActive(true);
        headphoneScreen.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        headphoneScreen.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        headphoneScreen.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);*/

        //prevEventSystem.SetActive(false);

        asyncLoad = SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);

        //yield return new WaitForSeconds(2);

        //headphoneScreen.SetTrigger("fadeIn");

        //yield return new WaitForSeconds(1);

        yield return new WaitUntil(()=> asyncLoad.isDone );

/*        if(activateUponSceneLoad.Length > 0)
        foreach (GameObject g in activateUponSceneLoad)
        {
            g.SetActive(true);
        }*/

        //headphoneScreen.SetTrigger("fadeOut");

/*        GameObject mainCamHolder = GameObject.FindGameObjectWithTag("MainCameraHolder");

        mainCamHolder.SetActive(true);
        gameObject.tag = "Untagged";

        Camera mainCam = mainCamHolder.transform.GetChild(0).GetComponent<Camera>();
        mainCam.enabled = true;

        GetComponent<Camera>().enabled = false;
        this.gameObject.SetActive(false);*/

        //Time.timeScale = 1;

        yield return new WaitForSeconds(3f);
    }

    void Update()
    {
        
    }
}
