using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class NumberType_OddEvenLevel : NumberType_Level {

        public override string GenerateNumberLabel() {
            int numberLabel = NumberGenerator.getRandomNumber(1, 1000);
            return numberLabel.ToString();
        }

        //inherite:  public virtual bool CheckHit(GameObject numMonster, GameObject powerBubble)
        //Check if numType of both is same

        protected override string DefineNumMonsterNumberType(string numberLabel) {
            bool check= int.TryParse(numberLabel, out int num);
            if (!check) {
                Debug.Log("Problam! NumberLabel can't convert to int" + numberLabel);
            }
            string numType = null;
            if(num % 2 == 0) {
                numType = "even";
            } else {
                numType = "odd";
            }
            return numType;
        }

        protected override string DefineBubbleNumberType(Color32 color) {
            string numType = null;
            Color32 pureColor = new Color32(color[0], color[1], color[2], 255);
            Color32 evenblue = new Color32(0, 0, 255, 255);
            if (pureColor.Equals(evenblue)) {
                numType = "even";
            } else {
                numType = "odd";
            }
            return numType;
        }
    }
}
