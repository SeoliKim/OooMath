using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Calculation {
    public class CM_FollowPlayer : MonoBehaviour {
        private GameObject calculationGame;
        [SerializeField]private GameObject player;
        private CinemachineVirtualCamera vcam;
        private void Awake() {
            vcam = GetComponent<CinemachineVirtualCamera>();
            calculationGame = transform.parent.gameObject;
            calculationGame.GetComponent<GameSetUp>().GameObjectInitializeDone += LinkToPlayer;
            
        }

        private void LinkToPlayer(GameSetUp.GameArgs obj) {
           player = obj.player;
            vcam.LookAt = player.transform;
            vcam.Follow = player.transform;
        }

        public void GameFailResponse() {
            gameObject.GetComponent<InputAccelRotator>().enabled = false;
            transform.rotation = Quaternion.identity;
        }

        public void GamePassResponse() {
            gameObject.GetComponent<InputAccelRotator>().enabled = false;
            transform.rotation = Quaternion.identity;
        }

        private void OnDestroy() {
            calculationGame.GetComponent<GameSetUp>().GameObjectInitializeDone -= LinkToPlayer;
        }

    }
}
