using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Number {
    public class GameInfoMenu : MonoBehaviour {
        private UIMessageReceiver uIMessageReceiver;
        [SerializeField] private TMP_Text _MoneyCount, _killCount;
        public int moneyCount;
        public int killCount;

        public void AddMoney() {
            moneyCount++;
            _MoneyCount.text = moneyCount.ToString();
        }

        public void AddKill() {
            killCount++;
            _killCount.text = killCount.ToString();
        }
    }
}
