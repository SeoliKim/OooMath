using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievement : MonoBehaviour {
    protected bool onGoing;
    protected int type, index;

    protected virtual void Awake() {
        //get type
        //get index
        UpdateAchievement();
    }

    protected void UpdateAchievement() {
        float recordData = GetUserData();
        int milestoneAchieved = MilestoneAchieved(recordData);
        int goalMilestone = milestoneAchieved+1;
        int claimRewardMilestone = User.I.GetRewardClaimMilestone(type, index);
        int rewardMilestone = claimRewardMilestone+1;
        if (rewardMilestone <= milestoneAchieved) {
            ReadyToClaimReward(rewardMilestone, recordData);
        } else if (rewardMilestone == goalMilestone) {
            if (onGoing) {
                NotAchievedYet(goalMilestone, recordData);
                float nextGoal = Goal(goalMilestone);
                UpdateAchievementUI(nextGoal, recordData);
            } else {
                ShowAchievementDone(recordData);
            }

        } else {
            Debug.LogError("error happened in Updata Achievement. Claim Reward Milestone of " + claimRewardMilestone + "is higher than milestoneAchieved of" + milestoneAchieved);
        }
    }

    protected virtual float GetUserData() {
        /*override
         * get the user data for different achieved
         */
        float record = 0;
        return record;
    }

    protected int MilestoneAchieved(float recordData) {
        int milestoneCount = GetMilstoneCount();
        for (int i=1; i <= milestoneCount; i++) {
            float goal = Goal(i);
            if (recordData < goal) {
                onGoing = true;
                return (i-1);
            }
        }
        onGoing = false;
        return -1;
    }

    protected virtual int GetMilstoneCount() {
        /*override
         * total number of milestone
         */
        int milestoneCount = 0;
        return milestoneCount;
    }

    protected virtual float Goal(int mileStone) {
        /*override
         * get the next goal the mileStone would be reaching
         * 
         */
        int goal = 0;
        return goal;
    }

    protected void ReadyToClaimReward(int rewardMilestone, float recordData) {
        float nextGoal = Goal(rewardMilestone);
        UpdateAchievementUI(nextGoal, recordData);
        SetCurrentReward(rewardMilestone);
        _ClaimButton.SetActive(true);
    }

    protected void NotAchievedYet(int goalMilestone, float recordData) {
        float nextGoal = Goal(goalMilestone);
        UpdateAchievementUI(nextGoal, recordData);
        SetCurrentReward(goalMilestone);
        _ClaimButton.SetActive(false);
    }

    protected void ShowAchievementDone(float recordData) {
        int maxMilestone = GetMilstoneCount();
        float maxGoal = Goal(maxMilestone);
        UpdateAchievementUI(maxGoal, recordData);
        _RewardPanel.SetActive(false);
        _DoneUI.SetActive(true);
    }


    #region Achievement UI
    [Space]
    [Header("Achievement UI")]
    [SerializeField] protected Image _AchievementProgress;
    [SerializeField] protected string titleText;
    [SerializeField] protected TMP_Text _TextNextGoalRecord, _TitleText;


    protected void UpdateAchievementUI(float nextGoal, float recordData) {
        UpdateTitleText(nextGoal);
        UpdateProgressBar(nextGoal, recordData);
    }

    protected void UpdateTitleText(float nextGoal) {
        _TextNextGoalRecord.text = nextGoal.ToString();
        _TitleText.text = titleText.ToUpper();
    }

    protected void UpdateProgressBar(float nextGoal, float recordData) {
        float fillAmount = recordData / nextGoal;
        if(fillAmount > 1) {
            _AchievementProgress.fillAmount = 1;
            return;
        }
        _AchievementProgress.fillAmount = fillAmount;
    }

  
    #endregion

    #region Reward
    [Space]
    [Header("Reward")]
    [SerializeField] protected TMP_Text _Text_CpCount;
    [SerializeField] protected GameObject _RewardPanel, _SkinRewardPanel; 

    protected int rewardCPCount;
    protected readonly int skinCollection = 2;
    [Space]
    [Header("Claim")]
    [SerializeField] protected GameObject _ClaimButton;
    public GameObject GetClaimButton() {
        return _ClaimButton;
    }
    [SerializeField] protected GameObject _DoneUI;

    protected void SetCurrentReward(int rewardMilestone) {
        int rewardCPCount = GetRewardCPAmount(rewardMilestone);
        int skinIndex = GetRewardSkinIndex(rewardMilestone);
        if (skinIndex < 0) {
            ShowReward(rewardCPCount, false, skinIndex);
        } else {
            ShowReward(rewardCPCount, true, skinIndex);
        }
    }
    
    protected virtual int GetRewardCPAmount(int rewardMilestone) {//override
        return -1;
    }

    protected virtual int GetRewardSkinIndex(int rewardMilestone) {//override
        return -1;
    }

    protected void ShowReward(int rewardCPCount, bool withSkin, int skinIndex) {
        this.rewardCPCount = rewardCPCount;
        _Text_CpCount.text = rewardCPCount.ToString();
        if (withSkin) {
            _SkinRewardPanel.SetActive(true);
            for (int i = 0; i < _SkinRewardPanel.transform.childCount; i++) {
                GameObject child = _SkinRewardPanel.transform.GetChild(i).gameObject;
                child.SetActive(false);
            }
            _SkinRewardPanel.transform.GetChild(skinIndex).gameObject.SetActive(true);
        } else {
            _SkinRewardPanel.SetActive(false);
        }
        DefueltClaimButton();
    }

    protected void DefueltClaimButton() {
        _ClaimButton.SetActive(false);
    }

    public void ClickClaim() {
        User.I.ClaimAchievementReward(type, index);
        CollectReward();
        UpdateAchievement();
    }

    protected void CollectReward() {
        CollectChips();
        CollectSkin();
    }

    protected void CollectChips() {
        PlayFabManager.PlayFabManagerInstance.AddCP(rewardCPCount);
        AudioManager.AudioManagerInstance.PlayAudio("many-coins");
    }

    protected void CollectSkin() {
        GameObject collectSkin = null;
        for (int i = 0; i < _SkinRewardPanel.transform.childCount; i++) {
            if (_SkinRewardPanel.transform.GetChild(i).gameObject.activeSelf) {
                collectSkin = _SkinRewardPanel.transform.GetChild(i).gameObject;
                break;
            } else {
                return;
            }     
        }
        SkinAchievementPrize skinAchievementPrize = collectSkin.GetComponent<SkinAchievementPrize>();
        User.I.OwnSkin(skinCollection, skinAchievementPrize.GetIndex());
        //close skin box
        for (int i = 0; i < _SkinRewardPanel.transform.childCount; i++) {
            GameObject child = _SkinRewardPanel.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        _SkinRewardPanel.SetActive(false);
    }

    
    #endregion

   
}
