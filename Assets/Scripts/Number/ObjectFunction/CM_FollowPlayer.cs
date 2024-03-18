using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Number {
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

        private void OnDestroy() {
            calculationGame.GetComponent<GameSetUp>().GameObjectInitializeDone -= LinkToPlayer;
        }

    }
}
