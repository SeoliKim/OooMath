using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool{
        public Object key;
        public GameObject prefab;
        public int size;

    }

    public List<Pool> objectPools;
    public static Dictionary<Object, Queue<GameObject>> objectDictionary;


    void Start() {
        objectDictionary = new Dictionary<Object, Queue<GameObject>>();

        foreach (Pool bubblePool in objectPools) {
            Queue<GameObject> bubbleQueue = new Queue<GameObject>();

            for (int i = 0; i < bubblePool.size; i++) {
                GameObject bubble = Instantiate(bubblePool.prefab);
                bubble.SetActive(false);
                bubbleQueue.Enqueue(bubble);
            }
            objectDictionary.Add(bubblePool.key, bubbleQueue);
        }
    }

    public static GameObject SpawnFromPool(Object key) {
        if (objectDictionary.ContainsKey(key)) {
            Debug.LogWarning("key is invalid: " + key + "fail to spawn from pool");
            return null;
        }
        GameObject objectToSpawn = objectDictionary[key].Dequeue();

        objectToSpawn.SetActive(true);

        objectDictionary[key].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}

