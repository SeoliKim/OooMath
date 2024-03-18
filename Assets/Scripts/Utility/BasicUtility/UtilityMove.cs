using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityMove : MonoBehaviour
{
    public static IEnumerator LerpPosition(GameObject gameObject, Vector3 startPosition, Vector3 targetPosition, float duration) {
        float time = 0;
        while (time < duration) {
            gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = targetPosition;
    }

    public static IEnumerator LerpLocalPosition(GameObject gameObject, Vector3 startPosition, Vector3 targetPosition, float duration) {
        float time = 0;
        while (time < duration) {
            gameObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.localPosition = targetPosition;
    }
}
