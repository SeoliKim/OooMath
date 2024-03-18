using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;

public class PlayFabManager : MonoBehaviour
{
    #region singleton instance
    public static PlayFabManager PlayFabManagerInstance;
    private string username;

    private void Awake() {
        if (PlayFabManagerInstance != null && PlayFabManagerInstance != this) {
            Destroy(this);
        } else {
            PlayFabManagerInstance = this;
        }
    }
    #endregion

    #region LogIn

    public event Action SignUpResultError;
    public event Action LogInSuccesswPlayFab;
    public event Action LogInResultError;
    public event Action FinishLoadAccountInfo;
    public event Action FinishLoadUserData;
    public event Action AccountLinkSuccess;

    string email=null;
    string password=null;
    string userName = null;
    private bool asGuest;
    public bool CheckGuest() {
        return asGuest;
    }


    public void LogInAsGuest() {
#if UNITY_ANDROID
        var androidRequest = new LoginWithAndroidDeviceIDRequest {
            AndroidDeviceId = ReturnMobileID(),
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(androidRequest, Guest_LogInSuccess, LogInError);
#endif

#if UNITY_IOS
        var iosRequest = new LoginWithIOSDeviceIDRequest {
            DeviceId= ReturnMobileID(),
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithIOSDeviceID(iosRequest, Guest_LogInSuccess, LogInError);
#endif
        Debug.Log("log in as guest");
    }
    public static string ReturnMobileID() {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        Debug.Log("mobile ID" + deviceID);
        return deviceID;
    }
    void Guest_LogInSuccess(LoginResult result) {
        string Id = result.PlayFabId;
        Debug.Log(Id + "log in success");
        //LogInSuccesswPlayFab?.Invoke();
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), GetGuestAccountInfoSuccesss, error => {
            Debug.LogError("Fail to get account Info");
        });
        TryLoadUserData(Id);
        TryGetVirtualCurrency();
    }
    void GetGuestAccountInfoSuccesss(GetAccountInfoResult result) {
        User.I.titleID = result.AccountInfo.TitleInfo.TitlePlayerAccount.Id;
        PlayerPrefs.SetString("ID", User.I.titleID);
        User.I.displayName = "Guest";
        asGuest = true;
        PlayerPrefs.SetInt("LOGTYPE", 0);
        User.I.logType = 0;
        FinishLoadAccountInfo?.Invoke();
    }

    public void SignUp(string mail, string username, string password) {
        this.password = password;
        PlayFabClientAPI.RegisterPlayFabUser(
                new RegisterPlayFabUserRequest() {
                    Email = mail,
                    Username = username,
                    Password = password,
                    RequireBothUsernameAndEmail = true
                },
                OnSuccess => {
                    Debug.Log("Sign Up Success"+mail + username + "create new account");
                    SetDisplayName(username);
                    LogInWPlayFabUserName(username, password);
                },
                SignUpError);
    }

    void SignUpError(PlayFabError error) {
        SignUpResultError?.Invoke();
        Debug.Log("Error while login");
        Debug.Log(error.GenerateErrorReport());
    }

    public void LogInWPlayFabUserName(string username, string password) {
        this.password = password;
        this.username = username;
        PlayFabClientAPI.LoginWithPlayFab(
                new LoginWithPlayFabRequest() {
                    Username = username,
                    Password = password,
                }, LogInSuccessResult,
                LogInError
        );
    }

    public void LogInWPlayFabMail(string mail, string password) {
        this.password = password;
        this.email = mail;
        PlayFabClientAPI.LoginWithEmailAddress(
                new LoginWithEmailAddressRequest {
                    Email = mail,
                    Password = password,
                }, LogInSuccessResult,
                LogInError
        );
    }



    void LogInSuccessResult(LoginResult result) {
        asGuest = false;
        string Id = result.PlayFabId;
        Debug.Log(Id + "log in success");
        //LogInSuccesswPlayFab?.Invoke();
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), GetAccountInfoSuccesss, error=> {
            Debug.LogError("Fail to get account Info");
        });
        TryLoadUserData(Id);
        TryGetVirtualCurrency();
        PlayerPrefs.SetInt("LOGTYPE", 1);
    }

       void GetAccountInfoSuccesss(GetAccountInfoResult result) {
        User.I.titleID = result.AccountInfo.PlayFabId;
        if(password!= null) {
            PlayerPrefs.SetString("PASSWORD",password);
        }
        if (username != null) {
            User.I.displayName = result.AccountInfo.Username;
            PlayerPrefs.SetString("USERNAME", username);
        } else {
            User.I.displayName = "Guest";
        }
        if (email != null) {
            User.I.displayName = result.AccountInfo.Username;
            PlayerPrefs.SetString("EMAIL", email);
        }
        FinishLoadAccountInfo?.Invoke();
    }

    void LogInError(PlayFabError error) {
        LogInResultError?.Invoke();
        Debug.Log("Error while login");
        Debug.Log(error.GenerateErrorReport());
    }

    public void LinkAccount(string mail, string username, string password) {
        this.password = password;
        PlayFabClientAPI.AddUsernamePassword(
                new AddUsernamePasswordRequest() {
                    Email = mail,
                    Username = username,
                    Password = password,
                },
                LinkAccountSuccess, SignUpError);

    }

    void LinkAccountSuccess(AddUsernamePasswordResult result) {
        AccountLinkSuccess?.Invoke();
        Debug.Log("Link Acccount Success" + result.Username + "create new account");
        asGuest = false;
        SetDisplayName(result.Username);
        LogInWPlayFabUserName(username, password);
    }


    void DeleteAccountError(PlayFabError error) {
        Debug.Log("Error while delete account");
        Debug.Log(error.GenerateErrorReport());
    }

    private void SetDisplayName(string username) {
        var request = new UpdateUserTitleDisplayNameRequest() { 
            DisplayName = username
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            OnSuccess => {
                Debug.Log("SetvDisplayName");
            }, OnError => {
                Debug.Log("Error while Set Display Name");
            }
            );
    }
    
    #endregion

    #region save & load userdata
    private void TryLoadUserData(string userIDName) {
        InternetManager.instance.CheckInternetConnection();
        PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
            PlayFabId = userIDName,
            Keys = null
        }, LoadUserData,
        LoadUserDataError => {
            Debug.Log("Error of loading user data" + userIDName);
        });
    }

    void LoadUserData(GetUserDataResult result) {
        Debug.Log("Load User Data Success" + result.Data);
        if(result.Data!= null) {
            if (result.Data.ContainsKey("ExperienceInfo")) {
                ExperienceInfo experienceInfo = JsonConvert.DeserializeObject<ExperienceInfo>(result.Data["ExperienceInfo"].Value);
                User.I.LoadExperienceInfo(experienceInfo);
            } else {
                ExperienceInfo experienceInfo = new ExperienceInfo();
                PlayFabManager.PlayFabManagerInstance.SaveUserDataToPlayFab("ExperienceInfo", experienceInfo);
                User.I.LoadExperienceInfo(experienceInfo);
                Debug.LogError("fail to load Experience Info");
            }

            if (result.Data.ContainsKey("GameRecord")) {
                GameRecord gameRecord = JsonConvert.DeserializeObject<GameRecord>(result.Data["GameRecord"].Value);
                User.I.LoadGameRecord(gameRecord);
            } else {
                GameRecord gameRecord = new GameRecord();
                PlayFabManager.PlayFabManagerInstance.SaveUserDataToPlayFab("GameRecord", gameRecord);
                User.I.LoadGameRecord(gameRecord);
                Debug.LogError("fail to load GameRecord");
            }

            if (result.Data.ContainsKey("MissionRecord")) {
                MissionRecord missionRecord = JsonConvert.DeserializeObject<MissionRecord>(result.Data["MissionRecord"].Value);
                User.I.LoadMissionRecord(missionRecord);
            } else {
                MissionRecord missionRecord = new MissionRecord();
                PlayFabManager.PlayFabManagerInstance.SaveUserDataToPlayFab("MissionRecord", missionRecord);
                User.I.LoadMissionRecord(missionRecord);
                Debug.LogError("fail to load Mission Record");
            }

            if (result.Data.ContainsKey("DailyMission")) {
                DailyMission dailyMission = JsonConvert.DeserializeObject<DailyMission>(result.Data["DailyMission"].Value);
                Debug.Log("in playfab manager from server" + dailyMission.missions.Length);
                User.I.LoadDailyMission(dailyMission);
            } else {
                DailyMission dailyMission = new DailyMission();
                PlayFabManager.PlayFabManagerInstance.SaveUserDataToPlayFab("DailyMission", dailyMission);
                User.I.LoadDailyMission(dailyMission);
                Debug.LogError("fail to load DailyMission");
            }

        } else {
            Debug.LogError("userdata is null");
        }
        SetStatistic();
        FinishLoadUserData?.Invoke();
        User.I.userOn = true;
    }


    public void SaveUserDataToPlayFab(string key, object value) {
        InternetManager.instance.CheckInternetConnection();
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
            Data = new Dictionary<string, string> {
                {key, JsonConvert.SerializeObject(value)}
            }
        }, UpdateDataSuccess => {
            Debug.Log(key + " is updated with " + value);
        }, UpdateError => {
            Debug.LogError("fail to update " + key + " with" + value);
        });
        SetStatistic();
    }


    #endregion

    #region Virtual Currency
    public event Action<int> CPamount;
    public event Action<int> DMamount;
    public event Action<int> BPamount;
    public event Action<int> SecondNeedBP;
    public event Action<bool> SpendResult;


    private int chips;
    private int diamonds;
    private int brainPower;

    public void TryGetVirtualCurrency() {
        InternetManager.instance.CheckInternetConnection();
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), GetUserInventorySuccess, 
            OnError => {
                Debug.Log("fail to load virtual currency");
            });

    }
    void GetUserInventorySuccess(GetUserInventoryResult result) {
        chips = result.VirtualCurrency["CP"];
        CPamount?.Invoke(chips);
        diamonds = result.VirtualCurrency["DM"];
        DMamount?.Invoke(diamonds);
        brainPower= result.VirtualCurrency["BP"];
        BPamount?.Invoke(brainPower);
        int secondNeedBP = result.VirtualCurrencyRechargeTimes["BP"].SecondsToRecharge;
        SecondNeedBP?.Invoke(secondNeedBP);
    }

    public bool SpendCP(int cost) {
        TryGetVirtualCurrency();
        if (cost > chips) {
            SpendResult?.Invoke(false);
            Debug.Log("Not Enough Ships");
            return false;
        }
        PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest() { 
                VirtualCurrency= "CP",
                Amount = cost
            }, SpendSuccess=> {
                SpendResult?.Invoke(true);
                Debug.Log("successfully spend chips of " + cost);
                TryGetVirtualCurrency();
            }, SpendError => {
                Debug.LogError("spend chip error");
            });
        return true;
    }

    public void AddCP(int gain) {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest() {
            VirtualCurrency = "CP",
            Amount = gain
        }, SpendSuccess => {
            Debug.Log("successfully gain chips of " + gain);
            TryGetVirtualCurrency();
        }, SpendError => {
            Debug.LogError("gain chip error");
        });
    }

   
    public bool SpendDM(int cost) {
        TryGetVirtualCurrency();
        if (cost > diamonds) {
            SpendResult?.Invoke(false);
            Debug.Log("Not Enough Diamond");
            return false;
        }

        PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest() {
            VirtualCurrency = "DM",
            Amount = cost
        }, SpendSuccess => {
            TryGetVirtualCurrency();
            SpendResult?.Invoke(true);
            Debug.Log("successfully spend diamonds of" + cost);
        }, SpendError => {
            Debug.LogError("spend diamond error");
        });
        return true;
    }

    public void AddDM(int gain) {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest() {
            VirtualCurrency = "DM",
            Amount = gain
        }, SpendSuccess => {
            TryGetVirtualCurrency();
            Debug.Log("successfully gain diamonds of " + gain);
        }, SpendError => {
            Debug.LogError("gain diamond error");
        });
    }

    public bool SpendBP(int cost) {

        TryGetVirtualCurrency();
        if (cost > brainPower) {
            SpendResult?.Invoke(false);
            Debug.Log("Not Enough brain Power");
            return false;
        }
        PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest() {
            VirtualCurrency = "BP",
            Amount = cost
        }, SpendSuccess => {
            SpendResult?.Invoke(true);
            Debug.Log("successfully spend brain Power of " + cost);
            TryGetVirtualCurrency();
        }, SpendError => {
            Debug.LogError("spend brain Power error");
        });
        return true;
    }

    public void AddBP(int gain) {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest() {
            VirtualCurrency = "BP",
            Amount = gain
        }, SpendSuccess => {
            TryGetVirtualCurrency();
            Debug.Log("successfully gain brain power of " + gain);
        }, SpendError => {
            Debug.LogError("gain brain power error");
        });
    }
#endregion

#region Friends
    List<FriendInfo> _friends = null;

    public event Action<List<FriendInfo>> UpdateFriendList;
    public event Action<bool> addFriendResult;
    

    public void GetFriends() {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest {
            ProfileConstraints = new PlayerProfileViewConstraints {
                ShowDisplayName= true,
                ShowStatistics = true
            }
        }, result => {
            _friends = result.Friends;
            UpdateFriendList?.Invoke(_friends);
        }, DisplayPlayFabError);
    }

    
    public enum FriendIdType { PlayFabId, Username, Email, DisplayName };
    public void AddFriend(FriendIdType idType, string friendId) {
        var request = new AddFriendRequest();
        switch (idType) {
            case FriendIdType.PlayFabId:
                request.FriendPlayFabId = friendId;
                break;
            case FriendIdType.Username:
                request.FriendUsername = friendId;
                break;
            case FriendIdType.Email:
                request.FriendEmail = friendId;
                break;
            case FriendIdType.DisplayName:
                request.FriendTitleDisplayName = friendId;
                break;
        }
        // Execute request and update friends when we are done
        PlayFabClientAPI.AddFriend(request, result => {
            addFriendResult?.Invoke(true);
            Debug.Log("Friend added successfully!");
        }, AddFriendError);
    }

    private void AddFriendError(PlayFabError error) {
        addFriendResult?.Invoke(false);
        Debug.Log(error.GenerateErrorReport());
    }

    void RemoveFriend(FriendInfo friendInfo) {
        PlayFabClientAPI.RemoveFriend(new RemoveFriendRequest {
            FriendPlayFabId = friendInfo.FriendPlayFabId
        }, result => {
            _friends.Remove(friendInfo);
        }, DisplayPlayFabError);
    }

    private void DisplayPlayFabError(PlayFabError error) {
        Debug.Log(error.GenerateErrorReport());
    }

    public void SetStatistic() {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "level", Value = User.I.GetLv() },
                new StatisticUpdate { StatisticName = "bestCalcGame", Value = User.I.GetBestCalcGameType()},
                new StatisticUpdate { StatisticName = "bestCalcRecord", Value = User.I.GetHighestCalcRecord()},
            }
        }, result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    #endregion
}
