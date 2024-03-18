using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

namespace Calculation {
    public class RotateIntegralAdder : MonoBehaviour {
        private GameObject _RotateIntegralPrefab;
        public List<GameObject> integrals;
        public float rotatingSpeed;
        private int hardLevel;

        private bool clash = false;
        
        public void AddRotateIntegral(GameObject _RotateIntegralPrefab, int hardLevel) {
            this._RotateIntegralPrefab = _RotateIntegralPrefab;
            integrals = new List<GameObject>();
            this.hardLevel = hardLevel;
            HandleHardLevel();
            
        }

        private void HandleHardLevel() {
            rotatingSpeed = 50 + (hardLevel/3) * 25;
            int amountMin = 1 + (hardLevel / 3);

            StartCoroutine(GetReady(amountMin));
        }

        private IEnumerator GetReady(int amountMin) {
            int amount = NumberGenerator.getRandomNumber(amountMin, amountMin+3);

            GameObject platform = gameObject.GetComponent<GameSetUp>().platform;

            for (int i = 0; i < amount; i++) {
                Vector3 surfaceposition = PlatformSurface.GetRandomPointOnPlatform(platform);
                GameObject integral = Instantiate(_RotateIntegralPrefab, surfaceposition, Quaternion.identity, transform);
                integral.transform.localScale = GetRandomScale();
                integral.name = _RotateIntegralPrefab.name;
                integrals.Add(integral);
                integral.GetComponent<RotateObstacle>().rotatingSpeed = rotatingSpeed;
                int count = 0;
                while (clash && count < 10000) {
                    integral.transform.position = PlatformSurface.GetRandomPointOnPlatform(platform);
                    count++;
                    Debug.Log("clash" + count);
                }
                clash = false;
                NavMeshSurface navMeshSurface = integral.GetComponent<NavMeshSurface>();
                navMeshSurface.BuildNavMesh();
            }

            yield return null;
        }

        private Vector3 GetRandomScale() {
            float r = Random.Range(1.8f, 4);
            return new Vector3(r, r, r);
        }
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Structure")) {
                clash = true;
            }
        }
    }
}
