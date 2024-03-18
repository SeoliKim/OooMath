using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _GamePanel, _DefultPage, _HomeTouch, _OptionList, _itemList, _TouchMenu;

    [Space]
    [Header("Notifucation")]
    [SerializeField] private GameObject _GoalNotification;
    [SerializeField]
    private GameObject _Achievementnotification, _DailyNotification;

    [Space]
    [Header("Account Info")]
    [SerializeField]
    private TMP_Text _IDName_SetPage;
    [SerializeField] private GameObject _LinkToAccountButton, _SignUpWindow,_InvalidSignUpText;
    [SerializeField] private TMP_InputField _signupName, _signupmail, _signupPW;

    private void Awake() {
        AwakeAllUI();
        PlayFabManager.PlayFabManagerInstance.FinishLoadAccountInfo += FinishLoadAccountInfo;
    }

    private void Start() {
        //set default page
        SetDefaultGameMenu();
        _OptionList.SetActive(true);
        _itemList.SetActive(true);
        SetDefaultTouchMenu();
        SetDefaultNotification();

        AudioManager.AudioManagerInstance.PlayMusic("MainMenuMusic");
        SetAccountInfoUI();
        CheckAccountLink();
    }

    private void Update() {
        UpdateNotification();
    }

    private void AwakeAllUI() {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>()) {
            child.gameObject.SetActive(true);
        }
    }

    public void SetDefaultGameMenu() {
        _GamePanel.SetActive(true);
        for (int i = 0; i < _GamePanel.transform.childCount; i++) {
            GameObject child = _GamePanel.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        _DefultPage.SetActive(true);
    }

    public void SetDefaultTouchMenu() {
        _HomeTouch.SetActive(false);
        _TouchMenu.SetActive(true);
        for (int i = 0; i < _TouchMenu.transform.childCount; i++) {
            GameObject child = _TouchMenu.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }

    private void SetDefaultNotification() {
        _GoalNotification.SetActive(false);
        _Achievementnotification.SetActive(false);
        _DailyNotification.SetActive(false);
    }

    private void UpdateNotification() {
        if(_Achievementnotification.activeSelf || _DailyNotification.activeSelf) {
            _GoalNotification.SetActive(true);
        } else {
            _GoalNotification.SetActive(false);
        }
    }
   

    private void OnDestroy() {
        PlayFabManager.PlayFabManagerInstance.FinishLoadUserData -= FinishLoadAccountInfo;
    }


    #region AccountInfo

    private void CheckAccountLink() {
        bool asGuest= PlayFabManager.PlayFabManagerInstance.CheckGuest();
        if (!asGuest) {
            _LinkToAccountButton.SetActive(false);
            _SignUpWindow.SetActive(false);
            return;
        } else {
            _LinkToAccountButton.SetActive(true);
            _SignUpWindow.SetActive(false);
        }
    }

    public void Click_CheckAccountLink() {
        _SignUpWindow.SetActive(true);
    }

    public void Click_SignUp_LinkAccount() {
        InternetManager.instance.CheckInternetConnection();
        string mail = _signupmail.text;
        string name = _signupName.text;
        if (name.Length < 3) {
            _InvalidSignUpText.SetActive(true);
            AudioManager.AudioManagerInstance.PlayAudio("invalid");
            return;
        }
        string password = _signupPW.text;
        if (password.Length < 6) {
            _InvalidSignUpText.SetActive(true);
            AudioManager.AudioManagerInstance.PlayAudio("invalid");
            return;
        }
        PlayFabManager.PlayFabManagerInstance.AccountLinkSuccess += AccountLinkSuccess;
        PlayFabManager.PlayFabManagerInstance.SignUpResultError += SignUpResultError;
        PlayFabManager.PlayFabManagerInstance.LinkAccount(mail, name, password);
    }

    private void SignUpResultError() {
        _InvalidSignUpText.SetActive(true);
        AudioManager.AudioManagerInstance.PlayAudio("invalid");
    }

    private void AccountLinkSuccess() {
        _SignUpWindow.SetActive(false);
        AudioManager.AudioManagerInstance.PlayAudio("happy-4-note");
    }


    private void FinishLoadAccountInfo() {
        SetAccountInfoUI();
        CheckAccountLink();
    }

    private void SetAccountInfoUI() {
        _IDName_SetPage.text = User.I.displayName;
    }

    #endregion

}
