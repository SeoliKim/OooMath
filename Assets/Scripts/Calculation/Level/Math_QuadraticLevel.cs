using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Calculation {
    public class Math_QuadraticLevel : Math_CalculationLevel {

        private float a, b, c, d;
        private int symbol;
        protected override void SetUpNum() {
            //base equation
            int baseSquare = NumberGenerator.getRandomNumber(-9, 9);
            symbol = NumberGenerator.getRandomNumber(1, 2);
            int insideNum = NumberGenerator.getRandomNumber(0, 9);
            if (symbol == 1) {//addition
                x = baseSquare - insideNum;
            } else {
                x = baseSquare + insideNum;
            }
            
            //get a
            a = NumberGenerator.getRandomNumber(1, 9);

            //get b
            b = 2 * insideNum * a;

            int constant = (int)a * insideNum * insideNum;

            d = NumberGenerator.getRandomNumber(0, constant);
            c = d + constant;

        }
        
        protected override float SetUpX() {
            return x;
        }

        protected override string SetEquation() {
            string symbolString;

            if (symbol == 1) {
                symbolString = "+";
            } else {
                symbolString = "-";
            }
            equation = "" + a.ToString() + "?" + "^2 " + symbolString + b.ToString() + "?" + " + " + c.ToString() + " = " + d.ToString();
            return equation;
        }

        protected override float GenerateBallNumLabel() {
            int range = 12;
            int rLow = (int)(x - range);
            int rUp = (int)(x + range);
            float n = NumberGenerator.getRandomNumber(rLow, rUp);
            float check;
            if (symbol == 1) {
                check = a * n * n + b * n + c - d;
            } else {
                check = a * n * n - b * n + c - d;
            }
            while(check == 0) {
                n = NumberGenerator.getRandomNumber(rLow, rUp);
                if (symbol == 1) {
                    check = a * n * n + b * n + c - d;
                } else {
                    check = a * n * n - b * n + c - d;
                }
            }
            
            return n;
        }

    }
}
