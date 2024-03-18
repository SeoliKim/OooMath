using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculation {
    public class PlatformSurface : MonoBehaviour {
        public static Vector3 GetRandomPointOnPlatform(GameObject platform) {
            Renderer r = platform.GetComponentInChildren<Renderer>();
            var bounds = r.bounds;
            var center = bounds.center;
            float x = Random.Range((center.x - 60), (center.x + 60));
            float z = Random.Range((center.z - 40), (center.z + 40));
            Vector3 position = new Vector3(x, 0, z);
            return position;
        }

        public static Vector3 GetRandomPointOnPlatformAwayFromOrigin(GameObject platform) {
            Renderer r = platform.GetComponentInChildren<Renderer>();
            var bounds = r.bounds;
            var center = bounds.center;
            float x = Random.Range((center.x - 60), (center.x + 60));
            float z = Random.Range((center.z - 40), (center.z + 40));
            Vector3 position = new Vector3(x, 0, z);
            Bounds playerRegion = new Bounds(Vector3.zero, new Vector3(10, 20, 5));
            while (playerRegion.Contains(position)) {
                x = Random.Range((center.x - 60), (center.x + 60));
                z = Random.Range((center.z - 40), (center.z + 40));
                position = new Vector3(x, 0, z);
            }

            return position;
        }


    }
}
