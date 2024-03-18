using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private GameObject _HomeTouch;
    [SerializeField] private TMP_Text cr0, cr1, cr2, cr3, cr4, cr5, cr6;
    private void OnEnable() {
        _HomeTouch.SetActive(false);
        UpdateCalcGameRecord();
    }

    private void UpdateCalcGameRecord() {
        cr0.text = User.I.GetCalcQuestion(0).ToString();
        cr1.text = User.I.GetCalcQuestion(1).ToString();
        cr2.text = User.I.GetCalcQuestion(2).ToString();
        cr3.text = User.I.GetCalcQuestion(3).ToString();
        cr4.text = User.I.GetCalcQuestion(4).ToString();
        cr5.text = User.I.GetCalcQuestion(5).ToString();
        cr6.text = User.I.GetCalcQuestion(6).ToString();

    }
}
