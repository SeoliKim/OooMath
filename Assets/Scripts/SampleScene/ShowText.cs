using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    [SerializeField] TMP_Text _text; 

    // Update is called once per frame
    void Update()
    {
        Vector3 accel = Input.acceleration;
        _text.text = accel.ToString();
    }
}
