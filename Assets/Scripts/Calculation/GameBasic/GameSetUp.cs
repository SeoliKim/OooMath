using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.AI.Navigation;

namespace Calculation {
    public class GameSetUp : MonoBehaviour {

        #region Field
       
        [Space]
        [Header("Prefabs")]
        [SerializeField] private GameObject _GameLevelPrefeb;
        private Math_CalculationLevel math_CalculationLevel;
        [SerializeField] private GameObject _PlayerPrefeb, _CMPrefab, _PlatformPrefab, _BallNumPrefab, _ClawPrefab;

        private Material platformMaterial;

        [HideInInspector]
        public GameObject platform, player, cm, gameLevel, claw;
        [HideInInspector]
        public GameObject[] ballNums;
        private bool ballNumDoneCheck = false;


        public event Action<GameArgs> GameObjectInitializeDone;

        public class GameArgs : EventArgs {
            public GameObject platform;
            public GameObject player;
            public GameObject cm;
            public GameObject[] ballNums;
            public GameObject gameLevel;
        }

        [Header("Speed")]
        public float ballNumSpeed;
        public float ballNumAccer, playerAccelRate, playerConstantRate, playervMax,
            sillyMonsterSpeed, sillyMonsterAccer;

        [Space]
        [Header("Math Question")]
        public bool mathQLink = false;
        public float x;
        private int numofBall;

        private int roundCount;
        private readonly float speedIncrease = 1.07f; 
        #endregion


#region GetReady-GameSetUp
        //Instantiate GameObjects
        public void SetGameSetUp (GameObject gameLevelPrefab, int roundCount) {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            _GameLevelPrefeb = gameLevelPrefab;
            this.roundCount = roundCount;
            platformMaterial = _GameLevelPrefeb.GetComponent<Math_CalculationLevel>().GetPlatformMaterial();
            //RoundManager.RoundUpdated += RoundManager_RoundUpdated;
            InistializatizeGame();
            StartCoroutine(SetUp());
        }

        private void InistializatizeGame() {
            int speedlevel = roundCount / 10;
            if (speedlevel > 20) {
                speedlevel = 20;
            }
            this.gameObject.transform.localPosition = Vector3.zero;
            ballNums = new GameObject[numofBall];
            ballNumSpeed = 2 * Mathf.Pow(speedIncrease, speedlevel);
            ballNumAccer = 6 * Mathf.Pow(speedIncrease, speedlevel);
            numofBall = 12;
            sillyMonsterSpeed = 2 * Mathf.Pow(speedIncrease, speedlevel);
            sillyMonsterAccer = 6 * Mathf.Pow(speedIncrease, speedlevel);
            playerConstantRate = 0.7f * Mathf.Pow(speedIncrease, speedlevel);
            playerAccelRate = 9f * Mathf.Pow(speedIncrease, speedlevel);
            playervMax = 9 * Mathf.Pow(speedIncrease, speedlevel);
        }

        IEnumerator SetUp() {
            SetUpGameLevel();
            SetUpPlatform();
            SetUpPlayer();
            SetUpCM();
            yield return new WaitUntil(() => (gameLevel != null) && (platform != null) && (player != null) && (cm != null) && ballNumDoneCheck);
            GameObjectInitializeDone?.Invoke(new GameArgs { gameLevel = gameLevel, player = player, ballNums = ballNums, cm = cm, platform = platform });
        }

        private bool SetUpGameLevel() {
            gameLevel = Instantiate(_GameLevelPrefeb, transform);
            gameLevel.name = _GameLevelPrefeb.name;
            math_CalculationLevel = gameObject.GetComponentInChildren<Math_CalculationLevel>();
            math_CalculationLevel.MathProblemSetDone += MathProbalemDone;
            Debug.Log("set up" + gameLevel);
            return true;
        }

        private void MathProbalemDone(Math_CalculationLevel.MathProblemArgs mathProblemArgs) {
            x = mathProblemArgs.xA;
            mathQLink = true;
            SetUpBallNum();
            gameObject.GetComponentInChildren<Math_CalculationLevel>().MathProblemSetDone -= MathProbalemDone;
        }


        private bool SetUpPlatform() {
            platform = Instantiate(_PlatformPrefab, transform);
            platform.name = _PlatformPrefab.name;
            platform.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = platformMaterial;
            NavMeshSurface navMeshSurface = platform.GetComponent<NavMeshSurface>();
            navMeshSurface.BuildNavMesh();

            Debug.Log("set up" + platform);
            return true;
        }

        private bool SetUpCM() {
            cm = Instantiate(_CMPrefab, transform);
            cm.AddComponent<CM_FollowPlayer>();
            cm.name = _CMPrefab.name;
            Debug.Log("set up" + cm);
            return true;
        }

        private bool SetUpPlayer() {
            player = Instantiate(_PlayerPrefeb, new Vector3 (0, 5, 0), Quaternion.identity, transform);
            player.name = _PlayerPrefeb.name;
            player.AddComponent<PlayerMathGame>();
            claw = Instantiate(_ClawPrefab, player.transform);
            claw.name = _ClawPrefab.name;
            claw.SetActive(false);
            ClawControler clawControler= player.AddComponent<ClawControler>();
            Debug.Log("set up" + player + clawControler.enabled);
            return true;
        }

        private void SetUpBallNum() {
            SetUpBallNum setUpBallNum = this.gameObject.AddComponent<SetUpBallNum>().SetSetUpBallNum(_BallNumPrefab ,numofBall, ballNumSpeed, ballNumAccer, math_CalculationLevel);
            setUpBallNum.ballNum_Prefab = this._BallNumPrefab;
            setUpBallNum.BallNumSetUpDone += SetUpBallNum_BallNumSetUpDone;

        }

        private void SetUpBallNum_BallNumSetUpDone(GameObject[] bns) {
            ballNums = bns;
            ballNumDoneCheck = true;
        }


        #endregion


        #region GameOn- GameSetup
        //Add Game Functionality
        private void GameManager_OnGameStateChanged(GameState state) {
            if (state == GameState.GameOn) {
                PlayerAddMotion();

            }
        }
        private bool PlayerAddMotion() {
            Rigidbody rb = player.AddComponent<Rigidbody>();
            rb.mass = 5;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            Player3DMover player3DMover = player.AddComponent<Player3DMover>();
            player3DMover._Rotator = cm.transform;
            player3DMover.accelRate = playerAccelRate;
            player3DMover.vMax = playervMax;
            player3DMover.constantRate = playerConstantRate;
            player.GetComponent<ClawControler>().AssignCameraRotator(player3DMover._Rotator);
            return true;
        }
        #endregion

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
            //RoundManager.RoundUpdated -= RoundManager_RoundUpdated;
        }
    }
}
