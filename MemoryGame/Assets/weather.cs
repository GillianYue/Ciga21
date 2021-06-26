using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather : MonoBehaviour
{
    public bool lightning;
    public CamMovement camMovement;
    public Coroutine activeLightningCoroutine;

    private void Awake()
    {
        if (camMovement == null) camMovement = FindObjectOfType<CamMovement>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void startLightening()
    {
        lightning = true;
        activeLightningCoroutine = StartCoroutine(intermittentLightening());
    }

    public IEnumerator intermittentLightening()
    {
        while (lightning)
        {
            camMovement.cam.Play("camLightening");
            yield return new WaitForSeconds(Random.Range(2, 10));
        }
    }

    public void terminateLightning()
    {
        StopCoroutine(activeLightningCoroutine);
        lightning = false;
    }
}
