using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullSetBombBahavior : MonoBehaviour
{
    [SerializeField] private Animator NullSetBombAnimator;

    private Material nullSetMaterial;
    private GameObject player;

    public float power;
    public float radius;
    public float upForce;

    private void Awake() {
        power = 10f;
        radius = 7f;
        upForce = 3f;
       
    }
   
    private void OnTriggerEnter(Collider collider) {
        //Debug.Log(gameObject.name + collider.gameObject.name);
        if (collider.gameObject.CompareTag("Player")) {

            player = collider.gameObject;
            StartCoroutine(BombReadyToExplode());
        }
    }

    IEnumerator BombReadyToExplode() {
        NullSetBombAnimator.Play("NullSetBombExplode");
        
        yield return new WaitForSeconds(1f) ;
        //BombExplode
        Vector3 explodePosition = gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explodePosition, radius);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            
            if (rb != null) {
                Debug.Log("nullset bomb" + rb.gameObject);
                rb.AddExplosionForce(power*100, explodePosition, radius, upForce, ForceMode.VelocityChange);
                
            }
        }

        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }

    

}
