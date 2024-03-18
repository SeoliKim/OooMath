using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class HelpCardFunction : MonoBehaviour {

        protected HelpCardManager helpCardManager;
        protected UIMessageReceiver uIMessageReceiver;

        public int starPtCost;

        public virtual void SetHelpCardFunction(HelpCardManager helpCardManager) {
            this.helpCardManager = helpCardManager;
            this.uIMessageReceiver = helpCardManager.uIMessageReceiver;

        }

        public void TryUseCard() {
            bool enoughStarPt = uIMessageReceiver.SpendStarPt(starPtCost);
            if (enoughStarPt) {
                StartFunction();
                AudioManager.AudioManagerInstance.PlayAudio("success3note");
                helpCardManager.UseCard(this.gameObject);
            } else {
                AudioManager.AudioManagerInstance.PlayAudio("wrong");
                Debug.Log("Not enough StarPt");
            }
        }

        
        protected virtual void StartFunction() {}
        
    }
}
