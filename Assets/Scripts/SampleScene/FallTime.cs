using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTime : MonoBehaviour
{
    private float t = 0;
    public float height;
    bool fall = true;
    // Update is called once per frame

    private void Start() {
        gameObject.transform.position = new Vector3(0, height, 0);
    }
    void Update()
    {
        t += Time.deltaTime;
        while (fall) {
            if (gameObject.transform.position.y < 0) {
                Debug.Log("time used" + t);
                fall = false;
            }
        }
       

    }
}
