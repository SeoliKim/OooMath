using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinBasic : Skin
{
    [Space]
    [Header("Skin Basic")]
    [SerializeField] private TMP_Text _textCpCost;
    [SerializeField] private int CPcost;
    public int GetCPcost() {
        return CPcost;
    }
    public string GetCPcostString() {
        string CPcostString = "" + CPcost + " Chips";
        return CPcostString;
    }

    protected override void SetSkinUI() {
        _textCpCost.text = CPcost.ToString();
    }

    protected override int SetCollection() {
        return 0;
    }

  
}
