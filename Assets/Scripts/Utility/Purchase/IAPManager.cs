using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour{
    #region singleton
    public static IAPManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }
    #endregion

    //product
    private string dm3 = "com.skimgame.ooomath.dm3";
    private string dm10 = "com.skimgame.ooomath.dm10";
    private string dm30 = "com.skimgame.ooomath.dm30";
    private string dm100= "com.skimgame.ooomath.dm100";

    public void OnPurchaseComplete(Product product) {
        if(string.Equals(product.definition.id, dm3, System.StringComparison.Ordinal)){
            PlayFabManager.PlayFabManagerInstance.AddDM(3);
            Debug.Log("Purchase 3 diamonds success");
        }else if (string.Equals(product.definition.id, dm10, System.StringComparison.Ordinal)){
            PlayFabManager.PlayFabManagerInstance.AddDM(10);
            Debug.Log("Purchase 10 diamonds success");
        } else if (string.Equals(product.definition.id, dm30, System.StringComparison.Ordinal)){
            PlayFabManager.PlayFabManagerInstance.AddDM(30);
            Debug.Log("Purchase 30 diamonds success");
        } else if (string.Equals(product.definition.id, dm100, System.StringComparison.Ordinal)){
            PlayFabManager.PlayFabManagerInstance.AddDM(100);
            Debug.Log("Purchase 100 diamonds success");
        } else {
            Debug.LogError("Error in Purchasing");
        }
            
    }


    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason) {
        Debug.Log("purchase of" + product.definition.id + "failed due to" + reason);
    }
}


