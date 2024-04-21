using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayoutInfo
{
    public BoolRow[] rows;
}

[System.Serializable]
public class BoolRow
{
    public bool[] columns;
}

