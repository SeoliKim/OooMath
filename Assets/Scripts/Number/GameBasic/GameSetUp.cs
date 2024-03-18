using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.AI.Navigation;

namespace Number {
    public class GameSetUp : MonoBehaviour {

        #region Field

        [Header("Prefab")]
        [SerializeField] private GameObject _GameLevelPrefeb;
        [SerializeField] private GameObject _CMPrefab, _NumberStagePrefab, _MonsterFactoryPrefab, _OooHousePrefab;

        [Space]
        [SerializeField] private Material _PlatformMaterial;

        
        [Header("Player")]
        [SerializeField] private GameObject _PlayerPrefeb;
        [SerializeField] private GameObject _PowerBubbleHolderPrefab, _throwLinePrefab;
        public float playerAccelRate, playervMax, playerConstantRate;

        [HideInInspector]
        public GameObject numberStage, cm, gameLevel, bubbleHolder, monsterFactory, oooHouse;

        [HideInInspector]
        public GameObject player;
        private readonly Vector3 playerHomePos = new Vector3(0, 3, 25);

        public event Action<GameArgs> GameObjectInitializeDone;
        public class GameArgs : EventArgs {
            public GameObject numberStage;
            public GameObject player;
            public GameObject cm;
            public GameObject gameLevel;
        }

        #endregion



        #region PreGame-SetUp
        public void Start() {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
            StartCoroutine(SetUp());
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }

        IEnumerator SetUp() {
            SetUpNumberStage();
            SetUpMonsterFactory();
            SetUpOooHouse();
            SetUpPlayer();
            SetUpCM();
            SetUpGameLevel();
            yield return new WaitUntil(() => (gameLevel != null) && (numberStage != null) && (player != null) && (cm != null));
            GameObjectInitializeDone?.Invoke(new GameArgs { gameLevel = gameLevel, player = player, cm = cm, numberStage = numberStage });
            yield return new WaitForSeconds(2f);
            GameManager.GameManagerInstance.UpdateGameState(GameState.GameOn);
        }

        private bool SetUpNumberStage() {
            numberStage = Instantiate(_NumberStagePrefab, transform);
            numberStage.name = _NumberStagePrefab.name;
            numberStage.GetComponentInChildren<MeshRenderer>().material = _PlatformMaterial;
            numberStage.GetComponentInChildren<NavMeshSurface>().BuildNavMesh();
            Debug.Log("set up" + numberStage);
            return true;
        }

        private bool SetUpMonsterFactory() {
            monsterFactory = Instantiate(_MonsterFactoryPrefab, new Vector3 (0, 0, -30), Quaternion.identity, transform);
            monsterFactory.name = _MonsterFactoryPrefab.name;
            Debug.Log("set up" + monsterFactory);
            return true;
        }

        private bool SetUpOooHouse() {
            oooHouse = Instantiate(_OooHousePrefab, transform);
            oooHouse.name = _OooHousePrefab.name;
            Debug.Log("set up" + oooHouse);
            return true;
        }

        private bool SetUpGameLevel() {
            gameLevel = Instantiate(_GameLevelPrefeb, transform);
            gameLevel.name = _GameLevelPrefeb.name;
            Debug.Log("set up" + gameLevel);
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
            player = Instantiate(_PlayerPrefeb,playerHomePos, Quaternion.identity, transform);
            player.name = _PlayerPrefeb.name;
            playerConstantRate = 0.4f ;
            playerAccelRate = 8f ;
            playervMax = 3f ;
            bubbleHolder = Instantiate(_PowerBubbleHolderPrefab, Vector3.zero, Quaternion.identity, transform);
            bubbleHolder.name = _PowerBubbleHolderPrefab.name;
            PlayerNumberFunction playerNumberFunction= player.AddComponent<PlayerNumberFunction>();
            playerNumberFunction.SetPlayerNumberFunction(bubbleHolder, _throwLinePrefab);
            Debug.Log("set up" + player);
            return true;
        }

        #endregion

        private void GameManager_OnGameStateChanged(GameState state) {
            if(state == GameState.GameOn) {
                PlayerAddMotion();
                BroadcastMessage("SetCM", cm, SendMessageOptions.DontRequireReceiver);
                BroadcastMessage("SetPlayer", player, SendMessageOptions.DontRequireReceiver);
                AudioManager.AudioManagerInstance.PlayMusic("theme");
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
            return true;
        }

    }
}
