using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTabControler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> OpenTabs, CloseTabs;

    public void OpenTab() {
        foreach(GameObject opentab in OpenTabs) {
            if (opentab != null) {
                opentab.SetActive(true);
            }
        }

        foreach (GameObject closetab in CloseTabs) {
            if (closetab != null) {
                closetab.SetActive(true);
            }
        }

    }
}
