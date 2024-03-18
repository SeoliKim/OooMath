using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class NumberType_Level : MonoBehaviour {

        public virtual string GenerateNumberLabel() {
            string numberLabel = null;
            return numberLabel;
        }

        public virtual bool CheckHit(GameObject numMonster, GameObject powerBubble) {
            string numMonsterNumType = null;
            string powerBubbleNumType = null;
            if (numMonster.CompareTag("Monster")) {
                string numberLabel = numMonster.GetComponent<NumMonsterFunction>().GetNumberLabel();
                numMonsterNumType = DefineNumMonsterNumberType(numberLabel);
            }

            if (powerBubble.CompareTag("PowerBubble")) {
                Color32 powerBubbleColor = powerBubble.GetComponent<PowerBubbleVariable>().GetColor();
                powerBubbleNumType = DefineBubbleNumberType(powerBubbleColor);
            }
            return string.Equals(numMonsterNumType.ToLower(), powerBubbleNumType.ToLower());
        }

        protected virtual string DefineNumMonsterNumberType(string numberLabel) {
            return null;
        }

        protected virtual string DefineBubbleNumberType(Color32 color) {
            return null;
        }
    }
}
