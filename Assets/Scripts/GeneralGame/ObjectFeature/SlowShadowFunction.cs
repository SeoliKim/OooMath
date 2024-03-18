using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowShadowFunction : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;

    public void SetSlowShadow(GameObject player) {
        this.player = player;
        rb = player.GetComponent<Rigidbody>();
    }

    private void Start() {
        GetScale();
    }

    private void GetScale() {
        float scale = Random.Range(3, 4);
        transform.localScale = new Vector3(scale, .1f, scale);
    }

    private void OnTriggerStay(Collider collider) {
        //Debug.Log(gameObject.name + collider.gameObject.name);
        if (collider.gameObject.CompareTag("Player")) {
            rb.velocity *= 0.8f;
            
        }
    }


}
