using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SkinManager : MonoBehaviour
{
    public Material[,] skinList = new Material[3, 100];
    public Skin[,] skinObjects = new Skin[3, 100];

    [SerializeField] private GameObject _SkinPanel;
    [SerializeField] private Renderer _OooDefaultPageBody;
    private void Awake() {
        StartCoroutine(InitializeSkinMenu());
    }
    
    private IEnumerator InitializeSkinMenu() {
        InitilizeSkinOnPage(_BasicCollect);
        InitilizeSkinOnPage(_RoyalCollect);
        InitilizeSkinOnPage(_AwardCollect);
        OpenCollection(_RoyalCollect);
        _SkinPanel.SetActive(false);
        _SkinPanel.transform.parent.gameObject.SetActive(false); //Shop Page Set to false
        yield return null;
    }

    private void InitilizeSkinOnPage(GameObject collection) {
        foreach (Transform page in collection.transform) {
            foreach (Transform skinChild in page) {
                Skin skin = skinChild.gameObject.GetComponent<Skin>();
                skin.Initialize();
            }
        }
        
    }

    void Start() {
        //prepare user data
        User.I.SetOnSkinMaterial(skinList);
        if(User.I.GetOnSkinMaterial()==null){
            Debug.LogError("User GetOnSkinMaterial()==null");
            return;
        }
        //update MainMenuUI
        _OooDefaultPageBody.material = User.I.GetOnSkinMaterial();

        //update SkinStoreUI
        UpdateSkinPanel();

        OpenCollection(_RoyalCollect);
        User.I.OwnNewSkin += OwnNewSkinUpdate;
    }

    private void OwnNewSkinUpdate() {
        UpdateSkinPanel();
    }

    private void OnDisable() {
        VCurrencyManager.instance.Yes_ConfirmResponse -= Yes_ConfirmResponse;
    }

    #region Skin
    public void CollectNewSkin(Skin newSkin) {
        //save user data
        User.I.OwnSkin(newSkin.collection, newSkin.index);

        //update SkinStore
        newSkin.UpdateSkin(true);
    }

    public bool TryChangeOnSkin(Skin skin) {
        int skinCollection = skin.collection;
        int index = skin.index;
        //Check if owned
        if (!User.I.IfSkinOwn(skinCollection, index)) {
            Debug.Log("skin not owned!");
            return false;
        }
        StartCoroutine(ChangeOnSkin(skinCollection, index));
        return true;
    }

    private IEnumerator ChangeOnSkin(int skinCollection, int index) {
        //change user save
        yield return StartCoroutine(User.I.SaveSkinUserData(skinCollection, index,skinList));

        //update game page
        _OooDefaultPageBody.material = User.I.GetOnSkinMaterial();
    }

    #endregion

    #region SkinStoreUI

    private Skin modelSkin;

    [Space]
    [SerializeField] private Renderer _OooSkinModelBody;
    [SerializeField] private GameObject _SkinLockTextPanel;
    [SerializeField] private TMP_Text _TextPaymentBox, _TextLockTextBox;
    [Space]
    [Header("Skin Collection Grid")]
    [SerializeField] private GameObject _RoyalCollect;
    [SerializeField] private GameObject _BasicCollect, _AwardCollect;
    [Space]
    [Header("Model Button")]
    [SerializeField] private GameObject _LockButton;
    [SerializeField] private GameObject _BuyButton, _WearButton;

    private void UpdateSkinPanel() {
        for (int i = 0; i < skinObjects.GetLength(0); i++) {
            for (int j = 0; j < skinObjects.GetLength(1); j++) {
                Skin skin = skinObjects[i, j];
                if (skin != null) {
                    skin.UpdateSkin(User.I.IfSkinOwn(i, j));
                } else {
                    //Debug.Log("skin is null at collection " + i + " index " + j);
                }
            }
        }
        _OooSkinModelBody.material = User.I.GetOnSkinMaterial();

    }

    //Step 1- Choose Skin Collection
    public void OpenCollection(GameObject Collection) {
        DefaultSkinShop();
        Collection.SetActive(true);
        Collection.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void DefaultSkinShop() {
        UtilityUI.SetChildFalse(_RoyalCollect);
        UtilityUI.SetChildFalse(_BasicCollect);
        UtilityUI.SetChildFalse(_AwardCollect);
        _RoyalCollect.SetActive(false);
        _BasicCollect.SetActive(false);
        _AwardCollect.SetActive(false);
    }

    public void Click_Up_Page() {
        GameObject acticeSelection = GetOnActiceSelection();
        int activePage=0;
        foreach(Transform child in acticeSelection.transform) {
            if (child.gameObject.activeSelf) {
                activePage = child.GetSiblingIndex();
            }
            child.gameObject.SetActive(false);
        }
        int upPage = activePage - 1;
        if(upPage < 0) {
            upPage = acticeSelection.transform.childCount-1;
        }
        acticeSelection.transform.GetChild(upPage).gameObject.SetActive(true);
    }

    public void Click_Down_Page() {
        GameObject acticeSelection = GetOnActiceSelection();
        int activePage = 0;
        foreach (Transform child in acticeSelection.transform) {
            if (child.gameObject.activeSelf) {
                activePage = child.GetSiblingIndex();
            }
            child.gameObject.SetActive(false);
        }
        int downPage = activePage +1;
        if (downPage >= acticeSelection.transform.childCount) {
            downPage = 0;
        }
        acticeSelection.transform.GetChild(downPage).gameObject.SetActive(true);
    }

    private GameObject GetOnActiceSelection() {
        if (_AwardCollect.activeSelf) {
            return _AwardCollect;
        }else if (_BasicCollect.activeSelf) {
            return _BasicCollect;
        }
        return _RoyalCollect;
    }

    //Step 2- Try Skin On Model

    public void TryAwardSkinOnModel(SkinAward skin) {
        modelSkin = skin;
        _OooSkinModelBody.material = skin.skin;
        if(User.I.IfSkinOwn(skin.collection, skin.index)) {
            ShowModelButton(_WearButton);
        } else {
            ShowModelButton(_LockButton);
        }

    }

    public void TryBasicSkinOnModel(SkinBasic skin) {
        modelSkin = skin;
        _OooSkinModelBody.material = skin.skin;
        if (User.I.IfSkinOwn(skin.collection, skin.index)) {
            ShowModelButton(_WearButton);
        } else {
            ShowModelButton(_BuyButton);
        }
    }

    public void TryRoyalSkinOnModel(SkinRoyal skin) {
        modelSkin = skin;
        _OooSkinModelBody.material = skin.skin;
        if (User.I.IfSkinOwn(skin.collection, skin.index)) {
            ShowModelButton(_WearButton);
        } else {
            ShowModelButton(_BuyButton);
        }
    }

    private void ShowModelButton(GameObject showButton) {
        _LockButton.SetActive(false);
        _BuyButton.SetActive(false);
        _WearButton.SetActive(false);
        showButton.SetActive(true);
    }

    //Step 3- Model Button
    public void Click_Wear_ModelBotton() {
        bool success = TryChangeOnSkin(modelSkin);
        if (!success) {
            Debug.LogError("Wear button showed but skin is not owned, skin collection: " + modelSkin.collection + "index: " + modelSkin.index);
        } else {
            Debug.Log("Now wear skin of skin collection: " + modelSkin.collection + "index: " + modelSkin.index);
        }
    }

    public void Click_Buy_ModelButton() {
        VCurrencyManager.instance.Yes_ConfirmResponse += Yes_ConfirmResponse;

        var modelSkinRoyal = modelSkin as SkinRoyal;
        if(modelSkinRoyal!= null) {
            VCurrencyManager.instance.Open_PaymentConfirmPanel(modelSkinRoyal.GetDMcostString());
            return;
        }
        var modelSkinBasic = modelSkin as SkinBasic;
        if (modelSkinBasic != null) {
            VCurrencyManager.instance.Open_PaymentConfirmPanel(modelSkinBasic.GetCPcostString());
            return;
        }
        Debug.LogError("model skin is not skinRoyal or skinBasic, skin collection: " + modelSkin.collection + "index: " + modelSkin.index);
    }

    private void Yes_ConfirmResponse() {
        PlayFabManager.PlayFabManagerInstance.SpendResult += SpendResult;
        var modelSkinRoyal = modelSkin as SkinRoyal;
        if (modelSkinRoyal != null) {
            PlayFabManager.PlayFabManagerInstance.SpendDM(modelSkinRoyal.GetDMcost());
            _TextPaymentBox.gameObject.SetActive(false);
            return;
        }
        var modelSkinBasic = modelSkin as SkinBasic;
        if (modelSkinBasic != null) {
            PlayFabManager.PlayFabManagerInstance.SpendCP(modelSkinBasic.GetCPcost());
            _TextPaymentBox.gameObject.SetActive(false);
            return;
        }
        Debug.LogError("model skin is not skinRoyal or skinBasic, skin collection: " + modelSkin.collection + "index: " + modelSkin.index);
    }

    private void SpendResult(bool success) {
        PlayFabManager.PlayFabManagerInstance.SpendResult -= SpendResult;
        if (success) {
            User.I.OwnSkin(modelSkin.collection, modelSkin.index);
            ChangeOnSkin(modelSkin.collection, modelSkin.index);
            UpdateSkinPanel();
            TryChangeOnSkin(modelSkin);
            ShowModelButton(_WearButton);
        }
    }

    public void Click_LockModelButton() {
        var modelSkinAward = modelSkin as SkinAward;
        if (modelSkinAward != null) {
            _TextLockTextBox.text = modelSkinAward.GetText_HowToUnlock();
            _SkinLockTextPanel.SetActive(true);
            return;
        } else {
            Debug.LogError("model skin is not skinaward, skin collection: " + modelSkin.collection + "index: " + modelSkin.index);
        }
    }


    #endregion


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy() {
        User.I.OwnNewSkin -= OwnNewSkinUpdate;
    }
}
