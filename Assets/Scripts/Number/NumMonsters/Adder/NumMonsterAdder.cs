using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Number {
    public class NumMonsterAdder : MonoBehaviour {
        protected GameObject player;
        protected List<GameObject> numMonsters = new List<GameObject>();
        //add another list to contain the specific type of numMonster
        private GameObject _NumMonsterPrefab;
        [HideInInspector] public float speed, accer, angleAccer;
        public int speedLevel;

        protected NumberType_Level numberType_Level;
        protected NumMonsterGenerator numMonsterGenerator;
        protected Vector3 factoryMouthPos;
        protected NumMonsterManager numMonsterManager;
        protected GameObject numberGame;

        public void SetNumMonsterAdder(GameObject _BasicNumMonsterPrefab, List<GameObject> numMonsters, NumMonsterGenerator numMonsterGenerator) {
            this._NumMonsterPrefab = _BasicNumMonsterPrefab;
            this.numMonsters = numMonsters;
            numberGame = GameManager.GameManagerInstance.gameObject.transform.Find("NumberGame").gameObject;
            this.numMonsterGenerator = numMonsterGenerator;
            this.factoryMouthPos = numMonsterGenerator.factoryMouthPos;
            this.numberType_Level = numMonsterGenerator.numberType_Level;
            this.numMonsterManager = numMonsterGenerator.numMonsterManager;
            InitializeMotion();
        }

        protected virtual void InitializeMotion() {
            speed = 0;
            accer = 0;
            angleAccer = 0;
        }

        public void LinkToPlayer(GameObject player) {
            this.player = player;
        }


        public IEnumerator CreateNumMonsterWave(int waveNum, int round) {
            int count = CalculateMonsterCount(waveNum, round);
            yield return StartCoroutine(AddNumMonster(factoryMouthPos, count, waveNum));
            numMonsterManager.SaveNMToRecord(count, round);
        }

        protected virtual int CalculateMonsterCount(int waveNum, int round) {
            return 0;

        }


        public IEnumerator AddNumMonster(Vector3 position, int count, int waveNum) {
            for (int i = 0; i < count; i++) {
                GameObject numMonster = Instantiate(_NumMonsterPrefab, position, Quaternion.identity, numberGame.transform);
                numMonster.name = _NumMonsterPrefab.name;
                numMonsters.Add(numMonster);
                //Link to GameBasic
                numMonster.GetComponent<NumMonsterFunction>().LinkToGameBasic(numberType_Level, numMonsterManager, numMonsterGenerator);
                //Assign color
                Color32 color = AssignColor();
                numMonster.GetComponent<NumMonsterFunction>().AssignColor(color);
                //Assign AI
                AssignAI(numMonster);
                //Assgn Motion
                AssignMotion(numMonster, waveNum);
                yield return new WaitForSeconds(.5f);
            }
        }


        protected virtual Color32 AssignColor() {
            Color32 color = UnityEngine.Random.ColorHSV(0, 1f, 1f, 1f, 0, 1f);
            return color;
        }

        protected virtual void AssignAI(GameObject numMonster) {
            /*assign target for AI movement, usually player
            AIFollow aIFollow = numMonster.AddComponent<AIFollow>();
            aIFollow.SetAIFollow(player);
            */
        }

        protected virtual void AssignMotion(GameObject numMonster, int waveNum) {
            speedLevel= CalculateSpeedLevel(waveNum);
            numMonster.GetComponent<NavMeshAgent>().speed = speed* speedLevel;
            numMonster.GetComponent<NavMeshAgent>().acceleration = accer* speedLevel;
            numMonster.GetComponent<NavMeshAgent>().angularSpeed = angleAccer * speedLevel;
        }

        protected virtual int CalculateSpeedLevel(int waveNum) {
            return 0;
        }


    }
}
