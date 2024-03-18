using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float movetime, waittime;
    public Vector3 firstPosition, secondPosition;

    private bool move;

    // Start is called before the first frame update
   
    public void SetMovingObstacle(float movetime, float waittime, Vector3 firstPosition, Vector3 secondPosition) {
        this.movetime = movetime;
        this.waittime = waittime;
        this.firstPosition = firstPosition;
        this.secondPosition = secondPosition;
    }

    private void Start() {
        move = true;
        StartCoroutine(MoveBetweenPosition());
    }


    IEnumerator MoveBetweenPosition() {
        while(move) {
            float mtLow = movetime - 2;
            if (mtLow < 1) {
                mtLow = 1;
            }
            float mt = Random.Range(mtLow, movetime);
            yield return StartCoroutine(SmoothLerp(mt, firstPosition, secondPosition));
            yield return new WaitForSecondsRealtime(waittime);
            yield return StartCoroutine(SmoothLerp(mt, secondPosition, firstPosition));
            yield return new WaitForSecondsRealtime(waittime);
        }
    }

    private IEnumerator SmoothLerp(float time, Vector3 startPosition, Vector3 finalposition) {
        float elapsedTime = 0;

        while (elapsedTime < time) {
            transform.position = Vector3.Lerp(startPosition, finalposition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
