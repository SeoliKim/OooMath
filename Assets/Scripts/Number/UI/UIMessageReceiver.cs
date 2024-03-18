using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Number {
    public class UIMessageReceiver : MonoBehaviour {

        public GameObject gameManager, numberGame;
        public GameObject player;
        public int waveNum, round;

        private GameSetUp GameSetUp;


        private void Start() {
            gameManager = GameManager.GameManagerInstance.gameObject;
            GameManager.OnGameStateChanged += OnGameStateChanged;
            numberGame = gameManager.transform.GetChild(0).gameObject;
            GameSetUp = numberGame.GetComponent<GameSetUp>();
            GameSetUp.GameObjectInitializeDone += GameObjectInitializeDone;
            NumMonsterManager numMonsterManager = numberGame.GetComponent<NumMonsterManager>();
            numMonsterManager.MonsterDie += MonsterDie;
            bloodBarImage = _BloodBar.GetComponent<Image>();
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void Update() {
            
        }

        private void OnGameStateChanged(GameState state) {
            if(state == GameState.Fail) {
                User.I.AddTotalNumGame(1);
                SaveVCurrecy();
                GameSetUp.GameObjectInitializeDone -= GameObjectInitializeDone;
                player.GetComponent<PlayerNumberFunction>().ChangePlayerBlood -= ChangePlayerBlood;
                NumMonsterGenerator numMonsterGenerator = numberGame.GetComponentInChildren<NumMonsterGenerator>();
                numMonsterGenerator.GenerateNewWave -= GenerateNewWave;
            }
        }


        private void GenerateNewWave(int waveNum) {
            this.waveNum = waveNum;
            round = waveNum / 5 + 1;
        }

        private void GameObjectInitializeDone(GameSetUp.GameArgs args) {
            Debug.Log("UI Receiver + GameObjectInitializeDone + broadcastmessage");
            NumMonsterGenerator numMonsterGenerator = numberGame.GetComponentInChildren<NumMonsterGenerator>();
            numMonsterGenerator.GenerateNewWave += GenerateNewWave;
            player = args.player;
            player.GetComponent<PlayerNumberFunction>().ChangePlayerBlood += ChangePlayerBlood;
        }

        private void SaveVCurrecy() {
            PlayFabManager.PlayFabManagerInstance.AddCP(moneyCount);
        }

        

        #region GameMenu Info
        [Space]
        [Header("GameMenu Info")]
        public int moneyCount;
        public int killCount;
        public int starPtCount;
        [SerializeField] private TMP_Text _MoneyCount, _killCount, _StarPtCount;
        [SerializeField] private GameObject _BloodBar;
        private Image bloodBarImage;
        private float bloodValue;
        

        private void MonsterDie(GameObject obj) {
            AddKill();
            AddStarPt(2);
        }

        private void ChangePlayerBlood(float amount) {
            UpdateBloodValue(amount);
        }


        private void UpdateBloodValue(float amount) {
            bloodValue = amount;
            BloodBarUI(bloodValue);
        }

        private void BloodBarUI(float bloodValue) {
            float bloodBarFill = (float)(bloodValue / 100);
            bloodBarImage.fillAmount = bloodBarFill;
            if(bloodValue > 50) {
                bloodBarImage.color = Color.green;
            }else if (bloodValue > 20) {
                bloodBarImage.color = Color.yellow;
            } else {
                bloodBarImage.color = Color.red;
            }
            
        }


        public void AddMoney() {
            moneyCount++;
            _MoneyCount.text = moneyCount.ToString();
        }

        public void AddKill() {
            killCount++;
            _killCount.text = killCount.ToString();
        }

        public void AddStarPt(int amount) {
            starPtCount += amount;
            _StarPtCount.text = starPtCount.ToString();
        }

        public bool SpendStarPt(int amount) {
            int resultStarPt =starPtCount - amount;
            if (resultStarPt < 0) {
                return false;
            }
            starPtCount -= amount;
            _StarPtCount.text = starPtCount.ToString();
            return true;
        }
        #endregion

    }
}
