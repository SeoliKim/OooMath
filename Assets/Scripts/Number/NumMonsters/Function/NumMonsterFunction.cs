using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace Number {
    public class NumMonsterFunction : MonoBehaviour {
        //Basic
        protected string numberLabel;
        [SerializeField] protected Renderer[] renderers;
        [SerializeField]
        protected TMP_Text _front, _back, _up;
        public Color32 color;

        //Motion
        public float attackBloodStrength;

        protected NumberType_Level numberType_Level;
        protected NumMonsterManager numMonsterManager;
        protected NumMonsterGenerator numMonsterGenerator;

        protected virtual void Awake() {
            attackBloodStrength = 0;
            color = renderers[0].material.GetColor("_Color");
        }

        protected virtual void Start() {
            string numberLabel = numberType_Level.GenerateNumberLabel();
            SetNumberLabel(numberLabel);
        }

        public virtual void LinkToGameBasic(NumberType_Level numberType_Level, NumMonsterManager numMonsterManager, NumMonsterGenerator numMonsterGenerator) {
            this.numberType_Level = numberType_Level;
            this.numMonsterManager = numMonsterManager;
            this.numMonsterGenerator = numMonsterGenerator;
        }

        #region Basic

        public virtual void AssignColor(Color32 color) {
            foreach (Renderer renderer in renderers) {
                renderer.GetComponent<Renderer>().material.SetColor("_Color", color);
            }
        }

        public virtual string GetNumberLabel() {
            return numberLabel;
        }

        public virtual void SetNumberLabel(string numberLabel) {
            this.numberLabel = numberLabel;
            _front.text = numberLabel;
            _back.text = numberLabel;
            _up.text = numberLabel;
        }
        #endregion

        #region Motion

        protected virtual void OnCollisionEnter(Collision collision) {
            if (collision.collider.gameObject.CompareTag("PowerBubble")) {
                GameObject hitBubble = collision.collider.gameObject;
                HitResponse(hitBubble);
            }
        }

        protected virtual void HitResponse(GameObject hitBubble) {
            bool correct = numberType_Level.CheckHit(this.gameObject, hitBubble);
            if (correct) {
                numMonsterManager.HitByCorrectBubble(this.gameObject);
            } else {
                numMonsterManager.HitByWrongBubble(this.gameObject);
            }
            
        }

        public virtual float GetAttackBloodStrength() {
            return attackBloodStrength;
        }
        #endregion


    }
}
