using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    // Update is called once per frame
    private double a = 0;
    void Update()
    {
        a += 0.5;
        transform.eulerAngles = new Vector3(0, 0, (float)a);
    }
}
