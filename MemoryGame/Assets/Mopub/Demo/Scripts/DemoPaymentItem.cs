using UnityEngine;
using System.Collections;
using MopubNS;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class DemoPaymentItem : MonoBehaviour
{
    [Serializable]
    public class PaymentItemClickEvent : UnityEvent<MopubSDKPaymentItemDetails> { }
    public PaymentItemClickEvent onPaymentItemClickEvent;

    private MopubSDKPaymentItemDetails _itemDetail;
    [HideInInspector]
    public MopubSDKPaymentItemDetails itemDetail {
        set
        {
            this._itemDetail = value;
            if (this.text == null)
            {
                this.text = GetComponentInChildren<Text>();
            }
            this.text.text = value.displayName + "\n" + value.formattedPrice;
            if(this._itemDetail == null)
            {
                return;
            }

            this.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                if (this.onPaymentItemClickEvent != null)
                {
                    this.onPaymentItemClickEvent.Invoke(this._itemDetail);
                }
            });
        }
        get
        {
            return this._itemDetail;
            
        }
    }

    private Text text;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
