using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSurface : MonoBehaviour
{
    public static Vector3 GetPositionOnSurface(RaycastHit hitInfo, Collider objectToPlaceCollider, GameObject objectToPlace, float distanceFromSurface) {

        // Distance between the center of the object you want to place and the point its surface
        float distFromCenter = Vector3.Distance(objectToPlaceCollider.ClosestPoint(hitInfo.point), objectToPlace.transform.position);

        // Total Distance between the center of the object and the surface of the object you want to place your object on
        float totalDistance = distanceFromSurface + distFromCenter;
        return hitInfo.point + hitInfo.normal * totalDistance;
    }
}
