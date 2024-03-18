using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace Calculation {
    public class SetUpBallNum : MonoBehaviour {
        private int numOfBall;
        private Math_CalculationLevel math_CalculationLevel;

        public float ballNumSpeed;
        public float ballNumAccer;

        public event Action<GameObject[]> BallNumSetUpDone;

        public GameObject ballNum_Prefab;
        public GameObject[] ballNums;
        public float[] numberLabels;

        private GameObject platform;
        private float goal;

        private Color[] hueColors;

        public SetUpBallNum SetSetUpBallNum(GameObject _ballNumPrefab, int numOfBall, float ballNumSpeed, float ballNumAccer, Math_CalculationLevel math_CalculationLevel) {
            this.ballNum_Prefab = _ballNumPrefab;
            this.numOfBall = numOfBall;
            this.math_CalculationLevel = math_CalculationLevel;
            this.ballNumSpeed = ballNumSpeed;
            this.ballNumAccer = ballNumAccer;
            ballNums = new GameObject[numOfBall];
            numberLabels = new float[numOfBall];

            hueColors = new Color[numOfBall];
            hueColors = getHueColor();

            GameManager.OnGameStateChanged += OnGameStateChanged;
            StartCoroutine(InstanciateBallNum());
            return this;
        }

        private void OnGameStateChanged(GameState gameState) {
            if(gameState == GameState.GameOn) {
                for (int i = 0; i < numOfBall; i++) {
                    AIMoveTo aIMoveTo = ballNums[i].AddComponent<AIMoveTo>();
                    aIMoveTo.radius = 20;
                    ballNums[i].GetComponent<NavMeshAgent>().speed = ballNumSpeed;
                    ballNums[i].GetComponent<NavMeshAgent>().acceleration = ballNumAccer;
                }
            }
        }

        IEnumerator InstanciateBallNum() { 
            yield return new WaitUntil(()=> LinkToGameObject());
            Debug.Log("Start clone ball");
            cloneBall();
            BallNumSetUpDone?.Invoke(ballNums);
        }
        

        private void cloneBall() {
            for (int i = 0; i < numOfBall; i++) {
                Vector3 surfaceposition = PlatformSurface.GetRandomPointOnPlatformAwayFromOrigin(platform);
                Vector3 position = new Vector3(surfaceposition.x, 0, surfaceposition.z);

                GameObject ballNum = Instantiate(ballNum_Prefab, position, Quaternion.identity, transform) as GameObject;

                //number label
                float numberLabel;
                //get number label
                if (i == 0) {//Get X
                    numberLabel = goal;
                    Debug.Log("numBall 0 get X set label as" + numberLabel);
                } else {   //set other ball
                    numberLabel = math_CalculationLevel.GetBallNumLabel(numberLabels);
                }
                //set number label
                numberLabels[i] = numberLabel;


                ballNum.name = "BallNum" + numberLabel;
                ballNums[i] = ballNum;

                //update BallNum numberLabel to ballNumObject
                ballNum.GetComponent<BallNumVariable>().numberLabel = numberLabel;

                //Change color
                Color32 color = hueColors[i];
                ballNum.transform.Find("Hat").GetComponent<MeshRenderer>().material.SetColor("_Color", color);

                //stay no motion at first
                ballNum.GetComponent<NavMeshAgent>().speed = 0;
                ballNum.GetComponent<NavMeshAgent>().acceleration = 0;

            }

            //invoke action
        }

        public Vector3 setRandomPosition(GameObject ballNum) {
            Vector3 randomPosition = ballNum.GetComponent<RandomMovePoint>().GetRandomPoint();
            return randomPosition;
        }

        public Color[] getHueColor() {
            Color[] hueColors = new Color[numOfBall];
            for (int i = 0; i < numOfBall; i++) {
                hueColors[i] = UnityEngine.Random.ColorHSV(0, 1f, 1f, 1f, 0, 1f);
            }
            return hueColors;
        }

        public void AssignBallNumSetUp(int numofBalls, float distance, float speed, float accer) {
            numOfBall = numofBalls;
            ballNumSpeed = speed;
            ballNumAccer = accer;
        }

        private bool LinkToGameObject() {
            GameSetUp gameSetUp = gameObject.GetComponent<GameSetUp>();
            if (gameSetUp?.platform != null) {
                if (gameSetUp.mathQLink) {
                    platform = gameSetUp.platform;
                    goal = gameSetUp.x;
                    return true;
                }
            }
            Debug.Log("Link to GameObject in Ball Num Set Up" + gameSetUp);
            return false;
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }


    }
}
