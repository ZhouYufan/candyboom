using UnityEngine;
using System.Collections;

public class CandyObject : MonoBehaviour {

	//radius from rotation center
	public float dislength = 1f;

	//angle of rotation
	public float posdeg;
	//target angle for smooth move
	public float tarpos = 0;
	//angle speed for smooth move
	private float posSpeed = 0;

	//interpolation of animation
	public Vector3 transformpos;

	//control animation caculation
	public bool Aactive = false;
	
	//candy type
	public int ctype = 1;
	// Use this for initialization
	void Start () {
	}
	
	//set candy color and type
	public void SetColor(int t)
	{
		ctype = t;
		GetComponent<SpriteRenderer> ().sprite = GameObject.FindGameObjectWithTag ("Config").GetComponent<CandyBoom> ().candyCharge [t];
		//GetComponent<SpriteRenderer> ().color = GameObject.FindGameObjectWithTag ("Config").GetComponent<CandyBoom> ().colors [t];
	}
	
	// Update is called once per frame
	void Update () {
		//smooth Move;
		if(!active)
		{
			return;
		}
		if(Mathf.Abs(posdeg - tarpos) < 0.05f)
		{
			posdeg = tarpos;
		}
		else
		{
			//angle interpolation 
			posdeg = Mathf.SmoothDamp (posdeg, tarpos, ref posSpeed, 0.3f);
		}

		Quaternion q = Quaternion.Euler (0, 0, posdeg);
		transformpos = q * new Vector3(0,dislength,0);
		//smooth move
		transform.localPosition += (transformpos - transform.localPosition) / 10f;
	}

	public void SetTarPos(float pos)
	{
		pos = pos % 360f;
		tarpos = pos;

		//fix pos
		posdeg = posdeg % 360f;
		if(Mathf.Abs(posdeg	- tarpos) <= 180)
		{
			return;
		}
		if(posdeg > tarpos)
		{
			posdeg-=360f;
		}
		else
		{
			posdeg+=360f;
		}
	}
}
