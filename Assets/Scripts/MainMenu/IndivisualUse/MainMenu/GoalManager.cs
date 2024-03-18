using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoalManager : MonoBehaviour
{

    #region singleton
    public static GoalManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }
    #endregion

    public event Action<int> CompleteOneMission;
    [SerializeField] private List<MissionUI> _missionUis;
    [SerializeField] private GameObject _DailyNotification;


    private void Start() {
        DateTime nowdate = DateTime.Now;
        _DailyNotification.SetActive(false);
        User.I.gameObject.AddComponent<TimeManager>();
        DateTime saveTime = User.I.GetSaveLoginTime();
        if (saveTime.Date.CompareTo(nowdate.Date) != 0) {
            User.I.StartNewDailyMission();
        }
        UpdateDailyMission();
    }
    private void UpdateMissionUIs() {
        for (int i = 0; i < _missionUis.Count; i++) {
            _missionUis[i].SetMissionUI(User.I.GetDailyMission(i));
        }
    }

    public void UpdateDailyMission() {
        UpdateMissionUIs();
        StartCoroutine(CheckMission());
    }

    private IEnumerator CheckMission() {
        while (true) {
            CheckM0();
            CheckM1();
            CheckM2();
            CheckM3();
            CheckNotification();
            yield return new WaitForEndOfFrame();
        }
    }

    private void CheckM0() {
        Mission m = User.I.GetDailyMission(0);
        if (!m.complete) {
            m.complete = true;
            CompleteOneMission?.Invoke(0);
        }
    }

    private void CheckM1() {
        Mission m = User.I.GetDailyMission(1);
        if (!m.complete) {
            int count = 0;
            for (int i = 0; i < 11; i++) {
                count += User.I.GetDailyCalcGameTotal(i);
                if (count >= 10) {
                    CompleteOneMission?.Invoke(1);
                }
            }
        }
        
    }

    private void CheckM2() {
        Mission m = User.I.GetDailyMission(2);
        if (!m.complete) {
            int strike = 0; 
            for (int i = 0; i < 11; i++) {
                if (User.I.GetDailyCalcAnswerStrike(i) > strike) {
                    strike = User.I.GetDailyCalcAnswerStrike(i);
                }
            }
            if (strike >=6) {
                CompleteOneMission?.Invoke(2);
            }
        }
    }

    private void CheckM3() {
        Mission m = User.I.GetDailyMission(3);
        if (!m.complete) {
            int count = 0;
            for (int i = 0; i < 11; i++) {
                count += User.I.GetDailyCalcRecordBreak(i);
                if (count >= 7) {
                    CompleteOneMission?.Invoke(3);
                }
            }
        }
    }

   

    private void CheckNotification() {
        _DailyNotification.SetActive(false);
        for (int i = 0; i < _missionUis.Count; i++) {
            if ((!User.I.GetDailyMission(i).claim) && User.I.GetDailyMission(i).complete) {
                _DailyNotification.SetActive(true);
            }
        }
    }
}
