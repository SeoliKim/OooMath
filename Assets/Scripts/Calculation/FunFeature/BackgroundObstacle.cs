using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class BackgroundObstacle : MonoBehaviour {
        
        public List<GameObject> obstacles;
        public int countCubeObstacle;
        public int countNumberObstacle;

        private GameObject[] _CubeObstaclePrefabs;
        private GameObject[] _NumberObstaclePrefabs;

        private GameObject platform;

        public List<GameObject> SetBackgoundObstacle(GameObject[] _CubeObstaclePrefabs, GameObject[] _NumberObstaclePrefabs, int hardlevel) {
            obstacles = new List<GameObject>();
            countCubeObstacle = 5+1*hardlevel;
            countNumberObstacle = 4 + 1 * hardlevel;
            this._CubeObstaclePrefabs = _CubeObstaclePrefabs;
            this._NumberObstaclePrefabs = _NumberObstaclePrefabs;
            gameObject.GetComponent<GameSetUp>().GameObjectInitializeDone += LinkToPlatform;
            return obstacles;
        }

        private void LinkToPlatform(GameSetUp.GameArgs obj) {
            platform = obj.platform;
            CreateCubeObstacles();
            CreateNumberObstacles();
            gameObject.GetComponent<GameSetUp>().GameObjectInitializeDone -= LinkToPlatform;
        }

        private void CreateCubeObstacles() {
            for (int i = 0; i < countCubeObstacle; i++) {
                GameObject obstaclePrefab = GetCubeObstacle();
                Vector3 surfaceposition = PlatformSurface.GetRandomPointOnPlatform(platform);
                Vector3 position = new Vector3(surfaceposition.x, 1, surfaceposition.z);
                GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, transform) as GameObject;
                obstacle.transform.localScale = NumberGenerator.RandomScale(0.5f, 5f);
                obstacles.Add(obstacle);
            }
        }

        private GameObject GetCubeObstacle() {
            int randomLabel = NumberGenerator.getRandomNumber(0, 8);
            GameObject obstaclePrefab = _CubeObstaclePrefabs[randomLabel];
            return obstaclePrefab;
        }

        private void CreateNumberObstacles() {
            for (int i = 0; i < countNumberObstacle; i++) {
                GameObject obstaclePrefab = GetNumberObstacle();
                Vector3 surfaceposition = PlatformSurface.GetRandomPointOnPlatform(platform);
                Vector3 position = new Vector3(surfaceposition.x, 1, surfaceposition.z);
                GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, transform) as GameObject;
                obstacle.transform.localScale = NumberGenerator.RandomScale(0.5f, 5f);
                obstacles.Add(obstacle);
            }
        }

        private GameObject GetNumberObstacle() {
            int randomLabel = NumberGenerator.getRandomNumber(0, 9);
            GameObject obstaclePrefab = _NumberObstaclePrefabs[randomLabel];
            return obstaclePrefab;
        }


    }
}
