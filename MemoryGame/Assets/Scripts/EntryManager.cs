using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntryManager : MonoBehaviour
{
    public Animator headphoneScreen;

    private AsyncOperation asyncLoad;

    public MonoBehaviour[] activateUponSceneLoad;

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

        headphoneScreen.gameObject.SetActive(true);
        headphoneScreen.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        headphoneScreen.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        headphoneScreen.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);

        asyncLoad = SceneManager.LoadSceneAsync("main", LoadSceneMode.Additive);

        yield return new WaitForSeconds(2);

        headphoneScreen.SetTrigger("fadeIn");

        yield return new WaitForSeconds(1);

        yield return new WaitUntil(()=> asyncLoad.isDone );

        foreach(MonoBehaviour s in activateUponSceneLoad)
        {
            s.enabled = true;
        }

        headphoneScreen.SetTrigger("fadeOut");

        yield return new WaitForSeconds(3f);
    }

    void Update()
    {
        
    }
}
