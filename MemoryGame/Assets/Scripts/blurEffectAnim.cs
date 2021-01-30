using System.Collections;
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
    bool lerping;
    float lerpStartSize;
    public float lerpTime = 2;

    void Start()
    {
        myShader = GetComponent<Image>().material;
        setNewScale(midPoint, intensity);

        if (lerpTime == 0) lerpTime = 2;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (lerping)
        {
            myShader.SetFloat("_Size", Mathf.Lerp(lerpStartSize, midPoint, Mathf.Clamp(timer / lerpTime, 0, 1)));
            if (timer >= lerpTime || Mathf.Abs(myShader.GetFloat("_Size") - midPoint) < 0.02f)
            {
                lerping = false;
                timer = 0.5f; // to start at midpoint
            }
        }
        else
        {
            myShader.SetFloat("_Size", oscillate(timer, 30));
        }
    }


    float oscillate(float time, float speed)
    {
        return Mathf.Lerp(minScale, maxScale, Mathf.Cos(time * speed / Mathf.PI) / 2 + 0.5f);
    }

    public void setNewScale(float newScale, float fluctuation)
    {
        lerping = true;

        midPoint = newScale;
        maxScale = Mathf.Clamp(midPoint + fluctuation, 0, 12);
        minScale = Mathf.Clamp(midPoint - fluctuation, 0, 12);
        intensity = fluctuation;

        lerpStartSize = myShader.GetFloat("_Size");
        timer = 0;
    }

    public void becomeReallyBlurry()
    {
        setNewScale(3, 0.5f);
    }
}
