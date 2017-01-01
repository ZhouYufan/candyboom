using UnityEngine;
using System.Collections;

public class CandyBoom : MonoBehaviour {
	public Sprite[] candyCharge;
	public Color[] colors;
	public int typecount = 3;
	public static Color[] Colors;
	public static int TypeCount;
	void Start () {
		Colors = colors;
		TypeCount = typecount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
