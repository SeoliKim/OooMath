using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField]
    protected Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null) {
            this.transform.position = _target.position;
        } else {
            Debug.Log("Cannot fint Target");
        }
    }
}
