using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class PowerBubbleVariable : MonoBehaviour {
        [SerializeField] private Renderer meshRenderer;
        public Color32 color;

        private Rigidbody rb;

        private void Awake() {
            rb = GetComponentInChildren<Rigidbody>();
            color = meshRenderer.material.GetColor("_Color");
        }
        private void Update() {
            if (Vector3.Distance(transform.position, Vector3.zero) > 80) {
                OutOfBoundary();
            }
        }
        public Color32 GetColor() {
            return meshRenderer.material.GetColor("_Color");
        }

        public void SetColor(Color32 color) {
            meshRenderer.material.SetColor("_Color", color);
        }

        private void OnCollisionEnter(Collision collision) {
            if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Stage")) {
                gameObject.SetActive(false);
            }
            
        }

        private void OutOfBoundary() {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }

    }
}
