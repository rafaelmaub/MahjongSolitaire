using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class UIPopUp : MonoBehaviour
{
    private UnityEvent OnConfirm;
    private UnityEvent OnYes;
    private UnityEvent OnNo;

    [SerializeField] private Animator animator;
    private const string AnimTriggerClose = "Close";
    private const string AnimTriggerOpen = "Open";

    [Header("UI Parts")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject confirmBtn;
    [SerializeField] private GameObject yesBtn;
    [SerializeField] private GameObject noBtn;
    public void ShowPopUp(UIPopUpCaller call)
    {
        
        OnYes = call.OnYes;
        OnNo = call.OnNo;
        OnConfirm = call.OnConfirm;

        
        title.text = call.popUpData.Title;
        description.text = call.popUpData.Description;
        switch(call.popUpData.PopUpType)
        {
            case PopUpType.YesNo:
                yesBtn.SetActive(true);
                noBtn.SetActive(true);
                confirmBtn.SetActive(false);
                break;
            case PopUpType.Confirm:
                yesBtn.SetActive(false);
                noBtn.SetActive(false);
                confirmBtn.SetActive(true);
                break;
        }

        animator.SetTrigger(AnimTriggerOpen);
    }

    public void Btn_OnYes()
    {
        OnYes?.Invoke();
        ClosePopUp();
    }
    public void Btn_OnNo()
    {
        OnNo?.Invoke();
        ClosePopUp();
    }

    public void Btn_OnConfirm()
    {
        OnConfirm?.Invoke();
        ClosePopUp();
    }

    void ClosePopUp()
    {
        animator.SetTrigger(AnimTriggerClose);
    }
}
