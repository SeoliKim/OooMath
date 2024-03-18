using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Calculation {
    public class UIEventSender : MonoBehaviour {

        public event Action ClawButtonDown;
        public event Action ClawButtonRelease;

        public void ClawButtonPressDown() {
            ClawButtonDown?.Invoke();

        }

        public void ClawButtonReleaseUp() {
            ClawButtonRelease?.Invoke();
        }
    }
}

