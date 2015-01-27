using UnityEngine;
using System.Collections;
using System;


//Tutti gli aggiornamenti sui testi del game passeranno di qua
//---------------------------
public class Text_Manager : MonoBehaviour {

	public GUISkin customSkin;
	string playerCurHealth;
	string playerToTHealth;
	string targetCurHealth;
	string targetTotHealth;
	string targetDamage;
	string playerDamage;
	Vector3 mousePosition;
	float disappearTime;
	Rect buttonRect;
	// Use this for initialization
	void Start () {

		disappearTime = 0.7F;
		buttonRect = new Rect (0, 0, 300, 25);
	}
	
	// Update is called once per frame
	void Update () {
		//fa scomparire il testo del danno dopo un po' di tempo
		disappearTime -= Time.deltaTime;
		if (disappearTime <= 0) {
			playerDamage = "";
		}
		mousePosition = Input.mousePosition;
        

	}

	void OnGUI(){
		float curh = (float)Convert.ToInt32 (playerCurHealth);
		float toth = (float)Convert.ToInt32 (playerToTHealth);
		int percent = Mathf.RoundToInt((curh/toth) * 100);
		//Debug.Log (percent);
		GUI.skin = customSkin;
		GUI.color = Color.black;
		GUI.Box (buttonRect, " ");
		GUI.color = Color.green;
		GUI.Button (new Rect (0, 0, percent * 3 , 25), " ");
		GUI.color = Color.black;
		if (buttonRect.Contains(Event.current.mousePosition))
			GUI.Label (new Rect (100, -30, 80, 80), playerCurHealth + "/" + playerToTHealth);
		else
			GUI.Label (new Rect (110 , -30, 80, 80), percent + "%");

		GUI.color = Color.red;
		GUI.Label (new Rect (Screen.width / 2 - 20, Screen.height / 2 - 20, 40, 40), playerDamage);

	}
	//funzione che chiama il player quando deve aggiornare i suoi dati
	void playerText(string value) {
		char[] del = {'-'};
		string[] values = value.Split(del);
        playerCurHealth = values [0];
		playerToTHealth = values [1];
		playerDamage = values [2];
		disappearTime = 0.7F;
	}

	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
}
