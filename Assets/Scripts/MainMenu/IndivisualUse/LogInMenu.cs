using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LogInMenu : MonoBehaviour
{
    [Header("Window")]
    [SerializeField] private GameObject _LogInWindow;
    [SerializeField] private GameObject _RegistryWindow;

    [Space]
    [SerializeField] private GameObject _InvalidSignUpText;
    [SerializeField] private GameObject _InvalidLogInText;

    [Header("InputField")]
    [SerializeField] private GameObject _login_NamePanel;
    [SerializeField] private GameObject _login_MailPanel;
    [SerializeField] private TMP_InputField _loginPW;
    [SerializeField] private TMP_InputField _loginName, _loginMail, _signupName, _signupmail, _signupPW;


    private void Awake() {
        _LogInWindow.SetActive(false);
        _RegistryWindow.SetActive(false);
        _InvalidLogInText.SetActive(false);
        _InvalidSignUpText.SetActive(false);
    }
    private void Start() {
        AudioManager.AudioManagerInstance.PlayMusic("menutheme");
        PlayFabManager.PlayFabManagerInstance.FinishLoadUserData += FinishLoadUserData;
        InternetPanel.instance.ReconnectToInternet += ReconnectToInternet;
        InternetManager.instance.CheckInternetConnection();
        if (!User.I.userOn) {
            TryAutomaticLogIn();
        }
       

    }

    private void ReconnectToInternet() {
        TryAutomaticLogIn();
    }

    private bool TryAutomaticLogIn() {
        if (PlayerPrefs.HasKey("LOGTYPE") && (PlayerPrefs.GetInt("LOGTYPE") == 0)) {
            PlayFabManager.PlayFabManagerInstance.LogInAsGuest();
            return true;
        } else if (PlayerPrefs.HasKey("USERNAME") && PlayerPrefs.HasKey("PASSWORD")) {
            Debug.Log(PlayerPrefs.GetString("ID"));
            PlayFabManager.PlayFabManagerInstance.LogInWPlayFabUserName(PlayerPrefs.GetString("USERNAME"), PlayerPrefs.GetString("PASSWORD"));
             return true;
        } else if (PlayerPrefs.HasKey("EMAIL") && PlayerPrefs.HasKey("PASSWORD")) {
            Debug.Log(PlayerPrefs.GetString("ID"));
            PlayFabManager.PlayFabManagerInstance.LogInWPlayFabMail(PlayerPrefs.GetString("EMAIL"), PlayerPrefs.GetString("PASSWORD"));
            return true;
        }
        return false;
    }

    private void OnDestroy() {
        PlayFabManager.PlayFabManagerInstance.LogInResultError -= LogInResultError;
        PlayFabManager.PlayFabManagerInstance.SignUpResultError -= SignUpResultError;
        PlayFabManager.PlayFabManagerInstance.FinishLoadUserData -= FinishLoadUserData;
    }

    public void GuestAccount_Click() {
        InternetManager.instance.CheckInternetConnection();
        Debug.Log("GuestAccount_Click");
        PlayFabManager.PlayFabManagerInstance.LogInAsGuest();
    }

    public void OpenLogin() {
        _LogInWindow.SetActive(true);
        Open_LoginType_Box(_login_NamePanel);
    }

    public void Open_LoginType_Box(GameObject LoginPanel) {
        _login_NamePanel.SetActive(false);
        _login_MailPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }

    private void AccountExist() {
        _RegistryWindow.SetActive(true);
        _LogInWindow.SetActive(false);
    }

    
    public void TrySignUp() {
        InternetManager.instance.CheckInternetConnection();
        string mail = _signupmail.text;
        string name = _signupName.text;
        if(name.Length < 3) {
            _InvalidSignUpText.SetActive(true);
            AudioManager.AudioManagerInstance.PlayAudio("invalid");
            return;
        }
        string password = _signupPW.text;
        if(password.Length < 6) {
            _InvalidSignUpText.SetActive(true);
            AudioManager.AudioManagerInstance.PlayAudio("invalid");
            return;
        }
        PlayFabManager.PlayFabManagerInstance.SignUp(mail, name, password);
        PlayFabManager.PlayFabManagerInstance.SignUpResultError += SignUpResultError;
    }

    private void SignUpResultError() {
        _InvalidSignUpText.SetActive(true);
        AudioManager.AudioManagerInstance.PlayAudio("invalid");
    }

    public void TryLogIn() {
        InternetManager.instance.CheckInternetConnection();
        string password = _loginPW.text;
        if (password.Length < 6) {
            _InvalidLogInText.SetActive(true);
            AudioManager.AudioManagerInstance.PlayAudio("invalid");
            return;
        }
        if (_loginName.gameObject.activeSelf) {
            string name = _loginName.text;
            if (name.Length < 3) {
                _InvalidLogInText.SetActive(true);
                AudioManager.AudioManagerInstance.PlayAudio("invalid");
                return;
            }
            PlayFabManager.PlayFabManagerInstance.LogInWPlayFabUserName(name, password);
        }else if (_loginMail.gameObject.activeSelf) {
            string mail = _loginMail.text;
            PlayFabManager.PlayFabManagerInstance.LogInWPlayFabMail(mail, password);
            PlayFabManager.PlayFabManagerInstance.LogInResultError += LogInResultError;
        }
        
    }

    private void LogInResultError() {
        AudioManager.AudioManagerInstance.PlayAudio("invalid");
        _InvalidLogInText.SetActive(true);
    }


    private void FinishLoadUserData() {
        SceneLoader.instance.LoadScene(1);//MainMenu
    }

}
