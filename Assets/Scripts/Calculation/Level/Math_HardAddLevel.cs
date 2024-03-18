using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class Math_HardAddLevel : Math_CalculationLevel {
        private float firstNum, secondNum, answer;
        private int symbol;
        protected override void SetUpNum() {
            symbol = NumberGenerator.getRandomNumber(1, 2);
            if (symbol == 1) {//addition
                firstNum = NumberGenerator.getRandomNumber(10, 99);
                secondNum = NumberGenerator.getRandomNumber(10, 99);
                answer = firstNum + secondNum;
            } else {// substraction
                firstNum = NumberGenerator.getRandomNumber(10, 99);
                secondNum = NumberGenerator.getRandomNumber(1, (int)firstNum);
                answer = firstNum - secondNum;
            }

        }

        protected override float SetUpX() {
            pOX = NumberGenerator.getRandomNumber(1, 3);
            if (pOX == 1) {
                x = firstNum;
            } else if (pOX == 2) {
                x = secondNum;
            } else if (pOX == 3) {
                x = answer;
            } else {
                Debug.Log("error in setting math problem, position of x is : " + pOX);
            }
            return x;
        }

        protected override string SetEquation() {
            string symbolString;

            if (symbol == 1) {
                symbolString = "+";
            } else {
                symbolString = "-";
            }
            if (pOX == 1) {
                equation = " " + "?" + " " + symbolString + " " + secondNum.ToString() + " = " + answer.ToString();
            } else if (pOX == 2) {
                equation = " " + firstNum.ToString() + " " + symbolString + " " + "?" + " = " + answer.ToString();
            } else if (pOX == 3) {
                equation = " " + firstNum.ToString() + " " + symbolString + " " + secondNum.ToString() + " = " + "?";
            } else {
                Debug.Log("error in setting math problem, position of x is : " + pOX);
            }
            return equation;
        }

        protected override float GenerateBallNumLabel() {
            int range = 20;
            int rLow = (int)(x - range);
            if (rLow < 0) {
                rLow = 0;
            }
            int rUp = (int)(x + range + Mathf.Abs(x - range - rLow));
            float n = NumberGenerator.getRandomNumber(rLow, rUp);
            return n;
        }

    }
}
