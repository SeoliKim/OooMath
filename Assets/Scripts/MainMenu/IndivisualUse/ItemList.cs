using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ItemList : MonoBehaviour
{
    [SerializeField] private TMP_Text _BrainCountText, _ChipCountText, _DiamondCountText, _BrainRechargeTimeText;
    private float secondNeedBP;
    private bool brainOnRecharge;

    void Awake()
    {
        PlayFabManager.PlayFabManagerInstance.CPamount += CPamountUpdate;
        PlayFabManager.PlayFabManagerInstance.DMamount += DMamountUpdate;
        PlayFabManager.PlayFabManagerInstance.BPamount += BPamountUpdate;
        PlayFabManager.PlayFabManagerInstance.SecondNeedBP += SetSecondNeedBP;
    }

    // Update is called once per frame
    void Update()
    {
        if (brainOnRecharge) {
            secondNeedBP -= Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(secondNeedBP);
            _BrainRechargeTimeText.text = timeSpan.ToString("mm':'ss");
            if (secondNeedBP < 0) {
                PlayFabManager.PlayFabManagerInstance.TryGetVirtualCurrency();
            }
        }

    }
    private void SetSecondNeedBP(int time) {
        if(time == 0) {
            brainOnRecharge = false;
            _BrainRechargeTimeText.text = "FULL";
        }
        secondNeedBP = time;
        brainOnRecharge = true;
    }

    private void CPamountUpdate(int amount) {
        _ChipCountText.text = amount.ToString();
    }

    private void DMamountUpdate(int amount) {
        _DiamondCountText.text = amount.ToString();
    }
    private void BPamountUpdate(int amount) {
        _BrainCountText.text = amount.ToString();
    }

    private void OnDestroy() {
        PlayFabManager.PlayFabManagerInstance.CPamount -= CPamountUpdate;
        PlayFabManager.PlayFabManagerInstance.DMamount -= DMamountUpdate;
        PlayFabManager.PlayFabManagerInstance.BPamount -= BPamountUpdate;
        PlayFabManager.PlayFabManagerInstance.SecondNeedBP -= SetSecondNeedBP;
    }
}
