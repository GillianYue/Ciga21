using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class EventSystemAsyncLoad : MonoBehaviour
{
    private StandaloneInputModule _inputSystemUIInputModule;

    void Start()
    {
        _inputSystemUIInputModule = GetComponentInParent<StandaloneInputModule>();
    }

    private void OnEnable()
    {
        StartCoroutine(Co_ActivateInputComponent());
    }

    private IEnumerator Co_ActivateInputComponent()
    {
        yield return new WaitForEndOfFrame();
        _inputSystemUIInputModule.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _inputSystemUIInputModule.enabled = true;
    }
}