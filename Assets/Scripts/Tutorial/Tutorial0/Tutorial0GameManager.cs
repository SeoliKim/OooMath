using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial0GameManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private GameObject _PlayerPrefeb;
    [SerializeField] private GameObject _CMPrefab, _MoneyTutorial0Prefab;
    private GameObject player, cm;
    private PlayerTutorial0 playerTutorial0;

    [Header("UI")]
    [SerializeField] private GameObject _MainCanva;
    [SerializeField] private TMP_Text _OooMessgaeIDname;
    private Tutorial0CanvaManager tutorial0CanvaManager;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpGame();
        tutorial0CanvaManager = _MainCanva.GetComponent<Tutorial0CanvaManager>();
    }

    private void SetUpGame() {
        //initialize Camera
        cm = Instantiate(_CMPrefab, transform);
        CM_FollowPlayer cM_FollowPlayer = cm.AddComponent<CM_FollowPlayer>();
        cm.name = _CMPrefab.name;
        Debug.Log("set up" + cm);

        //initialize player
        player = Instantiate(_PlayerPrefeb, new Vector3(0, 2, 0), Quaternion.identity, transform);
        player.name = _PlayerPrefeb.name;

        cM_FollowPlayer.LinkToPlayer(player);

        //set player motion
        Rigidbody rb = player.AddComponent<Rigidbody>();
        rb.mass = 5;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        Player3DMover player3DMover = player.AddComponent<Player3DMover>();
        player3DMover._Rotator = cm.transform;
        player3DMover.accelRate = 8f;
        player3DMover.vMax = 4f;
        player3DMover.constantRate = 0.5f;
        playerTutorial0= player.AddComponent<PlayerTutorial0>();
    }

    private void Start() {
            _OooMessgaeIDname.text = User.I.displayName.ToUpper();    
        StartCoroutine(Step0());
    }
    private IEnumerator Step0() {
        AudioManager.AudioManagerInstance.PlayAudio("showOoo");
        Time.timeScale = 0;
        yield return StartCoroutine(tutorial0CanvaManager.Step0());
    }

    public void StartStep1() {
        StartCoroutine(Step1());
    }

    private IEnumerator Step1() {
        Time.timeScale = 1;
        AudioManager.AudioManagerInstance.PlayMusic("theme");
        AudioManager.AudioManagerInstance.PlayAudio("appear");
        yield return StartCoroutine(tutorial0CanvaManager.Step1());
        StartCoroutine(Step2());
    }

    private IEnumerator Step2() {
        StartCoroutine(tutorial0CanvaManager.Step2());
        AudioManager.AudioManagerInstance.PlayAudio("appear");
        yield return new WaitUntil(()=> Vector3.Distance(player.transform.position, Vector3.zero) >= 26);
        StartCoroutine(Step3());
    }

    private IEnumerator Step3() {
        StartCoroutine(tutorial0CanvaManager.Step3());
        AudioManager.AudioManagerInstance.PlayAudio("appear");
        playerTutorial0.StartPauseMotion();
        yield return new WaitForSecondsRealtime(4f);
        StartCoroutine(Step4());
    }

    private IEnumerator Step4() {
        yield return (StartCoroutine(tutorial0CanvaManager.Step4()));
        AudioManager.AudioManagerInstance.PlayAudio("appear");
        playerTutorial0.StopPauseMotion();
        yield return new WaitForSecondsRealtime(7f);
        StartCoroutine(Step5());
    }

    private IEnumerator Step5() {
        StartCoroutine(tutorial0CanvaManager.Step5());
        AudioManager.AudioManagerInstance.PlayAudio("appear");
        yield return new WaitForSecondsRealtime(7f);
        StartCoroutine(Step6());
    }

    private IEnumerator Step6() {
        StartCoroutine(tutorial0CanvaManager.Step6());
        AudioManager.AudioManagerInstance.PlayAudio("appear");
        yield return new WaitForSecondsRealtime(7f);
        StartCoroutine(Step7());
    }

    private IEnumerator Step7() {
        playerTutorial0.StopPauseMotion();
        AudioManager.AudioManagerInstance.PlayAudio("success3note");
        Time.timeScale = 0;
        StartCoroutine(tutorial0CanvaManager.Step7());
        yield return null;
    }

    //Step 8 Collect Money
    private int chipsCount = 0; 
    public void StartStep8() {
        Tutorial0MoneyController tutorial0MoneyController = gameObject.AddComponent<Tutorial0MoneyController>();
        tutorial0MoneyController.SetTutorial0MoneyController(_MoneyTutorial0Prefab);
        Time.timeScale = 1;
        StartCoroutine(Step8());
    }

    private IEnumerator Step8() {
        StartCoroutine(tutorial0CanvaManager.Step8());
        yield return new WaitUntil(()=> chipsCount >= 3);
        StartCoroutine(Step9());
    }

   public void ChipsAddOne() {
        chipsCount++;
    }

    private IEnumerator Step9() {
        Time.timeScale = 0;
        User.I.FinishTutorial(0);
        User.I.FinishGoal(0);
        User.I.AddExperience(30);
        PlayFabManager.PlayFabManagerInstance.AddCP(3);
        AudioManager.AudioManagerInstance.PlayAudio("Wowvoice");
        StartCoroutine(tutorial0CanvaManager.Step9());
        yield return null;
    }

    public void StartStep10() {
        Time.timeScale = 1;
        User.I.StartTutorial(1);
        SceneManager.LoadScene(1);
    }

}
