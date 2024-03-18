using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;

public class FriendManager : MonoBehaviour {
    [Header("Friend List")]
    [SerializeField] Transform _friendListScrollContent;
    private RectTransform rT_DriendListScrollContent;
    [SerializeField] GameObject _friendInListPrefab;
     

    [Header("Search Friend Box")]
    [SerializeField] GameObject _SearchFriendBox;
    [SerializeField] GameObject _IdNamePanel, _mailPanel;
    [SerializeField] private TMP_InputField _Id_AddFriend, _mail_AddFriend;

    [Header("Search Friend Result")]
    [SerializeField] GameObject _Success_AddFriendResult;
    [SerializeField] GameObject _Fail_AddFriendResult;


    private List<FriendInfo> myFriend;

    private void Awake() {
        PlayFabManager.PlayFabManagerInstance.UpdateFriendList += ReceiveFriendList;
        PlayFabManager.PlayFabManagerInstance.addFriendResult += addFriendResult;
        SetDefault();
    }

    private void SetDefault() {
        rT_DriendListScrollContent = _friendListScrollContent.gameObject.GetComponent<RectTransform>();
        rT_DriendListScrollContent.sizeDelta = new Vector2(1500, 600);
        UpdateFriendList();
        Close_AddFriendBox();
    }

    #region Add Friend

    public void Open_AddFriendBox() {
        _SearchFriendBox.SetActive(true);
        Open_FriendSearchType_Box(_IdNamePanel);
        _Success_AddFriendResult.SetActive(false);
        _Fail_AddFriendResult.SetActive(false);
    }

    public void Close_AddFriendBox() {
        _SearchFriendBox.SetActive(false);
    }

    public void Open_FriendSearchType_Box(GameObject friendTypePanel) {
        _IdNamePanel.SetActive(false);
        _mailPanel.SetActive(false);
        friendTypePanel.SetActive(true);
    }

    public void Click_Add_AddFriend() {
        if (_Id_AddFriend.gameObject.activeSelf) {
            string id = _Id_AddFriend.text;
            PlayFabManager.PlayFabManagerInstance.AddFriend(PlayFabManager.FriendIdType.Username, id);
        }
        else if (_mail_AddFriend.gameObject.activeSelf) {
            string mail = _mail_AddFriend.text;
            PlayFabManager.PlayFabManagerInstance.AddFriend(PlayFabManager.FriendIdType.Username, mail);
        } else {
            Debug.LogError("Click_Add_AddFriend response in error");
        }

    }

    private void addFriendResult(bool success) {
        if (success) {
            AudioManager.AudioManagerInstance.PlayAudio("happy-4-note");
            StartCoroutine(ShowAddFriendResult(_Success_AddFriendResult));
            UpdateFriendList();
        } else {
            AudioManager.AudioManagerInstance.PlayAudio("wrong");
            StartCoroutine(ShowAddFriendResult(_Fail_AddFriendResult));
        }
    }

    private IEnumerator ShowAddFriendResult(GameObject result) {
        _Success_AddFriendResult.SetActive(false);
        _Fail_AddFriendResult.SetActive(false);
        result.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        result.SetActive(false);
    }

    #endregion

    #region Display Friend


    #endregion

    private void UpdateFriendList() {
        PlayFabManager.PlayFabManagerInstance.GetFriends();
    }

    private void ReceiveFriendList(List<FriendInfo> friendInfos) {
        DisplayFriend(friendInfos);
    }

   
    public void DisplayFriend(List<FriendInfo> friendInfoList) {
        if(friendInfoList.Count > 3) {
            int contentHeight = friendInfoList.Count * 170;
            rT_DriendListScrollContent.sizeDelta = new Vector2(1500, contentHeight);
        }
        foreach(FriendInfo f in friendInfoList) {
            bool isFound = false;
            if(myFriend!= null) {
                foreach (FriendInfo m in myFriend) {
                    if (f.FriendPlayFabId == m.FriendPlayFabId) {
                        isFound = true;
                    }
                }
            }
            
            if(isFound == false) {
                GameObject friendInList = Instantiate(_friendInListPrefab, _friendListScrollContent);
                string displayName= f.TitleDisplayName;
                int lv = 0;
                int bestCalcGame = 0;
                int bestRecord = 0;
                foreach (var stat in f.Profile.Statistics) {
                    if (stat.Name == "level")
                        lv = stat.Value;
                    if (stat.Name == "bestCalcGame")
                        bestCalcGame = stat.Value;
                    if (stat.Name == "bestCalcRecord")
                        bestRecord = stat.Value;
                }

                friendInList.GetComponent<FriendInListUI>().SetFriendInListUI(displayName, lv, bestCalcGame, bestRecord);
            }
            
        }
        myFriend = friendInfoList;
    }

    private void OnDestroy() {
        PlayFabManager.PlayFabManagerInstance.UpdateFriendList -= ReceiveFriendList;
        PlayFabManager.PlayFabManagerInstance.addFriendResult -= addFriendResult;
    }
}
