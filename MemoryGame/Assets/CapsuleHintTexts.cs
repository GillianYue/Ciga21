using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleHintTexts : MonoBehaviour
{
    public List<List<string>> capsuleHintTextContent_eng, capsuleHintTextContent_chn;

    private void Awake()
    {
        capsuleHintTextContent_chn = new List<List<string>>();

        List<string> l0 = new List<string>();
        l0.Add(" "); l0.Add(" "); l0.Add(" "); 
        capsuleHintTextContent_chn.Add(l0);

        List<string> l1 = new List<string>();
        l1.Add("重置本关卡并退出？"); l1.Add("好"); l1.Add("不要"); 
        capsuleHintTextContent_chn.Add(l1);

        List<string> l2 = new List<string>();
        l2.Add("重置所有进度并退出？"); l2.Add("好"); l2.Add("不要"); 
        capsuleHintTextContent_chn.Add(l2);

        List<string> l3 = new List<string>();
        l3.Add(""); l3.Add(""); l3.Add(""); 
        capsuleHintTextContent_chn.Add(l3);

        List<string> l4 = new List<string>();
        l4.Add("回到主菜单（暂停当前进度）？"); l4.Add("好"); l4.Add("不要"); 
        capsuleHintTextContent_chn.Add(l4);

        List<string> l5 = new List<string>();
        l5.Add("退出游戏吗（丢失本关卡进度）？"); l5.Add("好"); l5.Add("不要"); 
        capsuleHintTextContent_chn.Add(l5);


        capsuleHintTextContent_eng = new List<List<string>>();

        List<string> l0e = new List<string>();
        l0e.Add(" "); l0e.Add(" "); l0e.Add(" ");
        capsuleHintTextContent_eng.Add(l0e);

        List<string> l1e = new List<string>();
        l1e.Add("Reset current level and quit?"); l1e.Add("yes"); l1e.Add("no");
        capsuleHintTextContent_eng.Add(l1e);

        List<string> l2e = new List<string>();
        l2e.Add("Reset entire game and quit?"); l2e.Add("yes"); l2e.Add("no");
        capsuleHintTextContent_eng.Add(l2e);

        List<string> l3e = new List<string>();
        l3e.Add(""); l3e.Add(""); l3e.Add("");
        capsuleHintTextContent_eng.Add(l3e);

        List<string> l4e = new List<string>();
        l4e.Add("Return to main menu (pausing current game)?"); l4e.Add("yes"); l4e.Add("no");
        capsuleHintTextContent_eng.Add(l4e);

        List<string> l5e = new List<string>();
        l5e.Add("Quit game (you will lose progress on current level)?"); l5e.Add("yes"); l5e.Add("no");
        capsuleHintTextContent_eng.Add(l5e);
    }

    void Start()
    {


    }

    void Update()
    {
        
    }

    //based on capsuleIndex update text of UI
    public void updateCapsuleHint(Animator[] capsuleTexts, bool isChn, int capsuleIndex)
    {
        List<List<string>> li = isChn ? capsuleHintTextContent_chn : capsuleHintTextContent_eng;

        Text hintMsg = capsuleTexts[0].GetComponent<Text>(), yesText = capsuleTexts[1].GetComponent<Text>(), 
            noText = capsuleTexts[2].GetComponent<Text>();

        //print(li[capsuleIndex][0] + li[capsuleIndex][1] + li[capsuleIndex][2]);

        hintMsg.text = li[capsuleIndex][0];
        yesText.text = li[capsuleIndex][1];
        noText.text = li[capsuleIndex][2];
    }
}
