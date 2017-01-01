using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandyRotater : MonoBehaviour {

	// Use this for initialization
	public List<CandyObject> candy;
	public GameObject candyPrefab;

	public CandyContainer candyContainer;

	public float deg;

	private int cname = 1;
	

	void Start () {
		GenerateCandy (3);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Euler(0,0,deg);


		// input to shoot candy
		if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.W))
		{
			emitCandy();
			GenerateCandy(Random.Range(1,4));
		}




	}

	void OnGUI()
	{
		// control rotation of rotater
		if(Input.GetKey(KeyCode.A))
		{
			deg +=0.7f;
		}
		if(Input.GetKey(KeyCode.D))
		{
			deg -= 0.7f;
		}

		deg = deg < 0 ? deg + 360f : deg;
		deg = deg >= 360f ? deg - 360f: deg;
	}
	

	private void emitCandy()
	{
		//List<CandyObject> tempcandy = new List<CandyObject>(candy.ToArray());
		//candy.Clear ();
		//candy = null;
		candyContainer.AddCandy(candy,deg);
	}

	private void GenerateCandy(int count)
	{
		Debug.Log ("gene");
		candy = new List<CandyObject> (count);
		float disdeg = 360f / count;
		float deg = 0;
		for(int i = 0;i < count ;i++)
		{
			GameObject insCandy = Instantiate (candyPrefab) as GameObject;
			insCandy.name = cname.ToString();
			cname++;

			insCandy.transform.SetParent(transform);
			CandyObject co = insCandy.GetComponent<CandyObject>();
			co.SetColor(Random.Range(0,4));
			co.dislength = 0.3f;
			co.posdeg = deg;
			co.tarpos = deg;
			deg += disdeg;
			co.Aactive = true;
			candy.Add(co);

		}


	}
}
