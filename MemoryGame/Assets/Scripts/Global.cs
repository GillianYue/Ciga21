using System;
using System.Collections;
using UnityEngine;

/**
 * contains some essential global functions that are commonly used
 */
public class Global : MonoBehaviour
{

    //~~~~~~~~~~~~~~~~~~~~~~~~~~Code Helper Methods~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /**
      * Usage: StartCoroutine(Global.Chain(...))
      * For example:
      *     StartCoroutine(Global.Chain(
      *         Global.Do(() => Debug.Log("A")),
      *         Global.WaitForSeconds(2),
      *         Global.Do(() => Debug.Log("B"))));
      */
    public static IEnumerator Chain(MonoBehaviour g, params IEnumerator[] actions)
    {
        foreach (IEnumerator action in actions)
        {
            yield return g.StartCoroutine(action);
        }
    }

    /**
     * Usage: StartCoroutine(Global.DelaySeconds(action, delay))
     * For example:
     *     StartCoroutine(Global.DelaySeconds(
     *         () => DebugUtils.Log("2 seconds past"),
     *         2);
     */
    public static IEnumerator DelaySeconds(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    public static IEnumerator WaitUntilThenDo(Action action, bool condition)
    {
        yield return new WaitUntil(() => condition);
        action();
    }

    public static IEnumerator WaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public static IEnumerator WaitUntil(bool condition)
    {
        yield return new WaitUntil(() => condition);
    }

    //Global.Do(() =>{ })
    public static IEnumerator Do(Action action)
    {
        action();
        yield return 0;
    }

}
