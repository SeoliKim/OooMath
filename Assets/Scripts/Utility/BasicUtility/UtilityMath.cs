using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityMath : MonoBehaviour
{
    public static int GCD(int num1, int num2) {
        int Remainder;

        while (num2 != 0) {
            Remainder = num1 % num2;
            num1 = num2;
            num2 = Remainder;
        }

        return num1;
    }
}
