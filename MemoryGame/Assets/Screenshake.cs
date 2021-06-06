using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// screenshake is somehow v sensitive about z ordering of components
/// 
/// children of a canvas should have different z indices to prevent sprite flickering (just sorting layer somehow won't do it)
/// </summary>
public class Screenshake : MonoBehaviour
{
    // Transform of the GameObject you want to shake (Our Camera in this case)
    public Transform shakeTransform;

    // Desired duration of the shake effect
    public float shakeDuration;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed;

    public float singleTriggerChance;

    // The initial position of the GameObject
    Vector3 initialPosition;
    public bool infiniteShake = false;
    public bool active = false;

    void Awake()
    {
        if (shakeTransform == null) shakeTransform = transform.GetChild(0); //cam
        initialPosition = shakeTransform.localPosition;

        if (shakeMagnitude == 0) shakeMagnitude = 10f;
        if (dampingSpeed == 0) dampingSpeed = 1.0f;
        if (singleTriggerChance == 0) singleTriggerChance = 1f;

    }

    void Start()
    {

    }


    void Update()
    {
        if (active)
        {
            if (infiniteShake || shakeDuration > 0)
            {
                Vector3 temp = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                if(Random.Range(0f, 1.0f) < singleTriggerChance) shakeTransform.localPosition = new Vector3(Mathf.FloorToInt(temp.x), Mathf.FloorToInt(temp.y), Mathf.FloorToInt(temp.z));

                if (!infiniteShake) shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            { //shake duration up, auto deactivate
                shakeDuration = 0f;
                shakeTransform.localPosition = initialPosition;
                active = false;
            }
        }
    }

    public void TriggerInfiniteShake() { TriggerInfiniteShake(shakeMagnitude, dampingSpeed); }

    public void TriggerInfiniteShake(float magnitude, float dampingSpd)
    {
        infiniteShake = true;
        shakeMagnitude = magnitude;
        dampingSpeed = dampingSpd;
    }

    public void StopInfiniteShake() { infiniteShake = false; active = false; }
    


    /// <summary>
    /// triggers a screenShake session, will activate the script. The script auto-deactivates upon completing the duration requirement
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="magnitude"></param>
    /// <param name="dampingSpd"></param>
    public void TriggerShake(float duration, float magnitude, float dampingSpd)
    {
        active = true;

        if (duration != -1)
        {
            shakeDuration = duration;
            shakeMagnitude = magnitude;
            dampingSpeed = dampingSpd;
        }
        else
        {
            TriggerInfiniteShake(magnitude, dampingSpd);
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        TriggerShake(duration, magnitude, dampingSpeed);
    }

    public void TriggerShake(float duration)
    {
        TriggerShake(duration, shakeMagnitude, dampingSpeed);
    }

}