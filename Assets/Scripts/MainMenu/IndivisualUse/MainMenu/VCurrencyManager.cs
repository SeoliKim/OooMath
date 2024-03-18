using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class VCurrencyManager : MonoBehaviour {
    #region singleton
    public static VCurrencyManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }
    #endregion

    [Header("message box")]
    [SerializeField] private GameObject _MessageBox;
    [SerializeField] private GameObject _PaymentConfirmPanel;
    [SerializeField] private TMP_Text _TextPaymentBox;

    [SerializeField] private GameObject _NotEnoughPanel;

    private void Start() {
        PlayFabManager.PlayFabManagerInstance.TryGetVirtualCurrency();
        SetDiamondMachine();
    }

    #region Purchase Item
    private ShopItem item;
    public void BuyItem(ShopItem item) {
        AudioManager.AudioManagerInstance.PlayAudio("button-pressed");
        this.item = item;
        Open_PaymentConfirmPanel(item.GetCostText());
    }

    private void Yes_ConfirmResponse_InClass() {
        if (item != null) {
            int cost = item.GetCostAmount();
            int costType = item.GetCostType();
            if (costType == 1) {
                PlayFabManager.PlayFabManagerInstance.SpendCP(cost);
            } else if (costType == 2) {
                PlayFabManager.PlayFabManagerInstance.SpendDM(cost);
            } else {
                Debug.LogError("Error when identify the cost type of the purchase");
            }
            PlayFabManager.PlayFabManagerInstance.SpendResult += SpendResult;
        }
    }

    private void SpendResult(bool success) {
        PlayFabManager.PlayFabManagerInstance.SpendResult -= SpendResult;
        //item
        if (item != null) {
            if (success) {
                PurchaseSuccess(item);
            }
            item = null;
        }


        //game
        if (game != null) {
            if (success) {
                SceneLoader.instance.LoadScene(game.GetSceneIndex());
                AudioManager.AudioManagerInstance.PlayAudio("startGame");
            } else {
                Open_NotEnoughPanel(0);
            }
            game = null;
        }


    }



    private void PurchaseSuccess(ShopItem item) {
        int itemType = item.GetItemType();
        int amount = item.GetItemAmount();
        if (itemType == 0) {
            PlayFabManager.PlayFabManagerInstance.AddBP(amount);
        } else if (itemType == 1) {
            PlayFabManager.PlayFabManagerInstance.AddCP(amount);
        } else if (itemType == 2) {
            PlayFabManager.PlayFabManagerInstance.AddDM(amount);
        } else {
            Debug.LogError("Error when identify the item type of the purchase");
        }
    }

    #endregion

    #region Payment Confirm
    public event Action Yes_ConfirmResponse;

    public void Open_PaymentConfirmPanel(string paymentText) {
        _MessageBox.SetActive(true);
        foreach (Transform child in _MessageBox.transform) {
            child.gameObject.SetActive(false);
        }
        _PaymentConfirmPanel.SetActive(true);
        _TextPaymentBox.text = paymentText;
    }

    public void Click_Yes_PaymentBox() {
        Yes_ConfirmResponse?.Invoke();
        Yes_ConfirmResponse_InClass();
    }

    public void Click_No_PaymentBox() {
        _PaymentConfirmPanel.SetActive(false);
        _MessageBox.SetActive(false);
    }

    #endregion

    #region Not Enough panel
    [Space]
    [Header("Link to shop panel")]
    [SerializeField] private TouchMenuSelector _ShopTouchMenuSelector;
    [SerializeField] private ShopPage shopPage;

    public void Open_NotEnoughPanel(int index) {// 0- brainPower   1- chips or diamonds
        _MessageBox.SetActive(true);
        foreach (Transform child in _MessageBox.transform) {
            child.gameObject.SetActive(false);
        }
        _NotEnoughPanel.SetActive(true);
        AudioManager.AudioManagerInstance.PlayAudio("wrong");

        _ShopTouchMenuSelector.OpenPage();
        if (index == 0) {
            shopPage.OnBrainPanel();
        } else {
            shopPage.OnDiamondPanel();
        }
    }

    public void Close_NotEnoughPanel() {
        _NotEnoughPanel.SetActive(false);
        _MessageBox.SetActive(false);
    }

    #endregion

    #region Diamond Machine

    [Header("Diamond Machine")]
    [SerializeField] GameObject _CPTradePanel;
    [SerializeField] GameObject _DMTradePanel;
    [SerializeField] TMP_Text _resultDM, _resultCP, _CPInput, _DMInput;
    private int CPInput, DMInput;

    private void SetDiamondMachine() {
        CPInput = 0;
        DMInput = 0;
        _CPTradePanel.SetActive(true);
        _DMTradePanel.SetActive(false);
        SetDMMachineUI();
    }

    private void SetDMMachineUI() {
        int CPInput1000 = CPInput * 1000;
        _CPInput.text = CPInput1000.ToString();
        _resultDM.text = CPInput.ToString();

        _DMInput.text = DMInput.ToString();
        int CPresult1000 = DMInput * 1000;
        _resultCP.text = CPresult1000.ToString();
    }

    public void Click_Add_Button() {
        if (_CPTradePanel.activeSelf) {
            CPInput = CPInput + 1;
            CPInput = CheckCPInput(CPInput);
        }
        if (_DMTradePanel.activeSelf) {
            DMInput = DMInput + 1;
            DMInput = CheckDMInput(DMInput);
        }
        SetDMMachineUI();
    }

    public void Click_Minus_Button() {
        if (_CPTradePanel.activeSelf) {
            CPInput = CPInput -1;
            CPInput = CheckCPInput(CPInput);
        }
        if (_DMTradePanel.activeSelf) {
            DMInput = DMInput - 1;
            DMInput = CheckDMInput(DMInput);
        }
        SetDMMachineUI();
    }

    private int CheckCPInput(int v) {
        int CPInput1000 = v * 1000;
        int cpAmount = User.I.chips;
        if (CPInput1000 > cpAmount) {
            return (v-1);
        }
        if (CPInput1000 < 0) {
            return 0;
        }
        return v;
    }

    private int CheckDMInput(int v) {
        int dmAmount = User.I.diamonds;
        if (v> dmAmount) {
            return (v - 1);
        }
        if (dmAmount < 0) {
            return 0;
        }
        return v;
    }

    public void Click_Change_Button() {
        _CPTradePanel.SetActive(!_CPTradePanel.activeSelf);
        _DMTradePanel.SetActive(!_DMTradePanel.activeSelf);
    }

    public void Click_Yes_Button() {
        if (_CPTradePanel.activeSelf) {
            int CPInput1000 = CPInput * 1000;
            PlayFabManager.PlayFabManagerInstance.SpendCP(CPInput1000);
            PlayFabManager.PlayFabManagerInstance.AddDM(CPInput);
        }

        if (_DMTradePanel.activeSelf) {
            PlayFabManager.PlayFabManagerInstance.SpendDM(DMInput);
            int DMInput1000 = DMInput * 1000;
            PlayFabManager.PlayFabManagerInstance.AddCP(DMInput);
        }

    }
    #endregion

    #region brain power
    private Game game;
    public void PlayGame(Game game) {
        this.game = game;
        PlayFabManager.PlayFabManagerInstance.SpendBP(game.GetBPCost());
        PlayFabManager.PlayFabManagerInstance.SpendResult += SpendResult;

    }
    
    #endregion

}
