using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial0CanvaManager : MonoBehaviour
{
    [SerializeField] private GameObject _Phone3DStep2, _Phone3DStep3, _Phone3DStep5, _Phone3DStep6;

    private void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }

    //Oootalk
    public IEnumerator Step0() {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return null; 
    }

    //move
    public IEnumerator Step1() {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
    }

    //phone front
    public IEnumerator Step2() {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        Animator animator = _Phone3DStep2.GetComponent<Animator>();
        animator.Play("PhoneFront");
        yield return null;
    }

    //fall down
    public IEnumerator Step3() {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
    }

    //phone back
    public IEnumerator Step4() {
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        Animator animator = _Phone3DStep3.GetComponent<Animator>();
        animator.Play("PhoneBack");
        yield return null;
    }

    //phone right
    public IEnumerator Step5() {
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(true);
        Animator animator = _Phone3DStep5.GetComponent<Animator>();
        animator.Play("PhoneRight");
        yield return null;
    }

    //phone left
    public IEnumerator Step6() {
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(true);
        Animator animator = _Phone3DStep6.GetComponent<Animator>();
        animator.Play("PhoneLeft");
        yield return null;
    }

    //diamond chip
    public IEnumerator Step7() {
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(7).gameObject.SetActive(true);
        yield return null;
    }

    // show goal
    public IEnumerator Step8() {
        transform.GetChild(7).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        transform.GetChild(8).gameObject.SetActive(false);
    }

    //goal complete
    public IEnumerator Step9() {
        transform.GetChild(8).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(true);
        yield return null;
    }


}
