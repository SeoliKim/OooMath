using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class TwoBubbleButton : MonoBehaviour , IDragHandler {
    [SerializeField] float selectOffSet = 25f;
    [SerializeField] private GameObject buttonPanel, shootButton;
    private RectTransform buttonPanelRectTransform;

    private bool onSelect;
    public bool GetOnSelect() {
        return onSelect;
    }
    public int selectButtonIndex;
   

    private void Start() {
        buttonPanelRectTransform= buttonPanel.GetComponent<RectTransform>();
        onSelect = false;
    }
    public void OnDrag(PointerEventData eventData) {
        selectButtonIndex = CheckIfSelect();
        if(selectButtonIndex < 0) {
            shootButton.SetActive(false);
            onSelect = false;
        }
        if(selectButtonIndex >= 0 ) {
            Color32 bubbleColor = buttonPanel.transform.GetChild(selectButtonIndex).gameObject.GetComponent<Image>().color;
            shootButton.GetComponent<Image>().color = bubbleColor;
            shootButton.SetActive(true);
            onSelect = true;
            Debug.Log("SelectBubbleButton: " + bubbleColor);
        }
        
        
    }

    private int CheckIfSelect() {
         Vector2 position = buttonPanelRectTransform.anchoredPosition;
        if (position.x >= 120 - selectOffSet) {
            position.x = 120;
            return 0;
        }
        if (position.x <= -120 + selectOffSet) {
            position.x = -120;
            return 1;
        }
        return -1;
    }
}
