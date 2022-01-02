using UnityEngine;
using UnityEngine.UI;

public class textAutoLanguage : MonoBehaviour
{

    public enabler enable;
    public string[] languageTexts; //will be used to change content of UI.Text, matches languageId in enabler
    public int[] languageTextSizes; //if -1 use default text size
    public Font[] languageFonts; //if null use default font
    public string[] mobileTexts;

    public Text myText;

    public bool changePosition;
    public Vector2 position_chn, position_eng;

    private void Awake()
    {
        if (enable == null) enable = FindObjectOfType<enabler>();
        if (myText == null) myText = GetComponent<Text>();
        if (mobileTexts.Length > 0 && enabler.isMobile()) languageTexts = mobileTexts;

    }

    void Start()
    {
        switchTextDisplayToCurrentLanguage(); //auto switch to correct language when awake
        enable.OnChangeLanguage += switchTextDisplayToCurrentLanguage; //will trigger when called
    }

    void Update()
    {

    }

    public void switchTextDisplayToCurrentLanguage()
    {
        int lang_id = enable.language;

        if (languageTexts.Length - 1 < lang_id || myText == null) return;

        myText.text = languageTexts[lang_id];

        if (changePosition) transform.localPosition = (lang_id == 0) ? position_eng : position_chn;

        if (languageTextSizes.Length - 1 >= lang_id && languageTextSizes[lang_id] != -1) myText.fontSize = languageTextSizes[lang_id];

        if (languageFonts.Length - 1 >= lang_id && languageFonts[lang_id] != null) myText.font = languageFonts[lang_id];
    }
}
