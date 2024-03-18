using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    //Trade Type
    //0-brain Power; 1- chips; 2- diamonds

    [SerializeField] protected int itemType;
    public int GetItemType() {
        return itemType;
    }

    [SerializeField] protected int itemAmount;
    public int GetItemAmount() {
        return itemAmount;
    }

    [SerializeField] protected int costType;
    public int GetCostType() {
        return costType;
    }

    [SerializeField] protected int costAmount;
    public int GetCostAmount() {
        return costAmount;
    }

    protected string costText;
    public string GetCostText() {
        return costText;
    }

    private void Awake() {
        string costTypeString="";
        if(costType == 1) {
            costTypeString = "Chips";
        } else if(costType == 2) {
            costTypeString = "Diamonds";
        }
        costText = "" + costAmount.ToString() + " " + costTypeString;
    }

}
