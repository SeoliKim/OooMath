using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [Range(3,50)]
    [SerializeField] private int _lineSegmentCount;

    private List<Vector3> linePoints = new List<Vector3>();

    
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint) {
        Vector3 velocity = (forceVector / rigidbody.mass)* Time.fixedDeltaTime;
        float flightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stopTime = flightDuration / _lineSegmentCount;
        linePoints.Clear();

        for(int i = 0; i < _lineSegmentCount; i++) {
            float stopTimePassed = stopTime * i;

            Vector3 movementVector = new Vector3(
                x: velocity.x * stopTimePassed,
                y: velocity.y * stopTimePassed - 0.5f * Physics.gravity.y * stopTimePassed * stopTimePassed,
                z: velocity.z * stopTimePassed
                );
            linePoints.Add(item: -movementVector + startingPoint);
        }

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
        
    }

    public void DrawShootLine(Vector3 targetPosition, Vector3 direction, float length) {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length)) {
            endPosition = raycastHit.point;
        }

        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    public void DrawShootLine(Vector3 targetPosition, Vector3 direction, float length, LayerMask ignore) {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length, ~ignore)) {
            endPosition = raycastHit.point;
        }

        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    public void HideLine() {
        lineRenderer.positionCount = 0;
    }
}
