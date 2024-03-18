using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private CinemachineVirtualCamera vcam;

    private void Awake() {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void LinkToPlayer(GameObject player) {
        this.player = player;
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
    }
}
