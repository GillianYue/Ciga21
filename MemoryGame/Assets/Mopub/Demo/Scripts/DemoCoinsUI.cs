using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemoCoinsUI : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        this.text = GetComponentInChildren<Text>();
    }

    public void coinsChanged(int coins)
    {
        this.text.text = coins.ToString();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
