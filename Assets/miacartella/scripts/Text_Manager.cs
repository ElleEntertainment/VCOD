﻿using UnityEngine;
using System.Collections;
using System;


//Tutti gli aggiornamenti sui testi del game passeranno di qua
//---------------------------
public class Text_Manager : MonoBehaviour {

	public GUISkin customSkin;
	string playerLevel = "";
	string playerExp = "";
	string playerExpToNextLevel = "";
	string playerCurHealth = "";
	string playerToTHealth = "";
	string targetCurHealth = "";
	string targetTotHealth = "";
	string targetLevel = "";
	string targetExpGive = "";
	string targetDamage = "";
	string playerDamage = "";
	Vector3 mousePosition;
	float disappearTime;
	float tdisappearTime;
	float expdisappearTime;
	Rect buttonRect;
	Rect tbuttonRect;
	Rect expbuttonRect;
	bool playerHasTarget;
	// Use this for initialization
	void Start () {

		disappearTime = 0.7F;
		tdisappearTime = 0.7F;
		expdisappearTime = 0.7F;
		buttonRect = new Rect (0, 0, 300, 25);
		tbuttonRect = new Rect (320, 0, 300, 25);
		expbuttonRect = new Rect (Screen.width/2 - 200,Screen.height - 35, 400, 25);
		playerHasTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		//fa scomparire il testo del danno dopo un po' di tempo
		disappearTime -= Time.deltaTime;
		if (disappearTime <= 0) {
			playerDamage = "";
		}
		tdisappearTime -= Time.deltaTime;
		if (tdisappearTime <= 0) {
			targetDamage = "";
		}
		expdisappearTime -= Time.deltaTime;
		if (expdisappearTime <= 0) {
			targetExpGive = "";
		}
		mousePosition = Input.mousePosition;
        

	}

	void OnGUI(){
		//backup colors
		Color backupColor = GUI.color;
		Color backupContentColor = GUI.contentColor;
		Color backupBackgroundColor = GUI.backgroundColor;


		//--------- Player
		float curh = (float)Convert.ToInt32 (playerCurHealth);
		float toth = (float)Convert.ToInt32 (playerToTHealth);
		int percent = Mathf.RoundToInt((curh/toth) * 100);
		//Debug.Log (percent);
		GUI.skin = customSkin;
		GUI.color = Color.black;
		GUI.Box (buttonRect, "");
		GUI.Box (expbuttonRect, "");
		GUI.color = Color.green;
		GUI.Button (new Rect (0, 0, percent * 3 , 25), "");
		GUI.color = Color.black;
		GUI.Label (new Rect (5, -30, 80, 80), "LV. " + playerLevel);
		if (buttonRect.Contains(Event.current.mousePosition))
			GUI.Label (new Rect (100, -30, 80, 80), playerCurHealth + "/" + playerToTHealth);
		else
			GUI.Label (new Rect (110 , -30, 80, 80), percent + "%");

		GUI.color = Color.red;
		GUI.Label (new Rect (Screen.width / 2 - 20, Screen.height / 2 - 20, 40, 40), playerDamage);
		//Exp
		GUI.color = Color.magenta;
		int expInt = Convert.ToInt32 (playerExp);
		int expToNextInt = Convert.ToInt32 (playerExpToNextLevel);
		int expPercent = Mathf.RoundToInt (((float)expInt / (float)expToNextInt) * 100);
		GUI.Label (new Rect (Screen.width / 2, Screen.height - 100, 40, 40), targetExpGive);
		if(expPercent > 0)
			GUI.Button (new Rect (Screen.width/2 - 200,Screen.height - 35, expPercent * 4 , 25), "");
		GUI.color = Color.white;
		GUI.Label (new Rect (Screen.width /2 - 40 , Screen.height -65, 40, 40), playerExp + "/" + playerExpToNextLevel);
		//-------- Infetto
		if (playerHasTarget) {
						float tcurh = (float)Convert.ToInt32 (targetCurHealth);
						float ttoth = (float)Convert.ToInt32 (targetTotHealth);
						int tpercent = Mathf.RoundToInt ((tcurh / ttoth) * 100);
						//Debug.Log (percent);
						GUI.color = Color.black;
						GUI.Box (tbuttonRect, " ");
						GUI.color= Color.red;
						GUI.Button (new Rect (320, 0, tpercent * 3, 25), " ");
						GUI.color = Color.black;
						GUI.Label (new Rect (325, -30, 80, 80), "LV. " + targetLevel);
						if (tbuttonRect.Contains (Event.current.mousePosition))
								GUI.Label (new Rect (420, -30, 80, 80), targetCurHealth + "/" + targetTotHealth);
						else
								GUI.Label (new Rect (430, -30, 80, 80), tpercent + "%");
						GUI.color = Color.white;
						GUI.Label (new Rect (Screen.width / 2 , Screen.height / 2 , 40, 40), targetDamage);
		}
		//Reset color
		GUI.color = backupColor;
		GUI.contentColor = backupContentColor;
		GUI.backgroundColor = backupBackgroundColor;


	}
	//funzione che chiama il player quando deve aggiornare i suoi dati
	void playerText(string value) {
		char[] del = {'-'};
		string[] values = value.Split(del);
        playerCurHealth = values [0];
		playerToTHealth = values [1];
		playerDamage = values [2];
		playerLevel = values [3];
		playerExp = values [4];
		playerExpToNextLevel = values [5];
		disappearTime = 0.7F;
	}

	//funzione che chiama l'infetto quando deve aggiornare i suoi dati
	void targetText(string value) {
		char[] del = {'-'};
		string[] values = value.Split(del);
		targetCurHealth = values [0];
		targetTotHealth = values [1];
		targetDamage = values [2];
		targetLevel = values [3];
		targetExpGive = values [4];
		tdisappearTime = 0.7F;
		expdisappearTime = 1.0F;
	}
	public void setTargetTrue(bool hasTarget){
		playerHasTarget = hasTarget;
	}
	public bool getTargetTrue(){
		return playerHasTarget;
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