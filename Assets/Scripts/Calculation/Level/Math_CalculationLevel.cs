using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Calculation {
    public class Math_CalculationLevel : MonoBehaviour {
        protected float x;
        protected int pOX, ballNumLabelRange;
        protected bool setUpDone;
        protected string equation;
        [Space]
        [SerializeField] private Material platformMaterial;
        public Material GetPlatformMaterial() {
            return platformMaterial;
        }

        public event Action<MathProblemArgs> MathProblemSetDone;
        public class MathProblemArgs : EventArgs {
            public float xA;
            public string equationA;
        }

        private void Awake() {
            SetUpNum();
            SetUpX();
            SetEquation();
            setUpDone = true;
        }

        private void Start() {
            if (setUpDone) {
                MathProblemSetDone?.Invoke(new MathProblemArgs { xA = x, equationA = equation});
            }
        }

        protected virtual void SetUpNum() {
            //firstNum =
            //secondNum =
            //answer = 
        }

        protected virtual float SetUpX() {
            //get pOX= NumberGenerator.getRandomNumber(1, 3);
            return x;
        }

        protected virtual string SetEquation() {
            //arrange equation
            return equation;
        }

        public virtual float GetBallNumLabel(float[] numberLabels) {
            float numLabel = BallNumLabelRepetitionCheck(numberLabels);
            return numLabel;
        }

        protected virtual float GenerateBallNumLabel() {
            float n = 0;
            //get number near x
            return n;
        }

        protected float BallNumLabelRepetitionCheck(float[] numberLabels) {
            float nL=0;
            int count = 0;
            while (count < 100) {
                nL = GenerateBallNumLabel();
                count++;
                bool check = NumberGenerator.NoRepetitionCheck(numberLabels, nL);
                if (check) {
                    return nL;
                }
            }
            return nL;
        }

        
        public float GetX() {
            return x;
        }
       


    }
}
   
