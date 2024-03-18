using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransEnterFunction : MonoBehaviour
{
    [SerializeField] private GameObject _TransExit;
    private Vector3 exitPos, enterPos;
    private bool onTeleport;
    private GameObject teleportObject;

    private void Start() {
        enterPos = gameObject.transform.position;
        exitPos = new Vector3(_TransExit.transform.position.x, .5f, _TransExit.transform.position.z-1);
    }
    private void Update() {
        if(onTeleport) {
            teleportObject.transform.position = enterPos;
        }
    }

    public void SetExitPos(Vector3 exitPos) {
        this.exitPos = exitPos;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            GameObject player = other.gameObject;
            StartCoroutine(Teleport(player));
        }
    }

    private IEnumerator Teleport(GameObject gameObject) {
        teleportObject = gameObject;
        onTeleport = true;
        yield return new WaitForSeconds(1f);
        onTeleport = false;
        teleportObject = null;
        gameObject.transform.position = exitPos;
    }
}
