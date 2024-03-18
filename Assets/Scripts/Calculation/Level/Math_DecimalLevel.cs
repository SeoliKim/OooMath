using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class Math_DecimalLevel : Math_CalculationLevel {
        private float firstNum, secondNum, answer;
        private int symbol;
        protected override void SetUpNum() {
            symbol = NumberGenerator.getRandomNumber(1, 2);
            if (symbol == 1) {//addition
                firstNum = GetRandomDecimal();
                secondNum = GetRandomDecimal();
                answer = firstNum + secondNum;
            } else { //substraction
                secondNum = GetRandomDecimal();
                answer = GetRandomDecimal();
                firstNum = secondNum + answer;
            }
        }

        private float GetRandomDecimal() {
            float num = (float)NumberGenerator.getRandomNumber(1, 1000);
            float d = num / 100.0f;
            return d;
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
            int range = 500;
            int xmultiple = (int)(x * 100);
            int rLow = (int)(xmultiple - range);
            if (rLow < 0) {
                rLow = 0;
            }
            int rUp = (int)(xmultiple + range + Mathf.Abs(xmultiple - range - rLow));
            float nmultiple = NumberGenerator.getRandomNumber(rLow, rUp);
            float n = nmultiple / 100;
            return n;
        }

    }
}
