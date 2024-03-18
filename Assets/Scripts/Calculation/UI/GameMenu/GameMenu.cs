
using UnityEngine;
using TMPro;
using System;

namespace Calculation {
    public class GameMenu : MonoBehaviour {
        public string _GameLevelName;
        [SerializeField]
        private TMP_Text _textEquetion, _textQNum;

        private Math_10digitAddLevel math_10DigitAddLevel;
        private UIMessageReceiver UIMessageReceiver;


        private void Awake() {
            UIMessageReceiver = transform.parent.gameObject.GetComponent<UIMessageReceiver>();
        }

        private void OnEnable() {
            GetMessageValue();
            SetMathProblem();
        }

        private void GetMessageValue() {
            _textQNum.text = UIMessageReceiver.roundCount.ToString();

        }

        private void SetMathProblem() {
            _textEquetion.text = UIMessageReceiver.equation;

            Debug.Log("game menu update math Problem");
        }

    }
}
