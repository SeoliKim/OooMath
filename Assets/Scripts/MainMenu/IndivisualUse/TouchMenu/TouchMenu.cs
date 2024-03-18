using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMenu : MonoBehaviour
{
    private void OnDisable() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child =transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }

    public void OpenTouchPage(int pageindex) {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
        }

        transform.GetChild(pageindex).gameObject.SetActive(true);

    }

    public void CloseTouchMenu() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }
}
