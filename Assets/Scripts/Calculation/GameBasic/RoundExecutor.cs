using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Calculation {
    public class RoundExecutor : MonoBehaviour {

        [Header("Prefabs")]
        [SerializeField]
        private GameObject[] _CubeObstacleNavmeshPrefabs;
        [SerializeField]
        private GameObject[] _NumberObstacleNavmeshPrefabs;
        [SerializeField]
        private GameObject[] _sillyMonsterNavmeshPrefabs = new GameObject[3];
        [SerializeField]
        private GameObject _RotateIntegralPrefab, _NullSetBombPrefab, _AngryUnEqualPrefab, _moneyPrefab, _SlowShadowPrefab, _Calc_TransDoorPrefab;
        [SerializeField]
        private GameObject _RecMonsterNavmeshPrefab0;

        [Header("GameObjects")]
        public List<GameObject> recMonsters = new List<GameObject>();
        public List<GameObject> obstacles;
        public List<GameObject> money;

        private GameSetUp GameSetUp;
        private int roundCount;
        private int hardLevel;

        private void Awake() {
            roundCount = RoundManager.RoundManagerInstance.GetRoundCount();
            Debug.Log("roundexecuter ready to listen");
        }

        private void Start() {
            GameSetUp = gameObject.GetComponent<GameSetUp>();
            hardLevel = (int)(roundCount / 10) + 1;
            obstacles = gameObject.AddComponent<BackgroundObstacle>().SetBackgoundObstacle(_CubeObstacleNavmeshPrefabs, _NumberObstacleNavmeshPrefabs, hardLevel);
            money = gameObject.AddComponent<MoneyAdder>().SetMoneyAdder(_moneyPrefab, 9f);
            if (roundCount > 1) {
                AddMonster();
            }
            
            ExecuteFeature();
        }

         private void AddMonster() {
            if (roundCount > 2) {
                this.gameObject.AddComponent<RecMonsterAdder>().SetRecMonsterAdder(roundCount,_RecMonsterNavmeshPrefab0,0);
            }

            if (roundCount > 40) {
                Instantiate(_Calc_TransDoorPrefab, transform);

            }


            SillyMonsterAdder sillyMonsterAdder = gameObject.AddComponent<SillyMonsterAdder>();
            sillyMonsterAdder.SetSillyMonsterAdder(_sillyMonsterNavmeshPrefabs, ((int)(1.5*hardLevel)+2));
        }

        private void ExecuteFeature() {
            List<int> indexList = new List<int>();
            indexList= SelectFeatureIndex();
            foreach (int featureIndex in indexList) {
                switch (featureIndex) {
                    case 1:
                        gameObject.AddComponent<RandomObstacle>().SetRandomObstacle( obstacles,_CubeObstacleNavmeshPrefabs, hardLevel);
                        break;
                    case 2:
                        gameObject.AddComponent<FocusObstacles>().SetFocusObstacle( obstacles, _CubeObstacleNavmeshPrefabs, hardLevel);
                        break;
                    case 3:
                        gameObject.AddComponent<RotateIntegralAdder>().AddRotateIntegral(_RotateIntegralPrefab, hardLevel);
                        break;
                    case 4:
                        gameObject.AddComponent<NullSetBombAdder>().SetNullSetBombAdder(_NullSetBombPrefab, hardLevel);
                        break;
                    case 5:
                        gameObject.AddComponent<AngryUnEqualAdder>().SetAngryUnEqualAdder(_AngryUnEqualPrefab, hardLevel);
                        break;
                    case 6:
                        gameObject.AddComponent<SlowShadowAdder>().SetSlowShadow(_SlowShadowPrefab, hardLevel);
                        break;

                }

            }
        }

        private List<int> SelectFeatureIndex() {
            int[] numArray = { 1, 2, 3, 4, 5, 6 };
            List<int> numList = new List<int>(numArray);
            List<int> indexList = new List<int>();

            if (roundCount >= 5) {
                int choice = GetRandomInNumList(numList);
                numList.Remove(choice);
                indexList.Add(choice);
            }
            if(roundCount >= 15) {
                int choice = GetRandomInNumList(numList);
                numList.Remove(choice);
                indexList.Add(choice);
            } 
            if(roundCount >= 40) {
                int choice = GetRandomInNumList(numList);
                numList.Remove(choice);
                indexList.Add(choice);
            }
            if (roundCount >= 70) {
                int choice = GetRandomInNumList(numList);
                numList.Remove(choice);
                indexList.Add(choice);
            }
            if (roundCount >= 100) {
                int choice = GetRandomInNumList(numList);
                numList.Remove(choice);
                indexList.Add(choice);
            }

            Debug.Log("index List" + indexList);

            return indexList;
        }

        private int findIndexinList(List<int> list, int num) {
            for (int i =0; i < list.Count; i ++) {
                if (list[i] == num) {
                    return i;
                }
            }
            return -1;
        }

        private int GetRandomInNumList(List<int> list) {
            int n = NumberGenerator.getRandomNumber(0, list.Count-1);
            return list[n];
        }

        private void CheckIndexList(List<int> indexList) {
            for (int i = 0; i < indexList.Count; i++) {
                int count = 0;
                while (!NumberGenerator.NoRepetitionCheck(indexList, indexList[i]) && count < 10000) {
                    int n = NumberGenerator.getRandomNumber(1, 6);
                    indexList[i] = n;
                    count++;
                }
            }
        }

        public int GetHardLevel() {
            return hardLevel;
        }
    }
}
