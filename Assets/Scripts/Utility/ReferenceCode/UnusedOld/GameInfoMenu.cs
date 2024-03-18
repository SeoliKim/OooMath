using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Calculation {
    public class GameInfoMenu : MonoBehaviour {
        [SerializeField] private TMP_Text _MoneyCount;
        public int moneyCount;


        public void AddMoney() {
            moneyCount++;
            _MoneyCount.text = moneyCount.ToString();
        }
    }
}
