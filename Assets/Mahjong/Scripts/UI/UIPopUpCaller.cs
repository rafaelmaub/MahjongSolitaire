using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UIPopUpCaller 
{
    public PopUpData popUpData;
    public UnityEvent OnYes;
    public UnityEvent OnNo;
    public UnityEvent OnConfirm;

    public void CallPopUp()
    {
        UIManager.Instance.PopUpWindow.ShowPopUp(this);
    }
}
