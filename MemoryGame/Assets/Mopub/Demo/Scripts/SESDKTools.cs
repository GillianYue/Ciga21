using System;
using UnityEngine;
using MopubNS;
using System.Collections;

public enum SESDKLogLevel
{
    debug,info,warning,error
}

public class SESDKTools
{
    
    public static void SELog(SESDKLogLevel level, MopubSDKError error, object msg)
    {
        var log = new System.Text.StringBuilder("SELog{ lv=");
        log.Append(level.ToString());
        if (msg != null)
        {
            log.Append(", msg=");
            log.Append(msg.ToString());
        }
        if(error != null)
        {
            log.Append(", error=");
            log.Append(error.ToString());
        }
        log.Append(" }");
        Debug.Log(log);
    }

    public static void SELogInfo(object msg)
    {
        SELog(SESDKLogLevel.info, null, msg);
    }

    public static void SELogError(MopubSDKError error, object msg)
    {

        SELog(SESDKLogLevel.error, error, msg);
    }

    public static IEnumerator ICoroutine(float time = 0, Action callBack = null, bool ignoreTimeScale = false)
    {
        if (time < 0.01)
        {
            yield return null;
        }
        else if (time > 0)
        {
            if (ignoreTimeScale)
            {
                float start = Time.realtimeSinceStartup;
                while (Time.realtimeSinceStartup < start + time)
                {
                    yield return null;
                }
            }
            else
                yield return new WaitForSeconds(time);
        }

        if (callBack != null)
        {
            callBack();
        }
    }
}
