using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionUI : MonoBehaviour
{
    private Mission mission;

    [SerializeField]
    private GameObject _CompleteShadow, _ClaimButtonReadyShadow;
    [SerializeField] private TMP_Text _TextCPNum;

    public void SetMissionUI(Mission mission) {
        
        this.mission = mission;
        _TextCPNum.text = mission.CPNum.ToString();
        if (mission.claim) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
        if (mission.complete) {
            _ClaimButtonReadyShadow.SetActive(true);
            
        } else {
            GoalManager.instance.CompleteOneMission += CompleteOneMission;
            _ClaimButtonReadyShadow.SetActive(false);
        }
    }
        
    

    private void CompleteOneMission(int index) {
        if(index == mission.missionIndex) {
            MissionComplete();
        }
    }

    public void MissionComplete() {
        _ClaimButtonReadyShadow.SetActive(true);
        mission.complete = true;
        User.I.UpdateDailyMission();
    }
    public void ClaimReward() {
        PlayFabManager.PlayFabManagerInstance.AddCP(mission.CPNum);
        mission.claim = true;
        gameObject.SetActive(false);
        User.I.UpdateDailyMission();
        GoalManager.instance.CompleteOneMission -= CompleteOneMission;
    }
}

public class Mission {
    public int CPNum;
    public int missionIndex;
    public bool complete;
    public bool claim;

    public Mission(int CPNum, int missionIndex) {
        this.CPNum = CPNum;
        this.missionIndex = missionIndex;
        complete = false;
        claim = false;
    }

}
