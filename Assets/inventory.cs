using UnityEngine;
using System.Collections;

public class inventory : MonoBehaviour {
	public static int charge = 0;
	public AudioClip collectsound;
	public Texture2D[] hudCharge;
	public GUITexture chargeHudGUI;
	public Texture2D[] meterCharge;
	public Renderer meter;
	public Light doorlight;
	bool haveMatches=false;
	public GUITexture matchGUIprefab;
	public GUIText textHints;
	GUITexture matchGUI;
	public bool firelit=false;
	public GameObject winObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void CellPickup()
	{
		AudioSource.PlayClipAtPoint (collectsound,transform.position);
		charge++;
		print (charge);
		chargeHudGUI.texture=hudCharge[charge];
		meter.material.mainTexture =meterCharge[charge];
		if (inventory.charge == 4) 
		{
			doorlight.color = Color.green;
		}
		if (inventory.charge == 1) 
		{
			chargeHudGUI.enabled=true;
		}
	}


	void MatchPickup()
	{
		haveMatches = true;
		AudioSource.PlayClipAtPoint(collectsound,transform.position);
		GUITexture matchHUD = Instantiate (matchGUIprefab, new Vector3 (0.15f, 0.47f, 0), transform.rotation)as GUITexture;
		matchGUI = matchHUD;
	}

	void OnControllerColliderHit(ControllerColliderHit col)
	{
		if (col.gameObject.name == "campfire") 
		{
			if(haveMatches==true&&!firelit)
			{
			   LightFire (col.gameObject);
				firelit=true;
			}
			if(haveMatches==false)
			{
				textHints.SendMessage("ShowHint","I could use this campfire to signal for help");
			}
		}
	}
	void LightFire(GameObject campfire)
	{
		winObj.SendMessage ("GameOver");
		ParticleEmitter[] fireEmitters;//两套粒子系统组件定义成数组
		fireEmitters=campfire.GetComponentsInChildren<ParticleEmitter>();//找到子级的emiter
		foreach (ParticleEmitter emitter in fireEmitters) //每一个火焰发射器的emitter
		{
			emitter.emit=true;
		}
		campfire.audio.Play ();
		Destroy (matchGUI);
//		haveMatches=false;
	}
}










