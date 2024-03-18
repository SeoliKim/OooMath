using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallNumVariable : MonoBehaviour
{
    public float numberLabel;
    public Color32 color;

    private void Start() {
        color = gameObject.transform.Find("Hat").GetComponent<MeshRenderer>().material.color;

        
    }


}
