using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Number {
    public class Thrower : MonoBehaviour {
        public float throwCoolDown;
        public float forceRate;
        private bool onThrowMode;
        public void SetThrowMode(bool b) {
            onThrowMode = b;
        }

        [SerializeField] private float range = 10;

        private GameObject rotator;
        public void SetCM(GameObject cm) {
            this.rotator = cm;
        }

        private GameObject bubbleHolder;
        public void SetBubbleHolder(GameObject bubbleHolder) {
            this.bubbleHolder = bubbleHolder;
        }

        private DrawTrajectory drawTrajectory;
        public void SetDrawTrajectory(DrawTrajectory drawTrajectory) {
            this.drawTrajectory = drawTrajectory;
            drawTrajectory.HideLine();
        }
        

        private void Awake() {
            onThrowMode = false;
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        }

        private void Start() {
            onThrowMode = false;
        }


        private void LateUpdate() {
            if (onThrowMode) {
                transform.localEulerAngles = new Vector3(0, rotator.transform.localEulerAngles.y, 0);

            }
        }

       private void GameManager_OnGameStateChanged(GameState state) {
            if(state == GameState.Fail) {
                bubbleButtonManager.ShootButtonRelease -= ShootButtonRelease;
            }
        }
        private void OnDestroy() {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        }

        #region PowerBubbleShooter

        public GameObject shootBubble;
        private BubbleButtonManager bubbleButtonManager;
        public float shootrate;
        public void SetBubbleButtonManager(BubbleButtonManager bubbleButtonManager) {
            this.bubbleButtonManager = bubbleButtonManager;
            bubbleButtonManager.ShootButtonRelease += ShootButtonRelease;
        }

        private GameObject GetShootBubble() {
            for (int i = 0; i < bubbleHolder.transform.childCount; i++) {
                GameObject bubble = bubbleHolder.transform.GetChild(i).gameObject;
                if (!bubble.activeSelf) {
                    return bubble;
                }
            }
            Debug.LogError("No active power bubble to use");
            return null;
        }

        private void ShootButtonRelease(Color32 buttonColor) {
            if (shootBubble == null) {
                shootBubble = GetShootBubble();
            }
            shootBubble.GetComponent<PowerBubbleVariable>().SetColor(buttonColor);
            ShootBubble(shootBubble);
            shootBubble = null;
        }

        public GameObject ShootBubble(GameObject shootBubble) {
            Vector3 instantiatePos = new Vector3(x: transform.position.x + transform.forward.x, .5f, z: transform.position.z + transform.forward.z);
            Rigidbody rigidbody = shootBubble.GetComponent<Rigidbody>();
            shootBubble.transform.position = instantiatePos;
            shootBubble.transform.rotation = Quaternion.identity;
            shootBubble.SetActive(true);
            AudioManager.AudioManagerInstance.PlayAudio("bubbleshoot");
            Vector3 addForce = new Vector3(rotator.transform.forward.x, 0, rotator.transform.forward.z) * shootrate;
            Debug.Log("ShootBubble" + addForce);
            rigidbody.velocity = addForce;
            SetThrowMode(false);
            return shootBubble;
        }

        #endregion


        #region straightLineShoot
        public void DrawLine() {
            LayerMask ignore = LayerMask.GetMask("Power");
            drawTrajectory.DrawShootLine(Vector3.zero, Vector3.forward, 100f);
        }

        #endregion


        #region JoyStick Throw 

        public GameObject throwItem;
        private ThrowJoyStick throwJoyStick;
        public void SetThrowJoyStick(ThrowJoyStick throwJoyStick) {
            this.throwJoyStick = throwJoyStick;
            throwJoyStick.JoyStickRelease += JoyStickRelease;
        }

        public void SetThrowItem(GameObject gameObject) {
            throwItem = gameObject;
        }
        public void DrawThrowLine(GameObject throwItem, Vector2 direction, float magnitude) {
            Vector3 addForce = CalculateAddForce(direction, magnitude);
            Rigidbody rigidbody = throwItem.GetComponent<Rigidbody>();
            drawTrajectory.UpdateTrajectory(addForce, rigidbody, Vector3.zero);
        }

        private void JoyStickRelease(ThrowJoyStick.JoyStickInputArgs args) {
            if(this.throwItem == null) {
                this.throwItem = args.throwObject;
            }
            Debug.Log(args.throwObject.name+ args.direction.ToString() + args.magnitude);

            throwItem.transform.position = Vector3.zero;
            throwItem.SetActive(true);
            ThrowObject(args.direction, args.magnitude);
        }

        public void ThrowObject(Vector2 direction, float magnitude) {
            Vector3 addForce = CalculateAddForce(direction, magnitude);
            Rigidbody rigidbody = throwItem.GetComponent<Rigidbody>();
            rigidbody.AddForce(addForce, ForceMode.Impulse);
            throwItem = null;
        }

        public Vector3 CalculateAddForce(Vector2 direction, float magnitude) {
            Vector2 targetDir = direction * (-1);
            Vector3 addForce = new Vector3(targetDir.x, magnitude, targetDir.y) * forceRate;
            return addForce;
        }
        #endregion



    }
}
