using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private Renderer _OooBodyRenderer;

    private void Start() {
        Material onSkin = User.I.GetOnSkinMaterial();
        _OooBodyRenderer.material = onSkin;
    }
}
