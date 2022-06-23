using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MopubNS.ThirdParty.MiniJSON;

[Serializable]
public class DemoPlayerData
{
    public int coins = 0;

    //After the player purchases "no ads", showAD will turn to false
    public bool showAD = true;

    [SerializeField]
    private string shippedOrdersJSON = null;
    //the orders that have Shipped
    private List<string> shippedOrders = new List<string>();

    public bool WhetherContainsConsumableOrder(string order)
    {
        return this.shippedOrders.Contains(order);
    }

    public bool AddNewConsumableOrder(string order, bool saveNow = true)
    {
        if (this.WhetherContainsConsumableOrder(order))
        {
            return true;
        }
        this.shippedOrders.Add(order);
        if (saveNow)
        {
            this.SaveData();
        }
        return true;
    }

    private static string keySaveData = "DemoPlayerDataSave";

    public void SaveData()
    {
        this.shippedOrdersJSON = Json.Serialize(this.shippedOrders);
        string json = JsonUtility.ToJson(this);
        Debug.Log("SavePlayerData: "+json);
        PlayerPrefs.SetString(keySaveData, json);
    }

    public static DemoPlayerData ReadData()
    {
        string json = PlayerPrefs.GetString(keySaveData);
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }
        Debug.Log("ReadPlayerData: " + json);
        DemoPlayerData data = JsonUtility.FromJson<DemoPlayerData>(json);
        List<object> _shippedOrders = (Json.Deserialize(data.shippedOrdersJSON) as List<object>);
        if(_shippedOrders != null)
        {
            List<string> temp = new List<string>();
            foreach(object value in _shippedOrders)
            {
                temp.Add(value.ToString());
            }
            data.shippedOrders = temp;
        }
        
        return data;
    }

    public static DemoPlayerData CreateNewData()
    {
        DemoPlayerData data = new DemoPlayerData();
        data.SaveData();
        return data;
    }
    
}
