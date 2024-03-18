using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Number {
    public class HelpCardManager : MonoBehaviour {
        [Header("UI")]
        [SerializeField] private GameObject _MainCanvas;
        [SerializeField] private GameObject _HelpCardMain, _CoolDownBox;
        private Image coolDownImage;

        [Space]
        private float coolDownTime;
        private float timer;
        private bool OnCoolDown;
        private bool GameOn = false;

        [Space]
        [Header("HelpCard")]
        [SerializeField] private GameObject _CardBack;
        [SerializeField] private GameObject _SpeedCard, _SBloodCard, _BBloodCard, _BombCard, _BalloonCard, _LittleMeCard; 
            //_FakeMeCard, _SuperOooCard;
        /* HelpCard Index _SpeedCard-1
         * _SpeedCard 1
         * _BombCard 2
         * _BalloonCard 3
         * _LittleMeCard 4
         * _FakeMeCard 5
         * _SuperOooCard 6
         * 
         */

        public List<GameObject> helpCards = new List<GameObject>();

        //Link to objects
        [HideInInspector]
        public GameObject gameManager, player;
        public UIMessageReceiver uIMessageReceiver;

        #region Basic SetUp

        void Start() {
            gameManager = GameManager.GameManagerInstance.gameObject;
            gameManager.GetComponentInChildren<GameSetUp>().GameObjectInitializeDone += LinkToGameBasic;
            GameManager.OnGameStateChanged += OnGameStateChanged;
            uIMessageReceiver = _MainCanvas.GetComponent<UIMessageReceiver>();
            AddHelpCardList();
            SetUIAwake();
        }

        private void LinkToGameBasic(GameSetUp.GameArgs obj) {
            player = obj.player;
            SetHelpCardFunction();
        }

        private void OnGameStateChanged(GameState state) {
            if (state == GameState.GameOn) {
                GameOn = true;
            }
            if (state == GameState.Fail) {
                gameManager.GetComponentInChildren<GameSetUp>().GameObjectInitializeDone -= LinkToGameBasic;
            }
        }

        private void OnDestroy() {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }
        private void AddHelpCardList() {
            helpCards.Add(_SpeedCard);
            helpCards.Add(_SBloodCard);
            helpCards.Add(_BBloodCard);
            helpCards.Add(_BombCard);
            helpCards.Add(_BalloonCard);
            helpCards.Add(_LittleMeCard);
            //helpCards.Add(_FakeMeCard);
            //helpCards.Add(_SuperOooCard);
        }
        private void SetHelpCardFunction() {
            foreach(GameObject helpCard in helpCards) {
                helpCard.GetComponent<HelpCardFunction>().SetHelpCardFunction(this);
            }
        }

        private void SetUIAwake() {
            foreach (GameObject helpCard in helpCards) {
                helpCard.SetActive(false);
            }
            coolDownImage = _CoolDownBox.GetComponent<Image>();
            _CoolDownBox.SetActive(false);
            coolDownTime = 6f;
            timer = coolDownTime;

            /*
             * for (int i = 0; i < _HelpCardSave.transform.childCount; i++) {
             * GameObject saveCard = _HelpCardSave.transform.GetChild(i).gameObject;
             * saveCard.SetActive(false);
             * }
             */
        }

        #endregion

        #region HelpCardUtility
        void Update() {
            if (GameOn) {
                _CoolDownBox.SetActive(OnCoolDown);
                if (OnCoolDown) {
                    timer -= Time.deltaTime;
                    float coolDownShadowFillAmount = timer / coolDownTime;
                    coolDownImage.fillAmount = coolDownShadowFillAmount;
                    if(timer < 0) {
                        OnCoolDown = false;
                    }
                }
            }
        }

        public void Shuffle() {
            DefaultState();
        }

        private void DefaultState() {
            foreach (GameObject helpCard in helpCards) {
                helpCard.SetActive(false);
            }
            coolDownImage.fillAmount = 1;
            timer = coolDownTime;
            OnCoolDown = true;
        }

        public void TryGetNewCard() {
            if (OnCoolDown) {
                Debug.Log("On cool down");
                AudioManager.AudioManagerInstance.PlayAudio("wrong");
                return;
            }
            AudioManager.AudioManagerInstance.PlayAudio("ShuffleCardOn");
            GameObject newCard = RandomCard();
            UIShowCard(newCard);
        }

        private GameObject RandomCard() {
            int cardCount = helpCards.Count;
            int randomCardIndex = NumberGenerator.getRandomNumber(0, cardCount - 1);
            GameObject randomCard = helpCards[randomCardIndex];
            return randomCard;
        }

        private void UIShowCard(GameObject showcard) {
            foreach (GameObject helpCard in helpCards) {
                helpCard.SetActive(false);
            }
            showcard.SetActive(true);
        }

        public void UseCard(GameObject CardToUse) {
            CardToUse.SetActive(false);
            DefaultState();

            /*
             * if (originalActiveMainIndex > 0) { //Used card is the Save Card
                _HelpCardMain.transform.GetChild(originalActiveMainIndex).gameObject.SetActive(true);
                originalActiveMainIndex = 0;
            } 
            */

        }
        #endregion

        /*
        #region SaveCardUtility

        private bool Onsave;
        private int originalActiveMainIndex = 0;

        public void TrySaveCard(GameObject cardToSave) {
            if (Onsave) {
                Debug.Log("one card already save");
            } else {
                int cardToSaveIndex = cardToSave.transform.GetSiblingIndex();
                GameObject savingCardUI = _HelpCardSave.transform.GetChild(cardToSaveIndex).gameObject;
                SaveCardUI(savingCardUI);
                OnCoolDown = true;
                Onsave = true;
            }
        }

        private void SaveCardUI(GameObject savingCard) {
            foreach (GameObject helpCard in helpCards) {
                helpCard.SetActive(false);
            }
            for (int i = 0; i < _HelpCardSave.transform.childCount; i++) {
                GameObject saveCard = _HelpCardSave.transform.GetChild(i).gameObject;
                saveCard.SetActive(false);
            }
            savingCard.SetActive(true);

        }

        public void UseSaveCard(GameObject SaveCardToUse) {
            int cardToUseIndex = SaveCardToUse.transform.GetSiblingIndex();
            UseSaveCardUI(cardToUseIndex);
            Onsave = false;
        }

        private void UseSaveCardUI(int cardToUseIndex) {
            foreach (GameObject helpCard in helpCards) {
                if (helpCard.activeSelf) {
                    originalActiveMainIndex = helpCard.transform.GetSiblingIndex();
                }
                helpCard.SetActive(false);
            }
            for (int i = 0; i < _HelpCardSave.transform.childCount; i++) {
                GameObject saveCard = _HelpCardSave.transform.GetChild(i).gameObject;
                saveCard.SetActive(false);
            }
            _HelpCardMain.transform.GetChild(cardToUseIndex).gameObject.SetActive(true);
        }
        #endregion
        */

        }
    }
