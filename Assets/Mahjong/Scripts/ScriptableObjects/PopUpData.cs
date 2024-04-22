using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PopUpData : ScriptableObject
{
    public string Title => title;
    public string Description => description;
    public PopUpType PopUpType => questionType;

    [SerializeField] private string title;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private PopUpType questionType;



}

public enum PopUpType
{
    YesNo, Confirm
}

