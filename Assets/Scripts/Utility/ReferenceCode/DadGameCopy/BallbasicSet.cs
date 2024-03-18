using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallbasicSet : MonoBehaviour
{   
    /*
    //BalanceGame Script [BaseEntity]&[BaseBall]
    protected Vector3 _initPos;
    public Vector3 InitPos {
        get {
            return _initPos;
        }
        set {
            _initPos = value;
        }
    }

    protected Vector3 _initScale;
    public Vector3 InitScale => _initScale;

    protected Quaternion _initRot;
    protected Rigidbody _rig;

    public Rigidbody Rigidbody => _rig;

    public float radius = 0.03f;

    private MeshRenderer _meshRender;
    private Material[] _materials;

    public Ballbasicmove Ballbasicmove { get; private set; }

    private bool _isStand = false;
    public bool IsStand => _isStand;

    protected virtual void Start() {
        _rig = gameObject.GetComponentInChildren<Rigidbody>();

        _initPos = transform.localPosition;
        _initRot = transform.localRotation;
        _initScale = transform.localScale;

        Ballbasicmove = GetComponent<Ballbasicmove>();

        Init();
    }

    public void Init() {
        _meshRender = GetComponentInChildren<MeshRenderer>();
        if (_meshRender)
            _materials = _meshRender.materials;
    }

    public void SetStand(bool stand, bool stop = true) {
        if (IsStand == stand)
            return;

        _isStand = stand;
        if (_isStand && stop) {
            Stop();
        }
    }

    public virtual void Stop() {
        Pause();

        if (_rig) {
            _rig.velocity = Vector3.zero;
            _rig.angularVelocity = Vector3.zero;
        }
    }

    public virtual void Pause() {
        _rig?.Sleep();
    }

    public virtual void Resume() {
        _rig?.WakeUp();
    }

    //BalanceGame Script [Ball]
    public Vector3 Acceleration { get; set; }

    public System.Action<GameObject> OnHit;

    private void FixedUpdate() {
        if (IsStand) {
            return;
        }

        Ballbasicmove.Move(Acceleration);
    }

    // private Vector3 m_preVelocity = Vector3.zero;
    protected virtual void OnCollisionEnter(Collision other) {

        OnHit?.Invoke(other.gameObject);
    }
    */
}
