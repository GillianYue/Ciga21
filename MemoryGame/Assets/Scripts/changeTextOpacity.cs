using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeTextOpacity : MonoBehaviour
{
    public Text myText;

    void Start()
    {
        if (myText == null) myText = GetComponent<Text>();
    }

    void Update()
    {
        
    }

    public void dimText() { myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, Mathf.Min(0.6f, myText.color.a - 0.4f)); }

    public void showText() { myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, Mathf.Min(1f, myText.color.a + 0.4f)); }
}
