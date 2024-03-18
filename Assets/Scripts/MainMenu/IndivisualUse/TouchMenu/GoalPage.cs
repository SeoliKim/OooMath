using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPage : MonoBehaviour
{
    [SerializeField] private GameObject _MissionPanel, _Missionwhiteshadow, _AchievementPanel, _Achievementwhiteshadow;
    private List<GameObject> whiteShadows = new List<GameObject>();
    private List<GameObject> panels = new List<GameObject>();

    private void Start() {
        whiteShadows.Add(_Missionwhiteshadow);
        whiteShadows.Add(_Achievementwhiteshadow);
        panels.Add(_MissionPanel);
        panels.Add(_AchievementPanel);
        OnMissionPanel();
    }

    public void OnMissionPanel() {
        ChangePanel(_MissionPanel, _Missionwhiteshadow);
    }

    public void OnAchievementPanel() {
        ChangePanel(_AchievementPanel, _Achievementwhiteshadow);
    }

    private void ChangePanel(GameObject panel, GameObject whiteShadow) {
        foreach (GameObject ws in whiteShadows) {
            ws.SetActive(true);
        }
        foreach (GameObject p in panels) {
            p.SetActive(false);
        }
        whiteShadow.SetActive(false);
        panel.SetActive(true);

    }
}
