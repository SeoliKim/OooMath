using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRotateFollow : FollowPosition {
    private void Start() {
        _target = this.transform.parent.GetChild(1).transform;
    }
}
