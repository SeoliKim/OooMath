using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinAward : Skin
{
    [Space]
    [Header("Skin Award")]
    private string text_howToUnlock;


    public string GetText_HowToUnlock() {
        return text_howToUnlock;
    }
    protected override int SetCollection() {
        SetText_howToUnlock();
        return 2;
    }

    private void SetText_howToUnlock() {
        index = GetIndex();
        int gameType = index / 3;
        string gameTypeString = CalcGame.GetCalcGameTitleName(gameType);
        int achievementSkinStep = index % 3;
        int achievementSkinQNum = 0;
        if (achievementSkinStep == 0) {
            achievementSkinQNum = 30;
        } else if(achievementSkinStep == 1) {
            achievementSkinQNum = 70;
        } else {
            achievementSkinQNum = 120;
        }
        text_howToUnlock = "Calculation Game \""+ gameTypeString + "\" - Reach Q" + achievementSkinQNum.ToString();
    }
}
