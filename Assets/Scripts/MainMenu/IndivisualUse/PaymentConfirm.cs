using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaymentConfirm : MonoBehaviour
{
    [SerializeField] private TMP_Text _TextPaymentCostAmount;
    [SerializeField] private GameObject _SuccessBox, _PaymentBox;

    private void OnEnable() {
        _SuccessBox.SetActive(false);
        _PaymentBox.SetActive(true);
        PlayFabManager.PlayFabManagerInstance.SpendResult += SpendResult;
    }

    private void SpendResult(bool success) {
        PlayFabManager.PlayFabManagerInstance.SpendResult -= SpendResult;
        if (success) {
            StartCoroutine(ShowSuccess());
        } else {
            StartCoroutine(ShowNotEnough());
        }
    }
    private IEnumerator ShowSuccess() {
        _PaymentBox.SetActive(false);
        _SuccessBox.SetActive(true);
        AudioManager.AudioManagerInstance.PlayAudio("happy-4-note");
        yield return new WaitForSecondsRealtime(3f);
        gameObject.SetActive(false);
    }

    private IEnumerator ShowNotEnough() {
        VCurrencyManager.instance.Open_NotEnoughPanel(1);
        yield return null;
    }
}
