using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Number {
    public class ThrowJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

        //GUI
        [SerializeField] protected RectTransform background = null;
        [SerializeField] private RectTransform handle = null;
        [SerializeField] private Vector2 center;
        private RectTransform baseRect = null;
        private Canvas canvas;
        private Camera cam;
        private GameObject player;
        private Thrower thrower;

        public GameObject throwObject;

        //User Input
        [SerializeField] private float handleRange = 1;
        public float HandleRange {
            get { return handleRange; }
            set { handleRange = Mathf.Abs(value); }
        }

        [SerializeField] private float deadZone = 0;
        public float DeadZone {
            get { return deadZone; }
            set { deadZone = Mathf.Abs(value); }
        }

        private Vector2 input;
        private Vector2 radius;
        public event Action<JoyStickInputArgs> JoyStickRelease;
        public class JoyStickInputArgs : EventArgs{
            public GameObject throwObject;
            public float magnitude;
            public Vector2 direction;
        }


        protected virtual void OnEnable() {
            HandleRange = handleRange;
            DeadZone = deadZone;
            baseRect = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            player = gameObject.GetComponentInParent<UIMessageReceiver>().player;
            thrower = player.GetComponentInChildren<Thrower>();
            thrower.SetThrowJoyStick(this);
            if (canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");
            center = Vector2.zero;
            radius = new Vector2(360, 360) / 2;
        }



        public virtual void OnPointerDown(PointerEventData eventData) {
            player.transform.rotation = Quaternion.identity;
            thrower.SetThrowMode(true);
            OnDrag(eventData);
        }

        protected virtual Vector2 HandleInput(Vector2 input, float magnitude, Vector2 normalised, Camera cam) {
            if (magnitude > deadZone) {
                if (magnitude > 1)
                    input = normalised;
            } else
                input = Vector2.zero;
            return input;
        }

        public void OnDrag(PointerEventData eventData) {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
            input = (eventData.position - position) / (radius * canvas.scaleFactor);
            input= HandleInput(input, input.magnitude, input.normalized, cam);
            handle.anchoredPosition = input * radius * handleRange;

            //update throwline current addForce
            Vector2 finalPosition = handle.anchoredPosition;
            Vector2 direction = finalPosition - center;
            float magnitude = new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y)).magnitude / 180;
            direction = HandleInput(direction, direction.magnitude, direction.normalized, cam);
            thrower.DrawThrowLine(throwObject, direction, magnitude);
        }

       

        public virtual void OnPointerUp(PointerEventData eventData) {
            cam = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = canvas.worldCamera;

            Vector2 finalPosition = handle.anchoredPosition;
            Vector2 result= finalPosition - center;
            float magnitude = new Vector2(Mathf.Abs(result.x), Mathf.Abs(result.y)).magnitude / 180;
            result= HandleInput(result, result.magnitude, result.normalized, cam);
            JoyStickRelease?.Invoke(new JoyStickInputArgs { throwObject = throwObject, direction = result, magnitude = magnitude });
            Debug.Log("ShooterRelease Event: " + throwObject.name + result + magnitude);
            thrower.SetThrowMode(false);
            handle.gameObject.SetActive(false);
            input = Vector2.zero;
            handle.anchoredPosition = center;
        }

        



    }
}
