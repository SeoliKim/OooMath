using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinRoyal : Skin
{
    [Space]
    [Header("Skin Royal")]
    [SerializeField] private TMP_Text _textDmCost;
    [SerializeField] private int DMcost;
    public int GetDMcost() {
        return DMcost;
    }
    public string GetDMcostString() {
        string DMcostString = "" + DMcost + " Diamonds";
        return DMcostString;
    }

    protected override void SetSkinUI() {
        _textDmCost.text = DMcost.ToString();
    }

    protected override int SetCollection() {
        return 1;
    }
}
