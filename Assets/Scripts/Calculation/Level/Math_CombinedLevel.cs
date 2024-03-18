using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class Math_CombinedLevel : Math_CalculationLevel {
        private int bracket;
        private int symbol;

        private int first, second, third, answer;
        protected override void SetUpNum() {
            bracket = NumberGenerator.getRandomNumber(1, 2);
            if (bracket == 1) {//multiplication is outside
                NumWithBracket();
            } else {
                NumNoBracket();
            }
        }

        private void NumWithBracket() {
            symbol = NumberGenerator.getRandomNumber(1, 2);
            if (symbol == 1) {//addition
                first = NumberGenerator.getRandomNumber(1, 9);
                second = NumberGenerator.getRandomNumber(1, 9);
                third = NumberGenerator.getRandomNumber(1, 9);
                answer = first * (second + third);
            } else {
                first = NumberGenerator.getRandomNumber(1, 9);
                second = NumberGenerator.getRandomNumber(2, 20);
                third = NumberGenerator.getRandomNumber(1, second);
                answer = first * (second - third);
            }
        }

        private void NumNoBracket() {
            symbol = NumberGenerator.getRandomNumber(1, 2);
            if (symbol == 1) {//addition
                first = NumberGenerator.getRandomNumber(1, 9);
                second = NumberGenerator.getRandomNumber(1, 9);
                third = NumberGenerator.getRandomNumber(1, 9);
                answer = first * second + third;
            } else {
                first = NumberGenerator.getRandomNumber(1, 9);
                second = NumberGenerator.getRandomNumber(2, 20);
                third = NumberGenerator.getRandomNumber(1, first * second);
                answer = first * second - third;
            }
        }

        protected override float SetUpX() {
            pOX = NumberGenerator.getRandomNumber(1, 4);
            if (pOX == 1) {
                x = first;
            } else if (pOX == 2) {
                x = second;
            } else if (pOX == 3) {
                x = third;
            } else if (pOX == 4) {
                x = answer;
            } else{
                Debug.Log("error in setting math problem, position of x is : " + pOX);
            }
            return x;
        }

        protected override string SetEquation() {
            if (bracket == 1) {//multiplication is outside
                EquationWithBracket();
            } else {
                EquationNoBracket();
            }
            return equation;
        }

        private string EquationWithBracket() {
            string symbolString;

            if (symbol == 1) {
                symbolString = "+";
            } else {
                symbolString = "-";
            }
            if (pOX == 1) {
                equation = " " + "?" + " * ( " + second.ToString() + " " + symbolString + " " + third.ToString() + " ) = " + answer.ToString();
            } else if (pOX == 2) {
                equation = " " + first.ToString() + " * ( " + "?" + " " + symbolString + " " + third.ToString() + " ) = " + answer.ToString();
            } else if (pOX == 3) {
                equation = " " + first.ToString() + " * ( " + second.ToString() + " " + symbolString + " " + "?" + " ) = " + answer.ToString();
            } else if (pOX == 4) {
                equation = " " + first.ToString() + " * ( " + second.ToString() + " " + symbolString + " " + third.ToString() + " ) = " + "?";
            } else {
                Debug.Log("error in setting math problem, position of x is : " + pOX);
            }
            return equation;
        }

        private string EquationNoBracket() {
            string symbolString;

            if (symbol == 1) {
                symbolString = "+";
            } else {
                symbolString = "-";
            }
            if (pOX == 1) {
                equation = " " + "?" + " * " + second.ToString() + " " + symbolString + " " + third.ToString() + " = " + answer.ToString();
            } else if (pOX == 2) {
                equation = " " + first.ToString() + " * " + "?" + " " + symbolString + " " + third.ToString() + " = " + answer.ToString();
            } else if (pOX == 3) {
                equation = " " + first.ToString() + " * " + second.ToString() + " " + symbolString + " " + "?" + " = " + answer.ToString();
            } else if (pOX == 4) {
                equation = " " + first.ToString() + " * " + second.ToString() + " " + symbolString + " " + third.ToString() + " = " + "?";
            } else {
                Debug.Log("error in setting math problem, position of x is : " + pOX);
            }
            return equation;
        }

        protected override float GenerateBallNumLabel() {
            int range = 8;
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
