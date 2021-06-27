using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setDateTimeTexts : MonoBehaviour
{

    public Text timeHour, timeMinute, weekday, date;

    void Start()
    {
        string tme = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm"),
            wkday = System.DateTime.UtcNow.ToLocalTime().ToString("dddd"),
            dte = System.DateTime.UtcNow.ToLocalTime().ToString("M");

        string hr = tme.Substring(0, 2), mnt = tme.Substring(3, 2);

        if (hr.Equals("00")) hr = "12";

        timeHour.text = hr;
        timeMinute.text = mnt;
        weekday.text = wkday;
        date.text = dte;
    }

}
