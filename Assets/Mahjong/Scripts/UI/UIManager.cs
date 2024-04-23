using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Initialization
    public static UIManager Instance => instance;
    private static UIManager instance;
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    public UIPopUp PopUpWindow => popUpWindow;
    [SerializeField] private UIPopUp popUpWindow;

    [Header("Global Pop Up Callers")]
    [SerializeField] private UIPopUpCaller winPopUpCaller;
    [SerializeField] private UIPopUpCaller defeatPopUpCaller;
    private void Start()
    {
        defeatPopUpCaller.OnConfirm.AddListener(GameManager.Instance.ResetGame);
        winPopUpCaller.OnConfirm.AddListener(GameManager.Instance.ResetGame);

        GameManager.Instance.OnGameOver += (win) => 
        {
            if(win)
            {
                PopUpWindow.ShowPopUp(winPopUpCaller);
            }
            else
            {
                PopUpWindow.ShowPopUp(defeatPopUpCaller);
            }
        };
    }
}
