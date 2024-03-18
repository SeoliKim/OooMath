using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public DateTime startDate;
    #region singleton
    public static TimeManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
        startDate = DateTime.Now;
    }
    #endregion

    private void Start() {
        StartCoroutine(UpdateTime());
    }

    private void Update() {
        DateTime nowdate = DateTime.Now;
        if (startDate.Date.CompareTo(nowdate.Date)!= 0) {
            startDate = DateTime.Now.Date;
            
            User.I.StartNewDailyMission();
            GoalManager.instance.UpdateDailyMission();
        }
    }

    public IEnumerator UpdateTime() {
        StartCoroutine(User.I.SaveLogInTime());
        yield return new WaitForSecondsRealtime(1800);
    }
}
