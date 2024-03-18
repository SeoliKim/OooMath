using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class User : MonoBehaviour {
    

    #region singleton
    public static User I;

    private void Awake() {
        if (I != null && I != this) {
            Destroy(this);
        } else {
            I = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    public bool userOn = false;

    private void Start() {
        PlayFabManager.PlayFabManagerInstance.CPamount += CPamount;
        PlayFabManager.PlayFabManagerInstance.DMamount += DMamount;
        PlayFabManager.PlayFabManagerInstance.BPamount += BPamount;
        
    }

    #region PlayFabAccount Info
    public string titleID;
    public string displayName;
    public int logType;


    #endregion

    #region Virtual Currency
    public int chips;
    public int diamonds;
    public int brainPower;

    private void CPamount(int amount) {
        chips = amount;
    }

    private void DMamount(int amount) {
        diamonds = amount;
    }
    private void BPamount(int amount) {
        brainPower = amount;
    }

    
    #endregion

    #region GameRecord

    private GameRecord gameRecord= new GameRecord();
    public void LoadGameRecord(GameRecord gameRecord) {
        if(gameRecord == null) {
            gameRecord = new GameRecord();
        }
        gameRecord.Update(gameRecord);
        this.gameRecord = gameRecord;
    }
    private void SaveGameRecord() {
        PlayFabManager.PlayFabManagerInstance.SaveUserDataToPlayFab("GameRecord", gameRecord);
    }

    public void UpdateCalcQuestion(int gameIndex, int current) {
        gameRecord.calcQuestion[gameIndex] = current;
        SaveGameRecord();
    }

    public int GetCalcQuestion(int gameIndex) {
        return gameRecord.calcQuestion[gameIndex];
    }

    public int GetBestCalcGameType() {
        int bestGame = 0;
        for(int i =0; i <12; i++) {
            if (gameRecord.calcQuestion[i] > gameRecord.calcQuestion[bestGame]) {
                bestGame = i;
            }
        }
        return bestGame;
    }

    public int GetHighestCalcRecord() {
        int bestGame = GetBestCalcGameType();
        return gameRecord.calcQuestion[bestGame];
    }

    public void UpdateNumMonsterRound(int gameIndex, int current) {
        gameRecord.numMonsterRound[gameIndex] = current;
        SaveGameRecord();
    }
    public int GetNumMonsterRound(int gameIndex) {
        return gameRecord.numMonsterRound[gameIndex];
    }

    public void UpdateNumMonsterKillMax(int gameIndex, int current) {
        if (current > gameRecord.numMonsterKillMax[gameIndex]) {
            gameRecord.numMonsterKillMax[gameIndex] = current;
            SaveGameRecord();
        }
    }
    public int GetNumMonsterKillMax(int gameIndex) {
        return gameRecord.numMonsterRound[gameIndex];
    }

    public void ClaimAchievementReward(int type, int index) {
        gameRecord.achievementRewards[type, index] += 1;
        SaveGameRecord();
    }

    public int GetRewardClaimMilestone(int type, int index) {
        return gameRecord.achievementRewards[type, index];
    }

    #endregion

    #region experience info

    public event Action<int> lvUp;
    public event Action XPUpdate;

    private ExperienceInfo experienceInfo = new ExperienceInfo();
    public void LoadExperienceInfo(ExperienceInfo experienceInfo) {
        if (experienceInfo == null) {
            experienceInfo = new ExperienceInfo();
        }
        this.experienceInfo = experienceInfo;
        UpdateLv();
    }

    private void SaveExperienceInfo() {
        PlayFabManager.PlayFabManagerInstance.SaveUserDataToPlayFab("ExperienceInfo", experienceInfo);
    }

    public void UpdateExperience(int current) {
        experienceInfo.experience = current;
        XPUpdate?.Invoke();
        UpdateLv();
        SaveExperienceInfo();
    }
    public void AddExperience(int add) {
        experienceInfo.experience += add;
        UpdateLv();
        SaveExperienceInfo();
    }
    public int GetExperience() {
        return experienceInfo.experience;
    }

    private void UpdateLv() {
        //Xp need to complete lv:   Xp = 90Lv + 10Lv^2
        int experience = GetExperience();
        int preLvXP = 0;
        int levelCount = 0;
        while(experience > preLvXP) {
            levelCount++;
            preLvXP += (int)(90 * levelCount + 10 * Mathf.Pow(levelCount, 2));
        }
        if(levelCount > experienceInfo.lv) {
            lvUp?.Invoke(levelCount);
        }
        if(experience == 0) {
            levelCount = 1;
        }
        experienceInfo.lv = levelCount;
        SaveExperienceInfo();
    }

    public int GetLv() {
        return experienceInfo.lv;
    }
    public void UpdateTotalCalcGame(int current) {
        experienceInfo.totalCalcGame = current;
        SaveExperienceInfo();
    }
    public void AddTotalCalcGame(int add) {
        experienceInfo.totalCalcGame += add;
        SaveExperienceInfo();
    }
    public int GetTotalCalcGame() {
        return experienceInfo.totalCalcGame;
    }

    public void UpdateTotalNumGame(int current) {
        experienceInfo.totalNumGame = current;
        SaveExperienceInfo();
    }
    public void AddTotalNumGame(int add) {
        experienceInfo.totalNumGame += add;
        SaveExperienceInfo();
    }
    public int GetTotalNumGame() {
        return experienceInfo.totalNumGame;
    }

    public void UpdateMusicVolume(float current) {
        experienceInfo.musicVolume = current;
        SaveExperienceInfo();
    }
    public float GetMusicVolume() {
        return experienceInfo.musicVolume;
    }

    public void UpdateSoundEffectVolume(float current) {
        experienceInfo.soundEffectVolume = current;
        SaveExperienceInfo();
    }
    public float GetSoundEffectVolume() {
        return experienceInfo.soundEffectVolume;
    }

    #endregion

    #region Mission Record
    private MissionRecord missionRecord = new MissionRecord();
    public void LoadMissionRecord(MissionRecord missionRecord) {
        if (missionRecord == null) {
            missionRecord = new MissionRecord();
        }
        this.missionRecord = missionRecord;
    }

    private void SaveMissionRecord() {
        PlayFabManager.PlayFabManagerInstance.SaveUserDataToPlayFab("MissionRecord", missionRecord);
    }

    public void FinishTutorial(int step) {
        missionRecord.tutorial[step] = false;
        SaveMissionRecord();
    }

    public void StartTutorial(int step) {
        missionRecord.tutorial[step] = true;
        SaveMissionRecord();
    }

    public int GetTutorialOnIndex() {
        for (int i = 0; i < 99; i++) {
            if (missionRecord.tutorial[i]) {
                return i;
            }
        }
        return -1;
    }
    public bool IfTutorialOn(int step) {
        return missionRecord.tutorial[step];
    }

    public void FinishGoal(int num) {
        missionRecord.goals[num] = true;
        SaveMissionRecord();
    }

    public bool CheckGoalDone(int num) {
        return missionRecord.goals[num];
    }

    public int GetGoalOnNum() {
        for (int i = 0; i < 99; i++) {
            if (!missionRecord.goals[i]) {
                return i;
            }
        }
        return -1;
    }

    public event Action OwnNewSkin;

    //Skin
    public void OwnSkin(int skinCollection, int index) {
        missionRecord.OwnSkins[skinCollection, index] = true;
        SaveMissionRecord();
        OwnNewSkin?.Invoke();
    }

    public bool IfSkinOwn(int skinCollection, int index) {
        return missionRecord.OwnSkins[skinCollection, index];
    }

    public int GetOnSkinCollection() {
        return missionRecord.onSkinCollection;
    }

    public int GetOnSkinIndex() {
        return missionRecord.onSkinIndex;
    }

    private Material onSkinMaterial;
    public Material GetOnSkinMaterial() {
        return onSkinMaterial;
    }

    public IEnumerator SaveSkinUserData(int skinCollection, int index, Material[,] skinList) {
        ChangeOnSkinNumRecord(skinCollection, index);
        yield return new WaitForFixedUpdate();
        SetOnSkinMaterial(skinList);
    }

    public void SetOnSkinMaterial(Material[,] skinList) {
        onSkinMaterial = skinList[missionRecord.onSkinCollection, missionRecord.onSkinIndex];
    }

    
    private void ChangeOnSkinNumRecord(int skinCollection, int index) {
        missionRecord.onSkinCollection = skinCollection;
        missionRecord.onSkinIndex = index;
        SaveMissionRecord();
    }




    #endregion

    #region Daily Mission

    private DailyMission dailyMission;

    public void LoadDailyMission(DailyMission dailyMission) {
        if (dailyMission == null) {
            dailyMission = new DailyMission();
        }
        this.dailyMission = dailyMission;
        DateTime now = DateTime.Now;
        if (dailyMission.lastLogInTime.Date.CompareTo(now.Date) != 0) {
            this.dailyMission = new DailyMission();
        }

    }

    private void SaveDailyMission() {
        PlayFabManager.PlayFabManagerInstance.SaveUserDataToPlayFab("DailyMission", dailyMission);
    }

    public void StartNewDailyMission() {
        SaveDailyMission();
    }

    public Mission GetDailyMission(int index) {
        return dailyMission.missions[index];
    }
    public void UpdateDailyMission() {
        SaveDailyMission();
    }

    public void AddDailyCalcGameTotal(int gameIndex, int add) {
        dailyMission.dailyCalcGameTotal[gameIndex] += add;
        SaveDailyMission();
    }

    public int GetDailyCalcGameTotal(int gameIndex) {
        return dailyMission.dailyCalcGameTotal[gameIndex];
    }

    public void AddDailyNumGameTotal(int gameIndex, int add) {
        dailyMission.dailyNumGameTotal[gameIndex] += add;
        SaveDailyMission();
    }
    public int GetDailyNumGameTotal(int gameIndex) {
        return dailyMission.dailyNumGameTotal[gameIndex];
    }

    public void AddDailyCalcRecordBreak(int gameIndex, int add) {
        dailyMission.dailyCalcRecordBreak[gameIndex] += add;
        SaveDailyMission();
    }

    public int GetDailyCalcRecordBreak(int gameIndex) {
        return dailyMission.dailyCalcRecordBreak[gameIndex];
    }

    public void UpdateDailyCalcAnswerStrike(int gameIndex, int value) {
        if(value > dailyMission.dailyCalcAnswerStrike[gameIndex]) {
            dailyMission.dailyCalcAnswerStrike[gameIndex] = value;
        }
        SaveDailyMission();
    }

    public int GetDailyCalcAnswerStrike(int gameIndex) {
        return dailyMission.dailyCalcAnswerStrike[gameIndex];
    }

    public void AddDailyMonsterKill(int gameIndex, int add) {
        dailyMission.dailyMonsterKill[gameIndex] += add;
        SaveDailyMission();
    }
    public int GetDailyMonsterKill(int gameIndex) {
        return dailyMission.dailyMonsterKill[gameIndex];
    }
    public IEnumerator SaveLogInTime() {
        if (dailyMission != null) {
            DateTime n = DateTime.Now;
            dailyMission.lastLogInTime = n;
            SaveDailyMission();
        }
        yield return new WaitForEndOfFrame();
    }

    public DateTime GetSaveLoginTime() {
        return dailyMission.lastLogInTime;
    }

    #endregion

}

