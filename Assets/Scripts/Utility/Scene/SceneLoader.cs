using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    // Start is called before the first frame update
    [SerializeField] private GameObject _LoadScenePrefab;
    GameObject loadScene;
    private float waitTime;
    private void Awake() {
        instance = this;
        if (loadScene != null) {
            Destroy(loadScene);
        }
        loadScene= Instantiate(_LoadScenePrefab, transform);
        loadScene.SetActive(false);
    }
    public virtual void LoadScene(int SceneIndex) {
        waitTime = 3f;
        StartCoroutine(LoadSceneCoroutine(SceneIndex));    
    }

    protected IEnumerator LoadSceneCoroutine(int SceneIndex) {
        loadScene.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Debug.Log(SceneManager.GetSceneByBuildIndex(SceneIndex));
        SceneManager.LoadScene(SceneIndex);

    }

}
