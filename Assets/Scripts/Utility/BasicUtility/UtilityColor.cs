using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UtilityColor : MonoBehaviour
{
    public static void setAlphaLevelUI(GameObject gameObject, float fromAlpha, float toAlpla, float time, LeanTweenType leanTweenType) {
        Image r = gameObject?.GetComponent<Image>();
            LeanTween.value(gameObject, fromAlpha, toAlpla, time).setEase(leanTweenType).setOnUpdate((float val)=>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
            });
    }

    public static void setAlphaLevelUI(GameObject gameObject, float fromAlpha, float toAlpla, float time) {
        Image r = gameObject?.GetComponent<Image>();
        LeanTween.value(gameObject, fromAlpha, toAlpla, time).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        });

    }

    public static void setAlphaLevelUIText(GameObject gameObject,  float fromAlpha, float toAlpla, float time) {
        TextMeshProUGUI tMP_Text = gameObject.GetComponent<TextMeshProUGUI>();
        LeanTween.value(gameObject, fromAlpha, toAlpla, time).setOnUpdate((float val) => {
            Color c = tMP_Text.color;
            c.a = val;
            tMP_Text.color = c;
        });

    }

    public static void setAlphaLevelUIText(GameObject gameObject, float fromAlpha, float toAlpla, float time, LeanTweenType leanTweenType) {
        TextMeshProUGUI tMP_Text = gameObject.GetComponent<TextMeshProUGUI>();
        LeanTween.value(gameObject, fromAlpha, toAlpla, time).setEase(leanTweenType).setOnUpdate((float val) => {
            Color c = tMP_Text.color;
            c.a = val;
            tMP_Text.color = c;
        });

    }
}
