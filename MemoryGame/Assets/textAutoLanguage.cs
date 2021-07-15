﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textAutoLanguage : MonoBehaviour
{

    public enabler enable;
    public string[] languageTexts; //will be used to change content of UI.Text, matches languageId in enabler

    public Text myText;

    private void Awake()
    {
        if (enable == null) enable = FindObjectOfType<enabler>();
        if (myText == null) myText = GetComponent<Text>();

        switchTextDisplayToCurrentLanguage(); //auto switch to correct language when awake
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void switchTextDisplayToCurrentLanguage()
    {
        int lang_id = enable.language;

        if (languageTexts.Length - 1 < lang_id || myText == null) return;

        myText.text = languageTexts[lang_id];
    }
}