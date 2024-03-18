using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private GameObject _AchievementPanel, _Achievementnotification;
    [Space]
    [Header("Achievement Type Scroli Content")]
    [SerializeField] private GameObject _NumType;
    [SerializeField] private GameObject _CalcType;


    private List<GameObject> claimButtons = new List<GameObject>();
    void Start()
    {
        _Achievementnotification.SetActive(false);
        for (int i = 0; i < _CalcType.transform.childCount; i++) {
            GameObject child = _CalcType.transform.GetChild(i).gameObject;
            GameObject claimButton = child.GetComponent<Achievement>().GetClaimButton();
            claimButtons.Add(claimButton);
        }

        for (int i = 0; i < _CalcType.transform.childCount; i++) {
            GameObject child = _CalcType.transform.GetChild(i).gameObject;
            GameObject claimButton = child.GetComponent<Achievement>().GetClaimButton();
            claimButtons.Add(claimButton);
        }
        OpenType(_CalcType);
    }

    // Update is called once per frame
    void Update()
    {
        CheckNotification();
    }

    private void CheckNotification() {
        for (int i = 0; i < claimButtons.Count; i++) {
            if (claimButtons[i].activeSelf) {
                _Achievementnotification.SetActive(true);
                break;
            }
            _Achievementnotification.SetActive(false);
        }
    }

    #region Achievement UI GoalMenu
    public void OpenType(GameObject _ShowType) {
        _CalcType.SetActive(false);
        _NumType.SetActive(false);
        _ShowType.SetActive(true);
    }

    #endregion
}
