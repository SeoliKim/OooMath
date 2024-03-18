using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour {

    [Header("Renderer")]
    [SerializeField] protected Renderer _SkinExpSphereRenderer;

    [Header("UI")]
    [SerializeField] protected GameObject _OwnUi;
    [SerializeField] protected GameObject _NotOwnUi;

    [Space]
    public int collection, index;
    public bool own;
    protected SkinManager skinManager;
    public Material skin;
    

    public void Initialize() {
        skin = _SkinExpSphereRenderer.material;
        index = GetIndex();
        collection = SetCollection();
        skinManager = GetComponentInParent<SkinManager>();
        skinManager.skinList[collection, index] = skin;
        skinManager.skinObjects[collection, index] = this;
        SetSkinUI();
    }

    protected int GetIndex() {
        int onPage = transform.parent.GetSiblingIndex();
        int index = onPage * 8 + transform.GetSiblingIndex();
        return index;
    }
    protected virtual void SetSkinUI() {

    }

    protected virtual int SetCollection() {
        return -1;
    }

    public void UpdateSkin(bool own) {
        this.own = own;
        if (own) {
            _OwnUi.SetActive(true);
            _NotOwnUi.SetActive(false);
        } else {
            _OwnUi.SetActive(false);
            _NotOwnUi.SetActive(true);
        }
    }
}
