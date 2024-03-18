using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class PowerBubbleHolder : MonoBehaviour {
        [SerializeField] GameObject _PowerBubblePrefab;
        public int count= 5;
        public List<GameObject> powerBubbles;



        public void Awake() {
            for (int i = 0; i < count; i++) {
                GameObject powerBubble = Instantiate(_PowerBubblePrefab, transform);
                powerBubble.SetActive(false);
                powerBubbles.Add(powerBubble);
                powerBubble.name = _PowerBubblePrefab.name + powerBubbles.Count.ToString();

            }
        }

        
    }
}
