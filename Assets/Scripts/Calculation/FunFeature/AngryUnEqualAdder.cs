using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class AngryUnEqualAdder : MonoBehaviour {

        private GameObject _AngryUnEqualPrefab;
        public List<GameObject> unEquals;
        private int hardLevel;
        public float movetime, waittime;

        public void SetAngryUnEqualAdder(GameObject _AngryUnEqualPrefab, int hardLevel) {
            this._AngryUnEqualPrefab = _AngryUnEqualPrefab;
            unEquals = new List<GameObject>();
            this.hardLevel = hardLevel;
            HandleHardLevel();
        }

        private void HandleHardLevel() {
            int amountMin = 0;
            switch (hardLevel) {
                case 1:
                case 2:
                case 3:
                    movetime = 8f;
                    waittime = 1f;
                    amountMin = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    movetime = 8f;
                    waittime = 1f;
                    amountMin = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    movetime = 7f;
                    waittime = .5f;
                    amountMin = 3;
                    break;
                case 10:
                case 11:
                case 12:
                    movetime = 6f;
                    waittime = .5f;
                    amountMin = 3;
                    break; 
                default:
                    movetime = 4f;
                    waittime = .5f;
                    amountMin = 3;
                    break;
            }
            StartCoroutine(GetReady(amountMin));
        }
        private IEnumerator GetReady(int amountMin) {
            int amount = NumberGenerator.getRandomNumber(amountMin, amountMin+2);

            int firstXPos = NumberGenerator.getRandomNumber(-9, (9 - amount));
            for (int i = 0; i < amount; i++) {
                Vector3 pos = new Vector3((firstXPos+i)*10, 0, 50);
                Vector3 posF = new Vector3((firstXPos + i)*10, 0, -50);
                GameObject angryUnEqual = Instantiate(_AngryUnEqualPrefab, pos, Quaternion.identity, transform);
                angryUnEqual.name = _AngryUnEqualPrefab.name;
                angryUnEqual.GetComponent<MovingObstacle>().SetMovingObstacle(movetime, waittime, pos, posF);
                unEquals.Add(angryUnEqual);
            }
            yield return null;
        }

    }
}
