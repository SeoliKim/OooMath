using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNumLabelGenerator : MonoBehaviour
{
    public static float IntLabelGenerator(int low, int up) {
        float n= NumberGenerator.getRandomNumber(low, up);
        return n;
    }

    
}
