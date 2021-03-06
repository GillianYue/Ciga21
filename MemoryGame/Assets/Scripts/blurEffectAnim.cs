﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blurEffectAnim : MonoBehaviour
{
    private Material myShader;
    float timer = 0;
    public float midPoint;
    float minScale, maxScale;
    public float intensity; //calculated from above values
    public float fluctuateSpeed = 2;
    bool lerping;
    float lerpStartSize;
    public float lerpTime = 2;

    void Start()
    {
        myShader = GetComponent<Image>().material;
        maxScale = Mathf.Clamp(midPoint + intensity, 0, 12);
        minScale = Mathf.Clamp(midPoint - intensity, 0, 12);

        if (Mathf.Abs(myShader.GetFloat("_Size") - midPoint) > 0.02f) lerping = true;

        if (lerpTime == 0) lerpTime = 2;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (lerping)
        {
            myShader.SetFloat("_Size", Mathf.Lerp(lerpStartSize, midPoint, Mathf.Clamp(timer / lerpTime, 0, 1)));
            if (Mathf.Abs(myShader.GetFloat("_Size") - midPoint) < 0.02f)
            {
                lerping = false;
                timer = 0.5f; // to start at midpoint
             //   print("lerp done" + myShader.GetFloat("_Size") + " " + midPoint);
            }
        }
        else
        {
            float size = oscillate(timer);
            myShader.SetFloat("_Size", oscillate(timer));
        }
    }


    float oscillate(float time)
    {
        return Mathf.Lerp(minScale, maxScale, Mathf.Cos(time * fluctuateSpeed / Mathf.PI) / 2 + 0.5f);
    }

    public void setNewScale(float newScale, float fluctuation)
    {
        lerping = true;

        midPoint = newScale;
        maxScale = Mathf.Clamp(midPoint + fluctuation, 0, 20);
        minScale = Mathf.Clamp(midPoint - fluctuation, 0, 20);
        intensity = fluctuation;

        lerpStartSize = myShader.GetFloat("_Size");
        timer = 0;
    }

    public void becomeReallyBlurry()
    {
        setNewScale(3, 0.5f);
    }

    public void becomeClear()
    {
        setNewScale(0, 0.01f);
    }
}
