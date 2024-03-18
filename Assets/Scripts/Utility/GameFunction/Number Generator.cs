using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour
{
    public static int getRandomNumber(int low, int up) {
        int answer = (int) Random.Range(low, (up+1));
        return answer;
    }
    public static Vector3 RandomScale(float min, float max) {
        float xScale = Random.Range(min,max);
        float yScale = Random.Range(min, max);
        float zScale = Random.Range(min, max);
        return new Vector3(xScale, yScale, zScale);
    }

    public static Vector3 RandomScale(float minx, float maxx, float miny, float maxy, float minz, float maxz) {
        float xScale = Random.Range(minx, maxx);
        float yScale = Random.Range(miny, maxy);
        float zScale = Random.Range(minz, maxz);
        return new Vector3(xScale, yScale, zScale);
    }
    public static bool NoRepetitionCheck(Object[] array,Object obj) {
        for (int i = 0; i < array.Length; i++) {
            if (array[i].Equals(obj)) {
                return false;
            }
        }
        return true;
    }

    public static bool NoRepetitionCheck(float[] array, float obj) {
        for (int i = 0; i < array.Length; i++) {
            if (array[i] == obj) {
                return false;
            }
        }
        return true;
    }

    public static bool NoRepetitionCheck(List<float> list, float obj) {
        for (int i = 0; i < list.Count; i++) {
            if (list.IndexOf(i) == obj) {
                return false;
            }
        }
        return true;
    }

    public static bool NoRepetitionCheck(List<int> list, int obj) {
        for (int i = 0; i < list.Count; i++) {
            if (list.IndexOf(i) == obj) {
                return false;
            }
        }
        return true;
    }
}
