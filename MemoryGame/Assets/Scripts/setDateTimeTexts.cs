using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setDateTimeTexts : MonoBehaviour
{
    public enabler enable;
    public Text timeHour, timeMinute, weekday, date, swipeUp;

    private void Awake()
    {
        if (enable == null) enable = FindObjectOfType<enabler>();
    }

    void Start()
    {
    }


    public void setDateTimeText()
    {
        string tme = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm"),
        wkday = System.DateTime.UtcNow.ToLocalTime().ToString("dddd"), //e.g. Friday
        dte = System.DateTime.UtcNow.ToLocalTime().ToString("M");

        string hr = tme.Substring(0, 2), mnt = tme.Substring(3, 2);

        if (hr.Equals("00")) hr = "12";


        timeHour.text = hr;
        timeMinute.text = mnt;


        if (enable.language == 1) //need to convert to chn
        {
            string[] md = System.DateTime.UtcNow.ToLocalTime().ToString("d").Split('/');

            string wkday_chn = "星期一", dte_chn = md[0] +"月" + md[1]+"日";

            switch (wkday)
            {
                case "Monday":
                    wkday_chn = "星期一";
                    break;
                case "Tuesday":
                    wkday_chn = "星期二";
                    break;
                case "Wednesday":
                    wkday_chn = "星期三";
                    break;
                case "Thursday":
                    wkday_chn = "星期四";
                    break;
                case "Friday":
                    wkday_chn = "星期五";
                    break;
                case "Saturday":
                    wkday_chn = "星期六";
                    break;
                case "Sunday":
                    wkday_chn = "星期日";
                    break;
            }

            //also, wkday and dte are swapped in chn
            weekday.text = dte_chn;
            date.text = wkday_chn;

            weekday.fontStyle = FontStyle.Normal;
            date.fontStyle = FontStyle.Normal;
            swipeUp.fontStyle = FontStyle.Normal;
        }
        else
        {
            weekday.text = wkday;
            date.text = dte;

            weekday.fontStyle = FontStyle.Bold;
            date.fontStyle = FontStyle.Bold;
            swipeUp.fontStyle = FontStyle.Bold;
        }

    }

}
