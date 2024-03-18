using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberLabel_BallNum : MonoBehaviour
{
    [SerializeField]
    public TMP_Text numberLabelText, numberLabelText2;


    // Start is called before the first frame update
    void Start()
    {
        //change text
        numberLabelText.text = this.GetComponent<BallNumVariable>().numberLabel.ToString();
        numberLabelText2.text = this.GetComponent<BallNumVariable>().numberLabel.ToString();


    }

    
}
