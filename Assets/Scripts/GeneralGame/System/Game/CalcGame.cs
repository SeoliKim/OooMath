using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalcGame : Game
{
    [SerializeField] TMP_Text _text_gameTitleName;
  
    private void Awake() {
        gameType = 0;
        gameIndex = transform.GetSiblingIndex();
        sceneIndex = 3 + gameIndex;
        bPCost = 1;
        _text_gameTitleName.text = GetCalcGameTitleName(gameIndex).ToUpper();
    }

    public static string GetCalcGameTitleName(int gameIndex) {
        switch (gameIndex) {
            case 0:
                return "Simple addtion";
            case 1:
                return "Hard addtion";
            case 2:
                return "Multiplication";
            case 3:
                return "Combined Operation";
            case 4:
                return "Decimal";
            case 5:
                return "Fraction";
            case 6:
                return "Quadratic";
            default:
                return null;
        }

    }
}
