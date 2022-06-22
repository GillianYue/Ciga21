using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textAutoLanguage : MonoBehaviour
{

    public enabler enable;
    public string[] languageTexts; //will be used to change content of UI.Text, matches languageId in enabler
    public int[] languageTextSizes; //if -1 use default text size
    public Font[] languageFonts; //if null use default font
    public Vector2[] positions; 
    public string[] mobileTexts;

    public Text myText;

    public bool changePosition;
    public Vector2 position_chn, position_eng;

    public int textIndex; //which row this text is in the csv file (use google sheet row index)
    public string myTextContent; 

    private void Awake()
    {
        if (enable == null) enable = FindObjectOfType<enabler>();
        if (myText == null) myText = GetComponent<Text>();
        if (mobileTexts.Length > 0 && enabler.isMobile()) languageTexts = mobileTexts;

    }

    void Start()
    {

        StartCoroutine(startCoroutine());
    }

    public IEnumerator startCoroutine()
    {

        yield return new WaitUntil(() => enable.textLoadDone);

        switchTextDisplayToCurrentLanguage(); //auto switch to correct language when awake
        enable.OnChangeLanguage += switchTextDisplayToCurrentLanguage; //will trigger when called
    }

    void Update()
    {

    }

    //fills myTextContent based on current language
    public void fillText()
    {
        int lang_id = enable.language;
        string lang = "";

        switch (lang_id)
        {
            case 0: lang = "english"; break;
            case 1: lang = "chinese"; break;
            case 2: lang = "korean"; break;
            case 3: lang = "thai"; break;
        }

        myTextContent = enable.textData[textIndex-2][lang].ToString();

        myText.text = myTextContent;
    }

    public void switchTextDisplayToCurrentLanguage()
    {

        int lang_id = enable.language;

        if (textIndex != 0) fillText();
        else
        {

            if (languageTexts.Length - 1 < lang_id || myText == null) myText.text = ""; 

            else myText.text = languageTexts[lang_id];
        }

        if (changePosition && positions.Length - 1 >= lang_id)
        {

            transform.localPosition = positions[lang_id];

        }

        if (languageTextSizes.Length - 1 >= lang_id && languageTextSizes[lang_id] != -1) myText.fontSize = languageTextSizes[lang_id];

        if (languageFonts.Length - 1 >= lang_id && languageFonts[lang_id] != null) myText.font = languageFonts[lang_id];
    }
}
