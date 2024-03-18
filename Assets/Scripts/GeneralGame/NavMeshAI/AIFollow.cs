using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Rigidbody rb;
    private GameObject player;

    public float radius;

    public void SetAIFollow(GameObject p) {
        agent = GetComponent<NavMeshAgent>();
        player = p;
        rb = GetComponent<Rigidbody>();
        this.rb.isKinematic = false;
    }

    private void Update() {
        if (!agent.hasPath) {
            agent.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject != null) {
            this.rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        this.rb.isKinematic = true;
    }
}
