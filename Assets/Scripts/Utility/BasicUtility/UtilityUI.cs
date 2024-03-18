using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityUI : MonoBehaviour
{
    public static void SetChildFalse(GameObject parent) {
        foreach (Transform child in parent.transform) {
            child.gameObject.SetActive(false);
        }
    }
}
