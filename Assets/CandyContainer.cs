using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandyContainer : MonoBehaviour {

	//container for candies

	public int initNum = 10;
	public GameObject candyPrefab;
	public List<CandyObject> candy = new List<CandyObject>();


	public float checkMatchDelayTime = 1f;
	void Start () {
		initCandy ();
		Invoke ("checkMatch", checkMatchDelayTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void initCandy()
	{
		float degdis = 360f / initNum;
		float deg = 0;
		for(int i = 0;i < initNum ; i++)
		{
			GameObject inscandy = Instantiate(candyPrefab) as GameObject;
			inscandy.transform.SetParent (transform);
			CandyObject co = inscandy.GetComponent<CandyObject> ();
			co.SetColor(Random.Range(0,4));
			candy.Add (co);
			co.posdeg = deg;
			co.SetTarPos(deg);
			deg += degdis;
			co.Aactive = true;
		}


	}

	public void AddCandy(List<CandyObject> c,float deg)
	{
		Debug.Log ("add");
		foreach(CandyObject co in c)
		{
			co.transform.SetParent(transform);
			co.Aactive = false;
			co.posdeg += deg;
			co.dislength = 1f;
		}
		Debug.Log ("clear");
		candy.AddRange (c);
		c.Clear ();
		c = null;

		//importent!!
		candy.Sort (CandySort);

		refreshCandy ();
		Invoke ("checkMatch", checkMatchDelayTime);
	}

	//refresh candy position
	private void refreshCandy()
	{
		float degdis = 360f / candy.Count;
		float deg = 0;
		for(int i = 0;i < candy.Count; i++)
		{
			candy[i].Aactive = false;
			candy[i].SetTarPos(deg);
			deg += degdis;
		}
		for(int i = 0;i < candy.Count; i++)
		{
			candy[i].Aactive = true;
		}
	}

	//sort candy by posdeg 
	public static int CandySort(CandyObject c1,CandyObject c2)
	{
		float f1 = c1.posdeg % 360f;
		float f2 = c2.posdeg % 360f;
		return f1.CompareTo(f2);
	}


	//check candy matches 
	private void checkMatch()
	{

		bool hasMatch = false;

		List<int> matchindexs = new List<int> ();

		int candycount = candy.Count;
		if(candycount <3)
		{
			return;
		}


		//check the first and last candy match

		int lastmatch = 0; //count candy num match
		int k = 1;

		bool lastallmatch = false;
		if(candy[0].ctype == candy[candycount-1].ctype)
		{
			lastmatch++;


			while(true)
			{
				if(candy[candycount - k].ctype == candy[candycount - k - 1].ctype)
				{
					lastmatch++;
				}
				else
				{
					break;
				}
				if(k >= candycount-1)
				{
					lastallmatch = true;
					break;
				}
				k++;
			}
		}


		for(int i =0 ;i <candy.Count - k ;i++)
		{

			if(candy[i].ctype == candy[i+1].ctype)
			{
				lastmatch++;
				if(i == (candy.Count - k - 1) && lastmatch >=2)
				{
					for(int n = i-lastmatch;n <= i;n++)
					{
						matchindexs.Add(n);
						
					}
					hasMatch = true;
					Debug.Log("Find " +(lastmatch + 1).ToString()+" matches");
				}
			}
			else
			{
				if(lastmatch >=2)
				{
					for(int n = i-lastmatch;n <= i;n++)
					{
						matchindexs.Add(n);

					}
					hasMatch = true;
					Debug.Log("Find " +(lastmatch + 1).ToString()+" matches");
				}
				lastmatch = 0;
			}

		}
		//clear
		for(int x = 0;x< matchindexs.Count;x++)
		{
			matchindexs[x]+= candycount;
			matchindexs[x] %= candycount;

		}

		matchindexs.Sort (IntSort);

		//REMOVE
		int indexoffset = 0;
		for(int a =0;a< matchindexs.Count;a++)
		{
			//Debug.Log("add" + matchindexs[a]);
			int reindex = matchindexs[a] + indexoffset;
			//candy[reindex].transform.localScale = new Vector3(0.3f,0.5f);
			DestroyObject(candy[reindex].gameObject);
			indexoffset--;
			candy.RemoveAt (reindex);
		}

		refreshCandy ();

		if(hasMatch)
		{
			Invoke("checkMatch",1f);
		}
	}


	public static int IntSort(int i1,int i2)
	{
		return i1.CompareTo(i2);
	}

}
