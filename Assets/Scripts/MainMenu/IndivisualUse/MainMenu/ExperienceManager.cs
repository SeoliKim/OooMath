using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private GameObject _ExperienceBar;
    [SerializeField] private TMP_Text _LvText;
    private Image XPimage;

    //Level info on Set Page
    [Header("Set Page Info")]
    [SerializeField] private GameObject _ExperienceBarSetPage;
    [SerializeField] private TMP_Text _LvTextSetPage, _XPCurrent, _XPTotal;
    private Image XPimageSetPage;


    private void Start() {
        User.I.lvUp += lvUp;
        User.I.XPUpdate += XPUpdate;
        PlayFabManager.PlayFabManagerInstance.FinishLoadUserData += FinishLoadUserData;
        XPimage = _ExperienceBar.GetComponent<Image>();
        XPimageSetPage = _ExperienceBarSetPage.GetComponent<Image>();
        UpdateExperienceUi();
    }

    private void OnDestroy() {
        PlayFabManager.PlayFabManagerInstance.FinishLoadUserData -= FinishLoadUserData;
        User.I.lvUp -= lvUp;
        User.I.XPUpdate -= XPUpdate;
    }

    private void FinishLoadUserData() {
        UpdateExperienceUi();
    }


    private void XPUpdate() {
        UpdateExperienceUi();
    }

    private void lvUp(int newLv) {
        UpdateExperienceUi();
    }
    private void UpdateExperienceUi() {
        int lv = User.I.GetLv();
        _LvText.text = lv.ToString();
        int experience = User.I.GetExperience();
        int preLvXP = 0;
        for (int i = 1; i < lv; i++) {
            preLvXP += (int)(90 * i + 10 * Mathf.Pow(i, 2));
        }
        float XPcurrent = experience - preLvXP;
        float XPtotal = 90 * lv + 10 * Mathf.Pow(lv, 2);
        float fillAmount = XPcurrent / XPtotal;
        XPimage.fillAmount = fillAmount;
        UpdateXPSetPage(lv, fillAmount, XPcurrent, XPtotal);
    }
    
    private void UpdateXPSetPage(int lv, float fillAmount, float XPcurrent, float XPtotal) {
        _LvTextSetPage.text = lv.ToString();
        _XPCurrent.text = XPcurrent.ToString();
        _XPTotal.text = XPtotal.ToString();
        XPimageSetPage.fillAmount = fillAmount;

    }
}
