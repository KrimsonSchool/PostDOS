using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColourArray
{

    [System.Serializable]
    public struct rowData
    {
        public Color[] row;
    }

    public rowData[] rows = new rowData[2];
}