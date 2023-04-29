using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArrayLayout  {

	[System.Serializable]
	public struct rowData{
		public Color[] row;
	}

	public Color[] row = new Color[8]; //Grid of 8x8
	public rowData[] rows = new rowData[8]; //Grid of 8x8
}
