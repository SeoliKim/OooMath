using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class Math_FractionCalcGame : Math_CalculationLevel {
        private int[] nums = new int[6]; //firstUp, secondUp, answerUp, firstDown, secondDown, answerDown
        private int symbol;
        protected override void SetUpNum() {
            symbol = NumberGenerator.getRandomNumber(1, 2);
            SetUpFraction(1);
            if (symbol == 1) {//addition
                SetUpFraction(0);
                AddFraction(0, 1, 2);
            } else {
                SetUpFraction(2);
                AddFraction(2, 1, 0);
            }

            CheckSimplify();

        }

        private void SetUpFraction(int fractionNum) { //0- first, 1-second, 2- answer
            int indexUp = 0 + fractionNum;
            int indexDown = 3 + fractionNum;
            nums[indexUp] = NumberGenerator.getRandomNumber(1, 9);
            nums[indexDown] = NumberGenerator.getRandomNumber(1, 9);
            
        }


        private void AddFraction(int fraction1Index, int fraction2Index, int answerIndex) {
            int up1 = nums[0 + fraction1Index];
            int down1 = nums[3 + fraction1Index];
            int up2 = nums[0 + fraction2Index];
            int down2 = nums[3 + fraction2Index];

            int finalDown = down1 * down2;
            int firstUpA = up1 * down2;
            int secondUpA = up2 * down1;

            int finalUp = firstUpA + secondUpA;
            
            nums[0 + answerIndex] = finalUp;
            nums[3 + answerIndex] = finalDown;
        }

        private void CheckSimplify() {
            for(int i =0; i < 3; i++) {
                int indexUp = 0 + i;
                int indexDown = 3 + i;
                int gcd = UtilityMath.GCD(nums[indexUp], nums[indexDown]);
                nums[indexUp] /= gcd;
                nums[indexDown] /= gcd;
                if (nums[indexDown] == 1) {
                    nums[indexDown] = -100;
                }
            }
        }

        protected override float SetUpX() {
            int posCount = 5;
            for(int i =0; i < 6; i++) {
                if (nums[i] < 0) {
                    posCount--;
                }
            }
            pOX = NumberGenerator.getRandomNumber(0, posCount);
            x = nums[pOX];
            if (x == 0) {
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
            string[] parts = new string[6];
            for (int i = 0; i < 3; i++) {
                parts[i] = nums[i].ToString();
            }

            for (int i = 3; i < 6; i++) {
                if (nums[i] < 0) {
                    parts[i] = "";
                } else {
                    parts[i] = "/ " + nums[i].ToString();
                }
            }
           
            if(pOX ==3 || pOX ==4 || pOX == 5) {
                parts[pOX] = "/ ?";
            } else {
                parts[pOX] = "?";
            }

            equation = " " + parts[0] + " " + parts[3] + " " + symbolString + " " + parts[1] + " " + parts[4] + " = " + " " + parts[2] + " " + parts[5];
            return equation;
        }

        protected override float GenerateBallNumLabel() {
            int range = 12;
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
