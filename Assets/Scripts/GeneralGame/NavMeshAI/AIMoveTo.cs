using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class AIMoveTo : MonoBehaviour {
        
        private NavMeshAgent agent;
        private Rigidbody rb;
        public float radius;

        private void Awake() {
        gameObject.AddComponent<RandomMovePoint>();
        }

    private void Start() {
            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            this.rb.isKinematic = false;
        }

        private void Update() {
            if (!agent.hasPath) {
            
                agent.SetDestination(RandomMovePoint.Instance.GetRandomPoint(transform, radius));
            }
            if (!agent.isOnNavMesh) {
                agent.Warp(Vector3.zero);
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        
        /*
        private void OnCollisionEnter(Collision collision) {
            if (collision.collider!= null){
                    if (collision.collider.CompareTag("BallNum")) {
                    Debug.Log("collide with another ballnum");

                    }
            }
        }
        */


#endif
    }

