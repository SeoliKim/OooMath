using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FriendInListUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _IdName, _LvNum, _RecordNum, _BestGameType;
   
    public void SetFriendInListUI(string disPlayName, int lv, int bestGameType, int gameRecord) {
        _IdName.text = disPlayName;
        _LvNum.text = lv.ToString();
        _RecordNum.text = gameRecord.ToString();
        _BestGameType.text= CalcGame.GetCalcGameTitleName(bestGameType).ToUpper();
    }

}
