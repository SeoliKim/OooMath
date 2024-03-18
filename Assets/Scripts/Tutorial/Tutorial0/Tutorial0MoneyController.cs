using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial0MoneyController : MonoBehaviour
{
    [SerializeField] private GameObject _MoneyTutorialPrefab;
    public List<GameObject> chips = new List<GameObject>();

    private float timeInterval;
    private float timer;
    private bool timerOn;

    public void SetTutorial0MoneyController(GameObject _MoneyPrefab) {
        this._MoneyTutorialPrefab = _MoneyPrefab;
        timeInterval = 10f;
        timer = timeInterval;
        timerOn = true;
        for (int i = 0; i < 5; i++) {
            AddMoney();
        }
    }

    private void Update() {
        if (timerOn) {
            timeInterval -= Time.deltaTime;
        }
        if (timeInterval < 0) {
            Debug.Log("times up! Add money");
            AddMoney();
            timeInterval = timer;
        }
    }

    private void AddMoney() {
        GameObject chip = Instantiate(_MoneyTutorialPrefab, GetRandomPos(), Quaternion.identity, transform);
        chips.Add(chip);
    }

    private Vector3 GetRandomPos() {
        int randomX = Random.Range(-10, 10);
        int randomZ = Random.Range(-25, 25);
        Vector3 randomPos = new Vector3(randomX, 0.5f, randomZ);
        return randomPos;
    }

    
}
