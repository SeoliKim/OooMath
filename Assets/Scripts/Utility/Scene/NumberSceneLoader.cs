using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Number {
    public class NumberSceneLoader : SceneLoader {
        public override void LoadScene(int SceneIndex) {
            GameManager.GameManagerInstance.Nullify();
            StartCoroutine(LoadSceneCoroutine(SceneIndex));
        }
    }
}
