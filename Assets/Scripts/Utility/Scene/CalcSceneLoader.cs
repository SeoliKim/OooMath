using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Calculation {
    public class CalcSceneLoader : SceneLoader {

        [SerializeField] private GameObject _NotEnoughPanel;

        private void Start() {
            _NotEnoughPanel.SetActive(false);
        }
        public override void LoadScene(int SceneIndex) {
            GameManager.GameManagerInstance.Nullify();
            StartCoroutine(LoadSceneCoroutine(SceneIndex));
        }

        public void RetryGame() {
            PlayFabManager.PlayFabManagerInstance.SpendBP(1);
            PlayFabManager.PlayFabManagerInstance.SpendResult += SpendResult;
        
        }

        private void SpendResult(bool success) {
            PlayFabManager.PlayFabManagerInstance.SpendResult -= SpendResult;
            if (success) {
                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                AudioManager.AudioManagerInstance.PlayAudio("startGame");
                LoadScene(sceneIndex);
            } else {
                _NotEnoughPanel.SetActive(true);
            }
        }
    }
}
